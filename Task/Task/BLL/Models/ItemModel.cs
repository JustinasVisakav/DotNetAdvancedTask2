using System.ComponentModel.DataAnnotations;

namespace Task.BLL.Models
{
    public class ItemModel
    {
        public ItemModel()
        {
            var rand = new Random();
            Id = Guid.NewGuid();
            Name = "Random name " + rand.Next(1, 100);
            Image = "Random url" + rand.Next(1, 100);
            Price = rand.Next(1, 100);
            Quantity = rand.Next(1, 100);
        }
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        [Required]
        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}
