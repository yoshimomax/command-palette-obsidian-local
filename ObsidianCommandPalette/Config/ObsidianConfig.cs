// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

namespace ObsidianCommandPalette.Config;

/// <summary>
/// 設定モデル。将来の複数 Vault 対応を見越して Vaults をリストで保持。
/// </summary>
public class ObsidianConfig
{
    /// <summary>Vault のリスト。先頭をデフォルトとして使用。</summary>
    public List<VaultEntry> Vaults { get; set; } = new();
}

public class VaultEntry
{
    /// <summary>Vault フォルダのフルパス（例: C:\Users\...\MyVault）</summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>Obsidian URI で使う Vault 名（通常はフォルダ名。日本語可）</summary>
    public string Name { get; set; } = string.Empty;
}
