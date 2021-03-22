using System;
using System.Configuration;
using System.Reflection;

namespace HW_08._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Hi();
            veification();
            var appSet = ConfigurationManager.AppSettings;
         
            Version();
            static void Hi()
            {
                var reedGreeting = ConfigurationManager.AppSettings;
                string value = reedGreeting["Greeting"];
                string poloski = new string('-', 15);
                Console.WriteLine($"\n\n{poloski} {value} {poloski}\n");
            }
            static void veification()
            {

                var appSet = ConfigurationManager.AppSettings;
                string[,] array = { { "UserName", "Age", "Profision" }, { "Введите Ваше имя", "Введите ваш возраст", "Ваша профессия" }, { "Имя респондента:", "Возраст респондента:", "Профессия респондента:" } };

                if (appSet.Count == 1)
                {
                    Console.WriteLine("Ввод данных: ");

                    string k, v;
                    for (int i = 0; i < array.GetLength(1); i++)
                    {
                        k = array[0, i];
                        Console.WriteLine(array[1, i]);
                        v = Console.ReadLine();
                        Load(k, v);
                    }
                    Console.WriteLine("Данные сохранены: ");
                }
                else
                {
                    for (int i = 0; i < array.GetLength(1); i++)
                    {
                        Console.WriteLine($"key {appSet.AllKeys[i + 1]} {array[2, i]} {appSet[array[0, i]]}");//appSet.AllKeys[i + 1] 
                    }
                    Console.WriteLine($"Очистить данные? Y/N");
                    Reset(Console.ReadLine());
                }

            }
            static void Load(string Key, string Value)
            {
                var UserName_Value = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var AppSettings = UserName_Value.AppSettings.Settings;

                try
                {
                    if (AppSettings[Key] == null)
                    {
                        AppSettings.Add(Key, Value);
                    }
                    else
                    {
                        AppSettings[Key].Value = Value;
                    }
                    UserName_Value.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(UserName_Value.AppSettings.SectionInformation.Name);


                }
                catch (ConfigurationErrorsException)
                {
                    Console.WriteLine("Ошибка записи");
                }

            }
            static void Reset(string YesNo)
            {

                if (YesNo == "Y" || YesNo == "N" || YesNo == "y" || YesNo == "n")
                {
                    if (YesNo == "Y" || YesNo == "y")
                    {
                        var UserName_Value = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        var AppSettings = UserName_Value.AppSettings.Settings;
                        AppSettings.Remove("UserName");
                        AppSettings.Remove("Age");
                        AppSettings.Remove("Profision");
                        UserName_Value.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection(UserName_Value.AppSettings.SectionInformation.Name);
                        Console.WriteLine("Данные стерты!!!");

                    }
                    if (YesNo == "N" || YesNo == "n")
                    {
                        Console.WriteLine("Данные оставляем без изменений");
                    }
                }
                else
                {
                    Console.WriteLine("Неверная команда!!!");
                }
            }
            static void Version()
            {
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                string Name = Assembly.GetExecutingAssembly().GetName().Name.ToString();
                //string CultureInfo = Assembly.GetExecutingAssembly().GetName().KeyPair.ToString();

                Console.WriteLine($"\nНазвание приложения {Name } version: {version}");
            }
            Console.ReadLine();
        }

    }
}
