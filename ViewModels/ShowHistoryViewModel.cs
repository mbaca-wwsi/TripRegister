using System;
using System.Collections.Generic;

namespace TI.ViewModels
{
    public class ShowHistoryViewModel
    {
        public int TripId { get; set; }
        public List<HistoryItemViewModel> History { get; set; }
    }
    public class HistoryItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int TripId { get; set; }
        public DateTime Date { get; set; }
    }
}