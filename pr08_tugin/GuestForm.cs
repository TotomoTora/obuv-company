using System;
using System.Drawing;
using System.Windows.Forms;

namespace ObuvCompany
{
    public partial class GuestForm : Form
    {
        private DataGridView productsDataGridView;
        private Label titleLabel;
        private Button backButton;
        private Panel headerPanel;
        private Panel footerPanel;
        private Label infoLabel;

        public GuestForm()
        {
            InitializeComponents();
            SetupDataGridView();
            SetupLayout();
            SetupEvents();
        }

        private void InitializeComponents()
        {
            // Настройка формы
            this.Text = "ООО 'Обувь' - Каталог товаров для гостей";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 600);
            this.BackColor = Color.White;

            // Верхняя панель
            headerPanel = new Panel();
            headerPanel.BackColor = Color.SteelBlue;
            headerPanel.Height = 80;
            headerPanel.Dock = DockStyle.Top;

            // Заголовок
            titleLabel = new Label();
            titleLabel.Text = "Каталог обуви";
            titleLabel.Font = new Font("Arial", 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;

            // Кнопка назад
            backButton = new Button();
            backButton.Text = "Назад";
            backButton.Size = new Size(80, 30);
            backButton.Font = new Font("Arial", 10, FontStyle.Regular);
            backButton.BackColor = Color.White;
            backButton.ForeColor = Color.SteelBlue;
            backButton.Cursor = Cursors.Hand;

            // DataGridView для товаров
            productsDataGridView = new DataGridView();
            productsDataGridView.Dock = DockStyle.Fill;
            productsDataGridView.ReadOnly = true;
            productsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            productsDataGridView.AllowUserToAddRows = false;
            productsDataGridView.AllowUserToDeleteRows = false;
            productsDataGridView.BackgroundColor = Color.White;
            productsDataGridView.BorderStyle = BorderStyle.None;
            productsDataGridView.RowHeadersVisible = false;

            // Нижняя панель
            footerPanel = new Panel();
            footerPanel.BackColor = Color.LightGray;
            footerPanel.Height = 40;
            footerPanel.Dock = DockStyle.Bottom;

            // Информационная метка
            infoLabel = new Label();
            infoLabel.Text = "Гостевой доступ. Только просмотр товаров.";
            infoLabel.Font = new Font("Arial", 9, FontStyle.Italic);
            infoLabel.ForeColor = Color.DarkGray;
            infoLabel.AutoSize = true;
            infoLabel.Dock = DockStyle.Fill;
            infoLabel.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void SetupDataGridView()
        {
            // Очищаем существующие колонки
            productsDataGridView.Columns.Clear();

            // Создаем колонки (без данных, как требуется в задании)
            DataGridViewColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "Id";
            idColumn.HeaderText = "ID";
            idColumn.Width = 50;

            DataGridViewColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Name = "Name";
            nameColumn.HeaderText = "Название товара";
            nameColumn.Width = 200;

            DataGridViewColumn categoryColumn = new DataGridViewTextBoxColumn();
            categoryColumn.Name = "Category";
            categoryColumn.HeaderText = "Категория";
            categoryColumn.Width = 120;

            DataGridViewColumn sizeColumn = new DataGridViewTextBoxColumn();
            sizeColumn.Name = "Size";
            sizeColumn.HeaderText = "Размер";
            sizeColumn.Width = 80;

            DataGridViewColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "Price";
            priceColumn.HeaderText = "Цена (руб.)";
            priceColumn.Width = 100;
            priceColumn.DefaultCellStyle.Format = "N2";
            priceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewColumn colorColumn = new DataGridViewTextBoxColumn();
            colorColumn.Name = "Color";
            colorColumn.HeaderText = "Цвет";
            colorColumn.Width = 100;

            DataGridViewColumn materialColumn = new DataGridViewTextBoxColumn();
            materialColumn.Name = "Material";
            materialColumn.HeaderText = "Материал";
            materialColumn.Width = 120;

            DataGridViewColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.Name = "Description";
            descriptionColumn.HeaderText = "Описание";
            descriptionColumn.Width = 250;

            // Добавляем колонки в DataGridView
            productsDataGridView.Columns.AddRange(new DataGridViewColumn[] {
                idColumn, nameColumn, categoryColumn, sizeColumn,
                priceColumn, colorColumn, materialColumn, descriptionColumn
            });

            // Настройка стилей
            productsDataGridView.EnableHeadersVisualStyles = false;
            productsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            productsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            productsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            productsDataGridView.ColumnHeadersHeight = 40;

            productsDataGridView.RowTemplate.Height = 35;
            productsDataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
            productsDataGridView.DefaultCellStyle.Padding = new Padding(5);

            // Альтернативный цвет строк
            productsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void SetupLayout()
        {
            // Настройка верхней панели
            FlowLayoutPanel headerFlow = new FlowLayoutPanel();
            headerFlow.Dock = DockStyle.Fill;
            headerFlow.FlowDirection = FlowDirection.LeftToRight;
            headerFlow.Padding = new Padding(20, 0, 20, 0);

            // Добавляем кнопку назад слева
            headerFlow.Controls.Add(backButton);
            headerFlow.SetFlowBreak(backButton, true);

            // Добавляем заголовок по центру
            Panel titlePanel = new Panel();
            titlePanel.Dock = DockStyle.Fill;
            titlePanel.Controls.Add(titleLabel);
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            headerFlow.Controls.Add(titlePanel);

            headerPanel.Controls.Add(headerFlow);

            // Настройка нижней панели
            footerPanel.Controls.Add(infoLabel);

            // Основная панель для DataGridView
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(15);
            mainPanel.Controls.Add(productsDataGridView);

            // Добавление всех панелей на форму
            this.Controls.Add(mainPanel);
            this.Controls.Add(footerPanel);
            this.Controls.Add(headerPanel);
        }

        private void SetupEvents()
        {
            // Обработчик кнопки "Назад"
            backButton.Click += (sender, e) =>
            {
                this.Close();
            };

            // Двойной клик по товару (можно добавить функционал)
            productsDataGridView.CellDoubleClick += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    MessageBox.Show("Для просмотра деталей товара требуется авторизация менеджера.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Центрирование заголовка
            titleLabel.Left = (headerPanel.Width - titleLabel.Width) / 2;
            titleLabel.Top = (headerPanel.Height - titleLabel.Height) / 2;
        }
    }
}