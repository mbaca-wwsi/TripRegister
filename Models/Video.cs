namespace TI.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}