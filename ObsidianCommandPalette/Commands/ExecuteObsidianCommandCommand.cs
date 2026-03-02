// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System.Web;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ObsidianCommandPalette.Commands;

/// <summary>
/// Advanced URI で Obsidian のコマンドを実行する。
/// 要: Obsidian Advanced URI プラグイン。
/// </summary>
public class ExecuteObsidianCommandCommand : InvokableCommand
{
    private readonly string _vaultName;
    private readonly string _commandId;
    private readonly string _displayName;

    public ExecuteObsidianCommandCommand(string vaultName, string commandId, string displayName)
    {
        _vaultName = vaultName;
        _commandId = commandId;
        _displayName = displayName;
        Name = displayName;
        Icon = new IconInfo("\uE7C8"); // Command
    }

    public string GetUri()
    {
        var vault = HttpUtility.UrlEncode(_vaultName);
        var encodedId = HttpUtility.UrlEncode(_commandId);
        return $"obsidian://advanced-uri?vault={vault}&commandid={encodedId}";
    }

    public override ICommandResult Invoke()
    {
        return new OpenObsidianUriCommand(GetUri()).Invoke();
    }
}
