# PUCPR-DevOps

**Disciplina:** DevOps – PUCPR  
**Atividade em andamento:** Atividade Somativa 2  

Este repositório contém o projeto desenvolvido ao longo da disciplina de **DevOps** da PUCPR.  
Ele foi iniciado na Atividade Somativa 1 e segue em evolução, incorporando práticas de integração contínua, testes automatizados e uso do GitHub Actions.

---

## 🎯 Objetivo

- Consolidar práticas de **integração contínua (CI)** e **entrega contínua (CD)**.
- Implementar **testes unitários** e executá-los em cada Pull Request (PR).
- Configurar **alertas via GitHub Actions**.
- Documentar a evolução das atividades por meio de screenshots (prints).

---

## 📚 Escopo da Atividade Somativa 2

De acordo com a proposta, esta entrega deve contemplar:

1. **Testes unitários**
   - Escrita de pelo menos **cinco testes unitários**.
   - Execução automática dos testes em cada PR criada no repositório.
   - Uso de **GitHub Actions** para orquestrar o workflow.

2. **Comprovação (prints/screenshots)**
   - URL e conteúdo do repositório (caso a Atividade Somativa 1 não tenha sido entregue).
   - Arquivo de workflow configurado para envio de alertas.
   - Prints de alertas recebidos do GitHub Actions.
   - Prints mostrando o código dos testes unitários criados.
   - Prints da execução dos testes dentro de uma PR.

---
# 📚 Projeto Somativa – Biblioteca Virtual (PUCPR‑OED)

![Build](https://github.com/vbarbosa/PUCPR-OED/actions/workflows/maven.yml/badge.svg)
![Cobertura de Testes](https://img.shields.io/badge/cobertura-92%25-brightgreen)

> **Versão atual:** **`v1.3.0`** –  (Dijkstra & Recomendações)

---
---

## ✨ Funcionalidades

| Categoria     | Descrição                                                                                                        |
| ------------- | ---------------------------------------------------------------------------------------------------------------- |
| Cadastro      | Livros (acervo físico + digital) e Usuários                                                                      |
| Estruturas    | Fila de espera (`Queue`), Histórico de navegação (`Stack`)                                                       |
| Recomendações | Grafo `HashMap<Book, Set<Book>>` + **algoritmo Dijkstra** não‑ponderado para sugerir livros pela menor distância |
| Persistência  | Arquivos JSON (`books.json`, `users.json`) via Jackson                                                           |
| Interface     | Menu interativo em terminal (opções 1‑23)                                                                        |
| Qualidade     | Testes JUnit 5, cobertura JaCoCo 92 %, CI GitHub Actions                                                         |
| Docs          | JavaDoc gerada com Maven e publicada no GitHub Pages                                                             |

---

## 🗂️ Estrutura de Pastas

```text
Somativa1/
├── pom.xml
├── src/
│   ├── main/
│   │   ├── java/com/vinot/somativa1/
│   │   │   ├── application/   # Main
│   │   │   ├── controller/    # Library, fila, histórico
│   │   │   ├── algorithm/     # GraphAlgorithms (Dijkstra)
│   │   │   ├── manager/       # JSON persistence
│   │   │   └── model/         # Book, User, InventoryItem
│   │   └── resources/         # books.json, users.json
│   └── test/
│       ├── java/com/vinot/somativa1/   # JUnit tests
│       └── resources/         # JSON de teste
└── target/                    # Build, relatórios, JavaDoc
```

---

## 🚀 Como Executar

### Pré‑requisitos

* **Java 23** (Temurin ou OpenJDK)
* **Maven 3.9+**

### Build

```bash
mvn clean package   # compila, roda testes e gera JAR
```

### Rodar aplicação

```bash
java -cp target/Somativa1-1.3.0.jar \
     com.vinot.somativa1.application.Main
```

---

## ✅ Testes & Cobertura

```bash
mvn test         # executa testes JUnit 5
mvn verify       # gera relatório JaCoCo
```

Relatório: `target/site/jacoco/index.html`

---

## 📘 Documentação JavaDoc

* Online: [https://vbarbosa.github.io/PUCPR-OED/](https://vbarbosa.github.io/PUCPR-OED/)
* Local: `target/site/apidocs/index.html`
* JAR: `target/Somativa1-1.3.0-javadoc.jar`

Gerar manualmente:

```bash
mvn javadoc:javadoc      # HTML
mvn javadoc:jar          # JAR
```

---

## 💼 Git Flow

| Branch      | Propósito           |
| ----------- | ------------------- |
| `main`      | produção estável    |
| `develop`   | integração contínua |
| `feature/*` | novas features      |
| `hotfix/*`  | correções urgentes  |

Exemplo:

```bash
git checkout -b feature/minha-feature
git add .
git commit -m "feat: minha feature"
git push -u origin feature/minha-feature
```

Abra PR → `develop` → revisão → merge.

---

## 🤖 GitHub Actions

Workflow principal (`.github/workflows/maven.yml`) executa:

1. Checkout
2. Setup Java 23
3. `mvn clean verify`
4. Publica artefatos (JAR, sources, javadoc) e JavaDoc em `gh-pages`.

---

## 📦 Releases

As versões estão em [Releases](https://github.com/vbarbosa/PUCPR-OED/releases). Cada release traz:

* `Somativa1-<versão>.jar` – binário executável
* `*-sources.jar` – código‑fonte
* `*-javadoc.jar` – documentação offline
* `Somativa2-Fontes.zip` – **somente** arquivos `.java` (para avaliação PUCPR)

---

## ✅ Checklist de Contribuição

* [ ] Build verde (CI)
* [ ] Testes passam (JUnit)
* [ ] Cobertura ≥ 90 % se possível
* [ ] Sem arquivos de build (`target/`, `.class`)
* [ ] Documentação atualizada

---
## 📝 Histórico de Atividades – Somativa 2 (22-09-25)

- Ajustado workflow de **notificações (notify-teams.yml)** para exibir corretamente o nome do job nas PRs.
- Criada e validada a branch **feature/test-ci-trigger** para testes de CI em Pull Requests.
- Executados testes unitários (`MainMenuTest`) com simulação de entrada/saída de console e uso de arquivos JSON temporários.
- Corrigido README para melhor estrutura e clareza das seções.
- Garantido que a pipeline no **GitHub Actions** executa:
  - Build com Maven
  - Testes JUnit 5
  - Relatório de cobertura JaCoCo
  - Notificações de PR no Discord/Teams

# ------------------------------------------------------
# ## 🧩 Criação da Branch `develop`
#
# Durante a evolução da **Somativa 2**, configuramos o fluxo Git Flow no repositório.  
# A branch `develop` foi criada a partir da `main` para centralizar as integrações contínuas de novas features antes de chegar à produção.
#
# ### 📌 Comandos utilizados
#
# ```bash
# git checkout -b develop main
# git push -u origin develop
# ```
#
# ### 📈 Papel da `develop` no fluxo
#
# - `feature/*` → desenvolvimento de novas funcionalidades.  
# - `develop` → integra todas as features antes da entrega.  
# - `main` → recebe apenas versões estáveis (releases).  
#
# Esse fluxo garante maior organização e qualidade no ciclo de vida do projeto.
# ------------------------------------------------------

## 📞 Contato


Vinícius Barbosa • Projeto acadêmico PUCPR – 2025