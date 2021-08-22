using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class Person
    {
        //public static Dictionary<int, string> family = new Dictionary<int, string>();
        public static List<Person> friends = new List<Person>();

        public int id { get; set; } = 0;
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
    }
    
    public class MShip
    {
        public static List<MShip> memberships = new List<MShip>();


        public int id { get; set; } = 0;
        public int OIB { get; set; } = 0;
        public int cijena { get; set; } = 0;

    }
    
        public class ValuesController : ApiController
        {
            // string connection = "Data Source=DESKTOP-KOQMAL6\\SQLEXPRESS;Initial Catalog = day3; Integrated Security = True";
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KOQMAL6\\SQLEXPRESS;Initial Catalog = day3; Integrated Security = True");
        
        [HttpGet]
            [Route("api/values")]
            // GET api/values
            public HttpResponseMessage Get()
            {
                using (connection)
                {
                SqlCommand command = new SqlCommand("SELECT OIB, first_name, last_name FROM Member WHERE OIB = 121;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        Person.friends.Add(new Person { id = reader.GetInt32(0),
                                firstName = reader.GetString(1), lastName = reader.GetString(2) });


                        
                    }
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, Person.friends);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "That table is empty.");
                }


            }
            }

        [HttpGet]
        [Route("api/values/{id}")]
        // GET api/values/5
        public HttpResponseMessage Get(int member_OIB)
            {
                using (connection)
                {
                SqlCommand command = new SqlCommand("SELECT * FROM Membership WHERE MemberOIB = @member_OIB;", connection);
          
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    MShip.memberships.Add(new MShip { id = reader.GetInt32(0),
                                OIB = reader.GetInt32(1), cijena = reader.GetInt32(2) });

                    connection.Close();

                    return Request.CreateResponse(HttpStatusCode.OK, MShip.memberships);
                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no student with that id. ");
                }


            }


            } 


      /*      [HttpGet]
            [Route("api/values")]
            // GET api/values
            public HttpResponseMessage Get()
            {
                if (Person.friends == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Name not found");
                } // 404 - can not find the requested resource

                return Request.CreateResponse(HttpStatusCode.OK, Person.friends);
            }

        [HttpGet]
        [Route("api/values/{id}")]
        // GET api/values/5
        public HttpResponseMessage Get(int id)
            {
                Person friend = Person.friends.Where(x => x.id == id).FirstOrDefault();
                if (friend == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Name not found");
                } // 404 - can not find the requested resource

                return Request.CreateResponse(HttpStatusCode.OK, friend);
            } // 200 - request has succeeded.

            [HttpPost]
            [Route("api/values/{id}")]
            // POST api/values
            public HttpResponseMessage Post(int id)
            {
            Person test = Person.friends.Where(x => x.id == id).FirstOrDefault();

            if (test != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, test);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Person does not exist.");
            }
        } */
            
            [HttpPut]
            [Route("api/values/{id}/{value1}/{value2}")]
   
            public HttpResponseMessage Put(int id, string value1, string value2)
            {
            Person test = Person.friends.Where(x => x.id == id).FirstOrDefault();
            
            if(test != null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Id already exists.");
                //return Request.CreateResponse(HttpStatusCode.OK, test);
            }
            else
            {
                Person.friends.Add(new Person { firstName = value1, lastName = value2, id = id });
                return Request.CreateResponse(HttpStatusCode.OK, "Ok, add person.");
            }
            
            }


        [HttpDelete]
        [Route("api/values/delete/{id}")]
        public HttpResponseMessage Delete(int id)
            {
            Person test2 = Person.friends.Where(x => x.id == id).FirstOrDefault();

            if (test2 != null)
            {
                Person.friends.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok, remove.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No such id.");
            }

        }
        }
    }
