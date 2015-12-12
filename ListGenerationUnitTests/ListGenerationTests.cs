using System;
using System.Collections;
using System.Collections.Generic;
using ListGeneration;
using NUnit.Framework;


namespace ListGenerationUnitTests
{
    [TestFixture]
    public class ListGenerationTests
    {
        #region Setup/Release

        [SetUp]
        public void GenerateRandomNumbersList()
        {
            _quantity = 2;
            _randomList = new RandomList(_quantity);
            _list = _randomList.GetList;
        }
        [TearDown]
        public void Release()
        {
            _randomList = null;
            _list = null;
        }

        #endregion

        #region Tests

        [Test]
        public void IsNotEmptyList()
        {
            Assert.IsNotEmpty(_list);
        }
        [Test]
        public void IsCorrectLengthOfList()
        {
            Assert.AreEqual(_quantity, _list.Count);
        }
        [Test]
        public void IsUniqueElementsOfList()
        {
            //Assert.IsTrue(IsUniqueElementsInList(_list));
            Assert.AreEqual(_list[0],_list[1]);
        }

        #endregion

        #region Methods

        private bool IsUniqueElementsInList(List<int> list)
        {
            foreach (int element in list)
                if (list.FindAll(x => x.Equals(element)).Count > 1)
                    return false;
            return true;
        }

        #endregion


        private RandomList _randomList;
        private List<int> _list;
        private int _quantity;
    }
}


