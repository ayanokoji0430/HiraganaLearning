using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.IO;

namespace 養護学校アプリ
{
    class Colors_Setup:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SolidColorBrush questionsColor;
        private SolidColorBrush mouseOver_questionsColor;
        private SolidColorBrush choicesColor;
        private SolidColorBrush mouseOver_choicesColor;
        private SolidColorBrush questions_wordColor;
        private SolidColorBrush choices_wordColor;
        private SolidColorBrush backgroundColor;
        private string setup_filePath="Setup.txt";
        
        public SolidColorBrush QuestionsColor
        {
            get { return questionsColor; }
            set
            {
                this.questionsColor = value;
                NotifiyPropertyChanged("QuestionsColor");
                    
            }
        }

        public SolidColorBrush MouseOver_QuestionsColor
        {
            get { return mouseOver_questionsColor; }
            set
            {
                this.mouseOver_questionsColor = value;
                NotifiyPropertyChanged("MouseOver_QuestionsColor");

            }
        }

        public SolidColorBrush ChoicesColor
        {
            get { return choicesColor; }
            set
            {
                this.choicesColor = value;
                NotifiyPropertyChanged("ChoicesColor");

            }
        }

        public SolidColorBrush MouseOver_ChoicesColor
        {
            get { return mouseOver_choicesColor; }
            set
            {
                this.mouseOver_choicesColor = value;
                NotifiyPropertyChanged("MouseOver_ChoicesColor");

            }
        }

        public SolidColorBrush BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                this.backgroundColor = value;
                NotifiyPropertyChanged("BackGroundColor");

            }
        }

        public SolidColorBrush Questions_WordColor
        {
            get { return questions_wordColor; }
            set
            {
                this.questions_wordColor = value;
                NotifiyPropertyChanged("Questions_WordColor");

            }
        }

        public SolidColorBrush Choices_WordColor
        {
            get { return choices_wordColor; }
            set
            {
                this.choices_wordColor = value;
                NotifiyPropertyChanged("Choices_WordColor");

            }
        }


        public Colors_Setup()
        {
            QuestionsColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
            MouseOver_QuestionsColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
            ChoicesColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
            MouseOver_ChoicesColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
            BackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            Questions_WordColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            Choices_WordColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));

            setup_Read();
        }


        //イベントが実装されてれば実行する。
        private void NotifiyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private void setup_Read()
        {
            


            StreamReader sr = null;

            try
            {
                sr = new StreamReader(setup_filePath);
            }
            catch
            {
                return;
            }
            QuestionsColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(sr.ReadLine() ?? "Gray"));
            MouseOver_QuestionsColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(sr.ReadLine() ?? "SkyBlue"));
            ChoicesColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(sr.ReadLine() ?? "Gray"));
            MouseOver_ChoicesColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(sr.ReadLine() ?? "SkyBlue"));
            BackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(sr.ReadLine() ?? "White"));
            Questions_WordColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(sr.ReadLine() ?? "Black"));
            Choices_WordColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(sr.ReadLine() ?? "Black"));
        }


    }
}
