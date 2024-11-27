using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Palleoptimering.Models
{
    public class XmlOrderLoader
    {
        public List<Order> LoadOrdersFromXml()
        {
            try
            {
                
                string filePath = @"C:\Users\UMUTCAN\OneDrive - Erhvervsakademi MidtVest\Dokumenter\GitHub\Palleoptimering\Palleoptimering\wwwroot\orders.xml";

                // Kontrollér om filen eksisterer
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("XML filen blev ikke fundet.", filePath);
                }

                XDocument doc = XDocument.Load(filePath);

                return doc.Descendants("Order")
                          .Select(o => new Order(
                              (int)o.Element("OrderId"),
                              DateTime.Parse((string)o.Element("OrderDate")),
                              (string)o.Element("Status"),
                              o.Element("ElementIds").Elements("ElementId").Select(e => (int)e).ToList()
                          ))
                          .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Fejl ved indlæsning af ordrefilen", ex);
            }
        }
    }
}
