using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Palleoptimering.Controllers;
using Palleoptimering.Models;
using Xunit;

namespace PalletTest
{
    public class Test
    {
       
        [Fact]
        public void CreateOrder_ShouldWriteToXML()
        {
            // Arrange
            string filePath = "orders.xml";s
            XDocument doc = new XDocument(new XElement("Orders"));
            doc.Save(filePath);

            var newOrder = new
            {
                OrderId = 1,
                Status = "Pending",
                OrderDate = DateTime.Now
            };

            // Act
            XElement newOrderElement = new XElement("Order",
                new XElement("OrderId", newOrder.OrderId),
                new XElement("Status", newOrder.Status),
                new XElement("OrderDate", newOrder.OrderDate.ToString("o")) // ISO 8601 format
            );

            doc.Element("Orders").Add(newOrderElement);
            doc.Save(filePath);

            // Assert
            var createdOrder = doc.Element("Orders")?
                .Elements("Order")
                .FirstOrDefault(x => (int)x.Element("OrderId") == 1);

            Assert.NotNull(createdOrder);
            Assert.Equal("Pending", (string)createdOrder.Element("Status"));
            Assert.Equal(1, (int)createdOrder.Element("OrderId"));
        }

        [Fact]
        public void AddPallet_ShouldAddPalletToDatabase()
        {
            // Arrange: Opretter en In-Memory DbContext for at simulere database
            var options = new DbContextOptionsBuilder<PalletDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")  // En midlertidig database til test
                .Options;

            // Mock for ApplicationDbContext
            var mockDbContext = new PalletDbContext(options);

            // Opretter en instans af service-klassen
            var EFPalletRepository = new EFPalletRepository(mockDbContext);

            // Opretter en palle at tilføje
            var pallet = new Pallet
            {
                Id = 1,
                PalletDescription = "Test Pallet",
                Length = 1200,
                Width = 800,
                Height = 1500,
                PalletGroup = 1,
                PalletType = "Wood",
                Weight = 50,
                MaxHeight = 2400,
                MaxWeight = 1000,
                Overmeasure = 50,
                AvailableSpaces = 1,
                SpecialPallet = false,
                SpaceBetweenElements = 0,
                Active = true
            };

            // Act: Kald til metoden, der tilføjer pallen
            EFPalletRepository.AddPallet(pallet);

            // Assert: Verificer at pallen blev tilføjet til databasen
            var addedPallet = mockDbContext.Pallets.FirstOrDefault(p => p.Id == 1);

            Assert.NotNull(addedPallet);  // Verificer at pallen blev tilføjet
            Assert.Equal("Test Pallet", addedPallet.PalletDescription); // Verificer at beskrivelsen matcher
            Assert.Equal(1, addedPallet.Id); // Verificer at ID'et matcher
        }
        [Fact]
        public void DeletePallet_ShouldDeletePalletFromDatabase()
        {
            // Arrange: Konfigurer en in-memory database og tilføj en test-palle
            var options = new DbContextOptionsBuilder<PalletDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")  // En midlertidig database til test
                .Options;
            var mockDbContext = new PalletDbContext(options);

            var efPalletRepository = new EFPalletRepository(mockDbContext);

            // Opret en palle og tilføj den til databasen
            var pallet = new Pallet
            {
                Id = 2,
                PalletDescription = "Pallet",
                Length = 1300,
                Width = 700,
                Height = 1600,
                PalletGroup = 2,
                PalletType = "Wood",
                Weight = 60,
                MaxHeight = 2500,
                MaxWeight = 1100,
                Overmeasure = 60,
                AvailableSpaces = 1,
                SpecialPallet = false,
                SpaceBetweenElements = 0,
                Active = true
            };

            mockDbContext.Pallets.Add(pallet);
            mockDbContext.SaveChanges();

            // Act: Slet pallen
            efPalletRepository.DeletePallet(pallet);

            // Assert: Verificer at pallen ikke længere findes i databasen
            var deletedPallet = mockDbContext.Pallets.FirstOrDefault(p => p.Id == pallet.Id);
            Assert.Null(deletedPallet); // Pallen skal være slettet
        }
        [Fact]
        public void EditPallet_ShouldUpdatePalletInDatabase()
        {
            // Arrange: Konfigurer en in-memory database og tilføj en test-palle
            var options = new DbContextOptionsBuilder<PalletDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")  // En midlertidig database til test
                .Options;
            var mockDbContext = new PalletDbContext(options);

            var efPalletRepository = new EFPalletRepository(mockDbContext);

            // Opret en palle og tilføj den til databasen
            var pallet = new Pallet
            {
                Id = 2,
                PalletDescription = "Pallet",
                Length = 1300,
                Width = 700,
                Height = 1600,
                PalletGroup = 2,
                PalletType = "Wood",
                Weight = 60,
                MaxHeight = 2500,
                MaxWeight = 1100,
                Overmeasure = 60,
                AvailableSpaces = 1,
                SpecialPallet = false,
                SpaceBetweenElements = 0,
                Active = true
            };

            mockDbContext.Pallets.Add(pallet);
            mockDbContext.SaveChanges();

            // Act: Opdater pallen
            pallet.PalletDescription = "Updated Pallet";
            pallet.Length = 1400;
            efPalletRepository.UpdatePallet(pallet);

            var updatedPallet = mockDbContext.Pallets.FirstOrDefault(p => p.Id == pallet.Id);
            Assert.NotNull(updatedPallet);
            Assert.Equal("Updated Pallet", updatedPallet.PalletDescription);
            Assert.Equal(1400, updatedPallet.Length);

        }
    }
}