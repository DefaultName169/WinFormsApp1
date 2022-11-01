using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection con_eoffice = new SqlConnection("Data Source= DUONGHVB; Initial Catalog= EO250213V586; Integrated Security=True");
        SqlConnection con_htlt = new SqlConnection("Data Source= DUONGHVB; Initial Catalog= htlt_tinh; Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                con_eoffice.Open();
                SqlCommand sc = new SqlCommand("SELECT TABLE_NAME as table_name FROM [eO250213V586].INFORMATION_SCHEMA.TABLES order by TABLE_NAME", con_eoffice);
                SqlDataReader reader;
                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("table_name", typeof(string));
                dt.Load(reader);
                select_table_eoffice.ValueMember = "table_name";
                select_table_eoffice.DataSource = dt;


                con_htlt.Open();
                SqlCommand sc2 = new SqlCommand("SELECT TABLE_NAME as table_name_2 FROM [htlt_tinh].INFORMATION_SCHEMA.TABLES order by TABLE_NAME", con_htlt);
                SqlDataReader reader2;
                reader2 = sc2.ExecuteReader();
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("table_name_2", typeof(string));
                dt2.Load(reader2);
                select_table_htlt.ValueMember = "table_name_2";
                select_table_htlt.DataSource = dt2;
                //con.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }


        private void selectcolumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand sc = new SqlCommand("SELECT COLUMN_NAME as selected FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + select_table_eoffice.Text + "' ORDER BY ORDINAL_POSITION ", con_eoffice);
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("selected", typeof(string));
            dt.Load(reader);
            comboBox2.ValueMember = "selected";
            comboBox2.DataSource = dt;
        }

        private void select_table_htlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand sc = new SqlCommand("SELECT COLUMN_NAME as selected FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + select_table_htlt.Text + "' ORDER BY ORDINAL_POSITION ", con_htlt);
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("selected", typeof(string));
            dt.Load(reader);
            comboBox3.ValueMember = "selected";
            comboBox3.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tab2.
        }
    }
}