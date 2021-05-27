using System;
using TechTalk.SpecFlow;
using System.Text.Json;
using System.Net.Http;

namespace Autotests.Steps
{
    [Binding]
    public class RestApiSteps : Utils
    {
        [Given(@"check get/post")]
        public void GivenCheckGetPost()
        {
            var response = Get(GetUri(Pass.Guid));
            Console.WriteLine("Полученные данные в запросе GET: " + response.Content.ReadAsStringAsync().Result);

            if (response.Content.ReadAsStringAsync().Result.Equals(Pass))
            {
                throw new Exception("Полученные данные не равны отправленным");
            }
        }

        [Given(@"check get error")]
        public void GivenCheckGetError()
        {
            try
            {
                Get(GetUri(GenerateString(10, new Random())));
            }
            catch (Exception)
            {
                Console.WriteLine("При запросе с несуществующим Guid ничего не было найдено");
            }
        }

        [Given(@"check validate date")]
        public void GivenCheckValidateDate()
        {
            Get(GetUriValidate(Pass.Guid));

            var time = DateTime.Now;

            if ((Pass.DateFrom > time) || (Pass.DateTo < time))
            {
                throw new Exception("Некорректный ответ, дата не валидна");
            }
        }

        [Given(@"check validate date not valid")]
        public void GivenCheckValidateDateNotValid()
        {
            try
            {
                Get(GetUriValidate(Pass.Guid));
            }
            catch (Exception)
            {
                Console.WriteLine("Дата невалидна");
            }
        }

        [Given(@"create post reqest")]
        public void GivenCreatePostReqest()
        {
            Pass = new Pass()
            {
                PersonName = GenerateString(10, new Random()),
                PersonSurname = GenerateString(10, new Random()),
                PersonPatronymic = GenerateString(10, new Random()),
                PassportNumber = GenerateString(10, new Random(), true),
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1)
            };

            var pass = JsonSerializer.Serialize(Pass);
            Console.WriteLine("Отправляемый объект в запросе POST: " + pass);

            var response = Post(GetUri(), pass);
            Pass.Guid = JsonSerializer.Deserialize<string>(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("Полученный guid: " + Pass.Guid);
        }

        [Given(@"check delete")]
        public void GivenCheckDelete()
        {
            var response = Delete(GetUri(Pass.Guid));

            try
            {
                Get(GetUri(Pass.Guid));
            }
            catch (Exception)
            {
                Console.WriteLine("При запросе с несуществующим Guid ничего не было найдено");
            }
        }

        [Given(@"update with not valid date")]
        public void GivenUpdateWithNotValidDate()
        {
            Pass.DateFrom = DateTime.Now.AddDays(10);
            Pass.DateTo = DateTime.Now.AddDays(11);

            Console.WriteLine("Отправляемый объект в запросе PUT: " + JsonSerializer.Serialize(Pass));
            Put(GetUri(), JsonSerializer.Serialize(Pass));
        }

        [Given(@"check put")]
        public void GivenCheckPut()
        {
            var pass = new Pass()
            {
                Guid = Pass.Guid,
                PersonName = GenerateString(10, new Random()),
                PersonSurname = GenerateString(10, new Random()),
                PersonPatronymic = GenerateString(10, new Random()),
                PassportNumber = GenerateString(10, new Random(), true),
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1)
            };

            Console.WriteLine("Отправляемый объект в запросе PUT: " + JsonSerializer.Serialize(pass));

            Put(GetUri(), JsonSerializer.Serialize(pass));

            var response = Get(GetUri(Pass.Guid));

            Console.WriteLine("Полученный объект в запросе GET после обновления: " + response.Content.ReadAsStringAsync().Result);

            // проверяем обновились ли данные
            if (response.Content.ReadAsStringAsync().Result.Equals(pass))
            {
                throw new Exception("Полученные данные не равны отправленным");
            }
        }
    }
}
