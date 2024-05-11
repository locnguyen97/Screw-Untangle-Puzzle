Thứ tự thực hiện việc thêm SDK vào một project :

`1 - (3,4,6,7,8) - 11 - 12`

Khi thêm một SDK mới vào project đã có sẵn các sdk khác, sau khi thêm sdk cần thực hiện lại bước 12 (nếu SDK có id thì phải thực hiện lại bước 11).

**1. External Dependency Manager:**

* `IEC - Set ID SDK - External Dependency Manager`

**3. Facebook SDK:**

* `IEC - Set ID SDK - Facebook SDK`

**4. Applovin SDK**

* `IEC - Set ID SDK - Applovin SDK`

* Với project Prototype: 

  Import các ad network giống các game trước (**không import Admob và Google Ad Manager**) trừ khi có yêu cầu riêng của bên monetizer.

* Với project chính thức:

  Import các ad network giống các game trước trừ khi có yêu cầu riêng của bên monetizer.

**5. IronSource SDK** (Deprecated)

* Link download và document: https://developers.ironsrc.com/ironsource-mobile/unity/unity-plugin/

* Với project Prototype: 

  Import các ad network giống các game trước (**không import Admob và Google Ad Manager**) trừ khi có yêu cầu riêng của bên monetizer.

* Với project chính thức:

  Import các ad network giống các game trước trừ khi có yêu cầu riêng của bên monetizer.

* Đọc kĩ và làm đầy đủ hướng dẫn trong document Iron Source về từng ad network.
* Thêm define `PluginIS`. (**)
* Thêm ad id vào file `ServicesConfig`.(*)
* **Important**: Với admob mediation của Iron Source bắt buộc phải sử dụng AndroidX. Đọc kĩ document Iron Source về admob mediation và các mục liên quan đến AndroidX ( mục 9. Multidex và mục 10. Jetifier).

**6. Firebase SDK**

* `IEC - Set ID SDK - Firebase Analytics (Firebase Remote Config)
* Liên hệ PM để biết cần import các plugin nào (Analytic, RemoteConfig).
* Thêm file config firebase vào project (Liên hệ PM để lấy link các file config).

**7. Kochava SDK**

* `IEC - Set ID SDK - Kochava SDK`

**8. Unity Iap SDK** (Coming soon)

**9. Multidex** (With AndroidX) (Deprecated)

* Thêm vào thẻ `application` trong file `AndroidManifest`:
  * TH **không** sử dụng Android X:`android:name="android.support.multidex.MultiDexApplication"`.
  * TH **có** sử dụng Android X: `android:name="androidx.multidex.MultiDexApplication"`.
* Thêm `multiDexEnabled true` vào mục `android - defaultConfig` trong file `mainTemplate.gradle`
* Thêm  vào mục `dependencies` trong file `mainTemplate.gradle`:
  * TH **không** sử dụng Android X: `implementation 'com.android.support:multidex:1.0.3'`.
  * TH **có** sử dụng Android X: `implementation 'androidx.multidex:multidex:2.0.1'`.

* Tài liệu tham khảo: https://developer.android.com/studio/build/multidex#kotlin

**10. Jetifier:** (Deprecated)

* `Assets - Play Service Resolver - Android Resolver - Setting`.
* TH **không** sử dụng AndroidX: Bỏ tick `Use Jetifier`.
* TH **có** sử dụng AndroidX: Tích `Use Jetifier`.
* Sau khi tick hay bỏ tick, nhấn OK và chạy resolver.

**11. Điền SDK ID từ checklist**

* Điền link checklist vào mục `IEC - Set ID SDK - Update Project Checklist`
* Bấm nút `Update from Google Docs`

**12. Update Manifest, Main Template and Android Resolve**
* `IEC - Set ID SDK - Update Main Template and Android Resolve`