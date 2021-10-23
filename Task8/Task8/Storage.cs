using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task8
{
    public class Storage
    {
        private List<Product> _products = null;
        public List<Product> Products => _products;
        public Product this[int index]
        {
            get
            {
                if (_products == null || index < 0 || index >= _products.Count)
                {
                    throw new IndexOutOfRangeException(
                        $"Index was out of range. Must be non-negative and less than the size of the array. Parameter name: {nameof(index)}");
                }
                return _products[index];
            }
        }
        public int Length
        {
            get => _products.Count;
        }
        public void ReadProducts(string inFilePath)
        {
            if (!File.Exists(inFilePath))
            {
                throw new FileNotFoundException("File not found.", inFilePath);
            }

            if (String.Compare(new FileInfo(inFilePath).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(inFilePath)}", inFilePath, "Only text files are supported (*.txt).");
            }

            string[] lines;
            using (StreamReader reader = new(inFilePath))
            {
                lines = reader.ReadToEnd().Split("\r\n");
            }

            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (data.Length > 0 && String.Compare(data[0], "Meat", true) == 0)
                {
                    _products.Add(new Meat(String.Join(", ", data.Where((_, index) => index > 0))));
                }
                else if (data.Length > 0 && String.Compare(data[0], "Dairy", true) == 0)
                {
                    _products.Add(new DairyProducts(String.Join(", ", data.Where((_, index) => index > 0))));
                }
                else if (data.Length > 0 && String.Compare(data[0], "Product", true) == 0)
                {
                    _products.Add(new Product(String.Join(", ", data.Where((_, index) => index > 0))));
                }
                else
                {
                    throw new FormatException($"Line #{i + 1} has invalid format.");
                }
            }
        }
        public void RemoveExpiredProducts(string outFilePath)
        {
            if (String.Compare(new FileInfo(outFilePath).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(outFilePath)}", outFilePath, "Only text files are supported (*.txt).");
            }
            List<DairyProducts> expired = _products
                .OfType<DairyProducts>()
                .Where(dairyProduct => dairyProduct.IsExpired)
                .ToList();
            expired.ForEach(dairyProduct => _products.Remove(dairyProduct));

            using (StreamWriter writer = new StreamWriter(outFilePath))
            {
                if (expired.Count == 0)
                {
                    writer.WriteLine("None product was removed.");
                }
                else
                {
                    writer.WriteLine($"Count of removed products: {expired.Count}\n");
                    expired.ForEach(dairyProduct => writer.WriteLine(dairyProduct));
                }
            }
        }
        public string PrintProducts()
        {
            StringBuilder sb = new();
            for (int i = 0; i < _products.Count; ++i)
            {
                sb.Append($"Product #{i + 1}\n");
                sb.Append(Check.PrintProduct(_products[i]) + "\n");
            }
            return sb.ToString();
        }
        public Meat[] GetAllMeatProducts()
        {
            return _products.OfType<Meat>().ToArray();
        }
        public void ChangeCostForAll(double percents)
        {
            for (int i = 0; i < _products.Count; ++i)
            {
                _products[i].ChangeCost(percents);
            }
        }
        public Storage(params Product[] products)
        {
            this._products = new List<Product>();
            for (int i = 0; i < products.Length; ++i)
            {
                if (products[i] == null)
                {
                    throw new ArgumentNullException($"products[{i}] is null");
                }
                this._products.Add(new Product(products[i]));
            }
        }
        public Storage(string filePath)
        {
            _products = new List<Product>();
            ReadProducts(filePath);
        }
    }
}
