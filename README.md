# App.Aluno

## Sobre o Projeto
Esta é uma aplicação desenvolvida para gerenciar alunos, turmas e suas relações. O sistema inclui funcionalidades como cadastro, edição, listagem e inativação de alunos e turmas, bem como a associação entre eles.

A aplicação foi implementada utilizando uma arquitetura moderna com separação de responsabilidades entre as camadas de **Domain**, **Infra**, **Application** e **Web**. O front-end foi desenvolvido com **Razor Pages** enquanto o back-end utiliza **ASP.NET Core Web API**.

## Configuração da Aplicação

### ConnectionString
A ConnectionString utilizada para o banco de dados é:
```json
"DefaultConnection": "Server=(localDb)\\MSSQLLocalDB;Database=App.Aluno;Trusted_Connection=True;MultipleActiveResultSets=true"
```
Certifique-se de que o banco de dados **MSSQLLocalDB** esteja configurado corretamente no ambiente local.

### Configurando o Banco de Dados
1. Navegue até o projeto **Fiap.App.Aluno.Infra** no **Package Manager Console**.
2. Execute o seguinte comando para aplicar as migrações ao banco de dados:
   ```powershell
   Update-Database
   ```
3. Verifique se o banco de dados **App.Aluno** foi criado e configurado corretamente no **SQL Server Management Studio** ou na ferramenta de sua preferência.

### Executando o Projeto
1. Certifique-se de configurar os projetos principais como **Startup Projects**:
   - **Fiap.App.Aluno.WebApi**
   - **Fiap.App.Aluno.Web.Interface**

2. Para configurar no **Visual Studio**:
   - Clique com o botão direito na solução e selecione **Properties**.
   - Em **Startup Projects**, selecione a opção **Multiple startup projects**.
   - Configure ambos os projetos (**Fiap.App.Aluno.WebApi** e **Fiap.App.Aluno.Web.Interface**) como **Start**.

3. Execute a solução pressionando **F5** ou clicando em **Start**.

### Endpoints Disponíveis
- **API:** Acesse os endpoints da Web API através do Swagger disponível em:
  - https://localhost:44308/swagger/index.html
- **Interface Web:** Acesse o front-end da aplicação em:
  - https://localhost:7062

## Principais Funcionalidades

### Alunos
- Cadastro e edição de alunos com validação de dados.
- Listagem e ações de inativação.

### Turmas
- Gerenciamento de turmas com validação de nomes exclusivos.
- Listagem e vinculação de alunos.

### Aluno-Turma
- Associação de alunos e turmas com prevenção de duplicatas.
- Inativação e edição das relações existentes.

## Tecnologias Utilizadas
- **ASP.NET Core**
- **Entity Framework Core**
- **Dapper**
- **Razor Pages**
- **SQL Server (LocalDB)**
- **FluentValidation**
- **XUnit** para testes

## Observações
Certifique-se de seguir as etapas descritas para configurar corretamente o ambiente local. Caso encontre erros, revise as dependências e o arquivo de configuração de startup da aplicação.

