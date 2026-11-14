using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

namespace OpenAppsPlugin;

public sealed class OpenAppConfigControl : ActionConfigControl
{
    private readonly PluginAction _pluginAction;
    private readonly ListBox _listPaths;
    private readonly Label _lblStatus;
    private CheckBox _chkAsync = null!;
    private CheckBox _chkShell = null!;

    public OpenAppConfigControl(PluginAction pluginAction)
    {
        _pluginAction = pluginAction;

        var lblTitle = new Label
        {
            Text = "Abrir Apps / Archivos - Rutas",
            Dock = DockStyle.Top,
            Height = 30,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Tahoma", 12f, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.FromArgb(45, 45, 45),
            Padding = new Padding(10, 5, 0, 0)
        };

        _listPaths = new ListBox
        {
            Dock = DockStyle.Top,
            Height = 130,
            IntegralHeight = true,
            ItemHeight = 25,
            BackColor = Color.FromArgb(60, 60, 60),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };

        var btnAdd = new Button
        {
            Text = "Agregar archivo...",
            Dock = DockStyle.Top,
            Height = 35,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 123, 255),
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Click += (_, _) => AddFiles();

        var btnRemove = new Button
        {
            Text = "Eliminar seleccionado",
            Dock = DockStyle.Top,
            Height = 35,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(150, 50, 50),
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnRemove.FlatAppearance.BorderSize = 0;
        btnRemove.Click += (_, _) =>
        {
            if (_listPaths.SelectedItem != null)
            {
                _listPaths.Items.Remove(_listPaths.SelectedItem);
            }
        };

        _chkAsync = new CheckBox
        {
            Text = "Ejecutar asíncrono (abrir todas a la vez)",
            Dock = DockStyle.Top,
            Height = 25,
            BackColor = Color.FromArgb(45, 45, 45),
            ForeColor = Color.White,
            Checked = true
        };

        _chkShell = new CheckBox
        {
            Text = "Usar aplicación predeterminada (para .txt, .pdf, etc.)",
            Dock = DockStyle.Top,
            Height = 25,
            BackColor = Color.FromArgb(45, 45, 45),
            ForeColor = Color.White,
            Checked = true
        };

        _lblStatus = new Label
        {
            Text = "",
            Dock = DockStyle.Bottom,
            Height = 25,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Tahoma", 9f),
            ForeColor = Color.LightGray,
            BackColor = Color.FromArgb(45, 45, 45),
            Padding = new Padding(10, 0, 0, 0)
        };

        var panelButtons = new Panel
        {
            Dock = DockStyle.Top,
            Height = 35
        };
        panelButtons.Controls.Add(btnRemove);
        panelButtons.Controls.Add(btnAdd);
        btnAdd.Dock = DockStyle.Right;
        btnRemove.Dock = DockStyle.Right;

        Controls.Add(lblTitle);
        Controls.Add(_listPaths);
        Controls.Add(panelButtons);
        Controls.Add(_chkAsync);
        Controls.Add(_chkShell);
        Controls.Add(_lblStatus);

        LoadConfiguration();
    }

    private void LoadConfiguration()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(_pluginAction.Configuration))
            {
                var config = JsonSerializer.Deserialize<AppConfig>(_pluginAction.Configuration);
                if (config?.Paths != null)
                {
                    foreach (var path in config.Paths)
                    {
                        _listPaths.Items.Add(path);
                    }
                    // Forzar que la primera entrada quede visible al cargar la configuraciÃ³n
                    if (_listPaths.Items.Count > 0)
                    {
                        _listPaths.TopIndex = 0;
                    }
                }
                if (config != null)
                {
                    _chkAsync.Checked = config.RunAsync;
                    _chkShell.Checked = config.UseShellExecute;
                }
            }
        }
        catch
        {
        }
    }

    private void AddFiles()
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Seleccionar aplicaciones o archivos",
            Filter = "Todos los archivos (*.*)|*.*|Ejecutables (*.exe)|*.exe",
            Multiselect = true
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            bool seAgregaron = false;
            foreach (var file in dialog.FileNames)
            {
                if (!_listPaths.Items.Contains(file))
                {
                    _listPaths.Items.Add(file);
                    seAgregaron = true;
                }
            }
            if (seAgregaron && _listPaths.Items.Count > 0)
            {
                // Garantizar que la primera entrada sea visible tras agregar
                _listPaths.TopIndex = 0;
            }
        }
    }

    public override bool OnActionSave()
    {
        var paths = new List<string>();
        foreach (var item in _listPaths.Items)
        {
            paths.Add(item.ToString()!);
        }

        var config = new AppConfig
        {
            Paths = paths,
            UseShellExecute = _chkShell.Checked,
            RunAsync = _chkAsync.Checked
        };

        _pluginAction.Configuration = JsonSerializer.Serialize(config);
        _lblStatus.Text = $"Guardado: {paths.Count} archivo(s)";
        return true;
    }

    private class AppConfig
    {
        public List<string>? Paths { get; set; }
        public bool UseShellExecute { get; set; } = true;
        public bool RunAsync { get; set; } = true;
    }
}
