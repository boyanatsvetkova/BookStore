using BookStore.Services;
using BookStore.Services.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.Tests
{
    public class StoreServiceTest
    {
        private const string DATA = "{\"Category\":[{\"Name\": \"Science Fiction\", \"Discount\": 0.05},{\"Name\": \"Fantastique\", \"Discount\": 0.1},{\"Name\": \"Philosophy\", \"Discount\": 0.15}],\"Catalog\": [{\"Name\": \"J.K Rowling - Goblet Of fire\", \"Category\": \"Fantastique\",\"Price\": 8,\"Quantity\": 2},{\"Name\": \"Ayn Rand - FountainHead\", \"Category\": \"Philosophy\",\"Price\": 12,\"Quantity\": 10},{\"Name\": \"Isaac Asimov - Foundation\", \"Category\": \"Science Fiction\", \"Price\": 16,\"Quantity\": 1},{\"Name\": \"Isaac Asimov - Robot series\", \"Category\": \"Science Fiction\",\"Price\": 5,\"Quantity\": 1},{\"Name\": \"Robin Hobb - Assassin Apprentice\", \"Category\": \"Fantastique\",\"Price\": 12,\"Quantity\": 8}]}";

        [Fact]
        public void Quantity_ShouldReturnQuantityOfBook_WhenBookAvailable()
        {
            // Arrange
            string bookName = "J.K Rowling - Goblet Of fire";

            var storeService = new StoreService();
            storeService.Import(DATA);

            // Act
            int quantity = storeService.Quantity(bookName);

            //Assert
            Assert.Equal(2, quantity);
        }

        [Theory]
        [InlineData(new string[] { "Ayn Rand - FountainHead" }, 12)]
        [InlineData(new string[] { "Robin Hobb - Assassin Apprentice", "J.K Rowling - Goblet Of fire" }, 18)]
        [InlineData(new string[] { "J.K Rowling - Goblet Of fire" , "Robin Hobb - Assassin Apprentice" , "Robin Hobb - Assassin Apprentice" }, 30)]
        [InlineData(new string[] { "Ayn Rand - FountainHead" ,
            "J.K Rowling - Goblet Of fire",
            "J.K Rowling - Goblet Of fire",
            "Robin Hobb - Assassin Apprentice" ,
            "Robin Hobb - Assassin Apprentice",
            "Isaac Asimov - Foundation",
            "Isaac Asimov - Robot series"}, 69.95)]
        [InlineData(new string[] { "Isaac Asimov - Foundation",
            "Isaac Asimov - Robot series",
            "Robin Hobb - Assassin Apprentice",
            "J.K Rowling - Goblet Of fire" }, 37.95)]
        public void Buy_ShouldReturnPriceOfBasket_WhenListOfBookNamesIsGiven(string[] bookNames, double expectedPriceOfBasket)
        {
            // Arrange
            StoreService storeService = new StoreService();
            storeService.Import(DATA);

            // Act
            double basketPrice = storeService.Buy(bookNames);

            // Assert
            Assert.Equal(expectedPriceOfBasket, basketPrice);
        }
    }
}
