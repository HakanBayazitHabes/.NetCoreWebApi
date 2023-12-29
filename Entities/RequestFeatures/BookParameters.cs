namespace Entities.RequestFeatures;

public class BookParameters : RequestParamaters
{
    public uint MinPrice { get; set; }
    public uint MaxPrice { get; set; } = 1000;
    public bool validPriceRange => MaxPrice > MinPrice;

    public String? SearchTerm { get; set; }
}
