package com.vinot.somativa1.manager;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.vinot.somativa1.model.Book;
import org.junit.jupiter.api.Test;

import java.io.InputStream;
import java.util.LinkedList;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;

public class BookJsonTest {

    private final ObjectMapper mapper = new ObjectMapper();

    @Test
    public void testDeserializeBooks() throws Exception {
        // Carrega arquivo JSON do classpath
        InputStream inputStream = getClass().getClassLoader().getResourceAsStream("test-books.json");
        assertNotNull(inputStream, "Arquivo test-books.json não encontrado em src/test/resources!");

        LinkedList<Book> books = mapper.readValue(inputStream, new TypeReference<>() {});

        assertEquals(2, books.size());

        Book b1 = books.get(0);
        Book b2 = books.get(1);

        assertEquals("Teste 1", b1.getTitle());
        assertEquals("Autor A", b1.getAuthor());
        assertEquals(2025, b1.getYear());

        assertEquals("1984", b2.getTitle());
        assertEquals("George Orwell", b2.getAuthor());
        assertEquals(1949, b2.getYear());
    }
}
