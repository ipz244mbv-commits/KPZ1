namespace Geometry_Dash
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            try
            {
                Application.Run(new StartMenuForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка запуску: " + ex.Message);
            }
        }
    }
}
