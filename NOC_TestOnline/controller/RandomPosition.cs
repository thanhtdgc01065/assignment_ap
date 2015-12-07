using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOC_TestOnline.controller
{
    class RandomPosition
    {
        public static List<int> getRandomQuestionPosition(List<int> myValues)
        {
            List<int> newList = new List<int>();
            Random r = new Random();
            IEnumerable<int> threeRandom = myValues.OrderBy(x => r.Next()).Take(3);
            foreach (int i in threeRandom)
            {
                newList.Add(i);
            }
            return newList;
        }
    }
}
