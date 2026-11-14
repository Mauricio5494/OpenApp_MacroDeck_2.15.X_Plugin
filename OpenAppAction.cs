using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

namespace OpenAppsPlugin;

public sealed class OpenAppAction : PluginAction
{
    public override string Name => "Abrir Apps / Archivos";

    public override string Description =>
        "Abre múltiples aplicaciones o archivos desde un solo botón. " +
        "Configura las rutas haciendo clic en esta acción en el configurador de Macro Deck.";

    public override bool CanConfigure => true;

    public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
    {
        return new OpenAppConfigControl(this);
    }

    public override void Trigger(string clientId, ActionButton actionButton)
    {
        var config = ParseConfiguration(Configuration);
        if (config == null || config.Paths == null || config.Paths.Count == 0)
        {
            return;
        }

        foreach (var path in config.Paths)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                continue;
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = config.UseShellExecute,
                };

                var process = Process.Start(psi);

                if (process != null && !config.RunAsync)
                {
                    process.WaitForExit();
                }
            }
            catch
            {
                // Si falla uno, seguimos con el siguiente
            }
        }
    }

    private static AppConfig? ParseConfiguration(string? configuration)
    {
        if (string.IsNullOrWhiteSpace(configuration))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<AppConfig>(configuration);
        }
        catch
        {
            return null;
        }
    }

    private class AppConfig
    {
        public List<string>? Paths { get; set; }
        public bool UseShellExecute { get; set; } = true;
        public bool RunAsync { get; set; } = true;
    }
}
