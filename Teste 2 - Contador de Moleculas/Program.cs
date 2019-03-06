using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            leituraArquivos();
            processar();

            //Fim
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void processar()
        {
            //Faz o processamento dos dados, equação por equação e salva em um arquivo
            for (int pos = 0; pos < Controle.todasFormulas.Count; pos++)
            {
                List<Formula> EquacaoQuimica = new List<Formula>();

                #region Facilitando testes
                Console.WriteLine("Arquivo: " + (pos + 1));
                Console.WriteLine(Controle.todasFormulas[pos]);
                for (int posicao = 0; posicao < EquacaoQuimica.Count; posicao++)
                {
                    Console.Write(EquacaoQuimica[posicao].ToString() + " ");
                }
                Console.WriteLine();
                Console.WriteLine();
                #endregion
            }
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
