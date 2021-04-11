using Wallets.BusinessLayer;
using Xunit;
using System.Drawing;

namespace Wallets.BusinessLayerTests
{
    public class CategoryTests
    {

        [Fact]
        public void ValidationTest()
        {
            // Arrange
            Category validCategory = new Category(Color.White, "Category", "This is a valid category",
                "cat.jpeg");
            Category invalidNameCategory = new Category(Color.White, "", "This is a valid category");
            Category invalidDescrCategory = new Category(Color.White, "Category", "");

            // Act
            bool isNameValid = invalidNameCategory.Validate();
            bool isDescrValid = invalidDescrCategory.Validate();
            bool isValidOneValid = validCategory.Validate();

            // Assert
            Assert.True(isValidOneValid);
            Assert.False(isNameValid);
            Assert.False(isDescrValid);
        }
    }
}
