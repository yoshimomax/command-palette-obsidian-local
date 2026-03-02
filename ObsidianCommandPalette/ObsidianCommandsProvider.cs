// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ObsidianCommandPalette;

public class ObsidianCommandsProvider : CommandProvider
{
    public ObsidianCommandsProvider()
    {
        DisplayName = "Obsidian";
        Icon = new IconInfo("\uE8A7"); // Document / note icon
    }

    private readonly ICommandItem[] _commands =
    [
        new CommandItem(new Pages.ObsidianNoteSearchPage())
        {
            Title = "ノートを検索して開く",
            Subtitle = "ファイル名・パスで検索し、Obsidian で開く",
        },
        new CommandItem(new Pages.ObsidianCommandPage())
        {
            Title = "Obsidian コマンドを実行",
            Subtitle = "Advanced URI でコマンドを実行（要 Advanced URI プラグイン）",
        },
        new CommandItem(new OpenConfigFolderCommand())
        {
            Title = "設定フォルダを開く",
            Subtitle = "Vault 設定用の config.json があるフォルダを開く",
        },
    ];

    public override ICommandItem[] TopLevelCommands() => _commands;
}
