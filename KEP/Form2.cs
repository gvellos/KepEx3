using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KEP
{
    public partial class Form2 : Form
    {
        String connectionString = "Data source=csharp2022_2.db;Version=3";
        SQLiteConnection connection;
        Requests requestHandler = new Requests();
        String tempRequest;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(UserKEP name)
        {
            InitializeComponent();
            label2.Text = name.Name;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            String createSQL = "Create table if not exists KEPCenter(KEP integer primary key autoincrement," +
                "Name Text,Email Text,Number Int,BirthDay Text,RequestType Text,Adress Text,RequestTime Text)";
            SQLiteCommand command = new SQLiteCommand(createSQL, connection);
            command.ExecuteNonQuery();
            connection.Close();

        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            connection.Open();
            String insertSQL = "Insert into KEPCenter(Name,Email,Number,BirthDay,RequestType,Adress,RequestTime) values(@name,@email,@number,@birthday,@requestType,@adress,@requestTime)";
            SQLiteCommand command = new SQLiteCommand(insertSQL, connection);
            command.Parameters.AddWithValue("name", textBoxName.Text);
            command.Parameters.AddWithValue("email", textBoxEmail.Text);
            command.Parameters.AddWithValue("number", Int64.Parse(textBoxPhone.Text));
            command.Parameters.AddWithValue("birthday", textBoxBirtDay.Text);
            command.Parameters.AddWithValue("requestType", textBoxRequestType.Text);
            command.Parameters.AddWithValue("adress", textBoxAdress.Text);
            command.Parameters.AddWithValue("requestTime", textBoxRequestTime.Text);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            connection.Open();
            String selectSQL = "Select * from KEPCenter";
            SQLiteCommand command = new SQLiteCommand(selectSQL, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            richTextBox1.Clear();
            while (reader.Read())
            {               
                richTextBox1.AppendText("Name: " + reader.GetString(1) + "  ");             
                richTextBox1.AppendText("RequestType: " + reader.GetString(5) + "\n");
                tempRequest = requestHandler.HandleRequest(reader.GetString(5));
            }
            while (reader.Read())
            {              
                richTextBox1.AppendText("All the RequestTypes: " + reader.GetString(5) + "\n");
                tempRequest = requestHandler.HandleRequest(reader.GetString(5));
            }
            connection.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            connection.Open();
            String deleteSQL = "Delete from KEPCenter where Name=@name";
            SQLiteCommand command = new SQLiteCommand(deleteSQL, connection);
            command.Parameters.AddWithValue("name", textBoxName.Text);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void buttonModification_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            connection.Open();
            String updateSQL = "Update KEPCenter set Email=@email, Number=@number, BirthDay=@birthday, RequestType=@requestType, Adress=@adress, RequestTime=@requestTime where Name=@name";
            SQLiteCommand command = new SQLiteCommand(updateSQL, connection);
            command.Parameters.AddWithValue("email", textBoxEmail.Text);
            command.Parameters.AddWithValue("number", Int64.Parse(textBoxPhone.Text));
            command.Parameters.AddWithValue("birthday", textBoxBirtDay.Text);
            command.Parameters.AddWithValue("requestType", textBoxRequestType.Text);
            command.Parameters.AddWithValue("adress", textBoxAdress.Text);
            command.Parameters.AddWithValue("requestTime", textBoxRequestTime.Text);
            command.Parameters.AddWithValue("name", textBoxName.Text);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void buttonAdvancedSearch_Click(object sender, EventArgs e)
        {
            String selectSQL = "Select * from KEPCenter where RequestTime between @startDate AND @endDate";
            SQLiteCommand command = new SQLiteCommand(selectSQL, connection);
            command.Parameters.AddWithValue("@startDate", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@endDate", dateTimePicker2.Value);
            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            richTextBox1.Clear();
            while (reader.Read())
            {
                richTextBox1.AppendText("Name: " + reader.GetString(1) + "\n");
                richTextBox1.AppendText("Email: " + reader.GetString(2) + "\n");
                richTextBox1.AppendText("Number: " + reader.GetInt32(3) + "\n");
                richTextBox1.AppendText("BirthDay: " + reader.GetString(4) + "\n");
                richTextBox1.AppendText("RequestType: " + reader.GetString(5) + "\n");
                richTextBox1.AppendText("Adress: " + reader.GetString(6) + "\n");
                richTextBox1.AppendText("RequestTime: " + reader.GetString(7) + "\n\n");
                tempRequest = requestHandler.HandleRequest(reader.GetString(5));
            }

            reader.Close();
            connection.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if(tempRequest != null)
            {
                this.Hide();
                Form3 form3 = new Form3(tempRequest);
                form3.Show();
                form3.StartPosition = FormStartPosition.Manual;
                form3.Location = this.Location;
                form3.Size = this.Size;
            }
        }
    }
}
