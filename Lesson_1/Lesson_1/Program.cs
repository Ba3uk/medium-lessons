using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        bool CanReturnStream { get; }
    }

    public interface VisualDisplay
    {
        void PrinteInfo(string message);
    }


    public class FolderTXTReceivingInformation : ReceivingInformation
    {
        private List<string> textFileName;

        public FolderTXTReceivingInformation(string path, string expansion)
        {
            textFileName = Directory.GetFiles(path, expansion).ToList();
        }

        public string GetInformation()
        {
            string resultLine;

            if (!IsFilesOver)
            {
                var fileName = textFileName[0];
                if (File.Exists(fileName))
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        resultLine = sr.ReadToEnd();
                        textFileName.Remove(fileName);

                        return resultLine;
                    }
                }
            }

            throw new NullReferenceException();
        }

        public bool IsFilesOver { get => textFileName.Count == 0; }
    }

    public class LineStreamProcessing : StreamProcessing
    {
        private const int MAX_CHAR_ON_LINE = 150;
        private string _allText =string.Empty;        

        public string SetText
        {
            set
            {
                if (_allText.Length == 0)
                    _allText = value;
                else
                {
                    _allText += value;
                }
            }
        }

        public string GetStreamProcessing()
        {
            string resultMessage = string.Empty;
            int lastChar = 0;

            for(int i = 0; i < MAX_CHAR_ON_LINE; i++)
            {
                if (_allText.Length > i)
                {
                    resultMessage += _allText[i];
                }
                else
                {
                    lastChar = i;
                    break;
                }
            }

            _allText = _allText.Substring(lastChar);

            return resultMessage;
        }

        public bool CanReturnStream { get => _allText.Length >= MAX_CHAR_ON_LINE; }

    }

    public class ConsoleDisplay : VisualDisplay
    {
        public void PrinteInfo(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class TextWriter
    {
        private ReceivingInformation _receivingInformation;
        private StreamProcessing _streamProcessing;
        private VisualDisplay _visualDisplay;

        public TextWriter(ReceivingInformation receivingInformation, StreamProcessing streamProcessing, VisualDisplay visualDisplay)
        {
            _receivingInformation = receivingInformation;
            _streamProcessing = streamProcessing;
            _visualDisplay = visualDisplay;
        } 

        public void Update()
        {
            if (!_streamProcessing.CanReturnStream)
            {
                _streamProcessing.SetText =_receivingInformation.GetInformation();
            }

            _visualDisplay.PrinteInfo(_streamProcessing.GetStreamProcessing());

            Thread.Sleep(1000);
        }
    }


    public class asdasd
    {
        public static void Main (string[] args)
        {
            FolderTXTReceivingInformation _tXTReceiving = new FolderTXTReceivingInformation(@"C:\Users\ba3a2\Desktop\test\", "*.txt");
            LineStreamProcessing lineStreamProcessing = new LineStreamProcessing();
            ConsoleDisplay consoleDisplay = new ConsoleDisplay();

            TextWriter textWriter = new TextWriter(_tXTReceiving, lineStreamProcessing, consoleDisplay);

            textWriter.Update();
            textWriter.Update();
            textWriter.Update();
            textWriter.Update();


        }
    }

}
