using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Task4
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
        public Polynomial Add(Polynomial polynomial)
        {
            Polynomial result = new();
            int rank = this.Rank > polynomial.Rank ? this.Rank : polynomial.Rank;
            for (int i = 0; i <= rank; ++i)
            {
                if (!this._coefficients.ContainsKey(i) && !polynomial._coefficients.ContainsKey(i))
                {
                    continue;
                }
                if (this._coefficients.ContainsKey(i) && !polynomial._coefficients.ContainsKey(i))
                {
                    result[i] = this[i];
                }
                else if (!this._coefficients.ContainsKey(i) && polynomial._coefficients.ContainsKey(i))
                {
                    result[i] = polynomial[i];
                }
                else
                {
                    result[i] = this[i] + polynomial[i];
                }
            }
            return result;
        }
        public Polynomial Subtract(Polynomial polynomial)
        {
            Polynomial result = new();
            int rank = this.Rank > polynomial.Rank ? this.Rank : polynomial.Rank;
            for (int i = 0; i <= rank; ++i)
            {
                if (!this._coefficients.ContainsKey(i) && !polynomial._coefficients.ContainsKey(i))
                {
                    continue;
                }
                else if (this._coefficients.ContainsKey(i) && !polynomial._coefficients.ContainsKey(i))
                {
                    result[i] = this[i];
                }
                else if (!this._coefficients.ContainsKey(i) && polynomial._coefficients.ContainsKey(i))
                {
                    result[i] = -polynomial[i];
                }
                else
                {
                    result[i] = this[i] - polynomial[i];
                }
            }
            return result;
        }
        public Polynomial Multiply(double value)
        {
            Polynomial result = new()
            {
                _coefficients = new SortedDictionary<int, double>(
                    this._coefficients.ToDictionary(x => x.Key, x => x.Value * value)
                )
            };
            return result;
        }
        public Polynomial Multiply(Polynomial polynomial)
        {
            Polynomial result = new();
            for (int i = 0; i <= this.Rank; ++i)
            {
                for (int j = 0; j <= polynomial.Rank; ++j)
                {
                    result[i + j] += this[i] * polynomial[j]; 
                }
            }
            return result;
        }
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
        public static Polynomial Parse(string s)
        {
            if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s))
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
