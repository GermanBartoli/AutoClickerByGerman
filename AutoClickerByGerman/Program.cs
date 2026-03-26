namespace AutoClickerByGerman
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            while (true)
            {
                using var menuPrincipal = new MenuPrincipalForm();
                menuPrincipal.ShowDialog();

                if (menuPrincipal.FormularioSeleccionado is not Form formularioSeleccionado)
                {
                    break;
                }

                using (formularioSeleccionado)
                {
                    formularioSeleccionado.ShowDialog();
                }

                if (formularioSeleccionado is AqwForm)
                {
                    break;
                }
            }
        }
    }
}
