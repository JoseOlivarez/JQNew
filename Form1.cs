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
        private object textBox1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = NewMethod();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT [Job #] FROM[jq].[dbo].[Job Instruction]" + jobnumber.Text + " ", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            jobnumber.Text = dt.Rows[0][0].ToString();
           
        }
        private static SqlConnection NewMethod()
        {
            return new SqlConnection("Data Source=sql;Initial Catalog=jq;Integrated Security=True");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

       
    }
}
