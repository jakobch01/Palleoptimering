using System.IO;
using System.Xml.Linq;

namespace Palleoptimering.Models
{
    public class XmlElementLoader
    {
        public List<Element> LoadElementsFromXml()
        {
            try
            {
                // Brug den faktiske sti til din XML-fil
                string filePath = @"C:\Users\UMUTCAN\OneDrive - Erhvervsakademi MidtVest\Dokumenter\GitHub\Palleoptimering\Palleoptimering\wwwroot\elements.xml";

                // Kontrollér om filen eksisterer
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("XML filen blev ikke fundet.", filePath);
                }

                XDocument doc = XDocument.Load(filePath);

                return doc.Descendants("Element")
                          .Select(e => new Element(
                              (int)e.Element("Id"),
                              (int)e.Element("Length"),
                              (int)e.Element("Width"),
                              (int)e.Element("Height"),
                              Enum.Parse<RotationOption>((string)e.Element("CanRotate")),
                              (bool)e.Element("SpecialPallet"),
                              (string)e.Element("PalletType"),
                              (int)e.Element("MaxPerPallet"),
                              (string)e.Element("Group"),
                              (int)e.Element("Weight")
                          ))
                          .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Fejl ved indlæsning af XML-fil", ex);
            }
        }
    }
}





