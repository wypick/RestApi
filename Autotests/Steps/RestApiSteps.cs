using System;
using TechTalk.SpecFlow;
using System.Text.Json;
using System.Net.Http;

namespace Autotests.Steps
{
    [Binding]
    public class RestApiSteps
    {
        [Given(@"check get/post")]
        public void GivenCheckGetPost()
        {
            var response = Utils.Get(Utils.GetUri(Utils.Pass.Guid));
            Console.WriteLine("Полученные данные в запросе GET: " + response.Content.ReadAsStringAsync().Result);

            if (response.Content.ReadAsStringAsync().Result.Equals(Utils.Pass))
            {
                throw new Exception("Полученные данные не равны отправленным");
            }
        }

        [Given(@"check get error")]
        public void GivenCheckGetError()
        {
            try
            {
                Utils.Get(Utils.GetUri(Utils.GenerateString(10, new Random())));
            }
            catch (Exception)
            {

            }
            
        }

        [Given(@"check validate date")]
        public void GivenCheckValidateDate()
        {
            HttpResponseMessage response = null;

            try
            {
                response = Utils.Get(Utils.GetUriValidate(Utils.Pass.Guid));
            }
            catch (Exception)
            {
                Console.WriteLine($"Ответ сервиса: ({response.StatusCode})");
            }

            var time = DateTime.Now;

            if ((Utils.Pass.DateFrom > time) || (Utils.Pass.DateTo < time))
            {
                throw new Exception("Некорректный ответ, дата не валидна");
            }
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

            var pass = JsonSerializer.Serialize(Utils.Pass);
            Console.WriteLine("Отправляемый объект в запросе POST: " + pass);

            var response = Utils.Post(Utils.GetUri(), pass);
            Utils.Pass.Guid = JsonSerializer.Deserialize<string>(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Полученный guid: " + Utils.Pass.Guid);
        }

        [Given(@"check delete")]
        public void GivenCheckDelete()
        {
            var response = Utils.Delete(Utils.GetUri(Utils.Pass.Guid));
        }

        [Given(@"update with not valid date")]
        public void GivenUpdateWithNotValidDate()
        {
            Utils.Pass.DateFrom = DateTime.Now.AddDays(10);
            Utils.Pass.DateTo = DateTime.Now.AddDays(11);

            Console.WriteLine("Отправляемый объект в запросе PUT: " + JsonSerializer.Serialize(Utils.Pass));
            Utils.Put(Utils.GetUri(), JsonSerializer.Serialize(Utils.Pass));
        }

        [Given(@"check put")]
        public void GivenCheckPut()
        {
            var pass = new Pass()
            {
                Guid = Utils.Pass.Guid,
                PersonName = Utils.GenerateString(10, new Random()),
                PersonSurname = Utils.GenerateString(10, new Random()),
                PersonPatronymic = Utils.GenerateString(10, new Random()),
                PassportNumber = Utils.GenerateString(10, new Random(), true),
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1)
            };

            Console.WriteLine("Отправляемый объект в запросе PUT: " + JsonSerializer.Serialize(pass));

            Utils.Put(Utils.GetUri(), JsonSerializer.Serialize(pass));

            var response = Utils.Get(Utils.GetUri(Utils.Pass.Guid));

            Console.WriteLine("Полученный объект в запросе GET после обновления: " + response.Content.ReadAsStringAsync().Result);

            // проверяем обновились ли данные
            if (response.Content.ReadAsStringAsync().Result.Equals(pass))
            {
                throw new Exception("Полученные данные не равны отправленным");
            }
        }
    }
}
