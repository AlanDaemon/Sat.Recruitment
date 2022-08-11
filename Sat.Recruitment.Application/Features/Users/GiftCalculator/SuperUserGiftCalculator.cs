using System;

namespace Sat.Recruitment.Application.Features.Users.GiftCalculator
{
    public class SuperUserGiftCalculator : IGiftCalculator
    {
        const decimal Treshhold = 100;

        public decimal Calculate(decimal baseMoney)
        {
            if (baseMoney > Treshhold)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gift = baseMoney * percentage;

                return gift;
            }

            return baseMoney;
        }
    }
}
