using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentResults;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;

namespace FilmesAPI.Services
{
    public class EnderecoService
    {
        private FilmeContext _context;
        private IMapper _mapper;
        
        public EnderecoService(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadEnderecoDto AdicionaEndereco(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            
            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public List<ReadEnderecoDto> RecuperaEnderecos()
        {
            List<Endereco> enderecos = _context.Enderecos.ToList();

            if (enderecos != null)
            {
                return _mapper.Map<List<ReadEnderecoDto>>(enderecos);
            }

            return null;
        }

        public ReadEnderecoDto RecuperaEnderecoPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

                return enderecoDto;
            }

            return null;
        }

        public Result AtualizaEndereco(int id, UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _context.Remove(endereco);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}