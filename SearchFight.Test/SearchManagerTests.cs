using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchFight.Business.Search;
using SearchFight.Core.Entities;
using System;
using System.Collections.Generic;
using static SearchFight.Core.Enumerations.Enumerations;

namespace SearchFight.Test
{
    [TestClass]
    public class SearchManagerTests
    {
        [TestMethod()]
        public void TestNullKeywordListCase()
        {
            SearchManager serviceMgr = new SearchManager();
            List<string> keywordList = null;
            SearchResult result;

            try
            {
                result = serviceMgr.Search(keywordList);
                Assert.IsTrue(result.Status == (short)ProcessStatus.Error &&
                    result.ErrorMessage.Contains("Keywords were not found to perform a search"));
            }
            catch (Exception)
            {
                Assert.Fail("Exception was thrown");
            }
        }

        [TestMethod()]
        public void TestEmptyKeywordListCase()
        {
            SearchManager serviceMgr = new SearchManager();
            List<string> keywordList = new List<string>();
            SearchResult result;

            try
            {
                result = serviceMgr.Search(keywordList);
                Assert.IsTrue(result.Status == (short)ProcessStatus.Error &&
                    result.ErrorMessage.Contains("Keywords were not found to perform a search"));
            }
            catch (Exception)
            {
                Assert.Fail("Exception was thrown");
            }
        }

        [TestMethod()]
        public void TestEmptykeywordCase()
        {
            SearchManager serviceMgr = new SearchManager();
            List<string> keywordList = new List<string> {
                ".net", " "
            };
            SearchResult result;

            try
            {
                result = serviceMgr.Search(keywordList);
                Assert.IsTrue(result.Status == (short)ProcessStatus.Error &&
                    result.ErrorMessage.Contains("Any keyword in the list cannot be empty or null"));
            }
            catch (Exception)
            {
                Assert.Fail("Exception was thrown");
            }
        }

        [TestMethod()]
        public void TestOneKeywordCase()
        {
            SearchManager serviceMgr = new SearchManager();
            List<string> keywordList = new List<string> { ".net" };
            SearchResult result;

            try
            {
                result = serviceMgr.Search(keywordList);
                Assert.IsTrue(result.Status == (short)ProcessStatus.Success &&
                    result.SearchEngineResults != null && result.SearchEngineResults.Count == 2);
            }
            catch (Exception)
            {
                Assert.Fail("Exception was thrown");
            }
        }

        [TestMethod()]
        public void TestMultipleKeywordsCase()
        {
            SearchManager serviceMgr = new SearchManager();
            List<string> keywordList = new List<string> { ".net", "java" };
            SearchResult result;

            try
            {
                result = serviceMgr.Search(keywordList);
                Assert.IsTrue(result.Status == (short)ProcessStatus.Success &&
                    result.SearchEngineResults != null && result.SearchEngineResults.Count == 4);
            }
            catch (Exception)
            {
                Assert.Fail("Exception was thrown");
            }
        }

        [TestMethod()]
        public void TestMultipleTermsCase()
        {
            SearchManager serviceMgr = new SearchManager();
            List<string> keywordList = new List<string> { "microsoft office" };
            SearchResult result;

            try
            {
                result = serviceMgr.Search(keywordList);
                Assert.IsTrue(result.Status == (short)ProcessStatus.Success &&
                    result.SearchEngineResults != null && result.SearchEngineResults.Count == 2);
            }
            catch (Exception)
            {
                Assert.Fail("Exception was thrown");
                return;
            }
        }
    }
}
