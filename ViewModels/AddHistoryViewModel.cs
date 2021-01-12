using System;
using System.ComponentModel.DataAnnotations;

namespace TI.ViewModels
{
    public class AddHistoryViewModel
    {
        [Required(ErrorMessage="Tytuł jest wymagany")]
        [Display(Name="Tytuł wpisu")]
        public string Title { get; set; }
        [Required(ErrorMessage="Opis jest wymagany")]
        [Display(Name="Opis")]
        public string Comment { get; set; }
        public int TripId { get; set; }
        public DateTime Date { get; set; }
    }
}