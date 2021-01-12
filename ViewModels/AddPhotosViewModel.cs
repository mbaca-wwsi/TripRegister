using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TI.ViewModels
{
    public class AddPhotosViewModel
    {
        public int TripId { get; set; }
        [Required(ErrorMessage = "Dodaj zdjęcia")]  
        [Display(Name = "Zdjęcia")] 
        public List<IFormFile> Photos { get; set; }
        public string Error { get; set; }
    }
}