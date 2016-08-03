using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;


namespace 養護学校アプリ
{
    /// <summary>
    /// GamePage.xaml の相互作用ロジック
    /// </summary>
    public partial class GamePage : Page
    {
        [DllImport("MikoVoice.dll", CharSet = CharSet.Unicode)]
        private extern static int MikoVoiceOutSync(string text);

        private int CurrentWordcnt=0;  //現在フォーカスが当たっている文字の番号
       // private List<Button> questionList;  //問題のテキストを一文字ずついれるリスト
        private string[] QuestionText; //問題のテキスト。ファイルから読み込む
        private BitmapImage[] QuestionImage;
        private int CurrentQuestionCnt = 0; //現在の問題番号
        private string escapeStr;
        private SoundPlayer complete = new SoundPlayer(Properties.Resources.Complete3);
        private SoundPlayer deden = new SoundPlayer(Properties.Resources.deden2);

        private Hashtable Wrong_Words = new Hashtable();    //問題の単語をキーにした誤答を格納するハッシュテーブル

        public GamePage()
        {
            
            InitializeComponent();
            this.DataContext = new Colors_Setup();
            FileRead fr = new FileRead();   //単語リスト読み込みのインスタンスの生成
            QuestionText = fr.showResult();     //単語リストを読み込んで配列に格納 
            QuestionImage = fr.showImg_Result();
           
            
            gen_question();                 //問題を画面に表示
            shuffle(); //シャッフル
            q_image.Source = QuestionImage[CurrentQuestionCnt];
            
        }
        //
        //グリッドを問題テキストの文字数だけ均等に分割（カラム）・カラムに問題の文字を一文字ずつ割り当て
        //特にいじる必要もないので変数やfor文に関する説明は割愛します
        private void gen_question()
        {
            //deden.Play();
            escapeStr = string.Join("", QuestionText[CurrentQuestionCnt].Split('^'));
            int ColumnNum = escapeStr.Length;
            ColumnDefinition[] ColumnArray = new ColumnDefinition[ColumnNum];
            QuestionFrame.ColumnDefinitions.Clear();
            for (int i = 0; i < ColumnNum; i++)
            {
                ColumnArray[i] = new ColumnDefinition();
                Button btn = new Button();
                btn.FontSize = 100;
                btn.Content = escapeStr[i];
                btn.Style = this.FindResource("ButtonStyle1") as Style;
                btn.Name = "btn" + i;
                btn.Width = 180-(ColumnArray.Length*4);
                btn.Height =btn.Width ;     
                btn.Click += new RoutedEventHandler(Question_Click);
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                QuestionFrame.ColumnDefinitions.Add(ColumnArray[i]);
                Grid.SetColumn(btn, i);
                btn.IsEnabled = false;
                QuestionFrame.Children.Add(btn);

            }
            QuestionFrame.Children[CurrentWordcnt].IsEnabled = true;
        }

        //
        //画面下半分に現れる文字をシャッフル、ここでは座標のシャッフルもどきをしています。
        //特にいじる必要もないので変数やfor文に関する説明は割愛します
        //
        public void shuffle()
        {
            
            char[] dummyChars = dummy_gen();
            Random rnd = new Random();
            int buttoncnt = 7;
            int plusX = (int)((Canvas)dummyCanvas).Width / buttoncnt;
            int plusXresult = plusX;
            for (int i = 0; i < buttoncnt; i++)
            {

                int btnX = rnd.Next(plusXresult - plusX, plusXresult - 100);
                Button btn = new Button();
                btn.Style = this.FindResource("ButtonStyle2") as Style;
                btn.Name = "dummybtn" + buttoncnt;
                btn.Click+= new RoutedEventHandler(dummy_Click);
                int btnY = rnd.Next(((int)((Canvas)dummyCanvas).Height) - 100);
                btn.Content = dummyChars[i];
                dummyCanvas.Children.Add(btn);
                btn.Width = 100;
                btn.Height = 100;
                btn.FontSize = 50;
                ((Button)dummyCanvas.Children[i]).Margin = new Thickness(btnX, btnY, 0, 0);
                plusXresult += plusX;
            }
        }

