using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 養護学校アプリ
{
    class ResultWrite
    {
        public void ResultWriter(Hashtable Wrong_Words,string[] Question_Words)
        {
            StreamWriter writer =new StreamWriter(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"/result.txt",false);

            var unique = new List<string>();

            foreach (var word in Question_Words)
            {
                //コレクション内に存在していなければ、追加する
                if (!unique.Contains(word))
                {
                    unique.Add(word);
                }
            }


            foreach (string word in unique)
            {
                writer.WriteLine(word);
                writer.WriteLine((string)Wrong_Words[word]);
            }
            writer.Close();
        }
    }
}
