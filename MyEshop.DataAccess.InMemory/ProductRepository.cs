using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyEshop.Core.Models;

namespace MyEshop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        //  Adds a product to List and instantiates the products list if it has not been done yet.
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        //  This is used to store changes to the cache instead of saving them straight away. Kind of you can review before commiting save or update.
        public void Commit()
        {
            cache["products"] = products;
        }

        //  Inserts a product to product list
        public void Insert(Product p)
        {
            products.Add(p);
        }

        //  Updates a product using lambda expression to find where id matches
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        //
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        //  Deletes product if exists
        public void Delete (string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }





    }
}
