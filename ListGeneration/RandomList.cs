using System;
using System.Collections;
using System.Linq;

namespace ListGeneration
{
    public class RandomList
    {
        public RandomList(int size)
        {
            RefreshList(size);
        }
        public ICollection GetList
        {
            get { return _array; }
        }

        private void CreateArray()
        {
            var random = new Random();
            _array = Enumerable.Range(1, _size).OrderBy(n => random.Next()).ToArray();
        }
        public void RefreshList(int? size = null)
        {
            if (size != null)
                _size = (int)size;

            CreateArray();
        }

        private int[] _array;
        private int _size;
    }
}
