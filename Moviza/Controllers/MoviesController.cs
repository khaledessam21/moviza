using Moviza.Models;
using Moviza.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace Moviza.Controllers
{
    public class MoviesController : Controller
    {

        ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Movies
        public ActionResult Random()
        {

            IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();
            if (User.IsInRole("CanManageMovies")) 
            return View("Random",movies);

            return View("RandomGuest", movies);



            //  return Content("my name is khaled");
            //  return HttpNotFound();
            //  return new EmptyResult();
            // return RedirectToAction("Index", "Home", new { page = 1, sortby = "name" });
        }

        [Authorize(Roles =RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewmodel = new NewMovieViewModel
            {
                Genres=genres
            };

            return View("MovieForm",viewmodel);
        }

        [HttpPost]
        public ActionResult Save(NewMovieViewModel movieVM)
        {


            if(!ModelState.IsValid)
            {
                var geners = _context.Genres.ToList();
                movieVM.Genres = geners;

                return View("MovieForm", movieVM);
            }

                if(movieVM.Movie.Id==0||movieVM.Movie.Id == 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(movieVM.file.FileName);
                string extension = Path.GetExtension(movieVM.file.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                movieVM.Movie.imagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                movieVM.file.SaveAs(fileName);

                _context.Movies.Add(movieVM.Movie);
                _context.SaveChanges();

            }
            else
            {
                var movieEdit = _context.Movies.Single(m => m.Id == movieVM.Movie.Id);
                movieEdit.Name = movieVM.Movie.Name;
                movieEdit.ReleaseDate = movieVM.Movie.ReleaseDate;
                movieEdit.NumberInStock = movieVM.Movie.NumberInStock;
                movieEdit.GenreId = movieVM.Movie.GenreId;
                _context.SaveChanges();

            }


            return RedirectToAction("Random", "Movies");
        }


        public ActionResult Edit(int Id)
        {

            var movie = _context.Movies.Single(m => m.Id == Id);
            var geners = _context.Genres.ToList();
            var viewmodel = new NewMovieViewModel
            {
                Movie=movie,
                Genres=geners
            };

            return View("MovieForm",viewmodel);
        }
        /*
        public ActionResult Index(int? pageIndex,string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if(string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";

            }

            return Content(string.Format("pageIndex={0}& sortBy={1}",pageIndex,sortBy));
        }

        [Route("movies/released/{year}/{month:regex(^\\d{2}$):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
             
            return Content(year+"/"+month);
        }*/

    }
}