
using System.Reflection.Metadata.Ecma335;

namespace Application
{
    public static class StaticMethod
    {
        private static string keys = "abcd";
        public static int Alphabet(string alphabet)
        {
            Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();
            for (int i = 0; i < 25; i++)
            {
                char upper = (char)('A' + i);
                char lower = (char)('a' + i);

                dic.Add(i + 1, new List<string>() { upper.ToString(), lower.ToString() });
            }
            var output = dic.FirstOrDefault(x => x.Value.Contains(alphabet)).Key;
            return output;
        }
        public static string FinalCipher(int deChipher)
        {
            Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();
            for(int i = 0; i <26; i++)
            {
                char upper = (char)('A' + i);
                char lower = (char)('a' + i);

                dic.Add(i + 1, new List<string>() { upper.ToString(), lower.ToString() });
            }
            var output = dic.FirstOrDefault(x => x.Key == deChipher).Value.FirstOrDefault();
            return output.ToString();
        }
        public static string getNetAplhabet(string str)
        {
            var list = str.ToList();
            var newArray = new List<string>();
            for (int i = 0; i < str.Length; i++)
            {
                var number = Alphabet(list[i].ToString());
                newArray.Add(number.ToString());
            }
            var finalInt = new List<int>();
            for (int i = 0; i < newArray.Count; i++)
            {
                List<int> arr = new List<int>();
                if (i > 0)
                {

                    var prev = Convert.ToInt32(Convert.ToInt32(newArray[i]) - 1);
                    arr.Add(Convert.ToInt32(newArray[i]));
                    arr.Add(prev);
                    //string upString = String.Concat(arr.Select(x => x.ToString()));
                    var number = getNextInt(arr);

                    finalInt.Add(number);
                }
                else
                {
                    arr.Add(0);
                    arr.Add(Convert.ToInt32(newArray[i]));
                    //string upString = String.Concat(arr.Select(x => x.ToString()));
                    var number = getNextInt(arr);
                    finalInt.Add(number);
                }
            }
            List<string> finalResult = new List<string>();
            foreach(var fonal in finalInt)
            {
               var dp= FinalCipher(fonal);
                finalResult.Add(dp);
            }
            //var st = String.Concat(finalResult.Select(x => x.ToString()));
            var st = String.Concat(finalResult.Select(x => x.ToString()));
            return st;
        }
        public static int getNextInt(List<int> arr)
        {
            int finalNumber = 0;
            foreach (var num in arr)
            {
                var intgr = Convert.ToInt32(num);
                finalNumber = finalNumber + intgr;
            }
            return finalNumber;

        }
      

    }
}
