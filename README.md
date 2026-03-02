# Obsidian Command Palette（PowerToys 拡張）

PowerToys の **Command Palette** 用拡張です。次ができます。

- **Obsidian のノートを検索して開く** … Vault 内の .md をファイル名・パスで検索し、選択で Obsidian で開く
- **Obsidian のコマンドを実行** … [Advanced URI](https://github.com/Vinzent03/obsidian-advanced-uri) を使ってコマンドを実行（要プラグイン）
- **設定フォルダを開く** … Vault 設定用の config.json があるフォルダを開く

## 必要環境

- Windows 10/11
- [PowerToys](https://github.com/microsoft/PowerToys)（Command Palette が含まれるバージョン）
- Obsidian
- コマンド実行を使う場合: Obsidian の **Advanced URI** プラグイン

## ビルド・デプロイ

### 方法 A: GitHub Actions でビルド（Visual Studio が無い場合）

**Step 1（Git で GitHub に上げる）** がまだの場合は、初心者向けの詳細手順を [docs/git-step1-beginner.md](docs/git-step1-beginner.md) にまとめてあります。そちらを参照してください。

1. このリポジトリを **GitHub に push** する（またはフォークする）。
2. ブランチ名が **main** または **master** であれば、push 時に自動でビルドが走ります。  
   別ブランチの場合は、GitHub の **Actions** タブで「Build MSIX」ワークフローを **Run workflow** から手動実行できます。
3. ビルド完了後、同じ Actions の実行ページで **Artifacts** から **MSIX-x64** をダウンロードします。
4. ダウンロードした ZIP を解き、中にある **.msix** をダブルクリックしてインストールします。  
   （自己署名証明書で署名しているため、Windows が警告を出した場合は「詳細」→「実行」で進めます。）
5. PowerToys の Command Palette を開き、「Reload」→ **Reload Command Palette Extension** を実行すると **Obsidian** が表示されます。

ワークフローは `.github/workflows/build-msix.yml` で定義されています。`windows-latest` 上で .NET 8 と MSBuild を使って MSIX をビルドし、自己署名証明書で署名しています。

### 方法 B: Visual Studio 2022 でビルド

1. **Visual Studio 2022** をインストールし、次のワークロードを有効にします。
   - 「Windows 用のデスクトップ開発」
   - 「.NET デスクトップ開発」

2. `ObsidianCommandPalette.sln` を開き、**ビルド** → **ソリューションのデプロイ** でデプロイします。  
   （「デプロイ」が必須です。実行だけでは拡張が登録されません。）

3. PowerToys の Command Palette を開き、一覧の末尾付近に **Obsidian** が表示されていれば成功です。  
   表示されない場合は、Command Palette で「Reload」と入力し、**Reload Command Palette Extension** を実行してください。

## 設定（Vault）

初回は「ノートを検索して開く」「Obsidian コマンドを実行」で **Vault を設定してください** と出ます。

1. Command Palette で **「設定フォルダを開く」** を実行するか、次のフォルダを開きます。  
   `%LocalAppData%\ObsidianCommandPalette`

2. その中に `config.json` を作成（または編集）し、次の形式で Vault を指定します。

```json
{
  "Vaults": [
    {
      "Path": "C:\\Users\\YourName\\Documents\\MyVault",
      "Name": "MyVault"
    }
  ]
}
```

- **Path**: Vault フォルダのフルパス（`\` は `\\` でエスケープ）
- **Name**: Obsidian の URI で使う Vault 名（通常はフォルダ名。日本語可）

複数 Vault を登録することもできます。現状は **先頭の 1 件** が使われます（将来、切り替え対応の余地あり）。

## 使い方

- **ノートを検索して開く** … 一覧でクエリを入力すると、ファイル名・パスでフィルタされます。ノートを選ぶと Obsidian でそのノートが開きます。
- **Obsidian コマンドを実行** … プリセットのコマンドを選ぶと、Advanced URI 経由で Obsidian 内のコマンドが実行されます。  
  使いたいコマンドが無い場合は、`ObsidianCommandPage.cs` のプリセット一覧に `commandid` と表示名を追加できます。

## プロジェクト構成

- `ObsidianCommandPalette/` … メインプロジェクト
  - `ObsidianExtension.cs` … Command Palette に登録する拡張のエントリ
  - `ObsidianCommandsProvider.cs` … トップレベルのコマンド（ノート検索・コマンド実行・設定フォルダ）
  - `Pages/ObsidianNoteSearchPage.cs` … ノート検索（DynamicListPage）
  - `Pages/ObsidianCommandPage.cs` … コマンド一覧（ListPage）
  - `Commands/` … URI を開く・ノートを開く・Advanced URI 実行・設定フォルダを開く
  - `Config/` … Vault 設定の読み書き（複数 Vault 対応のデータ構造）

## 参考

- [Command Palette Extensibility](https://learn.microsoft.com/en-us/windows/powertoys/command-palette/extensibility-overview)
- [Obsidian URI](https://help.obsidian.md/Extending+Obsidian/Obsidian+URI)
- [Obsidian Advanced URI - Commands](https://vinzent03.github.io/obsidian-advanced-uri/actions/commands)
