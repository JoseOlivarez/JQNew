/*************************************************************Written By Noe Olivarez an Employee of Piping Technology and Products in Feb 2019 *********************************************************************************************/
// **********************************************************Please comment any changes******************************************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Join;
using System.Linq;
using System.Text;
//using System.Linq.Cast-Method;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        // public object jobnum { get; private set; }
        //private object jobnum;

        public Form2(string jobnum)
        {
            string jobnum2 = jobnum;
            InitializeComponent();
            textBox1.Text = jobnum2;
            GrabData(); //function to grab the customer ID and billing contact ID 
                        // Customer.Text = textBox1.Text;

            // public class name {}

            //  textBox1.Text = jobnum;
        }
        public void GrabData()
        {
            /******************************************************Customer job is the grab data function that grabs information from the job master database located in jq**********************************************************/

            SqlDataReader reader = null;

            SqlConnection conn = NewCopy();
            conn.Open();
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Job";
            param.Value = Global.JobNumber;

            SqlCommand cmd = new SqlCommand("SELECT [Customer ID] , [Billing Contact ID]  FROM[jq].[dbo].[Customer Job] WHERE [Job #]=@Job", conn);
            cmd.Parameters.Add(param);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CustomerText.Text = reader["Customer ID"].ToString();
                textBox2.Text = reader["Billing Contact ID"].ToString();
                //textBox4.Text = reader[""].ToString();
                Global.Variable2 = CustomerText.Text;
                Global.Buyer = textBox2.Text;
            }


            conn.Close();
            /******************************************************Connection Master is the grab data function that grabs information from the job master database located in jq**********************************************************/

            SqlDataReader readCustName = null;


            SqlConnection connectionstring = NewCopy();
            connectionstring.Open();
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Cust";
            parameter.Value = Global.Variable2;
            SqlCommand command = new SqlCommand("SELECT [Customer Name] ,  [Phone Number], [Email], [Ship To State]  FROM [jq].[dbo].[Customer Master] WHERE [Customer ID]=@Cust", connectionstring);
            command.Parameters.Add(parameter);
            readCustName = command.ExecuteReader();
            while (readCustName.Read())
            {
                CustomerN.Text = readCustName["Customer Name"].ToString();
                //phoneae.Text = readCustName["Phone Number"].ToString();
                textBox6.Text = readCustName["Ship To State"].ToString();

                //textBox4.Text = reader[""].ToString();
                Global.Variable3 = CustomerN.Text;
            }

            connectionstring.Close();

            SqlDataReader readJobMaster = null;
            /******************************************************Job Master is the grab data function that grabs information from the job master database located in jq**********************************************************/

            SqlConnection connectionMaster = NewCopy();
            connectionMaster.Open();
            SqlParameter parameterM = new SqlParameter();
            parameterM.ParameterName = "@Job";
            parameterM.Value = Global.JobNumber;
            SqlCommand commandM = new SqlCommand("SELECT [PO #],[Req #],[Job],[Payment Terms],[Freight Terms],[Account],[FOB],[Delivery Terms],[Price Validity],[VIA],[Agent ID],[Project Engineer ID],[Takeoff Engineer ID] ,[Detailer ID],[Quote #],[ChangelogDate] ,[ChangelogType],[ChangelogUser]  FROM [jq].[dbo].[Changes_Job Master] WHERE [Job #]=@Job", connectionMaster);
            commandM.Parameters.Add(parameterM);
            readJobMaster = commandM.ExecuteReader();
            while (readJobMaster.Read())
            {
                PO.Text = readJobMaster["PO #"].ToString();
                //PO.Text = readJobMaster["PO"].ToString();
                ReqN.Text = readJobMaster["Req #"].ToString();
                PaymentT.Text = readJobMaster["Payment Terms"].ToString();
                FreightT.Text = readJobMaster["Freight Terms"].ToString();
                AccountN.Text = readJobMaster["Account"].ToString();
                FOB.Text = readJobMaster["FOB"].ToString();
                DeliveryT.Text = readJobMaster["Delivery Terms"].ToString();
                PriceV.Text = readJobMaster["Price Validity"].ToString();
                VIA.Text = readJobMaster["VIA"].ToString();
                ProjectEng.Text = readJobMaster["Project Engineer ID"].ToString();
                TakeoffEng.Text = readJobMaster["Takeoff Engineer ID"].ToString();
                Detail.Text = readJobMaster["Detailer ID"].ToString();
                QuoteN.Text = readJobMaster["Quote #"].ToString();
                //              PO.Text = readJobMaster["PO"].ToString();
                //            ReqN.Text = readJobMaster["Req #"].ToString();

                //string TakeOff = readJobMaster["Takeoff Engineer"].ToString();
                //          TakeoffEng.Items.Add(TakeOff);

                //textBox4.Text = reader[""].ToString(); 

            }
            connectionMaster.Close();
            /*********************************People master function access the new people master which is located in the jq database****************************************************/
            SqlDataReader readNPM = null;


            SqlConnection connectionPMaster = NewCopy();
            connectionPMaster.Open();
            SqlParameter parameterPM = new SqlParameter();
            parameterPM.ParameterName = "@ContactID";
            parameterPM.Value = Global.Buyer;
            SqlCommand commandPM = new SqlCommand("SELECT [Last Name],[First Name],[Phone1],[Phone2] ,[Phone3],[Fax1],[Fax2] ,[Fax3],[Email]  ,[Web Address],[CustomerID],[Address1] ,[Address2], [City],[State],[Zip code],[Country],[Valid] FROM[jq].[dbo].[New People Master] WHERE [Contact ID] = @ContactID", connectionPMaster);
            commandPM.Parameters.Add(parameterPM);
            readNPM = commandPM.ExecuteReader();
            while (readNPM.Read())
            {
                // phoneae.Text = readNPM["Phone1"].ToString() + "," + readNPM["Email"].ToString();

            }
            connectionPMaster.Close();













        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Customer_TextChanged(object sender, EventArgs e)
        { // obtaining information for the customer text field  
          //  string john = Form1.JQSearch1.Text();

            // string name =  Form1.jobnumber.Text();
            /// string ron = Form2.jobnum;
            // save the customer id and then clear the data table so we can find customer

        }
        private static SqlConnection NewCopy()
        {
            return new SqlConnection("Data Source=sql;Initial Catalog=jq;Integrated Security=True");
        }
        private static SqlConnection NewUq()
        {
            return new SqlConnection("Data Source=sql;Initial Catalog=JQNew;Integrated Security=True");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void PaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void asdf_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}