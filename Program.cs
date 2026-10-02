using System;

namespace ProjetoRestaurante
{
 
    // CLASSE ITEM

    public class Item
    {
        private int id;
        private string descricao;
        private double preco;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public Item(int id, string descricao, double preco)
        {
            this.id = id;
            this.descricao = descricao;
            this.preco = preco;
        }

        public Item() : this(0, "", 0.0) { }
    }

    // CLASSE PEDIDO

    public class Pedido
    {
        private int id;
        private string cliente;
        private Item[] itens;
        private int qtdItens; // Controle interno da quantidade atual de itens

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public Item[] Itens
        {
            get { return itens; }
        }

        public Pedido(int id, string cliente)
        {
            this.id = id;
            this.cliente = cliente;
            this.itens = new Item[10]; // Máximo de 10 itens por pedido
            this.qtdItens = 0;
        }

        public Pedido() : this(0, "") { }

        public bool AdicionarItem(Item item)
        {
            if (qtdItens >= 10)
            {
                return false; // Limite de 10 itens atingido
            }

            itens[qtdItens] = item;
            qtdItens++;
            return true;
        }

        public bool RemoverItem(Item item)
        {
            int posicao = -1;

            // Busca pelo item com o mesmo ID
            for (int i = 0; i < qtdItens; i++)
            {
                if (itens[i] != null && itens[i].Id == item.Id)
                {
                    posicao = i;
                    break;
                }
            }

            if (posicao == -1)
            {
                return false; // Item não encontrado
            }

            // Desloca os elementos para preencher a lacuna
            for (int i = posicao; i < qtdItens - 1; i++)
            {
                itens[i] = itens[i + 1];
            }

            itens[qtdItens - 1] = null;
            qtdItens--;
            return true;
        }

        public double CalcularTotal()
        {
            double total = 0.0;
            for (int i = 0; i < qtdItens; i++)
            {
                if (itens[i] != null)
                {
                    total += itens[i].Preco;
                }
            }
            return total;
        }

        public string DadosDoPedido()
        {
            string resumo = $"---------------------------------------------\n";
            resumo += $"Pedido ID: {id}\n";
            resumo += $"Cliente:   {cliente}\n";
            resumo += $"Itens:\n";

            if (qtdItens == 0)
            {
                resumo += "  (Nenhum item cadastrado neste pedido)\n";
            }
            else
            {
                for (int i = 0; i < qtdItens; i++)
                {
                    resumo += $"  [{itens[i].Id}] {itens[i].Descricao} - R$ {itens[i].Preco:F2}\n";
                }
            }

            resumo += $"Valor Total: R$ {CalcularTotal():F2}\n";
            resumo += $"---------------------------------------------";
            return resumo;
        }
    }


    // CLASSE RESTAURANTE

    public class Restaurante
    {
        private int proxPedido;
        private Pedido[] pedidos;
        private int qtdPedidos; // Controle interno da quantidade de pedidos armazenados

        public int ProxPedido
        {
            get { return proxPedido; }
        }

        public Restaurante()
        {
            this.proxPedido = 1;
            this.pedidos = new Pedido[50]; // Máximo de 50 pedidos diários
            this.qtdPedidos = 0;
        }

        public bool NovoPedido(Pedido pedido)
        {
            if (qtdPedidos >= 50)
            {
                return false; // Capacidade máxima de pedidos atingida
            }

            pedidos[qtdPedidos] = pedido;
            qtdPedidos++;
            proxPedido++;
            return true;
        }

        public Pedido BuscarPedido(Pedido pedido)
        {
            for (int i = 0; i < qtdPedidos; i++)
            {
                if (pedidos[i] != null && pedidos[i].Id == pedido.Id)
                {
                    return pedidos[i];
                }
            }
            return null;
        }

