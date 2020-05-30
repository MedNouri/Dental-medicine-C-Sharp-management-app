using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace MohamedNouriProject
{
    public partial class History : UserControl
    {
        public History()
        {
            InitializeComponent();

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
       
            BindGrid("");

            DataGridViewButtonColumn Deletelink = new DataGridViewButtonColumn();
        
            Deletelink.HeaderText = "Supprimer";
            Deletelink.DataPropertyName = "lnkColumn";
           
           Deletelink.Text = "supprimer";
          


  
            Deletelink.UseColumnTextForButtonValue = true;

            dataGridView2.Columns.Add(Deletelink);



        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string index = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
             
            if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "Supprimer")
            {



                Delete_Click(index);
            }






        }
        private void Delete_Click(string ID)
        {
            DialogResult dr = MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + ID, "supprimer ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);



            if (dr == DialogResult.Yes)
            {
                //

                String query = "DELETE FROM Interventions WHERE CodeInt =" + ID;
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

        private String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";

        

        private void BindGrid(String query)
        {

            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Interventions.CodeInt,Interventions.Date,  Client.CodeClient,Client.Name,Client.LastName FROM Interventions  INNER JOIN Client ON  Interventions.CodeClient = Client.CodeClient " + query + "ORDER BY Interventions.Date", con))
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

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (stardatev.Value == DateTimePicker.MinimumDateTime)
            {
                stardatev.Value = DateTime.Now; // This is required in order to show current month/year when user reopens the date popup.
                stardatev.Format = DateTimePickerFormat.Custom;
                stardatev.CustomFormat = " ";
            }
            else
            {
                stardatev.Format = DateTimePickerFormat.Short;
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            stardatev.Value = DateTimePicker.MinimumDateTime;
        }

        private void CodeClient_TextChanged(object sender, EventArgs e)
        {
            String query = " Where  Interventions.CodeInt like '#replace#%' ";


            var result = query.Replace("#replace#", CodeRDV.Text);

            BindGrid(result);
        }

        private void NomClient_TextChanged(object sender, EventArgs e)
        {
            String query = " Where  Client.Name like '#replace#%' OR  Client.LastName like '#replace#%' ";


            var result = query.Replace("#replace#", NomClient.Text);

            BindGrid(result);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            String query = " where Interventions.Date between '" + stardatev.Value  + "'AND'" + EndDate.Value  + "'";
            BindGrid(query);
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            String query = " where Date between '" + stardatev.Value + "' AND'" + EndDate.Value + "'";
            BindGrid(query);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindGrid("");
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
 
    }
}
