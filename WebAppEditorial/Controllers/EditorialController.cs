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
    public class EditorialController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EditorialController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select EditorialId, EditorialName, 
            EditorialAdress, EditorialPhone, EditorialEmail, EditorialMaxBook from dbo.Editorial";

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
        public JsonResult Post(Editorial edit)
        {
            string query = @"insert into dbo.Editorial values
                            ('" + edit.EditorialName + @"', 
                             '" + edit.EditorialAdress + @"',
                             '" + edit.EditorialPhone + @"',
                             '" + edit.EditorialEmail + @"',
                             '" + edit.EditorialMaxBook + @"'
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
        public JsonResult Put(Editorial edit)
        {
            string query = @"update dbo.Editorial set
                            EditorialName = '" + edit.EditorialName + @"',
                            EditorialAdress = '" + edit.EditorialAdress + @"',
                            EditorialPhone = '" + edit.EditorialPhone + @"',
                            EditorialEmail =  '" + edit.EditorialEmail + @"',
                            EditorialMaxBook = '" + edit.EditorialMaxBook + @"'
                            where EditorialId = '" + edit.EditorialId + @"'
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
            string query = @"delete from dbo.Editorial 
                            where EditorialId = '" + id + @"'
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
