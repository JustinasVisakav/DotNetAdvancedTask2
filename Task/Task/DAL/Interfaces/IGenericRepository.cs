using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartingService.BLL.Models;

namespace CartingService.DAL.Interfaces
{
    public interface IGenericRepository<T>
    {
        public bool UpsertRecord(CartModel record);
        public bool DeleteRecord(Guid id);
        public CartModel GetRecord(Guid id);
        public IEnumerable<CartModel> GetAllRecords();

    }
}
