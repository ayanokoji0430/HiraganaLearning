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
            StreamWriter writer =new StreamWriter(@"result.txt",false);
            foreach (string word in Question_Words)
            {
                writer.WriteLine(word);
                writer.WriteLine((string)Wrong_Words[word]);
            }
            writer.Close();
        }
    }
}
