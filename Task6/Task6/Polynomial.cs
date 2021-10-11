using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;


namespace Task6
{
    public class Polynomial
    {
        private SortedDictionary<int, double> _coefficients;
        public double this[int power]
        {
            get
            {
                if (power < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(power), "There is no such coefficient.");
                }
                if (!_coefficients.ContainsKey(power))
                {
                    return 0;
                }
                return _coefficients[power];
            }
            set
            {
                if (power < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(power), "Negative power.");
                }
                if (!_coefficients.ContainsKey(power) && value != 0.0)
                {
                    _coefficients.Add(power, value);
                }
                else if (_coefficients.ContainsKey(power) && value != 0.0)
                {
                    _coefficients[power] = value;
                }
                else if (_coefficients.ContainsKey(power) && value == 0.0)
                {
                    _coefficients.Remove(power);
                }
            }
        }
        public int Rank
        {
            get
            {
                if (_coefficients.Count == 0)
                {
                    return 0;
                }
                return _coefficients.Last().Key;
            }
        }
        public Polynomial(List<double> coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }
            this._coefficients = new SortedDictionary<int, double>();
            for (int i = 0; i < coefficients.Count; ++i)
            {
                if (coefficients[i] == 0.0)
                {
                    continue;
                }
                this._coefficients.Add(i, coefficients[i]);
            }
        }
        public Polynomial() : this(new List<double>()) { }
        public double Calculate(double x)
        {
            return _coefficients.Sum(coeff => coeff.Value * Math.Pow(coeff.Key, x));
        }
        public override string ToString()
        {
            string s = String.Join(" + ",
                _coefficients.Select(kv => kv.Key == 0
                    ? $"{kv.Value:F3}"
                    : $"{kv.Value:F3}x^{kv.Key}"));
            return String.Compare(s, "") == 0 ? "0" : s;
        }

        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            Polynomial result = new();
            int rank = left.Rank > right.Rank ? left.Rank : right.Rank;
            for (int i = 0; i <= rank; ++i)
            {
                if (!left._coefficients.ContainsKey(i) && !right._coefficients.ContainsKey(i))
                {
                    continue;
                }
                if (left._coefficients.ContainsKey(i) && !right._coefficients.ContainsKey(i))
                {
                    result[i] = left[i];
                }
                else if (!left._coefficients.ContainsKey(i) && right._coefficients.ContainsKey(i))
                {
                    result[i] = right[i];
                }
                else
                {
                    result[i] = left[i] + right[i];
                }
            }
            return result;
        }

        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            return left + -right;
        }

        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            Polynomial result = new();
            for (int i = 0; i <= left.Rank; ++i)
            {
                for (int j = 0; j <= right.Rank; ++j)
                {
                    result[i + j] += left[i] * right[j];
                }
            }
            return result;
        }

        public static Polynomial operator +(Polynomial polynomial)
        {
            Polynomial result = new();
            for (int i = 0; i <= polynomial.Rank; ++i)
            {
                result[i] = polynomial[i];
            }

            return result;
        }

        public static Polynomial operator -(Polynomial polynomial)
        {
            Polynomial result = new();
            for (int i = 0; i <= polynomial.Rank; ++i)
            {
                result[i] = -polynomial[i];
            }

            return result;
        }
        public static implicit operator Polynomial(double value)
        {
            return new Polynomial() { [0] = value };
        }
        public static Polynomial Parse(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Passed string is either null or empty or whitespace.", nameof(s));
            }
            Polynomial result = new();
            string currentDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var terms = s
                .Replace(" ", "")
                .Replace(",", currentDecimalSeparator)
                .Replace(".", currentDecimalSeparator)
                .Split("+", StringSplitOptions.RemoveEmptyEntries);
            foreach (var term in terms)
            {
                if (!term.Contains("*x^"))
                {
                    if (result._coefficients.ContainsKey(0))
                    {
                        throw new FormatException($"{nameof(s)} must not duplicate coefficients with same power.");
                    }
                    double value;
                    if (!Double.TryParse(term, out value))
                    {
                        throw new FormatException("Couldn't parse term. Perhaps string contains inappropriate symbols.");
                    }
                    result[0] = value;
                }
                else
                {
                    var kv = term.Split("*x^", StringSplitOptions.RemoveEmptyEntries);
                    if (kv.Length > 2)
                    {
                        throw new FormatException($"{nameof(s)} has invalid format.");
                    }
                    if (kv.Length < 2)
                    {
                        int power;
                        if (!Int32.TryParse(kv[0], out power))
                        {
                            throw new FormatException("Couldn't parse term. Perhaps string contains inappropriate symbols.");
                        }
                        result[power] = 1.0;
                    }
                    else
                    {
                        double coeff;
                        int power;
                        if (!Int32.TryParse(kv[1], out power) || !Double.TryParse(kv[0], out coeff))
                        {
                            throw new FormatException("Couldn't parse term. Perhaps string contains inappropriate symbols.");
                        }
                        result[power] = coeff;
                    }
                }
            }
            return result;
        }
    }
}
