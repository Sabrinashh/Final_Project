namespace ColumbiaProject.Models
{
    public class Review:BaseEntity
    {
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public string Text { get; set; }
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}
