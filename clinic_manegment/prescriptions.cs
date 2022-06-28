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


    public partial class prescriptions : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Subjects\Term 2\Programming 3\clinic_manegment\clinic_manegment\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void ClearData()
        {
            DoctorName.Clear();
            TestName.Clear();
            PatientName.Clear();
            Medicines.Clear();
            Cost.Clear();
            DocIDCom.SelectedIndex = 0;
            PatientIdCom.SelectedIndex = 0;
            TestIDCom.SelectedIndex = 0;
            

            key = 0;
        }
        private void GetDocID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select docid from DoctorTb1", con);
            SqlDataReader sqlDataReader;
            sqlDataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("docid", typeof(int));
            dataTable.Load(sqlDataReader);
            DocIDCom.ValueMember = "docid";
            DocIDCom.DataSource = dataTable;


            con.Close();
        }
        private void GetPatientcID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select patid from PatientTb1", con);
            SqlDataReader sqlDataReader;
            sqlDataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("patid", typeof(int));
            dataTable.Load(sqlDataReader);
            PatientIdCom.ValueMember = "patid";
            PatientIdCom.DataSource = dataTable;


            con.Close();
        }
        private void GetTestID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select testid from TestTb1", con);
            SqlDataReader sqlDataReader;
            sqlDataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("testid", typeof(int));
            dataTable.Load(sqlDataReader);
            TestIDCom.ValueMember = "testid";
            TestIDCom.DataSource = dataTable;


            con.Close();
        }

        private void GetDoctorName()
        {
            con.Open();
            string quary = "select * from DoctorTb1 where docid='" + DocIDCom.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(quary, con);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                DoctorName.Text = row["docname"].ToString();
            }
            con.Close();
        }

        private void GetPatientName()
        {
            con.Open();
            string quary = "select * from PatientTb1 where patid='" + PatientIdCom.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(quary, con);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                PatientName.Text = row["patname"].ToString();
            }
            con.Close();
        }
        private void GetTestName()
        {
            con.Open();
            string quary = "select * from TestTb1 where testid='" + TestIDCom.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(quary, con);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                TestName.Text = row["testname"].ToString();
                Cost.Text = row["testcost"].ToString();
            }
            con.Close();
        }
        int key = 0;
        private void DisplayData()
        {
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from PresciptionTb1", con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            con.Close();
        }
        public prescriptions()
        {
            InitializeComponent();
            DisplayData();
            GetDocID();
            GetPatientcID();
            GetTestID();
            Homes.CountDoctors(label4, con);
            Homes.CountPatient(label2, con);
            Homes.CountTest(label6, con);
            if (Form1.Role == "Doctor" || Form1.Role == "Admin")
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void prescriptions_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clinicDbDataSet5.PresciptionTb1' table. You can move, or remove it, as needed.
            this.presciptionTb1TableAdapter.Fill(this.clinicDbDataSet5.PresciptionTb1);
            // TODO: This line of code loads data into the 'clinicDbDataSet4.ReceptionistTb1' table. You can move, or remove it, as needed.
            this.receptionistTb1TableAdapter.Fill(this.clinicDbDataSet4.ReceptionistTb1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DoctorName.Text == "" || TestName.Text == "" || Medicines.Text == "" || PatientName.Text == ""||Cost.Text=="")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PresciptionTb1(docname,patname,labtestname,medicines,cost , docid , patid , labtestid) values ('" + DoctorName.Text + "','" + PatientName.Text + "' , '" + TestName.Text + "' , '" + Medicines.Text + "' , '" + Cost.Text + "' , '"+DocIDCom.SelectedValue.ToString()+ "', '" + PatientIdCom.SelectedValue.ToString() + "', '" + TestIDCom.SelectedValue.ToString() + "')", con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Presciption Add Successfully");
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
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox1.Text = "                     I Clinic \n\n"+ "                 **PRESCRIPTION**                     " + "\n\n\n           ***************************           \n\n\n   "+ DateTime.Today.Date + "\n\n\n\n* Doctor :  "+dataGridView1.CurrentRow.Cells[2].Value.ToString()+"   * Patient :  "+ dataGridView1.CurrentRow.Cells[4].Value.ToString() + "\n\n* Test : " + dataGridView1.CurrentRow.Cells[6].Value.ToString() + "   * Medicine : " + dataGridView1.CurrentRow.Cells[7].Value.ToString() +"\n\n\n             I Clinic (Health Center)" + "\n\n         We wish you a speedy recovery";

           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DocIDCom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDoctorName();
        }

        private void PatientIdCom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetPatientName();
        }

        private void TestIDCom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTestName();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text + "\n" , new Font("Cascadia Code" , 15 , FontStyle.Italic) , Brushes.Black , new Point(95,80));
    
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
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
    }
}
