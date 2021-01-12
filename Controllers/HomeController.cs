using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TI.Models;
using TI.ViewModels;

namespace TI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webhost;
        public HomeController(AppDbContext context, IWebHostEnvironment webhost)
        {
            _webhost = webhost;
            _context = context;

        }
        public IActionResult Index(string sortOrder, string search)
        {
            ViewData["Date"] = sortOrder == "DateReg" ? "datereg_desc" : "DateReg";
            ViewData["Search"] = search;
            var trips = _context.Trip.ToList();
            if (!String.IsNullOrEmpty(search))
            {
                trips = trips.Where(s => s.Name.Contains(search) || s.StartPlace.Contains(search) || s.FinishPlace.Contains(search)).ToList();
            }
            switch (sortOrder)
            {
                case "datereg_desc":
                    trips = trips.OrderByDescending(s => s.StartDate).ToList();
                    break;
                default:
                    trips = trips.OrderBy(s => s.StartDate).ToList();
                    break;
            }
            return View(trips);
        }
        public IActionResult AddTrip()
        {
            var model = new AddTripViewModel();
            model.StartDate = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddTrip(AddTripViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = new Trip()
                {
                    Name = model.Name,
                    StartDate = model.StartDate,
                    StartPlace = model.StartPlace,
                    FinishPlace = model.FinishPlace,
                    Distance = model.Distance,
                    Days = model.Days
                };
                _context.Trip.Add(trip);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult ShowTrip(int id)
        {
            var trip = _context.Trip.FirstOrDefault(x => x.Id == id);
            var model = new ShowTripViewModel()
            {
                Id = id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                StartPlace = trip.StartPlace,
                FinishPlace = trip.FinishPlace,
                Distance = trip.Distance,
                Days = trip.Days
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditTrip(ShowTripViewModel model)
        {
            var trip = _context.Trip.FirstOrDefault(x => x.Id == model.Id);
            if (ModelState.IsValid)
            {
                trip.Name = model.Name;
                trip.StartDate = model.StartDate;
                trip.StartPlace = model.StartPlace;
                trip.FinishPlace = model.FinishPlace;
                trip.Distance = model.Distance;
                trip.Days = model.Days;
                _context.Trip.Update(trip);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult ShowHistory(int id)
        {
            var trip = _context.Trip.FirstOrDefault(x => x.Id == id);
            List<HistoryItemViewModel> history = _context.History.Where(x => x.TripId == id).Select(x => new HistoryItemViewModel { Title = x.Title, Date = x.Date, Comment = x.Comment, TripId = x.TripId }).ToList();
            var model = new ShowHistoryViewModel()
            {
                TripId = id,
                History = history
            };
            return View(model);
        }
        public IActionResult AddHistory(int id)
        {
            var model = new AddHistoryViewModel();
            model.TripId = id;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddHistory(AddHistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var history = new History()
                {
                    Title = model.Title,
                    Comment = model.Comment,
                    TripId = model.TripId,
                    Date = DateTime.Now
                };
                _context.History.Add(history);
                _context.SaveChanges();
                return RedirectToAction("ShowHistory", new { id = model.TripId });
            }
            return View(model);
        }
        public IActionResult AddPhotos(int id)
        {
            var model = new AddPhotosViewModel();
            model.TripId = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPhotos(AddPhotosViewModel model)
        {
            var photos = _context.Photo.Where(x => x.TripId == model.TripId).ToList();
            if (photos.Count < 6)
            {
                foreach (var file in model.Photos)
                {
                    if (photos.Count <= 6)
                    {
                        var filename = Path.Combine(_webhost.WebRootPath, "Photos", file.FileName);
                        string ext = Path.GetExtension(file.FileName);
                        if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG")
                        {
                            using (var uploading = new FileStream(filename, FileMode.Create))
                            {
                                await file.CopyToAsync(uploading);
                                var photo = new Photo()
                                {
                                    Name = file.FileName,
                                    TripId = model.TripId
                                };
                                _context.Photo.Add(photo);
                                _context.SaveChanges();
                            }
                        }
                        else
                        {
                            model.Error = "Plik " + file.FileName + " jest nieprawidłowy. Proszę wskazać plik z rozszerzeniem JPG lub PNG";
                            return View(model);
                        }
                    }
                    else
                    {
                        model.Error = "Do danej wycieczki dodano już 6 zdjęć. Przykro nam, nie możesz dodać więcej.";
                        return View(model);
                    }

                }
                return RedirectToAction("ShowPhotos", new { id = model.TripId });
            }
            model.Error = "Do danej wycieczki dodano już 6 zdjęć. Przykro nam, nie możesz dodać więcej.";
            return View(model);
        }
        public IActionResult ShowPhotos(int id)
        {
            var photos = _context.Photo.Where(x => x.TripId == id);
            var model = new ShowPhotosViewModel()
            {
                TripId = id,
                Photos = photos.ToList()
            };
            return View(model);
        }
        public IActionResult AddVideo(int id)
        {
            var model = new AddVideoViewModel();
            model.TripId = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddVideo(AddVideoViewModel model)
        {
            var video = _context.Video.Where(x => x.TripId == model.TripId).ToList();
            if (!video.Any())
            {
                var filename = Path.Combine(_webhost.WebRootPath, "Video", model.Video.FileName);
                string ext = Path.GetExtension(model.Video.FileName);
                if (ext == ".mp4" || ext == ".MP4")
                {
                    using (var uploading = new FileStream(filename, FileMode.Create))
                    {
                        await model.Video.CopyToAsync(uploading);
                        var vid = new Video()
                        {
                            Name = model.Video.FileName,
                            TripId = model.TripId
                        };
                        _context.Video.Add(vid);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("ShowVideo", new { id = model.TripId });
                }
                else
                {
                    model.Error = "Plik " + model.Video.FileName + " jest nieprawidłowy. Proszę wskazać plik z rozszerzeniem MP4";
                    return View(model);
                }
            }
            model.Error = "Do danej wycieczki dodano już wideo. Przykro nam, nie możesz dodać więcej.";
            return View(model);
        }
        public IActionResult ShowVideo(int id)
        {
            var video = _context.Video.FirstOrDefault(x => x.TripId == id);
            var model = new ShowVideoViewModel();
            model.TripId=id;
            if(video==null)
            {
                model.Error="Brak dodanego wideo";
                return View(model);
            }
            
            else
            {
                model.Video= video.Name;
                return View(model);
            }
            
        }
    }
}