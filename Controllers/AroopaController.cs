using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AroopaTechnology.Controllers
{
    public class AroopaController : Controller
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public AroopaController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public SqlConnection DbConnection()
        {
            con = new SqlConnection(configuration.GetConnectionString("AroopaConnectionString"));
            con.Open();
            return con;
        }

        // GET: AroopaController
        public ActionResult Index()
        {
            return View();
        }
        

        // GET: AroopaController/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult DeleteEmployee(int EmpId)
        {
            try
            {

                using (con = DbConnection())
                {
                    SqlCommand cmd = new SqlCommand("DeleteSalaryInfo", con);
                    cmd.Parameters.AddWithValue("@EmpId", EmpId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                using (con = DbConnection())
                {
                    SqlCommand cmd = new SqlCommand("DeleteEmployeeInfo", con);
                    cmd.Parameters.AddWithValue("@EmpId", EmpId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return Json("Success");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult ViewEmployee()
        {
            return View();
        }

        public ActionResult UpdateEmployeeDetails(int EmpId, string NameUpdate, string DepartmentUpdate, string GenderUpdate, string MstatusUpdate, string SalUpdate)
        {
            try
            {
                using (con = DbConnection())
                {
                    SqlCommand cmd = new SqlCommand("UpdateEmployeeInfo", con);
                    cmd.Parameters.AddWithValue("@EmpId", EmpId);
                    cmd.Parameters.AddWithValue("@Empname", NameUpdate);
                    cmd.Parameters.AddWithValue("@Empdep", DepartmentUpdate);
                    cmd.Parameters.AddWithValue("@Empgen", GenderUpdate);
                    cmd.Parameters.AddWithValue("@Empmsta", MstatusUpdate);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                using (con = DbConnection())
                {
                    SqlCommand cmd = new SqlCommand("UpdateSalaryInfo", con);
                    cmd.Parameters.AddWithValue("@EmpId", EmpId);
                    cmd.Parameters.AddWithValue("@Empsal", SalUpdate);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }


                return Json("Success");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult EditEmployee(int IdSessionValue)
        {
            List<Dictionary<string, object>> ListCrudRegistration = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictCrudRegistration = new Dictionary<string, object>();
            try
            {

                DataTable TestData = new DataTable();
                using (con = DbConnection())
                {
                    SqlCommand cmd = new SqlCommand("EditEmployeeDetails", con);
                    cmd.Parameters.AddWithValue("@EmpId", IdSessionValue);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(TestData);
                    
                }
                foreach (DataRow DtRow in TestData.Rows)
                {
                    dictCrudRegistration = new Dictionary<string, object>();
                    foreach (DataColumn DtCol in TestData.Columns)
                    {
                        dictCrudRegistration.Add(DtCol.ColumnName, DtRow[DtCol]);
                    }
                    ListCrudRegistration.Add(dictCrudRegistration);


                }

                return Json(ListCrudRegistration);
            }
            catch (Exception ex)
            {
                return Json(ListCrudRegistration);
            }
        }


        [HttpPost]
        public ActionResult InserEmployeeDetails(string EmployeeName, string Department, string Gender, string Mstatus, string Empsal)
        {
            try
            {
                using (con = DbConnection())
                {
                    SqlCommand cmd = new SqlCommand("InsertEmployeeDetails", con);
                    cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                    cmd.Parameters.AddWithValue("@Department", Department);
                    cmd.Parameters.AddWithValue("@Sex", Gender);
                    cmd.Parameters.AddWithValue("@Marital_Status", Mstatus);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                using (con = DbConnection())
                {
                    SqlCommand cmd = new SqlCommand("InsertSalaryInfo", con);
                    cmd.Parameters.AddWithValue("@Salary", Empsal);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return Json("Success");
            }
            catch (Exception)
            {
                return Json("Error");
            }
            
        }
        [HttpPost]
        public ActionResult EmployeeView()
        {
            List<Dictionary<string, object>> ListCrudRegistration = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictCrudRegistration = new Dictionary<string, object>();
            try
            {

                DataTable TestData = new DataTable();
                using (con = DbConnection())
                {
                    SqlCommand cmd = new SqlCommand("EmployeeView", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(TestData);
                    foreach (DataRow DtRow in TestData.Rows)
                    {
                        dictCrudRegistration = new Dictionary<string, object>();
                        foreach (DataColumn DtCol in TestData.Columns)
                        {
                            dictCrudRegistration.Add(DtCol.ColumnName, DtRow[DtCol]);
                        }
                        ListCrudRegistration.Add(dictCrudRegistration);


                    }
                }
                return Json(ListCrudRegistration);
            }
            catch (Exception ex)
            {
                return Json(ListCrudRegistration);
            }
            
        }
    }
}
