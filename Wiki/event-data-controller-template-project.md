## Cấu trúc mẫu project EDC

### Scene

`Game` là Scene khởi chạy mặc định : `Assets/_IEC/Scene/Game.unity`

Bao gồm

* `_StaticObjects` : GameObject mang script `StaticObjectsContainer`, sẽ được đặt `DontDestroyOnLoad`, nạp sẵn `ServicesController` và `DataController` chung cho mọi Scene
* `_Controllers` : GameObject mang script `EventController`, mang các phần tử con đều là Controller để nạp riêng cho Scene, thiết lập ban đầu có `CheatCodeController`
* `_SceneRoot` : GameObject sẽ được `EventController` tự động xử lý tìm các phần tử con theo interface `IGroup`
* `IEC Module Controller` : GameObject cho singleton không nằm trong EDC

### Event

Đảm nhiệm xử lý bởi `EventController` và các class định nghĩa danh sách Event

* `EventController` : nạp các controller và quản lý luồng của toàn bộ các Event
  * `EventController.RaiseEvent(string gameEvent, EventParam param)` static function được dùng để phát sinh Custom Event
  * `EventParam` struct định nghĩa param bổ sung cho Custom Event, có thông tin sender và 5 tham số tùy biến
* `EventList` : partial class gồm nhiều class con, mỗi class con là một nhóm định nghĩa các Custom Event dạng `const string`, với quy ước đặt tên phải khớp với nội dung `const string` sao cho không bị trùng
  * `EventService` : nhóm một số Event chung, phát sinh từ Service
  * `EventGamePlay` : nhóm định nghĩa Event phát sinh từ logic Game

### Data

Dữ liệu dạng GameObject trên Scene nếu được kéo làm child của `_SceneRoot` và có script theo interface `IGroup` thì sẽ được tự động nạp vào `Pool`.

Dữ liệu được bắt đầu load và nạp từ `DataController` thông qua `Pool.Set(dataObject)`. Dữ liệu mẫu gồm 3 class

* `Game.cs` thông tin liên quan session game, không cần phải lưu lại
* `PlayerSecret.cs` thông tin liên quan tiến trình người chơi, cần phải lưu lại
* `GameConfig.cs` thông tin dạng config được lưu dạng `ScriptableObject`

Có thể truy cập dữ liệu qua `Pool.Get<T>` với T là tên class dữ liệu.

### Controller

Mỗi class Controller có đặc điểm sau

* Kế thừa MonoBehaviour : để gắn với GameObject, giúp cho việc debug trên Inspector và `EventController` có thể tự động nhận diện, quản lý trình tự
* Interface `IController` : để `EventController` nhận diện và nạp vào Locator `Controllers`
* Interface `IEventAwake`, `IEventStart`, `IEventUpdate`, `IEventFixedUpdate`, `IEventPauseApp`, `IEventQuitApp` : để `EventController` gửi các Event của Unity tương ứng với tên interface
* Interface `IEventListener` : để `EventController` gửi Custom Event
* Các Event Interface có dùng chung function `bool IsActive()` để nhận biết Controller đang bật hay tắt
* Controller chỉ cần implement Event Interface nào cần thiết
* Ưu tiên thứ tự khi xử lý Event : trước hết là `ServiceController` xong đến `DataController`

### Service

Hệ thống Module tái sử dụng giữa các project. Các Module cùng vai trò đều có Interface chung để thay đổi.

* Data : lưu trữ qua PlayerPrefs, hỗ trợ mã hóa chống hack, hỗ trợ đọc remote config từ Firebase
* Advertise : interface chung cho nhiều module quảng cáo như Admob, Appodeal, Applovin, IronSource, hỗ trợ hiển thị ads tối ưu ecpm
* Analytic Tracking : interface chung cho nhiều module analytic như Facebook, Firebase, Kochava
* AB Test : hỗ trợ random AB test tại local
* In App Purchase : hỗ trợ qua Unity IAP
* Time Server : lấy thời gian qua Online API
* User Location : lấy địa chỉ quốc gia qua Online API
* Score : lưu trữ, cập nhật highscore offline

Các code phụ thuộc vào SDK có thể bật tắt thông qua Scripting Define Symbols ở Unity Player Setting 

```
PluginFirebase;PluginFirebaseAnalytic;PluginFirebaseRemote;PluginFacebook;PluginKochava;PluginApplovin;PluginUnityIAP;PluginIS;PluginAppodeal
```

* `PluginFirebase` : code khởi tạo chung cho Firebase
* `PluginFirebaseAnalytic` : code cho Firebase Analytic
* `PluginFirebaseRemote` : code cho Firebase Remote Config
* `PluginFacebook` : code cho Facebook SDK
* `PluginKochava` : code cho Kochava SDK
* `PluginApplovin` : code cho Applovin SDK
* `PluginAppodeal` : code cho Appodeal SDK
* `PluginIS` : code cho IronSource SDK
* `PluginUnityIAP` : code cho Unity In App Purchase

### Trình tự code tính năng mới

1. Xây dựng Data
   * Dữ liệu cho session chơi
   * Dữ liệu lưu cho session chơi sau
   * Dữ liệu config
   * Dữ liệu dạng GameObject trên Scene hay Prefab
2. Khởi tạo Data
   * Dữ liệu dạng GameObject trên Scene cần tập trung theo từng Group để giảm bước kéo thả khi duplicate, đồng thời Interface `IGroup` trong `_SceneRoot` sẽ được Locate tự động cho vào `Pool`
   * Dữ liệu các dạng còn lại nên tập trung nạp vào `Pool` từ `DataController` để đảm bảo dữ liệu luôn có trước khi Controller khác xử lý
3. Xây dựng Controller cho tính năng và các Custom Event cần thiết