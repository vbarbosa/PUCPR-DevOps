package com.vinot.somativa1.application;

import com.vinot.somativa1.manager.LibraryFileManager;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;

import static org.junit.jupiter.api.Assertions.assertTrue;

import java.nio.file.Path;
import java.util.Objects;

public class MainMenuTest {

    private final InputStream originalIn = System.in;
    private final PrintStream originalOut = System.out;
    private ByteArrayOutputStream output;

    private static final File TEST_BOOK_FILE = new File("src/test/resources/test-books-menu.json");
    private static final File TEST_USER_FILE = new File("src/test/resources/test-users-menu.json");

    @BeforeEach
    void setup() throws IOException {
        // Redireciona os caminhos de arquivo para usar os arquivos de teste
        LibraryFileManager.overridePaths(TEST_BOOK_FILE, TEST_USER_FILE);
        TEST_BOOK_FILE.getParentFile().mkdirs();

        // Preenche os arquivos com um conteúdo JSON inicial válido
        Files.writeString(TEST_BOOK_FILE.toPath(), "[]", StandardCharsets.UTF_8); // Lista vazia de livros
        Files.writeString(TEST_USER_FILE.toPath(), "{}", StandardCharsets.UTF_8); // Mapa vazio de usuários
    }

    @AfterEach
    void restoreIO() {
        // Restaura os streams originais do sistema
        System.setIn(originalIn);
        System.setOut(originalOut);
    }

    /**
     * Simula a entrada do usuário (stdin) e redireciona a saída (stdout) para um buffer.
     *
     * @param input Texto que será injetado como se fosse a digitação do usuário no console.
     */
    private void simulateInput(String input) {
        System.setIn(new ByteArrayInputStream(input.getBytes(StandardCharsets.UTF_8)));
        output = new ByteArrayOutputStream();
        System.setOut(new PrintStream(output));
    }

    @Test
    void testAddBookAndExit() {
        // Simula:
        // 1 -> Adicionar livro
        //    -> "Livro Teste", "Autor", "2000"
        // [ENTER vazio pra seguir]
        // 0 -> Sair
        simulateInput(String.join("\n",
                "1",
                "Livro Teste",
                "Autor",
                "2000",
                "",
                "0"
        ));
        Main.main(null);
        String out = output.toString();
        assertTrue(out.contains("Livro adicionado"), "Saída esperada não encontrada (\"Livro adicionado\").");
    }

    @Test
    void testListBooksAndExit() {
        // Adiciona um livro, depois lista livros, depois sai
        simulateInput(String.join("\n",
                "1",
                "Livro Teste",
                "Autor",
                "2000",
                "",
                "2",
                "",
                "0"
        ));
        Main.main(null);
        String out = output.toString();
        // Verifica se aparece algo que sugira listagem do livro
        assertTrue(out.contains("Livro") || out.contains("title="), "Deveria listar livros na saída.");
    }

    @Test
    void testAddUserAndExit() {
        // 7 -> Adicionar usuário
        //    -> "Usuário Teste", "teste@teste.com", "usuario123"
        // [ENTER vazio]
        // 0 -> Sair
        simulateInput(String.join("\n",
                "7",
                "Usuário Teste",
                "teste@teste.com",
                "usuario123",
                "",
                "0"
        ));
        Main.main(null);
        String out = output.toString();
        assertTrue(out.contains("Usuário") || out.contains("userId"),
                "Usuário não adicionado corretamente (não foi encontrada substring esperada).");
    }

    @Test
    void testListUsersAndExit() {
        // Adiciona usuário e depois lista usuários
        simulateInput(String.join("\n",
                "7",
                "Usuário Teste",
                "teste@teste.com",
                "usuario123",
                "",
                "8",
                "",
                "0"
        ));
        Main.main(null);
        String out = output.toString();
        assertTrue(out.contains("userId") || out.contains("Usuário"),
                "Usuários não foram listados (não foi encontrada substring esperada).");
    }

    @Test
    void testSearchBookAndExit() {
        // Adiciona livro "Livro de Teste" e depois busca por "Teste"
        simulateInput(String.join("\n",
                "1",
                "Livro de Teste",
                "Autor",
                "2000",
                "",
                "3",
                "Teste",
                "",
                "0"
        ));
        Main.main(null);
        String out = output.toString();
        assertTrue(out.contains("Teste") || out.contains("Livro"),
                "Livro de teste não foi encontrado após busca (substring esperada não encontrada).");
    }

