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
    public class BookController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select Book.BookId, Book.BookTitle, Book.BookYear, Book.BookGender, Book.BookPages, 
                             Editorial.EditorialName, Author.AuthorFullName
                            from dbo.Book
                            INNER JOIN dbo.Editorial ON dbo.Book.BookEditorialId = dbo.Editorial.EditorialId
                            INNER JOIN dbo.Author ON dbo.Book.BookAuthorId = dbo.Author.AuthorId
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
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Book book)
        {
            string query = @"insert into dbo.Book values
                            ('" + book.BookTitle + @"', 
                             '" + book.BookYear + @"',
                             '" + book.BookGender + @"',
                             '" + book.BookPages + @"',
                             '" + book.BookEditorialId + @"',
                             '" + book.BookAuthorId + @"'
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
        public JsonResult Put(Book book)
        {
            string query = @"update dbo.Book set
                            BookTitle = '" + book.BookTitle + @"',
                            BookYear = '" + book.BookYear + @"',
                            BookGender = '" + book.BookGender + @"',
                            BookPages =  '" + book.BookPages + @"',
                            BookEditorialId = '" + book.BookEditorialId + @"',
                            BookAuthorId = '" + book.BookAuthorId + @"'
                            where BookId = '" + book.BookId + @"'
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
            string query = @"delete from dbo.Book 
                            where BookId = '" + id + @"'
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

        [Route("GetAllEditorialNames")]
        public JsonResult GetAllEditorialNames()
        {
            string query = @"
                    select EditorialId, EditorialName from dbo.Editorial
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
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [Route("GetAllAuthorNames")]
        public JsonResult GetAllAuthorNames()
        {
            string query = @"
                    select AuthorId, AuthorFullName from dbo.Author
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
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
