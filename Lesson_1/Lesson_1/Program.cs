using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2_4
{
    public interface ReceivingInformation
    {
        string GetInformation();
    }

    public interface StreamProcessing
    {
        string SetText { set; }
        string GetStreamProcessing();
    }

    public interface VisualDisplay
    {
        void PrinteInfo(string message);
    }

    public class FolderTXTReceivingInformation : ReceivingInformation
    {
        private string[] textFileName;
        public FolderTXTReceivingInformation(string path, string expansion)
        {
            textFileName = Directory.GetFiles(path, expansion);
            foreach(var fileName in textFileName)
            {
                Console.WriteLine(fileName);
            }
        }
        public string GetInformation()
        {
            return ";";
        }
    }


    public class LineStreamProcessing : StreamProcessing
    {
        private const int MAX_CHAR_ON_LINE = 150;
        private string _allText;        

        public string SetText
        {
            set => _allText = value;
        }

        public string GetStreamProcessing()
        {
            string resultMessage = string.Empty;
            int lastChar = 0;

            for(int i = 0; i < MAX_CHAR_ON_LINE; i++)
            {
                if (_allText.Length < i)
                {
                    resultMessage += _allText[i];
                }
                else
                {
                    lastChar = i;
                    break;
                }
            }

            _allText.Substring(lastChar);

            return resultMessage;
        }
    }

    public class ConsoleDisplay : VisualDisplay
    {
        public void PrinteInfo(string message)
        {
            Console.WriteLine(message);
        }
    }


    public class asdasd
    {
        public static void Main (string[] args)
        {
            FolderTXTReceivingInformation ds = new FolderTXTReceivingInformation(@"C:\Users\ba3a2\Desktop\test\", "*.txt");
        }
    }

}
