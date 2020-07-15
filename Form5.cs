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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        int selectedRow;

        public static bool deleted;

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jqDataSet1.Items' table. You can move, or remove it, as needed.
            // this.itemsTableAdapter.Fill(this.jqDataSet1.Items);
            // TODO: This line of code loads data into the 'jqDataSet.BlanketTakeoff' table. You can move, or remove it, as needed.
            //  this.blanketTakeoffTableAdapter.Fill(this.jqDataSet.BlanketTakeoff);

            //This form load will act as a source for inserting and deleting data grabbed by the database.

            this.dataGridView5.AutoGenerateColumns = false;

            SqlConnection conn = new SqlConnection();
            SqlDataReader readInformation = null;

            conn.ConnectionString = @"Data Source=SQL;Initial Catalog=jq;Integrated Security=True";


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Job";
            parameter.Value = Global.JobNumber;
            SqlCommand command = new SqlCommand("SELECT [Item #] ,[Item Quantity],[PT&P Tag #],[Client Tag #],[Item Description], [Upper Elevation] ,[Lower Elevation], [Cancelled],[CustomerRelease],[CustomerItemNumber] FROM[jq].[dbo].[Items] WHERE [Job #] =@Job ORDER BY [Item #] ASC", conn);

            command.Parameters.Add(parameter);

            command.Connection = conn;
            dataGridView5.AutoGenerateColumns = false;

            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);

            dataGridView1.DataSource = data;
            Part();
            textBox1.Text = Global.JobNumber;
            readInformation = command.ExecuteReader();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            // ReadT();
            conn.Close();


            //textBox2.Text = Global.MarkNumber;
            //  insert();




            // Jobnum.Text = Global.MarkNumber;

            /****************************/
            //SqlConnection connectionView = NewCopy(); connectionView.Open(); 

            // SqlParameter viewBot = new SqlParameter();
            //viewBot.ParameterName = "@Filler";
            //viewBot.Value = Global.JobNumber;

            // viewBot.ParameterName = "@View";

            // viewBot.ParameterName = "@"   
            //  SqlCommand viewBottom            

            // conn.Open();


            // conn.Close();
        }
        private void ReadT()
        {
            //  dataGridView5.ReadOnly = false;
            ///dataGridView5.Rows.Add();
            //dataGridView5.Rows[0].Cells[0].Value = Global.PartNumber;
            //  dataGridView5[]

            SqlDataReader read = null;
            SqlConnection con = new SqlConnection(@"Data Source=SQL;Initial Catalog=jq;Integrated Security=True"); con.Open();

            //   dataGridView5[1, 1].Value = Global.ItemNumber.ToString();
            // SqlParameter param = new SqlParameter();
            SqlCommand grabPaS = new SqlCommand("SELECT [Secondary Operation], [PrimaryOp] FROM [jq].[dbo].[PartsChanges] WHERE [Job #]=@JobNum AND [Item #] = @ItemNum AND [Part #] = @PartNum", con);
            SqlParameter parameters = new SqlParameter();
            //  MessageBox.Show(Global.JobNumber + "alright" + Global.ItemNumber + ", " + Global.PartNumber);
            grabPaS.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
            grabPaS.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
            grabPaS.Parameters.Add(new SqlParameter("@PartNum", Global.MaxPart));

            grabPaS.Parameters.Add(parameters);

            parameters.Value = Global.JobNumber;
            grabPaS.Connection = con;

            DataTable dataCat = new DataTable();
            SqlDataAdapter adapterCat = new SqlDataAdapter(grabPaS);
            adapterCat.Fill(dataCat);
            //  string okay = "okay";
            dataGridView5.DataSource = dataCat;
        }
        private void AddARow(DataTable table)
        {

            DataRow newRow = table.NewRow();


            table.Rows.Add(newRow);
        }
        public void Add()
        {
            SqlConnection con = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;Integrated Security=True"); con.Open();
            // SqlDataReader read = null;
            SqlCommand commandreadPI = new SqlCommand("SELECT [jobnum],[accessy],[marknumber],[itemnumber],[partnumber], [Stage1],[Stage2],[Stage3], [primaryop], [secondaryop] FROM [JQNew].[dbo].[access] WHERE [itemnumber] = @ItemNum AND [jobnum] = @JobNum", con);
            SqlParameter parameters = new SqlParameter();
            //parameters.ParameterName = "@Item";
            // parameters.
            commandreadPI.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
            commandreadPI.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));

            commandreadPI.Parameters.Add(parameters);


            parameters.Value = Global.ItemNumber;
            //readPI.Parameters.Add(new SqlParameter("@PartNum", Global.PartNumber));

            commandreadPI.Connection = con;
            DataTable dataCat = new DataTable();
            SqlDataAdapter adapterCat = new SqlDataAdapter(commandreadPI);
            adapterCat.Fill(dataCat);
            string okay = "okay";
            dataGridView3.DataSource = dataCat;

            con.Close();

        }
        private void Check()
        {
            string jan = "false";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[5] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == false)
                {
                    MessageBox.Show("time to input  ");
                    Form3 forg = new Form3();
                    forg.Show();
                }
                else
                {
                    jan =
                         "false";
                    textBox11.Text = jan;
                }
                // else MessageBox.Show("hi");
            }
        }

        public void insertConstants()
        {
            SqlDataReader readGIT = null;
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Data Source=SQL;Initial Catalog=jq;Integrated Security=True"; conn.Open();

            SqlParameter parameterConstant = new SqlParameter();
            //    MessageBox.Show(Global.ItemNumber+ "is ITem number "+ Global.JobNumber + "is job number");

            parameterConstant.Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();

            SqlCommand commandGrabItem = new SqlCommand("SELECT  [Part #],[Part Quantity], [Total Pieces],[Figure name],[Fit-up?],[Secondary Operation],[X1],[X2],[X3],[Data1],[Data2],[Data3],[Done],[Raw Finish],[Final Finish],[List Price],[Ext Part Weight][Description]  FROM [jq].[dbo].[Parts] WHERE [Item #] = @ItemNum  AND [Job #] =@JobNum ORDER BY [Part #] ASC", conn);
            //commandGrabItem.CommandType = CommandType.StoredProcedure;
            commandGrabItem.Parameters.Add(parameterConstant);
            commandGrabItem.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
            commandGrabItem.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
            commandGrabItem.Connection = conn;
            DataTable dataCat = new DataTable();
            SqlDataAdapter adapterCat = new SqlDataAdapter(commandGrabItem);
            adapterCat.Fill(dataCat);
            readGIT = commandGrabItem.ExecuteReader();
            DataTable empty = new DataTable();
            //dataGridView4.DataSource = empty;
            //readParts = commandParts.ExecuteReader();

            while (readGIT.Read())
            {
                textBox11.Text = readGIT["Part #"].ToString();

                string part = textBox11.Text;
                Global.PartNumber = Convert.ToInt32(part);
                // dataGridView4.Rows[Global.PartNumber].Cells[1].Value = textBox11.Text;
                string john = Global.PartNumber.ToString();
                // MessageBox.Show(part + "fuck");
            }
            /*
            if (dataGridView4.Rows.Count != Global.PartNumber)
            {

                for (int i = 0; i < Global.PartNumber; i++)
                {

                    empty.Rows.Add(empty.NewRow());
                    //  MessageBox.Show("fuck");

                }


            }*/
            dataGridView2.DataSource = dataCat;
            if (!readGIT.HasRows)
            {
                MessageBox.Show("It doesn't exist in current context");
                //insertParts();
            }
            conn.Close();
        }/*
        public void insertParts()
        {
            SqlDataReader readParts = null;
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Data Source=SQL;Initial Catalog=jq;Integrated Security=True"; conn.Open();

            SqlParameter parameterParts = new SqlParameter();

      //      parameterParts.ParameterName = "@Parts";

            SqlCommand commandParts = new SqlCommand("SELECT  [Part #], [Part Quantity], [Total Pieces], [Figure name], [Final Finish], [Fit-up?], [Secondary Operation], [X1], [X2], [X3], [Data1],[Data2],[Data3] FROM [jq].[dbo].[Parts] WHERE [Item #] = @ItemNum  AND [Job #] =@JobNum ORDER BY [Part #] ASC", conn);
            commandParts.Parameters.Add(parameterParts);
            commandParts.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
            commandParts.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
            commandParts.Connection = conn;
            DataTable dataCat = new DataTable();
            SqlDataAdapter adapterCat = new SqlDataAdapter(commandParts);
            adapterCat.Fill(dataCat);
            //readParts = commandParts.ExecuteReader();
            
            // kind of lost my train of though 


        }*/
        public void insertMaster()
        {
            insertConstants();
            ReadT();
            textBox3.Text = "SubAssembly JQ V1";
            bool[] partC = new bool[50]; int rowNum = 0;
            for (int i = 0; i < Global.PartNumber; i++)
            {
                partC[i] = Convert.ToBoolean(dataGridView2.Rows[rowNum].Cells[5].Value);
                if (partC[i] == true)
                    // MessageBox.Show("fuck");
                    rowNum++;
            }


            Add();

        }

        public void Part()
        {
            SqlDataReader readPartN = null;


            SqlConnection connectionstring = NewCopy(); connectionstring.Open();
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Job";
            parameter.Value = Global.JobNumber;

            SqlCommand view = new SqlCommand("SELECT [Job #],[PT&P Tag #] FROM [jq].[dbo].[Items] WHERE [Job #]= @Job", connectionstring);
            view.Parameters.Add(parameter);
            //view.Connection = conn;
            readPartN = view.ExecuteReader();
            // DataTable data1 = new DataTable();
            //SqlDataAdapter adapter1 = new SqlDataAdapter(view);
            //adapter.Fill(data1);
            //readPartN = view.ExecuteReader();
            while (readPartN.Read())
            {
                Global.MarkNumber = readPartN["PT&P Tag #"].ToString();
                textBox2.Text = Global.MarkNumber;

            }
            //    dataGridView2.DataSource = data1;
            //   return Global.MarkNumber;
            connectionstring.Close();
        }

        private void dataGrid1_Navigate(object sender, NavigateEventArgs ne)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            selectedRow = e.RowIndex;


            DataGridViewRow row = dataGridView2.Rows[selectedRow];
            // DataGridViewCell cell = dataGridView2.Cells[1];
            textBox8.Text = row.Cells[0].Value.ToString();
            string itemnum = row.Cells[0].Value.ToString();
            Global.MaxPart = Convert.ToInt32(itemnum); MessageBox.Show("farts" + Global.MaxPart.ToString());
            // MessageBox.Show(Global.MaxPart.ToString()); 
            ReadT();
        }

        private void Inputs_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                dataGridView3.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }/*
            for (int i = 0; i < dataGridView4.Columns.Count; i++)
            {
                dataGridView4.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }*/

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private static SqlConnection NewCopy()
        {
            return new SqlConnection("Data Source=sql;Initial Catalog=jq;Integrated Security=True");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {






        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            /// d//ataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            totalItems.Text = ("Total Items:" + Global.ItemNumber.ToString());
            textBox4.Text = ("Total Parts:" + Global.PartNumber.ToString());
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[selectedRow];
            textBox8.Text = row.Cells[0].Value.ToString();
            int itemnum = Convert.ToInt32(row.Cells[0].Value);
            Global.MaxPart = itemnum;
            ReadT();
            Global.MaxPart = Convert.ToInt32(itemnum); //MessageBox.Show("farts" + Global.MaxPart.ToString());

            // Check();
            // MessageBox.Show(Global.MaxPart.ToString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            textBox8.Text = row.Cells[2].Value.ToString();
            string itemnum = row.Cells[0].Value.ToString();
            Global.ItemNumber = Convert.ToInt32(itemnum);

            Global.PTPTag = textBox8.Text;
            insertMaster();

            Add();
            //insert table

            textBox3.Text = "False";

            // dataGridView4.RowStateChanged += GridView_RowStateChanged;



        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {



        }

        //private void dataGridView4_DefaultValuesNeeded(object sender, System.Windows.Forms.DataGridViewRowEventArgs e)
        // {

        // e.Row.Cells["Item #"].Value = Global.ItemNumber;
        // }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button6_Click(object sender, EventArgs e)
        {/*

            try
            {
                int rowNumber = 0;
                int rowNumber1 = 0;
                int columnNumber = 0;
                int i = 0;
                string[] arrayC = new string[50];
                int[] partC = new int[50];
                string jobnum = Global.JobNumber;
                //arrayC[0] = dataGridView4[0, dataGridView4.CurrentCell.RowIndex].Value.ToString();
                for (i = 0; i < Global.PartNumber; i++)
                {

                    partC[i] = Convert.ToInt32(dataGridView2.Rows[rowNumber1].Cells[0].Value);

                    rowNumber1++;

                }
                for (i = 0; i < Global.PartNumber; i++)
                {

                    arrayC[i] = dataGridView4.Rows[rowNumber].Cells[0].Value.ToString();
                    if (arrayC[i] == null)
                    {
                        MessageBox.Show("Do not enter null values");
                        break;
                    }
                    ++rowNumber; ++columnNumber;

                }

                //for(int j = 0; j< g)
                string marknum = Global.MarkNumber;
                if (marknum.Length >= 20)
                    MessageBox.Show("You have replaced the assembly numbers");
                //  int part = Global.PartNumber;

                SqlConnection con = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true; Integrated Security=True");
                con.Open();
                SqlDataReader readaccess = null;
                SqlParameter param = new SqlParameter();
                SqlCommand commGrab = new SqlCommand("SELECT[jobnum],[accessy],[marknumber],[itemnumber],[partnumber] FROM[JQNew].[dbo].[access] WHERE [itemnumber] = @ItemNum AND [jobnum] =@JobNum", con);
                param.Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                commGrab.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
                commGrab.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
                commGrab.Parameters.Add(param);
                commGrab.Connection = con;
                //  Add();
                readaccess = commGrab.ExecuteReader();

                while (readaccess.Read())
                {
                    Global.AccessNumber = readaccess["accessy"].ToString();
                    //MessageBox.Show("You are replacing" + Global.AccessNumber);
                    if (Global.AccessNumber != null)
                    {
                        //S/qlCommand command = new SqlCommand();
                        //command.CommandText = "UPDATE accessy SET [itemnumber] = ";
                        SqlParameter parameter = new SqlParameter();
                        // MessageBox.Show("NOOOOOOOOOO");
                        using (SqlCommand command = new SqlCommand("DELETE FROM access WHERE  [itemnumber] =   @ItemNum AND [jobnum]= @JobNum", con))
                        {
                            parameter.Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                            command.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
                            command.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
                            command.Parameters.Add(parameter);
                            command.Connection = con;
                            command.ExecuteNonQuery();
                        }

                    }
                    else
                    {
                        break;

                    }
                }
                int b = 1;
                for (int a = 0; a < Global.PartNumber; a++)
                {
                    string StrQuery = "INSERT INTO access(jobnum,accessy,marknumber, itemnumber,partnumber) VALUES('" + Global.JobNumber + "','" + arrayC[a] + "','" + Global.PTPTag + "','" + Global.ItemNumber + "','" + partC[a] + "')";
                    rowNumber++; columnNumber++;
                    using (SqlCommand getComm = new SqlCommand(StrQuery))
                    {
                        // inserting in the job number the colum and the mark number 
                        getComm.Connection = con;
                        getComm.CommandText = StrQuery;
                        getComm.ExecuteNonQuery();
                    }
                }
                Add();


                con.Close();

            }
            catch (InvalidCastException E)
            {
                textBox11.Text = "eRROR";
            }
            */

        }

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void totalItems_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView5.Rows[selectedRow];

            bool isSelected = Convert.ToBoolean(dataGridView5.Rows[selectedRow].Cells[6].Value);

                Form3 forg = new Form3();
                forg.Show();
                //MessageBox.Show("True");
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
        }
        int RowCount = 0;
        private void dataGridView5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView5.Rows[selectedRow];
            textBox8.Text = row.Cells[6].Value.ToString();

            int i = 0;

            bool isSelected = Convert.ToBoolean(row.Cells[6].Value);
            if (isSelected)
            {
                Form3 jorg = new Form3();
                jorg.Show();
                //MessageBox.Show("True");
            }
            else
            {
                //secondaryop[i] = "f";
                //MessageBox.Show("false");

                i++;
            }
        }


        private void RowCheckBoxClick()
        //private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            foreach (DataGridViewRow Row in dataGridView5.Rows)
            {
                if ((bool)(Row.Cells["chkBxSelect"].Value) == true)
                {
                    this.dataGridView5.Rows[Row.Index].Selected = true;
                }
                else
                {
                    this.dataGridView5.Rows[Row.Index].Selected = false;
                }
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.dataGridView5.AutoGenerateColumns = false;

            // data gridView5
            SqlDataReader readaccess = null;
            int inserted = 0;

            int[] arrayP = new int[50];
            string[] stage1 = new string[50];
            string[] stage2 = new string[50];
            string[] stage3 = new string[50];
            int[] access = new int[50];
            string[] primaryop = new string[50];
            string[] secondaryop = new string[50];



            int rownumber = 0; int columnnumber = 0;
            SqlConnection con = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); con.Open();
            SqlParameter param = new SqlParameter();
            SqlParameter parameterd = new SqlParameter();
            SqlCommand command = new SqlCommand();

            
            // SqlDataReader read = null;
            SqlCommand commandreadPI = new SqlCommand("SELECT [jobnum],[accessy],[marknumber],[itemnumber],[partnumber], [Stage1],[Stage2],[Stage3] FROM [JQNew].[dbo].[access] WHERE [itemnumber] = @ItemNum AND [jobnum] = @JobNum", con);
            parameterd.Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            commandreadPI.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
            commandreadPI.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
            commandreadPI.Parameters.Add(new SqlParameter("@PartNum", Global.JobNumber));
            readaccess = commandreadPI.ExecuteReader();
            while (readaccess.Read())
            {
                Global.AccessNumber = readaccess["accessy"].ToString();
                //MessageBox.Show("You are replacing" + Global.AccessNumber);
                if (Global.AccessNumber != null)
                {
                    //S/qlCommand command = new SqlCommand();
                    //command.CommandText = "UPDATE accessy SET [itemnumber] = ";
                    SqlParameter parameter = new SqlParameter();
                    // MessageBox.Show("NOOOOOOOOOO");
                    using (SqlCommand commands = new SqlCommand("DELETE FROM access WHERE  [itemnumber] =   @ItemNum AND [jobnum]= @JobNum", con))
                    {
                        parameter.Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                        commands.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
                        commands.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
                        // commands.Parameters.Add(new SqlParameter("@PartNum", Global.PartNumber));
                        commands.Parameters.Add(parameter);
                        commands.Connection = con;
                        commands.ExecuteNonQuery();
                    }
                                    }
                else
                {
                    break;

                }
            }
            param.Value = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            command.Parameters.Add(param);
            command.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
            command.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
            command.Parameters.Add(new SqlParameter("@UserInput", Global.PartEntered));
            int rowNumber = 0;
            int rowNums = 0;
            int j = 0;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (rowNums == Global.PartNumber)
                    break;
                else
                {
                    arrayP[j] = Convert.ToInt32(dataGridView5.Rows[rowNums].Cells[0].Value);
                    if (arrayP[j] == null)
                    {
                        MessageBox.Show("Do not enter null values");
                        break;
                    }
                    ++rowNums;
                    j++;
                }

            }
            int s = 0;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (rowNumber == Global.PartNumber)
                    break;
                else
                {
                    stage1[s] = dataGridView5.Rows[rowNumber].Cells[1].Value.ToString();
                    stage2[s] = dataGridView5.Rows[rowNumber].Cells[2].Value.ToString();
                    stage3[s] = dataGridView5.Rows[rowNumber].Cells[3].Value.ToString();
                    access[s] = Convert.ToInt32(dataGridView5.Rows[rowNumber].Cells[4].Value);
                    primaryop[s] = dataGridView5.Rows[rowNumber].Cells[5].Value.ToString();
                    //secondaryop[i] = Convert.ToBoolean(dataGridView5.Rows[rowNumber].Cells[6].Value.ToString());
                    rowNumber++; s++;
                }
            }
            //Console.WriteLine(stage1);
            int i = 0;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (i == Global.PartNumber)
                    break;
                else
                {

                    bool isSelected = Convert.ToBoolean(row.Cells[6].Value);
                    if (isSelected)
                    {
                        secondaryop[i] = "t";
                        //MessageBox.Show("True");
                    }
                    else
                    {
                        secondaryop[i] = "f";
                        //MessageBox.Show("false");
                    }
                    i++;
                }
            }

            for (int a = 0; a < Global.PartNumber; a++)
            {
                string StrQuery = "INSERT INTO access(jobnum, accessy, marknumber, itemnumber,partnumber,Stage1, Stage2, Stage3, primaryop, secondaryop) VALUES('" + Global.JobNumber + "','" + access[a] + "','" + Global.MarkNumber + "','" + Global.ItemNumber + "','" + arrayP[a] + "','" + stage1[a] + "','" + stage2[a] + "','" + stage3[a] + "','" + primaryop[a] + "','" + secondaryop[a] + "')";
                rownumber++; columnnumber++;
                using (SqlCommand getComm = new SqlCommand(StrQuery))
                {
                    // inserting in the job number the colum and the mark number 
                    getComm.Connection = con;
                    getComm.CommandText = StrQuery;
                    getComm.ExecuteNonQuery();
                }
            }
            dataGridView3.Refresh();
            Add();
            con.Close();
        }

        private void dataGridView5_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
