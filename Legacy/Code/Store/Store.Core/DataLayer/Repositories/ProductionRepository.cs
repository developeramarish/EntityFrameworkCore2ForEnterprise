using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.DataLayer.Contracts;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Repositories
{
    public class ProductionRepository : Repository, IProductionRepository
    {
        public ProductionRepository(IUserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IQueryable<Product> GetProducts(int? productCategoryID = null)
        {
            var query = DbContext.Set<Product>().AsQueryable();

            if (productCategoryID.HasValue)
                query = query.Where(item => item.ProductCategoryID == productCategoryID);

            return query;
        }

        public async Task<Product> GetProductAsync(Product entity)
            => await DbContext.Set<Product>().FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);

        public Product GetProductByName(string  productName)
            => DbContext.Set<Product>().FirstOrDefault(item => item.ProductName == productName);

        public IQueryable<ProductCategory> GetProductCategories()
            => DbContext.Set<ProductCategory>();

        public ProductCategory GetProductCategory(ProductCategory entity)
            => DbContext.Set<ProductCategory>().FirstOrDefault(item => item.ProductCategoryID == entity.ProductCategoryID);

        public IQueryable<ProductInventory> GetProductInventories(int? productID = null, string  warehouseID = null)
        {
            var query = DbContext.Set<ProductInventory>().AsQueryable();

            if (productID.HasValue)
                query = query.Where(item => item.ProductID == productID);

            if (!string.IsNullOrEmpty(warehouseID))
                query = query.Where(item => item.WarehouseID == warehouseID);

            return query;
        }

        public ProductInventory GetProductInventory(ProductInventory entity)
            => DbContext.Set<ProductInventory>().FirstOrDefault(item => item.ProductInventoryID == entity.ProductInventoryID);

        public IQueryable<Warehouse> GetWarehouses()
            => DbContext.Set<Warehouse>();

        public async Task<Warehouse> GetWarehouseAsync(Warehouse entity)
            => await DbContext.Set<Warehouse>().FirstOrDefaultAsync(item => item.WarehouseID == entity.WarehouseID);
    }
}
