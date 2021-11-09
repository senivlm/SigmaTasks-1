using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task9.Models
{
    public class Storage
    {//Можна описувати поза класом
        public delegate void InvalidProductHandler(Storage sender, string message, string line);
        public delegate void ExpiredProductsHandler(Storage sender, List<Product> expired);

        public event InvalidProductHandler OnInvalidProductDetected;
        public event ExpiredProductsHandler OnExpiredProductsShow;

        private List<Product> _products;
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
        
        public Storage(params Product[] products)
        {
            _products = new List<Product>();
            for (int i = 0; i < products.Length; ++i)
            {
                if (products[i] == null)
                {
                    throw new ArgumentNullException($"products[{i}] is null");
                }
                _products.Add(new Product(products[i]));
            }
        }
        public Storage(string filePath)
        {
            _products = new List<Product>();
            ReadProducts(filePath);
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

            using (StreamReader reader = new(inFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                    bool success = true;
                    string message = "";
                    if (data.Length > 0 && String.Compare(data[0], "Meat", true) == 0)
                    {
                        try
                        {
                            _products.Add(new Meat(String.Join(", ", data[1..])));
                        }
                        catch (Exception ex)
                        {
                            message = ex.Message;
                            success = false;
                        }
                        
                    }
                    else if (data.Length > 0 && String.Compare(data[0], "Dairy", true) == 0)
                    {
                        try
                        {
                            _products.Add(new DairyProducts(String.Join(", ", data[1..])));
                        }
                        catch (Exception ex)
                        {
                            message = ex.Message;
                            success = false;
                        }
                    }
                    else if (data.Length > 0 && String.Compare(data[0], "Product", true) == 0)
                    {
                        try
                        {
                            _products.Add(new Product(String.Join(", ", data[1..])));
                        }
                        catch (Exception ex)
                        {
                            message = ex.Message; 
                            success = false;
                        }
                    }
                    else
                    {
                        message = "Failed to read the type of product.";
                        success = false;
                    }

                    if (!success)
                    {
                        OnInvalidProductDetected?.Invoke(this, message, line);
                    }
                }
            }
        }
        public void ShowExpiredProducts()
        {
            List<Product> expired = _products
                .Where(product => product.IsExpired)
                .ToList();
            OnExpiredProductsShow?.Invoke(this, expired);
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
        public List<Meat> GetAllMeatProducts()
        {
            return _products.OfType<Meat>().ToList();
        }
        public void ChangeCostForAll(double percents)
        {
            for (int i = 0; i < _products.Count; ++i)
            {
                _products[i].ChangeCost(percents);
            }
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Argument is null.");
            }
            _products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _products.Remove(product);
        }
        public List<Product> RemoveProductsByName(string name)
        {
            List<Product> toRemove = _products
                .Where(product => String.Compare(product.Name, name, true) == 0)
                .ToList();
            toRemove.ForEach(product => _products.Remove(product));
            return toRemove;
        }

        public List<Product> RemoveProducts(Func<Product, bool> selector)
        {
            List<Product> removed = GetProducts(selector);
            removed.ForEach(product => _products.Remove(product));
            return removed;
        }
        public List<Product> GetProductsByName(string name)
        {
            return _products
                .Where(product => String.Compare(product.Name, name, true) == 0)
                .ToList();
        }

        public List<Product> GetProductsByCost(double cost)
        {
            return _products
                .Where(product => Math.Abs(product.Cost - cost) < 1e-8)
                .ToList();
        }

        public List<Product> GetProductsByWeight(double weight)
        {
            return _products
                .Where(product => Math.Abs(product.Weight - weight) < 1e-8)
                .ToList();
        }

        public List<Product> GetProducts(Func<Product, bool> selector)
        {
            return _products
                .Where(selector)
                .ToList();
        }
    }
}
