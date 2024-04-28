using CatalogService.Domain.ContextKeeper;
using CatalogService.Domain.Models;
using CategoryService.Tests.TestHelpers;

namespace CategoryService.Tests.DalTests
{
    public class DalTestDataStorage
    {
        public CategoryModel Category1 { get; set; }
        public CategoryModel Category2 { get; set; }
        public ItemModel Item1 { get; set; }
        public ItemModel Item2 { get; set; }

        public DalTestDataStorage()
        {
            Item1 = ModelCreator.CreateItemModel();
            Item2 = ModelCreator.CreateItemModel();
            Category1= ModelCreator.CreateCategoryModel();
            Category2 = ModelCreator.CreateCategoryModel();
        }

        public static void FillTheDatabaseWithTestData(DatabaseContext context, DalTestDataStorage inMemoryDataStorage)
        {
            context.Categories.AddRange(inMemoryDataStorage.Category1, inMemoryDataStorage.Category2);
            context.Items.AddRange(inMemoryDataStorage.Item2, inMemoryDataStorage.Item1);

            context.SaveChanges();
        }
    }
}
