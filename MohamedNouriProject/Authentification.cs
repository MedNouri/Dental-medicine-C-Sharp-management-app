using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MohamedNouriProject
{
    public partial class Authentification : Form
    {

        public string usernameValue
        {
            get { return "Mohamed"; }
        }


        private String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
        public Authentification()
        {
            InitializeComponent();
            this.login.Select();

           password.Text = "";
            // The password character is an asterisk.
            password.PasswordChar = '*';
            // The control will allow no more than 14 characters.
            password.MaxLength = 14;
        }

        protected override void OnShown(EventArgs e)
        {
         
          
            base.OnShown(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (ValidateChildren(ValidationConstraints.Selectable))
            {
                loginUser();
            } 
         
           
         
        }



        private void loginUser()
        {
            string query = "SELECT role from Users WHERE Login = @username and Password=@password";
            string returnValue = "";
   
            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {
                    sqlcmd.Parameters.Add("@username", SqlDbType.VarChar).Value = login.Text;
                    sqlcmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password.Text;
                    con.Open();
                    returnValue = (string)sqlcmd.ExecuteScalar();
                }
            } 
            if (String.IsNullOrEmpty(returnValue))
            {
                MessageBox.Show("Incorrect username or password");
                return;
            }
            returnValue = returnValue.Trim();
            if (returnValue == "Admin")
             {

           //MessageBox.Show("You are logged in as an Admin");
             

                Home fr1 = new Home();
                //fr1.MdiParent = this;
                fr1.usernameValue = returnValue;

                fr1.Show();
                this.Hide();
            }


            //else if (returnValue == "User")
            //{
              //  MessageBox.Show("You are logged in as a User");
                //UserHome fr2 = new UserHome();
                //fr2.Show();
                //this.Hide();
            //}
        }



        private void button1_Click_1(object sender, EventArgs e)
        {

        }
       

        private void userLogin_Validating(object sender, CancelEventArgs e)
        {
     
            if (string.IsNullOrWhiteSpace(login.Text))
            {
                e.Cancel = true;
                login.Focus();
                errorProviderApp.SetError(login, "login should not be left blank!");
                erroLogin.Text = "Login should not be left blank!";
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(login, "");
                erroLogin.Text = "";
            }
        }
        private void userpassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password.Text))
            {
                e.Cancel = true;
                password.Focus();
                errorProviderApp.SetError(password, "Name should not be left blank!");
                errorpassword.Text = "password should not be left blank!";

            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(password, "");
                errorpassword.Text = "";
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
