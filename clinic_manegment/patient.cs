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
    public partial class patient : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Subjects\Term 2\Programming 3\clinic_manegment\clinic_manegment\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void ClearData()
        {
            PatientAdress.Clear();
            PatientAllergies.Clear();
            PatientName.Clear();
            PatientPhone.Clear();
           
            key = 0;
        }
        int key = 0;
        private void DisplayData()
        {
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from PatientTb1", con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            con.Close();
        }
        public patient()
        {
            InitializeComponent();
            DisplayData();
            Homes.CountDoctors(label4, con);
            Homes.CountPatient(label2, con);
            Homes.CountTest(label6, con);

            if(Form1.Role== "Doctor" || Form1.Role == "Admin")
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (PatientPhone.Text == "" || PatientAdress.Text == "" || PatientAllergies.Text == "" || PatientName.Text == "" )
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PatientTb1(patname,patgen,patdob,patadress,patphone,pathiv ,patall) values ('" + PatientName.Text + "','" + GendercomboBox.SelectedItem.ToString() + "' , '" + dateTimePicker1.Value + "' , '" + PatientAdress.Text + "' , '" + PatientPhone.Text + "', '" +HIVcomboBox.SelectedItem.ToString()+ "', '" + PatientAllergies.Text + "')", con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Add Successfully");
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

        private void patient_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clinicDbDataSet2.PatientTb1' table. You can move, or remove it, as needed.
            this.patientTb1TableAdapter.Fill(this.clinicDbDataSet2.PatientTb1);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select Patient");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from PatientTb1 where patid = '" + key + "' ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Deleted Successfully");
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
            if (PatientPhone.Text == "" || PatientAdress.Text == "" || PatientAllergies.Text == "" || PatientName.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update PatientTb1 set patname = '" + PatientName.Text + "', patgen = '" + GendercomboBox.SelectedItem.ToString() + "', patdob = '" + dateTimePicker1.Value + "',patadress ='" + PatientAdress.Text + "' , patphone = '" + PatientPhone.Text + "' ,pathiv = '" + HIVcomboBox.SelectedItem.ToString() + "' ,patall= '" + PatientAllergies.Text + "'   where patid = '" + key + "'", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Data Updated  Successfully");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatientName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            GendercomboBox.SelectedItem = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           dateTimePicker1.Value = (DateTime) dataGridView1.CurrentRow.Cells[3].Value;
            PatientAdress.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            PatientPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
           HIVcomboBox.SelectedItem = dataGridView1.CurrentRow.Cells[6].Value.ToString();
           PatientAllergies.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
           




            if (PatientName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
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

        private void label13_Click(object sender, EventArgs e)
        {
            reseptionists obj = new reseptionists();
            obj.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            prescriptions obj = new prescriptions();
            obj.Show();
            this.Hide();
        }
    }
}
