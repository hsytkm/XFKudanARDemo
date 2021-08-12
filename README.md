# XFKudanARDemo

2021.8.12（夏休み1日目）

### はじめに

2021年の夏休みの課題として [KudanAR](https://www.xlsoft.com/jp/products/kudan/index.html) を試しましたのでメモっときます。

Unity でも動作するのですが、自身のUnity力 で完成まで持っていく自信がなかったので、少し触ったことのある Xamarin.Forms で実装しました。

本リポジトリでは、余計な変更を入れてない シンプルにライブラリを動作させています。

### 完成ソフトデモ

マーカー画像の傾きに応じて重畳画像 も変形しています。

また、画面の一部を写すと重畳画像の一部が表示されます。すごいですね！

![demo](https://github.com/hsytkm/XFKudanARDemo/blob/trunk/demo.gif)

### 対応作業

#### 1. Xamarin.Forms プロジェクトの作成

iOS, UWP プロジェクトも追加しときました。 Android 以外を実装する予定はないです。

**マーカー画像**

[公式のここから](https://www.xlsoft.com/doc/kudan/files/2019/04/Kudan-Marker-Basics-Assets.zip) ダウンロードしました。ちなみにダウンロードリンクは [公式 Developer Hub](https://www.xlsoft.com/doc/kudan/ja/android-marker-basics_jp/) から辿りつきました。

ダウンロードした画像 (2枚) は Android プロジェクトの `Assets` フォルダに登録します。 プロパティは `Android.Assets` , `コピーしない` にしておきましょう。

動作確認のため マーカー画像 を貼っときます。

![KudanMarker](https://github.com/hsytkm/XFKudanARDemo/blob/trunk/XFKudanARDemo/XFKudanARDemo.Android/Assets/KudanMarker.jpg)

#### 2. Android バインド ライブラリの追加

1. メアド登録により取得したダウンロードリンク (敢えてURL書いていません) から `Andoroid SDK` をダウンロードします。
2. `Jars` フォルダに `KudanAR.aar` ファイルを追加して、ビルドアクションを `LibraryProjectZip` に設定します。 **本リポジトリでは削除しています。**
3. Android プロジェクト から このプロジェクト を参照します。
4. この時点でビルドするとエラーになるので、Android プロジェクトの `AndroidManifest.xml` に `tools:replace="android:label"` を追加します。

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android"
          xmlns:tools="http://schemas.android.com/tools" <!-- ここ -->
          android:versionCode="1"
          android:versionName="1.0"
          package="com.companyname.xfkudanardemo">

    <uses-sdk android:minSdkVersion="21" android:targetSdkVersion="30" />
    <application android:label="XFKudanARDemo.Android"
                 android:theme="@style/MainTheme"
                 tools:replace="android:label" /> <!-- ここ -->

    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
</manifest>
```

めちゃ警告でるけど気にしない。

[.AAR のバインド - Xamarin | Microsoft Docs](https://docs.microsoft.com/ja-jp/xamarin/android/platform/binding-java-library/binding-an-aar)

#### 3. 各種ソースコードの追加

**MarkerARActivity.cs**

Android プロジェクトに `MarkerARActivity.cs` を追加します。

公式 GitHub の [MarkerActivity.java](https://github.com/XLsoft-Corporation/Public-Samples-Android/blob/master/app/src/main/java/com/xlsoft/publicsamples/MarkerActivity.java) を参考に C# に移植しました。

**Smapho.cs**

Android プロジェクトから パーミッション をチェックするための 静的クラス  を追加します。

**KudanARService.cs**

インターフェイス `IKudanARService.cs` は 共有プロジェクトに、クラス `KudanARService.cs` は Android プロジェクト に追加します。

共有クラスから使用する際は、DI を使ってインスタンスを取得します。

**MainPage(ViewModel).cs**

UI周りは雰囲気で実装します。 今回の実装では必要ないのですが、ViewModel に `INotifyPropertyChanged` を実装しておきました。（Xamarin.Forms に明るくないですが、WPF ではメモリリークの原因になるためです）

#### 4. ライセンスキーの設定

1. [公式](https://www.xlsoft.com/doc/kudan/ja/development-license-keys_jp/) から取得して  `MarkerARActivity.cs` 内のフィールドに設定します。 **本リポジトリでは削除しています。**
2. 開発用ライセンスを使用する場合は、`AndroidManifest.xml` の `package` を変更しましょう。 この作業の時点では `com.xlsoft.kudanar1` でした。~~DeveloperHub には  `com.xlsoft.kudanar` と書いてあったので少し詰まりました~~

### 参考リンク

[Kudan ホーム : エクセルソフト](https://www.xlsoft.com/jp/products/kudan/index.html)

[The Kudan Developer Hub](https://www.xlsoft.com/doc/kudan/ja/home_jp/)

[Xamarin.FormsでKudan ARを試してみる - Qiita](https://qiita.com/takapi_cs/items/581654f38ddb06a8e81c)

### 確認環境

| NuGetライブラリ名   | バージョン |
| :------------------ | :--------- |
| KudanAR Android SDK | 1.6.0      |
| Visual Studio 2019  | 16.11.0    |
| Xamarin.Forms       | 5.0.0.2083 |
| Xamarin.Essentials  | 1.7.0      |
| Google Pixel 3      | Android 11 |

EOF