        //                                                                          //
        //画面下半分に現れる文字の生成、ループが多すぎるので何かいい案があれば教えて//
        //                                                                          //
        private char[] dummy_gen()
        {
            
            char[] questionArray = (string.Join("",  QuestionText[CurrentQuestionCnt].Split('^','＾'))).ToCharArray();   //問題のテキストをchar型に変換
            char[] dummysArray = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもられるれろやゆよわをん".ToCharArray();  //ダミーになる文字達
            
           
            List<char> dummysList=new List<char>();     //addとかremoveを使いたいのでListにします
            List<char> gen_chars = new List<char>();    //

            dummysList = dummysArray.ToList();    //char型配列のダミー達を全部リストに収容
           
            foreach (char c in questionArray)   //
            {                                   //
                dummysList.Remove(c);           //正答とダブったダミー達は削除
                gen_chars.Add(c);               //正答リストに収容
            }                                   //

            Random rnd=new Random();    //randomオブジェクトの生成


            char[] dummyExtract = new char[7 - questionArray.Length];  //実際に表舞台に出るダミー達の配列


            for (int i=0; i < dummyExtract.Length; i++)                             //                         
            {                                                                       //
                dummyExtract[i] = dummysList[rnd.Next(0,dummysList.Count-1)];       //表舞台に出るダミー達をランダムに選抜
            }                                                                       //

            gen_chars.AddRange(dummyExtract);   //正答とダミーを連結            


            for (int i = 0; i < gen_chars.Count; i++)                   //
            {                                                           //
                char temp = gen_chars[i];                               //
                int randomIndex = rnd.Next(0, gen_chars.Count-1);       //配列をシャッフルしてダミーと正答を混ぜます
                gen_chars[i] = gen_chars[randomIndex];                  //
                gen_chars[randomIndex] = temp;                          //
            }                                                           //

            return gen_chars.ToArray();           //ダミー達と正答をグチャグチャにした配列を返します

        }



        //問題ボタンをクリックしたら音声を再生
        private void Question_Click(object sender, RoutedEventArgs e)
        {
            talking(((Button)sender).Content.ToString());
            
        }

        //選択肢ボタンをクリックしたら音声を再生
        private void dummy_Click(object sender, RoutedEventArgs e)
        {
            talking(((Button)sender).Content.ToString());
            string answer = escapeStr[CurrentWordcnt].ToString();
            string select=((Button)sender).Content.ToString();
            if ( answer==select)
            {
                //right.PlaySync();
                ((Button)QuestionFrame.Children[CurrentWordcnt]).IsEnabled = false;
                
                CurrentWordcnt++;
                if (CurrentWordcnt < escapeStr.Length)
                {
                    ((Button)QuestionFrame.Children[CurrentWordcnt]).IsEnabled = true;
                    ((Button)sender).IsEnabled = false;
                    
                }
                else
                {
                    complete.PlaySync();
                    talking(QuestionText[CurrentQuestionCnt]);
                    System.Threading.Thread.Sleep(1000);
                    //await Task.Run(()=>);
                    Next();

                }
            }
            else
            {
               // wrong.Play();
               
                Wrong_Words[QuestionText[CurrentQuestionCnt]] +="『"+answer + "』のとき『" + select+"』,";

            }
        }


        //次の問題へ
        private void Next() {
            for (int i = 0; i < dummyCanvas.Children.Count; i++)
            {
                ((Button)dummyCanvas.Children[i]).IsEnabled = false;
               
            }
            CurrentQuestionCnt++;
            if (CurrentQuestionCnt >= QuestionText.Length)
            {
                CurrentQuestionCnt = -1;
                Next();
            }
            CurrentWordcnt = 0;
            QuestionFrame.Children.Clear();
            dummyCanvas.Children.Clear();            
            gen_question();
            shuffle();
            q_image.Source = QuestionImage[CurrentQuestionCnt];
        }

        //音声合成を再生
        public void talking(string str)
        {
            MikoVoiceOutSync(str);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResultWrite rw = new ResultWrite();
            rw.ResultWriter(Wrong_Words, QuestionText);      //誤答のハッシュテーブルと問題の単語を書き出す

            Environment.Exit(0);
        }

        

    }
}