        public bool CancelarPedido(Pedido pedido)
        {
            int posicao = -1;

            for (int i = 0; i < qtdPedidos; i++)
            {
                if (pedidos[i] != null && pedidos[i].Id == pedido.Id)
                {
                    posicao = i;
                    break;
                }
            }

            if (posicao == -1)
            {
                return false; // Pedido não encontrado
            }

            // Reorganiza o array após remoção
            for (int i = posicao; i < qtdPedidos - 1; i++)
            {
                pedidos[i] = pedidos[i + 1];
            }

            pedidos[qtdPedidos - 1] = null;
            qtdPedidos--;
            return true;
        }

        public Pedido[] ObterPedidos()
        {
            Pedido[] listaAtual = new Pedido[qtdPedidos];
            for (int i = 0; i < qtdPedidos; i++)
            {
                listaAtual[i] = pedidos[i];
            }
            return listaAtual;
        }
    }


    // PROGRAMA PRINCIPAL (VIEW / CONSOLE INTERFACE)

    class Program
    {
        static void Main(string[] args)
        {
            Restaurante restaurante = new Restaurante();
            int opcao = -1;

            do
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("    IFSP CUBATÃO - SISTEMA DE RESTAURANTE        ");
                Console.WriteLine("==================================================");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Criar novo pedido");
                Console.WriteLine("2. Adicionar item ao pedido");
                Console.WriteLine("3. Remover item do pedido");
                Console.WriteLine("4. Consultar pedido");
                Console.WriteLine("5. Cancelar pedido");
                Console.WriteLine("6. Listar todos os pedidos");
                Console.WriteLine("==================================================");
                Console.Write("Digite a opção desejada: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("\n[ERRO] Por favor, digite um número válido!");
                    Pausar();
                    continue;
                }

                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        OpcaoCriarNovoPedido(restaurante);
                        break;
                    case 2:
                        OpcaoAdicionarItem(restaurante);
                        break;
                    case 3:
                        OpcaoRemoverItem(restaurante);
                        break;
                    case 4:
                        OpcaoConsultarPedido(restaurante);
                        break;
                    case 5:
                        OpcaoCancelarPedido(restaurante);
                        break;
                    case 6:
                        OpcaoListarTodosPedidos(restaurante);
                        break;
                    case 0:
                        Console.WriteLine("Encerrando o sistema... Até mais!");
                        break;
                    default:
                        Console.WriteLine("[ERRO] Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Pausar();
                }

            } while (opcao != 0);
        }

        static void OpcaoCriarNovoPedido(Restaurante restaurante)
        {
            Console.WriteLine("--- 1. CRIAR NOVO PEDIDO ---");
            Console.Write("Nome do Cliente: ");
            string cliente = Console.ReadLine();

            int idPedido = restaurante.ProxPedido;
            Pedido novoPedido = new Pedido(idPedido, cliente);

            if (restaurante.NovoPedido(novoPedido))
            {
                Console.WriteLine($"\n[SUCESSO] Pedido #{idPedido} registrado para o(a) cliente '{cliente}'.");
            }
            else
            {
                Console.WriteLine("\n[ERRO] Capacidade máxima diária de 50 pedidos atingida!");
            }
        }

        static void OpcaoAdicionarItem(Restaurante restaurante)
        {
            Console.WriteLine("--- 2. ADICIONAR ITEM AO PEDIDO ---");
            Console.Write("Informe o ID do pedido: ");
            if (!int.TryParse(Console.ReadLine(), out int idPedido))
            {
                Console.WriteLine("[ERRO] ID do pedido inválido.");
                return;
            }

            Pedido busca = new Pedido(idPedido, "");
            Pedido pedido = restaurante.BuscarPedido(busca);

            if (pedido == null)
            {
                Console.WriteLine($"[ERRO] Pedido #{idPedido} não encontrado!");
                return;
            }

            Console.Write("ID do Item: ");
            if (!int.TryParse(Console.ReadLine(), out int idItem))
            {
                Console.WriteLine("[ERRO] ID do item inválido.");
                return;
            }

            Console.Write("Descrição do Item: ");
            string descricao = Console.ReadLine();

            Console.Write("Preço do Item (R$): ");
            if (!double.TryParse(Console.ReadLine(), out double preco))
            {
                Console.WriteLine("[ERRO] Preço inválido.");
                return;
            }

            Item novoItem = new Item(idItem, descricao, preco);

            if (pedido.AdicionarItem(novoItem))
            {
                Console.WriteLine($"\n[SUCESSO] Item '{descricao}' adicionado ao Pedido #{idPedido}.");
            }
            else
            {
                Console.WriteLine("\n[ERRO] O pedido já atingiu o limite máximo de 10 itens!");
            }
        }

