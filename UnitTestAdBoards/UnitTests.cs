using AdBoards.ApiClient.Contracts.Requests;
using AdBoards.ApiClient.Contracts.Responses;
using AdBoards.ApiClient.Extensions;
using AdBoardsDesktop;
using AdBoardsDesktop.Models.db;

namespace UnitTestAdBoards
{
    public class Tests
    {


        [Test]
        public async Task RegistrationTest()
        {
            PersonReg person = new PersonReg();
            person.Login = "test";
            person.Email = "test@mail.ru";
            person.Birthday = Convert.ToDateTime("02.02.2000");
            person.Password = "test";
            person.ConfirmPassword = "test";
            person.Phone = "+79345668575";

            var result = await Context.Api.Registr(person);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task RegistrationTest2()
        {
            PersonReg person = new PersonReg();
            person.Login = "test2";
            person.Email = "test2@mail.ru";
            person.Birthday = Convert.ToDateTime("02.02.2000");
            person.Password = "test";
            person.ConfirmPassword = "tesd";
            person.Phone = "+79345668576";

            var result = await Context.Api.Registr(person);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AuthorizationTest()
        {
            var result = await Context.Api.Authorize("test", "test");

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddAdTest()
        {
            Context.UserNow = await Context.Api.Authorize("test", "test");
            Context.Api.Jwt = Context.UserNow.Token;

            AddAdModel ad = new AddAdModel();
            ad.Name = "testAd";
            ad.Description = "testD";
            ad.Price = 100;
            ad.CategoryId = 1;
            ad.AdTypeId = 1;
            ad.City = "Москва";
            var result = Context.Api.AddAd(ad);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DeleteAdTest()
        {
            Context.UserNow = await Context.Api.Authorize("test", "test");
            Context.Api.Jwt = Context.UserNow.Token;
            List<Ad> ads = await Context.Api.GetMyAds();

            var result = await Context.Api.DeleteAd(ads.LastOrDefault().Id);
            Assert.IsTrue(result);
        }
        
        [Test]
        public async Task DeletePersonTest()
        {
            Context.UserNow = await Context.Api.Authorize("admin", "admin");
            Context.Api.Jwt = Context.UserNow.Token;

            var result = await Context.Api.DeletePeople("test");
            Assert.IsTrue(result);
        }
    }
}