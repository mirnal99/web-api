//using Microsoft.Ajax.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
//using System.Web.Http;
using day4.Model;
using System.Threading.Tasks;

namespace day4.Repository
{

    public class day4Repository
    {
        // string connection = "Data Source=DESKTOP-KOQMAL6\\SQLEXPRESS;Initial Catalog = day3; Integrated Security = True";
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KOQMAL6\\SQLEXPRESS;Initial Catalog=day4;Integrated Security=True");


        
        // GET api/values
        public async Task<List<Person>> GetAsync()
        {
            using (connection)
            {
                //SqlCommand command = new SqlCommand("SELECT OIB, FirstName, LastName FROM Member WHERE OIB = 121;", connection);
                SqlCommand command = new SqlCommand("SELECT * FROM Member;", connection);

                //connection.Open(); (-WARNING-)
                await connection.OpenAsync();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Person.friends.Add(new Person
                        {
                            id = reader.GetInt32(0),
                            firstName = reader.GetString(1),
                            lastName = reader.GetString(2)
                        });



                    }
                    connection.Close();
                    //return Request.CreateResponse(HttpStatusCode.OK, Person.friends);
                    return Person.friends;
                }
                else
                {
                    connection.Close();
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "That table is empty.");
                    return null;
                }


            }
        }

        //[HttpGet]
        //[Route("api/values/{value}")]
        // GET api/values/5
        public async Task<List<MShip>> GetByIdAsync(int id)
        {
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT cijena FROM Membership WHERE id = @Id ", connection);
                //SqlCommand command = new SqlCommand("SELECT cijena FROM Membership WHERE Id = 2;", connection);

                await connection.OpenAsync();

                SqlParameter parameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                command.Parameters.Add(parameter).Value = id;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        MShip.memberships.Add(new MShip
                        {
                            //id = reader.GetInt32(0),
                            //OIB = reader.GetInt32(1),
                            cijena = reader.GetInt32(0)
                        }
                         );
                    }

                    connection.Close();

                    return MShip.memberships;
                }
                else
                {
                    connection.Close();
                    
                    return null;
                }


            }


        }



        //[HttpPost]
        //[Route("api/values/{id}")]
        // POST api/values      //Update price for given membership id

        //public async Task<bool> PostAsync(int Id, [FromBody] int cijena)
        public async Task<bool> PostAsync(int Id, int cijena)
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

                    //not done...

                    connection.Close();
                    //return Request.CreateResponse(HttpStatusCode.OK, "Price successfully changed.");
                    return true;
                }
                else
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No membership with that Id.");
                    return false;
                }
            }
        }

        //[HttpPut]
        //[Route("api/values/{oib}/{value1}/{value2}")]

        public async Task<bool> PutAsync(int OIB, string first_name, string last_name) //Insert 
        {
            SqlCommand command_insert = new SqlCommand("INSERT INTO Member VALUES(@OIB, @FirstName, @LastName);", connection);

            //connection.Open(); (-WARNING-)
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
                // return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with that OIB already exists.");
                return false;
            }
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command_insert;
                adapter.InsertCommand.ExecuteNonQuery(); //used to execute the sql statements like update, insert, delete

                connection.Close();
                //return Request.CreateResponse(HttpStatusCode.OK, "New member added.");
                return true;
            }
        }


        //[HttpDelete]
        //[Route("api/values/delete/{id}")]
        public async Task<bool> DeleteAsync(int OIB)
        {
            using (connection)
            {
                //connection.Open();
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT FirstName FROM Member WHERE OIB = @OIB;", connection);

                command.Parameters.Add("@OIB", System.Data.SqlDbType.Int).Value = OIB;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.DeleteCommand = new SqlCommand("DELETE FROM Member WHERE OIB = @OIB;", connection);
                    adapter.DeleteCommand.UpdatedRowSource = System.Data.UpdateRowSource.None;
                    adapter.DeleteCommand.ExecuteNonQuery();

                    connection.Close();
                    // return Request.CreateResponse(HttpStatusCode.OK, "Member deleted.");
                    return true;

                }
                else
                {
                    connection.Close();
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no member with that OIB.");
                    return false;
                }

            }

        }
    }
}

