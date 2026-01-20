using System;
using System.Drawing;
using System.Windows.Forms;

namespace ObuvCompany
{
    public partial class ManagerForm : Form
    {
        // Основные элементы
        private DataGridView productsDataGridView;
        private Panel controlPanel;
        private Panel gridPanel;
        private Button backButton;
        private Label titleLabel;
        private Panel headerPanel;

        // Элементы управления для поиска и фильтрации
        private TextBox searchTextBox;
        private ComboBox sortComboBox;
        private ComboBox filterComboBox;
        private ComboBox filterByComboBox;
        private Button searchButton;
        private Button resetButton;
        private Button addButton;
        private Button deleteButton;
        private Button saveButton;

        // Метки
        private Label searchLabel;
        private Label sortLabel;
        private Label filterLabel;
        private Label filterByLabel;

        // Панель статуса
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel recordCountLabel;

        public ManagerForm()
        {
            InitializeComponents();
            SetupDataGridView();
            SetupLayout();
            SetupEvents();
            SetupStatusBar();
        }

        private void InitializeComponents()
        {
            // Настройка формы
            this.Text = "ООО 'Обувь' - Панель управления менеджера";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 700);
            this.BackColor = Color.White;

            // Верхняя панель
            headerPanel = new Panel();
            headerPanel.BackColor = Color.DarkSlateBlue;
            headerPanel.Height = 80;
            headerPanel.Dock = DockStyle.Top;

            // Заголовок
            titleLabel = new Label();
            titleLabel.Text = "Управление товарами";
            titleLabel.Font = new Font("Arial", 18, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;

            // Кнопка назад
            backButton = new Button();
            backButton.Text = "Назад";
            backButton.Size = new Size(80, 30);
            backButton.Font = new Font("Arial", 10, FontStyle.Regular);
            backButton.BackColor = Color.White;
            backButton.ForeColor = Color.DarkSlateBlue;
            backButton.Cursor = Cursors.Hand;

            // Панель управления (верхняя)
            controlPanel = new Panel();
            controlPanel.BackColor = Color.FromArgb(240, 240, 240);
            controlPanel.Height = 120;
            controlPanel.Dock = DockStyle.Top;
            controlPanel.Padding = new Padding(15);

            // Метки
            searchLabel = new Label();
            searchLabel.Text = "Поиск:";
            searchLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            searchLabel.AutoSize = true;

            sortLabel = new Label();
            sortLabel.Text = "Сортировка:";
            sortLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            sortLabel.AutoSize = true;

            filterLabel = new Label();
            filterLabel.Text = "Фильтр:";
            filterLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            filterLabel.AutoSize = true;

            filterByLabel = new Label();
            filterByLabel.Text = "По полю:";
            filterByLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            filterByLabel.AutoSize = true;


            // Комбобокс сортировки (пустой - не заполнять как в задании)
            sortComboBox = new ComboBox();
            sortComboBox.Width = 180;
            sortComboBox.Height = 25;
            sortComboBox.Font = new Font("Arial", 10);
            sortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Комбобокс фильтра (пустой)
            filterComboBox = new ComboBox();
            filterComboBox.Width = 150;
            filterComboBox.Height = 25;
            filterComboBox.Font = new Font("Arial", 10);
            filterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Комбобокс "Фильтровать по" (пустой)
            filterByComboBox = new ComboBox();
            filterByComboBox.Width = 150;
            filterByComboBox.Height = 25;
            filterByComboBox.Font = new Font("Arial", 10);
            filterByComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Кнопки управления
            searchButton = new Button();
            searchButton.Text = "Найти";
            searchButton.Size = new Size(80, 25);
            searchButton.Font = new Font("Arial", 10);
            searchButton.BackColor = Color.SteelBlue;
            searchButton.ForeColor = Color.White;
            searchButton.Cursor = Cursors.Hand;

            resetButton = new Button();
            resetButton.Text = "Сброс";
            resetButton.Size = new Size(80, 25);
            resetButton.Font = new Font("Arial", 10);
            resetButton.BackColor = Color.Gray;
            resetButton.ForeColor = Color.White;
            resetButton.Cursor = Cursors.Hand;

            addButton = new Button();
            addButton.Text = "Добавить";
            addButton.Size = new Size(100, 30);
            addButton.Font = new Font("Arial", 10, FontStyle.Bold);
            addButton.BackColor = Color.ForestGreen;
            addButton.ForeColor = Color.White;
            addButton.Cursor = Cursors.Hand;

            deleteButton = new Button();
            deleteButton.Text = "Удалить";
            deleteButton.Size = new Size(100, 30);
            deleteButton.Font = new Font("Arial", 10, FontStyle.Bold);
            deleteButton.BackColor = Color.Crimson;
            deleteButton.ForeColor = Color.White;
            deleteButton.Cursor = Cursors.Hand;

            saveButton = new Button();
            saveButton.Text = "Сохранить";
            saveButton.Size = new Size(100, 30);
            saveButton.Font = new Font("Arial", 10, FontStyle.Bold);
            saveButton.BackColor = Color.DodgerBlue;
            saveButton.ForeColor = Color.White;
            saveButton.Cursor = Cursors.Hand;

            // DataGridView
            productsDataGridView = new DataGridView();
            productsDataGridView.Dock = DockStyle.Fill;
            productsDataGridView.ReadOnly = false; // Менеджер может редактировать
            productsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            productsDataGridView.AllowUserToAddRows = true;
            productsDataGridView.AllowUserToDeleteRows = true;
            productsDataGridView.BackgroundColor = Color.White;
            productsDataGridView.BorderStyle = BorderStyle.Fixed3D;

            // Панель для DataGridView
            gridPanel = new Panel();
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.Padding = new Padding(10);
        }

