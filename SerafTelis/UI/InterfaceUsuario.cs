using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SerafTelis.model.dao.implementacao;
using SerafTelis.model.entidades;

namespace SerafTelis.UI
{
    public static class InterfaceUsuario
    {
        // FORMATAÇÃO
        // Edição da Linha normal
        public static void VetorCriarUmaLinha(int tamanhoDaLinha, string tipoDaLinha) { 
            for (int i = 1; i <= tamanhoDaLinha; i++)
            {
                Console.Write(tipoDaLinha);
            }
            Console.Write("\n");
        }

        // Cria uma Linha nomal do mesmo tamanho da Linha com título
        private static void VetorCriarUmaLinha(int tamanhoDaLinha, string tipoDaLinha, int quantidadeDePalavras) {
            for (int i = 1; i <= tamanhoDaLinha + quantidadeDePalavras; i++)
            {
                Console.Write(tipoDaLinha);
            }
            Console.Write("\n");
        }
        // Criar uma Linha com título
        public static void VetorCriarUmaLinhaComTitulo(int tamanhoDaLinha, string tipoDaLinha, string titulo) {
            int tamanhoTotalDaLinha = (int)(Math.Round((decimal)tamanhoDaLinha / 2));

            for (int i = 0; i < tamanhoTotalDaLinha - 1; i++)
            {
                Console.Write(tipoDaLinha);
            }
            Console.Write(titulo);
            for (int i = 1; i <= tamanhoTotalDaLinha + 1; i++)
            {
                Console.Write(tipoDaLinha);
            }
            Console.Write("\n");
        }
        
        // Edição do Vetor de Inicialização de uma seleção de Opções
        public static void VetorEscolherOpcoes(ArrayList opcoesDoMenu, int tamanhoDaLinha, string tipoDaLinha, string titulo) {
            VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
            
            foreach (var opcoes in opcoesDoMenu)
            {
                Console.WriteLine("Selecione a opção [" + (opcoesDoMenu.IndexOf(opcoes) + 1) 
                                + "] para " + opcoes.ToString());
            }

            VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
        }


        // Situações do CRUD

        // Mostra se CADASTRAR do funcionario
        public static void VetorStatusCadastrar(int tamanhoDaLinha, string tipoDaLinha, string titulo)
        {
            VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
            if (SituacaoFuncionario.IsCriarFuncionario())
            {
                Console.WriteLine("CADASTRADO com SUCESSO!");
            }
            else
            {
                Console.WriteLine("ERRO ao CADASTRADO o funcionario. \nTente de novo!");
            }
            VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
        }

        // Mostra se o funcionario existe ou não

        public static void VetorStatusBuscarById(int tamanhoDaLinha, string tipoDaLinha, string titulo, bool situacaoDoFuncionario, bool situacaoDoProduto)
        {
            if (situacaoDoFuncionario && (!situacaoDoProduto))
            {
                VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
                Console.WriteLine("Funcionario não encontrado!");
                VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
            }
            else if (situacaoDoProduto && (!situacaoDoFuncionario))
            {
                VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
                Console.WriteLine("Produto não encontrado!");
                VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
            }
        }

        // Mostra se o funcionario ou produto foi atualizado ou não
        public static void VetorStatusFoiAtualizado(int tamanhoDaLinha, string tipoDaLinha, string titulo, bool situacaoDoFuncionario, bool situacaoDoProduto)
        {
            if (situacaoDoFuncionario && (!situacaoDoProduto))
            {
                Console.WriteLine("\n");
                VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
                Console.WriteLine("Funcionario atualizado com SUCESSO!");
                VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
                Console.WriteLine("\n");
            }
            else if (situacaoDoProduto && (!situacaoDoFuncionario))
            {
                Console.WriteLine("\n");
                VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
                Console.WriteLine("Funcionario atualizado com SUCESSO!");
                VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
                Console.WriteLine("\n");
            }
        }

        // Mostra se o usuário foi deletado ou não
        public static void VetorStatusFoiDeletado(int tamanhoDaLinha, string tipoDaLinha, string titulo, bool situacaoDoFuncionario, bool situacaoDoProduto)
        {
            if (situacaoDoFuncionario)
            {
                Console.WriteLine("\n");
                VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
                Console.WriteLine("Funcionario deletado com SUCESSO!");
                VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
                Console.WriteLine("\n");
            }
            else if (situacaoDoProduto)
            {
                Console.WriteLine("\n");
                VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
                Console.WriteLine("Produto deletado com SUCESSO!");
                VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
                Console.WriteLine("\n");
            }
        }

        // VENDA DO PRODUTO

        // Mostrar se o produto foi vendido ou não
        public static void VetorStatusFoiVendido(int tamanhoDaLinha, string tipoDaLinha, string titulo, bool situacaoDaVenda)
        {
            if (situacaoDaVenda)
            {
                Console.WriteLine();
                VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);
                Console.WriteLine("O produto foi adicioado para venda com SUCESSO!");
                VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
                Console.WriteLine();
            }
        }

