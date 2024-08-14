



using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.ValidationAttributes;
public class CapitalizeCheckAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // show this vedio for hindi  https://www.youtube.com/watch?v=ZJzskRbq2QA&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=19
        // show this vedio for DevGreed https://www.youtube.com/watch?v=bn2eBVjJzl8&list=PL62tSREI9C-cQ21T5HIWqqBOHQiNMOhBG&index=20
        if (value is string str)
            if (char.IsUpper(str[0]))
                return ValidationResult.Success;

        return new ValidationResult($"{validationContext.DisplayName} must start with a capital letter.");

    }
}
