using Moviza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Moviza.ViewModels;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Script.Serialization;
using System.Runtime.Remoting.Contexts;

namespace Moviza.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        private ApplicationDbContext _context;
        public CustomersController()
        {
             _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
             _context.Dispose();
        }

        public ActionResult ShowAllMovies()
        {
           return View("ShowAllMovies");
        }


        [HttpPost]
        public JsonResult GetFewMovie(int? pagenum,int? pagesize)
        {
            List<Movie> movies = new List<Movie>();
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var connection = new SqlConnection(constring);

            var command = new SqlCommand("GetMovies", connection);

            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@PageNumber", pagenum);
            //  command.Parameters.AddWithValue("@PageSize", pagesize);
            command.Parameters.Add(new SqlParameter()
            {
                ParameterName= "@PageNumber",
                Value=pagenum
            });
            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@PageSize",
                Value = pagesize
            });
            connection.Open();

            //   command.ExecuteNonQuery();

            //connection.Close();

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
         
            while (dr.Read())
            {
                Movie movie = new Movie();
                movie.Id = Convert.ToInt32(dr["Id"].ToString());
                movie.Name = dr["Name"].ToString();
                movie.NumberAvailable =Convert.ToInt32( dr["NumberAvailable"]);
                movie.ReleaseDate =  Convert.ToDateTime(dr["ReleaseDate"]);
                movie.imagePath = dr["imagePath"].ToString();
                //   DateTime.Parse(sReader("DateField").ToString).ToString("dd MMM yyyy")

                movies.Add(movie);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
           var data= js.Serialize(movies);
            return Json(data);

        }


        public ActionResult custNAme()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var connection = new SqlConnection(constring);

            var command = new SqlCommand("GetData", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@param1", 11);

            connection.Open();

         //   command.ExecuteNonQuery();

            //connection.Close();
           
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            var name="";
            while(dr.Read()){
                 name = dr["Name"].ToString();
            }
            return Content(name);
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(m => m.MembershipType).ToList();
            
            return View(customers);
        }


        public ActionResult Details(int? id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            return View(customer);
        }


        public ActionResult New()
        {
            DateTime? startDate = null;
            var membershiptypes = _context.MembershipTypes.ToList();
        //    Customer cus = new Customer { Id = 0 };
            var viewmodel = new NewCustomerViewModel(new Customer())
            {
                MembershipTypes=membershiptypes
              


            };
            viewmodel.Birthday = null;
            return View("CustomerForm",viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            
            if (!ModelState.IsValid)
            {
                var viewmodel = new NewCustomerViewModel(customer)
                {
                   
                    MembershipTypes=_context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewmodel);
            }
            else { 

            if (customer.Id == 0)
            {

                _context.Customers.Add(customer);
               
            }
            else
            {
                var customerIndb = _context.Customers.Single(c => c.Id == customer.Id);
                //  TryUpdateModel(customerIndb);
                customerIndb.Name = customer.Name;
                customerIndb.Birthday = customer.Birthday;
                customerIndb.MembershipTypeId = customer.MembershipTypeId;
                customerIndb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            _context.SaveChanges();

                }
            return RedirectToAction("Index","Customers");
        }


        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();

            }
            var viewmodel = new NewCustomerViewModel(customer)
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            

        };
            return View("CustomerForm",viewmodel);
        }

        /* private IEnumerable<Customer> GetCustomers()
          {


              return new List<Customer>
              {
                      new Customer {Id=1,Name="khaled" },
                      new Customer {Id=2,Name="ahmed" },
                      new Customer {Id=3,Name="mostafa" }
              }; 
          }
          */

    }
}