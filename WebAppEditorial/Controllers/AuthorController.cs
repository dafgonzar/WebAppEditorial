using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAppEditorial.Models;

namespace WebAppEditorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select AuthorId, AuthorFullName, 
            convert(varchar(10),AuthorDateBirth,120) as AuthorDateBirth, AuthorCity, AuthorEmail from dbo.Author";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EditorialAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Author author)
        {
            string query = @"insert into dbo.Author values
                            ('" + author.AuthorFullName + @"', 
                             '" + author.AuthorDateBirth + @"',
                             '" + author.AuthorCity + @"',
                             '" + author.AuthorEmail + @"'
                            )";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EditorialAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Added SuccessFully");
        }

        [HttpPut]
        public JsonResult Put(Author author)
        {
            string query = @"update dbo.Author set
                            AuthorFullName = '" + author.AuthorFullName + @"',
                            AuthorDateBirth = '" + author.AuthorDateBirth + @"',
                            AuthorCity = '" + author.AuthorCity + @"',
                            AuthorEmail =  '" + author.AuthorEmail + @"'
                            where AuthorId = '" + author.AuthorId + @"'
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EditorialAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Update SuccessFully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Author 
                            where AuthorId = '" + id + @"'
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EditorialAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Delete SuccessFully");
        }
    }
}
