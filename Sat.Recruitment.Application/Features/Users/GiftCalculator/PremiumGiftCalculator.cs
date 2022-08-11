namespace Sat.Recruitment.Application.Features.Users.GiftCalculator
{
    public class PremiumGiftCalculator : IGiftCalculator
    {
        const decimal Treshhold = 100;

        public decimal Calculate(decimal baseMoney)
        {
            if (baseMoney > Treshhold)
            {
                var gift = baseMoney * 2;

                return gift;
            }

            return baseMoney;
        }
    }
}
