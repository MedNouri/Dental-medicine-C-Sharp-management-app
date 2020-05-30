using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;




namespace MohamedNouriProject
{
    public partial class AddAnOppointment : Form
    {
        public Boolean Update = false;
        public string ID = "0";


        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi");
        String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
        DataTable dt = new DataTable();
        public AddAnOppointment()
        {
            InitializeComponent();


            comboBox2.Items.Add("08:00 - 09:00");
            comboBox2.Items.Add("09:00 - 10:00");
            comboBox2.Items.Add("10:00 - 11:00");
            comboBox2.Items.Add("11:00 - 12:00");
            comboBox2.Items.Add("13:00 - 14:00");
            comboBox2.Items.Add("14:00 - 15:00");
            comboBox2.Items.Add("15:00 - 16:00");
            comboBox2.Items.Add("16:00 - 17:00");
            comboBox2.SelectedIndex = 0;



            GetClientLIst();

            if (Update ) {


                save.Text = "Modifier";

            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {
     
        }
     


        private void GetClientLIst()
        {

        
      
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Client", connection);
 
        da.Fill(dt);

            Clients.DataSource=dt; // setting the datasource property of combobox
            Clients.DisplayMember= "LastName"; // Display Member which will display on screen
            Clients.ValueMember= "CodeClient";


            connection.Close();

        }

        private void AddAnOppointment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertFournisseur(commetns.Text,Clients.SelectedValue.ToString(), date.Value.Date, comboBox2.Text);
        }


        private void UpdateData(string comment, string ClientID, DateTime DateRDV, String time)
        {
      
  Int32 returnValue = 0;

            using (SqlConnection con = new SqlConnection(connetionString))
            {
                
              con.Open();

           

                try
                {
                   
                        string query = " UPDATE  RDV SET Comments = @CLIENT_comment , DateRDV = @CLIENT_RDV, Time = @CLIENT_time ,ClientID = @CLIENT_ID WHERE CodeRDV = @CLIENT_RDV ";





                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@CLIENT_comment", comment);
                        command.Parameters.AddWithValue("@CLIENT_RDV", ID);
                        command.Parameters.AddWithValue("@CLIENT_DateRDV", DateRDV);
                        command.Parameters.AddWithValue("@CLIENT_time", time);
                        command.Parameters.AddWithValue("@CLIENT_ID", ClientID);



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

        }
    


    private void InsertFournisseur(string comment, string ClientID, DateTime DateRDV, String time)
        {


            if (Update)
            {

                UpdateData( comment,  ClientID,  DateRDV,  time);


                return;


            }


            string query = "SELECT  COUNT(*) from RDV WHERE DateRDV = @date and Time=@time";
            Int32 returnValue = 0;
           
            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {
                    sqlcmd.Parameters.Add("@date", SqlDbType.VarChar).Value = DateRDV;
                    sqlcmd.Parameters.Add("@time", SqlDbType.VarChar).Value = time;
                    con.Open();
                    
                    returnValue = Convert.ToInt32(sqlcmd.ExecuteScalar());
                }
            }
            if (returnValue == 0)
            {

                try
                {
                    connection.Open();
                    string Query = " INSERT INTO RDV (Comments, DateRDV, Time, ClientID)" +
            " VALUES (" +
            "@CLIENT_comment," +

            "@CLIENT_DateRDV," +

            " @CLIENT_time  ," +
              "@CLIENT_ClientID)";


                    ;

                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@CLIENT_comment", comment);
                    command.Parameters.AddWithValue("@CLIENT_ClientID", ClientID);
                    command.Parameters.AddWithValue("@CLIENT_DateRDV", DateRDV);
                    command.Parameters.AddWithValue("@CLIENT_time", time);



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
                MessageBox.Show("cette date n'est plus disponible");
           

                return;

            }


        
  
        


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Annuler_Click(object sender, EventArgs e)
        {

        }
    }
}
