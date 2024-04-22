using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Task.DAL.Interfaces;
using Task.BLL.Models;

namespace Task.DAL.Repositories
{
    public class CartRepository : IGenericRepository<CartModel>
    {
        private readonly ILiteCollection<CartModel> db;

        public CartRepository(LiteDatabase db)
        {
            this.db = db.GetCollection<CartModel>("Cart");
        }

        public void UpsertRecord(CartModel record)
        {
            db.Upsert(record);
        }

        public void DeleteRecord(Guid id)
        {
            db.Delete(id);
        }

        public IEnumerable<CartModel> GetAllRecords()
        {
            return db.FindAll();
        }

        public CartModel GetRecord(Guid id)
        {
            return db.Find(x => x.Id == id).FirstOrDefault();
        }
    }
}
