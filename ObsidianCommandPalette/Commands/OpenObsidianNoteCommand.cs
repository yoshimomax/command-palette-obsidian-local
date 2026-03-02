// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System.Web;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ObsidianCommandPalette.Commands;

/// <summary>
/// Obsidian のノートを obsidian://open で開く。
/// </summary>
public class OpenObsidianNoteCommand : InvokableCommand
{
    private readonly string _vaultName;
    private readonly string _filePath;

    public OpenObsidianNoteCommand(string vaultName, string filePath)
    {
        _vaultName = vaultName;
        _filePath = filePath;
        Name = "Open note";
        Icon = new IconInfo("\uE8A5");
    }

    public string GetUri()
    {
        var vault = HttpUtility.UrlEncode(_vaultName);
        var file = HttpUtility.UrlEncode(_filePath.Replace('\\', '/'));
        return $"obsidian://open?vault={vault}&file={file}";
    }

    public override ICommandResult Invoke()
    {
        return new OpenObsidianUriCommand(GetUri()).Invoke();
    }
}
