# fiap-fcg-payment-api

**Microsserviço de Pagamentos** do ecossistema FIAP Cloud Games. Responsável pelo processamento, validação e persistência de pagamentos realizados pelos usuários, esse serviço opera de forma isolada e integrada aos demais microsserviços por meio de APIs REST e processamento assíncrono via Azure Service Bus.

## TechChallenge CloudGames

API REST independente e escalável, desenvolvida como parte do Tech Challenge da FIAP. Esse microsserviço foi projetado para concentrar toda a **lógica de pagamentos**, incluindo método de pagamento, bandeira do cartão e definição do status (Aprovado, Recusado ou Pendente), seguindo princípios de Clean Architecture e arquitetura orientada a eventos.

## Sobre o Projeto

O objetivo é fornecer um microsserviço **coeso, resiliente e desacoplado** para o domínio de pagamentos da plataforma FIAP Cloud Games.  
A API de Payments **não conhece regras de jogos ou compras**, apenas recebe solicitações de pagamento e decide o status do pagamento com base nas regras de negócio internas.

Esse serviço pode ser implantado e escalado de forma totalmente independente dentro da arquitetura distribuída.

### Arquitetura

O microsserviço segue os princípios da **Clean Architecture**, promovendo separação de responsabilidades, testabilidade e baixo acoplamento:

- `WebApi`: camada HTTP com endpoints REST, validações e Swagger.
- `Application`: casos de uso, handlers MediatR e orquestração de regras.
- `Domain`: regras de negócio de pagamento, entidades e enums.
- `Infrastructure`: persistência com EF Core e integrações externas.

### Pré-requisitos

| Requisito        | Link |
| ---------------- | ---- |
| `.NET SDK 8.0`   | https://dotnet.microsoft.com/en-us/download |
| `PostgreSQL 14+` | https://www.postgresql.org/download/ |
| `Docker`         | https://www.docker.com/products/docker-desktop/ |
| `Docker Compose` | https://docs.docker.com/compose/install/ |

## Execute localmente

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/fiap-fcg-payment-api.git
cd fiap-fcg-payment-api
```

### 2. Suba o banco de dados com Docker Compose

```bash
docker-compose up -d
```

### 3. Configure as variáveis de ambiente

```json
"PAYMENT_CONNECTION_STRING": "Host=localhost;Port=5434;Database=fcg_payments_db;Username=postgres;Password=postgres",
"SERVICEBUS_CONNECTION": "Endpoint=sb://fiap-games-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=..."
```

### 4. Aplique as migrations

```bash
dotnet ef database update   --project src/Fiap.FCG.Payment.Infrastructure   --startup-project src/Fiap.FCG.Payment.WebApi
```

### 5. Execute a aplicação

```bash
dotnet run --project src/Fiap.FCG.Payment.WebApi
```

Acesse:
https://localhost:7267/swagger

## Tecnologias Utilizadas

| Tecnologia | Link |
| --------- | ---- |
| .NET 8 | https://learn.microsoft.com/en-us/dotnet/ |
| Entity Framework Core | https://learn.microsoft.com/en-us/ef/core/ |
| PostgreSQL | https://www.postgresql.org/docs/ |
| Swashbuckle (Swagger) | https://github.com/domaindrivendev/Swashbuckle.AspNetCore |
| MediatR | https://github.com/jbogard/MediatR |
| Azure Service Bus | https://learn.microsoft.com/en-us/azure/service-bus-messaging/ |

## Funcionalidades

### Pagamentos
- Criação de pagamento
- Processamento automático
- Definição de status: Pendente, Aprovado ou Recusado
- Suporte simples a Pix e Cartão de Crédito
- Validação simples da bandeira de cartão

## Integrações

### Azure Service Bus
- Processamento assíncrono de pagamentos

### Microsserviço de Games
- Comunicação indireta via eventos
- Total desacoplamento entre domínios
