using ConstructionCalculator.Api.Helpers;
using ConstructionCalculator.Api.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Controllers
{
    [Route("[controller]")]
    public class CalculateController : Controller
    {
        [Route("calculatewall")]
        [HttpPost]
        public async Task<IActionResult> CalculateWall([FromBody] InputNumbersDto inputNumbers)
        {
            if (inputNumbers == null)
                return BadRequest("The input data is null");

            var h = DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
                .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(inputNumbers);

            var Np = DeterminationForceInTheSpacerHelper.GetForceInTheSpacer(inputNumbers, h);

            return Ok(Np);
        }

        [Route("calculatedepatwall")]
        [HttpPost]
        public async Task<IActionResult> CalculateDepthWall([FromBody] InputNumbersDto inputNumbers)
        {
            if (inputNumbers == null)
                return BadRequest("The input data is null");

            var h = DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
                .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(inputNumbers);

            return Ok(h);
        }

        [Route("calculateforceinthespacer")]
        [HttpPost]
        public async Task<IActionResult> CalculateForceInTheSpacer([FromBody] InputNumbersDto inputNumbers)
        {
            if (inputNumbers == null)
                return BadRequest("The input data is null");

            var h = DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
                .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(inputNumbers);

            var Np = DeterminationForceInTheSpacerHelper.GetForceInTheSpacer(inputNumbers, h);

            return Ok(Np);
        }
    }
}
