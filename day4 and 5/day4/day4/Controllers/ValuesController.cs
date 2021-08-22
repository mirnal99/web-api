using Microsoft.Ajax.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http; 
using System.Runtime.Remoting.Messaging;
using System.Web.Http;

using System.Configuration;
using System.Threading.Tasks;

using day4.Model;
using day4.Service;
//using day4.Repository;


namespace day4.Controllers
{
   

    public class ValuesController : ApiController
    {
        // string connection = "Data Source=DESKTOP-KOQMAL6\\SQLEXPRESS;Initial Catalog = day3; Integrated Security = True";
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KOQMAL6\\SQLEXPRESS;Initial Catalog=day4;Integrated Security=True");

        day4Service memberService = new day4Service();

        [HttpGet]
        [Route("api/values")]
        // GET api/values
        public async Task<HttpResponseMessage> Get()
        {
            using (connection)
            {
                day4Service serv = new day4Service();
                List<Person> persons = await serv.GetAsync();
              

               // SqlDataReader reader = command.ExecuteReader();

                if (persons == null)
                {

                    //connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "That table is empty.");
                    
                }
                else
                {
                    //connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, Person.friends);
                }


            }
        }

        [HttpGet]
        [Route("api/values/{value}")]
        // GET api/values/5
        public async Task< HttpResponseMessage> GetByIdAsync(int id)
        {
            day4Service serv = new day4Service();
            List<MShip> persons = await serv.GetByIdAsync(id);

            
                SqlCommand command = new SqlCommand("SELECT cijena FROM Membership WHERE id = @Id " , connection);
                //SqlCommand command = new SqlCommand("SELECT cijena FROM Membership WHERE Id = 2;", connection);

                //connection.Open();
                //SqlDataReader reader = command.ExecuteReader();

                if (persons == null)
                {

                //connection.Close();
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no membership with that id number. ");
            }
                else
                {
                //connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, MShip.memberships);
            }


        }



        [HttpPost]
        [Route("api/values/{id}")]
        // POST api/values      //Update price for given membership id
        public async Task<HttpResponseMessage> PostAsync(int Id, [FromBody] int cijena) 
        {
            using (connection)
            {
                SqlCommand command_change_price = new SqlCommand("SELECT cijena FROM Membership WHERE Id = @Id;", connection);

                //connection.Open();
                await connection.OpenAsync();

                command_change_price.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = Id;
                SqlDataReader reader_cijena = command_change_price.ExecuteReader();

                if (reader_cijena.HasRows)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, cijena FROM Membership;", connection);
                    adapter.UpdateCommand = new SqlCommand("UPDATE Membership SET cijena = @cijena WHERE Id = @ Id;", connection);

                    adapter.UpdateCommand.Parameters.Add("@cijena", System.Data.SqlDbType.Int).Value = cijena;

                    //not done...(?)

                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Price successfully changed.");

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no membership with that id number.");
                }
            }
        }

        [HttpPut]
        [Route("api/values/{oib}/{value1}/{value2}")]

        public async Task<HttpResponseMessage> PutAsync(int OIB, string first_name, string last_name) //Insert 
        {
            SqlCommand command_insert = new SqlCommand("INSERT INTO Member VALUES(@OIB, @FirstName, @LastName);", connection);

            //connection.Open();
            await connection.OpenAsync();

            command_insert.Parameters.Add("@OIB", System.Data.SqlDbType.Int).Value = OIB;
            command_insert.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar, 50).Value = first_name;
            command_insert.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar, 50).Value = last_name;

            //provjeri jel postoji vec osoba s tim oibom:
            SqlCommand command_check1 = new SqlCommand("SELECT OIB from Member WHERE OIB = @OIB", connection);

            SqlDataReader reader = command_check1.ExecuteReader();

            if (reader.HasRows)
            {
                connection.Close();
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with taht OIB already exists.");
            }
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command_insert;
                adapter.InsertCommand.ExecuteNonQuery(); //used to execute the sql statements like update, insert, delete
                
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "New member added.");
            }
        }


        [HttpDelete]
        [Route("api/values/delete/{id}")]
        public async Task<HttpResponseMessage> DeleteAsync(int OIB)
        {
            using (connection)
            {
               
                SqlCommand command = new SqlCommand("SELECT FirstName FROM Member WHERE OIB = @OIB;", connection);

                //connection.Open();
                await connection.OpenAsync();

                command.Parameters.Add("@OIB", System.Data.SqlDbType.Int).Value = OIB;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //SqlDataAdapter adapter = new SqlDataAdapter();

                    SqlCommand DeleteCommand = new SqlCommand("DELETE FROM Member WHERE OIB = @OIB;", connection);
                  
                    DeleteCommand.UpdatedRowSource = System.Data.UpdateRowSource.None;

                    command.ExecuteNonQuery();
                    DeleteCommand.ExecuteNonQuery();

                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Member deleted.");


                }
                else
                {
                    connection.Close();
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no member with that OIB.");
                }

            }

        }
    }
}

