using System;
using System.Collections;
using System.Collections.Generic;
using SerafTelis.model.dao;
using SerafTelis.model.dao.implementacao;
using SerafTelis.model.entidades;
using SerafTelis.StatusDoSistema;
using SerafTelis.UI;

namespace SerafTelis
{
    public class Programa
    {
        public static void Main(String[] args) {

            // Constantes de estilização

            /* Atenção!
             * TUDO o que estiver "Vetor" em um método, estará representado a estilização do sistema
             * Se encontra na classe InterfaceUsuario
             */

            const int quantidadeDeLinhasVetor = 38; /* Ele só fica alinhado se for número par, por conta da divisão no 
                                                     * laço de repetição:
                                                     * Classe: InterfaceUsuario >> Método: VetorCriarUmaLinhaComTitulo()
                                                     * */
            const string tipoDeLinhasVetor = "-";
            const bool programaRodandoDoSistema = true;

            // Inicio do programa
            while (programaRodandoDoSistema)
            {
                // Apenas adicione uma opção para o menu inicial do sistema
                ArrayList opcoesDeInicializacaoDoMenuSistema = new ArrayList {
                    "Área do Funcionário...",
                    "Área do Produto...",
                    "Vender", 
                    "Sair do sistema"
                };

                InterfaceUsuario.VetorEscolherOpcoes(new ArrayList(opcoesDeInicializacaoDoMenuSistema),
                                                quantidadeDeLinhasVetor, tipoDeLinhasVetor, "Serafim Telas");

                int opcaoDeOperacaoDoSistema = 0;
                try
                {
                    Console.Write("Opção: ");
                    opcaoDeOperacaoDoSistema = Convert.ToInt32(Console.ReadLine());
                    // Enquanto a opção for 
                    while (opcaoDeOperacaoDoSistema.Equals(opcoesDeInicializacaoDoMenuSistema)
                            || opcaoDeOperacaoDoSistema > (opcoesDeInicializacaoDoMenuSistema.Count))
                    {
                        Console.WriteLine("Opa! \nDigite um valor válido ao menu!");
                        Console.Write("Opção: ");

                        opcaoDeOperacaoDoSistema = Convert.ToInt32(Console.ReadLine());
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                }

                IFuncionarioDAO funcionarioDAO = DAOCentral.CriarFuncionarioDao();
                IProdutoDAO produtoDAO = DAOCentral.CriarProdutoDao();

                // Área do Funcionário
                if (opcaoDeOperacaoDoSistema == 1)
                {
                    Console.Clear();
                    // ÁREA DO FUNCIONARIO
                    const bool programaRodandoDoFuncionario = true;

                    while (programaRodandoDoFuncionario)
                    {

                        ArrayList opcoesDeInicializacaoDoMenuFuncionario = new ArrayList {
                            "CADASTRAR funcionario",
                            "GERAR RELATÓRIO dos funcionarios",
                            "GERAR RELATÓRIO do funcionario (pelo ID)",
                            "ATUALIZAR os dados do funcionario (pelo ID)",
                            "REMOVER funcionario (pelo ID)",
                            "Voltar para o início"
                        };

                        InterfaceUsuario.VetorEscolherOpcoes(new ArrayList(opcoesDeInicializacaoDoMenuFuncionario),
                                                        quantidadeDeLinhasVetor, tipoDeLinhasVetor, "Área do Funcionário");
                        
                        int opcaoDeOperacaoDoFuncionario = 0;
                        try
                        {
                            Console.Write("Opção: ");
                            opcaoDeOperacaoDoFuncionario = Convert.ToInt32(Console.ReadLine());
                            while (opcaoDeOperacaoDoFuncionario.Equals(opcoesDeInicializacaoDoMenuFuncionario)
                                || opcaoDeOperacaoDoFuncionario > opcoesDeInicializacaoDoMenuFuncionario.Count)
                            {
                                Console.WriteLine("Opa! \nDigite um valor válido ao menu!");
                                Console.Write("Opção: ");

                                opcaoDeOperacaoDoFuncionario = Convert.ToInt32(Console.ReadLine());
                            }
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                        }

                        Console.WriteLine();
                        // Cadastrar funcionário
                        if (opcaoDeOperacaoDoFuncionario == 1)
                        {
                            // Cadastrar funcionario

                            string email;
                            string senha;
                            string nome;
                            string sobrenome;

                            InterfaceUsuario.VetorCriarUmaLinhaComTitulo(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "CADASTRAR");

                            Console.Write("Digite seu primeiro nome: ");
                            nome = Console.ReadLine();

                            // Mensagem para o usuário visualizar a resposta e válvula de escape 
                            if (nome == "-1") { InterfaceUsuario.VetorPausarAplicacao(); continue; }

                            Console.Write("Digite seu segundo sobrenome: ");
                            sobrenome = Console.ReadLine();
                            Console.Write("Digite um email válido: ");
                            email = Console.ReadLine().ToLower();
                            Console.Write("Digite uma senha válida: ");
                            senha = Console.ReadLine().ToLower();

                            Funcionario cadastroFuncionario = new Funcionario(nome, sobrenome, email, senha);

                            funcionarioDAO.Inserir(cadastroFuncionario);

                            InterfaceUsuario.VetorStatusCadastrar(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "SITUAÇÃO");
                        } // Resolvido
                        // GERAR RELATÓRIO dos funcionarios
                        else if (opcaoDeOperacaoDoFuncionario == 2)
                        {
                            var listaFuncionarios = funcionarioDAO.MostrarTodos();

                            InterfaceUsuario.VetorGerarRelatorios(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "RELATÓRIO DOS FUNCIOÁRIOS", listaFuncionarios, null);
                        } // Resolvido
                        // GERAR RELATÓRIO do funcionario(pelo ID)
                        else if (opcaoDeOperacaoDoFuncionario == 3)
                        {
                            Console.Write("Id do funcionário: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            // Mensagem para o usuário visualizar a resposta e válvula de escape 
                            if (id == -1) { InterfaceUsuario.VetorPausarAplicacao(); continue; }

                            Funcionario funcionario = funcionarioDAO.ProcurarPeloId(id);

                            if (SituacaoFuncionario.IsBuscarFuncionarioPeloId())
                            {
                                InterfaceUsuario.VetorGerarRelatorio(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                    "RELATÓRIO DE " + funcionario.Nome.ToUpper(), funcionario, null);
                            }

                            InterfaceUsuario.VetorStatusBuscarById(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                "SITUAÇÃO", !SituacaoFuncionario.IsBuscarFuncionarioPeloId(), false);
                        } // Resolvido
                        // ATUALIZAR dados do funcionario (pelo ID)
                        else if (opcaoDeOperacaoDoFuncionario == 4)
                        {
                            InterfaceUsuario.VetorCriarUmaLinhaComTitulo(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "ATUALIZAR FUNCIONÁRIO");

                            Console.Write("Id do funcionário: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            // Mensagem para o usuário visualizar a resposta e válvula de escape, caso o usuário digire uma opção do menu errada
                            if (id == -1) { InterfaceUsuario.VetorPausarAplicacao(); continue; }

                            if (SituacaoFuncionario.IsBuscarFuncionarioPeloId())
                            {
                                InterfaceUsuario.VetorGerarRelatorio(quantidadeDeLinhasVetor, tipoDeLinhasVetor,
                                "RELATÓRIO", funcionarioDAO.ProcurarPeloId(id), null);

                                Funcionario funcionario = new Funcionario();
                                funcionario.Id = id;
                                string nomeAtualizado;
                                string sobrenomeAtualizado;
                                string emailAtualizado;
                                string senhaAtualizado;

                                Console.Write("Digite o primeiro nome: ");
                                nomeAtualizado = Console.ReadLine();
                                funcionario.Nome = nomeAtualizado;

                                Console.Write("Digite seu segundo nome: ");
                                sobrenomeAtualizado = Console.ReadLine();
                                funcionario.Sobrenome = sobrenomeAtualizado;

                                Console.Write("Digite o email válido: ");
                                emailAtualizado = Console.ReadLine().ToLower();
                                funcionario.Email = emailAtualizado;

                                Console.Write("Digite a senha válida: ");
                                senhaAtualizado = Console.ReadLine().ToLower();
                                funcionario.Senha = senhaAtualizado;

                                funcionarioDAO.Atualizar(funcionario);

                                InterfaceUsuario.VetorStatusFoiAtualizado(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                    "SITUAÇÃO", SituacaoFuncionario.IsAtualizarFuncionario(), false); // Foi atualizado!
                            }
                            
                            InterfaceUsuario.VetorStatusBuscarById(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                "SITUAÇÃO", !SituacaoFuncionario.IsBuscarFuncionarioPeloId(), false); // ID não encontrado
                        } // Resolvido
                        // REMOVER funcionario (pelo ID)
                        else if (opcaoDeOperacaoDoFuncionario == 5)
                        {
                            // REMOVER funcionario (pelo ID)
                            Console.Write("Id do funcionario para DELETAR: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            // Mensagem para o usuário visualizar a resposta e válvula de escape                    
                            if (id == -1) { InterfaceUsuario.VetorPausarAplicacao(); continue; }
                            
                            Funcionario funcionario = funcionarioDAO.ProcurarPeloId(id);

                            if (SituacaoFuncionario.IsBuscarFuncionarioPeloId() && funcionario != null)
                            {
                                InterfaceUsuario.VetorGerarRelatorio(quantidadeDeLinhasVetor, tipoDeLinhasVetor,
                                "RELATÓRIO", funcionario, null);

                                Console.WriteLine("Tem certeza que deseja excluir " + funcionario.Nome + "? [S/N]");
                                string resp = Console.ReadLine();

                                if (resp.ToUpper() == "S")
                                {
                                    funcionarioDAO.DeletePeloId(id);
                                    InterfaceUsuario.VetorStatusFoiDeletado(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                        "SITUAÇÃO", SituacaoFuncionario.IsDeletarFuncionario(), false); // Foi deletado
                                }
                            }

                            InterfaceUsuario.VetorStatusBuscarById(quantidadeDeLinhasVetor, tipoDeLinhasVetor,
                                "SITUAÇÃO", !SituacaoFuncionario.IsBuscarFuncionarioPeloId(), false); // ID não encontrado
                        } // Resolvido
                        // Voltar para o início: Área do Funcionário
                        else
                        {
                            Console.WriteLine("Tem certeza que deseja sair da área do  funcionário? [S/N]");
                            string resp = Console.ReadLine();

                            if (resp.ToUpper().Equals("S"))
                            {
                                Console.WriteLine("Voltando para o menu principal \n");
                                break;
                            }
                            Console.Clear();
                        }
                        InterfaceUsuario.VetorPausarAplicacao();
                    }
                } // Resolvida
                // Área do Produto
                else if (opcaoDeOperacaoDoSistema == 2)
                {
                    Console.Clear();
                    // ÁREA DO PRODUTO
                    const bool programaRodandoDoProduto = true;

                    while (programaRodandoDoProduto)
                    {
                        ArrayList opcoesDeInicializacaoDoMenuProduto = new ArrayList {
                            "CADASTRAR produto",
                            "GERAR RELATÓRIO dos produtos",
                            "GERAR RELATÓRIO do produto (pelo ID)",
                            "ATUALIZAR os dados do produto (pelo ID)",
                            "REMOVER o produto (pelo ID)",
                            "Voltar para o início"
                        };

                        InterfaceUsuario.VetorEscolherOpcoes(new ArrayList(opcoesDeInicializacaoDoMenuProduto),
                                                        quantidadeDeLinhasVetor, tipoDeLinhasVetor, "Produto");

                        int opcaoDeOperacaoDoProduto = 0;
                        try
                        {
                            Console.Write("Opção: ");
                            opcaoDeOperacaoDoProduto = Convert.ToInt32(Console.ReadLine());

                            while (opcaoDeOperacaoDoProduto.Equals(opcoesDeInicializacaoDoMenuProduto)
                                || opcaoDeOperacaoDoProduto > opcoesDeInicializacaoDoMenuProduto.Count)
                            {
                                Console.WriteLine("Opa! \nDigite um valor válido ao menu!");
                                Console.Write("Opção: ");

                                opcaoDeOperacaoDoProduto = Convert.ToInt32(Console.ReadLine());
                            }
                        }
                        catch (FormatException)
                        {
                            Console.Clear();
                        }


                        Console.WriteLine();
                        // Cadastrar produto
                        if (opcaoDeOperacaoDoProduto == 1)
                        {
                            string nome;
                            double preco;
                            int quantidade;

                            InterfaceUsuario.VetorCriarUmaLinhaComTitulo(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "CADASTRAR");

                            Console.Write("Digite o nome do produto: ");
                            nome = Console.ReadLine();

                            // Mensagem para o usuário visualizar a resposta e válvula de escape, caso o usuário digire uma opção do menu errada
                            if (nome == "-1") { InterfaceUsuario.VetorPausarAplicacao(); continue; }

                            Console.Write("Digite o preco: ");
                            preco = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Digite a quantidade: ");
                            quantidade = Convert.ToInt32(Console.ReadLine().ToLower());

                            Produto cadastrarProduto = new Produto(nome, preco, quantidade);

                            produtoDAO.Inserir(cadastrarProduto);

                            InterfaceUsuario.VetorStatusCadastrar(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "SITUAÇÃO");
                        } // RESOLVIDO
                        // Gerar relatório dos Produtos
                        else if (opcaoDeOperacaoDoProduto == 2)
                        {
                            List<Produto> listaDosProdutos = produtoDAO.MostrarTodos();
                            InterfaceUsuario.VetorGerarRelatorios(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "RELATÓRIO DOS PRODUTOS", 
                                null, listaDosProdutos); 
                        } // RESOLVIDO
                        // GERAR RELATÓRIO do produto (pelo ID)
                        else if (opcaoDeOperacaoDoProduto == 3)
                        {
                            Console.Write("Id do produto: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            // Mensagem para o usuário visualizar a resposta e válvula de escape, caso o usuário digire uma opção do menu errada
                            if (id == -1) { InterfaceUsuario.VetorPausarAplicacao(); continue; }

                            Produto produto = produtoDAO.ProcurarPeloId(id);
                            
                            if (SituacaoProduto.IsBuscarProdutoPeloId())
                            {
                                InterfaceUsuario.VetorGerarRelatorio(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                    "RELATÓRIO DE " + produto.Nome.ToUpper(), null, produto);
                            }

                            InterfaceUsuario.VetorStatusBuscarById(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "SITUAÇÃO",
                                false, !SituacaoProduto.IsBuscarProdutoPeloId()); // Se o ID não for encontrado
                        } // RESOLVIDO
                        // ATUALIZAR os dados do produto(pelo ID)
                        else if (opcaoDeOperacaoDoProduto == 4)
                        {
                            InterfaceUsuario.VetorCriarUmaLinhaComTitulo(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "ATUALIZAR FUNCIONÁRIO");

                            Console.Write("Id do produto: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            // Mensagem para o usuário visualizar a resposta e válvula de escape, caso o usuário digire uma opção do menu errada
                            if (id == -1) { InterfaceUsuario.VetorPausarAplicacao(); continue; }

                            if (SituacaoProduto.IsBuscarProdutoPeloId())
                            {
                                InterfaceUsuario.VetorGerarRelatorio(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                    "RELATÓRIO", null, produtoDAO.ProcurarPeloId(id));

                                string nomeAtualizado;
                                double precoAtualizado;
                                int quantidadeAtualizado;

                                Console.Write("Digite o nome do produto: ");
                                nomeAtualizado = Console.ReadLine();

                                Console.Write("Digite o preço: ");
                                precoAtualizado = Convert.ToDouble(Console.ReadLine());

                                Console.Write("Digite a quantidade: ");
                                quantidadeAtualizado = Convert.ToInt32(Console.ReadLine());

                                Produto produto = new Produto(id, nomeAtualizado, precoAtualizado, quantidadeAtualizado);
                                produtoDAO.Atualizar(produto);

                                InterfaceUsuario.VetorStatusFoiAtualizado(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                    "SITUAÇÃO", false, SituacaoProduto.IsAtualizarProduto()); // Foi atualizado
                            }
                            InterfaceUsuario.VetorStatusBuscarById(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                "SITUAÇÃO", false, !SituacaoProduto.IsBuscarProdutoPeloId()); // ID não econtrado
                        } // RESOLVIDO
                        // REMOVER produto (pelo ID)
                        else if (opcaoDeOperacaoDoProduto == 5)
                        {
                            // REMOVER produto (pelo ID)

                            Console.Write("Id do produto para DELETAR: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            // Mensagem para o usuário visualizar a resposta e válvula de escape                    
                            if (id == -1) { InterfaceUsuario.VetorPausarAplicacao(); continue; }

                            Produto produto = produtoDAO.ProcurarPeloId(id);

                            if (SituacaoProduto.IsBuscarProdutoPeloId() && produto != null)
                            {
                                InterfaceUsuario.VetorGerarRelatorio(quantidadeDeLinhasVetor, tipoDeLinhasVetor,
                                "RELATÓRIO", null, produto);

                                Console.WriteLine("Tem certeza que deseja excluir " + produto.Nome + "? [S/N]");
                                string resp = Console.ReadLine();

                                if (resp.ToUpper() == "S")
                                {
                                    produtoDAO.DeletePeloId(id);
                                    InterfaceUsuario.VetorStatusFoiDeletado(quantidadeDeLinhasVetor, tipoDeLinhasVetor, 
                                        "SITUAÇÃO", false, SituacaoProduto.IsDeletarProduto());
                                }
                            }

                            InterfaceUsuario.VetorStatusBuscarById(quantidadeDeLinhasVetor, tipoDeLinhasVetor,
                                "SITUAÇÃO", false, !SituacaoProduto.IsBuscarProdutoPeloId());
                        } // RESOLVIDO
                        // Voltar para o início: Serafim Telas
                        else
                        {
                            Console.WriteLine("Tem certeza que deseja sair da área da produto? [S/N]");
                            string resp = Console.ReadLine();

                            if (resp.ToUpper().Equals("S"))
                            {
                                Console.WriteLine("CARREGANDO...\n\nAté breve!");
                                break;
                            }
                            Console.Clear();
                        }
                        InterfaceUsuario.VetorPausarAplicacao();
                    }
                } // Resolvida
                // Área de Venda
                else if (opcaoDeOperacaoDoSistema == 3) 
                {
                    InterfaceUsuario.VetorCriarUmaLinhaComTitulo(quantidadeDeLinhasVetor, tipoDeLinhasVetor, "VENDER PRODUTO");

                    bool programaDeVenda = true;
                    int idFuncionario;
                    int idProduto;
                    double metros;
                    string isoFormatoDataParaMySql = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));

                    IVendaDAO vendaDAO = DAOCentral.CriarVendaDao();

                    Console.Write("Digite o seu id: ");
                    idFuncionario = Convert.ToInt32(Console.ReadLine());
                    funcionarioDAO.ProcurarPeloId(idFuncionario);

                    if (SituacaoFuncionario.IsBuscarFuncionarioPeloId()) // Vaidar id do funcionario
                    {
                        // É possível que o funcioário venda mais de um produto
                        while (programaDeVenda)
                        {
                            Console.Write("Digite o id do produto: ");
                            idProduto = Convert.ToInt32(Console.ReadLine());
                            produtoDAO.ProcurarPeloId(idProduto);

                            // Caso o funcionário queira sair da venda
                            if (idProduto == -1) { InterfaceUsuario.VetorPausarAplicacao(); break; }

                            // Enquanto o id do produto estiver incorreto o sistema perguntará novamente o id do produto
                            while (!SituacaoProduto.IsBuscarProdutoPeloId())
                            {
                                Console.Write("Digite o id de um produto válido: ");
                                idProduto = Convert.ToInt32(Console.ReadLine());
                                produtoDAO.ProcurarPeloId(idProduto);
                            }

                            Console.Write("Digite a quantidade (em metros): ");
                            metros = Convert.ToDouble(Console.ReadLine());

                            Venda venda = new Venda(idFuncionario, idProduto, metros, isoFormatoDataParaMySql);
                            vendaDAO.VenderProduto(venda); // Vender produto
                            InterfaceUsuario.VetorStatusFoiVendido(quantidadeDeLinhasVetor, tipoDeLinhasVetor,
                                "SITUAÇÃO", SituacaoVenda.IsVenderProduto());

                            // Mostra os dados da compra
                            Venda notaFiscal = vendaDAO.GerarNotaFiscal();
                            
                            // Gera nota fiscal e arquivo em formato txt
                            InterfaceUsuario.VetorGerarNotaFiscal(quantidadeDeLinhasVetor, tipoDeLinhasVetor, notaFiscal, "RECIBO");

                            // Atualiza a comissao do Funcionario
                            if (SituacaoVenda.IsVenderProduto())
                            {
                                vendaDAO.AtualizarComissaoDoFuncionario(venda.IdFuncionario, notaFiscal.Total);
                                vendaDAO.AtualizarEstoque(venda.IdProduto, metros);
                            }
                            
                            Console.WriteLine("Deseja adicionar um produto?");
                            string resp = Console.ReadLine().ToUpper();
                            if (resp == "N")
                            {
                                Console.WriteLine("OBRIGADO POR COMPRAR NA SERAFIM TELAS");
                                break; // sair da venda do produto
                            }
                        }
                    }

                    // Manda uma mensagem caso não encontre o id do funcionário
                    InterfaceUsuario.VetorStatusBuscarById(quantidadeDeLinhasVetor, tipoDeLinhasVetor,
                                "SITUAÇÃO", !SituacaoFuncionario.IsBuscarFuncionarioPeloId(), false);
                    InterfaceUsuario.VetorPausarAplicacao();
                } // Completo
                // Sair do sistema
                else
                {
                    Console.WriteLine("Tem certeza que deseja sair do sistema? [S/N]");
                    string resp = Console.ReadLine();

                    if (resp.ToUpper().Equals("S"))
                    {
                        Console.WriteLine("CARREGANDO...\n\nAté breve!");
                        break;
                    }
                    Console.Clear();
                }
                InterfaceUsuario.VetorPausarAplicacao();
            }
        }
    }
}