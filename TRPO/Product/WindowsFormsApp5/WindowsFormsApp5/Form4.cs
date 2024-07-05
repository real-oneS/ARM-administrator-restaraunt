using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    
    public partial class Form4 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        //string connectionString = "Server=server46;Database=ValiullinDR;User Id=stud;Password=stud;";
        string connectionString = "Data Source=HOME-PC;Initial Catalog=ValiullinDR;Integrated Security=true;";
        string sql = "SELECT * FROM dbo.Orders ORDER BY Id";
        public Form4()
        {
            InitializeComponent();
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
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
                dataGridView1.Columns["Number"].HeaderText = "Номер столика";
                dataGridView1.Columns["Components"].HeaderText = "Состав";
                dataGridView1.Columns["Status"].HeaderText = "Состояние";
                dataGridView1.Columns["Reason"].HeaderText = "Причина";
                dataGridView1.Columns["Ready"].HeaderText = "Готовность";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            Form f1 = new Form2();
            f1.Show();
            this.Hide();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    try
                    {
                        Convert.ToInt32(row.Cells[2].Value);
                        MessageBox.Show("Введите состав заказа, а не просто цифры!!!");
                        row.Cells[2].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[2].Style.BackColor = Color.White;
                    }
                    try
                    {
                        Convert.ToInt32(row.Cells[3].Value);
                        MessageBox.Show("Введите статус заказа, а не просто цифры!!!");
                        row.Cells[3].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[3].Style.BackColor = Color.White;
                    }
                    try
                    {
                        Convert.ToInt32(row.Cells[4].Value);
                        MessageBox.Show("Введите причину, а не просто цифры!!!");
                        row.Cells[4].Style.BackColor = Color.Red;
                        return;
                    }
                    catch
                    {
                        row.Cells[4].Style.BackColor = Color.White;
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
                    adapter.InsertCommand = new SqlCommand("sp_CreateOrders", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@number", SqlDbType.Int, 0, "Number"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@components", SqlDbType.NVarChar, 50, "Components"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@status", SqlDbType.NVarChar, 50, "Status"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@reason", SqlDbType.NVarChar, 50, "Reason"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@ready", SqlDbType.NVarChar, 50, "Ready"));

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

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования.");
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];

            using (var editForm = new EditForm(selectedRow))
            {
                editForm.Number = selectedRow.Cells["Number"].Value.ToString();
                editForm.Components = selectedRow.Cells["Components"].Value.ToString();
                editForm.Status = selectedRow.Cells["Status"].Value.ToString();
                editForm.Reason = selectedRow.Cells["Reason"].Value.ToString();
                editForm.Ready = selectedRow.Cells["Ready"].Value.ToString();

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Save the changes back to the DataGridView
                    selectedRow.Cells["Number"].Value = editForm.Number;
                    selectedRow.Cells["Components"].Value = editForm.Components;
                    selectedRow.Cells["Status"].Value = editForm.Status;
                    selectedRow.Cells["Reason"].Value = editForm.Reason;
                    selectedRow.Cells["Ready"].Value = editForm.Ready;
                }
                if (selectedRow.Index == 0)
                {
                    // Выполняем действия для первой строки
                    // Например, нажатие кнопки button1
                    button1_Click(sender, e);
                }
                if (selectedRow.Index == 1)
                {
                    button5_Click(sender, e);
                }
                if (selectedRow.Index == 2)
                {
                    button6_Click(sender, e);
                }
                if (selectedRow.Index == 3)
                {
                    button7_Click(sender, e);
                }
                if (selectedRow.Index == 4)
                {
                    button9_Click(sender, e);
                }
                if (selectedRow.Index == 5)
                {
                    button10_Click(sender, e);
                }
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли в DataGridView хотя бы одна строка
            if (dataGridView1.Rows.Count > 0)
            {
                // Выбираем первую строку
                dataGridView1.ClearSelection(); // Очищаем текущее выделение
                dataGridView1.Rows[0].Selected = true; // Выбираем первую строку
            }
            label7.Visible=false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label2.Text = "Столик №" + dataGridView1.Rows[0].Cells[1].Value;
            label2.Visible = true;

            // Проверяем, не равна ли ячейка null
            if ((dataGridView1.Rows[0].Cells[2].Value as string) != null && (dataGridView1.Rows[0].Cells[2].Value as string) != "null" && (dataGridView1.Rows[5].Cells[2].Value as string) != "")
            {
                // Если ячейка не равна null, делаем label3 видимым и меняем его текст
                label3.Text = "Состав";
                label3.Visible = true;
                label6.Text = (string)dataGridView1.Rows[0].Cells[2].Value;
                label6.Visible = true;
            }
            else
            {
                // Если ячейка равна null, делаем label3 видимым и меняем его текст на "Свободный столик"
                label3.Text = "Свободный столик";
                label3.Visible = true;
                label6.Visible = false;
            }

            if ((dataGridView1.Rows[0].Cells[3].Value as string) != null)
            {
                label4.Text = (string)dataGridView1.Rows[0].Cells[3].Value;
                label4.Visible = true;
            }

            if ((dataGridView1.Rows[0].Cells[4].Value as string) != null)
            {
                label5.Text = (string)dataGridView1.Rows[0].Cells[4].Value;
                label5.Visible = true;
            }
            if ((dataGridView1.Rows[0].Cells[3].Value as string) == "Отменен" && (dataGridView1.Rows[0].Cells[5].Value as string) == "Нет")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.krestik;
            }
            else if ((dataGridView1.Rows[0].Cells[3].Value as string) == "Готов" && (dataGridView1.Rows[0].Cells[5].Value as string) == "Да")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.galochka;
            }
            else if ((dataGridView1.Rows[0].Cells[3].Value as string) == "Задерживается")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.znachok;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection(); // Очищаем текущее выделение
                dataGridView1.Rows[1].Selected = true;
            }
            label7.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label2.Text = "Столик №" + dataGridView1.Rows[1].Cells[1].Value;
            label2.Visible = true;

            // Проверяем, не равна ли ячейка null
            if ((dataGridView1.Rows[1].Cells[2].Value as string) != null && (dataGridView1.Rows[1].Cells[2].Value as string) != "null" && (dataGridView1.Rows[5].Cells[2].Value as string) != "")
            {
                // Если ячейка не равна null, делаем label3 видимым и меняем его текст
                label3.Text = "Состав";
                label3.Visible = true;
                label6.Text = (string)dataGridView1.Rows[1].Cells[2].Value;
                label6.Visible = true;
            }
            else
            {
                // Если ячейка равна null, делаем label3 видимым и меняем его текст на "Свободный столик"
                label3.Text = "Свободный столик";
                label3.Visible = true;
                label6.Visible = false;
            }

            if ((dataGridView1.Rows[1].Cells[3].Value as string) != null)
            {
                label4.Text = (string)dataGridView1.Rows[1].Cells[3].Value;
                label4.Visible = true;
            }

            if ((dataGridView1.Rows[1].Cells[4].Value as string) != null)
            {
                label5.Text = (string)dataGridView1.Rows[1].Cells[4].Value;
                label5.Visible = true;
            }
            if ((dataGridView1.Rows[1].Cells[2].Value as string) == "Отменен" && (dataGridView1.Rows[1].Cells[4].Value as string) == "Нет")
            {
                pictureBox1.Image = Image.FromFile("~/obj/krestik.png");
            }
            if ((dataGridView1.Rows[1].Cells[3].Value as string) == "Отменен" && (dataGridView1.Rows[1].Cells[5].Value as string) == "Нет")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.krestik;
            }
            else if ((dataGridView1.Rows[1].Cells[3].Value as string) == "Готов" && (dataGridView1.Rows[1].Cells[5].Value as string) == "Да")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.galochka;
            }
            else if ((dataGridView1.Rows[1].Cells[3].Value as string) == "Задерживается")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.znachok;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection(); // Очищаем текущее выделение
                dataGridView1.Rows[2].Selected = true;
            }
            label7.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label2.Text = "Столик №" + dataGridView1.Rows[2].Cells[1].Value;
            label2.Visible = true;

            // Проверяем, не равна ли ячейка null
            if ((dataGridView1.Rows[2].Cells[2].Value as string) != null && (dataGridView1.Rows[2].Cells[2].Value as string) != "null" && (dataGridView1.Rows[5].Cells[2].Value as string) != "")
            {
                // Если ячейка не равна null, делаем label3 видимым и меняем его текст
                label3.Text = "Состав";
                label3.Visible = true;
                label6.Text = (string)dataGridView1.Rows[2].Cells[2].Value;
                label6.Visible = true;
            }
            else
            {
                // Если ячейка равна null, делаем label3 видимым и меняем его текст на "Свободный столик"
                label3.Text = "Свободный столик";
                label3.Visible = true;
                label6.Visible = false;
            }

            if ((dataGridView1.Rows[2].Cells[3].Value as string) != null)
            {
                label4.Text = (string)dataGridView1.Rows[2].Cells[3].Value;
                label4.Visible = true;
            }

            if ((dataGridView1.Rows[2].Cells[4].Value as string) != null)
            {
                label5.Text = (string)dataGridView1.Rows[2].Cells[4].Value;
                label5.Visible = true;
            }
            if ((dataGridView1.Rows[2].Cells[3].Value as string) == "Отменен" && (dataGridView1.Rows[2].Cells[5].Value as string) == "Нет")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.krestik;
            }
            else if ((dataGridView1.Rows[2].Cells[3].Value as string) == "Готов" && (dataGridView1.Rows[2].Cells[5].Value as string) == "Да")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.galochka;
            }
            else if ((dataGridView1.Rows[2].Cells[3].Value as string) == "Задерживается")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.znachok;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection(); // Очищаем текущее выделение
                dataGridView1.Rows[3].Selected = true; 
            }
            label7.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label2.Text = "Столик №" + dataGridView1.Rows[3].Cells[1].Value;
            label2.Visible = true;

            // Проверяем, не равна ли ячейка null
            if ((dataGridView1.Rows[3].Cells[2].Value as string) != null && (dataGridView1.Rows[3].Cells[2].Value as string) != "null" && (dataGridView1.Rows[5].Cells[2].Value as string) != "")
            {
                // Если ячейка не равна null, делаем label3 видимым и меняем его текст
                label3.Text = "Состав";
                label3.Visible = true;
                label6.Text = (string)dataGridView1.Rows[3].Cells[2].Value;
                label6.Visible = true;
            }
            else
            {
                // Если ячейка равна null, делаем label3 видимым и меняем его текст на "Свободный столик"
                label3.Text = "Свободный столик";
                label3.Visible = true;
                label6.Visible = false;
            }

            if ((dataGridView1.Rows[3].Cells[3].Value as string) != null)
            {
                label4.Text = (string)dataGridView1.Rows[3].Cells[3].Value;
                label4.Visible = true;
            }

            if ((dataGridView1.Rows[3].Cells[4].Value as string) != null)
            {
                label5.Text = (string)dataGridView1.Rows[3].Cells[4].Value;
                label5.Visible = true;
            }
            
            if ((dataGridView1.Rows[3].Cells[3].Value as string) == "Отменен" && (dataGridView1.Rows[3].Cells[5].Value as string) == "Нет")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.krestik;
            }
            else if ((dataGridView1.Rows[3].Cells[3].Value as string) == "Готов" && (dataGridView1.Rows[3].Cells[5].Value as string) == "Да")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.galochka;
            }
            else if ((dataGridView1.Rows[3].Cells[3].Value as string) == "Задерживается")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.znachok;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection(); // Очищаем текущее выделение
                dataGridView1.Rows[4].Selected = true; 
            }
            label7.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label2.Text = "Столик №" + dataGridView1.Rows[4].Cells[1].Value;
            label2.Visible = true;

            // Проверяем, не равна ли ячейка null
            if ((dataGridView1.Rows[4].Cells[2].Value as string) != null && (dataGridView1.Rows[4].Cells[2].Value as string) != "null" && (dataGridView1.Rows[5].Cells[2].Value as string) != "")
            {
                // Если ячейка не равна null, делаем label3 видимым и меняем его текст
                label3.Text = "Состав";
                label3.Visible = true;
                label6.Text = (string)dataGridView1.Rows[4].Cells[2].Value;
                label6.Visible = true;
            }
            else
            {
                // Если ячейка равна null, делаем label3 видимым и меняем его текст на "Свободный столик"
                label3.Text = "Свободный столик";
                label3.Visible = true;
                label6.Visible = false;
            }

            if ((dataGridView1.Rows[4].Cells[3].Value as string) != null)
            {
                label4.Text = (string)dataGridView1.Rows[4].Cells[3].Value;
                label4.Visible = true;
            }

            if ((dataGridView1.Rows[4].Cells[4].Value as string) != null)
            {
                label5.Text = (string)dataGridView1.Rows[4].Cells[4].Value;
                label5.Visible = true;
            }
            if ((dataGridView1.Rows[4].Cells[3].Value as string) == "Отменен" && (dataGridView1.Rows[4].Cells[5].Value as string) == "Нет")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.krestik;
            }
            else if ((dataGridView1.Rows[4].Cells[3].Value as string) == "Готов" && (dataGridView1.Rows[4].Cells[5].Value as string) == "Да")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.galochka;
            }
            else if ((dataGridView1.Rows[4].Cells[3].Value as string) == "Задерживается")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.znachok;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection(); // Очищаем текущее выделение
                dataGridView1.Rows[5].Selected = true; 
            }
            label7.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label2.Text = "Столик №" + dataGridView1.Rows[5].Cells[1].Value;
            label2.Visible = true;

            // Проверяем, не равна ли ячейка null
            if ((dataGridView1.Rows[5].Cells[2].Value as string) != null && (dataGridView1.Rows[5].Cells[2].Value as string) != "null" && (dataGridView1.Rows[5].Cells[2].Value as string) != "")
            {
                // Если ячейка не равна null, делаем label3 видимым и меняем его текст
                label3.Text = "Состав";
                label3.Visible = true;
                label6.Text = (string)dataGridView1.Rows[5].Cells[2].Value;
                label6.Visible = true;
            }
            else
            {
                // Если ячейка равна null, делаем label3 видимым и меняем его текст на "Свободный столик"
                label3.Text = "Свободный столик";
                label3.Visible = true;
                label6.Visible = false;
            }

            if ((dataGridView1.Rows[5].Cells[3].Value as string) != null)
            {
                label4.Text = (string)dataGridView1.Rows[5].Cells[3].Value;
                label4.Visible = true;
            }

            if ((dataGridView1.Rows[5].Cells[4].Value as string) != null)
            {
                label5.Text = (string)dataGridView1.Rows[5].Cells[4].Value;
                label5.Visible = true;
            }
            if ((dataGridView1.Rows[5].Cells[3].Value as string) == "Отменен" && (dataGridView1.Rows[5].Cells[5].Value as string) == "Нет")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.krestik;
            }
            else if ((dataGridView1.Rows[5].Cells[3].Value as string) == "Готов" && (dataGridView1.Rows[5].Cells[5].Value as string) == "Да")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.galochka;
            }
            else if ((dataGridView1.Rows[5].Cells[3].Value as string) == "Задерживается")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources.znachok;
            }
            else
            {
                pictureBox1.Visible=false;
            }
        }
    }
    public class EditForm : Form
    {
        public string Number { get; set; }
        public string Components { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string Ready { get; set; }

        private Dictionary<string, Control> controls = new Dictionary<string, Control>();

        public EditForm(DataGridViewRow selectedRow)
        {
            InitializeComponent(selectedRow);
        }

        private void InitializeComponent(DataGridViewRow selectedRow)
        {
            Text = "Редактирование";
            Size = new System.Drawing.Size(400, 300); // Установите размер формы
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;

            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoSize = true
            };

            int rowIndex = 0;
            foreach (DataGridViewCell cell in selectedRow.Cells)
            {
                string columnName = cell.OwningColumn.Name;
                string cellValue = cell.Value.ToString();

                // Пропускаем поле ID
                if (columnName == "Id") continue;

                string labelText;
                switch (columnName)
                {
                    case "Number":
                        labelText = "Номер столика:";
                        break;
                    case "Components":
                        labelText = "Компоненты:";
                        break;
                    case "Status":
                        labelText = "Статус:";
                        break;
                    case "Reason":
                        labelText = "Причина:";
                        break;
                    case "Ready":
                        labelText = "Готовность:";
                        break;
                    default:
                        labelText = columnName + ":";
                        break;
                }

                Label label = new Label
                {
                    Text = labelText,
                    Dock = DockStyle.Fill,
                    TextAlign = System.Drawing.ContentAlignment.MiddleRight
                };

                Control control;
                switch (columnName)
                {
                    case "Number":
                        control = new TextBox
                        {
                            Text = cellValue,
                            Dock = DockStyle.Fill,
                            ReadOnly = true
                        };
                        break;
                    case "Status":
                        control = new ComboBox
                        {
                            Dock = DockStyle.Fill,
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            Items = { "","Выполняется", "Задерживается", "Готов"}
                        };
                        // Установите выбранное значение
                        (control as ComboBox).SelectedItem = cellValue;
                        break;
                    case "Ready":
                        control = new FlowLayoutPanel
                        {
                            Dock = DockStyle.Fill,
                            FlowDirection = FlowDirection.LeftToRight
                        };
                        var radioButtonYes = new RadioButton { Text = "Да" };
                        var radioButtonNo = new RadioButton { Text = "Нет" };
                        (control as FlowLayoutPanel).Controls.Add(radioButtonYes);
                        (control as FlowLayoutPanel).Controls.Add(radioButtonNo);
                        // Установите выбранное значение
                        radioButtonYes.Checked = cellValue == "Да";
                        radioButtonNo.Checked = cellValue == "Нет";
                        break;
                    default:
                        control = new TextBox
                        {
                            Text = cellValue,
                            Dock = DockStyle.Fill
                        };
                        break;
                }

                panel.Controls.Add(label, 0, rowIndex);
                panel.Controls.Add(control, 1, rowIndex);
                rowIndex++;

                // Сохраняем ссылку на Control в словаре, используя имя столбца в качестве ключа
                controls[columnName] = control;
            }

            Button confirmButton = new Button
            {
                Text = "Подтвердить",
                Dock = DockStyle.Bottom, // Расположение кнопки внизу
                Size = new Size(100, 30) // Установите размер кнопки
            };

            confirmButton.Click += (sender, e) =>
            {
                // Обновляем свойства EditForm значениями из Controls
                Number = (controls["Number"] as TextBox).Text;
                Components = (controls["Components"] as TextBox).Text;
                Status = (controls["Status"] as ComboBox).SelectedItem.ToString();
                Reason = (controls["Reason"] as TextBox).Text;
                // Для Ready используем RadioButton
                var readyPanel = controls["Ready"] as FlowLayoutPanel;
                Ready = (readyPanel.Controls[0] as RadioButton).Checked ? "Да" : "Нет";

                DialogResult = DialogResult.OK;
            };

            panel.Controls.Add(confirmButton);
            Controls.Add(panel);
        }
    }
}
