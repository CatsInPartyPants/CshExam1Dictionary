using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshExam1Dictionary
{
    public interface IDictionary
    {
        void AddWordAndTranslate(string a, string b);
        void ShowAll();
        void ChangeWord(string w, string ch);
        void ChangeTranslate(string w, string t, string ch);
        void DeleteWord(string w);
        void DeleteTranslate(string w);

        void SearchTranslate(string a);

        void SaveToFile();
        void CreateFileWithResult(string w);
    }

    internal class DictionaryTranslate : IDictionary
    {
        string _dictionaryType;
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        public DictionaryTranslate() { }
        public DictionaryTranslate(string dictType)
        {
            dictionary = new Dictionary<string, List<string>>();
            _dictionaryType = dictType;
        }
        
        public void AddWordAndTranslate(string word, string translate)
        {
            if (dictionary.ContainsKey(word))
            {
                dictionary[word].Add(translate);
            }
            else
            {
                dictionary[word] = new List<string>();
                dictionary[word].Add(translate);
            }
        }

        public void ShowAll()
        {
            Console.WriteLine($"Тип словаря: {_dictionaryType}");
            foreach (string word in dictionary.Keys)
            {
                Console.Write($"{word}: ");
                foreach (string translate in dictionary[word])
                {
                    Console.Write(translate + " ");
                }
                Console.WriteLine();
            }
            
        }

        public void ChangeTranslate(string w, string t, string ch)
        {
            foreach(string word in dictionary.Keys)
            {
                if(word == w)
                {
                    foreach(string translate in dictionary[word])
                    {
                        if(translate == t)
                        {
                            dictionary[word].Add(ch);
                            dictionary[word].Remove(t);
                            break;
                        }
                    }
                }
            }
        }

        public void ChangeWord(string w, string changed)
        {
            foreach(string word in dictionary.Keys)
            {
                if(word == w)
                {
                    List<string> temp = dictionary[word];
                    dictionary.Remove(word);
                    dictionary.Add(changed, temp);
                    break;
                }
            }
        }

        public void DeleteTranslate(string w)
        {
            foreach(string word in dictionary.Keys)
            {
                foreach(string translate in dictionary[word])
                {
                    if(translate == w)
                    {
                        if(dictionary[word].Count >=2)
                        {
                            dictionary[word].Remove(translate);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Нельзя удалить перевод '{w}', он последний. В случае необходимости - удалите все слово полностью");
                        }
                    }
                }
            }
        }

        public void DeleteWord(string word)
        {
            foreach(string w in dictionary.Keys)
            {
                if(w == word)
                {
                    dictionary.Remove(w);
                    Console.WriteLine($"Слово {word} удалено из словаря вместе со всеми переводами.");
                    break;
                }
            }
        }

        public void SearchTranslate(string word)
        {
            foreach(string w in dictionary.Keys)
            {
                if(w == word)
                {
                    Console.WriteLine($"Переводы указанного слова {word}:");
                    foreach (string translate in dictionary[word])
                    {
                        Console.Write(translate + " ");
                    }
                    Console.WriteLine();
                }
            }
        }

        public void SaveToFile()
        {
            using(StreamWriter sw = File.CreateText("Dictionary.txt"))
            {
                foreach(string word in dictionary.Keys)
                {
                    sw.Write($"{word}: ");
                    foreach(string translate in dictionary[word])
                    {
                        sw.Write($"{translate} ");
                    }
                    sw.WriteLine();
                }
            }
        }

        public void CreateFileWithResult(string w)
        {
            using (StreamWriter sw = File.CreateText("SearchResult.txt"))
            {
                foreach (string word in dictionary.Keys)
                {
                    if (word == w)
                    {
                        sw.Write($"{word}: ");
                        foreach (string translate in dictionary[word])
                        {
                            sw.Write($"{translate} ");
                        }
                        sw.WriteLine();
                    }
                }
            }
        }

        public string ShowType()
        {
            return $"{_dictionaryType}";
        }

        public string TranslatePhrase(string phrase)
        {
            string[] words = phrase.Split(' ');
            string result = "";
            foreach(string word in words)
            {
                if (dictionary.ContainsKey(word))
                {
                    result += $"{dictionary[word][0]} ";
                }
                else
                {
                    result += $"'{word}'(нет в словаре) ";
                }
            }
            return result;
        }
    }
}
