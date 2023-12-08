using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac_16
{
    public class FileChangesLogger
    {
        private string directoryToWatch;
        private string logFilePath;
        private FileSystemWatcher watcher;

        public FileChangesLogger(string directoryToWatch, string logFilePath)
        {
            this.directoryToWatch = directoryToWatch;
            this.logFilePath = logFilePath;

            watcher = new FileSystemWatcher(directoryToWatch);

            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            watcher.IncludeSubdirectories = true;

            watcher.Created += OnFileChanged;
            watcher.Deleted += OnFileChanged;
            watcher.Renamed += OnFileRenamed;
            watcher.Changed += OnFileChanged;
        }

        /// <summary>
        /// Обработчик события создания, удаления и изменения файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            LogChange($"{DateTime.Now} - {e.ChangeType} - {e.FullPath}");
        }

        /// <summary>
        /// Обработчик события переименования файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            LogChange($"{DateTime.Now} - {e.ChangeType} - {e.OldFullPath} -> {e.FullPath}");
        }

        /// <summary>
        /// Метод для записи изменений в лог-файл
        /// </summary>
        /// <param name="logEntry"></param>
        private void LogChange(string logEntry)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог: {ex.Message}");
            }
        }

        /// <summary>
        /// Метод для запуска отслеживания изменений
        /// </summary>
        public void StartWatching()
        {
            try
            {
                Console.WriteLine($"Отслеживание изменений в директории {directoryToWatch}. Для остановки нажмите Enter.");

                watcher.EnableRaisingEvents = true;

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запуске отслеживания: {ex.Message}");
            }
            finally
            {
                watcher.EnableRaisingEvents = false;
            }
        }
    }
}
