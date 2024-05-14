using CatalogService.DAL.Models;
using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Tests.TestHelpers
{
    public static class ModelCreator
    {
        public static CategoryModel CreateCategoryModel()
        {
            Random rand = new Random();
            CategoryModel model = new CategoryModel()
            {
                Id = Guid.NewGuid(),
                Image = "Image " + rand.Next(1, 100),
                Name = "Name " + rand.Next(1, 100),
            };
            return model;
        }

        public static ItemModel CreateItemModel()
        {
            Random rand = new Random();
            ItemModel model = new ItemModel()
            {
                Id = Guid.NewGuid(),
                Amount = rand.Next(1, 100),
                Price = rand.Next(1, 100),
                Name = "Name " + rand.Next(1, 100),
                Image = "Image " + rand.Next(1, 100),
                Description = "Descroptiom " + rand.Next(1, 100),
            };
            return model;
        }

        public static CategoryDtoModel CreateCategoryDtoModel()
        {
            Random rand = new Random();
            CategoryDtoModel model = new CategoryDtoModel()
            {
                Id = Guid.NewGuid(),
                Image = "Image " + rand.Next(1, 100),
                Name = "Name " + rand.Next(1, 100),
            };
            return model;
        }

        public static ItemDtoModel CreateItemDtoModel()
        {
            Random rand = new Random();
            ItemDtoModel model = new ItemDtoModel()
            {
                Id = Guid.NewGuid(),
                Amount = rand.Next(1, 100),
                Price = rand.Next(1, 100),
                Name = "Name " + rand.Next(1, 100),
                Image = "Image " + rand.Next(1, 100),
                Description = "Descroptiom " + rand.Next(1, 100),
            };
            return model;
        }
    }
}
