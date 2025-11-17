
# Dupla: Rebecca Beccari e João Bender


# API de Auditoria e Controle Interno

Sistema de API RESTful para controle interno organizacional, desenvolvido em C# com .NET 7 e Entity Framework Core.

## Funcionalidades

- **Políticas**: Gerenciamento de políticas de controle interno
- **Permissões**: Controle de permissões de acesso
- **Logs de Acesso**: Registro de atividades dos usuários
- **Trilhas de Auditoria**: Rastreamento de mudanças no sistema

## Tecnologias Utilizadas

- .NET 7.0
- Entity Framework Core
- SQLite
- Minimal APIs (ASP.NET Core)

## Estrutura do Projeto

```
Trabalho-API/
├── Rotas/              # Rotas organizadas por tipo de operação HTTP
│   ├── GetRoutes.cs     # Todas as rotas GET
│   ├── PostRoutes.cs    # Todas as rotas POST
│   └── DeleteRoutes.cs  # Todas as rotas DELETE
├── Models/              # Modelos de dados
│   ├── Politica.cs
│   ├── Permissao.cs
│   ├── LogAcesso.cs
│   └── TrilhaAuditoria.cs
├── Data/                # DbContext e configurações
│   ├── ControleInternoContext.cs
│   └── SeedData.cs
├── wwwroot/                # Frontend
│   ├── index.html # Integração da API com FrontEnd, com a interface grafica do front com HTML e CSS
├── Services/              
│   ├── AuditoriaService.cs # Adiciona automaticamente nas trilhas de auditoria qualquer operação do usuario  
├── Program.cs           # Configuração da aplicação
└── Auditoria.csproj    # Arquivo de projeto
```

## Organização das Rotas

As rotas estão organizadas por tipo de operação HTTP em arquivos separados:
- **GetRoutes.cs**: Contém todas as rotas GET (listar e buscar por ID)
- **PostRoutes.cs**: Contém todas as rotas POST (criar novos registros)
- **DeleteRoutes.cs**: Contém todas as rotas DELETE (excluir registros)

## Como Executar
1. **Executar o projeto**:
   ```bash
   dotnet restore
   dotnet run --project Auditoria.csproj
   ```

2. **Acessar a API**:
   - API Base: `http://localhost:5000/api` ou `https://localhost:5001/api`
   - As rotas estão disponíveis conforme descrito na seção "Endpoints Disponíveis"

## Endpoints Disponíveis

### Políticas
- `GET /api/politicas` - Listar todas as políticas
- `GET /api/politicas/{id}` - Buscar política por ID
- `POST /api/politicas` - Criar nova política
- `DELETE /api/politicas/{id}` - Excluir política

### Permissões
- `GET /api/permissoes` - Listar todas as permissões
- `GET /api/permissoes/{id}` - Buscar permissão por ID
- `POST /api/permissoes` - Criar nova permissão
- `DELETE /api/permissoes/{id}` - Excluir permissão

### Logs de Acesso
- `GET /api/logsacesso` - Listar todos os logs (ordenados por data/hora decrescente)
- `GET /api/logsacesso/{id}` - Buscar log por ID
- `POST /api/logsacesso` - Criar novo log
- `DELETE /api/logsacesso/{id}` - Excluir log

### Trilhas de Auditoria
- `GET /api/trilhasauditoria` - Listar todas as trilhas (ordenadas por data/hora decrescente)
- `GET /api/trilhasauditoria/{id}` - Buscar trilha por ID
- `POST /api/trilhasauditoria` - Criar nova trilha
- `DELETE /api/trilhasauditoria/{id}` - Excluir trilha

### Estatísticas
- `GET /api/estatisticas` - Listar todas estatisticas geradas dos demais endpoints

## Dados Iniciais

O sistema é automaticamente populado com dados de exemplo na primeira execução:
- 10 políticas de controle interno
- 10 tipos de permissões
- 50 logs de acesso
- 50 trilhas de auditoria

**Total**: 120 registros iniciais

## Banco de Dados

- **Tipo**: SQLite
- **Arquivo**: `ControleInterno.db`
- **Criação**: Automática na primeira execução via `EnsureCreated()`
- **Dados**: Populados automaticamente com dados de exemplo através de `SeedData.Initialize()`

## Exemplos de Uso

### Criar uma nova política
```bash
POST /api/politicas
Content-Type: application/json

{
  "nome": "Política de Backup",
  "descricao": "Define procedimentos para backup de dados",
  "ativa": true
}
```

### Buscar todas as políticas
```bash
GET /api/politicas
```

### Buscar política por ID
```bash
GET /api/politicas/1
```

### Criar novo log de acesso
```bash
POST /api/logsacesso
Content-Type: application/json

{
  "usuario": "admin",
  "acao": "Login",
  "ipOrigem": "192.168.1.100"
}
```

### Criar trilha de auditoria
```bash
POST /api/trilhasauditoria
Content-Type: application/json

{
  "entidadeAfetada": "Usuario",
  "tipoOperacao": "CREATE",
  "usuario": "admin",
  "dadosNovos": "{\"nome\": \"novo_usuario\"}"
}
```

### Excluir registro
```bash
DELETE /api/politicas/1
```

## Respostas da API

A API retorna os seguintes códigos HTTP:
- `200 OK`: Operação bem-sucedida (GET)
- `201 Created`: Registro criado com sucesso (POST)
- `204 No Content`: Registro excluído com sucesso (DELETE)
- `400 Bad Request`: Dados inválidos
- `404 Not Found`: Registro não encontrado
- `500 Internal Server Error`: Erro interno do servidor

Sistema desenvolvido como trabalho acadêmico para demonstração de conceitos de API RESTful, Entity Framework Core, Minimal APIs e integração com banco de dados SQLite.
