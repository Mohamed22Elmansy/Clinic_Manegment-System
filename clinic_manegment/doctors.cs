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
    public partial class doctors : Form
    {
        public doctors()
        {
            InitializeComponent();
            DisplayData();
            Homes.CountDoctors(label4,con);
            Homes.CountPatient(label2 , con);
            Homes.CountTest(label6,con);
            if(Form1.Role== "Admin"|| Form1.Role== "Doctor")
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
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Subjects\Term 2\Programming 3\clinic_manegment\clinic_manegment\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ClearData()
        {
            DAdress.Clear();
            DName.Clear();
            DPassword.Clear();
            DPhone.Clear();
            DExperiance.Clear();
            key = 0;
        }
        int key = 0;
        private void DisplayData()
        {
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from DoctorTb1", con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            con.Close();
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
                    SqlCommand cmd = new SqlCommand("delete from DoctorTb1 where docid = '" + key + "' ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Deleted Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();
                    Homes.CountDoctors(label4, con);
                    Homes.CountPatient(label2, con);
                    Homes.CountTest(label6, con);

                }
                catch (Exception Ex)
                {
                    con.Close();
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DName.Text == "" || DPassword.Text == "" || DPhone.Text == "" || DExperiance.Text == "" || DAdress.Text=="")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DoctorTb1(docname,docdob,docgen,docspec,docexp,docphone ,docadd,docpass) values ('" + DName.Text + "','" + dateTimePicker1.Value + "' , '" + genderCom.SelectedItem.ToString() + "' , '" + SpecializationCom.SelectedItem.ToString() + "' , '" + int.Parse(DExperiance.Text) + "', '" + DPhone.Text + "', '" + DAdress.Text + "','" + DPassword.Text + "')", con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Add Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();
                    Homes.CountDoctors(label4, con);
                    Homes.CountPatient(label2, con);
                    Homes.CountTest(label6, con);

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
            if (DName.Text == "" || DPassword.Text == "" || DPhone.Text == "" || DExperiance.Text == "" || DAdress.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update DoctorTb1 set docname = '" + DName.Text + "',docdob = '" + dateTimePicker1.Value + "',docgen = '" + genderCom.SelectedItem.ToString() + "',docspec ='" + SpecializationCom.SelectedItem.ToString() + "' , docexp = '" + int.Parse(DExperiance.Text) + "' ,docphone = '" + DPhone.Text + "' ,docadd= '" + DAdress.Text + "'  ,docpass = '" + DPassword.Text + "' where docid = '" + key + "'", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Data Updated  Successfully");
                    con.Close();
                    DisplayData();
                    ClearData();
                    Homes.CountDoctors(label4, con);
                    Homes.CountPatient(label2, con);
                    Homes.CountTest(label6, con);

                }
                catch (Exception Ex)
                {
                    con.Close();
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void doctors_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clinicDbDataSet1.DoctorTb1' table. You can move, or remove it, as needed.
            this.doctorTb1TableAdapter.Fill(this.clinicDbDataSet1.DoctorTb1);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[2].Value;
            genderCom.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            SpecializationCom.SelectedItem = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            DExperiance.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            DPhone.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            DAdress.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            DPassword.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
           

            if (DName.Text == "")
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

        private void label11_Click(object sender, EventArgs e)
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

        private void label12_Click(object sender, EventArgs e)
        {
            labtests obj = new labtests();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            reseptionists obj = new reseptionists();
            obj.Show();
            this.Hide();
        }
    }
}
