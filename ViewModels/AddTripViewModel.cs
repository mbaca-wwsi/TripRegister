using System;
using System.ComponentModel.DataAnnotations;

namespace TI.ViewModels
{
    public class AddTripViewModel
    {

        [Required(ErrorMessage="Nazwa jest wymagana")]
        [Display(Name="Nazwa wycieczki")]        
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage="Miejsce startu wycieczki jest wymagane")]
        [Display(Name="Miejsce startu wycieczki")] 
        public string StartPlace { get; set; }
        public string FinishPlace { get; set; }
        [Required(ErrorMessage="Podaj pokonany dystans")]
        [Display(Name="Pokonany dystans")]
        [RegularExpression(@"\d*\,?[0-9]{2}?", ErrorMessage="Podaj liczbę z przecinkiem i dwoma miejscami po przecinku")]
        public decimal Distance { get; set; }
        [Required(ErrorMessage="Podaj ilość dni")]
        [Display(Name="Ilość dni")]
        public int Days { get; set; }
    }
}