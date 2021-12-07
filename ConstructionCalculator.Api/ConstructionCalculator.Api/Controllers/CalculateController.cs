using AutoMapper;
using ConstructionCalculator.Api.Helpers;
using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Models.DTO;
using ConstructionCalculator.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Controllers
{
    [Route("[controller]")]
    public class CalculateController : Controller
    {
        private readonly IRepository<CalculateEntity> _calculateRepository;
        private readonly IRepository<User> _userRepository;
        private readonly Mapper _mapper;

        public CalculateController(IRepository<User> userRepository, IRepository<CalculateEntity> calculateRepository, IAutomapperHelper automapperHelper)
        {
            _userRepository = userRepository;
            _calculateRepository = calculateRepository;
            _mapper = automapperHelper.GetAutomapper();
        }

        /// <summary>
        /// Роут расчета сетны в грунте с распоркой
        /// </summary>
        /// <param name="inputNumbers">Параметры для расчетов</param>
        /// <returns>Результат расчетов стены в грунте</returns>
        [Route("calculatewall")]
        [HttpPost]
        public async Task<ActionResult<CalculateResultDto>> CalculateWall([FromBody] InputNumbersDto inputNumbers)
        {
            var user = new User();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.Claims.ElementAt(1).Value;
                user = await _userRepository.GetById(new Guid(userId));
            }
            var validatonResult = new ValidateInputDataHelper().ValidateInputData(inputNumbers);
            if (validatonResult != null && validatonResult != "")
                return BadRequest(validatonResult);

            var h = await Task.Run(() => DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
                .GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(inputNumbers));

            var Np = await Task.Run(() => DeterminationForceInTheSpacerHelper.GetForceInTheSpacer(inputNumbers, h));

            var widthAndSquareArmature = await Task.Run(() => DeterminationSquareHelper.GetMomentAndSquare(inputNumbers, h, Np));

            var result = new CalculateResultDto(h, Np, widthAndSquareArmature.Mmax, widthAndSquareArmature.Square);

            var calculateEntity = new CalculateEntity() {
                User = user,
                H = inputNumbers.H,
                B = inputNumbers.B,
                S = inputNumbers.S,
                d1 = inputNumbers.d1,
                bf = inputNumbers.bf,
                q = inputNumbers.q,
                L1 = inputNumbers.L1,
                Power = inputNumbers.Power,
                gamma2 = inputNumbers.gamma2,
                c2 = inputNumbers.c2,
                fi2 = inputNumbers.fi2,
                h_result = result.h,
                Np = result.Np,
                Mmax = result.Mmax,
                As = result.As,
        };
            await _calculateRepository.Create(calculateEntity);

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

        /// <summary>
        /// Роут для показа истории расчетов пользователя
        /// </summary>
        /// <param></param>
        /// <returns>История расчетов</returns>
        [Authorize]
        [Route("showhistory")]
        [HttpGet]
        public async Task<IActionResult> ShowCalculateHistory()
        { 
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = HttpContext.User.Claims.ElementAt(1).Value;
            var user = await _userRepository.GetById(new Guid(userId));
            if (user == null)
                return Unauthorized("Пользователь не найден.");

            var calculate = _calculateRepository.Get().Where(c => c.User.Id == user.Id).Select(c => _mapper.Map<CalculateEntity, CalculateEntityDto>(c)).ToList();

            return Ok(calculate);
        }
    }
}
