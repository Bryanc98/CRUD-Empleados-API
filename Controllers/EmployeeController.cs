using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

//Aqui estan los Metodos para la manipulacion de los datos (API) Get, Post, Put, Delete y una adicional para visualizar todos los departamentos
//almacenados.

namespace EmployeeApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select EmployeeID,EmployeeName,Departament,
                    convert(varchar(10),DateOfJoining,120) as 
                    DateOfJoining,PhotoFileName from dbo.Employee
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Employee emp)
        {
            try
            {
                string query = @"
                        insert into dbo.Employee values
                        (
                          '" + emp.EmployeeName + @"'
                         ,'" + emp.Departament + @"'
                         ,'" + emp.DateOfJoining + @"'
                         ,'" + emp.PhotoFileName + @"'
                         )    
                         ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successully!!";
            }
            catch (Exception)
            {
                return "Failed to Add!!";
            }
        }

        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                        update dbo.Employee set
                        EmployeeName='" + emp.EmployeeName + @"'
                        ,Departament='" + emp.Departament + @"'
                        ,DateOfJoining='" + emp.DateOfJoining + @"'
                        ,PhotoFileName='" + emp.PhotoFileName + @"'
                        where EmployeeID=" + emp.EmployeeID + @"
                                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successully!!";
            }
            catch (Exception)
            {
                return "Failed to Update!!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                        delete from dbo.Employee 
                        where EmployeeID=" + id + @"
                                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successully!!";
            }
            catch (Exception)
            {
                return "Failed to Delete!!";
            }
        }

        [Route("api/Employee/GetAllDepartamentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"
                        select DepartamentName from dbo.Departaments";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK,table);
        }


        //Api para subir las fotos de los empleados

        [Route("api/Employee/SaveFile")]
        public string SafeFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;
            }
            catch(Exception)
            {
                return "profile.png";
            }
        }


    }
}
