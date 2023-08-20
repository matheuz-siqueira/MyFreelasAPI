# MyFreelas

<details open="open">
    <summary>Conteúdo</summary>
    <ol>
        <li>
            <a href="#sobre-o-projeto">Sobre o projeto</a>
            <ul>
                <li><a href="#built-with">Built With</a>
                <li><a href="#features">Feautures</a>
            </ul>
        </li>
        <li>
            <a href="#getting-started">Getting Started</a>
            <ul>
                <li><a href="#requisitos">Requisitos</a>
                <li><a href="#instalação">Instalação</a>
            </ul>
        </li>
        <li><a href="#licença">Licença</a>
    </ol>
</details>

### **Sobre o projeto**

Este é um projeto de uma API desenvolvida em .NET CORE 6 utilizando a abordagem de desenvolvimento em camadas, basicamente: **controllers, serviços e modelos.**

Desenvolvi este projeto para aprimorar meus conhecimentos com o desenvolvimento de API com .NET Core, bem como utilização de **design patterns** e boas práticas de desenvolvimento de software.

É um projeto relativamente simple, o objetivo é que ao se cadastrar o usuário consiga gerenciar informações a respeito de projetos freelas e de seus clientes.

Para isso a API fornece endpoints para o usuário (cadastro, recuperação de perfil, alteração de senha) e fornece também serviço de autenticação utilizando **token JWT** para que o usuário consiga acessar os recursos da API.

Ao logar no sistema, o usuário consegue realizar operações CRUD para **clientes e projetos** e ter acesso ao um **dashboard** com informações do total de clientes e projetos cadastrados, recorrências e etc.

A feature mais interessante da API é que ela fornece uma **previsão mensal de faturamento** com base no total de parcelas que você aceitou dividir o valor do projeto.

#### **Built With**

![ubuntu-shield]
![net-core]
![csharp-shield]
![swagger-shield]
![postman-shield]
![mysql-shield]
![vscode-shield]
![git-shield]

#### features

- Registrar usuário;
- Cadastrar cliente;
- Cadastrar projeto;
- Filtrar pesquisa de clientes e projetos;
- Dashboard;
- Verificar previsão mensal de faturamento;

E outras.

### Getting Started

#### Requisitos

- Visual Studio Code

- MySql

#### Instalação

1. Clone o repositório
   ```sh
   git clone https://github.com/matheuz-siqueira/myfreelas.git
   ```
2. Preenche as informações no arquivo `appsettings.Development.json`

3. Execute a API

   ```sh
   dotnet watch run
   ```

   ou

   ```sh
   dotnet run
   ```

4. Ótimos testes :)

### Licença

Fique a vontade para estudar e aprender com este projeto. Você não tem permissão para utiliza-lo para distribuição ou comercialização.

<!-- Badges -->

[ubuntu-shield]: https://img.shields.io/badge/Ubuntu-E95420?style=for-the-badge&logo=ubuntu&logoColor=white
[swagger-shield]: https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white
[net-core]: https://img.shields.io/badge/.NET_%20_Core_6.0-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[postman-shield]: https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white
[csharp-shield]: https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white
[mysql-shield]: https://img.shields.io/badge/mysql-%2300f.svg?style=for-the-badge&logo=mysql&logoColor=white
[vscode-shield]: https://img.shields.io/badge/Visual%20Studio%20Code-0078d7.svg?style=for-the-badge&logo=visual-studio-code&logoColor=white
[git-shield]: https://img.shields.io/badge/git-%23F05033.svg?style=for-the-badge&logo=git&logoColor=white
