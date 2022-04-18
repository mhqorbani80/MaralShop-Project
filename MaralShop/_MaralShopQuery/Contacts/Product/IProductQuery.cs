namespace _MaralShopQuery.Contacts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProduct(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
