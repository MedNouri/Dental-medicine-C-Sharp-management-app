using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using MohamedNouriProject.Resources;

namespace MohamedNouriProject
{
    public partial class Oppointment : UserControl
    {

        String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;

        public Oppointment()
        {



            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BindGrid("SELECT CodeRDV,Name,LastName,Time,Comments FROM RDV  INNER JOIN  " +
               "Client ON  RDV.ClientID = Client.CodeClient  ORDER BY RDV.DateRDV , RDV.Time ");

            DataGridViewImageColumn Editlink = new DataGridViewImageColumn();
         //   Editlink.UseColumnTextForLinkValue = true;
            Editlink.HeaderText = "Edit";
            Editlink.DataPropertyName = "lnkColumn";
            Editlink.Image = new Bitmap(SystemIcons.Exclamation.ToBitmap(), 8, 8);
            //  Editlink.Text = "Edit";
            dataGridView1.Columns.Add(Editlink);

            DataGridViewImageColumn Deletelink = new DataGridViewImageColumn();
          //  Deletelink.UseColumnTextForLinkValue = true;
            Deletelink.HeaderText = "delete";
            Deletelink.DataPropertyName = "lnkColumn";
            Deletelink.Image = new Bitmap(SystemIcons.Application.ToBitmap(), 8, 8);
            // Deletelink.Text = "Delete";
            dataGridView1.Columns.Add(Deletelink);

 
        }




        private void  Delete_Click(string ID)
        {
            DialogResult dr = MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + ID, "supprimer ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);



            if (dr == DialogResult.Yes)
            {
                //

                String query = "DELETE FROM RDV WHERE CodeRDV =" + ID;
                SqlConnection con = new SqlConnection(connetionString);

                SqlCommand sqlcom = new SqlCommand(query, con);

                con.Open();

                try
                {
                    sqlcom.ExecuteNonQuery();
                    MessageBox.Show("delete successful");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();


            }
       
              
           
        }


        private void Edit_Click(string ID)
        {

            AddAnOppointment fr1 = new AddAnOppointment();
            fr1.Update = true;
            fr1.ID = ID;


            fr1.Show();

        }

        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            return;
            if (e.RowIndex < 0)
                return;


          
            //I supposed your button column is at index 0
            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = 20;
                var h = 20;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.iconfinder_user_male2_172626, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void Oppointment_Load(object sender, EventArgs e)
        {

        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string index = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
         
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Edit")
            {

            Edit_Click(index);
             
            }


            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "delete")
            {
                
           

                Delete_Click(index);
            }






        }
        private void BindGrid(String query)
        {

            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Set AutoGenerateColumns False

                            dataGridView1.DataSource = dt;

                             

                        }
                    }
                }
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void telNumber_TextChanged(object sender, EventArgs e)
        {
            //  SELECT* FROM RDV ,Client Where RDV.ClientID = Client.CodeClient AND RDV.CodeRDV like '#replace#%


            String query = "SELECT * FROM RDV  INNER JOIN  " +
                "Client ON  RDV.ClientID = Client.CodeClient   Where  RDV.CodeRDV like '#replace#%'";
            var result = query.Replace("#replace#", CodeClient.Text);

            BindGrid(result);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)


        {

            String query = "SELECT * FROM RDV INNER JOIN Client ON  RDV.ClientID = Client.CodeClient   Where  Client.Name like '#replace# %' ";


            var result = query.Replace("#replace#", comments.Text);

            BindGrid(result);






        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {



            

            return;
            foreach (DataGridViewRow Myrow in dataGridView1.Rows)

            {

                return;
                String valur = Myrow.Cells[1].Value.ToString();
                DateTime d1 = DateTime.Parse(valur);
                DateTime d2 = new DateTime();

                int res = DateTime.Compare(d1, d2);
                if (res > 0)
                {
                    Myrow.DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                else
                {
                    Myrow.DefaultCellStyle.BackColor = Color.IndianRed;
                }
            }





        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            DateTime dt = this.dateTimePicker1.Value.Date;
            String query = "SELECT * FROM RDV INNER JOIN Client ON  RDV.ClientID = Client.CodeClient   Where RDV.DateRDV  = '#replace#' ";


            var result = query.Replace("#replace#", dt.ToString());

            BindGrid(result);




        }

        private void hommeRadio_CheckedChanged(object sender, EventArgs e)
        {

            String query = "SELECT * FROM RDV INNER JOIN Client ON  RDV.ClientID = Client.CodeClient   Where  Client.Sexe Like 'Homme%' ";




            BindGrid(query);
        }

        private void radioFemme_CheckedChanged(object sender, EventArgs e)
        {
            String query = "SELECT * FROM RDV INNER JOIN Client ON  RDV.ClientID = Client.CodeClient   Where  Client.Sexe Like 'Fomme%' ";




            BindGrid(query);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            return;

            AddIntervontion usersList = new AddIntervontion();



            usersList.Show();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void exporter_Click(object sender, EventArgs e)
        {

        }
    }
}


/*
  dataGridView1.AutoGenerateColumns = false;

                            //Set Columns Count
                            dataGridView1.ColumnCount = 6 ;

                            //Add Columns

                            dataGridView1.Columns[0].HeaderText = "CodeRDV";
                            dataGridView1.Columns[0].Name = "CodeRDV";
                            dataGridView1.Columns[0].DataPropertyName = "CodeRDV";


                            dataGridView1.Columns[1].Name = "DateRDV";
                            dataGridView1.Columns[1].HeaderText = "DateRDV";
                            dataGridView1.Columns[1].DataPropertyName = "DateRDV";



                            dataGridView1.Columns[2].Name = "Comments";
                            dataGridView1.Columns[2].HeaderText = "Comments";
                            dataGridView1.Columns[2].DataPropertyName = "Comments";



                            dataGridView1.Columns[3].Name = "Name";
                            dataGridView1.Columns[3].HeaderText = "Name";
                            dataGridView1.Columns[3].DataPropertyName = "Name";




                            dataGridView1.Columns[4].Name = "LastName";
                            dataGridView1.Columns[4].HeaderText = "LastName";
                            dataGridView1.Columns[4].DataPropertyName = "LastName";

                            dataGridView1.Columns[5].Name = "Time";
                            dataGridView1.Columns[5].HeaderText = "Time";
                            dataGridView1.Columns[5].DataPropertyName = "Time";
*/
