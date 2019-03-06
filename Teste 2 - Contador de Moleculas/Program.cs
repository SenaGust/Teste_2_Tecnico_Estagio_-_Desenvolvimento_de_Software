using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_2___Contador_de_Moleculas
{
    class Formula
    {
        #region Atributos
        private string elementoQuimico;
        private int quantidade;
        #endregion

        #region Métodos Getters e Setters
        public string ElementoQuimico
        {
            get { return elementoQuimico; }
            set { elementoQuimico = value; }
        }
        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }
        #endregion

        #region Contrutor
        public Formula(string elemento)
        {
            elementoQuimico = elemento;
            quantidade = 0;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return elementoQuimico + ": " + quantidade + ".";
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
