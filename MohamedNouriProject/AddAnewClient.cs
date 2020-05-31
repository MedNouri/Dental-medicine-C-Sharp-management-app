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


            if (ClientList.EditorAttribute)
            {


                button1.Text = "Modifier";

              
            }


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         //   MessageBox.Show(ClientList.iduser);


            if (ValidateChildren(ValidationConstraints.Enabled))
            {
              



            string sex = "";
            if (radioFemme.Checked)
            {
                sex = "Femme";
            }
            else
            {
                sex = "Hemme";
            }


            if (ClientList.EditorAttribute)
            {


             UpdateFournisseur(ClientList.iduser,userName.Text, lastName.Text, date.Value.Date, sex, adresse.Text, work.Text, telNumber.Text, email.Text);


                return;
            }
         


                InsertFournisseur(userName.Text,lastName.Text,date.Value.Date, sex, adresse.Text, work.Text , telNumber.Text,email.Text);

            }
            else
            {
                MessageBox.Show("Veuillez vérifier votre saisie et réessayer!");

                return;
            }
        }







        private void UpdateFournisseur(string id, string Name, string LastName, DateTime DateOfBirth, string Sexe, string Adresse, string Profession, string TelNumber, string Email)
        {









            string Query = " UPdate   Client SET  Name = @CLIENT_Name , LastName = @CLIENT_LastName, DateOfBirth = @CLIENT_DateOfBirth, Sexe = @CLIENT_SEXE, Adresse = @CLIENT_Adresse, Profession = @CLIENT_Profession, TelNumber = @CLIENT_TelNumber, Email = @CLIENT_Email WHERE CodeClient   = @fn";
             

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
                
 command.Parameters.AddWithValue("@fn", id);
                if (command.ExecuteNonQuery() == 0)
                    throw new ApplicationException("Aucune ligne a ete a  jour , vérifiez les paramètres!");
                else
                    MessageBox.Show("Updated");
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
            while (Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            this.Close();
            this.Close();
        }

        private void userName_Validating(object sender, CancelEventArgs e)
        {
            isEmpty(userName, e);
        }

        private void userPrenom_Validating(object sender, CancelEventArgs e)
        {
            isEmpty(lastName, e);
        }

        private void telNumber_Validating(object sender, CancelEventArgs e)
        {
            isEmpty(telNumber, e);
        }

        private void work_Validating(object sender, CancelEventArgs e)
        {
            isEmpty(work, e);
        }

        private void adresse_Validating(object sender, CancelEventArgs e)
        {
            isEmpty(adresse, e);
        }



       void isEmpty(TextBox textB, CancelEventArgs e)

        {
            if (string.IsNullOrWhiteSpace(textB.Text))
            {
                e.Cancel = true;
                lastName.Focus();
                errorProvider1.SetError(textB, "ne doit pas être laissé en blanc!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textB, "");
            }
        }

        private void email_Validating(object sender, CancelEventArgs e)
        {
            isEmpty(email, e);
        }
    }
}
