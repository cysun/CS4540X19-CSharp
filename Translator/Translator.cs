using System;
using System.Collections.Generic;
using System.IO;

namespace Translator
{
    class Translator
    {
        public Dictionary<string, List<string>> Dictionary { get; set; }

        public Translator(String file)
        {
            Dictionary = new Dictionary<string, List<string>>();

            string[] lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                if (line.StartsWith("#")) continue;
                string[] words = line.Split(new char[] { '\t' });
                List<string> translations;
                if (Dictionary.TryGetValue(words[0], out translations))
                {
                    translations.Add(words[1]);
                }
                else
                {
                    translations = new List<string>();
                    translations.Add(words[1]);
                    Dictionary.Add(words[0], translations);
                }
            }
        }

        public List<string> Translate(string word)
        {
            List<string> definitions;
            Dictionary.TryGetValue(word, out definitions);
            return definitions;
        }

        static void Main(string[] args)
        {
            Console.Write("Please enter the path to the dictionary file: ");
            Translator translator = new Translator(Console.ReadLine());

            while (true)
            {
                Console.Write("Please enter a word to translate or \"x\" to exit: ");
                string word = Console.ReadLine();
                if (word.ToLower() == "x") break;

                var translations = translator.Translate(word);
                Console.WriteLine("English: {0}", word);
                Console.WriteLine("Spanish:");
                if (translations != null)
                    for (int i = 0; i < translations.Count; ++i)
                        Console.WriteLine("\t{0}. {1}", i + 1, translations[i]);
                else
                    Console.WriteLine("\tNo translation found");
            }
        }
    }
}
