using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace MohamedNouriProject
{
    public partial class AddDent : Form
    {

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi");
        String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
        DataTable dt = new DataTable();
        public AddDent()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            string query = "SELECT  COUNT(*) from Dent WHERE Description = @Des ";
            Int32 returnValue = 0;

            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {
                    sqlcmd.Parameters.Add("@Des", SqlDbType.VarChar).Value = Description.Text;
 
                    con.Open();

                    returnValue = Convert.ToInt32(sqlcmd.ExecuteScalar());
                }
            }
            if (returnValue == 0)
            {

                try
                {
                    connection.Open();
                    string Query = " INSERT INTO Dent (Description)" +
            " VALUES (" +
            "@CLIENT_comment )" ;


                    ;

                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@CLIENT_comment",  Description.Text);
                     



                    if (command.ExecuteNonQuery() == 0)
                        throw new ApplicationException("Aucune ligne insérée, vérifiez les paramètres!");
                    else
                        MessageBox.Show("Created");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();

                }
            }
            else
            {
                MessageBox.Show("cette  Description  n'est plus disponible");


                return;

            }

        }

        private void Annuler_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
