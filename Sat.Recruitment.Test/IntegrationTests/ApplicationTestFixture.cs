using Microsoft.AspNetCore.Mvc.Testing;
using Sat.Recruitment.Infraestructure.Database;
using Sat.Recruitment.Test.IntegrationTests.Setup;

namespace Sat.Recruitment.IntegrationTest
{
    public class ApplicationTestFixture : WebApplicationFactory<TestsStartUp>
    {
        public readonly RecruitmentDbContext context;

        public ApplicationTestFixture()
        {            
            this.context = new RecruitmentDbContext();
            this.context.Database.EnsureCreated();
        }
    }
}