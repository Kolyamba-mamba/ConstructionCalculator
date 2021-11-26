using ConstructionCalculator.Api.Helpers;
using ConstructionCalculator.Api.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Controllers
{

//    Вообще в результате нам нужно только количество арматуры(но у нас оно всегда 5 будет в данном случае) и диаметр
//Еще можно выводить h, Np, Mmax, As
//По этим данным обычно преподы проверяют курсач
//Т.е.для моего случая самое главное выводить 32
    [Route("[controller]")]
    public class CalculateController : Controller
    {
        /// <summary>
        /// Роут расчета сетны в грунте с распоркой
        /// </summary>
        /// <param name="inputNumbers">Параметры для расчетов</param>
        /// <returns>Результат расчетов стены в грунте</returns>
        [Route("calculatewall")]
        [HttpPost]
        public async Task<ActionResult<CalculateResultDto>> CalculateWall([FromBody] InputNumbersDto inputNumbers)
        {
            var validatonResult = new ValidateInputDataHelper().ValidateInputData(inputNumbers);
            if (validatonResult != null && validatonResult != "")
                return BadRequest(validatonResult);

            var h = await Task.Run(() => DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
                .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(inputNumbers));

            var Np = await Task.Run(() => DeterminationForceInTheSpacerHelper.GetForceInTheSpacer(inputNumbers, h));

            var widthAndSquareArmature = await Task.Run(() => DeterminationSquareHelper.GetMomentAndSquare(inputNumbers, h, Np));

            var result = new CalculateResultDto(h, Np, widthAndSquareArmature.Mmax, widthAndSquareArmature.Square);

            return Ok(result);
        }

        /// <summary>
        /// Роут определения глубины заделки подпорной стены ниже дна котлована
        /// </summary>
        /// <param name="inputNumbers">Параметры для расчетов глубины заделки подпорной стены ниже дна котлована</param>
        /// <returns>Глубина заделки подпорной стены ниже дна котлована</returns>
        [Route("calculatedepatwall")]
        [HttpPost]
        public async Task<IActionResult> CalculateDepthWall([FromBody] InputNumbersDto inputNumbers)
        {
            var validatonResult = new ValidateInputDataHelper().ValidateInputData(inputNumbers);
            if (validatonResult != null && validatonResult != "")
                return BadRequest(validatonResult);

            var h = await Task.Run(() => DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
                .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(inputNumbers));

            return Ok(h);
        }

        /// <summary>
        /// Роут вычисления усилия в распорке
        /// </summary>
        /// <param name="inputNumbers">Параметры для расчетов</param>
        /// <returns>Усилие в распорке</returns>
        [Route("calculateforceinthespacer")]
        [HttpPost]
        public async Task<IActionResult> CalculateForceInTheSpacer([FromBody] InputNumbersDto inputNumbers)
        {
            var validatonResult = new ValidateInputDataHelper().ValidateInputData(inputNumbers);
            if (validatonResult != null && validatonResult != "")
                return BadRequest(validatonResult);

            var h = await Task.Run(() => DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
                .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(inputNumbers));

            var Np = await Task.Run(() => DeterminationForceInTheSpacerHelper.GetForceInTheSpacer(inputNumbers, h));

            return Ok(Np);
        }

        /// <summary>
        /// Роут определения площади поперечного сечения арматуры
        /// </summary>
        /// <param name="inputNumbers">Параметры для расчетов</param>
        /// <returns>Глубина заделки подпорной стены ниже дна котлована</returns>
        [Route("calculatemomentandsquarearmature")]
        [HttpPost]
        public async Task<IActionResult> CalculateMomentAndSquareArmature([FromBody] InputNumbersDto inputNumbers)
        {
            var validatonResult = new ValidateInputDataHelper().ValidateInputData(inputNumbers);
            if (validatonResult != null && validatonResult != "")
                return BadRequest(validatonResult);

            var h = await Task.Run(() => DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
    .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(inputNumbers));

            var Np = await Task.Run(() => DeterminationForceInTheSpacerHelper.GetForceInTheSpacer(inputNumbers, h));

            var result = await Task.Run(() => DeterminationSquareHelper.GetMomentAndSquare(inputNumbers, h, Np));

            return Ok(result);
        }
    }
}
