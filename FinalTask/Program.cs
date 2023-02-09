using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FinalTask
{
    class Program
    {
        const string DATABASE_FILENAME = "Students.dat";

        const string FOLDER_NAME = "Students";

        const string EXTENSION = ".txt";


        static void Main(string[] args)
        {
            try
            {
                Console.Write($"Укажите папку, где расположен файл {DATABASE_FILENAME}: ");
                var path = Console.ReadLine();

                var dataBasePath = Path.Combine(path, DATABASE_FILENAME);

                var students = ReadValues(dataBasePath);

                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FOLDER_NAME);

                if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); }

                WriteData(students, folder);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Ошибка: " + exception);
            }
        }

        static Student[] ReadValues(string path)
        {
            Student[] students = null;

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (var fs = new FileStream(path, FileMode.Open))
                {
                    students = (Student [])formatter.Deserialize(fs);    
                }
            }
            return students;
        }

        static void WriteData(Student[] students, string folder)
        {
            foreach (var item in students)
            {
                var filePath = Path.Combine(folder, item.Group + EXTENSION);

                //if (!File.Exists(filePath)) { File.Create(filePath); }

                //using (StreamWriter writer = File.AppendText(filePath))
                //{
                //    writer.WriteLine(item.Name + " " + item.DateOfBirth);
                //}

                if (!File.Exists(filePath))
                {
                    using (FileStream fs = File.Create(filePath))
                    {

                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(item.Name + " " + item.DateOfBirth + Environment.NewLine);
                    fs.Write(info, 0, info.Length);
                }

            }

        }
    }
}
