// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System.Diagnostics;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ObsidianCommandPalette.Commands;

/// <summary>
/// obsidian:// の URI を既定のハンドラ（Obsidian）で開く。
/// </summary>
public class OpenObsidianUriCommand : InvokableCommand
{
    private readonly string _uri;

    public OpenObsidianUriCommand(string uri)
    {
        _uri = uri;
        Name = "Open";
        Icon = new IconInfo("\uE8A7");
    }

    public override ICommandResult Invoke()
    {
        try
        {
            Process.Start(new ProcessStartInfo(_uri) { UseShellExecute = true });
        }
        catch
        {
            // 無視
        }

        return CommandResult.Hide();
    }
}
