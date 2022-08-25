using Sat.Recruitment.Domain.Enums;
using System.Collections.Generic;

namespace Sat.Recruitment.Application.Features.Users.GiftCalculator
{
    public class GiftCalculatorFactory
    {
        private static readonly IDictionary<UserTypes, IGiftCalculator>
          calculators = new Dictionary<UserTypes, IGiftCalculator>()
          {
              { UserTypes.Normal, new NormalGiftCalculator() },
              { UserTypes.Premium, new PremiumGiftCalculator() },
              { UserTypes.SuperUser, new SuperUserGiftCalculator() }
          };

        public static IGiftCalculator GetCalculator(UserTypes userType)
        {
            return calculators[userType];
        }
    }
}
