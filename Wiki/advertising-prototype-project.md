# Tạo project prototype chạy test quảng cáo

1. Ghép với empty project

   * Clone Empty Project và đổi tên folder project sang tên Game prototype

   * Nếu viết code mới từ empty project
     * Phát triển project từ thư mục `_IEC`
   * Nếu ghép từ source ngoài
     * Test riêng project source ngoài xem có lỗi không, phải sử dụng phiên bản Unity bao nhiêu
     * Export source ngoài thành package và import vào empty project
     * Có thể phải đồng bộ empty project cho cùng phiên bản Unity với source ngoài
     * Gom resource, script, scene của source ngoài vào một folder có tên dạng `_IEC_[Tên game]`
     * Thêm các Scene của source ngoài vào `Build Settings`, test thử game có thể chạy bình thường không
   * Ghép Start Scene (với source ngoài)
     * Mở Scene khởi chạy Game (Start Scene)
     * Thêm prefab `_IEC/Prefab/_PrototypeControllers` vào scene
     * Tùy chỉnh `_Controllers` cho phù hợp với game: bỏ Test, bổ sung CheatCode, thêm Controller kết nối với code source ngoài
   * Xóa `Readme.md` và folder Wiki
   * Tạo `first commit` lưu lại kết quả ghép project

2. Cập nhật IEC Modules

   ```
   git subtree pull -P Assets/_IECModules git@gitlab.com:iecgames/iec-unity-modules.git master --squash
   ```

3. Đẩy project lên gitlab

   * Tạo git repo trên gitlab `iecgames/advertising-prototype`
   * Đổi remote origin URL của Empty Project đã ghép sang SSH URL của git repo mới
   * Tiến hành push lên repo

4. Thêm các plugin SDK

   * Facebook : [Mục 3 SDK Documentation](Wiki/SdkDocumentation.md)
   * Kochava : [Mục 7 SDK Documentation](Wiki/SdkDocumentation.md)
   * Applovin : [Mục 4 SDK Documentation](Wiki/SdkDocumentation.md)
   * Define Symbols : `PluginFacebook;PluginKochava;PluginApplovin`

5. Thêm các event Analytic

   Sử dụng `Controllers.Get<ServicesController>().PushEvent("event-name");`

6. Hiển thị quảng cáo

   Sử dụng và chỉnh sửa code từ `AdsController`

7. Cập nhật ID từ File Checklist

   * Tìm Menu `IEC -> Set ID SDK`
   * Paste link google sheet Check List chứa các ID của project
   * Ấn nút `Set SDK`
   * Tiến hành Force Resolver của `Assets -> Play Services Resolver -> Android Resolver`

8. Thiết lập AB Test

   * Sử dụng và chỉnh sửa code từ `ABTestController`

9. Tạo branch build AAB

   * Branch master đặt thiết lập sau để build nhanh apk test
     * Scripting Backend : Mono
     * Target Architectures : ARMv7
     * Publish Settings : debug key
   * Tạo branch `release-aab` với các thiết lập sau
     * Bỏ Unity open flash screen : mở `ProjectSettings/ProjectSettings.asset` lên bằng phần mềm text hoặc code, sửa `m_ShowUnitySplashScreen: 0` thành `m_ShowUnitySplashScreen: 1`
     * Scripting Backend : IL2CPP
     * Target Architectures : ARMv7 + ARM64
     * Publish Settings : tạo keystore theo tên game. Ví dụ game tên "Cut Master" có keystore là `cutmaster.keystore` với password là `cutmaster`, nếu tên quá ngắn thì thêm `123456`

10. Thiết lập Unity Cloud Build

    * [Unity Cloud Build Documentation](https://docs.google.com/document/d/1S_Zg0yQF_j4PlHPyJLaGSL27jkoOpvxDYiwJ4fdwt5c/edit)
    * Chỉ Setup vào Discord :
      * Tạo một Integration Discord vào channel `build-advertising-prototypes` với các event build started, failure và success
      * Tạo một Integration Discord vào channel `advertising-prototypes` với event build success
    * Config build apk test
      * Tên : `Dev Armv7`
      * Branch: `master`
      * Auto build: `Yes`
    * Config build aab
      * Tên: `Release AAB`
      * Branch : `release-aab`
      * Advanced : bật `Build App Bundles (.aab)`
      * Auto build: `Yes`

