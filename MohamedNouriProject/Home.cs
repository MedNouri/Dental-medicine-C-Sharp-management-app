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
 
    public partial class Home : Form
    {


        
        public string   usernameValue

        {
            set { userName.Text = value; }
        }



        private String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
        public Home()
        {
            InitializeComponent();

            //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
          

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            //   Oppointment usersList = new Oppointment();
            //  usersList.Dock = DockStyle.Fill;
            //   usersList.AutoScroll = true;
            //  this.rootPanel.Controls.Clear();
            //  this.rootPanel.Controls.Add(usersList);

            //  usersList.Show();
            GetResumNumber();
         
            fillChart2();
        }



        private void GetResumNumber()
        {

         
            string query = "SELECT COUNT(*) FROM Client ;";
            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand(query, con))
                {

                    con.Open();
                   // returnValue = (string)sqlcmd.ExecuteScalar();
                    Int32 count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    sumLabel1.Text = count.ToString();
                }
           



            string query2 = "SELECT COUNT(*) FROM RDV ;";
              using (SqlCommand sqlcmd = new SqlCommand(query2, con))
                {

                  
                    // returnValue = (string)sqlcmd.ExecuteScalar();
                    Int32 count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    sumLabel2.Text = count.ToString();
                }


                string query3 = "SELECT COUNT(*) FROM Interventions ;";
                using (SqlCommand sqlcmd = new SqlCommand(query3, con))
                {


                    // returnValue = (string)sqlcmd.ExecuteScalar();
                    Int32 count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    sumLabel3.Text = count.ToString();
                }


                string query5 = "SELECT SUM(cout) FROM Interventions ;";
                using (SqlCommand sqlcmd = new SqlCommand(query5, con))
                {


                    // returnValue = (string)sqlcmd.ExecuteScalar();
                    Int32 count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    Sum.Text = count.ToString() + " TND ";
                }







                con.Close();
                BindGrid();
            }
        }

        private void BindGrid()
        {
 

            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM RDV where  DateRDV  = '" + DateTime.Today.ToString("dd-MM-yyyy") + "' Order By Time", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Set AutoGenerateColumns False
                            dataGridView1.AutoGenerateColumns = false;

                            //Set Columns Count
                            dataGridView1.ColumnCount = 3;

                            //Add Columns

                            dataGridView1.Columns[0].HeaderText = "Code RDV";
                            dataGridView1.Columns[0].Name = "CodeRDV";
                            dataGridView1.Columns[0].DataPropertyName = "CodeRDV";


                            dataGridView1.Columns[1].Name = "Time";
                            dataGridView1.Columns[1].HeaderText = "Time";
                            dataGridView1.Columns[1].DataPropertyName = "Time";



                            dataGridView1.Columns[2].Name = "Comments";
                            dataGridView1.Columns[2].HeaderText = "Comments";
                            dataGridView1.Columns[2].DataPropertyName = "Commentaire";



                          



                            NbreTotalRDV.Text = dt.Rows.Count.ToString();


                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'dentaldoctorDataSet.RDV'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            // this.rDVTableAdapter.Fill(this.dentaldoctorDataSet.RDV);
            // TODO: cette ligne de code charge les données dans la table 'dentaldoctorDataSet.RDV'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            //   this.dataGridView1.Fill(this.dentaldoctorDataSet.RDV);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // MessageBox.Show(" Value at 0,0" + dataGridView1.Rows[0].Cells[0].Value);
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                //this.rDVTableAdapter.FillBy(this.dentaldoctorDataSet.RDV);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void listeDesRendezVousToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
        }

        private void rdvButton_Click(object sender, EventArgs e)
        {

            rootPanel.Controls.Clear();
              Oppointment usersList = new Oppointment();
           usersList.Dock = DockStyle.Fill;
           usersList.AutoScroll = true;

             this.rootPanel.Controls.Add(usersList);
            usersList.Show();

            //rootPanel.Refresh();

        }



        private void UserList(object sender, EventArgs e)
        {
            rootPanel.Controls.Clear();
            DentsListe usersList = new DentsListe();
             usersList.Dock = DockStyle.Fill;
             usersList.AutoScroll = true;
       
             this.rootPanel.Controls.Add(usersList);

            usersList.Show();
        }


        private void rootPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addANewClientToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AddAnewClient fr1 = new AddAnewClient();
            //fr1.MdiParent = this;

            fr1.Show();
            
        }

        private void addARDVToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AddAnOppointment fr1 = new AddAnOppointment();
         

            fr1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //  this.rootPanel.Controls.
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
         
        }
       
        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            History history = new History();
            rootPanel.Controls.Clear();
            history.Dock = DockStyle.Fill;
            history.AutoScroll = true;

            this.rootPanel.Controls.Add(history);

            history.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("êtes-vous sûr de vouloir vous déconnecter ?", "Déconnecter ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

          

            if (dr == DialogResult.Yes)
            {
                //
                Authentification fr1 = new Authentification();
                fr1.Show();
                this.Hide();
            }
        
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }


        private void fillChart2()
        {

             


            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select Cout,CodeDent from Interventions", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);


                            chart1.DataSource = dt;
                             
                            chart1.Series["Cout"].XValueMember = "CodeDent";
                             
                            chart1.Series["Cout"].YValueMembers = "Cout";
                            chart1.Titles.Add("Cout / Dent");




                        }

                    }

}
            }
        }

        private void usersbuttons_Click(object sender, EventArgs e)
        {
            


                 rootPanel.Controls.Clear();
            ClientList usersList = new ClientList();
            usersList.Dock = DockStyle.Fill;
            usersList.AutoScroll = true;

            this.rootPanel.Controls.Add(usersList);

            usersList.Show();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rootPanel.Controls.Clear();
            History usersList = new History();
            usersList.Dock = DockStyle.Fill;
            usersList.AutoScroll = true;

            this.rootPanel.Controls.Add(usersList);

            usersList.Show();
        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addDentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AddDent fr1 = new AddDent();
            fr1.Show();
           
        }
    }
}
