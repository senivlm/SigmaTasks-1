using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class StorageSearch
    {
        public (Storage, Storage) Storages { get; }

        public StorageSearch(Storage storage1, Storage storage2)
        {
            if (storage1 == null)
            {
                throw new ArgumentNullException(nameof(storage1), "Storage1 is null.");
            }

            if (storage2 == null)
            {
                throw new ArgumentNullException(nameof(storage2), "Storage2 is null.");
            }
            Storages = (storage1, storage2);
        }

        public List<Product> GetCommonProducts()
        {
            List<Product> commonProducts = Storages.Item1
                .Products
                .Where(prod => Storages.Item2.Products.Contains(prod))
                .ToList();
            return commonProducts;
        }

        public List<Product> GetProductsOnlyInFirstStorage()
        {
            List<Product> productsSt1 = Storages.Item1
                .Products
                .Where(prod => !Storages.Item2.Products.Contains(prod))
                .ToList();
            return productsSt1;
        }

        public List<Product> GetProductsOnlyInSecondStorage()
        {
            List<Product> productsSt2 = Storages.Item2
                .Products
                .Where(prod => !Storages.Item1.Products.Contains(prod))
                .ToList();
            return productsSt2;
        }
        public List<Product> GetNonCommonProducts()
        {
            List<Product> productsSt1 = GetProductsOnlyInFirstStorage();
            productsSt1.AddRange(GetProductsOnlyInSecondStorage());
            return productsSt1;
        }
    }
}
