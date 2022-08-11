using Sat.Recruitment.Domain.Enums;
using System.Collections.Generic;

namespace Sat.Recruitment.Application.Features.Users.GiftCalculator
{
    public class GiftCalculatorFactory
    {
        private static readonly IDictionary<UserType, IGiftCalculator>
          calculators = new Dictionary<UserType, IGiftCalculator>()
          {
              { UserType.Normal, new NormalGiftCalculator() },
              { UserType.Premium, new PremiumGiftCalculator() },
              { UserType.SuperUser, new SuperUserGiftCalculator() }
          };

        public static IGiftCalculator GetCalculator(UserType userType)
        {
            return calculators[userType];
        }
    }
}
