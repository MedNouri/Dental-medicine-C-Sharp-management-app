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

 
    public partial class AddAnewClient : Form
    {
        public AddAnewClient()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertFournisseur(
               userName.Text,lastName.Text,date.Value.Date, "Homme", adresse.Text, work.Text , telNumber.Text,email.Text);;
        }



        






 
 
        private void InsertFournisseur(string Name, string LastName, DateTime DateOfBirth, string Sexe, string Adresse, string Profession, string TelNumber, string Email)
        {
            string Query = " INSERT INTO Client (Name, LastName, DateOfBirth, Sexe, Adresse, Profession, TelNumber, Email)" +
                " VALUES (" +
                "@CLIENT_Name," +
                "@CLIENT_LastName," +
                "@CLIENT_DateOfBirth," +
                " @CLIENT_SEXE," +
                "@CLIENT_Adresse ," +
                "@CLIENT_Profession," +
                "@CLIENT_TelNumber," +
                "@CLIENT_Email" +
                ") ";
 
 ;

            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi");
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Query, connection);
                command.Parameters.AddWithValue("@CLIENT_Name", Name);
                command.Parameters.AddWithValue("@CLIENT_LastName", LastName);
                command.Parameters.AddWithValue("@CLIENT_SEXE", Sexe);
                command.Parameters.AddWithValue("@CLIENT_DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@CLIENT_Adresse", Adresse);
                command.Parameters.AddWithValue("@CLIENT_Profession", Profession);
                command.Parameters.AddWithValue("@CLIENT_TelNumber", TelNumber);
                command.Parameters.AddWithValue("@CLIENT_Email", Email);


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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Annuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
