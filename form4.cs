
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
using System.Text.RegularExpressions;
using System.IO;
// this is pre useful somehow somewhere
namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {

        }
        public bool DetermineToTerminate = false;
        public static string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(" ");
            }
            return builder.ToString();
        }

        static string ConvertStringArrayToStringJoin(string[] array)
        {
            // Use string Join to concatenate the string elements.
            string result = string.Join(" ", array);
            return result;
        }
        public static void seeSubAssemblies()
        {
            SqlParameter param = new SqlParameter();

            SqlDataReader read = null;
            SqlConnection seeSA = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); seeSA.Open();
            SqlCommand seeSubA = new SqlCommand("SELECT [SubAssemblies] FROM [JQNew].[dbo].[subassembly] WHERE ([jobnum] = @JobNum AND [itemnum] = @ItemNum)", seeSA);
            SqlParameter around = new SqlParameter();
            //  seeSubA.Parameters.AddWithValue("@SA", Global.seedFirst);
            //  seeSubA.Parameters.AddWithValue("@SA1", Global.seedLast);
            seeSubA.Parameters.AddWithValue("@JobNum", Global.JobNumber);
            seeSubA.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
            string[] SubAssembly = new string[30];
            param.Value = Global.seedFirst;
            read = seeSubA.ExecuteReader();
            int indexer = 0;

            while (read.Read())
            {
                SubAssembly[indexer] = read["SubAssemblies"].ToString();

                if (SubAssembly[indexer] == null || SubAssembly[indexer] == " " || SubAssembly[indexer] == "          " || String.IsNullOrWhiteSpace(SubAssembly[indexer]))
                {
                    break;
                }

                // MessageBox.Show("" + PartContainer[indexer]);

                indexer++;
            }
            SubAssembly = SubAssembly.Where(item => item != null).ToArray();

            // MessageBox.Show("this is the " + indexer);
            if (indexer == 0)
            {
                Global.StageCounter = 2;
            }
            if (indexer == 1)
            {
                Global.StageCounter = 3;
            }
            if (indexer == 2)
            {
                Global.StageCounter = 4;
                //    MessageBox.Show("I am here");
            }
            if (indexer == 3)
            {
                Global.StageCounter = 4;
            }
            if (indexer == 4)
            {
                Global.StageCounter = 5;
            }
            if (indexer == 5)
            {
                Global.StageCounter = 6;
            }

            seeSA.Close();
        }
        public static void insert_partnumbers()
        {
            int[] holdme = new int[30];
            List<int> holdmine = new List<int>();
            int placeholder = Convert.ToInt32(Global.seeFirst);
            int placeholder1 = Convert.ToInt32(Global.seeSecond);
            int total_count = 1; int ptpholder = placeholder;
            if (placeholder1 != 0)
            {
                for (int i = placeholder; i <= placeholder1; i++)
                {
                    holdmine.Add(ptpholder);
                    holdme[i] = ptpholder;
                    //   MessageBox.Show("holdme from insert_PARTNUMBERS" + holdme[i]);
                    total_count++; ptpholder++;
                }
            } else
            {
                holdme[0] = placeholder; holdmine.Add(ptpholder); total_count = 1;
            }
            SqlConnection conn = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); conn.Open();
            SqlCommand comm = new SqlCommand();

            Global.PartsContainer = holdmine.ToArray();

            SqlParameter param = new SqlParameter();
            //  Global.Procedure = text;
            //   Global.Comments = text2;
            //     MessageBox.Show("Global Proceudre" + Global.Procedure + "Global comments" + Global.Comments);
            for (int holding = 0; holding < holdmine.Count; holding++)
            {

                string StrQuery = "INSERT INTO subassembly([Parts], [itemnum],[jobnum]) VALUES('" + holdmine[holding] + "','" + Global.ItemNumber + "','" + Global.JobNumber + "')";

                comm.Connection = conn;

                using (SqlCommand commm = new SqlCommand(StrQuery))
                {
                    // inserting in the job number the colum and the mark number 
                    comm.Connection = conn;
                    comm.CommandText = StrQuery;
                    comm.ExecuteNonQuery();
                }
            }


            conn.Close();

            Global.Truth = true;

        }


        public static void insert(string populate, int[] PartNumber, bool SAExists, int next_value)
        {
            //  Global.StageCounter = 1;
            //SqlConnection connecion = new SqlConnection();
            //  SqlCommand insert = new SqlCommand("");
            // MessageBox.Show("updateaccess " + populate); //Global.Populate = populate;
            SqlParameter param = new SqlParameter();
            //  seeSubAssemblies();
            //SqlConnection connecion = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); //connecion.Open();
            int counter = 1;
            //     MessageBox.Show("this is ee first and see second " + Global.seeFirst + " " + Global.seeSecond);
            int hold1 = Convert.ToInt32(Global.seeFirst);
            int part = 1;
            insert_partnumbers();
            if (Global.StageCounter > 1)
                seeSubAssemblies();
            if (Convert.ToInt32(Global.seeSecond) != 0)
            {
                for (int z = hold1; z < Convert.ToInt32(Global.seeSecond); z++)
                {
                    part++;
                }

                //MessageBox.Show("this i spart" + part);
            }
            int[] holdme = new int[part];
            if (part == 1)
            {
                //  for(int d = hold1; d< Convert.ToInt32(Global.seeSecond); d++) { 
                holdme[0] = hold1; MessageBox.Show("there is no second value enter in the parts ");
                //part = 1; 
                //holdme[1] = hold1;

            }
            else
            {
                for (int temp = 0; temp <= part - 1; temp++)
                {

                    holdme[temp] = hold1;
                    hold1++;
                    //MessageBox.Show("holdme temp" + holdme[temp]);
                }
            }
            SqlConnection conz = new SqlConnection("Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True");
            conz.Open();
            // MessageBox.Show("This is item numb" + Global.ItemNumber + "thi sis job num" + Global.JobNumber);
            try
            {
                for (int a = 0; a < part; a++)
                {
                    string StrQuerys = "INSERT INTO access(jobnum,  marknumber, itemnumber,partnumber) VALUES('" + Global.JobNumber + "','" + Global.MarkNumber + "','" + Global.ItemNumber + "','" + holdme[a] + "')";
                    //  rownumber++; columnnumber++;
                    using (SqlCommand getComms = new SqlCommand(StrQuerys))
                    {
                        // inserting in the job number the colum and the mark number 
                        //MessageBox.Show("this is the inser functinon"+ holdme[a]);
                        getComms.Connection = conz;
                        getComms.CommandText = StrQuerys;
                        getComms.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show($"Failed to update. Error message: {e.Message}");
            }
            conz.Close();

            for (int count = Convert.ToInt32(Global.seeFirst); count < Convert.ToInt32(Global.seeSecond); count++)
            {
                counter++;

            }
            //  MessageBox.Show("This is counter" + counter);
            int[] parts = new int[counter];
            int hold1st = Convert.ToInt32(Global.seeFirst);
            int hold2 = Convert.ToInt32(Global.seeSecond); int style = hold1;// counter is two in this instance 
                                                                             // MessageBox.Show("hold1 and two!!!" + hold1 + " , " + hold2);
            for (int temp = 0; temp <= counter - 1; temp++)
            {
                parts[temp] = style;
                //  MessageBox.Show("here is the error bitch" + parts[temp]);
                style++;
            }

            // SqlCommand comm = new SqlCommand();
            // MessageBox.Show("Im a dumb bithc");

            if (Global.StageCounter == 1)
            {
                //Form4 comboBox1;
                // MessageBox.Show("stage counter is = 1");
                // ComboBox comboBox1 = new ComboBox();
                // var comboBoxItem = comboBox1.Items[comboBox1.SelectedIndex] as ComboBoxItem;
                //  string selected = comboBox1.SelectedItem.Row;
                //    MessageBox.Show(selected);
                for (int j = 0; j <= counter; j++)
                {
                    var Sql = "Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True";
                    var StrQuery = "UPDATE access SET [Stage1] = @Populate, [Op1] = @Operation WHERE  [itemnumber] = @ItemNum AND [jobnum] = @JobNum AND [partnumber] = @Parts";

                    // commm.Connection = connecion;

                    try
                    {
                        SqlConnection connection = new SqlConnection(Sql); //connection.Open();

                        using (SqlCommand comm = new SqlCommand(StrQuery, connection))
                        {
                            // inserting in the job number the colum and the mark number
                            connection.Open();
                            //comm.Connection = connecion;
                            comm.CommandText = StrQuery;

                            comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
                            comm.Parameters.AddWithValue("@Operation", Global.Operation);
                            comm.Parameters.AddWithValue("@Populate", populate);
                            // comm.Parameters.AddWithValue("@Operation1", s);

                            //    MessageBox.Show("this is part Number" + parts[j]);
                            comm.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                            comm.Parameters.AddWithValue("@Parts", hold1st);
                            //  comm.Parameters.Add(new SqlParameter("@PartNum", PartNumber[temp]));
                            // MessageBox.Show("sukses");
                            param.Value = Global.ItemNumber;
                            comm.Parameters.Add(param);
                            hold1st++;
                            comm.ExecuteNonQuery();
                            comm.Parameters.Clear();
                        }
                        connection.Close();


                    }

                    catch (Exception e)
                    {
                        MessageBox.Show($"Failed to update. Error message: {e.Message}");
                        //enter = false; 
                    }
                    // comm.Parameters.Clear()

                    //  connection.Close();

                }
            }
            if (Global.StageCounter == 2)
            {
                // string noe =  comboBox1.SelectedItem.ToString();


                //MessageBox.Show("stage counter is = 1");
                for (int j = 0; j <= counter; j++)
                {
                    var Sql = "Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True";
                    var StrQuery = "UPDATE access SET [Stage1] = @Populate WHERE  [itemnumber] = @ItemNum AND [jobnum] = @JobNum AND [partnumber] = @Parts";

                    // commm.Connection = connecion;

                    try
                    {
                        SqlConnection connection = new SqlConnection(Sql); //connection.Open();

                        using (SqlCommand comm = new SqlCommand(StrQuery, connection))
                        {
                            // inserting in the job number the colum and the mark number
                            connection.Open();
                            //comm.Connection = connecion;
                            comm.CommandText = StrQuery;

                            comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
                            comm.Parameters.AddWithValue("@Populate", populate);
                            ///        comm.Parameters.AddWithValue("@Operation1", s);
                            //    MessageBox.Show("this is part Number" + parts[j]);
                            comm.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                            comm.Parameters.AddWithValue("@Parts", hold1st);
                            //  comm.Parameters.Add(new SqlParameter("@PartNum", PartNumber[temp]));
                            // MessageBox.Show("sukses");
                            param.Value = Global.ItemNumber;
                            comm.Parameters.Add(param);
                            hold1st++;
                            comm.ExecuteNonQuery();
                            comm.Parameters.Clear();
                        }
                        connection.Close();


                    }

                    catch (Exception e)
                    {
                        MessageBox.Show($"Failed to update. Error message: {e.Message}");
                        //enter = false; 
                    }
                    // comm.Parameters.Clear()

                    //  connection.Close();

                }
                //MessageBox.Show("Stage Counter is equal to 2");
                string[] PartContainer = new string[50];
                // MessageBox.Show("we made it bitch!!!!!!!!!!");
                //    MessageBox.Show("see dFirst " + Global.seedFirst + "see D last" + Global.seedLast);
                SqlDataReader read = null;
                SqlConnection seeSA = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); seeSA.Open();
                SqlCommand seeSubA = new SqlCommand("SELECT [Parts] FROM [JQNew].[dbo].[subassembly] WHERE ([jobnum] = @JobNum AND [itemnum] = @ItemNum)", seeSA);
                SqlParameter around = new SqlParameter();
                //  seeSubA.Parameters.AddWithValue("@SA", Global.seedFirst);
                //  seeSubA.Parameters.AddWithValue("@SA1", Global.seedLast);
                seeSubA.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                seeSubA.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);

                param.Value = Global.seedFirst;
                read = seeSubA.ExecuteReader();
                int indexer = 0;
                while (read.Read())
                {
                    PartContainer[indexer] = read["Parts"].ToString();
                    //   MessageBox.Show("" + PartContainer[indexer]);
                    indexer++;
                }
                seeSA.Close();
                int first_value = next_value - 1;
                //MessageBox.Show("this is first_value  " + first_value);

                populate = ("P" + first_value);
                string populate2 = ("SA" + next_value);
                string[] numbers = new string[50];
                numbers = PartContainer;
                int[] tey = new int[PartContainer.Length];
                string placeholder = String.Join("", Global.SubNum);
                string placeholder2 = ConvertStringArrayToStringJoin(numbers);
                // functions used to give us two placeholder values in order to to
                //MessageBox.Show("placeholder " + placeholder + "placeholder2 "+ placeholder2);
                //List<string> holder = tey.ToList();
                //  int[] myInts = Array.ConvertAll(PartContainer, s => int.Parse(s));

                string[] holdmine = placeholder.Split(',')
     .Select(x => x.Trim())
     .Where(x => !string.IsNullOrWhiteSpace(x))
     .ToArray();

                //MessageBox.Show("holdme elemtn1" + holdmine[3]);

                int[] tey2 = new int[20];
                //    tey2[0] = Convert.ToInt32(holdmine[0]);
                //     MessageBox.Show("tey2" + tey2[0]);
                //
                string words = string.Join(" ", holdmine);
                words = string.Join(" ", words.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                // MessageBox.Show("this is words" + words);

                //MessageBox.Show("thiks is woprds" + words[0] + words[1]);


                int[] array = words.Split(' ', '\t').Select(int.Parse).ToArray();

                // MessageBox.Show("this is array" + array[0] + "spped"+ array[1]);



                //     string words = phrase.Split(' ');

                //tey = numbers; //find the integer array
                /*                int[] array = new int[words.Length];
                                for (int i = 0; i < words.Length; i++)
                                {
                                    array[i] = Convert.ToInt32(words[i]);
                                    MessageBox.Show(" this is the parts array " + array[i] );
                                    if (array[i] == 0)
                                        break;
                                }

                            */
                int total_count = 1;
                int place_hold1 = Convert.ToInt32(Global.seeFirst); int place_hold2 = Convert.ToInt32(Global.seeSecond);
                int place_count = 0;

                for (int count = place_hold1; count < place_hold2; count++)
                {
                    place_count++;
                }

                int[] arr = new int[place_count + 1];
                if (place_hold2 != 0)
                {
                    for (int temp = 0; temp <= place_count; temp++)
                    {
                        arr[temp] = place_hold1;
                        place_hold1++;
                    }
                }
                if (place_hold2 == 0)
                {
                    arr = new int[Global.SubNum.Length + 1];

                    for (int i = 0; i <= Global.SubNum.Length; i++)
                    {
                        arr[i] = place_hold1;
                    }
                }
                int lowest_value = array.Min();
                int highest_value = array.Max();
                foreach (int count in array)
                {
                    total_count++;
                }

                //  MessageBox.Show("total_count" + total_count);
                for (int j = 0; j <= Global.SubNum.Length - 1; j++)
                {
                    var Sql = "Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True";
                    var StrQuery = "UPDATE access SET [Stage2] = @Populate2, [Op2] = @Operation  WHERE  [itemnumber] = @ItemNum AND [jobnum] = @JobNum AND [partnumber] = @Parts OR [partnumber] = @OurParts";

                    // commm.Connection = connecio
                    try
                    {
                        SqlConnection connection = new SqlConnection(Sql); //connection.Open();

                        using (SqlCommand comm = new SqlCommand(StrQuery, connection))
                        {
                            // inserting in the job number the colum and the mark number
                            connection.Open();
                            //comm.Connection = connecion;
                            comm.CommandText = StrQuery;

                            comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
                            //;/  comm.Parameters.AddWithValue("@Populate", populate);
                            comm.Parameters.AddWithValue("@Populate2", populate2);
                            //    MessageBox.Show("this is part Number" + parts[j]);
                            comm.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                            comm.Parameters.AddWithValue("@Parts", Global.SubNum[j]);
                            comm.Parameters.AddWithValue("@Operation", Global.Operation);

                            comm.Parameters.AddWithValue("@OurParts", arr[j]);
                            //      MessageBox.Show("print j " + j + "this is the array lengh we");
                            //  comm.Parameters.Add(new SqlParameter("@PartNum", PartNumber[temp]));
                            // MessageBox.Show("sukses");
                            param.Value = Global.ItemNumber;
                            comm.Parameters.Add(param);
                            // MessageBox.Show("this is part several times" + array[j]);


                            comm.ExecuteNonQuery();
                            comm.Parameters.Clear();

                        }
                        connection.Close();
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show($"Failed to update. Error message: {e.Message}");
                        //enter = false; 
                    }
                }
            }
            if (Global.StageCounter == 3)
            {
                Stage3(next_value, populate);
            }
            if (Global.StageCounter == 4)
            {
                Stage4();
            }
        }
        public static void Stage4()
        {
            MessageBox.Show("Stage four is about to begin please sit down and enjoy your seat!");
        }




        /*
        string[] PartContainer = new string[50];


        SqlDataReader read = null;
        SqlConnection seeSA = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); seeSA.Open();
        SqlCommand seeSubA = new SqlCommand("SELECT [Parts] FROM [JQNew].[dbo].[subassembly] WHERE ([jobnum] = @JobNum AND [itemnum] = @ItemNum)", seeSA);
        SqlParameter around = new SqlParameter();

        seeSubA.Parameters.AddWithValue("@JobNum", Global.JobNumber);
        seeSubA.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);

        param.Value = Global.seedFirst;
        read = seeSubA.ExecuteReader();
        int indexer = 0;
        while (read.Read())
        {
            PartContainer[indexer] = read["Parts"].ToString();

            MessageBox.Show("" + PartContainer[indexer]);
            indexer++;
        }
        seeSA.Close();
        int first_value = next_value - 1;
        MessageBox.Show("this is first_value  " + first_value);

        populate = ("P" + first_value);
        string populate2 = ("SA" + next_value);
        string[] numbers = new string[50];
        numbers = PartContainer;
        int[] tey = new int[PartContainer.Length];
        string placeholder = ConvertStringArrayToString(numbers);
        string placeholder2 = ConvertStringArrayToStringJoin(numbers);

        string[] holdmine = placeholder.Split(',')
.Select(x => x.Trim())
.Where(x => !string.IsNullOrWhiteSpace(x))
.ToArray();


        int[] tey2 = new int[20];


        MessageBox.Show("this is hold me " + holdme);

        string words = string.Join(" ", holdmine);
        words = string.Join(" ", words.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        MessageBox.Show("this is words" + words);



        int[] array = words.Split(' ', '\t').Select(int.Parse).ToArray();

        int total_count = 1;
        int lowest_value = array.Min();
        int highest_value = array.Max();
        foreach (int count in array)
        {
            total_count++;
        }

        //  MessageBox.Show("total_count" + total_count);
        for (int j = 0; j <= array.Length - 1; j++)
        {
            var Sql = "Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True";
            var StrQuery = "UPDATE access SET [Stage1] = @Populate, [Stage2] = @Populate2  WHERE  [itemnumber] = @ItemNum AND [jobnum] = @JobNum AND [partnumber] = @Parts";

            // commm.Connection = connecio
            try
            {
                SqlConnection connection = new SqlConnection(Sql); //connection.Open();

                using (SqlCommand comm = new SqlCommand(StrQuery, connection))
                {
                    // inserting in the job number the colum and the mark number
                    connection.Open();
                    //comm.Connection = connecion;
                    comm.CommandText = StrQuery;

                    comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
                    comm.Parameters.AddWithValue("@Populate", populate);
                    comm.Parameters.AddWithValue("@Populate2", populate2);
                    //    MessageBox.Show("this is part Number" + parts[j]);
                    comm.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                    comm.Parameters.AddWithValue("@Parts", array[j]);
                    MessageBox.Show("print j " + j + "this is the array lengh we");
                    //  comm.Parameters.Add(new SqlParameter("@PartNum", PartNumber[temp]));
                    // MessageBox.Show("sukses");
                    param.Value = Global.ItemNumber;
                    comm.Parameters.Add(param);
                    // MessageBox.Show("this is part several times" + array[j]);


                    comm.ExecuteNonQuery();
                    comm.Parameters.Clear();

                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show($"Failed to update. Error message: {e.Message}");
                //enter = false; 
            }


        }

    SqlConnection connecion = new SqlConnection("Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True"); connecion.Open();
    SqlParameter parameter = new SqlParameter();
    // MessageBox.Show("NOOOOOOOOOO");
    using (SqlCommand commands = new SqlCommand("DELETE FROM access WHERE  [jobnum]= @JobNum AND [partnumber] IS NULL", connecion))
    {
        parameter.Value = Global.JobNumber;

        commands.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
        //commands.Parameters.Add(new SqlParameter("@Parts", null));
        // commands.Parameters.Add(new SqlParameter("@PartNum", Global.PartNumber));
        commands.Parameters.Add(parameter);
        commands.Connection = connecion;
        commands.ExecuteNonQuery();
       // MessageBox.Show("you are an asshole here is proof");
    }
    connecion.Close();
}
*/



        /*
        if (Global.StageCounter == 2)
        {

            string StrQuery = "INSERT INTO access(jobnum, accessy, marknumber, itemnumber,partnumber,Stage1, Stage2, Stage3, Op1, Op2, Op3, prock, comments) VALUES('" + Global.JobNumber + "','" + access[a] + "','" + Global.MarkNumber + "','" + Global.ItemNumber + "','" + arrayP[a] + "','" + stage1[a] + "','" + stage2[a] + "','" + stage3[a] + "','" + primaryop[a] + "','" + secondaryop[a] + "','" + thirdop[a] + "','" + procedure[a] + "','" + comments[a] + "')";
            comm.Connection = connecion;

            using (SqlCommand commm = new SqlCommand(StrQuery))
            {
                // inserting in the job number the colum and the mark number 
                comm.Connection = connecion;
                comm.CommandText = StrQuery;
                comm.ExecuteNonQuery();
            }
        }
        if (Global.StageCounter == 3)
        {

            string StrQuery = "INSERT INTO access(jobnum, accessy, marknumber, itemnumber,partnumber,Stage1, Stage2, Stage3, Op1, Op2, Op3, prock, comments) VALUES('" + Global.JobNumber + "','" + access[a] + "','" + Global.MarkNumber + "','" + Global.ItemNumber + "','" + arrayP[a] + "','" + stage1[a] + "','" + stage2[a] + "','" + stage3[a] + "','" + primaryop[a] + "','" + secondaryop[a] + "','" + thirdop[a] + "','" + procedure[a] + "','" + comments[a] + "')";
            comm.Connection = connecion;

            using (SqlCommand commm = new SqlCommand(StrQuery))
            {
                // inserting in the job number the colum and the mark number 
                comm.Connection = connecion;
                comm.CommandText = StrQuery;
                comm.ExecuteNonQuery();
            }
        }
        if (Global.StageCounter == 4)
        {

            string StrQuery = "INSERT INTO access(jobnum, accessy, marknumber, itemnumber,partnumber,Stage1, Stage2, Stage3, Op1, Op2, Op3, prock, comments) VALUES('" + Global.JobNumber + "','" + access[a] + "','" + Global.MarkNumber + "','" + Global.ItemNumber + "','" + arrayP[a] + "','" + stage1[a] + "','" + stage2[a] + "','" + stage3[a] + "','" + primaryop[a] + "','" + secondaryop[a] + "','" + thirdop[a] + "','" + procedure[a] + "','" + comments[a] + "')";
            comm.Connection = connecion;

            using (SqlCommand commm = new SqlCommand(StrQuery))
            {
                // inserting in the job number the colum and the mark number 
                comm.Connection = connecion;
                comm.CommandText = StrQuery;
                comm.ExecuteNonQuery();
            }
        }
        */


        struct Buffer<TElement>
        {
            internal TElement[] items;
            internal int count;
            internal Buffer(IEnumerable<TElement> source)
            {
                TElement[] array = null;
                int num = 0;
                ICollection<TElement> collection = source as ICollection<TElement>;
                if (collection != null)
                {
                    num = collection.Count;
                    if (num > 0)
                    {
                        array = new TElement[num];
                        collection.CopyTo(array, 0);
                    }
                }
                else
                {
                    foreach (TElement current in source)
                    {
                        if (array == null)
                        {
                            array = new TElement[4];
                        }
                        else
                        {
                            if (array.Length == num)
                            {
                                TElement[] array2 = new TElement[checked(num * 2)];
                                Array.Copy(array, 0, array2, 0, num);
                                array = array2;
                            }
                        }
                        array[num] = current;
                        num++;
                    }
                }
                this.items = array;
                this.count = num;
            }
            public TElement[] ToArray()
            {
                if (this.count == 0)
                {
                    return new TElement[0];
                }
                if (this.items.Length == this.count)
                {
                    return this.items;
                }
                TElement[] array = new TElement[this.count];
                Array.Copy(this.items, 0, array, 0, this.count);
                return array;
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public static IEnumerable<int> StringToIntList(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                yield break;

            }
            foreach (var s in str.Split(','))
            {
                int num;
                if (int.TryParse(s, out num))
                {
                    yield return num;
                    //  MessageBox.Show("x"+ num);
                }
            }

        }
        public static void ExecuteOrder66(string[] pass, int hold_nxt)
        {
            if (Global.StageCounter == 3)
            {
                insert_partnumbers();
                SqlParameter param = new SqlParameter();
                // string populate \
                string populate_old = ("SA" + hold_nxt);
                string populate = ("SA" + Global.Head);
                string[] array = new string[pass.Length];
                array = pass;
                insert_partnumbers();
                // Global.seedFirst;
                for (int j = 0; j <= array.Length - 1; j++)
                {
                    var Sql = "Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True";
                    var StrQuery = "UPDATE access SET [Stage3] = @Populate3, [Op3] = @Operation WHERE  [itemnumber] = @ItemNum AND [jobnum] = @JobNum AND [Stage2] = @Parts";

                    // commm.Connection = connecio
                    try
                    {
                        SqlConnection connection = new SqlConnection(Sql); //connection.Open();

                        using (SqlCommand comm = new SqlCommand(StrQuery, connection))
                        {
                            // inserting in the job number the colum and the mark number
                            connection.Open();
                            //comm.Connection = connecion;
                            comm.CommandText = StrQuery;

                            comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
                            // comm.Parameters.AddWithValue("@Populate", populate);
                            comm.Parameters.AddWithValue("@Populate3", populate);
                            //    MessageBox.Show("this is part Number" + parts[j]);
                            comm.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                            comm.Parameters.AddWithValue("@Operation", Global.Operation);

                            comm.Parameters.AddWithValue("@Parts", array[j]);
                            //MessageBox.Show("print j " + j + "this is the array lengh we");
                            //  comm.Parameters.Add(new SqlParameter("@PartNum", PartNumber[temp]));
                            // MessageBox.Show("sukses");
                            param.Value = Global.ItemNumber;
                            comm.Parameters.Add(param);
                            comm.ExecuteNonQuery();
                            comm.Parameters.Clear();
                            // MessageBox.Show("this is hte proper out put that should me obtained");
                        }
                        connection.Close();
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show($"Failed to update. Error message: {e.Message}");
                        //enter = false; 
                    }
                }
            }
        }

        public static void ExecuteOrder68(string[] pass, int hold_nxt)
        {
            if (Global.StageCounter == 4)
            {
                insert_partnumbers();
                SqlParameter param = new SqlParameter();
                // string populate \
                string populate_old = ("SA" + hold_nxt);
                string populate = ("SA" + Global.Head);
                string[] array = new string[pass.Length];
                array = pass;
                insert_partnumbers();
                // Global.seedFirst;
                for (int j = 0; j <= array.Length - 1; j++)
                {
                    var Sql = "Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True";
                    var StrQuery = "UPDATE access SET [Stage4] = @Populate3, [Op4] = @Operation WHERE  [itemnumber] = @ItemNum AND [jobnum] = @JobNum AND [Stage3] = @Parts";

                    // commm.Connection = connecio
                    try
                    {
                        SqlConnection connection = new SqlConnection(Sql); //connection.Open();

                        using (SqlCommand comm = new SqlCommand(StrQuery, connection))
                        {
                            // inserting in the job number the colum and the mark number
                            connection.Open();
                            //comm.Connection = connecion;
                            comm.CommandText = StrQuery;

                            comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
                            // comm.Parameters.AddWithValue("@Populate", populate);
                            comm.Parameters.AddWithValue("@Populate3", populate);
                            //    MessageBox.Show("this is part Number" + parts[j]);
                            comm.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                            comm.Parameters.AddWithValue("@Operation", Global.Operation);

                            comm.Parameters.AddWithValue("@Parts", array[j]);
                            //MessageBox.Show("print j " + j + "this is the array lengh we");
                            //  comm.Parameters.Add(new SqlParameter("@PartNum", PartNumber[temp]));
                            // MessageBox.Show("sukses");
                            param.Value = Global.ItemNumber;
                            comm.Parameters.Add(param);
                            comm.ExecuteNonQuery();
                            comm.Parameters.Clear();
                            // MessageBox.Show("this is hte proper out put that should me obtained");
                        }
                        connection.Close();
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show($"Failed to update. Error message: {e.Message}");
                        //enter = false; 
                    }
                }
                Global.Stage4 = true; 
            }
        }

        public static void ExecuteOrder67(string[] pass, int hold_nxt)
        {
            if (Global.StageCounter == 2)
            {
                insert_partnumbers();
                SqlParameter param = new SqlParameter();
                // string populate \
                string populate_old = ("SA" + hold_nxt);
                string populate = ("SA" + Global.Head);
                string[] array = new string[pass.Length];
                List<string> amax = new List<string>();
                
                array = pass;
                amax = array.ToList();
            
                amax.Add(Global.PartsContainer);
              
                insert_partnumbers();
                // Global.seedFirst;
                for (int j = 0; j <= array.Length - 1; j++)
                {
                    var Sql = "Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True";
                    var StrQuery = "UPDATE access SET [Stage2] = @Populate3, [Op1] = @Operation WHERE  [itemnumber] = @ItemNum AND [jobnum] = @JobNum AND [Stage1] = @Parts";

                    // commm.Connection = connecio
                    try
                    {
                        SqlConnection connection = new SqlConnection(Sql); //connection.Open();

                        using (SqlCommand comm = new SqlCommand(StrQuery, connection))
                        {
                            // inserting in the job number the colum and the mark number
                            connection.Open();
                            //comm.Connection = connecion;
                            comm.CommandText = StrQuery;

                            comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
                            // comm.Parameters.AddWithValue("@Populate", populate);
                            comm.Parameters.AddWithValue("@Populate3", populate);
                            //    MessageBox.Show("this is part Number" + parts[j]);
                            comm.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                            comm.Parameters.AddWithValue("@Operation", Global.Operation);

                            comm.Parameters.AddWithValue("@Parts", array[j]);
                            //MessageBox.Show("print j " + j + "this is the array lengh we");
                            //  comm.Parameters.Add(new SqlParameter("@PartNum", PartNumber[temp]));
                            // MessageBox.Show("sukses");
                            param.Value = Global.ItemNumber;
                            comm.Parameters.Add(param);
                            comm.ExecuteNonQuery();
                            comm.Parameters.Clear();
                            // MessageBox.Show("this is hte proper out put that should me obtained");
                        }
                        connection.Close();
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show($"Failed to update. Error message: {e.Message}");
                        //enter = false; 
                    }
                }
            }

        }
    


        public static void Stage3(int next_value, string populate)
        {

         //   MessageBox.Show("Stage Counter is equal to 3");
            string[] PartContainer = new string[50];
            // MessageBox.Show("we made it bitch!!!!!!!!!!");
            //    MessageBox.Show("see dFirst " + Global.seedFirst + "see D last" + Global.seedLast);
            SqlDataReader read = null;
            SqlConnection seeSA = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); seeSA.Open();
            SqlCommand seeSubA = new SqlCommand("SELECT [Parts] FROM [JQNew].[dbo].[subassembly] WHERE ([jobnum] = @JobNum AND [itemnum] = @ItemNum)", seeSA);
            SqlParameter around = new SqlParameter();
            SqlParameter param = new SqlParameter();
            //  seeSubA.Parameters.AddWithValue("@SA", Global.seedFirst);
            //  seeSubA.Parameters.AddWithValue("@SA1", Global.seedLast);
            seeSubA.Parameters.AddWithValue("@JobNum", Global.JobNumber);
            seeSubA.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);

            param.Value = Global.seedFirst;
            read = seeSubA.ExecuteReader();
            int indexer = 0;
            while (read.Read())
            {
                PartContainer[indexer] = read["Parts"].ToString();

               // MessageBox.Show("" + PartContainer[indexer]);
                indexer++;
            }
            seeSA.Close();
            int first_value = next_value - 1;
          //  MessageBox.Show("this is first_value  " + first_value);

            populate = ("P" + first_value);
            string populate2 = ("SA" + next_value);
            string[] numbers = new string[50];
            numbers = PartContainer;
            int[] tey = new int[PartContainer.Length];
            string placeholder = ConvertStringArrayToString(numbers);
            string placeholder2 = ConvertStringArrayToStringJoin(numbers);
            // functions used to give us two placeholder values in order to to
            //MessageBox.Show("placeholder " + placeholder + "placeholder2 "+ placeholder2);
            //List<string> holder = tey.ToList();
            //  int[] myInts = Array.ConvertAll(PartContainer, s => int.Parse(s));
            string[] holdmine = placeholder.Split(',')
 .Select(x => x.Trim())
 .Where(x => !string.IsNullOrWhiteSpace(x))
 .ToArray();

            //MessageBox.Show("holdme elemtn1" + holdmine[3]);

            int[] tey2 = new int[20];
            //    tey2[0] = Convert.ToInt32(holdmine[0]);
            //     MessageBox.Show("tey2" + tey2[0]);
         //   MessageBox.Show("this is hold me " + holdme);

            string words = string.Join(" ", holdmine);
            words = string.Join(" ", words.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            //MessageBox.Show("this is words" + words);

            //MessageBox.Show("thiks is woprds" + words[0] + words[1]);


            int[] array = words.Split(' ', '\t').Select(int.Parse).ToArray();

            // MessageBox.Show("this is array" + array[0] + "spped"+ array[1]);



            //     string words = phrase.Split(' ');

            //tey = numbers; //find the integer array
                     
                        
            int total_count = 1;
            int lowest_value = array.Min();
            int highest_value = array.Max();
            foreach (int count in array)
            {
                total_count++;
            }

            //  MessageBox.Show("total_count" + total_count);
            for (int j = 0; j <= array.Length - 1; j++)
            {
                var Sql = "Data Source = SQL; Initial Catalog = JQNew; MultipleActiveResultSets = true; Integrated Security = True";
                var StrQuery = "UPDATE access SET [Stage3] = @Populate3  WHERE  [itemnumber] = @ItemNum AND [jobnum] = @JobNum AND [partnumber] = @Parts";

                // commm.Connection = connecio
                try
                {
                    SqlConnection connection = new SqlConnection(Sql); //connection.Open();

                    using (SqlCommand comm = new SqlCommand(StrQuery, connection))
                    {
                        // inserting in the job number the colum and the mark number
                        connection.Open();
                        //comm.Connection = connecion;
                        comm.CommandText = StrQuery;

                        comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber);
                       // comm.Parameters.AddWithValue("@Populate", populate);
                        comm.Parameters.AddWithValue("@Populate3", populate);
                        //    MessageBox.Show("this is part Number" + parts[j]);
                        comm.Parameters.AddWithValue("@JobNum", Global.JobNumber);
                        comm.Parameters.AddWithValue("@Parts", array[j]);
                        //MessageBox.Show("print j " + j + "this is the array lengh we");
                        //  comm.Parameters.Add(new SqlParameter("@PartNum", PartNumber[temp]));
                        // MessageBox.Show("sukses");
                        param.Value = Global.ItemNumber;
                        comm.Parameters.Add(param);
                        // MessageBox.Show("this is part several times" + array[j]);


                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();

                    }
                    connection.Close();
                }

                catch (Exception e)
                {
                    MessageBox.Show($"Failed to update. Error message: {e.Message}");
                    //enter = false; 
                }
            }
          //  public void Close();

    }
        public static int validateIfExists(string x)
        {

           // MessageBox.Show("validating...");
            SqlDataReader readInfo = null;
            SqlCommand view = new SqlCommand("SELECT  [Stage1],[Stage2], [Stage3] FROM [JQNew].[dbo].[access] WHERE [jobnum] = @JobNum AND [itemnumber] = @ItemNum AND([Stage1] =@StageCheck OR [Stage2] =@StageCheck OR [Stage3] = @StageCheck)");
            SqlConnection power = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); power.Open();
            SqlParameter condition = new SqlParameter();

            view.Parameters.Add(condition);
            view.Connection = power;
            condition.Value = Global.ItemNumber;

            view.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
            view.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));
            view.Parameters.Add(new SqlParameter("@StageCheck",x));

            readInfo = view.ExecuteReader();
            bool decide = true;
            bool react = false;
            int indexs = 0;
            string[] seeStage1 = new string[50];
            string[] seeStage2 = new string[50];
            string[] seeStage3 = new string[50];
            int[] partnum = new int[50];
            Boolean stage1E = false;
            Boolean stage3E = false;
            Boolean stage2E = false;
            Boolean stage4E = false;

            while (readInfo.Read())
            {
                seeStage1[indexs] = readInfo["Stage1"].ToString();
                if (seeStage1[indexs] == null || String.IsNullOrWhiteSpace(seeStage1[indexs]) )
                {
                   // MessageBox.Show("stage 1 is empty");
                }
                else stage1E = true; 
                seeStage2[indexs] = readInfo["Stage2"].ToString();
                if (seeStage2[indexs] == null || String.IsNullOrWhiteSpace(seeStage2[indexs]))
                {
                  //  MessageBox.Show("stage 2 is empty");
                }
                else stage2E = true;
                seeStage3[indexs] = readInfo["Stage3"].ToString();
                if (seeStage1[indexs] == null || String.IsNullOrWhiteSpace(seeStage3[indexs]))
                {
                  //  MessageBox.Show("stage 3 is empty");
                }
                else stage3E = true; 
                indexs++;
            }
            if (stage1E & !stage2E & !stage3E)
            {
            //    MessageBox.Show("yeh dude");
                Global.StageCounter = 2;
                for(int temp = 0; temp < seeStage1.Length; temp++)
                {
                    if (seeStage1[temp] == x)
                    {
                        string numberOnly = Regex.Replace(x, "[^0-9.]", "");
                        return Convert.ToInt32(numberOnly);
                    }
                    else break;
                }
                
            }
            if (stage2E & !stage3E)
            {
              //  MessageBox.Show("heck yeah");
                Global.StageCounter = 3;
                for (int temp = 0; temp < seeStage1.Length; temp++)
                {
                    if (seeStage2[temp] == x)
                    {
                        string numberOnly = Regex.Replace(x, "[^0-9.]", "");
                        return Convert.ToInt32(numberOnly);
                    }
                    else break;
                }
            }
            if (stage3E & !stage4E)
            {
                //     MessageBox.Show("fuck ");
                Global.StageCounter = 4;
                for (int temp = 0; temp < seeStage1.Length; temp++)
                {
                    if (seeStage3[temp] == x)
                    {
                        string numberOnly = Regex.Replace(x, "[^0-9.]", "");
                        return Convert.ToInt32(numberOnly);
                    }
                    else break;
                }
            }
     
                return -1;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Global.StageCounter = 1;
            string operation = comboBox1.Text;
            Global.Operation = operation;
            //MessageBox.Show(operation);
            int[] seeFirstOld = new int[50];
            //    seeFirstOld[0] = "0";
            // Global.seeSecondO[0] = "0";
            int[] seeSOLD = new int[50];
            //   seeSOLD[0] = "0";
            bool enter = true;
            string head = name.Text;
            //     MessageBox.Show("" + head);
            string part = parts.Text;
            string subassembly = subs.Text;
            string placeholder = subs.Text;
            IList<string> names = part.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
            IList<string> subnum = placeholder.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
            string text = richTextBox1.Text;
            string text2 = richTextBox2.Text;
            string[] namez = new string[50];
            int test = 0; int jest = 0; int total = 0;
            //int dFirst = 0; int dLast = 0; 
            string first;
            bool single = false;

            for (int j = 0; j < names.Count; j++)
            {
                if (j == 0)
                {
                    first = names[j];
                    Global.seeFirst = null;
                    Global.seeFirst = first;// MessageBox.Show("This is a test testing out see First" + Global.seeFirst);
                 
                }
                else if (j == 1)
                {
                    string second = names[j];
                    test = Convert.ToInt32(second); if (test > Global.PartNumber) { MessageBox.Show("Enter a valid syntax in which the part number for this job is larger than the entered part range"); test = 0; }
                    Global.seeSecond = null;
                    Global.seeSecond = second; //MessageBox.Show("This is a test testing out see second" + Global.seeSecond);
                }
                else
                {
                    MessageBox.Show("Please enter valid syntax");
                    enter = false;
                }
            }
            int hold_nxt;
            string[] pass = new string[subnum.Count];
            Global.SubNum = subnum.ToArray();
            string dFirst = null; string dLast = null;
            string[] stringCheck = { "SA1", "SA2", "SA3", "SA4", "SA5", "SA6", "SA7", "SA8", "SA9", "SA10" };
            for (int q = 0; q < subnum.Count; q++)
            {
                if (q == 0)
                {
                    Global.StageCounter = 2;
                    foreach (string i in stringCheck)
                    {
                        if (subnum[q] == i)
                        {

                            int p_hold1 = validateIfExists(i);
                            MessageBox.Show("this is p_hold1" + p_hold1);
                            dFirst = p_hold1.ToString();
                            pass[0] = ("SA"+dFirst);
                            if (subnum.Count == 1)
                            {
                                hold_nxt = p_hold1 + 1;
                                ExecuteOrder66(pass, hold_nxt);
                                ExecuteOrder67(pass, hold_nxt);
                                ExecuteOrder68(pass, hold_nxt);

                                DetermineToTerminate = true;
                            }
                            break;
                        }
                        else
                            dFirst = subnum[q];
                        // MessageBox.Show(worst);
                    }
                    Global.seedFirst = Convert.ToInt32(dFirst);
                }
                if (q == 1)
                {
                    foreach (string i in stringCheck)
                    {
                        if (subnum[q] == i)
                        {
                            int p_hold2 = validateIfExists(i);
                            dLast = p_hold2.ToString();
                            ///            
                            ///insert("", 0, false, 0);
                            pass[1] = ("SA"+dLast);
                            hold_nxt = p_hold2 + 1;
                            ExecuteOrder66(pass, hold_nxt);
                            ExecuteOrder67(pass, hold_nxt);
                            ExecuteOrder68(pass, hold_nxt);

                            DetermineToTerminate = true;
                            break;
                        }
                        else dLast = subnum[q];
                    }
                    jest = Convert.ToInt32(dLast); if (jest > Global.PartNumber) { MessageBox.Show("Enter a valid syntax in which the part number for this job is larger than the entered part range"); jest = 0; }
                   // Global.StageCounter += 1;
                    //  MessageBox.Show("This should resolve your issues good lad");
                    Global.seedLast = Convert.ToInt32(dLast);
                }
                if (q > 2)
                {
                    MessageBox.Show("Please enter valid syntax");
                    enter = false;
                }
            }

            // get the distance between the two subassembly nubers

            if (!DetermineToTerminate)
            {
                MessageBox.Show(part);
                SqlDataReader readInfo = null;
                SqlCommand view = new SqlCommand("SELECT [Parts], [SubAssemblies], [SubAssembly] FROM [JQNew].[dbo].[subassembly] WHERE [jobnum] = @JobNum AND [itemnum] = @ItemNum");
                SqlConnection power = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); power.Open();
                SqlParameter condition = new SqlParameter();

                view.Parameters.Add(condition);
                view.Connection = power;
                condition.Value = Global.ItemNumber;

                view.Parameters.Add(new SqlParameter("@ItemNum", Global.ItemNumber));
                view.Parameters.Add(new SqlParameter("@JobNum", Global.JobNumber));

                readInfo = view.ExecuteReader();
                bool decide = true;
                bool react = false;
                int indexs = 0;
                string[] seeParts = new string[50];
                string[] seeSAName = new string[50];
                string[] stage1 = new string[50];
                int[] partnum = new int[50];

                while (readInfo.Read())
                {
                    seeParts[indexs] = readInfo["Parts"].ToString();
                    //  MessageBox.Show("These are the old parts" + seeParts[indexs]);
                    Global.seePart = seeParts[0];
                    //   MessageBox.Show("" + Global.seePart);
                    //   MessageBox.Show("," + Global.seePart);
                    Global.seeSA = readInfo["SubAssemblies"].ToString();
                   
                    seeSAName[indexs] = readInfo["SubAssembly"].ToString();
                    // if (Global.seePart == null) decide = false;
                    //if (Global.seePart == null) { break;  }
                    indexs++;
                }
                
                int[] partNumber = new int[50];
                int index = 1;
                for (int getPart = 1; getPart < Global.MaxPart; getPart++)
                {
                    partNumber[getPart] = index; index++;
                }
                index = 1; power.Close();
                //constraint does not read all parts onlly the parts int he sub assembly table this may cause complications later.      

                if (Global.seePart == null) decide = false;

                //MessageBox.Show("his is see part" + Global.seePart);
                string tempone = dFirst;
                string temptwo = dLast;
                int tempcount = 0; int counter = 0;
                int tempp = Convert.ToInt32(tempone);
                int temppt = Convert.ToInt32(temptwo);


                for (tempcount = tempp; tempcount < temppt; tempcount++)
                {
                    counter++;
                }
                int placeholder1 = 0; Boolean twostep1 = false;
                bool SAExists = false;
                Boolean twostep2 = false; int placeholder2 = 0;
                int rownumber = 0; int columnnumber = 0;
                SqlConnection connecion = new SqlConnection();
                SqlParameter paramba = new SqlParameter();
                int[] Part = new int[100];
                for (int i = 1; i < Global.PartNumber; i++)
                {
                    Part[i] = i;
                }
                string[] stage2 = new string[20];
                if (tempone != null)
                {
                    for (int counts = 0; counts <= 2; counts++)
                    {
                        if (tempone == seeSAName[counts])
                        {
                            //  MessageBox.Show("" + tempone);
                            twostep1 = true;
                            placeholder1 = counts;
                            //  MessageBox.Show(" " + placeholder1);
                        }
                        if (temptwo == seeSAName[counts])
                        {
                            //MessageBox.Show("" + temptwo);
                            twostep2 = true; placeholder2 = counts;
                            //  MessageBox.Show("this is p2");

                        }

                        if ((twostep1) & (twostep2))
                        {
                            //MessageBox.Show("You have enter valid subassembly values creating table now");
                            placeholder1 = Global.seedFirst;
                            placeholder2 = Global.seedLast;
                            SAExists = true;
                            Global.StageCounter = 2;
                        }
                        if (temptwo == null)
                        {
                            if (tempone == seeSAName[counts])
                            {
                                MessageBox.Show("Enter Valid Syntax");
                                enter = false;
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Enter a sub assembly name unless this is a stand alone stage");
                }

                //int[] seeSecondOld = new int[50];
               // MessageBox.Show("this is see parts" + seeParts[0]);
                string holder = seeParts.ToString();
                bool activated = false;
                if (decide == true)
                {
                    int compareOld = 0; int comparez2 = 0;
                    string holding = seeParts[0];
                    if (holding != null) 
                    {
                        //     MessageBox.Show("this is the holding" + holding);
                        //MessageBox.Show("holdings" + holding);
                        string[] numbers = holding.Split(',');
                        string[] tey = numbers;
                        //tey = String.Join("", seeParts);

                        for (int z = 0; z < tey.Length; z++)
                        {
                            //MessageBox.Show("" + tey);
                        }
                        // IList<string> seePk = holder.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);


                        //tey = holding.Split(',');
                        //  tey = seePk.ToArray();
                        //MessageBox.Show(seePk[0]);

                        // tey = seePk.ToArray();
                        //  MessageBox.Show("this is a test for your dumbass" + tey[0]);
                        //MessageBox.Show("This  is teyyy"+ tey[0] + tey[1]);
                        // string tey = seeParts.ToString();

                        Global.GetTruth = true;
                        int[] array = new int[tey.Length];
                        for (int i = 0; i < tey.Length; i++)
                        {
                            //f(tey[i] != ",")
                            array[i] = Convert.ToInt32(tey[i]);
                            // MessageBox.Show("This is array" + array[i]);
                        }
                        //int lowestOld = array.Max();
                        //string[] strarray = new string[] { tey };
                        //int[] comp = new int[3];
                        //  MessageBox.Show("this is strarray" + strarray[0]);
                        List<int> comp = new List<int>();
                        //   comp =  StringToIntList(tey).ToList();
                        int[] holdme = new int[50];
                        //   MessageBox.Show(""+comp[0]);
                        // List<int> comp = strarray.OfType<int>().ToList();
                        // /./comp = StringToIntList(tey).ToList();


                        int lowestOld = array.Where(f => f > 0).Min();
                        int highestOld = array.Max(); //MessageBox.Show("lowest" + lowestOld + "highest" + highestOld);
                        seeFirstOld[0] = lowestOld;
                        seeSOLD[0] = highestOld;
                        if (highestOld != 0) activated = true; bool biactive = false;
                        // comp.ForEach(Console.WriteLine);
                        //comp = Array.ConvertAll(tey.Split(','), Convert.ToInt32);
                        /*
                        for (int z = 0; z <= 50; z++)
                            {
                                if (array[z]== lowestOld)
                                {
                                    compareOld = array[z]; seeFirstOld[0] = compareOld;
                                MessageBox.Show("see fo"+ seeFirstOld[0]);
                                //MessageBox.Show("DT seeFirst O " + Global.seeFirst + " OLd" + Global.seeFirstO[z]);
                                }
                                if (array[z] == highestOld)
                                {
                                    comparez2 = array[z]; seeSOLD[0] = comparez2; if (comparez2 == null) { activated = true; break; }
                               //      MessageBox.Show("seeSeconnot =" + Global.seeSecondO[z]);
                                        test += 1;  MessageBox.Show("see ho" + seeSOLD[0]);
                                }

                                {
                                    //  MessageBox.Show("ENTER CORRECT SYNTAX");

                                    break;
                                }
                            }
                            bool biactive = false;
                            */

                        //  int counter = 0;

                        for (int temp = 0; temp < 2; temp++)
                        {
                            stage1[temp] = ("SA" + Convert.ToInt32(head));
                            partnum[temp] = temp;
                            index++;
                            // MessageBox.Show("lmao" + stage1[temp]);
                        }
                        int next_value = Convert.ToInt32(head) + 1;
                        Global.Head = Convert.ToInt32(head);
                        insert(stage1[0], partnum, SAExists, next_value);

                        //MessageBox.Show("this is stage1" + stage1);
                        if (activated)
                        {
                            counter++;
                            //);
                            //   MessageBox.Show("this is seeFirstOld" + seeFirstOlded);
                            for (int counts = 0; counts < 20; counts++)
                            {
                                //  int seeSecondOlded = Convert.ToInt32(Global.seeSecondO);
                                int seeFirsted = Convert.ToInt32(Global.seeFirst);
                                // MessageBox.Show("see first old" + Global.seeFirstO[count] + "see secondold " + Global.seeSecondO[count] + " see First" + seeFirsted);


                                if (Convert.ToInt32(seeSOLD[0]) == 0)
                                {
                                    biactive = true;
                                    if (seeFirsted == seeFirstOld[0]) ;
                                    {
                                        MessageBox.Show("Do not enter repeating values");
                                        enter = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    //see secondold is not null so we compare seesecondone->two with our seeFirst 
                                    int counted = 0;
                                    for (int temp = Convert.ToInt32(seeFirstOld[0]); temp < Convert.ToInt32(seeSOLD[0]); temp++)
                                    {
                                        counted++;
                                    }
                                    int holded = Convert.ToInt32(seeFirstOld[0]);
                                    for (int i = 0; i < counted; i++)
                                    {

                                        if (seeFirsted == holded)
                                        {
                                            MessageBox.Show("Enter no duplicate Values");
                                            enter = false;
                                            break;
                                        }
                                        holded++;
                                    }

                                }
                            }
                        }
                        // MessageBox.Show("This is seeFirstOld" + seeFirstOld[0] + "This is seeSecondOld" + seeSOLD[0]);
                        // this is where i am getting the ertror the reasonf or this is because it is fillind the range array with empty values which compares it 
                        //  MessageBox.Show("This is seeFirst old" + Global.seeFirstO.ToString());
                        int seeFirstOldz = seeFirstOld[0];
                        //  MessageBox.Show("this is seeFirstOld" + seeFirstOld[0]);
                        int seeSecondOld = seeSOLD[0];
                        //  MessageBox.Show("this is sesecondtOld" + seeSOLD[0]);
                        int seeFirst = Convert.ToInt32(Global.seeFirst);
                        int seeSecond = Convert.ToInt32(Global.seeSecond);
                        int count = 0; if (seeSecondOld == 0) seeSecondOld = 1;
                        if (seeSOLD[0] == 0) { seeSOLD[0] = seeFirstOld[0]; }
                        for (int temps = seeFirstOld[0]; temps <= seeSOLD[0]; temps++)
                        {
                            count++;   // finds ammount of elements in between two elemnts 

                            if ((seeFirst == temps || seeSecond == Convert.ToInt32(seeSOLD[0])) && seeSOLD[0] != 0)
                            {
                                enter = false; MessageBox.Show("Enter valid syntax"); break;
                            }
                            if (seeSecond == 0)
                            {
                                if (seeFirst == seeFirstOld[0])
                                {
                                    enter = false; //MessageBox.Show("seeSecond was = to 0"); break;
                                }
                            }
                            seeFirst = seeFirst + 1; seeSecond = seeSecond + 1;
                        }
                        //MessageBox.Show("this is count" + count);
                        int[] range = new int[count + 1]; int dummy = seeFirstOld[0];
                        for (int temp = 0; temp <= count; temp++) // gets the range of the seeFirstOld value  using count 
                        {
                            range[temp] = dummy;
                            // seeFirstOld++;
                            // MessageBox.Show("see first old"+dummy);
                            dummy++;
                        }           //int[] compare = new int[10];
                        int coun = 0;
                        // MessageBox.Show("this is " + count);
                        for (int j = Convert.ToInt32(Global.seeFirst); j <= Convert.ToInt32(Global.seeSecond); j++)
                        {
                            coun++;
                            if (Convert.ToInt32(Global.seeSecond) == 0)
                            {
                                coun = 1; break;
                            }
                        }
                        //MessageBox.Show("this is coun" + coun);
                        int[] compare = new int[coun + 1];   //MessageBox.Show("this is coun " + coun);
                                                             //M//essageBox.Show("this is see first & second" + seeFirst + " " + seeSecond); // this is see first and see second print out 
                        for (int temps = 0; temps < coun; temps++) // grabs the distance between seeFirst and seeSecond and checks store it in array compare 
                        {
                            compare[temps] = seeFirst;
                            seeFirst++;
                            //MessageBox.Show("this is seefirst" + seeFirst);
                            //    compare[temps] = Global.PartsContainer[temps];
                        }
                        int comparez = 0;

                        foreach (int numberA in range)
                        {
                            foreach (int numberB in compare)
                            {
                                //MessageBox.Show("rthis is enter a and b " + numberA + "" + numberB);
                                if (numberA == numberB)
                                {
                                    enter = false;
                                    MessageBox.Show("Enter valid syntax this is the loop ");
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    stage1[0] = "SA1";
                    insert(stage1[0], partnum, SAExists, 2);

                }
            }
                if (enter)
                {
                    SqlConnection conn = new SqlConnection(@"Data Source=SQL;Initial Catalog=JQNew;MultipleActiveResultSets=true;Integrated Security=True"); conn.Open();
                    SqlCommand comm = new SqlCommand();
                    int[] PartsHolder = new int[Global.PartsContainer.Length];
                    PartsHolder = Global.PartsContainer;
                    SqlParameter param = new SqlParameter();
                    Global.Procedure = text;
                    Global.Comments = text2;
                    comm.Parameters.AddWithValue("@JobNumz", Global.JobNumber);
                    comm.Parameters.AddWithValue("@ItemNum", Global.ItemNumber); comm.Parameters.AddWithValue("@Prock", Global.Procedure);
                    comm.Parameters.AddWithValue("@Comments", Global.Comments);
                    comm.Parameters.AddWithValue("@SubAssembly", head);
                    comm.Parameters.AddWithValue("@SubAssemblies", subassembly);
                    comm.Parameters.AddWithValue("@Parts", PartsHolder);


                    comm.Parameters.AddWithValue("@Operation", jest);
                 //   MessageBox.Show("Global Proceudre" + Global.Procedure + "Global comments" + Global.Comments);
                    for (int d = 0; d < PartsHolder.Length; d++)
                    {
                        string StrQuery = "UPDATE subassembly  SET  [Procedure]= @Prock, [Comments] = @Comments,[SubAssembly]=@SubAssembly,[SubAssemblies] = @SubAssemblies, [Operation] = @Operation WHERE [jobnum] = @JobNumz AND [itemnum] = @ItemNum AND [Parts] = @Parts";

                        comm.Connection = conn;

                        using (SqlCommand commm = new SqlCommand(StrQuery))
                        {
                            // inserting in the job number the colum and the mark number 

                            comm.Parameters["@Parts"].Value = PartsHolder[d];
                            comm.Connection = conn;
                            comm.CommandText = StrQuery;
                            comm.ExecuteNonQuery();
                        }

                    }
                    conn.Close();

                    Global.Truth = true;

                }

                enter = false;
            
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                    // Display a MsgBox asking the user to save changes or abort.
                    if (MessageBox.Show("Do you want to save changes to your text?", "My Application",
                       MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                MessageBox.Show("This has not been implemented yet.");
                        // Cancel the Closing event from closing the form.
                        e.Cancel = true;
                        // Call method to save file...
                    }        }
    }
    }
