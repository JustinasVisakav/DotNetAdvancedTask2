using CatalogService.Domain.ContextKeeper;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories
{
    internal class ItemRepository : IItemRepository
    {
        private readonly DatabaseContext db;

        public ItemRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public bool AddItem(ItemModel item)
        {
            try
            {
                db.Items.Add(item);
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
                db.Remove(GetItem(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ItemModel GetItem(Guid id)
        {
            return db.Items.FirstOrDefault(x => x.Id == id);
        }

        public List<ItemModel> GetItems()
        {
            return db.Items.Include(x => x.Category).ToList();
        }

        public bool UpdateItem(ItemModel item)
        {
            try
            {
                db.Update(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
