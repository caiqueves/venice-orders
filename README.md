
# Venice.Orders

Sistema de gerenciamento de pedidos e usuários, com autenticação JWT, usando .NET 8, MediatR, AutoMapper e MongoDB/SQL Server.


# Tecnologias Utilizadas

 * .NET 8
 * C#
 * MediatR (CQRS)
 * AutoMapper
 * FluentValidation
 * SQL Server + MongoDB
 * Docker + Docker Compose
 * JWT Authentication
 * xUnit + Moq + FluentAssertions (testes unitários)
 
 # Design Pattern e Arquitetura
 
 # Arquitetura
 
 ## Camadas:

	* Domain: Entidades, agregados e interfaces de repositório
	*Application: Commands, Queries, Handlers, DTOs, regras de negócio
	*Infrastructure: Implementações de persistência (SQL Server e MongoDB)
	*Api: Controllers, Requests, Validators, AutoMapper Profiles

 ## Comunicação:

   *API → Application via MediatR (CQRS)
   * Handlers manipulam Commands/Queries de forma desacoplada
   
 ## Design Pattern adotado
	
 ### CQRS (Command Query Responsibility Segregation)
	
	* Justificativa:
	
		* Separei operações de escrita (Commands) e leitura (Queries)
		* Facilita escalabilidade e manutenção
		* Handlers isolam a lógica de negócio, melhorando testabilidade e coesão
		
 ### Repository Pattern
	
	* Justificativa:
	
	* Abstrai acesso a dados
	* Permite trocar o banco sem impactar Application/Domain
	* Facilita testes unitários usando mocks

# Como rodar localmente

## Pré-requisitos

	* Docker
	* Docker Compose
	* .NET 8 SDK
	* SQL SERVER LOCAL

## Rodando a aplicação

   * 1 - Alterar a connection do SQL Server no arquivo docker-compose para conseguir o container funcionar
	* Deve abrir o arquivo docker-compose na linha 46
     
	 ```bash
     - ConnectionStrings__Default=<Incluir aqui sua conexao>
	 ```
   * 2 - Rodar Migrations (SQL SERVER)
      
	* 2.1 Abrir o arquivo appsettings.json atualizar a chave ConnectionStrings__Default
		
	  ```bash
	   "ConnectionStrings": {
			"Default": <Incluir aqui sua conexao>
	   }
	  ```
	
	* 2.2 Recomendo abrir o visual studio Console do gerenciador de pacotes
	* 2.4 Selecionar o projeto venice.Orders.SqlServer
	
	 ```bash
	   Update-Database
	 ```
   
   * 3 - Subir Containers
   
	   ```bash
	   docker-compose up -d
	   ```
	   
   * 4 - Acessar Swagger
   
	   ```bash
		http://localhost:5000/swagger
	   ```
 ## Testes Unitarios
 
	```bash
	   cd test/Venice.Orders.Tests
	   dotnet test
	```
	
	* Testes incluem autenticação e criação de usuário
    * Cobrem cenários positivos e negativos
	
