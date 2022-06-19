using Dapper;
using FullStack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using FullStack.Dto;

namespace FullStack.Repository
{
    public class EmployeeRepository
    {
       
        private DbFactory conn;
     
        public EmployeeRepository()
        {
             conn = new DbFactory();
           
        }

       

        //INSERT
        public void Add(EmployeeForCreation employee)
        {
            employee.PhotoFileName= FileToByteArray(employee.PhotoFile);


            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"
                           insert into dbo.Employee
                           (EmployeeName,Department,DateOfJoining,PhotoFileName)
                           values (@EmployeeName,@Department,@DateOfJoining,@PhotoFileName)
                            ";
                dbConnection.Open();
                dbConnection.Execute(sql,employee);
                dbConnection.Close();
            }
        }


        //GET ALL
        public IEnumerable<Employee> GetAll()
        {
            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"
                            select EmployeeId, EmployeeName,Department,
                            convert(varchar(10),DateOfJoining,120) as DateOfJoining,PhotoFileName
                            from
                            dbo.Employee
                            ";
                dbConnection.Open();
                return dbConnection.Query<Employee>(sql);
                


            }
        }


        //GET BY ID
        public Employee GetById(int id)
        {
            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"SELECT * FROM dbo.Employee WHERE EmployeeId=@id";
                dbConnection.Open();

                return dbConnection.Query<Employee>(sql, new { Id = id }).FirstOrDefault();

                dbConnection.Close();

            }
        } 
        //UPDATE
        public void Update(EmployeeForUpdate employee)
        {
            employee.PhotoFileName = FileToByteArray(employee.PhotoFile);
           
            using (IDbConnection dbConnection = conn.Connection)
            {
                if (employee.PhotoFileName != null)
                {
                    string sql = @"
                            update dbo.Employee
                            set EmployeeName= @EmployeeName,
                            Department=@Department,
                            DateOfJoining=@DateOfJoining,
                            PhotoFileName=@PhotoFileName 
                            where EmployeeId=@EmployeeId
                            ";
                    dbConnection.Open();
                    dbConnection.Query(sql, employee);
                    dbConnection.Close();
                }
                else if (employee.IsPhotoNull == true)
                {
                    string sql = @"
                            update dbo.Employee
                            set EmployeeName= @EmployeeName,
                            Department=@Department,
                            DateOfJoining=@DateOfJoining,
                            PhotoFileName=null 
                            where EmployeeId=@EmployeeId
                            ";
                    dbConnection.Open();
                    dbConnection.Query(sql, employee);
                    dbConnection.Close();
                }
                else
                {
                    string sql = @"
                            update dbo.Employee
                            set EmployeeName= @EmployeeName,
                            Department=@Department,
                            DateOfJoining=@DateOfJoining
                            where EmployeeId=@EmployeeId
                            ";
                    dbConnection.Open();
                    dbConnection.Query(sql, employee);
                    dbConnection.Close();
                }
                

            }
        }

        //DELETE

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = conn.Connection)
            {
                string sql = @"
                            delete from dbo.Employee
                            where EmployeeId=@Id
                            ";
                dbConnection.Open();
                dbConnection.Query(sql, new { Id = id });
                dbConnection.Close();

            }
        }

        //UPDATE IMAGE
        //public void removeImageFromDB(int employeeId)
        //{
        //    using (IDbConnection dbConnection = conn.Connection)
        //    {
        //        string sql = @"
        //                    update dbo.Employee
        //                    set PhotoFileName=null 
        //                    where EmployeeId=@employeeId
        //                    ";

        //        dbConnection.Open();
        //        dbConnection.Query(sql, new { EmployeeId = employeeId });
        //        dbConnection.Close();

        //    }
        //}

        //INSERTFILE
        public void FileUpload(MultipleFile documents)
        {
            //if (employee.Files.Count > 0)
            //{}
            foreach (var file in documents.Files)
            {

                documents.FileUp = FileToByteArray(file);
                using (IDbConnection dbConnection = conn.Connection)
                {
                    string sql = @"
                           insert into dbo.Employee
                           (DocumentFile)
                           values (@FileUp)
                            ";
                    dbConnection.Open();
                    dbConnection.Execute(sql, documents);
                    dbConnection.Close();
                }



            }
            int fLength = documents.Files.Count;
            Console.WriteLine(documents.FileUp.Length);







        }


        public static byte[] FileToByteArray(IFormFile file)
        {
            byte[] byteArray = null;
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyToAsync(memoryStream);
                    byteArray = memoryStream.ToArray();
                }
            }
            return byteArray;
        }

    }
}



