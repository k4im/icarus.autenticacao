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

#### Login.

```http
  POST api/usuarios/login
```

## Deployment dotnet

Para rodar este projeto utilizando dotnet realize os seguintes comandos:

```bash
  cd ~/icarus-autenticação
```

```bash
  dotnet restore
```

```bash
  cd estoque.service/
```

```bash
  dotnet run
```


## Deployment docker

Para rodar este projeto utilizando docker realize os seguintes comandos:

```bash
  docker run --name=container_autenticacao -p 5112:5112 k4im/autenticacao:v0.1
```
