using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces
{
    public interface IItemRepository
    {
        public ItemModel GetItem(Guid id);
        public List<ItemModel> GetItems();
        public bool AddItem(ItemModel item);
        public bool UpdateItem(ItemModel item);
        public bool DeleteItem(Guid id);
    }
}
