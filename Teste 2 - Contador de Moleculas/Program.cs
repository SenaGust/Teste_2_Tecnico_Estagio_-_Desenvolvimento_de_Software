using System;
using System.Collections.Generic;
using System.IO;
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
    class Controle
    {
        public static List<string> todasFormulas = new List<string>();
    }
    class Program
    {
        static void Main(string[] args)
        {

        }

        #region Arquivos
        static void gerarArquivos(List<Formula> teste, int indice)
        {
            //Escreve arquivo por arquivo, dizendo quais foram as quantidades de cada elemento quimico passado pelo arquivo .in
            StreamWriter escrita = new StreamWriter("teste" + indice + ".out");
            for (int pos = 0; pos < teste.Count; pos++)
            {
                escrita.Write(teste[pos].ToString() + " ");
            }
            escrita.Close();
        }
        static void leituraArquivos()
        {
            //leitura e armazenamento de todos os arquivos com a sintaxe "testeX.in" na lista 'todasFormulas', sendo que X deve estar em ordem crescente, sem 'buracos'.
            StreamReader leitura;
            for (int indice = 1; File.Exists("teste" + indice + ".in"); indice++)
            {
                leitura = new StreamReader("teste" + indice + ".in");
                Controle.todasFormulas.Add(leitura.ReadLine());
                leitura.Close();
            }
        }
        #endregion
    }
}
