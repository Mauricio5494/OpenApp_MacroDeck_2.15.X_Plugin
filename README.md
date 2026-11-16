# Open Multiple Apps Plugin for Macro Deck 2.15.3

Un plugin de Macro Deck 2.15.3 que abre varias aplicaciones o archivos desde un solo botón. Puedes lanzarlas todas a la vez (de forma asíncrona) o una tras otra (secuencialmente), según lo que necesites.

Solo funciona con **Macro Deck 2.15.3**. No es compatible con Macro Deck 3.

Un pulsador abre la lista de ejecutables o archivos que configures:

- `.exe` — lanzados directamente
- `.txt`, `.pdf` y otros archivos — abiertos con la aplicación predeterminada de Windows (usando Shell Execute)
- **Modo paralelo**: todas las apps se abren al mismo tiempo, sin esperar
- **Modo secuencial**: cada app se abre solo después de que la anterior termine

## Características

- Añade varias rutas de archivo o ejecutable por botón
- Activa o desactiva "Usar aplicación predeterminada" para archivos no ejecutables (.txt, .pdf, etc.)
- Activa o desactiva ejecución asíncrona (abrir todo a la vez vs. uno por uno)
- Diálogo de configuración sencillo dentro del configurador de acciones de Macro Deck
- Ligero y sin dependencias (solo referencia `Macro Deck 2.dll`)

## Requisitos

- Windows 10 o Windows 11
- Macro Deck 2.15.3 instalado
- .NET 10 runtime (solo para desarrollo; no es necesario para ejecutar el plugin)

## Instalación en Windows

1. Cierra Macro Deck completamente si está en ejecución.

2. Copia los archivos del plugin a la carpeta de plugins de Macro Deck:

   ```
   C:\Users\<tu-usuario>\AppData\Roaming\Macro Deck\plugins\OpenAPP\
   ```

   La carpeta debe contener:
   - `OpenAppsPlugin.dll`
   - `OpenAppsPlugin.pdb`
   - `OpenAppsPlugin.deps.json`
   - `ExtensionManifest.json`

3. Asegúrate de que no haya ningún archivo `.delete` dentro de la carpeta del plugin. Macro Deck usa ese archivo para ignorar plugins.

4. Inicia Macro Deck 2.15.3.

5. Ve al configurador de acciones y busca **"Abrir Apps / Archivos"** (Open Multiple Apps) en la lista de acciones.

6. Arrastra la acción a un botón y haz clic para configurarla:
   - Añade las rutas de las apps o archivos que quieras abrir
   - Marca o desmarca **"Ejecutar asíncrono (abrir todas a la vez)"** (Run asynchronously)
   - Marca o desmarca **"Usar aplicación predeterminada (para .txt, .pdf, etc.)"** (Use default application)

7. Guarda y prueba tu botón.

## Solución de problemas

- **El plugin no aparece:** asegúrate de que `ExtensionManifest.json` está en la misma carpeta que la DLL, y de que reiniciaste Macro Deck tras la instalación.
- **NullReferenceException al cargar:** comprueba que `repository` en `ExtensionManifest.json` no esté vacío (`""`). Debe contener una URL válida.
- **El plugin está compilado para .NET 10:** la DLL tiene como destino `net10.0-windows`. Asegúrate de que tu sistema cumple los requisitos de Macro Deck 2.15.3.

## Compatibilidad

| Versión | Soportada |
|---------|-----------|
| Macro Deck 2.15.3 | Sí |
| Macro Deck 3 | No |

## Licencia

MIT License