    @Test
    void testInventoryViewAndExit() {
        // 6 -> Ver inventário
        // [ENTER]
        // 0 -> Sair
        simulateInput(String.join("\n",
                "6",
                "",
                "0"
        ));
        Main.main(null);
        String out = output.toString();
        // Verifica alguma parte do inventário (pode mostrar "Inventário" ou algo do InventoryItem)
        assertTrue(out.contains("Inventário") || out.contains("ObraHash"),
                "Inventário não exibido corretamente (substring esperada não encontrada).");
    }

    // =============================================
    // TESTES PARA AS NOVAS OPÇÕES (16, 17 e 18)
    // =============================================

    /**
     * Testa a adição de um usuário à fila de espera de um livro (opção 16).
     */
    @Test
    void testAddUserToWaitlistAndExit() {
        /*
         * Fluxo simulado:
         *  7  -> Adicionar usuário
         *     -> "UserFila", "fila@teste.com", "userFila"
         *  [ENTER vazio]
         *  1  -> Adicionar livro ao acervo
         *     -> "Livro Fila", "Autor Fila", "2021"
         *  [ENTER vazio]
         * 16 -> Adicionar usuário à fila de espera
         *     -> "userFila", "Livro Fila"
         *  [ENTER vazio]
         *  0  -> Sair
         */
        simulateInput(String.join("\n",
                "7", "UserFila", "fila@teste.com", "userFila", "",
                "1", "Livro Fila", "Autor Fila", "2021", "",
                "16", "userFila", "Livro Fila", "",
                "0"
        ));
        Main.main(null);

        String out = output.toString();
        // Verifica se houve confirmação de que o usuário foi adicionado à fila
        // O Main mostra: "Usuário userFila adicionado à fila de espera do livro 'Livro Fila'"
        assertTrue(out.contains("adicionado à fila de espera") || out.contains("userFila"),
                "Usuário não foi adicionado à fila de espera corretamente (substring esperada não encontrada).");
    }

    /**
     * Testa a visualização da fila de espera de um livro (opções 16 e 17).
     */
    @Test
    void testViewWaitlistAndExit() {
        /*
         * Fluxo:
         *  7  -> Adicionar usuário
         *     -> "UserFila2", "fila2@teste.com", "userFila2"
         *  1  -> Adicionar livro
         *     -> "Livro Fila2", "Autor Fila2", "2022"
         *  16 -> Adicionar usuário à fila de espera
         *     -> "userFila2", "Livro Fila2"
         *  17 -> Visualizar fila de espera de "Livro Fila2"
         *  0  -> Sair
         */
        simulateInput(String.join("\n",
                "7", "UserFila2", "fila2@teste.com", "userFila2", "",
                "1", "Livro Fila2", "Autor Fila2", "2022", "",
                "16", "userFila2", "Livro Fila2", "",
                "17", "Livro Fila2", "",
                "0"
        ));
        Main.main(null);

        String out = output.toString();
        // "Fila de espera para 'Livro Fila2':" e/ou presença de "UserFila2"
        assertTrue(out.contains("Fila de espera para 'Livro Fila2'") || out.contains("UserFila2"),
                "Fila de espera não exibida corretamente (não foi encontrada substring esperada).");
    }

    /**
     * Testa a exibição do histórico de navegação de um usuário (opção 18).
     * Como o menu não adiciona livros ao histórico de forma explícita, esperamos ver a mensagem de histórico vazio.
     */
    @Test
    void testViewUserHistoryAndExit() {
        /*
         * Fluxo:
         *  7  -> Adicionar usuário
         *     -> "UserHist", "hist@teste.com", "histUser"
         *  18 -> Visualizar histórico
         *     -> "histUser"
         *  0  -> Sair
         */
        simulateInput(String.join("\n",
                "7", "UserHist", "hist@teste.com", "histUser", "",
                "18", "histUser", "",
                "0"
        ));
        Main.main(null);

        String out = output.toString();
        // Neste ponto, nenhum livro foi "visitado" pelo usuário via UserHistoryManager,
        // então a saída deve conter algo como "Não há histórico para este usuário."
        assertTrue(out.contains("Não há histórico para este usuário") || out.contains("histUser"),
                "Histórico não exibido corretamente (mensagem de histórico vazio não encontrada).");
    }
}
