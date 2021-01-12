using System.Collections.Generic;
using TI.Models;

namespace TI.ViewModels
{
    public class ShowPhotosViewModel
    {
        public int TripId { get; set; }
        public List<Photo> Photos { get; set; }
    }
}