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
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1)
            };

            var response = Utils.Post(Utils.GetUri(), JsonSerializer.Serialize(Utils.Pass));
            Utils.Pass.Guid = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(Utils.Pass.Guid);
        }

        [Given(@"check delete")]
        public void GivenCheckDelete()
        {
            var response = Utils.Delete(Utils.GetUri(Utils.Pass.Guid));
        }

        [Given(@"check put")]
        public void GivenCheckPut()
        {
            var pass = new Pass()
            {
                Guid = Utils.Pass.Guid,
                PersonSurname = Utils.GenerateString(10, new Random()),
                PersonPatronymic = Utils.GenerateString(10, new Random()),
                PassportNumber = Utils.GenerateString(10, new Random(), true),
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1)
            };

            Console.WriteLine(JsonSerializer.Serialize(pass));

            Utils.Put(Utils.GetUri(), JsonSerializer.Serialize(pass));

            var response = Utils.Get(Utils.GetUri(Utils.Pass.Guid));

            if (response.Content.ReadAsStringAsync().Result.Equals(pass))
            {
                throw new Exception("Полученные данные не равны отправленным");
            }
        }
    }
}
