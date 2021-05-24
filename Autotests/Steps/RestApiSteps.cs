using System;
using TechTalk.SpecFlow;
using System.Text.Json;

namespace Autotests.Steps
{
    [Binding]
    public class RestApiSteps
    {
        [Given(@"check get")]
        public void GivenCheckGet()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"create post reqest")]
        public void GivenCreatePostReqest()
        {
            var pass = new Pass()
            {
                PersonName = Utils.GenerateString(10, new Random()),
                PersonSurname = Utils.GenerateString(10, new Random()),
                PersonPatronymic = Utils.GenerateString(10, new Random()),
                PassportNumber = Utils.GenerateString(10, new Random(), true),
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1)
            };

            var a = Utils.Post(Utils.GetUri(Utils.host), JsonSerializer.Serialize(pass));
        }
    }
}
