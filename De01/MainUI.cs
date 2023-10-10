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

namespace De01
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
            LoadClassNames();
            InitDatatoDGV();
            InitComp();
        }

        private string connectstring = "Data Source=.;Initial Catalog=QuanlySV;Integrated Security=True;MultipleActiveResultSets=True";
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        private void ClearTextBox()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void InitComp()
        {
            button4.Enabled=false;
            button5.Enabled = false;
            comboBox1.SelectedIndex = 0;
        }

        private void SaveNotSave()
        {
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void LoadClassNames()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();

                    string query = "SELECT TenLop FROM Lop";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tenLop = reader["TenLop"].ToString();
                                comboBox1.Items.Add(tenLop);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void InitDatatoDGV()
        {
            DGV1.Rows.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();
                    string query = "SELECT * FROM Sinhvien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DGV1.Rows.Add(row["MaSV"], row["HotenSV"], row["BDay"], row["MaLop"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();

                    string queryLop = "SELECT MaLop FROM Lop WHERE TenLop = @TenLop";
                    using (SqlCommand commandLop = new SqlCommand(queryLop, connection))
                    {
                        commandLop.Parameters.AddWithValue("@TenLop", comboBox1.SelectedItem.ToString());
                        string maLop = commandLop.ExecuteScalar() as string;

                        if (!string.IsNullOrEmpty(maLop))
                        {
                            string query = "INSERT INTO Sinhvien (MaSV, HotenSV, BDay, MaLop) VALUES (@MaSV, @HotenSV, @BDay, @MaLop)";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@MaSV", textBox1.Text);
                                command.Parameters.AddWithValue("@HotenSV", textBox2.Text);
                                command.Parameters.AddWithValue("@BDay", dateTimePicker1.Value);
                                command.Parameters.AddWithValue("@MaLop", maLop);

                                command.ExecuteNonQuery();

                                MessageBox.Show("Thêm dữ liệu thành công!");
                                InitDatatoDGV();
                                ClearTextBox();

                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mã lớp tương ứng!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox4.Text.Trim();
            foreach (DataGridViewRow row in DGV1.Rows)
            {
                string studentName = row.Cells["hovaten"].Value.ToString();
                if (studentName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGV1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá sinh viên này không?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string maSV = DGV1.SelectedRows[0].Cells["masv"].Value.ToString();

                        using (SqlConnection connection = new SqlConnection(connectstring))
                        {
                            connection.Open();

                            string query = "DELETE FROM Sinhvien WHERE MaSV = @MaSV";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@MaSV", maSV);
                                command.ExecuteNonQuery();

                                DGV1.Rows.RemoveAt(DGV1.SelectedRows[0].Index);

                                MessageBox.Show("Xoá sinh viên thành công!");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn sinh viên để xoá.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void AddDelButtonEnable()
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void AddDelButtonDisable()
        {
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DGV1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = DGV1.SelectedRows[0];
                string maSV = selectedRow.Cells["masv"].Value.ToString();
                string hoTen = selectedRow.Cells["hovaten"].Value.ToString();
                DateTime ngaySinh = DateTime.Parse(selectedRow.Cells["BDay"].Value.ToString());
                string lopHoc = selectedRow.Cells["lop"].Value.ToString();
                textBox1.Text = maSV;
                textBox2.Text = hoTen;
                dateTimePicker1.Value = ngaySinh;
                AddDelButtonDisable();
                textBox1.Enabled= false;
                SaveNotSave();
            }
            else
            {
                MessageBox.Show("Chưa chọn sinh viên để hiển thị thông tin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = textBox1.Text;


                if (string.IsNullOrEmpty(maSV))
                {
                    MessageBox.Show("Vui lòng nhập mã số sinh viên.");
                    return;
                }

                if (DeleteStudentByMaSV(maSV))
                {

                    AddStudent();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên có mã số: " + maSV);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            AddDelButtonEnable();
            textBox1.Enabled = true;
            InitComp();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddDelButtonEnable();
            textBox1.Text=null; 
            textBox2.Text= null;
            textBox1.Enabled = true;
            InitComp();
            InitDatatoDGV();
        }

        private bool DeleteStudentByMaSV(string maSV)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM Sinhvien WHERE MaSV = @MaSV";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@MaSV", maSV);
                        deleteCommand.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false; 
            }
        }

        private void AddStudent()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();

                    string queryLop = "SELECT MaLop FROM Lop WHERE TenLop = @TenLop";
                    using (SqlCommand commandLop = new SqlCommand(queryLop, connection))
                    {
                        commandLop.Parameters.AddWithValue("@TenLop", comboBox1.SelectedItem.ToString());
                        string maLop = commandLop.ExecuteScalar() as string;

                        if (!string.IsNullOrEmpty(maLop))
                        {
                            string query = "INSERT INTO Sinhvien (MaSV, HotenSV, BDay, MaLop) VALUES (@MaSV, @HotenSV, @BDay, @MaLop)";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@MaSV", textBox1.Text);
                                command.Parameters.AddWithValue("@HotenSV", textBox2.Text);
                                command.Parameters.AddWithValue("@BDay", dateTimePicker1.Value);
                                command.Parameters.AddWithValue("@MaLop", maLop);

                                command.ExecuteNonQuery();

                                MessageBox.Show("Cập nhật thông tin sinh viên thành công!");
                                InitDatatoDGV();
                                ClearTextBox();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mã lớp tương ứng!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
