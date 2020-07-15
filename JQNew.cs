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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public object textBox1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void button1_Click(object sender, EventArgs e)
        {
            //   int buttonpressed = 0;
            Global.JobNumber = JQSearch.Text;
            //SqlConnection conn = NewCopy();
            //SqlDataAdapter sda = new SqlDataAdapter("SELECT [Job #] FROM[jq].[dbo].[Job Instruction] WHERE [Job #] ='" + JQSearch.Text + "'", conn);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //jobnumber.Text = dt.Rows[0][0].ToString();
            //dt.Rows.Clear();
            //public static string = jobNumber.Text();
            

            Form2 Form3 = new Form2(JQSearch.Text);
            Form3.Show();
            //public static int quantity;
            //public static string jobnumber = jobnumber.Text;
            //  jobnumber.Text = dt.Rows[1][1].ToString();
            /*
            SqlConnection conncust = NewUq();
            SqlDataAdapter sda2 = new SqlDataAdapter();
            // SqlConnection conncids = NewCopy();

            using (SqlConnection con = NewUq())
            {
                con.Open();
                String query = "INSERT  INTO  JQNew.dbo.access(jobnum) VALUES(@jobnumber)";
                using (SqlCommand command = new SqlCommand(query, conncust))
                {

                    command.Parameters.AddWithValue("@jobnumber", jobnumber.Text);


                    int result = command.ExecuteNonQuery();

                   //// string customer = (string)dt.Rows[0][0];
                   //  Customer.Text = customer;
                    con.Close();
                }
    */
        }
    
        
        private static SqlConnection NewCopy()
        {
            return new SqlConnection("Data Source=sql;Initial Catalog=jq;Integrated Security=True");
        }
        private static SqlConnection NewUq()
        {
            return new SqlConnection("Data Source=sql;Initial Catalog=JQNew;Integrated Security=True");
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

       public  void jobnumber_TextChanged(object sender, EventArgs e)
        {
        }


        

        public void JQSearch1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
