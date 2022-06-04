using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Razor_Page_CRUD_NET.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClient = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                 string connectionString = "Data Source=THISPC\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string query = "SELECT * FROM clients";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ClientInfo clientInfo = new ClientInfo();
                    clientInfo.id = " " + sqlDataReader.GetInt32(0);
                    clientInfo.name = sqlDataReader.GetString(1);
                    clientInfo.email = sqlDataReader.GetString(2);
                    clientInfo.phone = sqlDataReader.GetString(3);

                    listClient.Add(clientInfo);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }

    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
    }
}
