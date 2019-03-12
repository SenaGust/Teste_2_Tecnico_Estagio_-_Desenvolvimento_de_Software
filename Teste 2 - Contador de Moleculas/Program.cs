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
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void processar()
        {
            //Faz o processamento dos dados, equação por equação e salva em um arquivo
            int pos;
            for (pos = 0; pos < Controle.todasFormulas.Count; pos++)
            {
                Regex regex;
                List<Formula> EquacaoQuimica = new List<Formula>();

                regex = new Regex(@"([A-Z][a-z]?)");
                foreach (Match item in regex.Matches(Controle.todasFormulas[pos]))
                    if (pesquisaElemento(item.Groups[1].Value, EquacaoQuimica) == -1)
                        EquacaoQuimica.Add(new Formula(item.Groups[1].Value)); //se não foi criado, criar. Se não existe, não é necessário criar outro objeto

                //contabilizar os elementos
                contabilizarElementosRecursivo(Controle.todasFormulas[pos], 1, EquacaoQuimica);

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

                gerarArquivos(EquacaoQuimica, pos + 1);
            }
            Console.WriteLine("Foram Criados {0} arquivos.", pos);
        }
        static int pesquisaElemento(string Elemento, List<Formula> ondeProcurar)
        {
            for (int posicao = 0; posicao < ondeProcurar.Count; posicao++)
                if (ondeProcurar[posicao].ElementoQuimico == Elemento)
                    return posicao; //Se encontrar o elemento, retorna a posição onde o item está
            return -1;  //caso não encontre, retorna -1 (flag)
        }
        static void contabilizarElementosRecursivo(string equacaoQuimica, int indiceMultiplica, List<Formula> listaElementos)
        {
            /*
             * Grupos: 1 e 2 - Elemento quimico com a sua quantidade
             * Grupos: 3 e 4 - Elementos que estão dentro de parenteses, chaves ou colchetes
             */
            Regex regex = new Regex(@"(?<1>[A-Z][a-z]?)(?<2>\d*)|\{(?<3>.*?)\}(?<4>\d*)|\[(?<3>.*?)\](?<4>\d*)|\((?<3>.*?)\)(?<4>\d*)?");
            foreach (Match item in regex.Matches(equacaoQuimica))
            {
                if (item.Groups[3].Success) //grupo de elementos dentro de parentêses, colchetes ou chaves
                    if (item.Groups[4].Value.Length == 0)
                        contabilizarElementosRecursivo(item.Groups[3].Value, indiceMultiplica, listaElementos);
                    else
                        contabilizarElementosRecursivo(item.Groups[3].Value, Convert.ToInt32(item.Groups[4].Value) * indiceMultiplica, listaElementos);
                else //Base
                    if (item.Groups[2].Value.Length == 0)
                        listaElementos[pesquisaElemento(item.Groups[1].Value, listaElementos)].Quantidade += 1 * indiceMultiplica;
                    else
                        listaElementos[pesquisaElemento(item.Groups[1].Value, listaElementos)].Quantidade += Convert.ToInt32(item.Groups[2].Value) * indiceMultiplica;
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