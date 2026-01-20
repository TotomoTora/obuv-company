using System;
using System.Windows.Forms;

namespace ObuvCompany
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Форма выбора пользователя (гость или менеджер)
            Application.Run(new UserSelectionForm());
        }
    }
}