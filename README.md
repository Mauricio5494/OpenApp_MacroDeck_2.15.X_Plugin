# Open Multiple Apps Plugin for Macro Deck 2.15.3

A Macro Deck 2.15.3 plugin that lets you open multiple applications or files from a single button. You can run them all at once (asynchronously) or one after another (sequentially), depending on your needs.

**Important:** This plugin is for **Macro Deck 2.15.3 only**. It is **not** compatible with Macro Deck 3.

## What it does

With one button press, this plugin opens a list of executables or files you configure:

- **.exe files** — launched directly
- **.txt, .pdf, and other files** — opened with the default Windows application (using Shell Execute)
- **Parallel mode** — all apps open at the same time without waiting
- **Sequential mode** — each app opens only after the previous one finishes

## Features

- Add multiple file or executable paths per button
- Toggle "Use default application" for non-executable files (.txt, .pdf, etc.)
- Toggle asynchronous execution (open all at once vs. one by one)
- Simple configuration dialog inside Macro Deck's action configurator
- Lightweight and dependency-free (only references `Macro Deck 2.dll`)

## Requirements

- Windows 10 or Windows 11
- Macro Deck 2.15.3 installed
- .NET 10 runtime (for development only; not required to run the plugin)

## Installation on Windows

1. Close Macro Deck completely if it is running.

2. Copy the plugin files to the Macro Deck plugins folder:

   ```
   C:\Users\<your-username>\AppData\Roaming\Macro Deck\plugins\OpenAPP\
   ```

   The folder should contain:
   -  `OpenAppsPlugin.dll`
   -  `OpenAppsPlugin.pdb`
   -  `OpenAppsPlugin.deps.json`
   -  `ExtensionManifest.json`

3. Make sure there is **no `.delete` file** inside the plugin folder. Macro Deck uses that file to ignore plugins.

4. Start Macro Deck 2.15.3.

5. Go to the action configurator and look for **"Abrir Apps / Archivos"** (Open Multiple Apps) in the action list.

6. Drag the action onto a button, then click it to configure:
   - Add the paths to the apps or files you want to open
   - Check or uncheck **"Ejecutar asíncrono (abrir todas a la vez)"** (Run asynchronously)
   - Check or uncheck **"Usar aplicación predeterminada (para .txt, .pdf, etc.)"** (Use default application)

7. Save and test your button.

## Troubleshooting

- **Plugin does not appear:** Make sure `ExtensionManifest.json` is in the same folder as the DLL, and that Macro Deck was restarted after installation.
- **NullReferenceException on load:** Ensure `repository` in `ExtensionManifest.json` is not empty (`""`). It should contain a valid URL string.
- **Plugin compiled for .NET 10:** The DLL targets `net10.0-windows`. Make sure your system meets the requirements for Macro Deck 2.15.3.

## Compatibility

| Version | Supported |
|---------|-----------|
| Macro Deck 2.15.3 | Yes |
| Macro Deck 3 | No |

## License

MIT License
