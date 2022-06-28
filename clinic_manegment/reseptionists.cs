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
    public partial class reseptionists : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Subjects\Term 2\Programming 3\clinic_manegment\clinic_manegment\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");

        public reseptionists()
        {
            InitializeComponent();
            DisplayData();
            Homes.CountDoctors(label4, con);
            Homes.CountPatient(label2, con);
            Homes.CountTest(label6, con);

            if(Form1.Role== "Admin" || Form1.Role == "Receptionist")
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (key == 0)
            {
                MessageBox.Show("Please Select Customer");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ReceptionistTb1 where respid = '" + key + "' ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Deleted Successfully");
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

        private void DisplayData()
        {
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from ReceptionistTb1", con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            ReceptionstDGV.DataSource = dataSet.Tables[0];
            con.Close();
        }
        private void ClearData()
        {
            RNameTb.Clear();
            RPassword.Clear();
            RPhoneTb.Clear();
            RAdressTb.Clear();
            key = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (RNameTb.Text == "" || RPassword.Text == "" || RPhoneTb.Text == "" || RAdressTb.Text=="")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ReceptionistTb1(respname,respphone,respadd,resppass) values ('"+RNameTb.Text+ "','" + RPhoneTb.Text + "' , '" + RAdressTb.Text + "' , '" + RPassword.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Add Successfully");
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

        private void reseptionists_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clinicDbDataSet.ReceptionistTb1' table. You can move, or remove it, as needed.
            this.receptionistTb1TableAdapter.Fill(this.clinicDbDataSet.ReceptionistTb1);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int key = 0;
        private void ReceptionstDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RNameTb.Text = ReceptionstDGV.CurrentRow.Cells[1].Value.ToString();
            RPhoneTb.Text = ReceptionstDGV.CurrentRow.Cells[2].Value.ToString();
            RAdressTb.Text = ReceptionstDGV.CurrentRow.Cells[3].Value.ToString();
            RPassword.Text = ReceptionstDGV.CurrentRow.Cells[4].Value.ToString();

            if (RNameTb.Text=="")
            {
                key = 0;
            }
            else
            {
                key = int.Parse(ReceptionstDGV.CurrentRow.Cells[0].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RNameTb.Text == "" || RPassword.Text == "" || RPhoneTb.Text == "" || RAdressTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update ReceptionistTb1 set respname = '" + RNameTb.Text + "',respphone = '" + RPhoneTb.Text + "',respadd = '" + RAdressTb.Text + "',resppass ='" + RPassword.Text + "' where respid = '"+key+"'", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Updated  Successfully");
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

        private void label14_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            patient obj = new patient();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            doctors obj = new doctors();
            obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            labtests obj = new labtests();
            obj.Show();
            this.Hide();
        }
    }
}
