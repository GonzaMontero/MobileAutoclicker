# MobileAutoclicker

This is a project which will begin as a college project which will scale to a portfolio project


EN:
The design patterns I chose to tackle with this project are: Singleton, Object Pooling as well as Factory.

Singleton: In order to handle singletons in this project I created 2 base classes for different singleton types (which can be found in the Utils section on the script folder). These singleton types are MonobehaviourSingleton and MonobehaviourSingletonInScene - which handle both a singleton which persists during the whole project as well as singletons which only remain during a single scene (such as, for example, game managers which should not be active during menus or loading screens). These are used as base files for Managers and data handlers.

Object Pooling: Inside the General Managers folder in the Utils folder (where the singletons are stored) there is a file called ObjectPooler. This file (a singleton) is in charge of generating copies of certain items before starting to play, and of enabling and disabling those items as requested. This allows better memory usage instead of deleting and instancing new objects.

Factory: The factory pattern, which can be found in the Towers folder within the Frontend file, is a collections of methods which are used as a middleman to call and generate bullets during runtime, which also helps alter those bullets when required (spawn position as well as target for example)

ES:
Los patrones de diseño que elegí abordar con este proyecto son: Singleton, Object Pooling y Factory.

Singleton:
Para manejar los singletons en este proyecto, creé dos clases base para diferentes tipos de singletons (que se pueden encontrar en la sección Utils dentro de la carpeta de scripts). Estos tipos de singletons son MonobehaviourSingleton y MonobehaviourSingletonInScene, los cuales manejan tanto un singleton que persiste durante todo el proyecto como singletons que solo permanecen activos durante una única escena (como, por ejemplo, gestores de juego que no deberían estar activos en menús o pantallas de carga). Estas clases se usan como base para los gestores (Managers) y manejadores de datos.

Object Pooling:
Dentro de la carpeta General Managers en la carpeta Utils (donde se almacenan los singletons), hay un archivo llamado ObjectPooler. Este archivo (un singleton) se encarga de generar copias de ciertos elementos antes de iniciar el juego y de habilitar o deshabilitar esos elementos según se requiera. Esto permite un mejor uso de la memoria en lugar de eliminar e instanciar nuevos objetos constantemente.

Factory:
El patrón Factory, que se puede encontrar en la carpeta Towers dentro del archivo Frontend, es una colección de métodos que actúan como intermediarios para generar proyectiles en tiempo de ejecución. Este patrón también facilita la modificación de los proyectiles según sea necesario (por ejemplo, la posición de aparición o el objetivo).
