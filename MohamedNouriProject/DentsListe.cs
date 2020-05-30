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

namespace MohamedNouriProject
{
    public partial class DentsListe : UserControl
    {

        String connetionString = @"Data Source=DESKTOP-PITV65G;Initial Catalog=dentaldoctor;integrated security=sspi";
        public DentsListe()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BindGrid("SELECT * FROM Dent ");

            DataGridViewButtonColumn Deletelink = new DataGridViewButtonColumn();
            Deletelink.HeaderText = "Supprimer";
            Deletelink.DataPropertyName = "lnkColumn";

            Deletelink.Text = "supprimer";




            Deletelink.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(Deletelink);
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




                            totlaNumbre.Text = dt.Rows.Count.ToString();


                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
        }

    
        private void Delete_Click(string ID)
        {
            DialogResult dr = MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + ID, "supprimer ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);



            if (dr == DialogResult.Yes)
            {
                //

                String query = "DELETE FROM Dent WHERE CodeDent =" + ID;
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string index = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Supprimer")
            {



                Delete_Click(index);
            }


        }

        private void comments_TextChanged(object sender, EventArgs e)
        {
             
            String query = " SELECT * FROM Dent  Where   description like '#replace#%' ";


            var result = query.Replace("#replace#", description.Text);

            BindGrid(result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindGrid("SELECT * FROM Dent ");
        }
    }
}
