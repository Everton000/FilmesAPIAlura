using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentResults;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Sessao;

namespace FilmesAPI.Services
{
    public class SessaoService
    {
        private FilmeContext _context;
        private IMapper _mapper;
        
        public SessaoService(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadSessaoDto AdicionaSessao(CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            
            return _mapper.Map<ReadSessaoDto>(sessao);
        }

        public List<ReadSessaoDto> RecuperaSessoes()
        {
            List<Sessao> sessoes = _context.Sessoes.ToList();

            if (sessoes != null)
            {
                return _mapper.Map<List<ReadSessaoDto>>(sessoes);
            }

            return null;
        }

        public ReadSessaoDto RecuperaSessaoPorId(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);

            if (sessao != null)
            {
                ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

                return sessaoDto;
            }

            return null;
        }

        public Result DeletaSessao(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);

            if (sessao == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _context.Remove(sessao);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}