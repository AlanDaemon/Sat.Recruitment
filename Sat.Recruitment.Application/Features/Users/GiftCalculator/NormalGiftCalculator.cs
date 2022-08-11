using System;

namespace Sat.Recruitment.Application.Features.Users.GiftCalculator
{
    public class NormalGiftCalculator : IGiftCalculator
    {
        const decimal UpperTreshhold = 100;
        const decimal LowerTreshhold = 10;

        public decimal Calculate(decimal baseMoney)
        {
            if (baseMoney > UpperTreshhold)
            {
                var percentage = Convert.ToDecimal(0.12);
                var gift = baseMoney * percentage;

                return gift;
            }
            if (baseMoney <= UpperTreshhold && baseMoney > LowerTreshhold)
            {
                var percentage = Convert.ToDecimal(0.8);
                var gift = baseMoney * percentage;

                return gift;
            }

            return baseMoney;
        }
    }
}
