﻿using e_Locadora5.Dominio.VeiculosModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Locadora5.Aplicacao.VeiculoModule
{
    public class VeiculoAppService
    {
        private readonly IVeiculoRepository veiculoRepository;

        public VeiculoAppService(IVeiculoRepository veiculoRepository)
        {
            this.veiculoRepository = veiculoRepository;
        }
        public string InserirNovo(Veiculo registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                veiculoRepository.InserirNovo(registro);
            }

            return resultadoValidacao;
        }

        public string Editar(int id, Veiculo registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                veiculoRepository.Editar(id, registro);
            }

            return resultadoValidacao;
        }

        public bool Excluir(int id)
        {
            try
            {
                veiculoRepository.Excluir(id);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Existe(int id)
        {
            return veiculoRepository.Existe(id);
        }

        public Veiculo SelecionarPorId(int id)
        {
            return veiculoRepository.SelecionarPorId(id);
        }

        public List<Veiculo> SelecionarTodos()
        {
            return veiculoRepository.SelecionarTodos();
        }
    }
}
