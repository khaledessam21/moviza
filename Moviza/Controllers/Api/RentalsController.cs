using Moviza.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moviza.Models;

namespace Moviza.Controllers.Api
{
    public class RentalsController : ApiController
    {
        ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpPost]
        public IHttpActionResult CreateNewRental(RentalsDto renDto)
        {

            if (renDto.MoviesIds.Count == 0)
                return BadRequest("no movie Ids have been given");


            var customer = _context.Customers.SingleOrDefault(c => c.Id == renDto.CustomerId);
            var movies = _context.Movies.Where(m => renDto.MoviesIds.Contains(m.Id)).ToList();

            if (customer == null) return BadRequest("Customer Id is Invaild");

            if (movies.Count != renDto.MoviesIds.Count)
                return BadRequest("one or more movieIds are invalid");
         
            foreach(var item in movies)
            {
                if (item.NumberAvailable == 0) return BadRequest("movie is not available");

                item.NumberAvailable--;
                Rental rental = new Rental
                {
                    Customer = customer,
                    Movie = item,
                    DateRented = DateTime.Now,
                    DateReturned =null
                };
                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();
          

            return Ok();
        }



    }
}
