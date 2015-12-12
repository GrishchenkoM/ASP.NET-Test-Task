using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ListGenerationTests
{
    [TestFixture]
    public class ListGenerationTests
    {
        [Test]
        public void GenerateRandomNumbersList()
        {
            var list = ListGeneration.GetList();
        }
    }
}


