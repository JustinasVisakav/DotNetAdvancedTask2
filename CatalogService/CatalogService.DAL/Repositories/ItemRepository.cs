using CatalogService.DAL.ContextKeeper;
using CatalogService.DAL.Extensions;
using CatalogService.DAL.Interfaces;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DAL.Repositories
{
    internal class ItemRepository : IItemRepository
    {
        private readonly DatabaseContext db;
        private readonly IItemMapper mapper;

        public ItemRepository(DatabaseContext db, IItemMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public bool AddItem(ItemDtoModel item)
        {
            try
            {
                if (db.Items.Any(x=>x.Id == item.Id))
                {
                    return false;
                }
                db.Items.Add(mapper.ItemToModel(item));
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteItem(Guid id)
        {
            try
            {
                db.Items.Remove(db.Items.FirstOrDefault(x => x.Id == id));
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ItemDtoModel GetItem(Guid id)
        {
            var result = db.Items.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                return mapper.ItemToItemDtoModel(result);
            }
            return null;
        }

        public List<ItemDtoModel> GetItems()
        {
            List<ItemDtoModel> result = new();
            var resultFromDb = db.Items.ToList();
            foreach (var item in resultFromDb)
            {
                result.Add(mapper.ItemToItemDtoModel(item));
            }
            return result;
        }

        public bool UpdateItem(ItemDtoModel item)
        {
            try
            {
                db.Items.Remove(db.Items.FirstOrDefault(x => x.Id == item.Id));
                db.Items.Add(mapper.ItemToModel(item));

                return db.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
