namespace Palleoptimering.Models
{
	public class Pallet
	{
		public int Id { get; set; }
		public string PalletDescription { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Length { get; set; }
		public int PalletGroup { get; set; }
		public string PalletType { get; set; }
		public int Weight { get; set; }
		public int MaxHeight { get; set; }
		public int MaxWeight { get; set; }
		public int Overmeasure { get; set; }
		public int AvailableSpaces { get; set; }
		public bool SpecialPallet { get; set; }
		public int SpaceBetweenElements { get; set; }
		public bool Active { get; set; }
	}
}

