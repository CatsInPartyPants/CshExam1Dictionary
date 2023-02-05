using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshExam1Dictionary
{
    internal class DictionaryWorker
    {
        DictionaryTranslate dict = new DictionaryTranslate();
        public DictionaryWorker()
        {
            Console.WriteLine("Введите тип словаря: Русско-английский или англо-русский: ");
            string temp = Console.ReadLine();
            dict = new DictionaryTranslate(temp);
            if(File.Exists("Dictionary.txt"))
            {
                using(StreamReader sr = File.OpenText("Dictionary.txt"))
                {
                    string tempAll = sr.ReadToEnd();
                    Console.WriteLine(tempAll);
                    string[] tempLines = tempAll.Split('\n');
                    foreach(string line in tempLines)
                    {
                        Console.WriteLine(line);
                        try
                        {
                            string[] tempDict = line.Split(':');
                            string[] tempTranslates = tempDict[1].Split(' ');
                            foreach (string translate in tempTranslates)
                            {
                                dict.AddWordAndTranslate(tempDict[0], translate);
                            }
                        }catch(Exception ex) { }
                    }
                }
            }
        }

        public void ShowMenu()
        {
           int choise = -1;
            do
            {
                Console.Clear();
                Console.WriteLine($"{dict.ShowType()} словарь.");
                Console.WriteLine($"1. Показать весь словарь.");
                Console.WriteLine($"2. Добавить слово в словарь.");
                Console.WriteLine($"3. Добавить перевод слова в словарь.");
                Console.WriteLine($"4. Заменить слово в словаре.");
                Console.WriteLine($"5. Заменить перевод слова в словаре.");
                Console.WriteLine($"6. Удалить слово из словаря.");
                Console.WriteLine($"7. Удалить перевод из словаря.");
                Console.WriteLine($"8. Искать перевод слова.");
                Console.WriteLine($"9. Искать перевод слова с сохранением в файл.");
                Console.WriteLine($"10. Перевести фразу.");
                Console.WriteLine($"0. Выход");

                choise = Int32.Parse(Console.ReadLine());

                switch(choise)
                {
                    case 1:
                        dict.ShowAll();
                        Console.ReadKey();
                        break;
                    case 2:
                        string tempWord;
                        string tempTranslate;
                        Console.WriteLine("Введите слово для добавления в словарь:");
                        tempWord = Console.ReadLine();
                        Console.WriteLine("Введите перевод этого слова для добавления в словарь:");
                        tempTranslate = Console.ReadLine();
                        dict.AddWordAndTranslate(tempWord, tempTranslate);
                        break;
                    case 3:
                        string tempWord3;
                        string tempTranslate3;
                        Console.WriteLine("Перевод какого слова вы хотите добавить:");
                        tempWord3 = Console.ReadLine();
                        Console.WriteLine("Введите значение перевода, для добавления:");
                        tempTranslate3 = Console.ReadLine();
                        dict.AddWordAndTranslate(tempWord3, tempTranslate3);
                        break;
                    case 4:
                        string tempWord4;
                        string tempWordForChange4;
                        Console.WriteLine("Какое слово вы хотите заменить:");
                        tempWord4 = Console.ReadLine();
                        Console.WriteLine("На какое слово произвести замену:");
                        tempWordForChange4 = Console.ReadLine();
                        dict.ChangeWord(tempWord4, tempWordForChange4);
                        break;
                    case 5:
                        string tempWord5;
                        string tempTranslate5;
                        string tempTranslateForChange5;
                        Console.WriteLine("У какого слова вы хотите произвести замену перевода:");
                        tempWord5 = Console.ReadLine();
                        Console.WriteLine("Какой перевод вы хотите заменить:");
                        tempTranslate5 = Console.ReadLine();
                        Console.WriteLine("На какой::");
                        tempTranslateForChange5 = Console.ReadLine();
                        dict.ChangeTranslate(tempWord5, tempTranslate5, tempTranslateForChange5);
                        break;
                    case 6:
                        string tempWord6;
                        Console.WriteLine("Какое слово удалить:");
                        tempWord6 = Console.ReadLine();
                        dict.DeleteWord(tempWord6);
                        break;
                    case 7:
                        string tempWord7;
                        Console.WriteLine("Какой перевод удалить:");
                        tempWord7 = Console.ReadLine();
                        dict.DeleteTranslate(tempWord7);
                        break;
                    case 8:
                        string tempWord8;
                        Console.WriteLine("Перевод какого слова искать?");
                        tempWord8 = Console.ReadLine();
                        dict.SearchTranslate(tempWord8);
                        Console.ReadKey();
                        break;
                    case 9:
                        string tempWord9;
                        Console.WriteLine("Перевод какого слова искать и сохранить результат в файл?");
                        tempWord9 = Console.ReadLine();
                        dict.CreateFileWithResult(tempWord9);
                        break;
                    case 10:
                        string tempPhrase;
                        string translatedPhrase;
                        Console.WriteLine("Введите фразу для перевода:");
                        tempPhrase = Console.ReadLine();
                        translatedPhrase = dict.TranslatePhrase(tempPhrase);
                        Console.WriteLine(translatedPhrase);
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Try again");
                        break;
                }
            } while (choise != 0);
            dict.SaveToFile();
        }
    }
}
