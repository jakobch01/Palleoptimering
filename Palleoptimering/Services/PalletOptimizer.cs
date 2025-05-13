using Palleoptimering.Models.Domain;

namespace Palleoptimering.Services
{
    public class PalletOptimizer
    {
        private readonly List<Pallet> _availablePallets;
        private readonly PalletSettings _settings;

        public PalletOptimizer(List<Pallet> pallets, PalletSettings settings)
        {
            _availablePallets = pallets.Where(p => p.IsActive).ToList();
            _settings = settings;
        }

		public OptimizationResult Optimize(List<Element> elements)
		{
			var results = new OptimizationResult();
			var groupedElements = elements.GroupBy(e => e.OptimizationGroup);

			foreach (var group in groupedElements)
			{
				var sorted = group
					.OrderBy(e => e.Series)
					.ThenBy(e => e.Rotation)
					.ThenBy(e => e.Height)
					.ThenBy(e => e.Weight)
					.ToList();

				foreach (var element in sorted)
				{
					bool placed = false;

					foreach (var result in results.PackedPallets)
					{
						if (CanPlaceOnPallet(result, element))
						{
							PlaceOnPallet(result, element);
							placed = true;
							break;
						}
					}

					if (!placed)
					{
						var pallet = GetBestFittingPallet(element);
						if (pallet != null)
						{
							var newResult = new PackingResult { Pallet = pallet };
							if (CanPlaceOnPallet(newResult, element))
							{
								PlaceOnPallet(newResult, element);
								results.Add(newResult);
							}
							else
							{
								results.UnplacedElements.Add(element);
							}
						}
						else
						{
							results.UnplacedElements.Add(element);
						}
					}
				}
			}

			return results;
		}


		private Pallet GetBestFittingPallet(Element element)
        {
            return _availablePallets
                .Where(p =>
                    element.Width <= p.Width + p.Overhang &&
                    element.Depth <= p.Length + p.Overhang &&
                    (!element.RequiresSpecialPallet || p.IsSpecial) &&
                    (string.IsNullOrEmpty(element.PalletType) || p.Type.ToString() == element.PalletType))
                .OrderBy(p => p.Width * p.Length)
                .FirstOrDefault();
        }

        private bool CanPlaceOnPallet(PackingResult result, Element element)
        {
            var newHeight = result.CurrentHeight + element.Height + result.Pallet.SpacingBetweenElements;
            var newWeight = result.CurrentWeight + element.Weight;
            var newLayer = result.CurrentLayers + 1;

            if (element.IsGeometric && result.Elements.Any())
                return false; 

            if (newHeight > result.Pallet.MaxHeight)
                return false;

            if (newWeight > result.Pallet.MaxWeight)
                return false;

            if (newLayer > _settings.MaxLayers)
                return false;

            if (element.MaxElementsPerPallet.HasValue &&
                result.Elements.Count >= element.MaxElementsPerPallet.Value)
                return false;

            return true;
        }

        private void PlaceOnPallet(PackingResult result, Element element)
        {
            bool shouldRotate = ShouldRotate(element, result.Pallet);

            // Udskift mål hvis element roteres
            int elementHeight = shouldRotate ? element.Width : element.Height;

            result.Elements.Add(element);
            result.CurrentHeight += elementHeight + result.Pallet.SpacingBetweenElements;
            result.CurrentWeight += element.Weight;

            if (result.Elements.Count % 2 == 0)
                result.CurrentLayers++; 
        }

        private bool ShouldRotate(Element element, Pallet pallet)
        {
            if (element.Rotation == RotationBehavior.NotAllowed)
                return false;

            if (element.Rotation == RotationBehavior.Required)
                return true;

            if (element.Weight > _settings.MaxWeightAllowedToRotate)
                return false;

            // Hvis rotering er påkrævet pga. max højde
            if (element.Height > pallet.MaxElementHeight &&
                _settings.AllowRotationWhenExceedingMaxHeight)
                return true;

            bool isSingle = element.MaxElementsPerPallet == 1 || false;
            if (_settings.HeightWidthFactorOnlyForSingleElements && !isSingle)
                return false;

            double factor = (double)Math.Min(element.Width, element.Height) / Math.Max(element.Width, element.Height);
            return factor < _settings.HeightWidthFactor;
        }
    }
}