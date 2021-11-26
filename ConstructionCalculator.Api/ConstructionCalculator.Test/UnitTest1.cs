using NUnit.Framework;
using ConstructionCalculator.Api.Controllers;
using ConstructionCalculator.Api.Helpers;
using ConstructionCalculator.Api.Models.DTO;
using System.Threading.Tasks;

namespace ConstructionCalculator.Test
{
    public class Tests
    {
        private InputNumbersDto GetValidDto()
        {
            return new InputNumbersDto
            {
                H = 13,
                B = 14,
                S = 4.9,
                d1 = 1.2,
                bf = 0.9,
                q = 100,
                L1 = 2.2,
                Power = 10,
                gamma2 = 15,
                c2 = 24,
                fi2 = 20
            };
        }

        private InputNumbersDto GetInvalidDto()
        {
            return new InputNumbersDto
            {
                H = 13,
                B = 14,
                S = 0,
                d1 = 1.2,
                bf = 0.9,
                q = 100,
                L1 = -1,
                Power = 10,
                gamma2 = 15,
                c2 = 24,
                fi2 = 20
            };
        }

        [Test]
        public void EmptyInputDataTest()
        {
            var result = new ValidateInputDataHelper().ValidateInputData(null);
            Assert.AreEqual("Входные параметры не могут быть пустыми, проверьте корректность ввода.", result);
        }

        [Test]
        public void IncorretValueInInputdataTest()
        {
            var invalidDto = GetInvalidDto();
            var result = new ValidateInputDataHelper().ValidateInputData(invalidDto);
            Assert.AreEqual("Входные параметры Шаг распорок, Расстояние от оси фундамента здания №1 до подпорной стены должны быть больше 0.", result);
        }

        [Test]
        public void ValidDataTest()
        {
            var validDto = GetValidDto();
            var result = new ValidateInputDataHelper().ValidateInputData(validDto);
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CorrectCalculatedDepthOfSealingOfRetainingWallBelowTheBottomOfPitTest()
        {
            var validDto = GetValidDto();
            var result = DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
                .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(validDto);
            Assert.AreEqual(6.6, result);
        }

        [Test]
        public void CorrectCalculatedForceInTheSpacerTest()
        {
            var validDto = GetValidDto();
            var result = DeterminationForceInTheSpacerHelper.GetForceInTheSpacer(validDto, 6.6);
            Assert.AreEqual(301.91, result);
        }

        [Test]
        public void CorrectCalculatedMomentAndSquareTest()
        {
            var validDto = GetValidDto();
            var result = DeterminationSquareHelper.GetMomentAndSquare(validDto, 6.6, 301.91);
            Assert.AreEqual(1208.58, result.Mmax);
            Assert.AreEqual(0.00417, result.Square);
        }
    }
}