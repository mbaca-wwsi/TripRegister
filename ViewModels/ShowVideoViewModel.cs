using Microsoft.AspNetCore.Http;

namespace TI.ViewModels
{
    public class ShowVideoViewModel
    {
        public int TripId { get; set; }
        public string Video { get; set; }
        public string Error { get; set; }
    }
}