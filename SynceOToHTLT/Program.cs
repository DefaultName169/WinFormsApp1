using Microsoft.Extensions.Configuration;
using SynceOToHTLT.Common;

namespace SynceOToHTLT
{
    internal static class Program
    {
        public static AppSettings AppSettings = new AppSettings();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.Build().Bind(AppSettings);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}