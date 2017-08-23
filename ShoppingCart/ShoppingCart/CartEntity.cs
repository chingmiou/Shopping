using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ShoppingCart
{
    public class CartService
    {
        public CartService()
        {
        }

        private List<CartModel> GetCartList()
        {
            List<CartModel> cartList = new List<CartModel>
            {
                new CartModel() {Id = 1, Cost = 1, Revenue =  11, SellPrice = 21},
                new CartModel() {Id = 2, Cost = 2, Revenue =  12, SellPrice = 22},
                new CartModel() {Id = 3, Cost = 3, Revenue =  13, SellPrice = 23},
                new CartModel() {Id = 4, Cost = 4, Revenue =  14, SellPrice = 24},
                new CartModel() {Id = 5, Cost = 5, Revenue =  15, SellPrice = 25},
                new CartModel() {Id = 6, Cost = 6, Revenue =  16, SellPrice = 26},
                new CartModel() {Id = 7, Cost = 7, Revenue =  17, SellPrice = 27},
                new CartModel() {Id = 8, Cost = 8, Revenue =  18, SellPrice = 28},
                new CartModel() {Id = 9, Cost = 9, Revenue =  19, SellPrice = 29},
                new CartModel() {Id = 10, Cost =10, Revenue = 20, SellPrice = 30},
                new CartModel() {Id = 11, Cost =11, Revenue = 21, SellPrice = 31},
            };
            return cartList;
        }

        public List<int> GetGroupByItemsSum(int items, string column)
        {
            if (items < 0)
            {
                throw new ArgumentException();
            }

            if (items == 0)
            {
                return new List<int>() { 0 };
            }

            Type type = typeof(CartModel);
            PropertyInfo info = type.GetProperty(column);
            if (info == null)
            {
                throw new ArgumentException();
            }

            int rounds = GetMaxRound(items);

            List<int> result = new List<int>();
            for (int round = 0; round < rounds; round++)
            {
                int sum = GetRoundSum(round, items, column);
                result.Add(sum);
            }

            return result;
        }

        private int GetMaxRound(int items)
        {
            var list = GetCartList();
            int result = list.Count % items > 0 ? list.Count / items + 1 : list.Count / items;
            return result;
        }

        private int GetRoundSum(int round, int items, string column)
        {
            int result = 0;
            List<CartModel> rangeList = GetRangeItems(round, items);

            foreach (var r in rangeList)
            {
                result += (int)typeof(CartModel).GetProperty(column).GetValue(r);
            }

            return result;
        }

        private List<CartModel> GetRangeItems(int round, int items)
        {
            List<CartModel> list = GetCartList() ;
            int endIndex = items * round + (items - 1);
            int getItems = items;
            if (endIndex > list.Count - 1)
            {
                getItems = list.Count % items;
            }
            return list.GetRange(items * round, getItems);
        }
    }
}