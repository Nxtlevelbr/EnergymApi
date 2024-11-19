# EnergymApi

## Equipe de Desenvolvimento

- **RM99841** - Marcel Prado Soddano  
- **RM551261** - Giovanni Sguizzardi  
- **RM98057** - Nicolas E. Inohue  
- **RM552302** - Samara Moreira  
- **RM552293** - Vinicius Monteiro  

---

## 🔥 Sobre o Projeto

**EnergymApi** é uma API RESTful desenvolvida em **ASP.NET Core**, com o objetivo de promover a prática de exercícios físicos enquanto 
transforma academias em hubs de geração de energia sustentável. Por meio de um sistema gamificado, os usuários acumulam pontos ao realizar
exercícios, que podem ser trocados por prêmios.

### ✨ Funcionalidades Principais:
- **Gestão de Usuários:** Cadastro e gerenciamento completo.
- **Sistema de Pontuação:** Acompanhe e acumule pontos baseados nos exercícios realizados.
- **Recomendações de Prêmios Personalizadas:** Utilize Machine Learning para oferecer prêmios com base no desempenho.
- **Gestão de Academias e Endereços:** Organização centralizada para múltiplas unidades.
- **Registros de Exercícios:** Histórico completo do desempenho dos usuários.

---

## 🏗️ Arquitetura do Projeto

A arquitetura escolhida para o **EnergymApi** é **monolítica em camadas**, garantindo simplicidade e manutenibilidade, enquanto organiza o 
código de forma modular.

### 🔍 Estrutura das Camadas:
1. **Controllers**  
   Intermediários entre a interface do usuário e a lógica de negócios. São responsáveis por receber requisições, chamar os serviços e retornar
    respostas apropriadas.

3. **Services**  
   Implementam a lógica de negócios do sistema. Esta camada realiza as regras do negócio e orquestra chamadas aos repositórios.

4. **Repositories**  
   Gerenciam o acesso ao banco de dados, garantindo que a lógica de persistência seja isolada e reutilizável.

5. **Models**  
   Representam as entidades do sistema e seu mapeamento para o banco de dados.

6. **DTOs (Data Transfer Objects)**  
   Facilitam a transferência de dados entre o cliente e a API, expondo apenas o necessário.

---

## ⚙️ Padrões de Projeto Utilizados

Para garantir flexibilidade, escalabilidade e facilidade de manutenção, aplicamos os seguintes padrões de projeto:

- **Injeção de Dependência (Dependency Injection):** Promove o desacoplamento entre as camadas.
- **Repository Pattern:** Encapsula a lógica de acesso a dados, centralizando operações no banco.
- **Factory Pattern:** Facilita a criação de objetos complexos, como o contexto do banco de dados.
- **Singleton:** Garante que instâncias críticas, como configurações, tenham um único ponto de criação.

---

## 📊 Machine Learning

O **EnergymApi** utiliza **ML.NET** para fornecer recomendações personalizadas de prêmios. O modelo de Machine Learning é
treinado com base nos pontos acumulados pelos usuários.

### Fluxo de Treinamento do Modelo:
1. **Carregamento de Dados:** A API lê um conjunto de dados inicial (`premios_dataset.csv`).
2. **Pipeline de Treinamento:** Utiliza **Matrix Factorization** para criar um modelo de recomendação.
3. **Avaliação do Modelo:** Métricas como **RMSE** e **MAE** são analisadas para validar a eficiência.
4. **Persistência do Modelo:** O modelo treinado é salvo para uso em previsões futuras.

---

## 📄 Documentação Interativa

### Swagger/OpenAPI
A API conta com documentação interativa por meio do **Swagger**, permitindo a exploração de endpoints diretamente no navegador.

- **Acesse:** `http://localhost:{porta}/swagger`

---

## 🚀 Instruções de Instalação e Execução

### Pré-requisitos:
- **.NET SDK 8.0 ou superior**  
  Verifique com o comando:  
  
  dotnet --version

## Passos para Configuração:

### Clone o Repositório:


git clone https://github.com/Nxtlevelbr/EnergymApi.git
cd EnergymApi/EnergymApi

dotnet restore
dotnet build
dotnet run

## 🔄 Fluxo de Inserção de Dados

Para garantir a consistência e o correto funcionamento do sistema, siga esta ordem ao inserir os dados:

### Usuários:
- **Endpoint:** `POST /api/usuarios`
- Insira usuários com seus dados básicos.

### Prêmios:
- **Endpoint:** `POST /api/premios`
- Cadastra os prêmios disponíveis para resgate.

### Resgates:
- **Endpoint:** `POST /api/resgates`
- Registra os resgates realizados pelos usuários.

### Endereços:
- **Endpoint:** `POST /api/enderecos`
- Adiciona endereços relacionados a academias ou usuários.

### Academias:
- **Endpoint:** `POST /api/academias`
- Administra academias e associa locais aos usuários.

### Registros de Exercícios:
- **Endpoint:** `POST /api/registrosexercicios`
- Monitora e registra o desempenho dos usuários.

---

## 🔑 Principais Endpoints

### Usuários:
- **GET /api/usuarios**: Lista todos os usuários.
- **POST /api/usuarios**: Adiciona um novo usuário.

### Prêmios:
- **GET /api/premios**: Lista prêmios disponíveis.
- **POST /api/premios**: Cadastra um novo prêmio.

### Resgates:
- **GET /api/resgates**: Lista todos os resgates.
- **POST /api/resgates**: Registra um novo resgate.

### Exercícios:
- **GET /api/registrosexercicios**: Lista todos os registros de exercícios.
- **POST /api/registrosexercicios**: Adiciona um novo registro.

---

## 🔍 Métricas e Indicadores

- **Engajamento dos Usuários:** Análise do desempenho físico e dos pontos acumulados.
- **Eficiência do Modelo de Recomendação:** Avaliação contínua para garantir sugestões relevantes.

---

## 🌟 Conclusão

EnergymApi é uma solução moderna que une saúde e sustentabilidade, oferecendo um ambiente digital que incentiva o bem-estar físico e ambiental. 
Com uma arquitetura sólida, aplicação de padrões de projeto e integração de Machine Learning, a API é altamente escalável e preparada para o crescimento.

---

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **ML.NET**
- **Swagger/OpenAPI**
- **Oracle Database**



