using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyEshop.Core.Models;

namespace MyEshop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        //  Adds a product to List and instantiates the products list if it has not been done yet.
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        //  This is used to store changes to the cache instead of saving them straight away. Kind of you can review before commiting save or update.
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        //  Inserts a product to product list
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        //  Updates a product using lambda expression to find where id matches
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found!");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found!");
            }
        }

        //
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        //  Deletes product if exists
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found!");
            }
        }
    }
}
