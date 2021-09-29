﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class DairyProducts : Product
    {
        private int daysToExpiration = 365;
        public int DaysToExpiration 
        {
            get => daysToExpiration;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(value)} is lower than 0", $"{nameof(DaysToExpiration)} cannot be set to negative number.");
                }
                daysToExpiration = value;
            }
        }
        public DairyProducts(string name, double cost, double weight, int daysToExpiration) : base(name, cost, weight)
        {
            this.DaysToExpiration = daysToExpiration;
        }
        public DairyProducts(DairyProducts dairyProducts) : base(dairyProducts)
        {
            this.DaysToExpiration = dairyProducts.DaysToExpiration;
        }

        public override void ChangeCost(double percents)
        {
            if (percents < -100.0)
            {
                throw new ArgumentException($"Invalid percents passed. {nameof(percents)} must be greater or equal to -100");
            }
            base.ChangeCost(percents);
            switch (this.DaysToExpiration)
            {
                case <= 5:
                    base.ChangeCost(-20.0);
                    break;
                case <= 30:
                    base.ChangeCost(-8.0);
                    break;
                default:
                    break;

            }
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            var other = obj as DairyProducts;
            return this.DaysToExpiration == other.DaysToExpiration &&
                base.Equals(other);
        }
        public override string ToString()
        {
            return String.Concat($"Days to expiration: {this.DaysToExpiration}\n", base.ToString());
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(DaysToExpiration, base.GetHashCode());
        }
    }
}
