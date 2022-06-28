using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace clinic_manegment
{
    public partial class labtests : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Subjects\Term 2\Programming 3\clinic_manegment\clinic_manegment\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void ClearData()
        {
            TestName.Clear();
            cost.Clear();
           
            key = 0;
        }
        int key = 0;
        private void DisplayData()
        {
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from TestTb1", con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            con.Close();
        }
        public labtests()
        {
            InitializeComponent();
            DisplayData();
            if (Form1.Role == "Doctor" || Form1.Role == "Admin")
            {
                label19.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else 
            {
                button3.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                
            }
           
        }

        private void labtests_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clinicDbDataSet3.TestTb1' table. You can move, or remove it, as needed.
            this.testTb1TableAdapter.Fill(this.clinicDbDataSet3.TestTb1);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TestName.Text == "" || cost.Text == "" )
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TestTb1(testname,testcost) values ('" + TestName.Text + "','" + int.Parse(cost.Text) + "' )", con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Add Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();

                }
                catch (Exception Ex)
                {
                    con.Close();
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TestName.Text == "" || cost.Text == "" )
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update TestTb1 set testname = '" + TestName.Text + "', testcost = '" + int.Parse(cost.Text) + "'  where testid = '" + key + "'", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Data Updated  Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();

                }
                catch (Exception Ex)
                {
                    con.Close();
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select Test");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from TestTb1 where testid = '" + key + "' ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test Deleted Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();

                }
                catch (Exception Ex)
                {
                    con.Close();

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TestName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cost.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            
            





            if (TestName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            prescriptions obj = new prescriptions();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }
    }
}