        private void SetupDataGridView()
        {
            // Очищаем существующие колонки
            productsDataGridView.Columns.Clear();

            // Создаем колонки (без данных)
            DataGridViewColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "Id";
            idColumn.HeaderText = "ID";
            idColumn.Width = 50;
            idColumn.ReadOnly = true;

            DataGridViewColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Name = "Name";
            nameColumn.HeaderText = "Название товара";
            nameColumn.Width = 200;

            DataGridViewColumn categoryColumn = new DataGridViewComboBoxColumn();
            categoryColumn.Name = "Category";
            categoryColumn.HeaderText = "Категория";
            categoryColumn.Width = 120;

            DataGridViewColumn sizeColumn = new DataGridViewTextBoxColumn();
            sizeColumn.Name = "Size";
            sizeColumn.HeaderText = "Размер";
            sizeColumn.Width = 70;

            DataGridViewColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "Price";
            priceColumn.HeaderText = "Цена";
            priceColumn.Width = 90;
            priceColumn.DefaultCellStyle.Format = "N2";
            priceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewColumn quantityColumn = new DataGridViewTextBoxColumn();
            quantityColumn.Name = "Quantity";
            quantityColumn.HeaderText = "Кол-во";
            quantityColumn.Width = 70;
            quantityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewColumn supplierColumn = new DataGridViewTextBoxColumn();
            supplierColumn.Name = "Supplier";
            supplierColumn.HeaderText = "Поставщик";
            supplierColumn.Width = 150;

            DataGridViewColumn dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.Name = "DateAdded";
            dateColumn.HeaderText = "Дата поступления";
            dateColumn.Width = 120;

            DataGridViewColumn inStockColumn = new DataGridViewCheckBoxColumn();
            inStockColumn.Name = "InStock";
            inStockColumn.HeaderText = "В наличии";
            inStockColumn.Width = 80;

            // Добавляем колонки в DataGridView
            productsDataGridView.Columns.AddRange(new DataGridViewColumn[] {
                idColumn, nameColumn, categoryColumn, sizeColumn,
                priceColumn, quantityColumn, supplierColumn,
                dateColumn, inStockColumn
            });

            // Настройка стилей
            productsDataGridView.EnableHeadersVisualStyles = false;
            productsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateBlue;
            productsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            productsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            productsDataGridView.ColumnHeadersHeight = 40;

            productsDataGridView.RowTemplate.Height = 30;
            productsDataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
            productsDataGridView.DefaultCellStyle.Padding = new Padding(3);

            // Альтернативный цвет строк
            productsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // Стиль для редактируемых ячеек
            productsDataGridView.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            productsDataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void SetupLayout()
        {
            // Настройка верхней панели с заголовком
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

            // Настройка панели управления
            TableLayoutPanel controlTable = new TableLayoutPanel();
            controlTable.Dock = DockStyle.Fill;
            controlTable.ColumnCount = 8;
            controlTable.RowCount = 2;
            controlTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60)); // searchLabel
            controlTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200)); // searchTextBox
            controlTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80)); // searchButton
            controlTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90)); // sortLabel
            controlTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180)); // sortComboBox
            controlTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60)); // filterLabel
            controlTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150)); // filterComboBox
            controlTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // buttons

            controlTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            controlTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            // Первая строка: поиск и сортировка
            controlTable.Controls.Add(searchLabel, 0, 0);
            controlTable.Controls.Add(searchTextBox, 1, 0);
            controlTable.Controls.Add(searchButton, 2, 0);
            controlTable.Controls.Add(sortLabel, 3, 0);
            controlTable.Controls.Add(sortComboBox, 4, 0);
            controlTable.Controls.Add(filterLabel, 5, 0);
            controlTable.Controls.Add(filterComboBox, 6, 0);

            // Вторая строка: фильтрация и кнопки действий
            controlTable.Controls.Add(filterByLabel, 0, 1);
            controlTable.Controls.Add(filterByComboBox, 1, 1);
            controlTable.Controls.Add(resetButton, 2, 1);

            // Панель для кнопок действий
            FlowLayoutPanel actionButtonsPanel = new FlowLayoutPanel();
            actionButtonsPanel.Dock = DockStyle.Fill;
            actionButtonsPanel.FlowDirection = FlowDirection.LeftToRight;
            actionButtonsPanel.Padding = new Padding(10, 0, 0, 0);
            actionButtonsPanel.Controls.Add(addButton);
            actionButtonsPanel.Controls.Add(deleteButton);
            actionButtonsPanel.Controls.Add(saveButton);

            controlTable.Controls.Add(actionButtonsPanel, 7, 1);
            controlTable.SetColumnSpan(actionButtonsPanel, 1);

            // Выравнивание по вертикали
            searchLabel.TextAlign = ContentAlignment.MiddleRight;
            sortLabel.TextAlign = ContentAlignment.MiddleRight;
            filterLabel.TextAlign = ContentAlignment.MiddleRight;
            filterByLabel.TextAlign = ContentAlignment.MiddleRight;

            controlPanel.Controls.Add(controlTable);

            // Настройка панели с DataGridView
            gridPanel.Controls.Add(productsDataGridView);

            // Добавление всех панелей на форму
            this.Controls.Add(gridPanel);
            this.Controls.Add(controlPanel);
            this.Controls.Add(headerPanel);
        }

        private void SetupStatusBar()
        {
            // Создаем статус бар
            statusStrip = new StatusStrip();
            statusStrip.BackColor = Color.LightGray;
            statusStrip.Dock = DockStyle.Bottom;

            // Метка статуса
            statusLabel = new ToolStripStatusLabel();
            statusLabel.Text = "Готово";
            statusLabel.Spring = true;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            // Метка количества записей
            recordCountLabel = new ToolStripStatusLabel();
            recordCountLabel.Text = "Записей: 0";
            recordCountLabel.TextAlign = ContentAlignment.MiddleRight;

            // Добавляем элементы в статус бар
            statusStrip.Items.Add(statusLabel);
            statusStrip.Items.Add(recordCountLabel);

            this.Controls.Add(statusStrip);
        }

        private void SetupEvents()
        {
            // Обработчик кнопки "Назад"
            backButton.Click += (sender, e) =>
            {
                this.Close();
            };

            // Обработчик кнопки "Найти"
            searchButton.Click += (sender, e) =>
            {
                statusLabel.Text = "Выполняется поиск...";
                // Здесь будет логика поиска
            };

            // Обработчик кнопки "Сброс"
            resetButton.Click += (sender, e) =>
            {
                searchTextBox.Clear();
                sortComboBox.SelectedIndex = -1;
                filterComboBox.SelectedIndex = -1;
                filterByComboBox.SelectedIndex = -1;
                statusLabel.Text = "Фильтры сброшены";
            };

            // Обработчик кнопки "Добавить"
            addButton.Click += (sender, e) =>
            {
                // Добавление новой строки
                productsDataGridView.Rows.Add();
                UpdateRecordCount();
                statusLabel.Text = "Добавлена новая запись";
            };

            // Обработчик кнопки "Удалить"
            deleteButton.Click += (sender, e) =>
            {
                if (productsDataGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in productsDataGridView.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            productsDataGridView.Rows.Remove(row);
                        }
                    }
                    UpdateRecordCount();
                    statusLabel.Text = "Удалены выбранные записи";
                }
                else
                {
                    MessageBox.Show("Выберите строки для удаления",
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Обработчик кнопки "Сохранить"
            saveButton.Click += (sender, e) =>
            {
                // Здесь будет логика сохранения
                statusLabel.Text = "Данные сохранены";
                MessageBox.Show("Изменения сохранены успешно!",
                    "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Обработчик изменения текста поиска
            searchTextBox.TextChanged += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(searchTextBox.Text))
                {
                    statusLabel.Text = "Поиск: " + searchTextBox.Text;
                }
            };

            // Обработчик выбора в комбобоксе сортировки
            sortComboBox.SelectedIndexChanged += (sender, e) =>
            {
                if (sortComboBox.SelectedIndex >= 0)
                {
                    statusLabel.Text = "Сортировка по: " + sortComboBox.Text;
                }
            };

            // Обработчик изменения данных в DataGridView
            productsDataGridView.CellValueChanged += (sender, e) =>
            {
                statusLabel.Text = "Изменена ячейка [" + e.RowIndex + "," + e.ColumnIndex + "]";
            };

            // Обработчик загрузки формы
            this.Load += (sender, e) =>
            {
                UpdateRecordCount();
            };
        }

        private void UpdateRecordCount()
        {
            int rowCount = productsDataGridView.Rows.Count;
            if (productsDataGridView.AllowUserToAddRows)
            {
                rowCount--; // Не учитываем новую строку
            }
            recordCountLabel.Text = $"Записей: {rowCount}";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Центрирование заголовка
            titleLabel.Left = (headerPanel.Width - titleLabel.Width) / 2;
            titleLabel.Top = (headerPanel.Height - titleLabel.Height) / 2;

            // Обновление количества записей
            UpdateRecordCount();
        }
    }
}