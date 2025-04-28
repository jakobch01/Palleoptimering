using Palleoptimering.Models;

namespace Palleoptimering.Services
{
    public class PalletOptimizer
    {
        public List<PlacedElement> Optimize(Pallet pallet, List<Element> elements, PalletSettings settings)
        {
            var placedElements = new List<PlacedElement>();
            int currentLayer = 0;
            int currentX = 0;
            int currentY = 0;
            int layerHeight = 0;

            // 1. Sortér elementer baseret på strategi
            var sortedElements = elements
                .Where(e => CanPlaceOnPallet(e, pallet, settings))
                .OrderByDescending(e => settings.RowDistribution == RowDistributionType.LongestOutside ? e.Depth : e.Weight)
                .ToList();

            foreach (var element in sortedElements)
            {
                bool fitsNormal = Fits(element.Width, element.Depth, currentX, currentY, pallet.Width, pallet.Length);
                bool canRotate = settings.MaxWeightAllowedToRotate >= element.Weight && element.Rotation != RotationBehavior.NotAllowed;
                bool fitsRotated = canRotate && Fits(element.Depth, element.Width, currentX, currentY, pallet.Width, pallet.Length);

                bool placed = false;
                if (fitsNormal || fitsRotated)
                {
                    int w = fitsNormal ? element.Width : element.Depth;
                    int d = fitsNormal ? element.Depth : element.Width;

                    placedElements.Add(new PlacedElement
                    {
                        Element = element,
                        X = currentX,
                        Y = currentY,
                        Layer = currentLayer,
                        Rotated = !fitsNormal
                    });

                    currentX += w + settings.SpacingBetweenElements;
                    layerHeight = Math.Max(layerHeight, element.Height);
                    placed = true;
                }

                if (!placed)
                {
                    // Gå til ny række eller nyt lag
                    currentX = 0;
                    currentY += layerHeight + settings.SpacingBetweenElements;

                    if (currentY >= pallet.Length)
                    {
                        currentLayer++;
                        currentY = 0;
                        if (currentLayer >= settings.MaxLayers)
                            break;
                    }
                    layerHeight = 0;
                }
            }

            return placedElements;
        }

        private bool Fits(int w, int d, int x, int y, int palletW, int palletL)
        {
            return x + w <= palletW && y + d <= palletL;
        }

        private bool CanPlaceOnPallet(Element e, Pallet pallet, PalletSettings settings)
        {
            if (e.RequiresSpecialPallet && !pallet.IsSpecial)
                return false;

            if (e.Width > pallet.Width + settings.MaxOverhang)
                return false;

            if (e.Depth > pallet.Length + settings.MaxOverhang)
                return false;

            if (e.Height + pallet.Height > settings.MaxStackingHeight)
                return false;

            return true;
        }

    }
}