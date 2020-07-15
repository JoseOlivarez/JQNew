using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Global
    {
        private static string v_Variable = "";
        public static string JobNumber
        {
            get { return v_Variable; }
            set
            {
                v_Variable = value;
            }

        }
        private static string v_Variable2 = "";
        public static string Variable2
        {
            get { return v_Variable2; }
            set
            {
                v_Variable2 = value;
            }
        }

        private static string v_Variable3 = "";
        public static string Variable3
        {
            get { return v_Variable3; }
            set
            {
                v_Variable3 = value;
            }
        }
        private static string vBuyer = "";
        public static string Buyer
        {
            get { return vBuyer; }
            set
            {
                vBuyer = value;
            }
        }
        private static string vPart = "";
        public static string MarkNumber
        {
            get { return vPart; }
            set
            {
                vPart = value;
            }
        }
        private static string vPTPTag = "";
        public static string PTPTag
        {
            get { return vPTPTag; }
            set
            {
                vPTPTag = value;
            }
        }
        private static string vClientTag = "";
        public static string ClientTag
        {
            get { return vClientTag; }
            set
            {
                vClientTag = value;
            }
        }
        private static int vPartNumber;
        public static int PartNumber
        {
            get { return vPartNumber; }
            set
            {
                vPartNumber = value;
            }
        }
        private static int vPartEntered;
        public static int PartEntered
        {
            get { return vPartEntered; }
            set
            {
                vPartEntered = value;
            }
        }
        private static int vMaxPart;
        public static int MaxPart
        {
            get { return vMaxPart; }
            set
            {
                vMaxPart = value;
            }
        }
        private static int vItemNumber;
        public static int ItemNumber
        {
            get { return vItemNumber; }
            set
            {
                vItemNumber = value;
            }
        }
        private static string vAccessNumber;
        public static string AccessNumber
        {
            get { return vAccessNumber; }
            set
            {
                vAccessNumber = value;
            }
        }
    }
 
    }

