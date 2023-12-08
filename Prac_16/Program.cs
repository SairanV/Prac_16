using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac_16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Приложение для логирования изменений в файлах");

                Console.Write("Введите путь к отслеживаемой директории: ");
                string directoryToWatch = Console.ReadLine();

                Console.Write("Введите путь к лог-файлу: ");
                string logFilePath = Console.ReadLine();

                FileChangesLogger fileChangesLogger = new FileChangesLogger(directoryToWatch, logFilePath);
                fileChangesLogger.StartWatching();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
