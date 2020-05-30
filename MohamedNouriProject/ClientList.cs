using System;
using System.Windows.Forms;
using System.Data;

using System.Data.SqlClient;
namespace MohamedNouriProject
{
    public partial class ClientList : UserControl
    {
        public ClientList()
        {
            InitializeComponent();
            Console.WriteLine("Hello there");
            //   dataGridView1.Update();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BindGrid();
            DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
            bcol.HeaderText = "Button Column ";
            bcol.Text = "Deleye";
            bcol.Name = "btnClickMe";
            bcol.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(bcol);


            DataGridViewButtonColumn Ebcol = new DataGridViewButtonColumn();
            Ebcol.HeaderText = "Button EDIT ";
            Ebcol.Text = "Click Me";
            Ebcol.Name = "EDIT";
            Ebcol.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(Ebcol);
        }
     
        private void BindGrid()
        {
              String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
            using (SqlConnection con = new SqlConnection(connetionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Client", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Set AutoGenerateColumns False
                            dataGridView2.AutoGenerateColumns = false;

                            //Set Columns Count
                            dataGridView2.ColumnCount = 4;

                            //Add Columns

                            dataGridView2.Columns[0].HeaderText = "CodeClient";
                            dataGridView2.Columns[0].Name = "CodeClient";
                            dataGridView2.Columns[0].DataPropertyName = "CodeClient";


                            dataGridView2.Columns[1].Name = "LastName";
                            dataGridView2.Columns[1].HeaderText = "Customer Id";
                            dataGridView2.Columns[1].DataPropertyName = "LastName";



                            dataGridView2.Columns[2].Name = "Email";
                            dataGridView2.Columns[2].HeaderText = "Email";
                            dataGridView2.Columns[2].DataPropertyName = "Email";


                            dataGridView2.Columns[3].Name = "TelNumber";
                            dataGridView2.Columns[3].HeaderText = "TelNumber";
                            dataGridView2.Columns[3].DataPropertyName = "TelNumber";







                            dataGridView2.DataSource = dt;
                        }
                    }
                }
            }
        }



        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String ID  = dataGridView1.Rows[e.RowIndex].Cells["CodeClient"].Value.ToString();
            String nom = dataGridView1.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
         
            if (e.ColumnIndex == 5)
            {

                DeleteCleint(ID ,nom);
                //StateMents you Want to execute to Get Data 
            }

        }





        private void DeleteCleint( String ID,String name)
        {
            
                DialogResult dr = MessageBox.Show(name, "Want something else?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    //
                }
                else if (dr == DialogResult.Cancel)
                {
                    //
                }
       


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
