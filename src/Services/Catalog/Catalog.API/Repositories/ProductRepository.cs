using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                        .Products
                        .Find(p => true).ToListAsync();
        }
        /// <summary>
        /// Get All Products By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
       
        public async Task<Product> GetProductById(string Id)
        {
            return await _context.
                          Products.
                          Find(p => p.Id == Id)
                          .FirstOrDefaultAsync();
        }
       
        /// <summary>
        /// Get All products by catagory
        /// </summary>
        /// <param name="catagory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductByCatagory(string catagory)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Catagory, catagory);

            return await _context
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }
       
        /// <summary>
        /// Get All Products by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _context
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }
       
        /// <summary>
        /// Insert a new Product Object
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
       
        /// <summary>
        /// Updata Project Object
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                    .Products
                                    .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
       
        /// <summary>
        /// Delete a Product Object
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProduct(string Id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, Id);
            DeleteResult deleteResult = await _context
                                              .Products
                                              .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;                                 
        }

    }
}
