using System;
using System.Drawing;
using System.Windows.Forms;

namespace ObuvCompany
{
    public partial class UserSelectionForm : Form
    {
        private Button guestButton;
        private Button managerButton;
        private Label titleLabel;
        private PictureBox logoPictureBox;

        public UserSelectionForm()
        {
            InitializeComponents();
            SetupLayout();
        }

        private void InitializeComponents()
        {
            // Настройка формы
            this.Text = "ООО 'Обувь' - Вход в систему";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Логотип (заглушка)
            logoPictureBox = new PictureBox();
            logoPictureBox.Size = new Size(150, 150);
            logoPictureBox.BackColor = Color.LightBlue;
            logoPictureBox.BorderStyle = BorderStyle.FixedSingle;

            // Заголовок
            titleLabel = new Label();
            titleLabel.Text = "Добро пожаловать в ООО 'Обувь'";
            titleLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            titleLabel.ForeColor = Color.DarkBlue;
            titleLabel.AutoSize = true;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Кнопка "Гость"
            guestButton = new Button();
            guestButton.Text = "Войти как гость";
            guestButton.Size = new Size(200, 50);
            guestButton.Font = new Font("Arial", 11, FontStyle.Regular);
            guestButton.BackColor = Color.LightGray;
            guestButton.ForeColor = Color.Black;
            guestButton.Cursor = Cursors.Hand;
            guestButton.Click += GuestButton_Click;

            // Кнопка "Менеджер"
            managerButton = new Button();
            managerButton.Text = "Войти как менеджер";
            managerButton.Size = new Size(200, 50);
            managerButton.Font = new Font("Arial", 11, FontStyle.Regular);
            managerButton.BackColor = Color.SteelBlue;
            managerButton.ForeColor = Color.White;
            managerButton.Cursor = Cursors.Hand;
            managerButton.Click += ManagerButton_Click;
        }

        private void SetupLayout()
        {
            // Создание основной панели
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);

            // Используем TableLayoutPanel для центрирования
            TableLayoutPanel tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.ColumnCount = 1;
            tableLayout.RowCount = 4;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15));

            // Центрируем элементы в ячейках
            logoPictureBox.Dock = DockStyle.Fill;
            titleLabel.Dock = DockStyle.Fill;
            guestButton.Dock = DockStyle.Fill;
            managerButton.Dock = DockStyle.Fill;

            // Добавляем элементы в таблицу
            tableLayout.Controls.Add(logoPictureBox, 0, 0);
            tableLayout.Controls.Add(titleLabel, 0, 1);
            tableLayout.Controls.Add(guestButton, 0, 2);
            tableLayout.Controls.Add(managerButton, 0, 3);

            // Центрируем содержимое в ячейках
            logoPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            guestButton.TextAlign = ContentAlignment.MiddleCenter;
            managerButton.TextAlign = ContentAlignment.MiddleCenter;

            mainPanel.Controls.Add(tableLayout);
            this.Controls.Add(mainPanel);
        }

        private void GuestButton_Click(object sender, EventArgs e)
        {
            // Открываем форму гостя
            GuestForm guestForm = new GuestForm();
            guestForm.Show();
            this.Hide();

            // Обработка закрытия формы гостя
            guestForm.FormClosed += (s, args) => this.Show();
        }

        private void ManagerButton_Click(object sender, EventArgs e)
        {
            // Открываем форму менеджера
            //ManagerForm managerForm = new ManagerForm();
            //managerForm.Show();
            //this.Hide();

            // Обработка закрытия формы менеджера
            //managerForm.FormClosed += (s, args) => this.Show();
        }
    }
}
