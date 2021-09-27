﻿using e_Locadora5.Dominio.CupomModule;
using e_Locadora5.Infra.Log;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Locadora5.Aplicacao.CupomModule
{
    public class CupomAppService
    {
        private readonly ICupomRepository cupomRepository;

        public CupomAppService(ICupomRepository cupomRepo)
        {
            GeradorDeLog.ConfigurarLog();
            cupomRepository = cupomRepo;
        }

        public string InserirNovo(Cupons cupons)
        {
            string resultadoValidacao = cupons.Validar();
            
            if(cupomRepository.ExisteCupomMesmoNome(cupons.Nome))
            {
                Log.Warning("Já há um cupom com este nome cadastrado {nome}", cupons.Nome);
                return "Já há um cupom com este nome cadastrado";
            }
            bool cupomValido = resultadoValidacao == "ESTA_VALIDO";
            if (cupomValido)
            {
                try
                {
                    cupomRepository.InserirNovo(cupons);
                    Log.Information("Cupom {cupons} foi inserido com sucesso.", cupons);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Não foi possível inserir o cupom {cupons}", cupons);
                }
            }
            else
                Log.Warning("Cupom inválido: {resultadoValidacao}", resultadoValidacao);
            return resultadoValidacao;
        }

        public bool Existe(int id)
        {
            try
            {
                bool existe = cupomRepository.Existe(id);
                Log.Information("Verificado se existe o cupom com id {idCupom}", id);
                return existe;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Não foi possível verificar se existe o cupom com id {idCupom}", id);
                return false;
            }
        }

        public string Editar(int id, Cupons cupons)
        {
            string resultadoValidacao = cupons.Validar();

            if (cupomRepository.ExisteCupomMesmoNome(cupons.Nome))
            {
                Log.Warning("Já há um cupom com este nome cadastrado {nome}", cupons.Nome);
                return "Já há um cupom com este nome cadastrado";
            }
            bool cupomValido = resultadoValidacao == "ESTA_VALIDO";
            if (cupomValido)
            {
                try
                {
                    cupons.Id = id;
                    cupomRepository.Editar(id,cupons);
                    Log.Information("Cupom {cupons} foi editado com sucesso.", cupons);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Não foi possível editar o cupom {cupons}", cupons);
                }
            }
            else
                Log.Warning("Cupom inválido: {resultadoValidacao}", resultadoValidacao);
            return resultadoValidacao;
        }

        public bool Excluir(int id)
        {
            try
            {
                cupomRepository.Excluir(id);
                Log.Information("Cupom do id {idCupom} foi excluído com sucesso", id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Não foi possível excluir o cupom com id {id}", id);
                return false;
            }

            return true;
        }

        public List<Cupons>? SelecionarTodos()
        {
            try
            {
                List<Cupons> todosCupons = cupomRepository.SelecionarTodos();
                Log.Information("Selecionado todos os cupons {todosCupons}", todosCupons);
                return todosCupons;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Não foi possível selecionar todos os cupons");
                return null;
            }
        }

        public Cupons? SelecionarPorId(int id)
        {
            try
            {
                Cupons cupomSelecionado = cupomRepository.SelecionarPorId(id);
                Log.Information("Selecionado cupom {cupomSelecionado}", cupomSelecionado);
                return cupomSelecionado;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Não foi possível selecionar o cupom com id {idCupom}", id);
                return null;
            }
        }

        public string Validar(Cupons novoCupons, int id = 0)
        {
            //validar placas iguais
            if (novoCupons != null)
            {
                if (id != 0)
                {//situação de editar
                    int countCuponsIguaiss = 0;
                    List<Cupons> todosCupons = SelecionarTodos();
                    foreach (Cupons cupons in todosCupons)
                    {
                        if (novoCupons.Nome.Equals(cupons.Nome) && cupons.Id != id)
                            countCuponsIguaiss++;
                    }
                    if (countCuponsIguaiss > 0)
                        return "Cupom já cadastrada, tente novamente.";
                }
                else
                {//situação de inserir
                    int countTaxasIguais = 0;
                    List<Cupons> todosCupons = SelecionarTodos();
                    foreach (Cupons cupons in todosCupons)
                    {
                        if (novoCupons.Nome.Equals(cupons.Nome))
                            countTaxasIguais++;
                    }
                    if (countTaxasIguais > 0)
                        return "Cupom já cadastrada, tente novamente.";
                }
            }
            return "ESTA_VALIDO";
        }
    }
}
