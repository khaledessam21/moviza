using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Moviza.Models;
using Moviza.Dtos;
using AutoMapper;
namespace Moviza.Controllers.Api
{

    public class MovieController : ApiController
    {
         ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/Customers

        public IHttpActionResult GetMovies(string query=null)
        {
            var movies = _context.Movies.Include(m => m.Genre);
            if (!string.IsNullOrWhiteSpace(query))
                movies.Where(m => m.Name.Contains(query)).Where(m => m.NumberAvailable > 0)
                    .ToList().Select(Mapper.Map<Movie, MovieDto>);
            else
                movies.ToList().Select(Mapper.Map<Movie,MovieDto>);

            return Ok(movies);
        }

        //POST /api/Customers

        public IHttpActionResult CreateMovies(Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            _context.Movies.Add(movie);
            _context.SaveChanges();


            return Created("",movie);
        }

        //PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.Single(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            movieInDb.GenreId = movie.GenreId;

            movieInDb.Name = movie.Name;

            movieInDb.NumberInStock = movie.NumberInStock;

            movieInDb.ReleaseDate = movie.ReleaseDate;

            _context.SaveChanges();

            return Ok();

        }

        //GET /api/cutomer/id

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }


        //DELETE /api/customer/1
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
