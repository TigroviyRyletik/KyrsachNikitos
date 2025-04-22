using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KyrsachNikitos
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        //  Методи заповнення ДатаГрід
        private void FillTable1()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
            string selectQuery = "SELECT * FROM Booking";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    try
                    {
                        sqlConnection.Open();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при отриманні даних: {ex.Message}");
                    }
                }
            }
        }

        private void FillTable2()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
            string selectQuery = "SELECT * FROM Child";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    try
                    {
                        sqlConnection.Open();
                        adapter.Fill(dataTable);
                        dataGridView2.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при отриманні даних: {ex.Message}");
                    }
                }
            }
        }

        private void FillTable3()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
            string selectQuery = "SELECT * FROM ResortBase";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    try
                    {
                        sqlConnection.Open();
                        adapter.Fill(dataTable);
                        dataGridView3.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при отриманні даних: {ex.Message}");
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            sqlConnection.Open();

            //  Заповнення ДатаГрід
            FillTable1();
            FillTable2();
            FillTable3();

            dataGridView1.Dock = DockStyle.Fill;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView3.Dock = DockStyle.Fill;

            //  Налаштування КомбоБоксів
            comboBox1.Items.Add("BookingID");
            comboBox1.Items.Add("ChildID");
            comboBox1.Items.Add("ArrivalDate");
            comboBox1.Items.Add("DepartureDate");
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Add("ChildID");
            comboBox2.Items.Add("FirstName");
            comboBox2.Items.Add("LastName");
            comboBox2.Items.Add("DateOfBirth");
            comboBox2.SelectedIndex = 0;

            comboBox3.Items.Add("BaseID");
            comboBox3.Items.Add("Name");
            comboBox3.Items.Add("City");
            comboBox3.SelectedIndex = 0;
        }

        //  Кнопки додавання запису
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Booking (ChildID, ArrivalDate, DepartureDate) VALUES (@ChildID, @ArrivalDate, @DepartureDate)", sqlConnection);

            if (int.TryParse(textBox1.Text, out int childID))
            {
                command.Parameters.AddWithValue("@ChildID", childID);
            }
            else
            {
                MessageBox.Show("Неправильний формат ID дитини.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DateTime.TryParse(textBox2.Text, out DateTime arrivalDate))
            {
                command.Parameters.AddWithValue("@ArrivalDate", arrivalDate);
            }
            else
            {
                MessageBox.Show("Неправильний формат дати заїзду.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DateTime.TryParse(textBox3.Text, out DateTime departureDate))
            {
                command.Parameters.AddWithValue("@DepartureDate", departureDate);
            }
            else
            {
                MessageBox.Show("Неправильний формат дати виїзду.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Бронювання успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillTable1();
                }
                else
                {
                    MessageBox.Show("Не вдалося додати бронювання.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка при додаванні бронювання: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Child (FirstName, LastName, DateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth)", sqlConnection);

            command.Parameters.AddWithValue("FirstName", textBox4.Text);
            command.Parameters.AddWithValue("LastName", textBox5.Text);

            if (DateTime.TryParse(textBox6.Text, out DateTime dateOfBirth))
            {
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            }
            else
            {
                MessageBox.Show("Неправильний формат дати народження.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Дитину успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillTable2();
                }
                else
                {
                    MessageBox.Show("Не вдалося додати дитину.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка при додаванні дитини: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO ResortBase (Name, City) VALUES (@Name, @City)", sqlConnection);

            command.Parameters.AddWithValue("Name", textBox7.Text);
            command.Parameters.AddWithValue("City", textBox8.Text);

            if(command.ExecuteNonQuery() >= 1)
            {
                MessageBox.Show("Запис було додано!");
            }
            else
            {
                MessageBox.Show("Запис не було додано!");
            }
            FillTable3();
        }

        //  Кнопки зміни запису
        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Booking SET ChildID = @ChildID, ArrivalDate = @ArrivalDate, DepartureDate = @DepartureDate WHERE BookingID = @BookingID;", sqlConnection);

            if (int.TryParse(textBox9.Text, out int childID))
            {
                command.Parameters.AddWithValue("@ChildID", childID);
            }
            else
            {
                MessageBox.Show("Неправильний формат ID дитини.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DateTime.TryParse(textBox10.Text, out DateTime arrivalDate))
            {
                command.Parameters.AddWithValue("@ArrivalDate", arrivalDate);
            }
            else
            {
                MessageBox.Show("Неправильний формат дати заїзду.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DateTime.TryParse(textBox11.Text, out DateTime departureDate))
            {
                command.Parameters.AddWithValue("@DepartureDate", departureDate);
            }
            else
            {
                MessageBox.Show("Неправильний формат дати виїзду.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(textBox12.Text, out int bookingID))
            {
                command.Parameters.AddWithValue("@BookingID", bookingID);
            }
            else
            {
                MessageBox.Show("Неправильний формат ID бронювання.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected >= 1)
                {
                    MessageBox.Show("Запис було змінено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Запис не було змінено!", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                FillTable1();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка при оновленні запису: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Child SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth WHERE ChildID = @ChildID;", sqlConnection);

            if (int.TryParse(textBox16.Text, out int childID))
            {
                command.Parameters.AddWithValue("@ChildID", childID);
            }
            else
            {
                MessageBox.Show("Неправильний формат ID дитини.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            command.Parameters.AddWithValue("@FirstName", textBox13.Text);
            command.Parameters.AddWithValue("@LastName", textBox14.Text);

            if (DateTime.TryParse(textBox15.Text, out DateTime dateOfBirth))
            {
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            }
            else
            {
                MessageBox.Show("Неправильний формат дати народження.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected >= 1)
                {
                    MessageBox.Show("Дані дитини було оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillTable2(); 
                }
                else
                {
                    MessageBox.Show("Не вдалося оновити дані дитини. Можливо, запис з таким ID не існує.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка при оновленні даних дитини: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE ResortBase SET Name = @Name, City = @City WHERE BaseID = @BaseID;", sqlConnection);

            if (int.TryParse(textBox19.Text, out int baseID))
            {
                command.Parameters.AddWithValue("@BaseID", baseID);
            }
            else
            {
                MessageBox.Show("Неправильний формат ID бази відпочинку.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            command.Parameters.AddWithValue("@Name", textBox17.Text);
            command.Parameters.AddWithValue("@City", textBox18.Text);

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected >= 1)
                {
                    MessageBox.Show("Дані бази відпочинку було оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillTable3();
                }
                else
                {
                    MessageBox.Show("Не вдалося оновити дані бази відпочинку. Можливо, запис з таким ID не існує.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка при оновленні даних бази відпочинку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  Кнопки видалення запису
        private void button7_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox20.Text, out int bookingIdToDelete))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();
                        string deleteQuery = "DELETE FROM Booking WHERE BookingID = @BookingID;";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection))
                        {
                            deleteCommand.Parameters.AddWithValue("@BookingID", bookingIdToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                int maxId = 0;
                                string maxIdQuery = "SELECT ISNULL(MAX(BookingID), 0) FROM Booking;";
                                using (SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, sqlConnection))
                                {
                                    maxId = (int)maxIdCommand.ExecuteScalar();
                                }

                                string reseedQuery = $"DBCC CHECKIDENT ('Booking', RESEED, {maxId});";
                                using (SqlCommand reseedCommand = new SqlCommand(reseedQuery, sqlConnection))
                                {
                                    reseedCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Запис успішно видалено.");
                                FillTable1();
                                textBox20.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Запис з таким ID не знайдено.");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Помилка при видаленні: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox21.Text, out int childIdToDelete))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();

                        string deleteQuery = "DELETE FROM Child WHERE ChildID = @ChildID;";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection))
                        {
                            deleteCommand.Parameters.AddWithValue("@ChildID", childIdToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                int maxId = 0;
                                string maxIdQuery = "SELECT ISNULL(MAX(ChildID), 0) FROM Child;";
                                using (SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, sqlConnection))
                                {
                                    maxId = (int)maxIdCommand.ExecuteScalar();
                                }

                                string reseedQuery = $"DBCC CHECKIDENT ('Child', RESEED, {maxId});";
                                using (SqlCommand reseedCommand = new SqlCommand(reseedQuery, sqlConnection))
                                {
                                    reseedCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Дитину успішно видалено.");
                                FillTable2();
                                textBox21.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Дитину з таким ID не знайдено.");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Помилка при видаленні: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox22.Text, out int baseIdToDelete))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();

                        string deleteQuery = "DELETE FROM ResortBase WHERE BaseID = @BaseID;";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection))
                        {
                            deleteCommand.Parameters.AddWithValue("@BaseID", baseIdToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                int maxId = 0;
                                string maxIdQuery = "SELECT ISNULL(MAX(BaseID), 0) FROM ResortBase;";
                                using (SqlCommand maxIdCommand = new SqlCommand(maxIdQuery, sqlConnection))
                                {
                                    maxId = (int)maxIdCommand.ExecuteScalar();
                                }

                                string reseedQuery = $"DBCC CHECKIDENT ('ResortBase', RESEED, {maxId});";
                                using (SqlCommand reseedCommand = new SqlCommand(reseedQuery, sqlConnection))
                                {
                                    reseedCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Запис бази відпочинку успішно видалено.");
                                FillTable3();
                                textBox22.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Базу з таким ID не знайдено.");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Помилка при видаленні: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID.");
            }
        }

        //  Кнопки пошуку запису
        private void button10_Click(object sender, EventArgs e)
        {
            string searchText = textBox23.Text.Trim();
            string searchField = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(searchField))
            {
                MessageBox.Show("Будь ласка, виберіть поле для пошуку.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT BookingID, ChildID, ArrivalDate, DepartureDate FROM Booking WHERE ";

            switch (searchField)
            {
                case "BookingID":
                    if (int.TryParse(searchText, out int bookingID))
                    {
                        query += "BookingID = @SearchValue";
                    }
                    else
                    {
                        MessageBox.Show("Будь ласка, введіть ціле число для пошуку за ID бронювання.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                case "ChildID":
                    if (int.TryParse(searchText, out int childID))
                    {
                        query += "ChildID = @SearchValue";
                    }
                    else
                    {
                        MessageBox.Show("Будь ласка, введіть ціле число для пошуку за ID дитини.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                case "ArrivalDate":
                case "DepartureDate":
                    query += searchField + " LIKE @SearchValue";
                    break;
                default:
                    MessageBox.Show("Невідоме поле для пошуку.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            query += ";";

            try
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);

                if (searchField == "BookingID" || searchField == "ChildID")
                {
                    if (int.TryParse(searchText, out int searchValue))
                    {
                        command.Parameters.AddWithValue("@SearchValue", searchValue);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@SearchValue", "%" + searchText + "%");
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable searchResultTable = new DataTable();
                adapter.Fill(searchResultTable);

                dataGridView1.DataSource = searchResultTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка пошуку бронювань: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string searchText = textBox24.Text.Trim(); // Використовуємо textBox24
            string searchField = comboBox2.SelectedItem?.ToString(); // Використовуємо comboBox2

            if (string.IsNullOrEmpty(searchField))
            {
                MessageBox.Show("Будь ласка, виберіть поле для пошуку.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT ChildID, FirstName, LastName, DateOfBirth FROM Child WHERE ";

            switch (searchField)
            {
                case "ChildID":
                    if (int.TryParse(searchText, out int childID))
                    {
                        query += "ChildID = @SearchValue";
                    }
                    else
                    {
                        MessageBox.Show("Будь ласка, введіть ціле число для пошуку за ID дитини.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                case "FirstName":
                case "LastName":
                    query += searchField + " LIKE @SearchValue"; // Пошук часткового співпадіння тексту
                    break;
                case "DateOfBirth":
                    query += "CAST(DateOfBirth AS NVARCHAR) LIKE @SearchValue"; // Пошук часткового співпадіння дати
                    break;
                default:
                    MessageBox.Show("Невідоме поле для пошуку.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            query += ";";

            try
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);

                if (searchField == "ChildID")
                {
                    if (int.TryParse(searchText, out int searchValue))
                    {
                        command.Parameters.AddWithValue("@SearchValue", searchValue);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@SearchValue", "%" + searchText + "%");
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable searchResultTable = new DataTable();
                adapter.Fill(searchResultTable);

                dataGridView2.DataSource = searchResultTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка пошуку дітей: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string searchText = textBox25.Text.Trim();
            string searchField = comboBox3.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(searchField))
            {
                MessageBox.Show("Будь ласка, виберіть поле для пошуку.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT BaseID, Name, City FROM ResortBase WHERE ";

            switch (searchField)
            {
                case "BaseID":
                    if (int.TryParse(searchText, out int baseID))
                    {
                        query += "BaseID = @SearchValue";
                    }
                    else
                    {
                        MessageBox.Show("Будь ласка, введіть ціле число для пошуку за ID бази.", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                case "Name":
                case "City":
                    query += searchField + " LIKE @SearchValue";
                    break;
                default:
                    MessageBox.Show("Невідоме поле для пошуку.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            query += ";";

            try
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);

                if (searchField == "BaseID")
                {
                    if (int.TryParse(searchText, out int searchValue))
                    {
                        command.Parameters.AddWithValue("@SearchValue", searchValue);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@SearchValue", "%" + searchText + "%");
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable searchResultTable = new DataTable();
                adapter.Fill(searchResultTable);

                dataGridView3.DataSource = searchResultTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка пошуку баз відпочинку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
