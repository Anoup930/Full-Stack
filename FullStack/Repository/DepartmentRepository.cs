using Dapper;

using FullStack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace FullStack.Repository
{
    public class DepartmentRepository
    {


        private DbFactory conn;
        public DepartmentRepository()
        {
             conn = new DbFactory();
        }

       



        //INSERT
        public JsonResult Add(Department dep)
        {

            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"
                             insert into dbo.Department
                             values (@DepartmentName)
                             ";
                dbConnection.Open();
                dbConnection.Execute(sql, dep);
                dbConnection.Close();
            }
            return new JsonResult("Added Successfully");
        }

        //GET ALL
        public IEnumerable<Department> GetAll()
        {
            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"
                             select DepartmentId, DepartmentName from
                             dbo.Department
                            ";

                dbConnection.Open();
                return dbConnection.Query<Department>(sql);

                dbConnection.Close();


            }
        }
        //GET BY ID
        public Department GetById(int id)
        {
            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"SELECT * FROM dbo.Department WHERE DepartmentId=@id";
                dbConnection.Open();

                return dbConnection.Query<Department>(sql, new { Id = id }).FirstOrDefault();

                dbConnection.Close();

            }
        }



        //UPDATE
        public void Update(Department dep)
        {
            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"
                            update dbo.Department
                            set DepartmentName= @DepartmentName
                            where DepartmentId=@DepartmentId
                            ";

                dbConnection.Open();
                dbConnection.Query(sql, dep);
                dbConnection.Close();

            }
        }

        //DELETE

        public JsonResult Delete(int id)
        {
            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"
                            delete from dbo.Department
                            where DepartmentId=@Id
                            ";
                dbConnection.Open();
                dbConnection.Query(sql, new { Id = id });
                dbConnection.Close();

            }
            return new JsonResult ("Deleted Succesfully");
        }
    }
}
