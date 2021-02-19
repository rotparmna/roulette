using Masiv.Roulette.API.Contracts;
using Masiv.Roulette.APITests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Masiv.Roulette.API.Service.Tests
{
    [TestClass()]
    public class RouletteServiceTests
    {
        [TestMethod()]
        public void AddRouletteOkTest()
        {
            var cache = new MockCache();
            var random = new MockGenerateRandom();
            IRouletteService service = new RouletteService(cache, random);
            var dto = service.AddRoulette();
            Assert.IsFalse(string.IsNullOrEmpty(dto.Id));
        }

        [TestMethod]
        public void StartRouletteSuccessTest()
        {
            var cache = new MockCache();
            var random = new MockGenerateRandom();
            IRouletteService service = new RouletteService(cache, random);
            var dtoAdd = service.AddRoulette();
            var dtoStart = service.StartRoulette(new Domain.Dtos.RouletteStartDto()
            {
                Id = dtoAdd.Id
            });
            Assert.IsTrue(dtoStart.Result == Domain.Enums.ResultEnum.Success);
        }

        [TestMethod]
        public void StartRouletteFailTest()
        {
            var cache = new MockCache();
            var random = new MockGenerateRandom();
            IRouletteService service = new RouletteService(cache, random);
            var dtoStart = service.StartRoulette(new Domain.Dtos.RouletteStartDto()
            {
                Id = Guid.Empty.ToString()
            });
            Assert.IsTrue(dtoStart.Result == Domain.Enums.ResultEnum.Denied);
        }

        [TestMethod]
        public void StartRouletteWithStatusCloseTest()
        {
            var cache = new MockCache();
            var random = new MockGenerateRandom();
            IRouletteService service = new RouletteService(cache, random);
            var dtoAdd = service.AddRoulette();
            cache.roulettes.First(x=>x.Id==dtoAdd.Id).Status = Domain.Enums.StatusEnum.Close;
            var dtoStart = service.StartRoulette(new Domain.Dtos.RouletteStartDto()
            {
                Id = dtoAdd.Id
            });
            Assert.IsTrue(dtoStart.Result == Domain.Enums.ResultEnum.Denied);
        }

        [TestMethod]
        public void CloseRoueletteWinningByNumberAndColorTest()
        {
            var cache = new MockCache();
            var random = new MockGenerateRandom();
            IRouletteService service = new RouletteService(cache, random);
            var dtoAdd = service.AddRoulette();
            var dtoStart = service.StartRoulette(new Domain.Dtos.RouletteStartDto
            {
                Id = dtoAdd.Id
            });
            service.Bet("mna", new Domain.Dtos.RouletteBetDto
            {
                IdRoulette = dtoAdd.Id,
                CashAmount = 1000,
                Color = Domain.Enums.ColorEnum.Red,
                Number = 10
            });
            var dtoClose = service.CloseRoulette(new Domain.Dtos.RouletteCloseDto()
            {
                Id = dtoAdd.Id
            });
            Assert.IsTrue(dtoClose.WinningNumber == 10);
            Assert.IsTrue(dtoClose.Bets[0].WinnerAmount == 6800);
        }
    }
}