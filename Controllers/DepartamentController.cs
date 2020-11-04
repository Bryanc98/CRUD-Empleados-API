using EmployeeApi.Areas.HelpPage.ModelDescriptions;
using EmployeeApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeApi.Controllers
{
    public class DepartamentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select DepartamentID,DepartamentName from
                    dbo.Departaments
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
        public string Post(Departaments dep)
        {
            try
            {
                string query = @"
                        insert into dbo.Departaments values
                        ('"+dep.DepartamentName+@"')    
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
            catch(Exception)
            {
                return "Failed to Add!!";
            }
        }

        public string Put(Departaments dep)
        {
            try
            {
                string query = @"
                        update dbo.Departaments set DepartamentName=
                        '" + dep.DepartamentName + @"'
                        where DepartamentID="+dep.DepartamentID+@"
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
                        delete from dbo.Departaments 
                        where DepartamentID=" + id + @"
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
    }
}
