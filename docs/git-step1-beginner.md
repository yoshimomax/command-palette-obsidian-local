# Git でのやり方（初心者向け）— Step 1

このフォルダを GitHub に上げて、Actions でビルドするまでの **Step 1** を、できるだけ細かく説明します。

---

## Step 1 の目標

- 今のプロジェクトフォルダを **Git の管理下** にする
- 変更を **1 回目のコミット** として記録する
- **GitHub にリポジトリ** を作る
- そのリポジトリに **コードを push（送る）** する

---

## 事前に用意するもの

1. **Git** がインストールされていること  
   - 未インストールの場合: [Git for Windows](https://git-scm.com/download/win) をダウンロードしてインストール
2. **GitHub アカウント**  
   - 未作成の場合: [GitHub](https://github.com/) で「Sign up」から無料で作成

---

## 1-1. コマンドを打つ場所を開く

1. エクスプローラーで、このプロジェクトのフォルダを開く  
   （例: `C:\Users\あなたの名前\...\Obsidian for Command Pallet`）
2. フォルダの中の **何もないところ** を右クリック
3. **「ターミナルで開く」** または **「Git Bash  here」** を選ぶ  
   - どちらも無い場合は、**「パスをアドレスバーにコピー」** でパスをコピーし、  
     **スタートメニュー** から **「コマンドプロンプト」** や **「PowerShell」** を開いて、  
     `cd ` のあとにそのパスを貼り付けて Enter を押し、このフォルダに移動する

これで「このフォルダ」がカレントフォルダになった状態でコマンドを打てます。

---

## 1-2. このフォルダを Git のリポジトリにする

ターミナル（またはコマンドプロンプト / PowerShell）で、次のコマンドを **1 行ずつ** 打って Enter を押します。

```bash
git init
```

- **意味**: このフォルダを「Git で管理するリポジトリ」として初期化する
- **結果**: フォルダの中に `.git` という隠しフォルダができれば成功（中身を触る必要はありません）

---

## 1-3. 初回の Git 設定（名前とメール）

まだ Git を一度も設定していない場合だけ、次の 2 行を実行します（GitHub に表示される名前とメールです）。

```bash
git config --global user.name "あなたの名前"
git config --global user.email "あなたのGitHub用メールアドレス"
```

- 例: `git config --global user.name "Tanaka Taro"`
- 例: `git config --global user.email "taro@example.com"`
- **一度設定すれば**、他のプロジェクトでも同じ名前・メールが使われます。

---

## 1-4. 全部のファイルを「ステージング」する

次のコマンドを実行します。

```bash
git add .
```

- **意味**: このフォルダ以下の **全部のファイル** を「次のコミットに含める」と印をつける（ステージング）
- **「.」** = いまいるフォルダ全体

---

## 1-5. 1 回目のコミットを作る

次のコマンドを実行します。

```bash
git commit -m "Initial commit: Obsidian Command Palette extension"
```

- **意味**: ステージングした内容を、1 つの「コミット」として記録する
- **`-m "..."`** の部分は「このコミットの説明」なので、好きなメッセージに変えてかまいません  
  例: `git commit -m "初回コミット"`

ここまでで、「このフォルダの今の状態」が Git に 1 回分の履歴として保存されました。

---

## 1-6. GitHub で新しいリポジトリを作る

1. ブラウザで [GitHub](https://github.com/) にログインする
2. 画面右上の **「+」** をクリック → **「New repository」** を選ぶ
3. 次のように入力する：
   - **Repository name**: 例として `ObsidianCommandPalette` や `obsidian-command-palette`（好きな名前で OK）
   - **Description**: 空でもよい。例「PowerToys Command Palette extension for Obsidian」
   - **Public** を選ぶ
   - **「Add a README file」** や **「Add .gitignore」** は **チェックしない**（こちらのプロジェクトに既にあるため）
4. **「Create repository」** をクリックする

作成が終わると、「Quick setup」という画面になります。ここは **何もせず** 次の 1-7 に進みます。

---

## 1-7. 作った GitHub リポジトリの URL を確認する

GitHub のリポジトリ画面の **緑色の「Code」ボタン** をクリックすると、URL が表示されます。

- **HTTPS** の例: `https://github.com/あなたのユーザー名/ObsidianCommandPalette.git`
- この **HTTPS の URL** をコピーしておく（次のステップで使います）

---

## 1-8. リモートを追加して、main ブランチを push する

ターミナルに戻り、次のコマンドを **1 行ずつ** 実行します。

**1 行目**（`URL` のところを、1-7 でコピーした URL に置き換える）:

```bash
git remote add origin https://github.com/あなたのユーザー名/リポジトリ名.git
```

- **意味**: 「このリポジトリの先（リモート）」の名前を `origin`、URL を指定したアドレスにする

**2 行目**（ブランチ名を `main` にする場合）:

```bash
git branch -M main
```

- **意味**: いまのブランチ名を `main` に変える（GitHub の標準の「メインのブランチ」名）

**3 行目**（`origin` の `main` に push する）:

```bash
git push -u origin main
```

- **意味**: いまのコミットを、リモート `origin` の `main` ブランチに送る（初回は `-u` で紐づけ）
- **初回だけ**、GitHub の **ユーザー名** と **パスワード** を聞かれることがあります。  
  - パスワードには **Personal Access Token (PAT)** を使う必要があります（通常のログイン用パスワードでは push できない場合があります）。  
  - Token の作り方: GitHub → 右上のアイコン → **Settings** → 左の **Developer settings** → **Personal access tokens** → **Tokens (classic)** → **Generate new token** で、`repo` にチェックを入れて作成し、表示されたトークンをパスワードの代わりに入力する

ここまで成功すると、GitHub のリポジトリのページを更新すると、ファイル一覧が表示されます。

---

## まとめ（Step 1 の流れ）

| 順番 | やること           | コマンド例 |
|------|--------------------|------------|
| 1    | このフォルダでターミナルを開く | （エクスプローラーから開く） |
| 2    | Git リポジトリにする | `git init` |
| 3    | 名前・メール設定（初回だけ） | `git config --global user.name "名前"` など |
| 4    | 全部ステージング   | `git add .` |
| 5    | 1 回目のコミット   | `git commit -m "Initial commit: ..."` |
| 6    | GitHub で新規リポジトリ作成 | （ブラウザで操作） |
| 7    | リモートを追加     | `git remote add origin URL` |
| 8    | ブランチを main に | `git branch -M main` |
| 9    | 送る               | `git push -u origin main` |

---

## うまくいかないとき

- **「git は認識されていません」**  
  → Git をインストールしたあと、**ターミナルを一度閉じて開き直す**。まだ出る場合は PC を再起動する。

- **push で「Permission denied」や「Authentication failed」**  
  → GitHub の **Personal Access Token** を作り、パスワードの代わりにそのトークンを入力する。

- **「branch 'main' does not exist」**  
  → 先に `git commit` ができているか確認する。できていれば `git branch -M main` を実行してから `git push -u origin main` をもう一度試す。

Step 1 が終わったら、README の「方法 A」の **Step 2**（Actions でビルド → Artifacts から MSIX をダウンロード）に進めます。
