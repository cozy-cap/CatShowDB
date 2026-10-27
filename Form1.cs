using System.Data;
using Npgsql;

namespace CatShowApp
{
    public partial class Form1 : Form
    {
        private string connectionString = "Host=192.168.1.105;Port=5432;Database=catshowdb;Username=postgres;Password=Cozy_Cap2602:D";

        private readonly DataGridView dgvCats = new();
        private readonly DataGridView dgvVets = new();
        private readonly DataGridView dgvChecks = new();
        private readonly TabControl tabControl = new();

        public Form1()
        {
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
