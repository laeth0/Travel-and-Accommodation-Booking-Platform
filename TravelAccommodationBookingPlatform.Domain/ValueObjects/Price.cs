using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Domain.ValueObjects;
public class Price
{
    public double Value { get; set; }

    public Price CalculateFinalPrice(Discount? discount)
    {
        return discount is null ? this : CalculateFinalPrice(discount.Percentage);
    }

    public Price CalculateFinalPrice(double percentage)
    {
        var discountAmount = Value * (percentage / 100);
        return new Price { Value = Value - discountAmount };
    }
}
