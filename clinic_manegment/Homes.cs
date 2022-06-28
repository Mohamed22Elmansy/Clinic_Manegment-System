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
    public partial class Homes : Form
    {
        public Homes()
        {
            InitializeComponent();
            CountPatient(patname , con);
            CountDoctors(label4 , con);
            CountTest(label6 , con);
            CountHIV();
           
        }

        private void CountHIV()
        {
            con.Open();
            string stats = "Positive";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select count(*) from PatientTb1 where pathiv = '"+stats+"'", con);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            label8.Text = dataTable.Rows[0][0].ToString() + " Patient";
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Subjects\Term 2\Programming 3\clinic_manegment\clinic_manegment\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static void CountPatient(Label obj , SqlConnection connection)
        {
            connection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select count(*) from PatientTb1", connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            obj.Text = dataTable.Rows[0][0].ToString() +" Patient";
            connection.Close();
        }
        public static void CountDoctors(Label obj, SqlConnection connection)
        {
            connection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select count(*) from DoctorTb1", connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            obj.Text = dataTable.Rows[0][0].ToString() + " Doctors";
            connection.Close();
        }
        public static void CountTest(Label obj, SqlConnection connection)
        {
            connection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select count(*) from TestTb1", connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            obj.Text = dataTable.Rows[0][0].ToString() + "  Tests";
            connection.Close();
        }

        private void Homes_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            doctors obj = new doctors();
            obj.Show();
            this.Hide();
        }

        private void Patientlb_Click(object sender, EventArgs e)
        {
            patient obj = new patient();
            obj.Show();
            this.Hide();
        }

        private void Lablb_Click(object sender, EventArgs e)
        {
            labtests obj = new labtests();
            obj.Show();
            this.Hide();
        }

        private void Receplab_Click(object sender, EventArgs e)
        {
            reseptionists obj = new reseptionists();
            obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }
    }
}
