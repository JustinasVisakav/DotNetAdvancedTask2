﻿using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Interfaces
{
    public interface IItemService
    {
        public ItemDtoModel GetItem(Guid id);
        public List<ItemDtoModel> GetItems();
        public bool AddItem(ItemDtoModel item);
        public bool UpdateItem(ItemDtoModel item);
        public bool DeleteItem(Guid id);
    }
}
