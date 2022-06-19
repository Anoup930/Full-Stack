using FullStack.Models;
using FullStack.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {


        private DepartmentRepository depObj;
        public DepartmentController()
        {
            depObj = new DepartmentRepository();
        }


        //Get ALL
        [HttpGet]
        [Route("Get")]
        public IEnumerable<Department> GetAll()
        {
            return depObj.GetAll();
        }

        //Get By Id
        [HttpGet]
        [Route("Get/{id}")]
        public Department GetById(int id)
        {
            return depObj.GetById(id);

        }



        //INSERT
        [HttpPost]
        public void Post([FromBody] Department department)
        {
            if (ModelState.IsValid)
                depObj.Add(department);
        }

        //UPDATE
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Department department)
        {
            department.DepartmentId = id;

            if (ModelState.IsValid)
                depObj.Update(department);
        }

        //DELETE
        [HttpDelete]
        public void Delete(int id)
        {
            depObj.Delete(id);
        }
    }
}   

