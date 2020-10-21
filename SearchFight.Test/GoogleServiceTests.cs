using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchFight.Core.Entities;
using SearchFight.Core.Services;
using System;

namespace SearchFight.Test
{
    [TestClass()]
    public class GoogleServiceTests
    {
        [TestMethod()]
        public void TestNullCriteriaCase()
        {
            GoogleService service = new GoogleService();
            string searchCriteria = null;
            SearchEngineQuery result;

            try
            {
                result = service.Search(searchCriteria).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message,"Search criteria is empty or null");
                return;
            }
            Assert.Fail("Exception was not thrown");
        }

        [TestMethod()]
        public void TestEmptyCriteriaCase()
        {
            GoogleService service = new GoogleService();
            string searchCriteria = " ";
            SearchEngineQuery result;

            try
            {
                result = service.Search(searchCriteria).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Search criteria is empty or null");
                return;
            }
            Assert.Fail("Exception was not thrown");
        }

        [TestMethod()]
        public void TestOneTermCase()
        {
            GoogleService service = new GoogleService();
            string searchCriteria = ".net";
            SearchEngineQuery result;

            try
            {
                result = service.Search(searchCriteria).GetAwaiter().GetResult();
                Assert.IsTrue(result != null && result.TotalResults > 0);
            }
            catch (Exception)
            {
                Assert.Fail("Exception was thrown");
            }
        }

        [TestMethod()]
        public void TestMultipleTermsCase()
        {
            GoogleService service = new GoogleService();
            string searchCriteria = "microsoft office";
            SearchEngineQuery result;

            try
            {
                result = service.Search(searchCriteria).GetAwaiter().GetResult();
                Assert.IsTrue(result != null && result.TotalResults > 0);
            }
            catch (Exception)
            {
                Assert.Fail("Exception was thrown");
            }
        }
    }
}
