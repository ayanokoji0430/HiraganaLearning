using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 養護学校アプリ
{
    class FileRead
    {
        private List<string> result = new List<string>() ;
        public FileRead() {

            System.IO.StreamReader cReader;
            try
            {
                cReader = (
                new System.IO.StreamReader("Question.txt", System.Text.Encoding.Default)
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
                    result.Add(cReader.ReadLine());
                }
            }
            cReader.Close();
        
        }

        public string[] showResult()
        {
            string[] sArray = result.OrderBy(i => Guid.NewGuid()).ToArray();
            return sArray;
        }
    }
}
