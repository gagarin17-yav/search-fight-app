using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchFight.Core.Entities;
using SearchFight.Core.Services;
using System;

namespace SearchFight.Test
{
    [TestClass()]
    public class BingServiceTests
    {
        [TestMethod()]
        public void TestNullCriteriaCase()
        {
            BingService service = new BingService();
            string searchCriteria = null;
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
        public void TestEmptyCriteriaCase()
        {
            BingService service = new BingService();
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
            BingService service_one = new BingService();
            string searchCriteria_one = "kubernetes";
            SearchEngineQuery result_one;

            try
            {
                System.Threading.Thread.Sleep(2000);
                result_one = service_one.Search(searchCriteria_one).GetAwaiter().GetResult();
                Assert.IsTrue(result_one != null && result_one.TotalResults > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception was thrown" + ex.Message);
            }
        }

        [TestMethod()]
        public void TestMultipleTermsCase()
        {
            BingService service = new BingService();
            string searchCriteria = "internet information server";
            SearchEngineQuery result;

            try
            {
                System.Threading.Thread.Sleep(2000);
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
