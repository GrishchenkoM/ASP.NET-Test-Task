using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListGeneration
{
    public class RandomList
    {
        public RandomList()
        {
            _list = new List<int>() {};
        }
        public RandomList(int size)
        {
            _list = new List<int>(size);
            var random = new Random();
            for (var i = 0; i < size; ++i)
            {
                var temp = 0;
                var x = 0;
                while (true)
                {
                    temp = random.Next(1, size);
                    if (!_list.Contains(temp))
                        break;
                    ++x;
                    if (x > size) break;
                }
                
                _list.Add(temp);
            }
        }
        public List<int> GetList
        {
            get { return _list; }
        }

        private readonly List<int> _list;
    }
}
