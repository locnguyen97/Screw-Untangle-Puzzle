## Phương pháp hướng đối tượng đơn giản


### Mô tả
Phương pháp được Unity3D hướng dẫn cho người mới tiếp cận, gần sát với những kiến thức về lập trình hướng đối tượng (*OOP*). Các đối tượng trong Game được coi như Object và có Script cho Class đối tượng đó. Class đối tượng kế thừa từ `MonoBehaviour` và gắn với GameObject tạo ra trên Scene hoặc Prefab.

### Ví dụ

Game platformer dạng Mario với 2 Class cơ bản

* `Character.cs` (có thể kế thừa ra Player và Enemy)
* `Ground.cs` 

### Xử lý liên kết

Để Class A có thể gọi sang Class B thì có một số cách

* Sử dụng API có sẵn của Unity : `FindObjectOfType<T>()`, `GetComponent<T>()`, `Find(string name)`, `FindWithTag(string tag)`
* Class A có chứa public field của B để có thể kéo thả GameObject có B trên Inspector GameObject A
* Gọi qua static reference của Class B

### Ưu điểm

* Rất dễ code, dễ hình dung
* Gắn trực tiếp với GameObject của Unity nên dễ debug qua Inspector

### Nhược điểm

* Các sự kiện xảy ra được xử lý riêng rẽ ở từng `MonoBehaviour` với thứ tự không biết trước. Ví dụ function sự kiện `Start` có thể là A chạy trước B trên Android nhưng A lại chạy sau B trên iOS, nên có thể phát sinh bug riêng chỉ trên một số thiết bị.
* Vấn đề về hiệu năng theo bài viết trên [Unity Blogs](https://blogs.unity3d.com/2015/12/23/1k-update-calls/)
* Sử dụng API như `FindObjectOfType<T>()` bị phụ thuộc vào trạng thái active của GameObject
* Liên kết code qua việc kéo thả giữa 2 GameObject sẽ phát sinh thêm quá nhiều kéo thả khi phát triển thêm nhiều code. Đồng thời việc liên kết kéo thả được lưu lại theo file Scene (hay Prefab) nên khó theo dõi qua git changes.
* Tính năng có chung của 2 Class A, B sẽ cần phải cho kế thừa từ Class Parent để không phải trùng lặp code. Nhưng việc kế thừa sẽ gây phức tạp cho việc mở rộng tính năng. Tham khảo [Component Based Architecture](https://www.raywenderlich.com/2806-introduction-to-component-based-architecture-in-games).
* Gọi qua static reference thì dễ phát sinh coupling rối rắm nếu không có thêm design pattern tốt.
* Cách xây dựng hệ thống Class và GameObject phụ thuộc kinh nghiệm và quan điểm từng cá nhân, thậm chí theo từng loại game khác nhau. Vậy nên việc đọc code của người khác có thể mất nhiều thời gian.
* Hệ thống Class và GameObject thường chỉ phù hợp với thiết kế tính năng game ban đầu. Khi game cần thêm nhiều tính năng phức tạp có thể sẽ rất khó thực hiện, có thể phải code lại nhiều thành phần.
* Hệ thống xây dựng xoay quanh Object và có nhiều liên kết thì khi bổ sung tính năng mới có thể phải chỉnh sửa rất nhiều Object liên quan và phát sinh bug.
* Do phụ thuộc trực tiếp vào hệ thống event và API của Unity nên khó xây dựng [unit test](https://viblo.asia/p/unit-test-la-gi-loi-ich-va-nhung-luu-y-khi-viet-unit-test-YWOZr20NZQ0).

### Đánh giá

* Có thể khắc phục một số nhược điểm với một số design pattern như Singleton, Service Locator nhưng không tạo được phương pháp tổng quát.
* Thích hợp với xây dựng game prototype đơn giản trong thời gian ngắn.