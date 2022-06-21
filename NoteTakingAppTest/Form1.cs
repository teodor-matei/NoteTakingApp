using System.Data;
using System.Data.SqlClient;

namespace NoteTakingAppTest
{
    public partial class Form1 : Form
    {
        DataTable notes;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            String connectionString = "Data Source=DESKTOP-464DU9K;Initial Catalog=POO;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Notes",connection);

            notes = new DataTable();
            adapter.Fill(notes);

            notes.Columns[0].ColumnName = "ID";
            notes.Columns[1].ColumnName = "Title";
            notes.Columns[2].ColumnName = "Content";
            notes.Columns[3].ColumnName = "Date";

            /*
            notes = new DataTable();
            notes.Columns.Add("Title", typeof(String));
            notes.Columns.Add("Content", typeof(String));
            notes.Columns.Add("Date", typeof(String));
            */

            dataGridView1.DataSource = notes;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Content"].Visible = false;
            dataGridView1.Columns["Title"].Width = 160;
            dataGridView1.Columns["Date"].Width = 63;

            connection.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textTitle.Clear();
            textContent.Clear();
            dataGridView1.ClearSelection();
            textTitle.Enabled = true;
            textContent.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textTitle.Text) && !String.IsNullOrEmpty(textContent.Text))
            {
                String connectionString = "Data Source=DESKTOP-464DU9K;Initial Catalog=POO;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                String date = DateTime.Now.ToShortDateString();
                //notes.Rows.Add(textTitle.Text, textContent.Text, date);

                SqlCommand command = new SqlCommand("INSERT INTO Notes (Title,Data,Date) VALUES (@Title,@Content,@Date)", connection);
                command.Parameters.AddWithValue("@Title", textTitle.Text);
                command.Parameters.AddWithValue("@Content", textContent.Text);
                command.Parameters.AddWithValue("Date", date);
                command.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Notes", connection);
                notes.Clear();
                adapter.Fill(notes);
                dataGridView1.DataSource = notes;

                connection.Close();

                textTitle.Clear();
                textContent.Clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            if (index > -1)
            {
                textTitle.Text = notes.Rows[index].ItemArray[1].ToString();
                textContent.Text = notes.Rows[index].ItemArray[2].ToString();
                textTitle.Enabled = false;
                textContent.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            String connectionString = "Data Source=DESKTOP-464DU9K;Initial Catalog=POO;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE Notes WHERE ID=@ID", connection);
            command.Parameters.AddWithValue("@ID", notes.Rows[index].ItemArray[0]);
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Notes", connection);
            notes.Clear();
            adapter.Fill(notes);
            dataGridView1.DataSource = notes;

            connection.Close();

            //notes.Rows[index].Delete();
        }
    }
}