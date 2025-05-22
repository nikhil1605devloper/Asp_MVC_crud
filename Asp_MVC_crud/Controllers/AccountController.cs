using Asp_MVC_crud.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Asp_MVC_crud.Controllers
{
    public class AccountController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count == 1)
                {
                    Session["Username"] = user.Username;
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ViewBag.Message = "Invalid Username or Password";
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}
