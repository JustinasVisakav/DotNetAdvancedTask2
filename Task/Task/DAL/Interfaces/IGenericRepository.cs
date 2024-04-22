using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.BLL.Models;

namespace Task.DAL.Interfaces
{
    public interface IGenericRepository<T>
    {
        public void UpsertRecord(CartModel record);
        public void DeleteRecord(Guid id);
        public CartModel GetRecord(Guid id);
        public IEnumerable<CartModel> GetAllRecords();

    }
}
