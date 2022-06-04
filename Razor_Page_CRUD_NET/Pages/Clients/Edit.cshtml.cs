using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Razor_Page_CRUD_NET.Pages.Clients
{
    public class EditModel : PageModel
    {

        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=THISPC\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string query = "SELECT * FROM clients WHERE id = @id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", id);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    clientInfo.id = "" + sqlDataReader.GetInt32(0);
                    clientInfo.name = sqlDataReader.GetString(1);
                    clientInfo.email = sqlDataReader.GetString(2);
                    clientInfo.phone = sqlDataReader.GetString(3);
                }
                
            }
            catch (Exception ex)
            {
               errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            string id = Request.Query["id"];

            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];

            try
            {
                if (clientInfo.name.Length > 0 && clientInfo.email.Length > 0 && clientInfo.phone.Length > 0)
                {
                    string connectionString = "Data Source=THISPC\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";

                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();

                    string query = "UPDATE clients SET name = @name, email = @email, phone = @phone WHERE id = @id";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@name", clientInfo.name);
                    sqlCommand.Parameters.AddWithValue("@email", clientInfo.email);
                    sqlCommand.Parameters.AddWithValue("@phone", clientInfo.phone);
                    sqlCommand.ExecuteNonQuery();
                    successMessage = "Client Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }
    }
}
