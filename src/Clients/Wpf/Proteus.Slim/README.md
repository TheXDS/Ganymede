# Proteus.Slim
Mítica interfaz gráfica de [Proteus](https://github.com/TheXDS/Proteus.git) reescrita como un UI Broker para [Ganymede](https://github.com/TheXDS/Ganymede.git).
### Introducción
`Proteus` inició como un proyecto privado para crear aplicaciones con UI dinámica conectadas a bases de datos. Con el paso del tiempo, algunas falencias importantes en el proyecto impidieron que evolucionara, pero su elegante interfaz aún tiene un lugar importante. Este proyecto apunta a recrear la mítica experiencia de UI disponible en `Proteus`, a la vez que se arreglan algunos de sus problemas existentes.

### Limitaciones
En vista de simplificar las dependencias del proyecto, intencionalmente no se incluyen plantillas para `Xceed.Wpf.Toolkit`, como se hacía en la versión original de Proteus. Por lo tanto, una nueva librería de estilos debería ser incluída o creada para manejar tales controles.

Dado que este proyecto se basa en `Ganymede`, cabe recalcar que obviamente este no es el toolkit de `Proteus`, aquí únicamente se emula el aspecto visual. Por lo tanto, no se incluye ningún tipo de generador dinámico de UI mas allá del necesario para los servicios básicos de diálogo.