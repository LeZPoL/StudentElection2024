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
using MySql.Data.MySqlClient;

namespace StudentElectionV1
{

    public partial class RegisterVoter : Window
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

        public RegisterVoter()
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
            database = "sql6702798";
            username = "sql6702798";
            password = "tnhxSdnahe";
            mySQLConnection();
        }

        public void addVoter()
        {
            if ( txtFirstName.Text.Trim()=="" ||
                txtLastName.Text.Trim() == "" ||
                txtMiddleName.Text.Trim() == "" || 
                cmbCourse.Text.Trim()== "" ||
                cmbYearLevel.Text.Trim()=="") 
            {
                blueBorder.BorderBrush=Brushes.Red;
                blueBorder.BorderThickness = new Thickness(5);
                errorMessage.Visibility = Visibility.Visible;

            }
            else
            {
                blueBorder.BorderBrush = Brushes.Transparent;
                blueBorder.BorderThickness = new Thickness(0);
                errorMessage.Visibility = Visibility.Hidden;
                sql = "INSERT INTO VOTERS(LASTNAME, FIRSTNAME,MIDDLENAME,COURSE,YEARLEVEL) VALUES(@lastname,@firstname,@middlename,@course,@yearlevel)";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
                cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@middlename", txtMiddleName.Text);
                cmd.Parameters.AddWithValue("@course", cmbCourse.Text);
                cmd.Parameters.AddWithValue("@yearlevel", cmbYearLevel.Text);
                cursor = cmd.ExecuteReader();
                // at this point, we should be able to ADD data to our table (VOTERS)
                cursor.Close();

            }
            
        }

        private void showData() 
        {
            DataTable dt = new DataTable();
            sql = "SELECT * FROM VOTERS ORDER BY LASTNAME,FIRSTNAME,MIDDLENAME";
            using (MySqlDataAdapter da = new MySqlDataAdapter(sql, conn)) 
            {
                da.Fill(dt);
            }
            votersList.ItemsSource = dt.DefaultView;
        }

        public void editVoter() 
        {
            sql = "UPDATE VOTERS SET LASTNAME=@lastname, FIRSTNAME=@firstname, MIDDLENAME=@middlename, COURSE=@course, YEARLEVEL=@yearlevel WHERE ID=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
            cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@middlename", txtMiddleName.Text);
            cmd.Parameters.AddWithValue("@course", cmbCourse.Text);
            cmd.Parameters.AddWithValue("@yearlevel", cmbYearLevel.Text);
            cmd.Parameters.AddWithValue("@id",txtVoterID.Text);
            cursor = cmd.ExecuteReader();
            // at this point, we should be able to edit/update data to our table (VOTERS)
            cursor.Close();
        }

        public void deleteVoter() 
        {
            sql = "DELETE FROM VOTERS WHERE ID=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", txtVoterID.Text);
            cursor = cmd.ExecuteReader();
            cursor.Close();
        }

        public void findVoter() 
        {
        
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addVoter();
            showData();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
                   
            conn.Close();      // connection
            this.Hide();
            MainWindow bintana = new MainWindow();
            bintana.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            editVoter();
            showData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            deleteVoter();
            showData();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tempVal= txtSearch.Text.Trim();
            sql = "SELECT * FROM VOTERS WHERE LASTNAME LIKE '%"+tempVal+
                "%' OR FIRSTNAME LIKE '%"+tempVal+"%' OR MIDDLENAME LIKE '%"+
                tempVal+"%' OR COURSE LIKE '%"+tempVal+"%'";
            DataTable dt = new DataTable();
            using (MySqlDataAdapter da = new MySqlDataAdapter(sql, conn)) 
            {
                da.Fill(dt);                
            }            
            votersList.ItemsSource = dt.DefaultView;
        }
    }
    
}
