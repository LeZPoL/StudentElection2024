using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentElectionV1
{   
    public partial class RegisterUser : Window
    {
        string username;
        string password;
        string database;
        string server;
        public MySqlConnection conn;
        public MySqlDataAdapter adapter;
        public MySqlDataReader cursor;
        public MySqlCommand cmd;
        public string sql = "";
        public DataTable dataTable;
        public DataSet dt;

        public RegisterUser()
        {
            InitializeComponent();  
            Connect2DB();
            showData();
        }

        private void mySQLConnection()
        {
            string connStr = $"server={server}; password={password};username={username}; database={database}";
            conn = new MySqlConnection(connStr);
            conn.Open();
        }
        public void Connect2DB()
        {
            // server = "localhost";
            // database = "honest";
            // username = "root";
            // password = "";
            server = "sql6.freesqldatabase.com";
            database = "sql6701485";
            username = "sql6701485";
            password = "qpw59Szx8A";
            mySQLConnection();
        }


        private void showData()
        {
            DataTable dt = new DataTable();
            sql = "SELECT * FROM USERS ORDER BY USERNAME";
            using (MySqlDataAdapter da = new MySqlDataAdapter(sql, conn))
            {
                da.Fill(dt);
            }
            userList.ItemsSource = dt.DefaultView;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow bintana = new MainWindow();
            bintana.Show(); 
        }

        private int Asc(char String)
        {
            int int32 = Convert.ToInt32(String);
            if (int32 < 128)
                return int32;
            try
            {
                Encoding fileIoEncoding = Encoding.Default;
                char[] chars = new char[1] { String };
                if (fileIoEncoding.IsSingleByte)
                {
                    byte[] bytes = new byte[1];
                    fileIoEncoding.GetBytes(chars, 0, 1, bytes, 0);
                    return (int)bytes[0];
                }
                byte[] bytes1 = new byte[2];
                if (fileIoEncoding.GetBytes(chars, 0, 1, bytes1, 0) == 1)
                    return (int)bytes1[0];
                if (BitConverter.IsLittleEndian)
                {
                    byte num = bytes1[0];
                    bytes1[0] = bytes1[1];
                    bytes1[1] = num;
                }
                return (int)BitConverter.ToInt16(bytes1, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CeasarCipher(string test)
        {
            string encoded = "";

            char[] highlet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' };
            char[] lowlet = { 'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N' };

            test = test.ToUpper();
            int el = test.Length;
            for (int i = 0; i < el; i++)
            {
                for (int k = 0; k < 13; k++)
                {
                    if (Asc(test[i]) > 64 && Asc(test[i]) < 78)
                    {
                        if (test[i] == highlet[k])
                        {
                            encoded = encoded + lowlet[k];
                            break;
                        }
                    }
                    else
                    {
                        if (Asc(test[i]) == 32)
                        {
                            encoded = encoded + " ";
                            break;
                        }
                        else
                        {
                            if (test[i] == lowlet[k])
                            {
                                encoded = encoded + highlet[k];
                                break;
                            }
                        } // == 32
                    }  // > 64 and < 78
                }  // for k
            } // for i

            return encoded;
        }
    }
}
