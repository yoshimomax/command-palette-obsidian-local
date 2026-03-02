// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using ObsidianCommandPalette.Commands;
using ObsidianCommandPalette.Config;

namespace ObsidianCommandPalette.Pages;

public sealed class ObsidianNoteSearchPage : DynamicListPage
{
    public ObsidianNoteSearchPage()
    {
        Icon = new IconInfo("\uE8A5");
        Name = "Open";
        PlaceholderText = "ノート名やフォルダパスで検索...";
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        RaiseItemsChanged();
    }

    public override IListItem[] GetItems()
    {
        var vault = ObsidianConfigService.GetCurrentVault();
        if (vault == null || string.IsNullOrWhiteSpace(vault.Path) || !Directory.Exists(vault.Path))
        {
            return GetConfigRequiredItems();
        }

        var query = (SearchText ?? "").Trim();
        var files = EnumerateMarkdownFiles(vault.Path);
        var filtered = string.IsNullOrEmpty(query)
            ? files.Take(100)
            : files.Where(f => MatchesQuery(f, query)).Take(100);

        return filtered
            .Select(relativePath => new ListItem(new OpenObsidianNoteCommand(vault.Name, relativePath))
            {
                Title = Path.GetFileName(relativePath),
                Subtitle = relativePath,
            })
            .ToArray();
    }

    private static IEnumerable<string> EnumerateMarkdownFiles(string vaultPath)
    {
        try
        {
            return Directory.EnumerateFiles(vaultPath, "*.md", SearchOption.AllDirectories)
                .Select(fullPath =>
                {
                    var relative = Path.GetRelativePath(vaultPath, fullPath);
                    return relative.Replace('\\', '/');
                });
        }
        catch
        {
            return Array.Empty<string>();
        }
    }

    private static bool MatchesQuery(string relativePath, string query)
    {
        var lower = query.ToLowerInvariant();
        var pathLower = relativePath.ToLowerInvariant();
        var nameLower = Path.GetFileName(relativePath).ToLowerInvariant();
        return pathLower.Contains(lower) || nameLower.Contains(lower);
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
                Subtitle = "設定フォルダを開き、config.json に Vault の Path と Name を追加してください",
            },
        ];
    }
}
