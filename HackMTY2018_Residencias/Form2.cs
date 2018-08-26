using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HackMTY2018_Residencias
{
    public partial class Form2 : Form
    {
        private Form1 _parent;
        private Form3 _f3;
        private string[] datavec;

        public Form2(string[] datavec, ref Form1 parent)
        {
            InitializeComponent();
            CenterToScreen();
            this.datavec = datavec;
            _parent = parent;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            string[] head = { "ID de vacante", "Titulo", "Carrera", "Empresa", "Ubicacion" };
            for (int i = 0; i < head.Length; i++)
                dataGridView1.Columns.Add(head[i], head[i]);
            LoadDB("");
            button1.Enabled = dataGridView1.Rows.Count > 0;
        }

        private void LoadDB(string str)
        {
            try
            {
                dataGridView1.Rows.Clear();
                SqlConnection con = new SqlConnection("Server=tcp:hackmty18.database.windows.net, 1433;Initial Catalog=residencias;Persist Security Info=False;User ID = team7;Password = ZXC741asd852qwe963;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate = False;Connection Timeout=30;");
                con.Open();
                string send = "SELECT vacante.vac_id, vacante.vac_titulo, vacante.vac_carrera, empresa.emp_nombre, vacante.vac_ubicacion FROM vacante JOIN empresa on (vacante.emp_id = empresa.emp_id) WHERE ";
                send += radioButton2.Checked ? ("vacante.vac_carrera like '" + str + "%'") : ("empresa.emp_nombre like '" + str + "%'");
                SqlDataReader read = new SqlCommand(send, con).ExecuteReader();
                while(read.Read())
                {
                    string[] vec = new string[read.FieldCount];
                    for (int i = 0; i < read.FieldCount; i++)
                        vec[i] = read.GetValue(i).ToString();
                    dataGridView1.Rows.Add(vec);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenInfo()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (_f3 == null || _f3.IsDisposed)
                {
                    _f3 = new Form3(Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value), datavec);
                    _f3.Show();
                }
            }
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Width = this.Width - 41;
            this.dataGridView1.Height = this.Height - 119;
            this.button1.Left = this.Width - 128;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenInfo();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parent.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadDB(textBox1.Text);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenInfo();
        }
    }
}
