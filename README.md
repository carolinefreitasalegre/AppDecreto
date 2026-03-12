## AppDecreto

O AppDecreto nasceu ao identificar a dificuldade no gerenciamento de solicitações de números de decretos no setor onde meu esposo trabalha.

O processo era totalmente manual: os pedidos eram anotados à mão e posteriormente enviados para um grupo de WhatsApp para controle. Esse método, além de pouco organizado, abria margem para problemas como:

- Números duplicados

- Quebra da sequência numérica

- Esquecimento de registros

- Falta de rastreabilidade...

Diante desse cenário, desenvolvi uma solução simples e eficiente para modernizar e automatizar esse controle.

## Objetivo

Criar um sistema que:

- Permita o registro das solicitações de decretos

- Garanta que os números não sejam duplicados

- Mantenha a sequência numérica correta

- Centralize e organize os registros

- Reduza erros humanos

O sistema assegura a integridade da numeração de forma automática, tornando o processo mais seguro e confiável.

## Arquitetura

O projeto está sendo desenvolvido seguindo boas práticas de engenharia de software:

- Clean Architecture

- Princípios SOLID

- Programação Orientada a Objetos (POO)

- Separação de responsabilidades

- Código limpo e organizado

A estrutura está dividida em camadas:

Domain → Entidades e regras de negócio

Application → Casos de uso e contratos

Infrastructure → Acesso a dados e implementações externas

API → Camada de exposição dos endpoints

## Tecnologias Utilizadas

.NET

PostgreSQL

Dapper

REST API

O banco de dados utilizado é o PostgreSQL, e o acesso aos dados é feito com Dapper, priorizando performance e controle direto sobre as queries.

- Status do Projeto

- Em desenvolvimento

Próximos passos:

Corrigir alguns bugs

Implementar autenticação de usuários

Criar controle de permissões

Adicionar logs de auditoria

Melhorar validações e tratamento de erros

Criar interface front-end

## Motivação

Além de resolver um problema real, o projeto também faz parte do meu processo de evolução como desenvolvedora, aplicando na prática conceitos como arquitetura limpa, boas práticas e organização de código.
