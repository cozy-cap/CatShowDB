using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace CatShowApp
{
    public partial class Form1 : Form
    {
        private readonly DataGridView dgvCats = new();
        private readonly DataGridView dgvVets = new();
        private readonly DataGridView dgvChecks = new();
        private readonly TabControl tabControl = new();

        private readonly string connectionString;

        public Form1()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            connectionString = configuration.GetConnectionString("DefaultConnection");

            InitializeComponentCustom();
            LoadData();
        }

        private void InitializeComponentCustom()
        {
            Text = "Виставка котів";
            Size = new Size(900, 600);

            tabControl.Dock = DockStyle.Fill;

            // Вкладка "Коти"
            TabPage tabCats = new TabPage("Коти (Cat)");
            ConfigureDataGridView(dgvCats);
            tabCats.Controls.Add(dgvCats);

            // Вкладка "Ветеринари"
            TabPage tabVets = new TabPage("Ветеринари (Vet)");
            ConfigureDataGridView(dgvVets);
            tabVets.Controls.Add(dgvVets);

            // Вкладка "Огляди"
            TabPage tabChecks = new TabPage("Огляди (Vet_Check)");
            ConfigureDataGridView(dgvChecks);
            tabChecks.Controls.Add(dgvChecks);

            tabControl.TabPages.Add(tabCats);
            tabControl.TabPages.Add(tabVets);
            tabControl.TabPages.Add(tabChecks);

            Controls.Add(tabControl);
        }

        // Допоміжний метод для налаштування захисту від редагування
        private void ConfigureDataGridView(DataGridView dgv)
        {
            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadData()
        {
            LoadTable("SELECT * FROM cat", dgvCats);
            LoadTable("SELECT * FROM vet", dgvVets);
            LoadTable("SELECT * FROM vet_check", dgvChecks);
        }

        private void LoadTable(string query, DataGridView dgv)
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                using var adapter = new NpgsqlDataAdapter(query, conn);
                DataTable dt = new();
                adapter.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка підключення: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
