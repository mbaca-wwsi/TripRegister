using Microsoft.AspNetCore.Http;

namespace TI.ViewModels
{
    public class AddVideoViewModel
    {
        public int TripId { get; set; }
        public IFormFile Video { get; set; }
        public string Error { get; set; }
    }
}