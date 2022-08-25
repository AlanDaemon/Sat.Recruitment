using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Features.Users.Entities;
using System;

namespace Sat.Recruitment.Application.Features.Users.Files
{
    public class UsersFileFormatter
    {
        public static User MapFileLineToUser(string line)
        {
            var userRawData = line.Split(',');

            return new User()
            {
                Name = userRawData[0],
                Email = userRawData[1],
                Phone = userRawData[2],
                Address = userRawData[3],
                UserTypeId = Enum.Parse<UserTypes>(userRawData[4]),
                Money = decimal.Parse(userRawData[5])
            };
        }
    }
}
