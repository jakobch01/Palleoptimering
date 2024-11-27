using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Palleoptimering.Models
{
    public enum RotationOption
    {
        Nej,
        Ja,
        Skal

    }
    
        public class Element
        {
            public int ElementId { get; set; }
            public int Length { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public RotationOption CanRotate { get; set; }
            public bool SpecialPallet { get; set; }
            public string PalletType { get; set; }
            public int MaxPerPallet { get; set; }
            public string Group { get; set; }
            public int Weight { get; set; }
            public Element() { }
            public Element(int elementId, int length, int width, int height, RotationOption canRotate, bool specialPallet, string palletType, int maxPerPallet, string group, int weight)
            {
                ElementId = elementId;
                Length = length;
                Width = width;
                Height = height;
                CanRotate = canRotate;
                SpecialPallet = specialPallet;
                PalletType = palletType;
                MaxPerPallet = maxPerPallet;
                Group = group;
                Weight = weight;
            }

            
            public bool IsHeavyElement()
            {
                
                return Weight > 100;
            }
        }
    }

