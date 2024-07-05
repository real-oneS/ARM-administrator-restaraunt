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

namespace WindowsFormsApp5
{
    public partial class Form5 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        //string connectionString = "Server=server46;Database=ValiullinDR;User Id=stud;Password=stud;";
        string connectionString = "Data Source=HOME-PC;Initial Catalog=ValiullinDR;Integrated Security=true;";
        string sql = "SELECT * FROM dbo.Menu ORDER BY Id";
        public Form5()
        {
            InitializeComponent();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["Name"].HeaderText = "Название блюда";
                dataGridView1.Columns["Components"].HeaderText = "Состав";
                dataGridView1.Columns["Price"].HeaderText = "Цена";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView1.Columns["Components"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        
        

        private void button8_Click_1(object sender, EventArgs e)
        {
            Form f1 = new Form2();
            f1.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds.Tables[0].Rows.Add(row);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    //Проверка на пустоту ячеек
                    if (row.Cells[i].Value == null || row.Cells[i].Value == DBNull.Value)
                    {
                        MessageBox.Show("Заполните пустые ячейки!!!");
                        row.Cells[i].Style.BackColor = Color.Red;
                        return;
                    }
                    else
                    {
                        row.Cells[i].Style.BackColor = Color.White;
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[1].Value);
                        MessageBox.Show("Введите названия блюда, а не просто цифры!!!");
                        row.Cells[1].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[1].Style.BackColor = Color.White;
                        
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[2].Value);
                        MessageBox.Show("Введите состав блюда, а не просто цифры!!!");
                        row.Cells[2].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[2].Style.BackColor = Color.White;
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        if (Convert.ToInt32(row.Cells[3].Value) > 5000)
                        {
                            MessageBox.Show("Стоимость блюда не может быть больше 5000!!!!!!");
                            row.Cells[3].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch
                    {
                        row.Cells[3].Style.BackColor = Color.White;
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateMenu", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@components", SqlDbType.NVarChar, 50, "Components"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@price", SqlDbType.Int, 0, "Price"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
            MessageBox.Show("Сохранение успешно выполнено.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }
        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
