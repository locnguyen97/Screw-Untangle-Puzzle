## Phương pháp Dependency Injection

### Mô tả

Việc xử lý liên kết giữa các Class được thực hiện bởi một hệ thống trung gian. Hệ thống này cung cấp (Inject) object cho sự liên kết và ẩn việc khởi tạo các object này khỏi các Class. 

Có thể thực hiện hệ thống đơn giản sử dụng thiết kế Service Locator, hoặc sử dụng framework như [Zenject](https://github.com/modesttree/Zenject) cho Unity3D.

### Ví dụ

Game có 2 class

* `UIManager.cs` xử lý event click button
* `SoundManager.cs` xử lý bật âm thanh

UIManager cần liên kết gọi sang SoundManager để bật âm thanh khi click button. Liên kết này được lấy ra thông qua `ServiceLocator.Get<SoundManager>()` chứ không sử dụng các phương pháp liên kết trực tiếp hoặc qua API tìm kiếm như thiết kế OOP thông thường.

```c#
SoundManager m_Sound;
void Start()
{
    m_Sound = ServiceLocator.Get<SoundManager>();
}
```

Nếu sử dụng framework Zenject thì ở class UIManager chỉ cần khai báo

```c#
[Inject] SoundManager m_Sound;
```

`m_Sound` sẽ được Inject tự động bởi Zenject từ thiết lập Context trên Scene vào UIManager

### Xử lý liên kết

* Thực hiện hoàn toàn qua hệ thống trung gian
* Cách thức khởi tạo cho object (single hay new cho mỗi lần `Get()`), hay nạp từ object cho trước được thực hiện riêng ngoài các Class và thường vào lúc Awake Scene.

### Ưu điểm

* Liên kết giữa các Class được xử lý bởi trung gian nên giảm bớt sự phụ thuộc trực tiếp coupling
* Nếu các Class giao tiếp thông qua Interface thì có thể dễ dàng làm object giả Interface cho việc viết Unit Test
* Tách bớt sự phụ thuộc vào Unity nên có thể áp dụng cho cả những game engine khác

### Nhược điểm

* Áp dụng framework như Zenject có thể khiến thiết kế code tăng sự phức tạp, phải làm quen với thêm nhiều khái niệm như Factory, Memory Pools, Signals,...
* Việc tìm reference qua Interface phức tạp hơn tìm trực tiếp qua Class

### Đánh giá

* Có thể áp dụng để xây dựng một hệ thống tổng quát dùng chung khi code game trên mọi engine hay nền tảng
* Kết hợp với OOP đơn giản hay Composite cũng có thể mang lại nhiều hiệu quả nhờ giảm coupling