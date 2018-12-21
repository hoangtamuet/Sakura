using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sakura.Models;
namespace Sakura.Controllers
{
    [RoutePrefix("api/Customers")]
    public class CustomerController : ApiController
    {
        [HttpGet, Route("Count")]
        public int Count()
        {
            TutorialDBEntities TutorialDBEntities = new TutorialDBEntities();

            return TutorialDBEntities.Customers.Count();
        }
        [HttpGet, Route("List")]
        public List<Customer> List()
        {
            TutorialDBEntities TutorialDBEntities = new TutorialDBEntities();

            return TutorialDBEntities.Customers.ToList();
        }
        [HttpGet, Route("{CustomerId}")]
        public HttpResponseMessage Get(int CustomerId)
        {
            TutorialDBEntities tutorialDBEntities = new TutorialDBEntities();
            Customer _customer = tutorialDBEntities.Customers.Where(c => c.CustomerId == CustomerId).FirstOrDefault();
            if(_customer != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, _customer);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid code or customer not found");
            }
        }

        [HttpPost, Route("")]
        public void Add([FromBody] Customer Customer)
        {
            TutorialDBEntities TutorialDBEntities = new TutorialDBEntities();
            TutorialDBEntities.Customers.Add(Customer);
            TutorialDBEntities.SaveChanges();

        }

        [HttpPut, Route("{CustomerId}")]
        public HttpResponseMessage Update(int CustomerId,[FromBody] Customer customer )
        {
            TutorialDBEntities tutorialDBEntities = new TutorialDBEntities();
            Customer _customer = tutorialDBEntities.Customers.Where(c => c.CustomerId == CustomerId).FirstOrDefault();
            if(_customer != null)
            {
                _customer.Name = customer.Name;
                _customer.Email = customer.Email;
                _customer.Location = customer.Location;
                tutorialDBEntities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, _customer);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid code or customer not found");
            }
        }

        [HttpDelete, Route("{CustomerId}")]
        public HttpResponseMessage Delete(int CustomerId)
        {
            try
            {
                TutorialDBEntities tutorialDBEntities = new TutorialDBEntities();
                Customer _customer = tutorialDBEntities.Customers.Where(c => c.CustomerId == CustomerId).FirstOrDefault();
                if(_customer != null)
                {
                    tutorialDBEntities.Customers.Remove(_customer);
                    tutorialDBEntities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, CustomerId);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid code or customer not found");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        
        
    }
}
