// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using ObsidianCommandPalette.Commands;
using ObsidianCommandPalette.Config;

namespace ObsidianCommandPalette.Pages;

/// <summary>
/// よく使う Obsidian コマンドをプリセット表示。
/// Advanced URI プラグインが必要。将来、config でコマンド一覧を拡張可能にする余地あり。
/// </summary>
public sealed class ObsidianCommandPage : ListPage
{
    public ObsidianCommandPage()
    {
        Icon = new IconInfo("\uE7C8");
        Name = "Open";
        PlaceholderText = "実行するコマンドを選んでください";
    }

    public override IListItem[] GetItems()
    {
        var vault = ObsidianConfigService.GetCurrentVault();
        if (vault == null || string.IsNullOrWhiteSpace(vault.Name))
        {
            return GetConfigRequiredItems();
        }

        var vaultName = vault.Name;
        var list = new List<IListItem>();

        // プリセット: よく使うコマンド（commandid は Obsidian の組み込み／プラグインで共通）
        Add(list, vaultName, "workspace:close", "現在のタブを閉じる");
        Add(list, vaultName, "editor:save-file", "ファイルを保存");
        Add(list, vaultName, "editor:toggle-bold", "太字");
        Add(list, vaultName, "editor:toggle-italics", "イタリック");
        Add(list, vaultName, "editor:toggle-code", "コード");
        Add(list, vaultName, "editor:insert-wikilink", "ウィキリンクを挿入");
        Add(list, vaultName, "editor:open-search", "検索を開く");
        Add(list, vaultName, "editor:open-search-replace", "検索と置換を開く");

        return list.ToArray();
    }

    private static void Add(List<IListItem> list, string vaultName, string commandId, string displayName)
    {
        list.Add(new ListItem(new ExecuteObsidianCommandCommand(vaultName, commandId, displayName))
        {
            Title = displayName,
            Subtitle = commandId,
        });
    }

    private static IListItem[] GetConfigRequiredItems()
    {
        var openFolderCommand = new AnonymousCommand(() =>
        {
            try
            {
                var path = ObsidianConfigService.GetConfigFolderPath();
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", path) { UseShellExecute = true });
            }
            catch { }
        }) { Result = CommandResult.KeepOpen() };

        return
        [
            new ListItem(openFolderCommand)
            {
                Title = "Vault を設定してください",
                Subtitle = "config.json に Vault の Path と Name を設定してください",
            },
        ];
    }
}
