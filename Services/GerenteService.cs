using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentResults;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;

namespace FilmesAPI.Services
{
    public class GerenteService
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public GerenteService(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();

            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public List<ReadGerenteDto> RecuperaGerentes()
        {
            List<Gerente> gerentes = _context.Gerentes.ToList();

            if (gerentes != null)
            {
                List<ReadGerenteDto> readGerenteDtos = _mapper.Map<List<ReadGerenteDto>>(gerentes);
                return readGerenteDtos;
            }

            return null;
        }

        public ReadGerenteDto RecuperaGerentePorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

                return gerenteDto;
            }

            return null;
        }

        public Result AtualizaGerente(int id, UpdateGerenteDto gerenteDto)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }

            _mapper.Map(gerenteDto, gerente);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }

            _context.Remove(gerente);
            _context.SaveChanges();
            
            return Result.Ok();
        }
    } 
}