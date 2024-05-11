## Phương pháp Event Data Controller

### Mô tả

Thiết kế Event Data Controller (EDC) gồm 3 thành phần

* Event : hệ thống nhận và điều chuyển các event bao gồm event Unity và event tự định nghĩa
* Data : các Class mang vai trò dữ liệu có thể đọc hoặc ghi, chứa code mô  tả dữ liệu và logic thao tác biến đổi dữ liệu
* Controller : nơi lắng nghe các event tùy theo tính năng đảm nhiệm, tương tác với Data

Mô hình các Class trong Data có thể linh hoạt theo nhiều mô hình như OOP hoặc Composite, tùy theo mức độ phức tạp của dự án.

### Ví dụ

Giả sử hệ thống gồm

* `GoldController.cs` : controller xử lý tính năng liên quan tới Gold
* `PlayerData.cs` : dữ liệu của người chơi, chứa số gold hiện có
* `GoldUI.cs` : dữ liệu scene, chứa text UI hiển thị số gold người chơi có
* `EventController.cs` : controller đặc biệt phụ trách nhận và điều chuyển event
* `Pool.cs` : class đặt biệt phụ trách vai trò như database

Tính năng cần thực hiện : sau khi người chơi mua InAppPurchase thành công, số tiền người chơi đã được tăng lên thì cần cập nhật UI hiển thị số gold mới.

Luồng xử lý : 

* Event phát sinh tự định nghĩa là `UPDATE_UI_GOLD` được `EventController` nhận và gửi lại cho toàn bộ các Controller. 
* `GoldController` nhận và kiểm tra thấy event là `UPDATE_UI_GOLD`, thì sẽ lấy từ `Pool` ra `PlayerData` và `GoldUI`, tiến hành cập nhật `GoldUI` theo `PlayerData`

### Xử lý liên kết

* Giả sử có 2 tính năng được thực hiện bởi 2 controller A và B. Nếu theo thiết kế OOP thông thường thì A sẽ cần liên kết tới B chỉ khi A cần báo event cho B hoặc do B giữ data mà A không có. Việc sử dụng hệ thống Event sẽ giải quyết việc A cần báo event cho B. Việc loại bỏ dữ liệu ra khỏi controller cho vào database mà cả A, B đều truy cập được sẽ giải quyết nốt toàn bộ lý do để A cần liên kết với B.

* Các Data đều có thể truy cập qua một Class có vai trò Service Locator là `Pool` với function `Pool.Get<T>()`
* Các Controller đều có thể truy cập qua Service Locator là `Controllers` với function `Controllers.Get<T>()` nhưng chỉ cần thiết cho Controller có vai trò Service dùng chung
* Một số Service dùng chung nhiều game như Advertise, Analytic,... sẽ được gọi qua `ServiceController`

### Ưu điểm

* Tương tự như MVC nhưng có Data đại diện chung cho Model lẫn View nên linh hoạt hơn để phát triển các tính năng
* Data có thể thiết kế cả theo OOP hoặc Composite nên dễ thay đổi tùy theo độ phức tạp của game
* Các event liên quan đến Unity được đưa về tập trung tại `EventController` nên có thể nắm được trình tự cố định thực thi khi xảy ra các event
* Thiết kế it phức tạp so với Entitas hay Zenject
* Tùy theo thiết kế Data mà có thể viết Unit Test
* Thiết kế tầng Data có thể linh hoạt phù hợp với nhiều mức độ dự án
* Mô hình EDC có thể áp dụng sang cả những nền tảng phát triển game khác

### Nhược điểm

* Chưa được áp dụng và chứng minh hiệu quả với nhiều dự án game lớn
* Chưa thấy mô hình tương tự ở các nền lập trình khác như web, server để so sánh

### Đánh giá

* Thích hợp để đầu tư xây dựng code base cho dự án game từ nhỏ tới mức tương đối lớn