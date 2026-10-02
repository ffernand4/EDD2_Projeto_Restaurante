# Projeto Restaurante — Sistema de Gestão de Pedidos

Atividade prática referente ao **Projeto 03 — Restaurante** da disciplina de **Estrutura de Dados 2 (CBTEDD2)**, do curso de Tecnologia em Análise e Desenvolvimento de Sistemas (TADS) do Instituto Federal de Educação, Ciência e Tecnologia de São Paulo — Campus Cubatão.

---

## Contexto do Problema

Uma cozinha industrial atende diariamente **até 50 pedidos** realizados por clientes, numerados sequencialmente.

- Cada pedido pode conter **no máximo 10 itens**, onde cada item possui um ID, uma descrição e um preço cadastrado no cardápio.
- O sistema deve calcular o valor total de cada pedido e permitir consultar o histórico diário de vendas, bem como o faturamento total acumulado no dia.

---

## Arquitetura e Modelagem

O projeto é uma **Console Application** desenvolvida com base nos paradigmas de **Programação Orientada a Objetos (POO)** e estruturada de acordo com o diagrama de classes fornecido:

```
+---------------------+
| Item                |
+---------------------+
| - id: int           |
| - descricao: string |
| - preco: double     |
+---------------------+

+----------------------------------+
| Pedido                           |
+----------------------------------+
| - id: int                        |
| - cliente: string                |
| - itens: Item[10]                |
+----------------------------------+
| + adicionarItem(Item item): bool |
| + removerItem(Item item): bool   |
| + dadosDoPedido(): string        |
| + calcularTotal(): double        |
+----------------------------------+

+---------------------------------------+
| Restaurante                           |
+---------------------------------------+
| - proxPedido: int                     |
| - pedidos: Pedido[50]                 |
+---------------------------------------+
| + novoPedido(Pedido pedido): bool     |
| + buscarPedido(Pedido pedido): Pedido |
| + cancelarPedido(Pedido pedido): bool |
+---------------------------------------+
```

---

## Funcionalidades do Seletor

| Opção | Funcionalidade | Descrição |
| :---: | :--- | :--- |
| **0** | **Sair** | Encerra a execução do programa. |
| **1** | **Criar novo pedido** | Registra um novo pedido atribuindo o ID sequencial atual (`proxPedido`) e o nome do cliente. |
| **2** | **Adicionar item ao pedido** | Inclui um item (ID, Descrição e Preço) ao pedido especificado (limite máximo de 10 itens). |
| **3** | **Remover item do pedido** | Remove um item específico de um pedido pelo ID do item. |
| **4** | **Consultar pedido** | Exibe detalhes do pedido: ID, Cliente, lista de itens e valor total. |
| **5** | **Cancelar pedido** | Remove um pedido da lista do restaurante. |
| **6** | **Listar todos os pedidos** | Lista todos os pedidos cadastrados, exibindo ID, cliente, valor total e a **soma geral do dia**. |

---

## Como Executar o Projeto

1. Certifique-se de ter o **SDK do .NET Core / .NET 6+** instalado em sua máquina.
2. Clone o repositório ou baixe os arquivos do projeto.
3. No terminal, navegue até a pasta do projeto e execute:
   ```bash
   dotnet run
   ```

---

## Informações Acadêmicas

- **Instituição:** Instituto Federal de Educação, Ciência e Tecnologia de São Paulo (IFSP) — Campus Cubatão
- **Curso:** Tecnologia em Análise e Desenvolvimento de Sistemas (TADS)
- **Disciplina:** Estrutura de Dados 2 (CBTEDD2)
- **Atividade:** 03 — Projeto Restaurante (Semana 5 — 31/08/2026)
