using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form7 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        //string connectionString = "Server=server46;Database=ValiullinDR;User Id=stud;Password=stud;";
        string connectionString = "Data Source=HOME-PC;Initial Catalog=ValiullinDR;Integrated Security=true;";
        public Form7()
        {
            InitializeComponent();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Обновленный запрос SQL для включения имени и фамилии сотрудника
                string sql = "SELECT SalaryRecord.Id, CONCAT(Staff.LastName, ' ', Staff.FirstName) AS FullName,Staff.Position AS Position, SalaryRecord.Salary, SalaryRecord.Bonus, SalaryRecord.LastPaymentDate FROM dbo.SalaryRecord INNER JOIN dbo.Staff ON SalaryRecord.StaffId = Staff.Id ORDER BY SalaryRecord.Id";

                adapter = new SqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                // Делаем недоступным столбец id для изменения
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["Position"].Visible = false;

                // Переименовываем столбцы
                dataGridView1.Columns["FullName"].HeaderText = "Имя и Фамилия";
                dataGridView1.Columns["Salary"].HeaderText = "Оклад";
                dataGridView1.Columns["Bonus"].HeaderText = "Премия";
                dataGridView1.Columns["LastPaymentDate"].HeaderText = "Дата последней выплаты";

                // Устанавливаем автоматический размер столбцов
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["FullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            // Увеличиваем размер ComboBox
            comboBox1.Size = new Size(300, 90); // Задаем новый размер

            // Получаем текущий шрифт ComboBox
            Font currentFont = comboBox1.Font;

            // Создаем новый шрифт с увеличенным размером
            Font newFont = new Font("Bookman Old Style", comboBox1.Font.Size * 2.5f, comboBox1.Font.Style);

            // Применяем новый шрифт к ComboBox
            comboBox1.Font = newFont;
            comboBox1.Items.Clear();

            // Получаем все уникальные значения для столбца "FullName" из DataGridView
            HashSet<string> uniqueFullNames = new HashSet<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["FullName"].Value != null && row.Cells["FullName"].Value != DBNull.Value)
                {
                    uniqueFullNames.Add(row.Cells["FullName"].Value.ToString());
                }
            }

            // Добавляем уникальные значения в ComboBox
            comboBox1.Items.AddRange(uniqueFullNames.ToArray());
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            Form f1 = new Form2();
            f1.Show();
            this.Hide();
        }
        
        
        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            label2.Visible = false;
            if (comboBox1.SelectedItem != null)
            {
                string selectedFullName = comboBox1.SelectedItem.ToString();

                // Находим строку с соответствующим FullName в DataGridView
                DataGridViewRow selectedRow = null;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["FullName"].Value != null && row.Cells["FullName"].Value.ToString() == selectedFullName)
                    {
                        selectedRow = row;
                        break;
                    }
                }

                if (selectedRow != null)
                {
                    // Получаем значения для соответствующих столбцов
                    object salaryValue = selectedRow.Cells["Salary"].Value;
                    object bonusValue = selectedRow.Cells["Bonus"].Value;
                    object lastPaymentDateValue = selectedRow.Cells["LastPaymentDate"].Value;

                    // Обновляем текст в соответствующих Label
                    label6.Text = salaryValue != null ? salaryValue.ToString() + " рублей" : "Данные о зарплате отсутствуют";
                    label7.Text = bonusValue != null ? bonusValue.ToString() + " рублей" : "Данные о бонусе отсутствуют";
                    label8.Text = lastPaymentDateValue != null ? ((DateTime)lastPaymentDateValue).ToShortDateString() : "Дата не указана";

                    // Делаем Labels видимыми
                    label6.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    object positionValue = selectedRow.Cells["Position"].Value;
                    if (positionValue != null)
                    {
                        if (positionValue as string == "Официант")
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Image = Properties.Resources.ofic;
                        }
                        else if (positionValue as string == "Повар" || positionValue as string == "Шеф-повар")
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Image = Properties.Resources.povar;
                        }
                        else if (positionValue as string == "Уборщик")
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Image = Properties.Resources.clean;
                        }
                        else if (positionValue as string == "Менеджер")
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Image = Properties.Resources.men;
                        }
                        else
                        {
                            pictureBox1.Visible = false; // Скрыть PictureBox, если позиция не соответствует условиям
                        }
                    }
                    else
                    {
                        pictureBox1.Visible = false; // Скрыть PictureBox, если значение позиции отсутствует
                    }
                }
                else
                {
                    label2.Text = "Не найдено соответствие для: " + selectedFullName;
                    label2.Visible = true;
                }
            }
            else
            {
                label2.Text = "Пожалуйста, выберите значение из списка";
                label2.Visible = true;
            }
        }
    }
}
