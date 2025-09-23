package com.vinot.somativa1.manager;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.vinot.somativa1.model.User;
import org.junit.jupiter.api.Test;

import java.io.InputStream;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;

public class UserJsonTest {

    private final ObjectMapper objectMapper = new ObjectMapper();

    @Test
    public void testDeserializeUsers() throws Exception {
        // Carrega arquivo JSON do classpath
        InputStream inputStream = getClass().getClassLoader().getResourceAsStream("test-users.json");
        assertNotNull(inputStream, "Arquivo test-users.json não encontrado em src/test/resources!");

        List<User> users = objectMapper.readValue(inputStream, new TypeReference<List<User>>() {});

        assertNotNull(users);
        assertEquals(2, users.size());

        User firstUser = users.get(0);
        assertEquals("Usuário Teste 1", firstUser.getName());
        assertEquals("teste1@email.com", firstUser.getEmail());
        assertEquals("teste1", firstUser.getUserId());
    }

    @Test
    public void testSerializeUsers() throws Exception {
        List<User> users = List.of(
                new User("Usuário Teste 1", "teste1@email.com", "teste1"),
                new User("Usuário Teste 2", "teste2@email.com", "teste2")
        );

        String jsonOutput = objectMapper.writeValueAsString(users);
        List<User> deserializedUsers = objectMapper.readValue(jsonOutput, new TypeReference<List<User>>() {});

        assertNotNull(deserializedUsers);
        assertEquals(2, deserializedUsers.size());
        assertEquals("Usuário Teste 1", deserializedUsers.get(0).getName());
    }
}
