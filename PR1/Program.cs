using System;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;

class Program
{
    static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nГлавное меню:");
            Console.WriteLine("1. Работа с текстовыми файлами");
            Console.WriteLine("2. Работа с JSON файлами");
            Console.WriteLine("3. Работа с XML файлами");
            Console.WriteLine("4. Работа с ZIP архивами");
            Console.WriteLine("5. Информация о логических дисках");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    TextFileMenu();
                    break;
                case "2":
                    JsonFileMenu();
                    break;
                case "3":
                    XmlFileMenu();
                    break;
                case "4":
                    ZipFileMenu();
                    break;
                case "5":
                    DisplayDriveInfo();
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    static void TextFileMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\nМеню работы с текстовыми файлами:");
            Console.WriteLine("1. Создать текстовый файл");
            Console.WriteLine("2. Записать в текстовый файл");
            Console.WriteLine("3. Прочитать текстовый файл");
            Console.WriteLine("4. Удалить текстовый файл");
            Console.WriteLine("5. Вернуться в главное меню");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateFileWithExtensionCheck("текстового файла");
                    break;
                case "2":
                    WriteToFile(".txt");
                    break;
                case "3":
                    ReadFile(".txt");
                    break;
                case "4":
                    DeleteFile(".txt");
                    break;
                case "5":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    static void JsonFileMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\nМеню работы с JSON файлами:");
            Console.WriteLine("1. Создать JSON файл");
            Console.WriteLine("2. Записать новый объект в JSON файл");
            Console.WriteLine("3. Прочитать JSON файл");
            Console.WriteLine("4. Удалить JSON файл");
            Console.WriteLine("5. Вернуться в главное меню");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateFileWithExtensionCheck("JSON файла");
                    break;
                case "2":
                    WriteObjectToJson();
                    break;
                case "3":
                    ReadFile(".json");
                    break;
                case "4":
                    DeleteFile(".json");
                    break;
                case "5":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    static void XmlFileMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\nМеню работы с XML файлами:");
            Console.WriteLine("1. Создать XML файл");
            Console.WriteLine("2. Записать данные в XML файл");
            Console.WriteLine("3. Прочитать XML файл");
            Console.WriteLine("4. Удалить XML файл");
            Console.WriteLine("5. Вернуться в главное меню");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateFileWithExtensionCheck("XML файла");
                    break;
                case "2":
                    WriteObjectToXml();
                    break;
                case "3":
                    ReadFile(".xml");
                    break;
                case "4":
                    DeleteFile(".xml");
                    break;
                case "5":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    static void ZipFileMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\nМеню работы с ZIP архивами:");
            Console.WriteLine("1. Создать ZIP архив");
            Console.WriteLine("2. Добавить файл в ZIP архив");
            Console.WriteLine("3. Извлечь файлы из ZIP архива");
            Console.WriteLine("4. Удалить ZIP архив");
            Console.WriteLine("5. Вернуться в главное меню");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateFileWithExtensionCheck("ZIP архива");
                    break;
                case "2":
                    AddToZipArchive();
                    break;
                case "3":
                    ExtractFromZipArchive();
                    break;
                case "4":
                    DeleteFile(".zip");
                    break;
                case "5":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    static void CreateFileWithExtensionCheck(string fileType)
    {
        Console.Write($"Введите имя файла для {fileType} (с правильным расширением): ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName) || !fileName.Contains("."))
        {
            Console.WriteLine("Ошибка: Укажите расширение файла. Например, 'filename.txt' или 'filename.json'.");
            return;
        }

        string expectedExtension = fileType switch
        {
            "текстового файла" => ".txt",
            "JSON файла" => ".json",
            "XML файла" => ".xml",
            "ZIP архива" => ".zip",
            _ => ""
        };

        if (!fileName.EndsWith(expectedExtension, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Ошибка: файл для {fileType} должен иметь расширение {expectedExtension}.");
            return;
        }

        try
        {
            using (var stream = File.Create(fileName))
            {
                Console.WriteLine($"Файл {fileName} успешно создан.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
        }
    }

    static void WriteToFile(string extension)
    {
        Console.Write($"Введите имя файла с расширением {extension}: ");
        string fileName = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        Console.Write("Введите строку для записи в файл: ");
        string input = Console.ReadLine();
        File.WriteAllText(fileName, input);
        Console.WriteLine("Строка успешно записана в файл.");
    }

    static void ReadFile(string extension)
    {
        Console.Write($"Введите имя файла с расширением {extension}: ");
        string fileName = Console.ReadLine();

        if (File.Exists(fileName))
        {
            string content = File.ReadAllText(fileName);
            Console.WriteLine($"Содержимое файла:\n{content}");
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    static void DeleteFile(string extension)
    {
        Console.Write($"Введите имя файла с расширением {extension}: ");
        string fileName = Console.ReadLine();

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
            Console.WriteLine("Файл успешно удален.");
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    static void WriteObjectToJson()
    {
        Console.Write("Введите имя JSON файла: ");
        string fileName = Console.ReadLine();

        if (!fileName.EndsWith(".json"))
        {
            Console.WriteLine("Ошибка: файл должен иметь расширение .json.");
            return;
        }

        Console.Write("Введите данные для записи (например, privet): ");
        string userInput = Console.ReadLine();

        var jsonObject = new
        {
            message = userInput
        };

        try
        {
            string jsonData = JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(fileName, jsonData, Encoding.UTF8);
            Console.WriteLine("Данные успешно записаны в JSON файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в JSON файл: {ex.Message}");
        }
    }


    static void WriteObjectToXml()
    {
        Console.Write("Введите имя XML файла: ");
        string fileName = Console.ReadLine();

        if (!fileName.EndsWith(".xml"))
        {
            Console.WriteLine("Ошибка: файл должен иметь расширение .xml.");
            return;
        }

        Console.Write("Введите данные для записи (например, privet): ");
        string userInput = Console.ReadLine();

        try
        {
            // Настройки для записи XML с форматированием
            var settings = new XmlWriterSettings
            {
                Indent = true, // Включаем отступы для читабельности
                Encoding = Encoding.UTF8, // Кодировка UTF-8
                NewLineOnAttributes = false // Атрибуты будут в той же строке
            };

            // Создаем XmlWriter для записи данных
            using (var writer = XmlWriter.Create(fileName, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("MessageData"); // Корневой элемент

                writer.WriteElementString("Message", userInput); // Элемент с данными пользователя

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            Console.WriteLine("Данные успешно записаны в XML файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в XML файл: {ex.Message}");
        }
    }


    static void AddToZipArchive()
    {
        Console.Write("Введите имя ZIP архива: ");
        string zipFileName = Console.ReadLine();
        if (File.Exists(zipFileName))
        {
            Console.Write("Введите путь к файлу для добавления в архив: ");
            string filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                try
                {
                    using (var zip = ZipFile.Open(zipFileName, ZipArchiveMode.Update))
                    {
                        zip.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                        Console.WriteLine("Файл добавлен в архив.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении файла в архив: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: указанный файл не найден.");
            }
        }
        else
        {
            Console.WriteLine("Ошибка: архив не найден.");
        }
    }

    static void ExtractFromZipArchive()
    {
        Console.Write("Введите имя ZIP архива: ");
        string zipFileName = Console.ReadLine();

        if (!File.Exists(zipFileName))
        {
            Console.WriteLine("Архив не найден.");
            return;
        }

        Console.Write("Введите путь для извлечения файлов: ");
        string extractPath = Console.ReadLine();

        try
        {
            ZipFile.ExtractToDirectory(zipFileName, extractPath);
            Console.WriteLine("Файлы успешно извлечены.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при извлечении файлов: {ex.Message}");
        }
    }

    static void DisplayDriveInfo()
    {
        Console.WriteLine("Информация о логических дисках:");
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                Console.WriteLine($"\nИмя диска: {drive.Name}");
                Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                Console.WriteLine($"Тип диска: {drive.DriveType}");
                Console.WriteLine($"Свободное место: {ConvertBytes(drive.AvailableFreeSpace)}");
                Console.WriteLine($"Общий размер: {ConvertBytes(drive.TotalSize)}");
            }
        }
    }

    static string ConvertBytes(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}
