using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Constant
{
    public static class SeedDatabaseConstant
    {
        //Category
        public static List<Category> CategoriesSeed()
        {
            List<Category> categories = new List<Category>();
            //Generate 10 categories
            for (int i = 1; i <= 10; i++)
            {
                Guid id = Guid.NewGuid();
                Category c = new Category
                {
                    Id = id,
                    Name = "Category " + i
                };
                categories.Add(c);
            }
            return categories;
        }
        public static List<Category> Default_Category = CategoriesSeed();

        //Product
        public static List<Product> ProductsSeed()
        {
            List<Product> products = new List<Product>();
            Random random = new Random();
            //Generate 50 product with random category
            for (int i = 1; i <= 50; i++)
            {
                Guid id = Guid.NewGuid();
                int randomIndex = random.Next(1, Default_Category.Count);
                Product c = new Product
                {
                    Id = id,
                    Name = "Product " + i,
                    Description = "This is description for product " + i,
                    CategoryId = Default_Category[randomIndex].Id
                };
                products.Add(c);
            }
            return products;
        }
        public static List<Product> Default_Product = ProductsSeed();

        //Role
        
    }
}
