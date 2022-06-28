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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Subjects\Term 2\Programming 3\clinic_manegment\clinic_manegment\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string Role;  

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            UserName.Clear();
            Password.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Your Position ");
            }
            else if (UserName.Text==""||Password.Text=="")
            {
                MessageBox.Show("Please Fill All Fields");
            }
            else if (comboBox1.SelectedIndex == 0)
            {
                if(UserName.Text=="Admin" && Password.Text == "Password")
                {
                    Role = "Admin";
                    Homes obj = new Homes();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User Name Or Password Incorrect");
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                con.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select count(*) from DoctorTb1 where docname='" + UserName.Text + "' and docpass='" + Password.Text + "'", con);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows[0][0].ToString() == "1")
                {
                    Role = "Doctor";
                    prescriptions prescriptions = new prescriptions();
                    prescriptions.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Doctor Not Found");
                }
                con.Close();
            }
            else if(comboBox1.SelectedIndex == 2)
            {
                con.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select count(*) from ReceptionistTb1 where respname='" + UserName.Text + "' and resppass='" + Password.Text + "'", con);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows[0][0].ToString() == "1")
                {
                    Role = "Receptionist";
                    Homes obj = new Homes();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    
                    MessageBox.Show("Receptionist Not Found");
                }
                con.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
