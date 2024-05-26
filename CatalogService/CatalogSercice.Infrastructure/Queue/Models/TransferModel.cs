using CatalogService.Domain.Models;

namespace CatalogSercice.Infrastructure.Queue.Models
{
    public class TransferModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }

        public TransferModel(ItemDtoModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Image = model.Image;
            Price = model.Price;
        }
    }
}
