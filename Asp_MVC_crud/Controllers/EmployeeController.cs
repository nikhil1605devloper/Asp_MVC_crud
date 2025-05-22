using Asp_MVC_crud.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Asp_MVC_crud.Controllers
{
    public class EmployeeController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // 👇 This will run before any action method in this controller
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Username"] == null)
            {
                // Redirect to Login if session is not set
                filterContext.Result = RedirectToAction("Login", "Account");
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        // 1. Index - List all employees
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Employees";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Department = rdr["Department"].ToString()
                    });
                }
            }
            return View(employees);
        }

        // 2. Create
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Employees (Name, Email, Department) VALUES (@Name, @Email, @Department)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // 3. Edit
        public ActionResult Edit(int id)
        {
            Employee emp = new Employee();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Employees WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    emp.Id = Convert.ToInt32(rdr["Id"]);
                    emp.Name = rdr["Name"].ToString();
                    emp.Email = rdr["Email"].ToString();
                    emp.Department = rdr["Department"].ToString();
                }
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Employees SET Name=@Name, Email=@Email, Department=@Department WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // 4. Delete
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM Employees WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}



