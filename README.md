# Aplicativo de Gerenciamento de Exercícios Físicos

O objetivo desse aplicativo é disponibilizar uma lista de exercícios físicos gerenciável (CRUD).

## Persistência de Dados

Os dados salvos são armazenados dentro de um arquivo **SQLite** (**.db3**), que simula o funcionamento de bancos de dados, como o MySQL, por exemplo.

## Funcionalidades

Dentre os recursos implementados, estão:

- **Cadastro de Itens**: é possível fornecer dados como descrição, data de prática, peso atual e observações para criar um item.

- **Edição de Itens**: é possível atualizar dados como descrição, data de prática, peso atual e observações para editar um item.

- **Deleção de Itens**: é possível excluir um item específico selecionado. É exigida uma confirmação antes de executar essa ação.

- **Listagem Geral**: é possível visualizar todos os itens salvos no SQLite através de uma listagem geral.

- **Pesquisa de Itens**: é possível pesquisar itens da lista de exercícios com base em parte de sua descrição.

## MVVM (**Model-View-ViewModel**)

Esse projeto segue o padrão MVVM, que visa separar as responsabilidades do aplicativo, com a seguinte estrutura:

- **View**: camada de páginas da interface gráfica.

- **ViewModel**: camada de mapeamento e atualização de dados da interface em tempo real, de acordo com a atualização da ViewModel.

- **Model**: camada que especifica a estrutura a ser seguida pelos objetos instanciados e transportados para a camada de persistência de dados, além de regras de negócio das variáveis.

- **Helper**: camada de persistência de dados, onde os registros serão armazenados no SQLite.
