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
    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto filmeDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(filmeDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemaPorId), new { Id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IActionResult RecuraCinemas()
        {
            return Ok(_context.Cinemas);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemaPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema != null)
            {
                ReadCinemaDto filmeDto = _mapper.Map<ReadCinemaDto>(cinema);

                return Ok(filmeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto filmeDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }

            _mapper.Map(filmeDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

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