using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Razor_Page_CRUD_NET.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];

            if (clientInfo.name.Length != 0 && clientInfo.email.Length != 0 && clientInfo.phone.Length != 0)
            {
                try
                {
                    
                    string connectionString = "Data Source=THISPC\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();

                    string query = "INSERT INTO clients (name, email, phone) VALUES (@name, @email, @phone)";

                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@name", clientInfo.name);
                    sqlCommand.Parameters.AddWithValue("@email", clientInfo.email);
                    sqlCommand.Parameters.AddWithValue("@phone", clientInfo.phone);

                    sqlCommand.ExecuteNonQuery();

                    successMessage = "Client Added Successfully";


                } catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
        }
    }
}
