## Phương pháp Model View Controller

### Mô tả

Dựa trên [định nghĩa](https://viblo.asia/p/doi-dieu-ve-mo-hinh-mvc-E375z0vJZGW) thường được sử dụng trong lập trình Web, Model View Controller hay còn gọi tắt là mô hình MVC, trong đó các Class được xây dựng là 1 trong 3 thành phần

* **Model** : chứa các giá trị dữ liệu và logic liên quan
* **View** : phụ trách hiển thị dữ liệu, nhận tương tác từ người dùng
* **Controller** : nơi tiếp nhận những sự kiện rồi xử lý Model, đưa ra dữ liệu cần thiết qua View

MVC được sử dụng nhiều trong lập trình Web với Model gắn liền với tầng Database và View gắn với nội dung trang Web HTML, các Controller xử lý theo các web request đến từ người dùng. Tuy nhiên trong lập trình Game thì MVC không có lý thuyết ứng dụng hay framework chuẩn mực.

### Ví dụ

Game có một số class như sau

* `PlayerModel.cs` : class có `int gold` là số tiền hiện tại của người chơi
* `PlayerView.cs` : class phụ trách cập nhật hiển thị UI Text số gold hiện tại
* `PlayerController.cs` : class tiếp nhận sự kiện tiêu gold hoặc nhận thêm gold, thực hiện thay đổi số gold và báo cho `PlayerView` cập nhật

### Xử lý liên kết

* Vẫn có thể dùng kế thừa kết hợp MonoBehaviour nên có thể dùng liên kết như OOP
* Sử dụng Dependency Injection
* Tương tự các user request trong lập trình Web, có thể sử dụng hệ thống Event Message để liên kết View và Controller

### Ưu điểm

* Trình tự xử lý rõ ràng theo từng thành phần
* Tạo mô hình chuẩn dễ làm quen khi bổ sung nhân sự dự án
* Phương pháp làm có thể dùng lại cho nền tảng code khác
* Không quá phức tạp để thiết kế như ECS

### Nhược điểm

* Trong Game thì sự khác biệt giữa Model và View thường không rõ ràng, nhiều khi View không đơn giản chỉ là thành phần UI, mà có thể là hiệu ứng, âm thanh đi kèm. Khi phát triển tính năng dễ phát sinh việc phải tách Model ra từ View, gây nhiều rắc rối cho thiết kế code.
* Model thường được xây dựng theo hướng OOP nên có thể gặp những khó khăn mà thiết kế Composite mới giải quyết ổn thỏa.

### Đánh giá

* Có thể áp dụng để xây dựng một hệ thống tổng quát dùng chung khi code game trên mọi engine hay nền tảng
* Có thể sử dụng làm những game với lộ trình phát triển lâu dài hơn mức demo nhưng không thể quá phức tạp