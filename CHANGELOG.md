# 📋 CHANGELOG

Todas as alterações relevantes deste projeto serão documentadas aqui.  
Este arquivo segue o formato de versionamento semântico ([semver](https://semver.org/lang/pt-BR/)).

---

## [v1.0.0] - 2025-04-08
### 🚀 Primeira versão estável
- Cadastro de livros com acervo (físico e digital)
- Cadastro e gerenciamento de usuários
- Persistência em arquivos JSON com Jackson
- Interface via terminal com menu interativo
- Testes automatizados com JUnit 5
- Relatórios de cobertura com JaCoCo
- Documentação JavaDoc gerada via Maven
- Esteira CI com GitHub Actions
- Estrutura de desenvolvimento com Git Flow
- Projeto acadêmico da PUCPR (Programação Orientada a Objetos)

---

## [1.1.1-SNAPSHOT] - Em desenvolvimento
### Adicionado
- Branch `feature/fila-e-pilha` criada para implementação da atividade formativa.
- Implementação de **fila (Queue)** para lista de espera de livros.
- Implementação de **pilha (Stack)** para histórico de navegação de usuários.
- Menu da interface expandido para suportar visualização de histórico e espera.
- Testes automatizados para filas e pilhas.
- Documentação atualizada (`README.md`, `pom.xml`, `CONTRIBUTING.md`, `CHANGELOG.md`).
- SCM e `developers` configurados no `pom.xml`.

### Alterado
- Versão `pom.xml` para `1.1.1-SNAPSHOT`.
- README com seção da atividade formativa da PUCPR.

---

## [1.2.0] - 2025-04-09

### ✨ Adicionado

- Nova branch `feature/grafo-recomendacao` com entrega final da atividade somativa.
- Implementação de **grafo de recomendações entre livros**.
- Extensão do sistema de navegação com histórico por usuário (padrão Stack).
- Módulo de **testes automatizados** por recurso:
    - `RecommendationFeatureTest.java`
    - `UserHistoryFeatureTest.java`
    - `WaitlistFeatureTest.java`
- Criação de **SuiteTest.java** para execução agrupada dos testes.
- Criação de arquivos JSON separados para cenários de teste:
    - `test-books-recommendation.json`, `test-users-history.json`, etc.
- Atualização do `Main.java` para suportar visualização e adição de recomendações.
- Documentação complementar de testes (`README_TESTS.md`).

### 🛠️ Alterado

- `LibraryFileManager` agora persiste o grafo de recomendações no `books.json`.
- Ajustes no menu interativo para integrar com o grafo e histórico.
- Modularização e refatoração de testes (`MainMenuTest` dividido em arquivos menores).
- Atualização de testes existentes com maior robustez.

### ✅ Corrigido

- Correções na persistência do grafo de recomendações que não estava sendo salva entre execuções.
- Correções menores em nomes de variáveis e arquivos de teste.
