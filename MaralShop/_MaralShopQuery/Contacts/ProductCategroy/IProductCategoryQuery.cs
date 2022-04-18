namespace _MaralShopQuery.Contacts.ProductCategroy
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoriesWithProductsBy(string slug);
        List<ProductCategoryQueryModel> GetList();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
