// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System.IO;
using System.Text.Json;

namespace ObsidianCommandPalette.Config;

public static class ObsidianConfigService
{
    private static readonly string ConfigDirectory =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ObsidianCommandPalette");
    private static readonly string ConfigPath = Path.Combine(ConfigDirectory, "config.json");
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static ObsidianConfig Load()
    {
        try
        {
            if (File.Exists(ConfigPath))
            {
                var json = File.ReadAllText(ConfigPath);
                var config = JsonSerializer.Deserialize<ObsidianConfig>(json);
                if (config?.Vaults.Count > 0)
                    return config;
            }
        }
        catch
        {
            // 読み込み失敗時はデフォルトを返す
        }

        return new ObsidianConfig();
    }

    public static void Save(ObsidianConfig config)
    {
        try
        {
            Directory.CreateDirectory(ConfigDirectory);
            var json = JsonSerializer.Serialize(config, JsonOptions);
            File.WriteAllText(ConfigPath, json);
        }
        catch
        {
            // 保存失敗は無視
        }
    }

    /// <summary>設定ファイルがあるフォルダのパス（「設定フォルダを開く」用）</summary>
    public static string GetConfigFolderPath() => ConfigDirectory;

    /// <summary>現在の Vault（先頭）。未設定なら null。</summary>
    public static VaultEntry? GetCurrentVault()
    {
        var config = Load();
        return config.Vaults.Count > 0 ? config.Vaults[0] : null;
    }
}
