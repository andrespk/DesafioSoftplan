using App1.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace App1.Controllers
{
    [ApiController]
    public class InterestsController : ControllerBase
    {
        [HttpGet]
        [Route("/api/taxaJuros")]
        public IActionResult GetRate()
        {
            /// <summary>
            /// API para recuperar o valor da taxa de Juros
            /// </summary>
            /// <returns>taxa de juros</returns>
            return Ok(0.01);
        }

        [HttpGet]
        [Route("/api/showmethecode")]
        public IActionResult ShowMeTheCode()
        {
            /// <summary>
            /// API para recuperar o valor da taxa de Juros
            /// </summary>
            /// <returns>taxa de juros</returns>
            return Ok("https://github.com/andrespk/desafiosoftplan");
        }

        [HttpGet]
        [Route("/api/calculaJuros")]
        public IActionResult Calc([FromQuery] InterestsInputs inputs)
        {
            /// <summary>
            /// API para cacular o valor do Juros
            /// </summary>
            /// <param name="valorInicial">Valor inicial do Montante</param>
            /// <param name="meses">Quantidade de meses</param>
            /// <returns>valor do juros</returns>
            if (inputs == null || inputs.meses < 1 || inputs.valorInicial <= 0) return BadRequest();
            var rate = (double)(GetRate() as OkObjectResult).Value;
            var interests = inputs.valorInicial * Math.Pow(1 + rate, inputs.meses);
            return Ok(Math.Truncate(interests) + double.Parse((interests - Math.Truncate(interests)).ToString().Substring(0, 4)));
        }
    }
}