using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace 養護学校アプリ
{
    class Image_Generate
    {
         
        public BitmapImage bmp_gen(string filePath){
            BitmapImage bmp=new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("../" + filePath, UriKind.Relative);
            bmp.EndInit();
            return bmp;
        }
        public BitmapImage stream_bmp(FileStream fs)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.StreamSource = fs;
            bmp.EndInit();
            return bmp;
        }

    }
}
