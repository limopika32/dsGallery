# dsGallery
金沢工業大学 電子計算機研究会向けの展示アプリ<br>
2024年工大祭「eXceed」にて展示したものになります

## 開発仕様
+ .Net Desktop 8.0
+ C# (Win UI 3)
なんだかんだで 約2ヶ月 ぐらい掛かりました

## 注意事項
+ Surface Pro などの **外部メモリーカードにアクセスする事を前提に** 作成しています
+ [アプリケーション] タブで各種アプリを動作させるためには **管理者権限で起動しないと落ちます**
+ 各種 `.json` ファイルは必要項目を正しく記述してください
### 動作確認環境
+ Surface Pro 5 (Model: 1796)
+ microSDカード (Sandisk Extreme 64GB)

## ファイル
:warning: **制約**
+ 各種パスを記述するときは、そのディレクトリをルートとして `/sample.png` のように記述します
+ 任意の項目でも、省略せずに空の文字列を `"icon" : ""` のように記述します
+ `description` などに改行が必要な時は エスケープ `\n` を利用してください

### ファイル構造
```
/ (root)
|
- appli/
| - app1/
| | - view.json
| | - (exe etc.)
| | 
| - app2/ ...
| 
- music/
| - jackets/
| | - jacket1.png ...
| |
| - mview.json
| - music1.mp3
| - music2.wav ...
|
- illust/
| - iview.json
| - illust1.png
| - illust2.jpg ...
|
- system/
```

### アプリケーション (appli)
+ **1アプリ(またはプロジェクト)毎** に **1ディレクトリ(フォルダ)** が必要です
+ 同ディレクトリに 以下の仕様を満たした `view.json` を配置してください
```
{
    "author": "制作者名",
    "title": "タイトル",
    "icon": "アプリのアイコンパス(任意)",
    "thumbnails":[
        "サムネイル画像パス(任意、複数可能)"...
    ]
    "execPath": "実行パス(任意)",
    "description": "説明"
}
```
+ `icon` が用意されていないときは noimage のアイコンが表示されます
+ `execPath` が用意されていないときは 紹介ページのみ閲覧可能になります


### 音楽 (music)
+ **1曲毎** に **1ファイル** が必要です
+ 同ディレクトリにある全ファイル( `mview.json` 除く) 全てリストアップするため、ジャケットなど音楽ファイル以外のものは `/jackets/` などのサブディレクトリに移動し、後述する `mview.json` で指定を行ってください。
+ 同ディレクトリに 以下の仕様を満たした `mview.json` を1つ配置してください (任意)
+ `mview.json` が用意されていないときは 各ファイル名 が読み込まれます
```
{
    "contents":[
        {
            "author": "制作者名",
            "title": "タイトル",
            "jacket": "ジャケットのファイルパス(任意)",
            "path": "ファイルパス",
            "description": "説明"
        },
        {...} ...(名前の表示変更を行いたい数だけ繰り返す)
    ]
}
```
+ `jacket` が用意されていないときは noimage のアイコンが表示されます
+ `.mp3` などでの**組み込みのジャケットは読み込めません**
+ `path` が用意されていないときは名前等の表示変更ができません
+ 一応 MVなどで動画も読み込むことができます

### イラストレーション (illust)
+ **1枚毎** に **1ファイル** が必要です
+ 同ディレクトリに 以下の仕様を満たした `iview.json` を1つ配置してください (任意)
+ `iview.json` が用意されていないときは 各ファイル名 が読み込まれます
```
{
    "contents":[
        {
            "author": "制作者名",
            "title": "タイトル",
            "path": "ファイルパス",
            "description": "説明"
        },
        {...} ...(名前の表示変更を行いたい数だけ繰り返す)
    ]
}
```
+ `path` が用意されていないときは名前等の表示変更ができません

## 設定 について
+ 隠し設定用に作っていたページですが、利用する機会はほとんど無いです
  + kiosk用のサインアウトボタン
  + 外部メモリーカードのリマウントボタン
+ ソースコードを改変して利用するなど、適宜変更して使用してください

## ライセンス (LICENCE)
このリポジトリは [MITライセンス](./LICENSE) に準拠しています。<br>
Released under the [MIT license.](./LICENSE)