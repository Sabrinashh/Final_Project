namespace ColumbiaProject.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime CreatedDate { get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }

    }
}
