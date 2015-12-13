using System;
using System.Collections;
using System.Collections.Generic;

namespace ListGeneration
{
    public class RandomList
    {
        public RandomList(int size)
        {
            CreateArray(size);
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
            var listOfNumbers = new List<int>(_size);
            var listOfEmptyCells = new List<int>(_size);

            for (int j = 0; j < _size; ++j)
            {
                listOfNumbers.Add(j + 1);
                listOfEmptyCells.Add(j);
            }

            for (var i = 0; i < _size; ++i)
            {
                // random choice of indexes of these lists
                var randomElementIndex = FindRandomIndex(listOfNumbers);
                var emptyCellIndex = FindRandomIndex(listOfEmptyCells);

                // filling of an array cell of a random index with a random value
                _array[listOfEmptyCells[emptyCellIndex]] = listOfNumbers[randomElementIndex];

                // removal of the used values
                listOfNumbers.Remove(listOfNumbers[randomElementIndex]);
                listOfEmptyCells.Remove(listOfEmptyCells[emptyCellIndex]);

                RandomChange(ref _array);
            }
            RandomChange(ref _array);
        }
        private static int FindRandomIndex(ICollection list)
        {
            return ChoiceOfIndex(0, list.Count - 1);
        }
        private static int ChoiceOfIndex(int firstIndex, int lastIndex)
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
                leftPart = random.Next(10000);
                rightPart = random.Next(10000);
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
        private static void RandomChange(ref int[] array)
        {
            int random1, eachElementOfNumber;
            var  random2 = random1 = 0;

            // Random variables creation
            var startPoint = DateTime.Now.Millisecond;
            var random = new Random(startPoint);

            while (random1 == random2 || random1 == 0 || random2 == 0)
            {
                random1 = random.Next(100);
                random2 = random.Next(100);
            }
            
            if (random1 > random2) 
                eachElementOfNumber = random1 / random2 + 1;
            else 
                eachElementOfNumber = random2 / random1 + 1;

            // Each element of 'eachElementOfNumber'th cell will be changed 
            // with element of 'i - eachElementOfNumber' th cell
            // number 'koef' - random number, but it has to be not less then 2, 
            // otherwise there will be OutOfRangeException
            const int koef = 3;
            for (var i = eachElementOfNumber * koef; i < array.Length; i++)
            {
                if (i % eachElementOfNumber == 0
                    && array[i] != 0
                    && array[i - eachElementOfNumber] != 0)
                {
                    var temp = array[i - eachElementOfNumber];
                    array[i - eachElementOfNumber] = array[i];
                    array[i] = temp;
                }
            }
        }
        public void RefreshList(int? size = null)
        {
            if (size != null)
                _size = (int)size;
            CreateArray(_size);
        }

        private int[] _array;
        private int _size;
    }
}
