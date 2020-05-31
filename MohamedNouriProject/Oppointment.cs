using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using MohamedNouriProject.Resources;
using Excel = Microsoft.Office.Interop.Excel;
namespace MohamedNouriProject
{
    public partial class Oppointment : UserControl
    {

        String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;




        public static Boolean EditorAttribute = false;
        public static string iduser = "";
        public static string CodeClientS = "";

        

        public Oppointment()
        {



            InitializeComponent();


            BindGrid("");



            DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
            bcol.HeaderText = "Supprimer";
            bcol.Text = "Supprimer";
            bcol.Name = "Supprimer";
            bcol.UseColumnTextForButtonValue = true;
            bcol.DataPropertyName = "lnkColumn";
      



            DataGridViewButtonColumn MD = new DataGridViewButtonColumn();
            MD.HeaderText = "Modfier";
            MD.Text = "Modfier";
            MD.DataPropertyName = "lnkColumn";
            MD.Name = "Modfier";
            MD.UseColumnTextForButtonValue = true;



            DataGridViewButtonColumn regler = new DataGridViewButtonColumn();
            regler.HeaderText = "régler";
            regler.Text = "régler";
            regler.DataPropertyName = "lnkColumn";
            regler.Name = "régler";
            regler.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(regler);
            dataGridView1.Columns.Add(MD);
            dataGridView1.Columns.Add(bcol);






            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


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
           


            fr1.Show();

        }
        private void Reg_Click(string ID)
        {

            AddIntervontion fr1 = new AddIntervontion();
         

            fr1.Show();

        }
        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        
        }

        private void Oppointment_Load(object sender, EventArgs e)
        {

        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {


                int orderno = 0;
                Int32.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out orderno);
                


                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Modfier")
            {
                string index = dataGridView1.Rows[e.RowIndex].Cells["CodeRDV"].Value.ToString();
                    EditorAttribute = true;
                iduser = index;

                Edit_Click(index);
             
            }


            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Supprimer")
            {
                string index = dataGridView1.Rows[e.RowIndex].Cells["CodeRDV"].Value.ToString();
                     
                  
                    Delete_Click(index);
            }

            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "régler")
            {
                    EditorAttribute = true;
               
                    string id = dataGridView1.Rows[e.RowIndex].Cells["CodeRDV"].Value.ToString();
                    iduser = id;
                    string code = dataGridView1.Rows[e.RowIndex].Cells["CodeClient"].Value.ToString();
                    CodeClientS = code;

                    Reg_Click(CodeClientS);
            }


            }



        }
        private void BindGrid(String query)
        {
           // this.dataGridView1.DataSource = null;
           //  this.dataGridView1.Rows.Clear();
      
            //ORDER BY RDV.DateRDV , RDV.Time 
            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CodeRDV, Client.CodeClient  , Name, LastName, Time, Comments  FROM RDV  INNER JOIN  " +
               "Client ON  RDV.ClientID = Client.CodeClient    "+ query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                          
                        
                            totlaNumbre.Text = dt.Rows.Count.ToString();


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


            String query = "RDV.CodeRDV like '#replace#%'";
            var result = query.Replace("#replace#", CodeClient.Text);

            BindGrid(result);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)


        {

            String query = " Client.Name like '#replace# %' ";


            var result = query.Replace("#replace#", comments.Text);

            BindGrid(result);






        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {



            

          
            foreach (DataGridViewRow Myrow in dataGridView1.Rows)

            {
 
                String valur = Myrow.Cells[1].Value.ToString();

                if (valur.Length > 1)
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
            String query = "RDV.DateRDV  = '#replace#' ";


            var result = query.Replace("#replace#", dt.ToString());

            BindGrid(result);




        }

        private void hommeRadio_CheckedChanged(object sender, EventArgs e)
        {

            String query = "Client.Sexe Like 'Homme%' ";




            BindGrid(query);
        }

        private void radioFemme_CheckedChanged(object sender, EventArgs e)
        {
            String query = "Client.Sexe Like 'Femme%' ";




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

 

        private void button1_Click(object sender, EventArgs e)
        {

            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }




        private void copyAlltoClipboard()
        {

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            BindGrid("");
        }

        private void totlaNumbre_Click(object sender, EventArgs e)
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
