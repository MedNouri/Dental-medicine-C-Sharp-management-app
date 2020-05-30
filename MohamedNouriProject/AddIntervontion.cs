using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace MohamedNouriProject.Resources
{
    public partial class AddIntervontion : Form
    {

        public String CodeRDV = "35";
        public int CodeDent = 1;
        public int CodeClient = 35;
        String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
        public AddIntervontion()
        {
            InitializeComponent();



            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Dent", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);

                            //Assign DataTable as DataSource.
                     
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                checkedListBox.Items.Add(dt.Rows[i]["CodeDent"].ToString());

                            }
                        }
                    }

                }

            }
        }

        private void AddIntervontion_Load(object sender, EventArgs e)
        {

         
        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertFournisseur(Cout.Text, CodeClient, DateTime.Now , NbreInt.Text, Observations.Text, comboBox2.Text, CodeDent );
        }

        

        private void InsertFournisseur(String Cout, int CodeClient, DateTime Date, String Nbre,String observation, String Acte, int CodeDent )
        {

            using (SqlConnection
                    connection = new SqlConnection(connetionString))
            {

                try
                {
                    connection.Open();
                    string Query = " INSERT INTO Interventions (Cout, Date, Nbre, CodeClient,observation,Acte,CodeDent)" +
            " VALUES ( @CLIENT_Cout, @CLIENT_Date, @CLIENT_Nbre  , @CLIENT_CodeClient , @CLIENT_observation  , @CLIENT_Acte  ,@CLIENT_CodeDent)" ;


                     

                    SqlCommand command = new SqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@CLIENT_Cout", Cout);
                    command.Parameters.AddWithValue("@CLIENT_CodeClient", CodeClient);
                    command.Parameters.AddWithValue("@CLIENT_Date", Date);
                    command.Parameters.AddWithValue("@CLIENT_Nbre", Nbre);
                    command.Parameters.AddWithValue("@CLIENT_observation", observation);
                    command.Parameters.AddWithValue("@CLIENT_Acte", Acte);

                    command.Parameters.AddWithValue("@CLIENT_CodeDent", CodeDent);





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


    }

}
