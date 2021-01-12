using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TI.Models;

namespace TI.ViewModels
{
    public class ShowTripViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Nazwa wycieczki jest wymagana")]
        [Display(Name="Nazwa wycieczki")]
        public string Name { get; set; }
        [Required(ErrorMessage="Data rozpoczęcia wycieczki jest wymagana.")]
        [Display(Name="Początek wycieczki")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage="Miejsce rozpoczęcia wycieczki jest wymagane")]
        [Display(Name="Miejsce rozpoczęcia wycieczki")]
        public string StartPlace { get; set; }
        [Display(Name="Miejsce zakonczenia wycieczki")]
        public string FinishPlace { get; set; }
        [Required(ErrorMessage="Podaj pokonany dystans")]
        [Display(Name="Pokonany dystans")]
        [RegularExpression(@"\d*\,?[0-9]{2}?", ErrorMessage="Podaj liczbę z przecinkiem i dwoma miejscami po przecinku")]
        public decimal Distance { get; set; }
        [Required(ErrorMessage="Podaj liczbę dni")]
        [Display(Name="Ile dni trwała wycieczka")]
        public int Days { get; set; }
        public virtual ICollection<Photo> Photos  { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}