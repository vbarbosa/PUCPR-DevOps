# 📚 Projeto Somativa1 – Biblioteca Virtual (PUCPR‑OED)

![Build](https://github.com/vbarbosa/PUCPR-OED/actions/workflows/maven.yml/badge.svg)
![Cobertura de Testes](https://img.shields.io/badge/cobertura-92%25-brightgreen)

> **Versão atual:** **`v1.3.0`** – Somativa 2 (Dijkstra & Recomendações)

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

## 📞 Contato

Vinícius Barbosa • Projeto acadêmico PUCPR – 2025
