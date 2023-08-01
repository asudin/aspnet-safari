using Savanna.CodeLibrary;
using Savanna.CodeLibrary.Configurations.Savanna;

namespace Savanna.ConsoleApp
{
    internal class Program
    {
        static async Task Main()
        {
            int consoleFontSize = 14;

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleUtils.MaximizeConsoleWindow();
            ConsoleUtils.SetConsoleFontSize(consoleFontSize);
            var service = new GameService();

            //await Task.Run(() => service.StartGame());
        }
    }
}
