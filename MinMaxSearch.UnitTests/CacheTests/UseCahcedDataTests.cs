﻿using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinMaxSearch.Cache;

namespace MinMaxSearch.UnitTests.CacheTests
{
    /// <summary>
    /// This class checks that the search used the cached data
    /// </summary>
    [TestClass]
    public class UseCahcedDataTests
    {
        [TestMethod]
        public void ExactValueInCach_UseValue()
        {
            var searchTree = new SampleTree();
            var engine = new SearchEngine(){CacheMode = CacheMode.ReuseCache};
            engine.CacheManager.AddExactEvaluation(searchTree.EndState1, 3);
            Assert.AreEqual(3, engine.Search(searchTree.ChildState1, 4).Evaluation);
        }

        [TestMethod]
        public void CachedMaxSmallerThanAlpha()
        {
            var searchTree = new SampleTree();
            searchTree.EndState1.SetEvaluationTo(-2);
            var engine = new SearchEngine()
            {
                CacheMode = CacheMode.ReuseCache,
                ParallelismMode = ParallelismMode.NonParallelism
            };
            engine.CacheManager.AddMaxEvaluation(searchTree.ChildState2, -4);
            Assert.AreEqual(-2, engine.Search(searchTree.ManyChildrenState, 5).Evaluation);
        }

        [TestMethod]
        public void CachedMinGreatrThanBeta()
        {
            var searchTree = new SampleTree();
            searchTree.EndState2.SetEvaluationTo(2);
            searchTree.EndState2.SetEvaluationTo(4);
            var engine = new SearchEngine()
            {
                CacheMode = CacheMode.ReuseCache,
                ParallelismMode = ParallelismMode.NonParallelism
            };
            engine.CacheManager.AddMinEvaluation(searchTree.EndState3, 8);
            Assert.AreEqual(4, engine.Search(searchTree.ManyChildrenState, 5).Evaluation);
        }

        [TestMethod]
        [DataRow(Player.Max)]
        [DataRow(Player.Min)]
        public void MinCachedValueEqualeMaxScore_UseValue(Player firstPlayer)
        {
            var maxEvaluation = 10;
            var searchTree = new SampleTree(firstPlayer);
            var engine = new SearchEngine() {CacheMode = CacheMode.ReuseCache, MaxScore = maxEvaluation};
            engine.CacheManager.AddMinEvaluation(searchTree.EndState1, maxEvaluation);
            Assert.AreEqual(maxEvaluation, engine.Search(searchTree.ChildState1, 4).Evaluation);
        }

        [TestMethod]
        [DataRow(Player.Max)]
        [DataRow(Player.Min)]
        public void MaxCachedValueEqualeMinScore_UseValue(Player firstPlayer)
        {
            var minEvaluation = -10;
            var searchTree = new SampleTree(firstPlayer);
            var engine = new SearchEngine() { CacheMode = CacheMode.ReuseCache, MinScore = minEvaluation };
            engine.CacheManager.AddMaxEvaluation(searchTree.EndState1, minEvaluation);
            Assert.AreEqual(minEvaluation, engine.Search(searchTree.ChildState1, 4).Evaluation);
        }
    }
}
