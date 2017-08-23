using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart;
using ExpectedObjects;
using FluentAssertions;

namespace ShoppingCartTest
{
    [TestClass]
    public class CartServiceTests
    {
        [TestMethod]
        public void Test_GroupBy_3_Items_Sum_Cost()
        {
            // arrange
            var expected = new List<int>() { 6, 15, 24, 21 };

            var target = new CartService();
            int items = 3;
            string column = "Cost";

            // act
            var actual = target.GetGroupByItemsSum(items, column);

            // assert
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [TestMethod]
        public void Test_GroupBy_4_Items_Sum_Revenue()
        {
            // arrange
            List<int> expected = new List<int>() { 50, 66, 60 };

            var target = new CartService();
            int items = 4;
            string column = "Revenue";

            // act
            var actual = target.GetGroupByItemsSum(items, column);

            // assert
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [TestMethod]
        public void Test_筆數輸入負數_拋ArgumentException()
        {
            // arrange
            var target = new CartService();
            int items = -1;
            string column = "Cost";

            // act
            Action act = () => target.GetGroupByItemsSum(items, column);

            // assert
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void Test_尋找的欄位若不存在_拋ArgumentException()
        {
            // arrange
            var target = new CartService();
            int items = 5;
            string column = "Stock";

            // act
            Action act = () => target.GetGroupByItemsSum(items, column);

            // assert
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void Test_GroupItem_0_Should_Return_0()
        {
            // arrange
            var expected = new List<int>() { 0 };
            var target = new CartService();
            int items = 0;
            string column = "Cost";

            // act
            var actual = target.GetGroupByItemsSum(items, column);

            // assert
            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}
