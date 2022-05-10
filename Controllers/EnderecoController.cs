using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using AutoMapper;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco cinema = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecoPorId), new { Id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IActionResult RecuraEnderecos()
        {
            return Ok(_context.Enderecos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecoPorId(int id)
        {
            Endereco cinema = _context.Enderecos.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(cinema);

                return Ok(enderecoDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Endereco cinema = _context.Enderecos.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }

            _mapper.Map(enderecoDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Endereco cinema = _context.Enderecos.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }

            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }
    }
}