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
    public partial class Form3 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        //string connectionString = "Server=server46;Database=ValiullinDR;User Id=stud;Password=stud;";
        string connectionString = "Data Source=HOME-PC;Initial Catalog=ValiullinDR;Integrated Security=true;";
        string sql = "SELECT * FROM dbo.Staff ORDER BY Id";


        public Form3()
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
                dataGridView1.Columns["Lastname"].HeaderText = "Фамилия";
                dataGridView1.Columns["Firstname"].HeaderText = "Имя";
                dataGridView1.Columns["Position"].HeaderText = "Должность";
                dataGridView1.Columns["PhoneNumber"].HeaderText = "Телефон";
                dataGridView1.Columns["Status"].HeaderText = "Состояние";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
        }

        
        

        private void button8_Click(object sender, EventArgs e)
        {
            Form f1 = new Form2();
            f1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) // add
        {
            DataRow row = ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds.Tables[0].Rows.Add(row);

        }

        private void button3_Click(object sender, EventArgs e) // save
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
                        MessageBox.Show("Введите фамилию, а не просто цифры!!!");
                        row.Cells[1].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[1].Style.BackColor = Color.White;
                        // Проверка, есть ли в фамилии цифры
                        for (int k = 0; k < row.Cells[1].Value.ToString().Length; k++)
                        {
                            if (row.Cells[1].Value.ToString()[k] >= '0' && row.Cells[1].Value.ToString()[k] <= '9')
                            {
                                MessageBox.Show("В фамилии не может быть цифр!");
                                row.Cells[1].Style.BackColor = Color.Red;
                                return;
                            }
                        }
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[2].Value);
                        MessageBox.Show("Введите имя, а не просто цифры!!!");
                        row.Cells[2].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[2].Style.BackColor = Color.White;
                        // Проверка, есть ли в имени цифры
                        for (int k = 0; k < row.Cells[2].Value.ToString().Length; k++)
                        {
                            if (row.Cells[2].Value.ToString()[k] >= '0' && row.Cells[2].Value.ToString()[k] <= '9')
                            {
                                MessageBox.Show("В имени не может быть цифр!");
                                row.Cells[2].Style.BackColor = Color.Red;
                                return;

                            }
                        }
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[3].Value);
                        MessageBox.Show("Введите должность, а не просто цифры!!!");
                        row.Cells[3].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[3].Style.BackColor = Color.White;
                        // Проверка, есть ли в должности цифры
                        for (int k = 0; k < row.Cells[3].Value.ToString().Length; k++)
                        {
                            if (row.Cells[3].Value.ToString()[k] >= '0' && row.Cells[3].Value.ToString()[k] <= '9')
                            {
                                MessageBox.Show("В должности не может быть цифр!");
                                row.Cells[3].Style.BackColor = Color.Red;
                                return;

                            }
                        }
                    }
                    // Проверка на правильность номера
                    try
                    {
                        if (Convert.ToInt32(row.Cells[4].Value.ToString().Replace("+", "").Length) == 11)
                        {
                            row.Cells[5].Style.BackColor = Color.White;

                        }
                        else
                        {
                            MessageBox.Show("Введите телефонный номер, состоящий из 11 цифр!!!");
                            row.Cells[4].Style.BackColor = Color.Red;
                            return;
                        }


                    }
                    catch
                    {
                        MessageBox.Show("Введите корректный номер, без лишних символов!!!");
                        row.Cells[4].Style.BackColor = Color.Red;
                        return;
                    }
                    // Если в номере нету плюса, программа добавит
                    if (row.Cells[4].Value.ToString().IndexOf("+") == -1)
                    {
                        row.Cells[4].Value = row.Cells[5].Value.ToString().Insert(0, "+");
                    }
                    // Проверка на то, содержит ли в себе ячейка только цифры
                    try
                    {
                        Convert.ToInt32(row.Cells[5].Value);
                        MessageBox.Show("Введите состояние, а не просто цифры!!!");
                        row.Cells[5].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[5].Style.BackColor = Color.White;
                        // Проверка, есть ли в состоянии цифры
                        for (int k = 0; k < row.Cells[5].Value.ToString().Length; k++)
                        {
                            if (row.Cells[5].Value.ToString()[k] >= '0' && row.Cells[5].Value.ToString()[k] <= '9')
                            {
                                MessageBox.Show("В состоянии не может быть цифр!");
                                row.Cells[5].Style.BackColor = Color.Red;
                                return;

                            }
                        }
                    }
                }
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql, connection);
                    commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = new SqlCommand("sp_CreateStaff", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@lastname", SqlDbType.NVarChar, 50, "LastName"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@firstname", SqlDbType.NVarChar, 50, "FirstName"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@position", SqlDbType.NVarChar, 50, "Position"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@phonenumber", SqlDbType.NVarChar, 50, "PhoneNumber"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@status", SqlDbType.NVarChar, 50, "Status"));

                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    adapter.Update(ds);
                }
                MessageBox.Show("Сохранение успешно выполнено.");
            }
            catch
            {
                MessageBox.Show("Вы не можете удалить данную строку, так как она используется в других таблицах!!!");
            }


        }

        private void button4_Click(object sender, EventArgs e) // delete
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
                label2.Visible = false;
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Получаем первую выбранную строку
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    string firstCellValue = selectedRow.Cells[1].Value.ToString();
                    string secondCellValue = selectedRow.Cells[2].Value.ToString();

                    string modifiedSecondCellValue = secondCellValue.Substring(0, 1) + ".";

                    // Формируем новую строку для отображения в label2
                    string result = $"{firstCellValue} {modifiedSecondCellValue}";

                    // Присваиваем новое значение в label2
                    label2.Text = result;
                    // Получаем значение из первой ячейки у выбранной строки и присваиваем его в label
                    label2.Visible = true;
                }

            }
            if ((dataGridView1.SelectedRows[0].Cells[3].Value as string) == "Официант")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.ofic;
            }
            if ((dataGridView1.SelectedRows[0].Cells[3].Value as string) == "Повар" || (dataGridView1.SelectedRows[0].Cells[3].Value as string) == "Шеф-повар")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.povar;
            }
            if ((dataGridView1.SelectedRows[0].Cells[3].Value as string) == "Уборщик")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.clean;
            }
            if ((dataGridView1.SelectedRows[0].Cells[3].Value as string) == "Менеджер")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.men;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Создание нового формы
            Form inputForm = new Form();
            inputForm.Text = "Ввод суммы штрафа";
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.Height = 150;

            // Создание текстового поля
            TextBox textBox = new TextBox();
            textBox.Location = new Point(40, 20);
            textBox.Size = new Size(200, 20);
            textBox.MaxLength = 5; // Ограничиваем количество символов до 5, чтобы пользователь мог ввести максимум 50000
            inputForm.Controls.Add(textBox);

            // Создание кнопки подтверждения
            Button confirmButton = new Button();
            confirmButton.Text = "Подтвердить";
            confirmButton.Location = new Point(40, 50);
            confirmButton.Size = new Size(200, 23);
            confirmButton.Click += (s, args) =>
            {
                if (int.TryParse(textBox.Text, out int amount) && amount <= 50000)
                {
                    // Здесь можно обработать введенное значение
                    MessageBox.Show($"Выписан штраф на сумму: {amount}");
                    inputForm.Close();
                }
                else
                {
                    MessageBox.Show("Введите корректное значение от 0 до 50000.");
                }
            };
            inputForm.Controls.Add(confirmButton);

            // Показ формы
            inputForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Создание нового формы
            Form inputForm = new Form();
            inputForm.Text = "Ввод суммы премии";
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.Height = 150;

            // Создание текстового поля
            TextBox textBox = new TextBox();
            textBox.Location = new Point(40, 20);
            textBox.Size = new Size(200, 20);
            textBox.MaxLength = 5; // Ограничиваем количество символов до 5, чтобы пользователь мог ввести максимум 50000
            inputForm.Controls.Add(textBox);

            // Создание кнопки подтверждения
            Button confirmButton = new Button();
            confirmButton.Text = "Подтвердить";
            confirmButton.Location = new Point(40, 50);
            confirmButton.Size = new Size(200, 23);
            confirmButton.Click += (s, args) =>
            {
                if (int.TryParse(textBox.Text, out int amount) && amount <= 50000)
                {
                    // Здесь можно обработать введенное значение
                    MessageBox.Show($"Выписана премия на сумму: {amount}");
                    inputForm.Close();
                }
                else
                {
                    MessageBox.Show("Введите корректное значение от 0 до 50000.");
                }
            };
            inputForm.Controls.Add(confirmButton);

            // Показ формы
            inputForm.ShowDialog();
        }
    }
}

