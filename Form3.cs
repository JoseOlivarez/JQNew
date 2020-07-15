using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections;


namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            textBox1.Text = Global.MaxPart.ToString();
            textBox2.Text = Global.JobNumber.ToString(); 

        }
        private void check ()
        {
        
    }


        private void textBox7_TextChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); con.Open();
            string[] one = new string[20];
            string[] label = new string[20];
            Boolean[] check = new bool[20];


            if (checkBox1.Checked == true)
            {
                one[0] = textBox1.Text;
                label[0] = "2B Finish";
                check[0] = true; 
                          
            }
            else if (checkBox2.Checked == true)
            {
                one[1] = textBox2.Text;
                label[1] = "Bar Thread";
                check[1] = true;

            }
            else if (checkBox3.Checked == true)
            {
                one[2] = textBox3.Text;
                label[2] = "Bend"; check[2] = true;

            }
            else if (checkBox4.Checked == true)
            {
                one[3] = textBox4.Text;
                label[3] = "Bevel";
                check[3] = true;

            }
            else if (checkBox5.Checked == true)
            {
                one[4] = textBox5.Text;
                label[4] = "Bonding/Bolting";
                check[4] = true;

            }
            else if (checkBox6.Checked == true)
            {
                one[5] = textBox6.Text;
                label[5] = "Chamfer";
                check[5] = true;

            }
            else if (checkBox7.Checked == true)
            {
                one[6] = textBox7.Text;
                label[6] = "Clip Corners";
                check[6] = true;


            }
            else if (checkBox8.Checked == true)
            {
                one[7] = textBox8.Text;
                label[7] = "Constant";
                check[7] = true;

            }
            else if (checkBox9.Checked == true)
            {
                one[8] = textBox9.Text;
                label[8] = "Cope";
                check[8] = true;

            }
            else if (checkBox10.Checked == true)
            {
                one[9] = textBox10.Text;
                label[9] = "Cotter Pin";
                check[9] = true;

            }
            else if (checkBox11.Checked == true)
            {
                one[10] = textBox11.Text;
                label[10] = "Cut Diagonal ";
                check[10] = true;
                
            }
            string[] empty = new string[50]; int j = 0; 
       

            for (int a = 0; a < 10; a++)
            {
                string StrQuery = "INSERT INTO SecondaryOperations(Selection, description, ItemNumber, JobNumber) VALUES('" + label[a] + "','" + one[a] + "','"  + Global.ItemNumber + "','" + Global.JobNumber + "')";
                //rownumber++; columnnumber++;
                using (SqlCommand getComm = new SqlCommand(StrQuery))
                {
                    // inserting in the job number the colum and the mark number 
                    getComm.Connection = con;
                    getComm.CommandText = StrQuery;
                    getComm.ExecuteNonQuery();
                }
            }
            con.Close(); 
        }
    }
}
