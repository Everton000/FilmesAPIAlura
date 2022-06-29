using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FluentResults;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Services;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService { get; set; }

        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AdicionaSessao([FromBody] CreateSessaoDto sessaoDto)
        {
            ReadSessaoDto readDto = _sessaoService.AdicionaSessao(sessaoDto);
            
            return CreatedAtAction(nameof(RecuperaSessaoPorId), new { Id = readDto.Id }, readDto);
        }

        [HttpGet()]
        public IActionResult RecuperaSessoes()
        {
            List<ReadSessaoDto> readDto = _sessaoService.RecuperaSessoes();

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaSessaoPorId(int id)
        {
            ReadSessaoDto readDto = _sessaoService.RecuperaSessaoPorId(id);

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaSessao(int id)
        {
            Result resultado = _sessaoService.DeletaSessao(id);

            if (resultado.IsFailed)
            { 
                return NotFound();
            }

            return NoContent();
        }
    }
}