        // Amostrar os dados do banco 

        // Mostra de TODOS os relatórios: Funcioário e Produto (separadamente)
        public static void VetorGerarRelatorios(int tamanhoDaLinha, string tipoDaLinha, string titulo, List<Funcionario> listaDeFuncionarios, List<Produto> listaDeProdutos)
        {

            VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);

            if (listaDeFuncionarios != null && listaDeProdutos == null)
            {
                foreach (Funcionario listFuncionario in listaDeFuncionarios)
                {
                    Console.WriteLine("Id: " + listFuncionario.Id);
                    Console.WriteLine("Nome: " + listFuncionario.Nome);
                    Console.WriteLine("Sobrenome: " + listFuncionario.Sobrenome);
                    Console.WriteLine("Email: " + listFuncionario.Email);
                    VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
                }
            }
            else if (listaDeProdutos != null && listaDeFuncionarios == null)
            {
                foreach (Produto listProduto in listaDeProdutos)
                {
                    Console.WriteLine("Id: " + listProduto.Id);
                    Console.WriteLine("Nome: " + listProduto.Nome);
                    Console.WriteLine("Preco: " + listProduto.Preco);
                    Console.WriteLine("Quantidade: " + listProduto.Quantidade);
                    VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
                }
            }
        }

        // Mostrar o funcionario ou produto específico (Buscar funcionario pelo ID)
        public static void VetorGerarRelatorio(int tamanhoDaLinha, string tipoDaLinha, string titulo, Funcionario funcionario, Produto produto)
        {

            VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);

            if (funcionario != null && produto == null)
            {
                Console.WriteLine("Id: " + funcionario.Id);
                Console.WriteLine("Nome: " + funcionario.Nome);
                Console.WriteLine("Sobrenome: " + funcionario.Sobrenome);
                Console.WriteLine("Email: " + funcionario.Email);
                Console.WriteLine("Senha: " + funcionario.Senha);
                Console.WriteLine("Comissão: " + funcionario.Comissao);
            }
            else if (produto != null && funcionario == null)
            {
                Console.WriteLine("Id: " + produto.Id);
                Console.WriteLine("Nome: " + produto.Nome);
                Console.WriteLine("Preco: " + produto.Preco);
                Console.WriteLine("Quantidade: " + produto.Quantidade);
            }

            VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
        }

        public static void VetorGerarNotaFiscal(int tamanhoDaLinha, string tipoDaLinha, Venda notaFiscalDaVenda, string titulo)
        {
            VetorCriarUmaLinhaComTitulo(tamanhoDaLinha, tipoDaLinha, titulo);

            Console.WriteLine("Nome do produto: " + notaFiscalDaVenda.Produto.Nome);
            Console.WriteLine("Quantidade (em metros): " + notaFiscalDaVenda.Produto.Quantidade);
            Console.WriteLine("Preço (por metro): R$" + notaFiscalDaVenda.Produto.Preco);
            Console.WriteLine("Data da compra: " + notaFiscalDaVenda.DataAtual);
            Console.WriteLine("Total: R$" + notaFiscalDaVenda.Total);

            VetorCriarUmaLinha(tamanhoDaLinha, tipoDaLinha, titulo.Count());
            
            VetorGerarArquivoTXT(notaFiscalDaVenda);
        }

        // Relatório da venda
        private static void VetorGerarArquivoTXT(Venda notaFiscalDaVenda)
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\SerafTelis\\Relatório_de_vendas.txt", true, Encoding.ASCII);
                sw.WriteLine("----------------" + notaFiscalDaVenda.DataAtual + "----------------");
                sw.WriteLine("Codigo da venda: " + notaFiscalDaVenda.Id);
                sw.WriteLine("Codigo do produto: " + notaFiscalDaVenda.Produto.Id);
                sw.WriteLine("Nome do produto: " + notaFiscalDaVenda.Produto.Nome);
                sw.WriteLine("Quantidade (em metros): " + notaFiscalDaVenda.Produto.Quantidade);
                sw.WriteLine("Preço (por metro): R$" + notaFiscalDaVenda.Produto.Preco);
                sw.WriteLine("Data da compra: " + notaFiscalDaVenda.DataAtual);
                sw.WriteLine("Total: R$" + notaFiscalDaVenda.Total);
                sw.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Falha em gerar o relatório de vendas, " +
                    "verifique o caminho do arquivo: C:\\SerafTelis. Cod.:00c18 \n" + error);
            }
            finally
            {
                Console.WriteLine("Verifique o arquivo em C:\\SerafTelis");
            }
        }

        // Pausa aplicação e limpar a tela
        public static void VetorPausarAplicacao()
        {
            Console.Write("Pressione ENTER para voltar ao menu...");
            string objqualquer = Console.ReadLine();
            Console.Clear();
        }
    }
}
