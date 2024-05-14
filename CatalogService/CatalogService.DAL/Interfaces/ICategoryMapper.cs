using CatalogService.DAL.Models;
using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Interfaces
{
    public interface ICategoryMapper
    {
        public CategoryDtoModel CategoryToDtoModel(CategoryModel categoryModel);
        public CategoryModel CategoryToModel(CategoryDtoModel categoryDtoModel);
    }
}
