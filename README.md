# API Web para Gestión de usuarios

Este proyecto es una API Web en .NET 8 diseñada para gestionar preguntas de cuestionarios. Incluye endpoints para recuperar, agregar y eliminar preguntas de cuestionarios. La API está construida con ASP.NET Core y utiliza Entity Framework Core para las interacciones con la base de datos en SQL Server.

## Características

**Operaciones CRUD:** Recuperar, agregar, actualizar y eliminar usuarios.
**Validación:** Asegura que los datos de entrada cumplan con los requisitos establecidos.
**Integración con Swagger:** Proporciona documentación interactiva de la API en modo desarrollo

## Configuración Inicial

## 1. Clonar el Repositorio

```
git clone https://github.com/ingenierocastroucc/dasigno.git

```
## 2. Creación de base de datos

**Descripción:** Para la creacion de la base de datos.

Ejecuta Add-Migration InitialCreate para la creación inicial de la migración de base de datos. 

```
Add-Migration InitialCreate

```

Luego ejecuta Update-Database para la creación inicial de las tablas de base de datos. 

```
Update-Database

```

## 3. Actualizar la Cadena de Conexión

Actualiza la cadena de conexión DefaultConnection en el archivo appsettings.json con tu cadena de conexión a SQL Server:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=LAPTOP-PH1R9POH;Database=Demokrata;Integrated Security=True;TrustServerCertificate=True;"
  }
}

```

## Ejemplo de Solicitud:

```
GET /api/Usuarios/ObtenerUsuarioPorId/{id}

```

```
GET /api/Usuarios/ObtenerUsuarios

```

```
PUT /api/Usuarios/ActualizarUsuario/{id}

```

```
DELETE /api/Usuarios/EliminarUsuario/{id}

```

# Endpoints

## POST /api/Usuarios/CrearUsuario

**Descripción:** Crea un usuario nuevo.

**Parámetros:** No parameters

**Respuestas:**

**200 OK:** Si crea el usuario exitosamente.
**400 Bad Request:** Si el usuario no se creo con éxito.
**500 Internal Server Error:** Si ocurre un error inesperado.

## JSON POST para postman:

```
[
	{
	  "primerNombre": "Pedro",
	  "segundoNombre": "Jose",
	  "primerApellido": "Castro",
	  "segundoApellido": "Colon",
	  "fechaNacimiento": "2024-11-19T04:55:37.323Z",
	  "sueldo": 6.000.000,
	  "fechaCreacion": "2024-11-19T04:55:37.323Z",
	  "fechaModificacion": "2024-11-19T04:55:37.323Z"
	}
]

```

## GET /api/Usuarios/ObtenerUsuarioPorId/{id}

**Descripción:** Obtiene los usuarios por Id.

## Parámetros:

**Id (cuerpo):** Identificador único del usuario.

**Esquema del Cuerpo:**

```
https://localhost:puerto/api/Usuarios/ObtenerUsuarioPorId/1
```

## Respuestas:

**200 OK:** Si la pregunta se agrega correctamente. La respuesta incluye la ubicación del nuevo recurso.
**400 Bad Request:** Si el payload de la solicitud es inválido.
**401 Not found:** Si no se encuentra el registro.

## Ejemplo de Solicitud:

```
GET https://localhost:puerto/api/Usuarios/ObtenerUsuarioPorId/1

```
## Ejemplo de Respuesta:

```
{
  "id": 1,
  "primerNombre": "Carlos",
  "segundoNombre": "Alberto",
  "primerApellido": "Gonzalez",
  "segundoApellido": "Martinez",
  "fechaNacimiento": "1990-05-15T00:00:00",
  "sueldo": 3500,
  "fechaCreacion": "2024-11-19T09:39:17.4601856",
  "fechaModificacion": "2024-11-19T09:39:17.4659443"
}
```

## GET /api/Usuarios/ObtenerUsuarios

**Descripción:** Obtiene los usuarios por nombres.

## Parámetros:

**Nombre:** Nombre del usuario.

**Esquema del Cuerpo:**

```
https://localhost:7156/api/Usuarios/ObtenerUsuarios?primerNombre=Carlos&pageNumber=1&pageSize=10
```

## Respuestas:

**200 OK:** Si la pregunta se agrega correctamente. La respuesta incluye la ubicación del nuevo recurso.
**400 Bad Request:** Si el payload de la solicitud es inválido.
**401 Not found:** Si no se encuentra el registro.

## Ejemplo de Solicitud:

```
GET https://localhost:7156/api/Usuarios/ObtenerUsuarios?primerNombre=Carlos&pageNumber=1&pageSize=10

```
## Ejemplo de Respuesta:

```
[
  {
    "id": 1,
    "primerNombre": "Carlos",
    "segundoNombre": "Alberto",
    "primerApellido": "Gonzalez",
    "segundoApellido": "Martinez",
    "fechaNacimiento": "1990-05-15T00:00:00",
    "sueldo": 3500,
    "fechaCreacion": "2024-11-19T09:39:17.4601856",
    "fechaModificacion": "2024-11-19T09:39:17.4659443"
  }
]
```

## PUT /api/Usuarios/ActualizarUsuario/{id}

**Descripción:** Actualiza un usuario por su ID.

**Parámetros:**

**id (Guid):** El identificador único del usuario a actualizar.

## Respuestas:

**200 OK:** Si la pregunta se elimina correctamente.
**400 Bad Request:** Si el ID es inválido.
**404 Not Found:** Si la pregunta con el ID especificado no existe.
**500 Internal Server Error:** Si ocurre un error inesperado.

## Ejemplo de Solicitud:

```
https://localhost:puerto/api/Usuarios/ActualizarUsuario/1
```

## Ejemplo de Respuesta:

```
[
	{
	  "id": 1,
	  "primerNombre": "string",
	  "segundoNombre": "string",
	  "primerApellido": "string",
	  "segundoApellido": "string",
	  "fechaNacimiento": "2024-11-19T15:45:56.828Z",
	  "sueldo": 100000,
	  "fechaCreacion": "2024-11-19T09:39:17.4601856",
	  "fechaModificacion": "2024-11-19T15:46:11.3901643Z"
	}
]
```

## DELETE /api/Usuarios/EliminarUsuario/{id}

**Descripción:** Elimina el usuario por su ID.

**Parámetros:**

**id (Guid):** El identificador único del usuario a eliminar.

## Respuestas:

**200 OK:** Si el usuario se elimina correctamente.
**400 Bad Request:** Si el ID es inválido.
**404 Not Found:** Si el usuario con el ID especificado no existe.
**500 Internal Server Error:** Si ocurre un error inesperado.

## Ejemplo de Solicitud:

```
DELETE https://localhost:(puerto)/api/Usuarios/EliminarUsuario/{id}
```

## Pruebas Unitarias


Este README proporciona una visión general completa del proyecto, cubriendo la configuración, los endpoints, la configuración de CORS, la validación del modelo y pruebas unitarias con Xunit y Moq. Está diseñado para ayudar a otros desarrolladores a entender y usar la API de manera efectiva.