# 🏛️ Sistema de Gestão de Decretos

Aplicação backend para gerenciamento, consulta e organização de decretos, com foco em controle administrativo, rastreabilidade e padronização de dados.

---

##  Problema

A gestão de decretos em órgãos públicos pode se tornar complexa devido a:

* Grande volume de documentos
* Dificuldade de localizar decretos específicos
* Falta de padronização nos cadastros
* Risco de inconsistência em informações importantes

Sem um sistema estruturado, o controle se torna manual, sujeito a erros e difícil de escalar.

---

##  Solução

Este projeto propõe uma API para gerenciamento de decretos, permitindo:

* Cadastro estruturado de decretos
* Consulta por número, data e outros critérios
* Atualização segura de informações (mantendo regras de negócio)
* Organização centralizada dos dados

A aplicação simula um cenário real de uso em ambientes administrativos.

---

##  Arquitetura

A aplicação segue boas práticas de desenvolvimento backend:

*  **Controller** → Responsável pelas requisições HTTP
*  **Service** → Regras de negócio
*  **Repository** → Acesso a dados
*  **DTOs** → Transporte de dados entre camadas

Esse modelo garante:

* Separação de responsabilidades
* Facilidade de manutenção
* Escalabilidade

---

##  Tecnologias utilizadas

* C# / .NET
* Entity Framework
* PostgreSQL

---

##  Regras de negócio implementadas

* O número do decreto não pode ser alterado após criação
* Validação de dados obrigatórios
* Estrutura preparada para consultas eficientes

---

##  Funcionalidades

*  Cadastro de decretos
*  Atualização de informações (com restrições)
*  Consulta por critérios específicos
*  Organização estruturada dos dados
*  Operações CRUD

---

##  Possíveis melhorias (roadmap)

Para evolução do sistema:

* Autenticação com JWT
* Controle de acesso por perfil (admin/usuário)
* Versionamento de decretos
* Logs de alterações
* Paginação e filtros avançados
* Upload de documentos (PDF dos decretos)
* Testes automatizados

---

##  Como rodar o projeto

```bash
git clone https://github.com/carolinefreitasalegre/AppDecreto
cd AppDecreto

# configurar connection string no appsettings
dotnet restore
dotnet run
```

---

##  Demonstração

<img width="1903" height="857" alt="imagem01Swagger" src="https://github.com/user-attachments/assets/c7f15c64-f364-4ad5-b6e2-c98070eb15f7" />
<img width="1920" height="857" alt="imagem02Swagger" src="https://github.com/user-attachments/assets/be3e6549-4f06-4a50-9d90-48a4c36cdc4a" />
<img width="1433" height="966" alt="imagem03Swagger" src="https://github.com/user-attachments/assets/2c473dfa-4fd3-4d65-8cff-0bc3f33dc0c6" />

---

##  Diferenciais do projeto

Este projeto se destaca por:

* Foco em um problema real (gestão administrativa)
* Implementação de regras de negócio específicas
* Estrutura backend organizada e escalável
* Uso de boas práticas com .NET

---

##  Autora

Caroline Freitas
https://github.com/carolinefreitasalegre
