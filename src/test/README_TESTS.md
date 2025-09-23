
# ✅ Testes Automatizados - Biblioteca Virtual (Somativa PUCPR)

Este projeto utiliza **JUnit 5** para testes automatizados, seguindo uma estrutura modular com foco em clareza, cobertura e manutenção.

---

## 📁 Estrutura de Diretórios

```
src/
├── main/
│   └── java/
│       └── com/vinot/somativa1/
│           ├── application/       # Código principal da aplicação (Main.java, etc)
│           └── controller/        # Classe Library e controladores centrais
│
├── test/
    ├── java/
    │   └── com/vinot/somativa1/
    │       ├── application/       # Testes específicos por classe
    │       │   ├── MainMenuTest.java
    │       │   ├── BookQueueTest.java
    │       │   ├── WaitlistTest.java
    │       │   ├── UserHistoryTest.java
    │       │   └── RecommendationGraphTest.java
    │       ├── features/          # Testes integrados por funcionalidade
    │       │   ├── WaitlistFeatureTest.java
    │       │   ├── UserHistoryFeatureTest.java
    │       │   └── RecommendationFeatureTest.java
    │       └── SuiteTest.java     # Suite geral de execução
    │
    └── resources/
        ├── test-books-menu.json
        ├── test-users-menu.json
        ├── test-books-queue.json
        ├── test-users-queue.json
        ├── test-books-history.json
        ├── test-users-history.json
        ├── test-books-recommendation.json
        ├── test-users-recommendation.json
        └── ...
```

---

## 🧪 Arquivos de Teste

| Arquivo                          | Finalidade                                    |
|----------------------------------|-----------------------------------------------|
| `MainMenuTest.java`              | Testa o menu interativo principal             |
| `BookQueueTest.java`             | Testa estrutura interna da fila de espera     |
| `WaitlistTest.java`              | Testa a fila no contexto do sistema           |
| `UserHistoryTest.java`           | Testa estrutura interna da pilha de histórico |
| `RecommendationGraphTest.java`   | Testa grafo de recomendações diretamente      |
| `WaitlistFeatureTest.java`       | Testa fila de espera via interface da `Main`  |
| `UserHistoryFeatureTest.java`    | Testa histórico via interface da `Main`       |
| `RecommendationFeatureTest.java` | Testa recomendações via interface da `Main`   |
| `SuiteTest.java`                 | Executa todos os testes simultaneamente       |

---

## ⚙️ Como Executar

Para executar todos os testes de uma vez:

```bash
mvn test
```

Ou para rodar apenas a suíte de testes:

```bash
mvn -Dtest=SuiteTest test
```

---

## 🧪 Cobertura de Testes

O projeto já está integrado com **JaCoCo**. Para gerar o relatório de cobertura:

```bash
mvn clean test jacoco:report
```

O relatório será salvo em:

```
target/site/jacoco/index.html
```

---

## 📌 Notas

- Todos os testes utilizam arquivos `.json` específicos no diretório `src/test/resources`, isolando o ambiente de produção.
- Não há dependência de banco de dados.
- Arquitetura testável com uso de **injeção de dependência indireta** (`LibraryFileManager.overridePaths(...)`).

---


### 🧪 Testado com sucesso para a versão `v1.2.0`
=======
# ✅ Testes Automatizados — Projeto Somativa1

Este projeto utiliza **JUnit 5** para validar as funcionalidades principais da aplicação de biblioteca virtual.

## 📦 Estrutura dos Testes

| Arquivo Java                   | Objetivo                                                       |
|-------------------------------|----------------------------------------------------------------|
| `MainMenuTest.java`           | Testa funcionalidades principais do menu interativo (`Main`)  |
| `BookQueueTest.java`          | Testa a fila de espera por livro (`opções 16 e 17`)            |
| `UserHistoryTest.java`        | Testa o histórico de navegação por usuário (`opção 18`)        |
| `RecommendationGraphTest.java` | Testa o grafo de recomendações (`opções 19 e 20`)             |

---

## ▶️ Executando todos os testes

Use sua IDE (IntelliJ, Eclipse) ou o Maven:

```bash
./mvnw test

