## Arquitectura N-Capas
 Se creo un servicio web para poder gestionar la información del producto (agregar, actualizar y obtener).

## Patrones Utilizados

### CQRS (Command Query Responsibility Segregation)
El patrón CQRS separa las operaciones de lectura (consultas) y las operaciones de escritura (comandos) en aplicaciones. Esta separación permite una mayor escalabilidad y flexibilidad.

### Mediator Pattern
El patrón Mediator se utiliza para desacoplar componentes y promover una comunicación indirecta entre ellos.

### Unit of Work
El patrón Unit of Work se utiliza para gestionar las transacciones y garantizar la coherencia en la base de datos.

### Repository
El patrón Repository se utiliza para abstraer y encapsular el acceso a los datos.

## Servicio Externo
Se utiliza el servicio de [apimocha](https://apimocha.com/) para obtener los descuentos de los productos

## Sqlite
Se utilizo sqlite para almacenar la información de manera local
