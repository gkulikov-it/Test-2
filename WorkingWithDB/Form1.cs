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

namespace WorkingWithDB
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //Если будете запускать у себя то необходимо прописать путь на базу данных
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g_kul\OneDrive\Рабочий стол\Тестовое задание №2 ТМК\WorkingWithDB\WorkingWithDB\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Products]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["Name"]) + "       " + Convert.ToString(sqlReader["Volume"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text) &&
                !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Products] (Name, Volume, State, Closed_volume, Brand, Diameter, Wall, Date_begin, Date_end)VALUES(@Name, @Volume, @State, @Closed_volume, @Brand, @Diameter, @Wall, @Date_begin, @Date_end)", sqlConnection);

                command.Parameters.AddWithValue("Name", textBox1.Text);
                command.Parameters.AddWithValue("Volume", textBox2.Text);
                command.Parameters.AddWithValue("State", textBox9.Text);
                command.Parameters.AddWithValue("Closed_volume", textBox7.Text);
                command.Parameters.AddWithValue("Brand", comboBox1.Text);
                command.Parameters.AddWithValue("Diameter", comboBox2.Text);
                command.Parameters.AddWithValue("Wall", comboBox3.Text);
                command.Parameters.AddWithValue("Date_begin", dateTimePicker1.Value);
                command.Parameters.AddWithValue("Date_end", dateTimePicker2.Value);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Visible = true;

                label7.Text = "Все поля должны быть заполнены!";
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Products]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["Name"]) + "       " + Convert.ToString(sqlReader["State"]) + "       " + Convert.ToString(sqlReader["Volume"]) + "       " + Convert.ToString(sqlReader["Closed_volume"]) + "       " + Convert.ToString(sqlReader["Brand"]) + "       " + Convert.ToString(sqlReader["Diameter"]) + "       " + Convert.ToString(sqlReader["Wall"]) + "       " + Convert.ToString(sqlReader["Date_begin"]) + "       " + Convert.ToString(sqlReader["Date_end"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
                label8.Visible = false;

            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Products] SET [Name]=@Name, [Volume]=@Volume, [State]=@State, [Closed_volume]=@Closed_volume, [Brand]=@Brand, [Diameter]=@Diameter, [Wall]=@Wall, [Date_begin]=@Date_begin, [Date_end]=@Date_end WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox5.Text);
                command.Parameters.AddWithValue("Name", textBox4.Text);
                command.Parameters.AddWithValue("Volume", textBox3.Text);
                command.Parameters.AddWithValue("State", textBox8.Text);
                command.Parameters.AddWithValue("Closed_volume", textBox10.Text);
                command.Parameters.AddWithValue("Brand", comboBox6.Text);
                command.Parameters.AddWithValue("Diameter", comboBox5.Text);
                command.Parameters.AddWithValue("Wall", comboBox4.Text);
                command.Parameters.AddWithValue("Date_begin", dateTimePicker4.Value);
                command.Parameters.AddWithValue("Date_end", dateTimePicker3.Value);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label8.Visible = true;

                label8.Text = "Id должнен быть заполнен!";
            }
            else
            {
                label8.Visible = true;

                label8.Text = "Все поля должны быть заполнены!";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
                label8.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Products] WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox6.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label8.Visible = true;

                label8.Text = "Id должнен быть заполнен!";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
