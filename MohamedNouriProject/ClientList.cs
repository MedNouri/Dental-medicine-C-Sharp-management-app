using System;
using System.Windows.Forms;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
namespace MohamedNouriProject
{
    public partial class ClientList : UserControl
    {


        public static  Boolean EditorAttribute = false;
        public static  string iduser = "";
        public ClientList()
        {
            InitializeComponent();
            Console.WriteLine("Hello there");
            //   dataGridView1.Update();
    
            BindGrid("");
            DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
            bcol.HeaderText = "Supprimer";
            bcol.Text = "Supprimer";
            bcol.Name = "Supprimer";
            bcol.UseColumnTextForButtonValue = true;
            bcol.DataPropertyName = "lnkColumn";
            dataGridView2.Columns.Add(bcol);



            DataGridViewButtonColumn MD = new DataGridViewButtonColumn();
            MD.HeaderText = "Modfier";
            MD.Text = "Modfier";
            MD.DataPropertyName = "lnkColumn";
            MD.Name = "Modfier";
            MD.UseColumnTextForButtonValue = true;
     
            dataGridView2.Columns.Add(MD);

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }


        private String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";



        private void BindGrid(String query)
        {

            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Client  " + query  , con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Set AutoGenerateColumns False
                            totlaNumbre.Text = dt.Rows.Count.ToString();

                            dataGridView2.DataSource = dt;



                        }
                    }
                }
            }
        }

 



        private void Delete_Click(string ID)
        {
            DialogResult dr = MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + ID, "Supprimer ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);



            if (dr == DialogResult.Yes)
            {
                //

                String query = "DELETE FROM Client WHERE CodeClient =" + ID;
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
 
     


        private void button2_Click(object sender, EventArgs e)
        {
            BindGrid("");
        }

        private void comments_TextChanged(object sender, EventArgs e)
        {
            String query = " Where  Client.Name like '#replace#%' OR  Client.LastName like '#replace#%' ";


            var result = query.Replace("#replace#", nomprenom.Text);

            BindGrid(result);
        }

        private void CodeClient_TextChanged(object sender, EventArgs e)
        {
            String query = " Where  Client.CodeClient like '#replace#%'  ";


            var result = query.Replace("#replace#", CodeClient.Text);

            BindGrid(result);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            String query = " Where  Client.DateOfBirth = '" + dateTimePicker1.Value + "'";
            BindGrid(query);
        }

        private void hommeRadio_CheckedChanged(object sender, EventArgs e)
        {
            String query = " Where  Client.Sexe = 'Homme' ";

 

            BindGrid(query);
        }

        private void radioFemme_CheckedChanged(object sender, EventArgs e)
        {
            String query = " Where  Client.Sexe = 'Femme' ";



            BindGrid(query);
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

            dataGridView2.RowHeadersVisible = false;
            dataGridView2.SelectAll();
            DataObject dataObj = dataGridView2.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
                string index = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "Modfier")
                {


                AddAnewClient addAnewClient = new AddAnewClient();

                EditorAttribute = true;
             iduser = index;
        addAnewClient.Show();


                }


                if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "Supprimer")
                {



                    Delete_Click(index);
                }






            }

        private void totlaNumbre_Click(object sender, EventArgs e)
        {

        }
    }
}
