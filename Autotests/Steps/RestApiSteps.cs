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
            var response = Utils.Get(Utils.GetUri(Utils.Pass.Guid));
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }

        [Given(@"create post reqest")]
        public void GivenCreatePostReqest()
        {
            Utils.Pass = new Pass()
            {
                PersonName = Utils.GenerateString(10, new Random()),
                PersonSurname = Utils.GenerateString(10, new Random()),
                PersonPatronymic = Utils.GenerateString(10, new Random()),
                PassportNumber = Utils.GenerateString(10, new Random(), true),
                DateFrom = DateTime.Now.Add(new TimeSpan(00, 00, 00)),
                DateTo = DateTime.Now.AddDays(1).Add(new TimeSpan(23, 59, 59))
            };

            var response = Utils.Post(Utils.GetUri(Utils.host), JsonSerializer.Serialize(Utils.Pass));
            Utils.Pass.Guid = response.Content.ReadAsStringAsync().Result;
        }
    }
}
