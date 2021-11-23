using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample
{
    public class MultiLang
    {
        public enum Language
        {
            Kor,
            Eng
        }

        private static MultiLang instance;

        public static MultiLang Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MultiLang();
                }

                return instance;
            }
        }

        private Dictionary<string, string> languageValues;

        public void Initialize(Language language)
        {
            // read language data form language file
            languageValues = new Dictionary<string, string>();

            languageValues.Add("KeyValue", "DisplayValue");
            languageValues.Add("Hello", "안녕하세요");
            languageValues.Add("ExitQuestionMsg", "Do you want to exit?");
        }

        public string GetText(string defaultValue)
        {
            string displayValue;

            if (languageValues.TryGetValue(defaultValue, out displayValue))
            {
                return displayValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public struct KeyValue
        {
            public const string Hello = "Hello";
            public const string ExitQuestionMsg = "ExitQuestionMsg";
        }
    }
}