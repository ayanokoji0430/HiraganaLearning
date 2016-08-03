using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace 養護学校アプリ
{
    class FileRead
    {
        private List<string> result = new List<string>() ;        
        private List<BitmapImage> result_img = new List<BitmapImage>();        

        public FileRead() {
            List<string> img_path = new List<string>();
            string target_dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) +"/Hiragana_Setup/images/";
            string default_img_path="./default_image/";
            string setup_Text = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Hiragana_Setup/word_image.txt";
            List<string> default_imagePath = new List<string>{ "fish.jpg", "chicken.jpg", "chair.jpg", "desk.jpg", "chikuwa.jpg" };
            List<string> default_str =new List<string>{ "さかな", "にわとり", "いす", "つくえ", "ちくわ" };
            List<BitmapImage> default_img=new List<BitmapImage>();
            var bmp_gen1=new Image_Generate();

            foreach (var path in default_imagePath)
            {
                default_img.Add(bmp_gen1.bmp_gen(default_img_path+path));
            }

            System.IO.StreamReader cReader;
            try
            {
                cReader = (
                new System.IO.StreamReader(setup_Text)
            );
            }
            catch
            {
                result.AddRange(default_str);
                result_img.AddRange(default_img);
                //設定ファイルが見つからなかった場合に表示する画像の追加//
                return;
            }
            if (cReader.EndOfStream)
            {
                result.AddRange(default_str);
                result_img.AddRange(default_img);
                //設定ファイルが空だった場合に表示する画像の追加//

            }else{
                while (cReader.Peek() >= 0)
                {
                    var str = cReader.ReadLine().Split(',');
                    string replace_Str = str[0].Replace('＾','^');
                    int caretCount = CountChar(replace_Str, '^');
                    if (replace_Str.Length - caretCount > 7) replace_Str = replace_Str.Remove(7 + caretCount);
                    result.Add(replace_Str);
                    img_path.Add(str[1]);
                }
            }
            cReader.Close();
            
            foreach (var path in img_path)
            {
                var ig = new Image_Generate();

                if (File.Exists(target_dir + path))
                {
                    FileStream fs = new FileStream(target_dir + path, FileMode.Open, FileAccess.Read);
                    result_img.Add(ig.stream_bmp(fs));
                    fs.Close();
                }
                else
                {
                    //設定ファイルに記載したパスに画像が無かった場合の処理。後で考える//
                    result_img.Add(bmp_gen1.bmp_gen(default_img_path+"noimage.jpg"));
                }
            }
        
        }

        public string[] showResult()
        {
            string[] sArray = result.ToArray();
            return sArray;
        }
        public BitmapImage[] showImg_Result()
        {
            BitmapImage[] img_Array =result_img.ToArray();
            return img_Array;
        }

        public static int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }
    }

}
