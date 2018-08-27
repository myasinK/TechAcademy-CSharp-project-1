using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Xml.Schema;
using TACSProject.ViewModels;
using TACSProject.Models;


namespace TACSProject.Controllers
{

    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Submit(CustomerConfirm customerConfirm)
        {
            string connectionString = @"Data Source=LAPTOP-F7SSE5AO\SQLEXPRESS;Initial Catalog=db_QuotesIssued;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string insertString = @"INSERT INTO quotes   
                                    (
                                    firstName,
                                    lastName,
                                    email,
                                    dob,
                                    carYear,
                                    carMake,
                                    carModel,
                                    dui,
                                    tickets,
                                    fullCoverage,
                                    finalQuote
                                    ) 
                                    VALUES 
                                    (
                                    @firstName, @lastName,
                                    @email, @dob,
                                    @carYear, @carMake,
                                    @carModel, @dui,
                                    @tickets, @fullCoverage, 
                                    @finalQuote
                                    )";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand( insertString, connection );
                sqlCommand.Parameters.Add("@firstName", SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@lastName", SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@email", SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@dob", SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@carYear", SqlDbType.Int);
                sqlCommand.Parameters.Add("@carMake", SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@carModel", SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@dui", SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@tickets", SqlDbType.Int);
                sqlCommand.Parameters.Add("@fullCoverage", SqlDbType.VarChar);
                sqlCommand.Parameters.Add("@finalQuote", SqlDbType.Float);

                sqlCommand.Parameters["@firstName"].Value = customerConfirm.First;
                sqlCommand.Parameters["@lastName"].Value = customerConfirm.Last;
                sqlCommand.Parameters["@email"].Value = customerConfirm.Email;
                sqlCommand.Parameters["@dob"].Value = customerConfirm.Month + "/" +
                                                        customerConfirm.Day + "/" +
                                                        customerConfirm.BirthYear.ToString();
                sqlCommand.Parameters["@carYear"].Value = customerConfirm.Year;
                sqlCommand.Parameters["@carMake"].Value = customerConfirm.Make;
                sqlCommand.Parameters["@carModel"].Value = customerConfirm.Model;
                sqlCommand.Parameters["@dui"].Value = customerConfirm.Dui;
                sqlCommand.Parameters["@tickets"].Value = customerConfirm.Tickets;
                sqlCommand.Parameters["@fullCoverage"].Value = customerConfirm.FullCoverage;
                sqlCommand.Parameters["@finalQuote"].Value = customerConfirm.Total;

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            return View(customerConfirm);
        }

        public ActionResult Admin()
        {
            string connectionString = @"Data Source=LAPTOP-F7SSE5AO\SQLEXPRESS;Initial Catalog=db_QuotesIssued;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string queryString = @"SELECT * FROM quotes";
            using ( SqlConnection sqlConnection = new SqlConnection( connectionString ) )
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<ClientInfo> clientList = new List<ClientInfo>();
                while (sqlDataReader.Read())
                {
                    var client = new ClientInfo
                    {
                        ID = Convert.ToInt32(sqlDataReader["id"]),
                        First = sqlDataReader["firstName"].ToString(),
                        Last = sqlDataReader["lastName"].ToString(),
                        Email = sqlDataReader["email"].ToString(),
                        Dui = sqlDataReader["dui"].ToString(),
                        CarMake = sqlDataReader["carMake"].ToString(),
                        CarModel = sqlDataReader["carModel"].ToString(),
                        CarYear = (int) sqlDataReader["carYear"],
                        Dob = sqlDataReader["dob"].ToString(),
                        Tickets = (int) sqlDataReader["tickets"],
                        FullCoverage = sqlDataReader["fullCoverage"].ToString(),
                        Total = sqlDataReader["finalQuote"].ToString()
                    };
                    // add to client list
                    clientList.Add(client);
                }
                return View(clientList);
            }
        }

        
        public ActionResult Edited(int id)
        {
            //var id = Request.QueryString["id"];

            string connectionString = @"Data Source=LAPTOP-F7SSE5AO\SQLEXPRESS;Initial Catalog=db_QuotesIssued;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string queryString = @"SELECT * FROM quotes";
            string deleteString = @"DELETE FROM quotes WHERE id = '" + Convert.ToInt32(id) + "'";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                SqlCommand deleteCommand = new SqlCommand(deleteString, sqlConnection);
                deleteCommand.ExecuteNonQuery();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<ClientInfo> clientList = new List<ClientInfo>();
                while (sqlDataReader.Read())
                {
                    var client = new ClientInfo
                    {
                        ID = Convert.ToInt32(sqlDataReader["id"]),
                        First = sqlDataReader["firstName"].ToString(),
                        Last = sqlDataReader["lastName"].ToString(),
                        Email = sqlDataReader["email"].ToString(),
                        Dui = sqlDataReader["dui"].ToString(),
                        CarMake = sqlDataReader["carMake"].ToString(),
                        CarModel = sqlDataReader["carModel"].ToString(),
                        CarYear = (int)sqlDataReader["carYear"],
                        Dob = sqlDataReader["dob"].ToString(),
                        Tickets = (int)sqlDataReader["tickets"],
                        FullCoverage = sqlDataReader["fullCoverage"].ToString(),
                        Total = sqlDataReader["finalQuote"].ToString()
                    };
                    // add to client list
                    clientList.Add(client);
                }
                return View(clientList);
            }
        }

        [HttpPost]
        public ActionResult Quote(
            string first, 
            string last, 
            int year,
            int tickets,
            string day,
            string month,
            int birthYear,
            string make,
            string model,
            string fullCoverage,
            string dui,
            string email)
        {

            CustomerConfirm customerConfirm = new CustomerConfirm()
            {
                First = first,
                Last = last,
                BirthYear = birthYear,
                Day = day,
                Dui = dui,
                Email = email,
                FullCoverage = fullCoverage,
                Make = make,
                Model = model,
                Month = month,
                Year = year,
                Tickets = tickets
            };

            return View(customerConfirm);  
        } 
    }
}