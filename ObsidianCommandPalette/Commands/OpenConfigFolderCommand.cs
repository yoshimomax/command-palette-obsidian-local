// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System.Diagnostics;
using Microsoft.CommandPalette.Extensions.Toolkit;
using ObsidianCommandPalette.Config;

namespace ObsidianCommandPalette.Commands;

/// <summary>
/// 設定フォルダ（config.json がある場所）をエクスプローラーで開く。
/// </summary>
public class OpenConfigFolderCommand : InvokableCommand
{
    public OpenConfigFolderCommand()
    {
        Name = "Open config folder";
        Icon = new IconInfo("\uE8B7"); // Folder
    }

    public override ICommandResult Invoke()
    {
        try
        {
            var path = ObsidianConfigService.GetConfigFolderPath();
            Process.Start(new ProcessStartInfo("explorer.exe", path) { UseShellExecute = true });
        }
        catch
        {
            // 無視
        }

        return CommandResult.Hide();
    }
}
