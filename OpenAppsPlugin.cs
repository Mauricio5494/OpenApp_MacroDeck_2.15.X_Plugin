using System.Collections.Generic;
using SuchByte.MacroDeck.Plugins;

namespace OpenAppsPlugin;

public sealed class OpenAppsPlugin : MacroDeckPlugin
{
    private readonly OpenAppAction _action;

    public OpenAppsPlugin()
    {
        _action = new OpenAppAction();
        Actions.Add(_action);
    }

    public override bool CanConfigure => false;

    public override void Enable()
    {
    }
}
