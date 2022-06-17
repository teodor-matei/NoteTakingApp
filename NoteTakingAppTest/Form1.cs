using System.Data;

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
            notes = new DataTable();
            notes.Columns.Add("Title", typeof(String));
            notes.Columns.Add("Content", typeof(String));
            notes.Columns.Add("Date", typeof(String));

            dataGridView1.DataSource = notes;
            dataGridView1.Columns["Content"].Visible = false;
            dataGridView1.Columns["Title"].Width = 160;
            dataGridView1.Columns["Date"].Width = 63;
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
                String date = DateTime.Now.ToShortDateString();
                notes.Rows.Add(textTitle.Text, textContent.Text, date);

                textTitle.Clear();
                textContent.Clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            if (index > -1)
            {
                textTitle.Text = notes.Rows[index].ItemArray[0].ToString();
                textContent.Text = notes.Rows[index].ItemArray[1].ToString();
                textTitle.Enabled = false;
                textContent.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            notes.Rows[index].Delete();
        }
    }
}