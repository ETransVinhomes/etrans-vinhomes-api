![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white) 
![RabbitMQ](https://img.shields.io/badge/rabbitmq-%23FF6600.svg?&style=for-the-badge&logo=rabbitmq&logoColor=white)
# E-Transportation-Vinhomes Backend
### running
```terminal
    docker-compose build
    docker-compose up
```

### Default URL
- **ETransVinhomesAPI**  : `http://localhost:4201/api/...`
- **ETransVinhomes-AuthAPI** : `http://localhost:4202/api/auth`
- **Server** : `http://4.194.94.219:port/...` 
    Ex: for `http get` all `locationtypes`
    ```
    http://4.194.94.219:7061/gateway/api/location-types
    ```
- `Authentication` & `Authorization` implement with Identity User
- All endpoints config at `ocelot.json`

    
