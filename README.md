# API voltada para autenticação do projeto Icarus.
Está trata-se da api utilizada para autenticação dos usuarios no projeto distribuido chamado **Icarus**.

## Tecnologias utilizadas no projeto.
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white) ![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)

## Endpoint para autenticação

#### Realiza get em todos os usuarios.

```http
  GET api/usuarios/${pagina}/${resultado}
```

| Header | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Authorization` | `string` | **Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Pagina` | `int` | Parametro para mudança de paginas. |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Resultado` | `int` | Parametro para mudança quantidade de resultados por pagina. |


#### Criar usuario.

```http
  POST api/usuarios/novo
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |

![FluxogramaAutenticacao-Etapa de registro drawio](https://github.com/k4im/icarus.autenticacao/assets/108486349/ffca5e05-b4d5-4c42-87ff-3d89dd3c5df2)



#### Desativar usuario.

```http
  POST api/usuarios/desativar/{chave}
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Chave` | `string` | Chave para desativar usuario. |

![FluxogramaAutenticacao-Desativar usuario drawio(1)](https://github.com/k4im/icarus.autenticacao/assets/108486349/f609b0fb-bee4-4462-84ae-106409a01be5)


#### Reativar usuario.

```http
  POST api/usuarios/desativar/{chave}
```

| Header | Tipo     | Descrição                         |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Authorization` |**Autenticação**. Jwt token |

| Parametro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `Chave` | `string` | Chave para reavitar usuario. |

![FluxogramaAutenticacao-Ativar usuario drawio](https://github.com/k4im/icarus.autenticacao/assets/108486349/db836ebe-898d-4232-96df-25045747b7e3)


#### Login.

```http
  POST api/usuarios/login
```

![FluxogramaAutenticacao-Etapa de login drawio](https://github.com/k4im/icarus.autenticacao/assets/108486349/66c68b29-5b6e-4dd0-9512-cd6286d3eb31)

## Environment Variables

Realizei uma verificação referente as variaveis de ambiente configuraveis.


`ASPNETCORE_ENVIRONMENT`

`DB_CONNECTION`


#### DB_CONNECTION
* Variavel responsavel por estar realizando a configuração de conexão com o banco de dados. A mesma pode ser configuravel através dos arquivos de configurações assim como repassando por argumentos na execução docker.
  
## Deployment dotnet

Para rodar este projeto utilizando dotnet realize os seguintes comandos:

```bash
  cd ~/icarus.autenticação
```

```bash
  dotnet restore
```

```bash
  cd autenticacao.service/
```

```bash
  dotnet run
```


## Deployment docker

Para rodar este projeto utilizando docker realize os seguintes comandos:

```bash
  docker run --name=container_autenticacao -p 5112:5112 k4im/autenticacao:v0.1
```
