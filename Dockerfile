# Etapa de build com Maven + JDK
FROM maven:3.9.9-eclipse-temurin-23 AS build
WORKDIR /app

# Copia pom.xml e baixa dependências (cache)
COPY pom.xml .
RUN mvn dependency:go-offline

# Copia o restante e empacota (gera uber-jar)
COPY . .
RUN mvn clean package -DskipTests

# Etapa final: apenas o uber-jar
FROM openjdk:23-jdk-slim
WORKDIR /app
COPY --from=build /app/target/*-jar-with-dependencies.jar app.jar

# Executa diretamente com -jar (manifest e libs já incluídos)
CMD ["java", "-jar", "app.jar"]