        static void OpcaoRemoverItem(Restaurante restaurante)
        {
            Console.WriteLine("--- 3. REMOVER ITEM DO PEDIDO ---");
            Console.Write("Informe o ID do pedido: ");
            if (!int.TryParse(Console.ReadLine(), out int idPedido))
            {
                Console.WriteLine("[ERRO] ID do pedido inválido.");
                return;
            }

            Pedido busca = new Pedido(idPedido, "");
            Pedido pedido = restaurante.BuscarPedido(busca);

            if (pedido == null)
            {
                Console.WriteLine($"[ERRO] Pedido #{idPedido} não encontrado!");
                return;
            }

            Console.Write("Informe o ID do Item a ser removido: ");
            if (!int.TryParse(Console.ReadLine(), out int idItem))
            {
                Console.WriteLine("[ERRO] ID do item inválido.");
                return;
            }

            Item itemParaRemover = new Item(idItem, "", 0.0);

            if (pedido.RemoverItem(itemParaRemover))
            {
                Console.WriteLine($"\n[SUCESSO] Item ID {idItem} removido com sucesso do Pedido #{idPedido}.");
            }
            else
            {
                Console.WriteLine($"\n[ERRO] Item ID {idItem} não foi encontrado no Pedido #{idPedido}.");
            }
        }

        static void OpcaoConsultarPedido(Restaurante restaurante)
        {
            Console.WriteLine("--- 4. CONSULTAR PEDIDO ---");
            Console.Write("Informe o ID do pedido: ");
            if (!int.TryParse(Console.ReadLine(), out int idPedido))
            {
                Console.WriteLine("[ERRO] ID do pedido inválido.");
                return;
            }

            Pedido busca = new Pedido(idPedido, "");
            Pedido pedido = restaurante.BuscarPedido(busca);

            if (pedido == null)
            {
                Console.WriteLine($"[ERRO] Pedido #{idPedido} não encontrado!");
                return;
            }

            Console.WriteLine("\n" + pedido.DadosDoPedido());
        }

        static void OpcaoCancelarPedido(Restaurante restaurante)
        {
            Console.WriteLine("--- 5. CANCELAR PEDIDO ---");
            Console.Write("Informe o ID do pedido que deseja cancelar: ");
            if (!int.TryParse(Console.ReadLine(), out int idPedido))
            {
                Console.WriteLine("[ERRO] ID do pedido inválido.");
                return;
            }

            Pedido busca = new Pedido(idPedido, "");

            if (restaurante.CancelarPedido(busca))
            {
                Console.WriteLine($"\n[SUCESSO] Pedido #{idPedido} foi cancelado.");
            }
            else
            {
                Console.WriteLine($"\n[ERRO] Pedido #{idPedido} não foi encontrado.");
            }
        }

        static void OpcaoListarTodosPedidos(Restaurante restaurante)
        {
            Console.WriteLine("--- 6. LISTA DE TODOS OS PEDIDOS DO DIA ---");
            Pedido[] lista = restaurante.ObterPedidos();

            if (lista.Length == 0)
            {
                Console.WriteLine("Nenhum pedido foi registrado hoje.");
                return;
            }

            double somaGeralDia = 0.0;

            foreach (Pedido p in lista)
            {
                double totalPedido = p.CalcularTotal();
                somaGeralDia += totalPedido;
                Console.WriteLine($"ID: {p.Id,3} | Cliente: {p.Cliente,-20} | Valor Total: R$ {totalPedido,8:F2}");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"SOMA GERAL DO DIA (FATURAMENTO): R$ {somaGeralDia:F2}");
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}