using Dapper;
using FullStack.Dto;
using FullStack.Models;
using FullStack.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace FullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {



        private EmployeeRepository empObj;
        private DbFactory conn;
        public EmployeeController()
        {
            empObj = new EmployeeRepository();
            conn = new DbFactory();

        }


        //Get ALL
        [HttpGet]
        [Route("Get")]
        public IEnumerable<Employee> GetAll()
        {

            return empObj.GetAll();
        }

        //Get By Id
        [HttpGet]
        [Route("Get/{id}")]
        public Employee GetById(int id)
        {
            return empObj.GetById(id);

        }

        //INSERT
        [HttpPost]
        public void Post([FromForm] EmployeeForCreation employee)
        {
            if (ModelState.IsValid)
                empObj.Add(employee);
        }

        //UPDATE
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] EmployeeForUpdate employee)
        {
            employee.EmployeeId = id;

           // if (ModelState.IsValid)
                empObj.Update(employee);
        }

        //DELETE
        [HttpDelete]
        public void Delete(int id)
        {
            empObj.Delete(id);
        }

        //UPDATE IMAGE
        //[HttpDelete("deleteImage/{id}")]
        //public void deleteImage(int id)
        //{
        //    empObj.removeImageFromDB(id);
        //}

        //INSERT
        [HttpPost("uploadDocuments")]
        public void FileUpload([FromForm] MultipleFile documents)
        {
            //if (ModelState.IsValid)
                empObj.FileUpload(documents);
        }



    }
}

