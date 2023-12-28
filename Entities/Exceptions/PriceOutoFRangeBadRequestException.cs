namespace Entities.Exceptions
{
    public class PriceOutoFRangeBadRequestException : BadRequestException
    {
        public PriceOutoFRangeBadRequestException() : base("Maximum price should be less than 1000 and greater than 10")
        {
        }
    }
}