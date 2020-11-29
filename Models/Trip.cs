using System;
using System.Collections.Generic;

namespace TI.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string StartPlace { get; set; }
        public string FinishPlace { get; set; }
        public float Distance { get; set; }
        public int Days { get; set; }
        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<Photo> Photos  { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}