using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace YL_COMM.BizFrm
{
    public class ExcelDataBaseHelper
    {
        //»ùÇÃÆÄÀÏ 
        byte[] _SampleFile;

        public byte[] SampleFile
        {
            get
            {
                return _SampleFile;
            }
            set
            {
                if (_SampleFile == value)
                    return;
                _SampleFile = value;
            }
        }

        public static object OpenFile(string fileName)
        {
            //var fullFileName = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), fileName);
            var fullFileName = string.Format("{0}", fileName);
            if (!File.Exists(fullFileName))
            {
                System.Windows.Forms.MessageBox.Show("File not found");
                return null;
            }
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fullFileName);
            var adapter =  new OleDbDataAdapter("select * from [Sheet1$]", connectionString);
            var ds = new DataSet();
            string tableName = "excelData";
            adapter.Fill(ds, tableName);
            DataTable data = ds.Tables[tableName];
            return data;
        }

        public static object OpenFile2(string fileName)
        {
            var fullFileName = string.Format("{0}", fileName);

            if (!File.Exists(fullFileName))
            {
                System.Windows.Forms.MessageBox.Show("File not found");
                return null;
            }

            String name = "Sheet1"; //Name of your Sheet in the work book
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fullFileName +
                            ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            OleDbConnection Connection = new OleDbConnection(constr);
            OleDbCommand OleConnection = new OleDbCommand("SELECT * FROM [" + name + "$]", Connection);
            Connection.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(OleConnection);
            DataTable data = new DataTable();
            sda.Fill(data);
            return data;
        }

    }
}
