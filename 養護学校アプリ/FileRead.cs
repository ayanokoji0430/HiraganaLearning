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
            var img_path = new List<string>();
            var target_dir = "./images/";
            var setup_Text = "./word_image.txt";

            System.IO.StreamReader cReader;
            try
            {
                cReader = (
                new System.IO.StreamReader(setup_Text)
            );
            }
            catch
            {
                string[] s={"さかな","とりにく","いす","つくえ","ちくわ"};
                result.AddRange(s);
                return;
            }
            if (cReader.EndOfStream)
            {
                string[] s = { "さかな", "とりにく", "いす", "つくえ", "ちくわ" };
                result.AddRange(s);
            }else{
                while (cReader.Peek() >= 0)
                {
                    var str = cReader.ReadLine().Split(',');
                    result.Add(str[0]);
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
    }
}
