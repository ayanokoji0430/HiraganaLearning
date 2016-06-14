using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 養護学校アプリ
{
    /// <summary>
    /// GamePage.xaml の相互作用ロジック
    /// </summary>
    public partial class GamePage : Page
    {
        private int CurrentWordcnt=0;  //現在フォーカスが当たっている文字の番号
       // private List<Button> questionList;  //問題のテキストを一文字ずついれるリスト
        private string[] QuestionText; //問題のテキスト。ファイルから読み込む
        private int CurrentQuestionCnt = 0; //現在の問題番号
        private SoundPlayer complete = new SoundPlayer(Properties.Resources.Complete3);
        private SoundPlayer deden = new SoundPlayer(Properties.Resources.deden2);

        public GamePage()
        {
            
            InitializeComponent();
            this.DataContext = new Colors_Setup();
            FileRead fr = new FileRead();   //単語リスト読み込みのインスタンスの生成
            QuestionText = fr.showResult();     //単語リストを読み込んで配列に格納 
            gen_question();                 //問題を画面に表示
            shuffle(); //シャッフル
            
        }
        //
        //グリッドを問題テキストの文字数だけ均等に分割（カラム）・カラムに問題の文字を一文字ずつ割り当て
        //特にいじる必要もないので変数やfor文に関する説明は割愛します
        private void gen_question()
        {
            deden.Play();
            int ColumnNum = QuestionText[CurrentQuestionCnt].Length;
            ColumnDefinition[] ColumnArray = new ColumnDefinition[ColumnNum];
            QuestionFrame.ColumnDefinitions.Clear();
            for (int i = 0; i < ColumnNum; i++)
            {
                ColumnArray[i] = new ColumnDefinition();
                Button btn = new Button();
                btn.FontSize = 120;
                btn.Content = QuestionText[CurrentQuestionCnt][i];
                btn.Style = this.FindResource("ButtonStyle1") as Style;
                btn.Name = "btn" + i;
                btn.Width = 200-(ColumnArray.Length*4);
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
            char[] questionArray = QuestionText[CurrentQuestionCnt].ToCharArray();      //問題のテキストをchar型に変換
            string dummys = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもられるれろやゆよわをん";  //ダミーになる文字達
            char[] dummysArray = dummys.ToCharArray();      //ダミー達、stringからchar配列に変身
           
            List<char> dummysList=new List<char>();     //addとかremoveを使いたいのでListにします
            List<char> gen_chars = new List<char>();    //

            dummysList = dummysArray.ToList();    //char型配列のダミー達を全部リストに収容
           
            foreach (char c in questionArray)   //
            {                                   //
                dummysList.Remove(c);           //問題の文字達とダブったダミー達は削除
                gen_chars.Add(c);               //問題の文字達をリストに収容
            }                                   //

            Random rnd=new Random();    //randomオブジェクトの生成


            char[] dummyExtract = new char[7 - QuestionText[CurrentQuestionCnt].Length];  //実際に表舞台に出るダミー達の配列


            for (int i=0; i < dummyExtract.Length; i++)                             //                         
            {                                                                       //
                dummyExtract[i] = dummysList[rnd.Next(0,dummysList.Count-1)];       //表舞台に出るダミー達をランダムに選抜
            }                                                                      //
            
            foreach (char c in dummyExtract)                                        //
            {                                                                       //
                gen_chars.Add(c);                                                   //問題の文字達が入ったリストにダミーをおまけで追加
            }                                                                       // 

            char[] gen_Chars = gen_chars.ToArray();                         //ダミーと問題の文字が入ったリストをchar型の配列に変換


            for (int i = 0; i < gen_Chars.Length; i++)                  //
            {                                                           //
                char temp = gen_Chars[i];                               //
                int randomIndex = rnd.Next(0, gen_Chars.Length);        //配列をシャッフルしてダミーと問題の文字を混ぜます
                gen_Chars[i] = gen_Chars[randomIndex];                  //
                gen_Chars[randomIndex] = temp;                          //
            }                                                           //

            return gen_Chars;           //ダミー達と問題の文字達をグチャグチャにした配列を返します

        }



        //問題ボタンをクリックしたら音声を再生
        private void Question_Click(object sender, RoutedEventArgs e)
        {
            talking(((Button)sender).Content.ToString());
            
        }

        //選択肢ボタンをクリックしたら音声を再生
        private async void dummy_Click(object sender, RoutedEventArgs e)
        {
            talking(((Button)sender).Content.ToString());

            string answer=((Button)QuestionFrame.Children[CurrentWordcnt]).Content.ToString();
            string select=((Button)sender).Content.ToString();
            if ( answer==select)
            {
                //right.PlaySync();
                ((Button)QuestionFrame.Children[CurrentWordcnt]).IsEnabled = false;
                
                CurrentWordcnt++;
                if (CurrentWordcnt < QuestionText[CurrentQuestionCnt].Length)
                {
                    ((Button)QuestionFrame.Children[CurrentWordcnt]).IsEnabled = true;
                    ((Button)sender).IsEnabled = false;
                    
                }
                else
                {
                    complete.PlaySync();
                    await Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Task.Run(() => talking(QuestionText[CurrentQuestionCnt]));
                    }));
                    await Task.Run(()=>System.Threading.Thread.Sleep(1000));
                    Next();

                }
            }
            else
            {
               // wrong.Play();
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
        }

        //音声合成を再生
        public void talking(string str)
        {
            AquesTalk.Play(str,100);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        

    }
}
