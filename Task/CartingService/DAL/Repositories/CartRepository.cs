using LiteDB;
using CartingService.DAL.Interfaces;
using CartingService.BLL.Models;

namespace CartingService.DAL.Repositories
{
    public class CartRepository : IGenericRepository<CartModel>
    {
        private readonly ILiteCollection<CartModel> db;

        public CartRepository(LiteDatabase db)
        {
            this.db = db.GetCollection<CartModel>("Cart");
        }

        public bool UpsertRecord(CartModel record)
        {
            return !db.Upsert(record);
        }

        public bool DeleteRecord(Guid id)
        {
            return db.Delete(id);
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
