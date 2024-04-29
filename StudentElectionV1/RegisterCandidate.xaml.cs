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
    /// <summary>
    /// Interaction logic for RegisterCandidate.xaml
    /// </summary>
    public partial class RegisterCandidate : Window
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

        public RegisterCandidate()
        {
            InitializeComponent();
            Connect2DB();
            showData();
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Hide();
            main.Show();
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
            database = "sql6702798";
            username = "sql6702798";
            password = "tnhxSdnahe";
            mySQLConnection();
        }
        
        private void showData()
        {
            DataTable dataTable = new DataTable();
            sql = "SELECT * FROM CANDIDATES ORDER BY LASTNAME,FIRSTNAME";
            using (MySqlDataAdapter da = new MySqlDataAdapter(sql, conn))
            {
                da.Fill(dt);
            }
            candidatesList.ItemsSource = dt.DefaultView;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
