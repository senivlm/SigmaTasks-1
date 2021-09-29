﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Check
    {
        private Check() { }
        public static string PrintProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return product.ToString();
        }
        public static string PrintBuy(Buy buy)
        {
            if (buy == null)
            {
                throw new ArgumentNullException(nameof(buy));
            }
            return buy.ToString();
        }
    }
}
