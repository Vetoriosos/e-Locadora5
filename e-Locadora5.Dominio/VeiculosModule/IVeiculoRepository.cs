﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Locadora5.Dominio.VeiculosModule
{
    public interface IVeiculoRepository
    {
        public void InserirNovo(Veiculo registro);

        public void Editar(int id, Veiculo registro);

        public bool Excluir(int id);

        public bool Existe(int id);

        public bool ExisteVeiculoComEssaPlaca(string placa);

        public Veiculo SelecionarPorId(int id);

        public List<Veiculo> SelecionarTodos();
    }
}
