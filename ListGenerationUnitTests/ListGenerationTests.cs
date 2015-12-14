using System.Collections;
using System.Linq;
using ListGeneration;
using NUnit.Framework;

namespace ListGenerationUnitTests
{
    [TestFixture]
    public class ListGenerationTests
    {
        #region Setup/Release

        [SetUp, Description("Create instance of class which generate the list with random numbers")]
        public void GenerateRandomNumbersList()
        {
            _quantity = 10000;
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
        [Test, Description("Checks for coincidence of the size of the massif with a given size.")]
        public void IsCorrectLengthOfList()
        {
            Assert.AreEqual(_quantity, _list.Count);
        }
        [Test, Description("Checks for uniqueness of array cells")]
        public void IsUniqueElementsOfList()
        {
            Assert.IsTrue(IsUniqueElementsInList(_list));
        }
        [Test, Description("Checks for coincidence of arrays after refreshing")]
        public void IsCorrectRefreshOfList()
        {
            var randomList1 = new RandomList(_quantity);
            var randomList2 = new RandomList(_quantity);
            Assert.AreNotEqual(_randomList,randomList1);
            Assert.AreNotEqual(randomList1, randomList2);
        }
        [Test, MaxTime(500), Description("Checks the speed of arrays creation")]
        public void IsFastCreation()
        {
            _randomList = new RandomList(_quantity);
        }
        [Test, MaxTime(500), Description("Checks the speed of arrays refresh")]
        public void IsFastRefresh()
        {
            _randomList.RefreshList();
        }

        #endregion

        #region Methods

        private static bool IsUniqueElementsInList(ICollection list)
        {
            var existedArray = (int[]) list;

            var standartArray = new int[existedArray.Length];
            for (var i = 0; i < standartArray.Length; ++i)
                standartArray[i] = i + 1;

            var result = existedArray.Except(standartArray);
            return !result.Any();
        }

        #endregion
        
        private RandomList _randomList;
        private ICollection _list;
        private int _quantity;
    }
}


