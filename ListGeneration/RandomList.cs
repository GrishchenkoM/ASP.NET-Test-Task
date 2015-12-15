using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

        private void CreateArray(int size)
        {
            _size = size;
            _array = new int[_size];

            // creation  of two lists:
            // - list of numbers, which are not added to the array (resulting list) yet
            // - list of indexes of array cells which are not filled yet
            _listOfNumbers = new List<int>(_size);
            _listOfEmptyCells = new List<int>(_size);
        }

        public void FillArray()
        {
            var th1 = new ParameterizedThreadStart(FillList);
            var th2 = new ParameterizedThreadStart(FillList);
            var listOfNumbersFillParams = new ArrayList {_listOfNumbers, 1, _locker1};
            var listOfEmptyCellsFillParams = new ArrayList {_listOfEmptyCells, _locker2};

            th1(listOfNumbersFillParams);
            th2(listOfEmptyCellsFillParams);

            for (var i = 0; i < _size; ++i)
            {
                _isOver = false;

                var task1 = Task<int>.Factory.StartNew(Find(_listOfNumbers));
                var task2 = Task<int>.Factory.StartNew(Find(_listOfEmptyCells));

                Task.WaitAll(task1);
                Task.WaitAll(task2);

                var randomElementIndex = task1.Result;
                var emptyCellIndex = task2.Result;

                // filling of an array cell of a random index with a random value
                _array[_listOfEmptyCells[emptyCellIndex]] = _listOfNumbers[randomElementIndex];

                // removal of the used values
                _listOfNumbers.Remove(_listOfNumbers[randomElementIndex]);
                _listOfEmptyCells.Remove(_listOfEmptyCells[emptyCellIndex]);
            }
        }

        private int FindRandomIndex(ICollection list)
        {
            return ChoiceOfIndex(0, list.Count - 1);
        }
        private int ChoiceOfIndex(int firstIndex, int lastIndex)
        {
            // variables which define part of the list 
            // from which required data will be taken
            int leftPart, rightPart;
            leftPart = rightPart = 0;

            if (lastIndex - firstIndex < 1) 
                return lastIndex;
            var middle = (lastIndex + firstIndex) / 2;

            var startPoint = DateTime.Now.Millisecond;
            var random = new Random(startPoint);

            while (leftPart == rightPart)
            {
                leftPart = random.Next(_size);
                rightPart = random.Next(_size);
            }

            if (leftPart > rightPart)
                return ChoiceOfIndex(firstIndex, middle);
            return ChoiceOfIndex(middle + 1, lastIndex);
        }
        /// <summary>
        /// Shift of random elements in the array.
        /// Improves randomness of arrangement of elements in the array
        /// </summary>
        /// <param name="array">The array which elements need to be reshuffled</param>
        private void RandomChange()
        {
            int random1, eachElementOfNumber;
            var  random2 = random1 = 0;

            while (!_isOver)
            {
                // Random variables creation
                var startPoint = DateTime.Now.Millisecond;
                var random = new Random(startPoint);

                while (random1 == random2 || random1 == 0 || random2 == 0)
                {
                    random1 = random.Next(_size);
                    random2 = random.Next(_size);
                }

                if (random1 > random2)
                    eachElementOfNumber = random1/random2 + 1;
                else
                    eachElementOfNumber = random2/random1 + 1;

                // Each element of 'eachElementOfNumber'th cell will be changed 
                // with element of 'i - eachElementOfNumber' th cell
                try
                {
                    for (var i = eachElementOfNumber; i + eachElementOfNumber < _array.Length; i++)
                        if (i % eachElementOfNumber == 0
                                && _array[i] != 0
                                && _array[i + eachElementOfNumber] != 0)
                        {
                            var temp = _array[i + eachElementOfNumber];
                            _array[i + eachElementOfNumber] = _array[i];
                            _array[i] = temp;
                        }
                }
                catch (Exception) { }
            }
        }
        public void RefreshList(int? size = null)
        {
            if (size != null)
            {
                _size = (int) size;
                CreateArray(_size);
            }
            Parallel.Invoke(FillArray, RandomChange);
        }

        private void FillList(object parameters)
        {
            var arrayList = parameters as ArrayList;
            List<int> list = null;
            int firstIndex = 0;
            object locker = null;

            foreach (var item in arrayList)
            {
                if (item is List<int>)
                    list = item as List<int>;
                if (item is int)
                    firstIndex = (int)(item as int?);
                if (!(item is List<int>) && !(item is int))
                    locker = item;
            }

            lock (locker)
                if (list != null)
                    for (int j = (int)firstIndex; j < _size + firstIndex; ++j)
                        list.Add(j);
        }

        private Func<int> Find(List<int> list)
        {
            var result = FindRandomIndex(list);
            return (() => result);
        }

        private List<int> _listOfNumbers;
        private List<int> _listOfEmptyCells;
        private int[] _array;
        private int _size;
        private static object _locker1 = new object();
        private static object _locker2 = new object();
        private static bool _isOver;
    }
}
