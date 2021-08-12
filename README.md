# XFKudanARDemo

2021.8.12

### はじめに

2021年の夏休みの課題として、[KudanAR](https://www.xlsoft.com/jp/products/kudan/index.html) を試してみました。

Unity でも動作する 自身のUnity力 でゴールできる自信がなかったので、触ったことのある Xamarin.Forms で実装しています。

余計な変更を入れてない シンプルに動作するプロジェクトの作成方法を以下にメモります。



### 完成

![demo.gif](https://github.com/hsytkm/XFKudanARDemo/blob/master/demo.gif)



### 1. Xamarin.Forms でプロジェクトを作成する。

Android 以外使う予定ないけど、iOS, UWP も追加しといた。

**マーカー画像**

公式の [ここから](https://www.xlsoft.com/doc/kudan/files/2019/04/Kudan-Marker-Basics-Assets.zip) ダウンロードした。

ちなみに上記ダウンロードリンクは [ここ](https://www.xlsoft.com/doc/kudan/ja/android-marker-basics_jp/) から辿りついた。 

ダウンロードした画像 (2枚) は Android プロジェクトの `Assets` フォルダに登録した。 プロパティは `Android.Assets` , `コピーしない` にしておく。

### 2. Android バインド ライブラリの追加

メアド登録により取得したリンク (敢えて書いてない) から `Andoroid SDK` をダウンロードした。

1. `Jars` フォルダに `KudanAR.aar` ファイルを追加する。

2. ビルドアクションを `LibraryProjectZip` に設定する。

3. Android プロジェクト から 本プロジェクト を参照する。

4. この時点でビルドするとエラーになるので、Android プロジェクトの `AndroidManifest.xml` に `tools:replace="android:label"` を追加して対応する。

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

### 3. 各種ソース追加

**MarkerARActivity.cs**

Android プロジェクトに `MarkerARActivity.cs` を追加する。

公式 GitHub の [MarkerActivity.java](https://github.com/XLsoft-Corporation/Public-Samples-Android/blob/master/app/src/main/java/com/xlsoft/publicsamples/MarkerActivity.java) を参考に C# に移植した。

**Smapho.cs**

Android プロジェクトから パーミッション をチェックするための 静的クラス  を追加する。

**KudanARService.cs**

インターフェイス `IKudanARService.cs` は 共有プロジェクトに、クラス `KudanARService.cs` は Android プロジェクト に追加する。

共有クラスから使用する際は、DI を使ってインスタンスを取得する。

**MainPage(ViewModel).cs**

雰囲気で実装する。 必要ないけど ViewModel に `INotifyPropertyChanged` は実装しといた。（WPFではリークの原因なる）

### 4. ライセンスキー

1. [公式](https://www.xlsoft.com/doc/kudan/ja/development-license-keys_jp/) から取得して、 `MarkerARActivity.cs` 内に埋める。
2. 開発用ライセンスを使用する場合は、`AndroidManifest.xml` の `package` を変更する。 この時点では `com.xlsoft.kudanar1` だった。

### 参考

[Kudan ホーム : エクセルソフト](https://www.xlsoft.com/jp/products/kudan/index.html)

[The Kudan Developer Hub](https://www.xlsoft.com/doc/kudan/ja/home_jp/)

[Xamarin.FormsでKudan ARを試してみる - Qiita](https://qiita.com/takapi_cs/items/581654f38ddb06a8e81c)

### 確認環境

| NuGetライブラリ名   | バージョン |
| :------------------ | :--------- |
| Visual Studio 2019  | 16.11.0    |
| Xamarin.Forms       | 5.0.0.2083 |
| Xamarin.Essentials  | 1.7.0      |
| KudanAR Android SDK | 1.6.0      |
| Google Pixel 3      | Android 11 |




