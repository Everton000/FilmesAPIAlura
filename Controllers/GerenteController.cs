using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentResults;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using FilmesAPI.Services;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        public GerenteService _gerenteService { get; set; }
        
        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = _gerenteService;
        }

        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            ReadGerenteDto readDto = _gerenteService.AdicionaGerente(gerenteDto);

            return CreatedAtAction(nameof(RecuperaGerentePorId), new { Id = readDto.Id }, readDto);
        }

        [HttpGet()]
        public IActionResult RecuperaGerentes()
        {
            List<ReadGerenteDto> readDto = _gerenteService.RecuperaGerentes();

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGerentePorId(int id)
        {
            ReadGerenteDto readDto = _gerenteService.RecuperaGerentePorId(id);

            if (readDto != null)
            {
                return Ok(readDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaGerente(int id, [FromBody] UpdateGerenteDto gerenteDto)
        {
            Result resultado = _gerenteService.AtualizaGerente(id, gerenteDto);

            if (resultado.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaGerente(int id)
        {
            Result resultado = _gerenteService.DeletaGerente(id);

            if (resultado.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}