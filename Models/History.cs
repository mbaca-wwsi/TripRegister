namespace TI.Models
{
    public class History
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}