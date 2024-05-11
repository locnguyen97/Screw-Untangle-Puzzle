## Phương pháp Composite MonoBehaviour

### Mô tả

Phương pháp bám sát với thiết kế MonoBehaviour của Unity, trong đó mỗi GameObject là một kết hợp của nhiều Component. Mỗi Component là một script kế thừa MonoBehaviour, thực hiện một tính năng, hành động của GameObject, có thể đem lắp sang một GameObject khác hoặc tháo bỏ mà không gây lỗi.

### Ví dụ

Game platformer dạng Mario với 3 GameObject

* Player
* Enemy
* Ground

Một số Component

* `HitPoint.cs` : gắn vào Player, Enemy
* `SpriteEffect.cs` : hiệu ứng kiểu như nhấp nháy hình ảnh, có thể gắn vào cả Player, Enemy lẫn Ground

### Xử lý liên kết

Để Class A có thể gọi sang Class B thì có một số cách (giống Phương pháp OOP đơn giản)

* Sử dụng API có sẵn của Unity : `FindObjectOfType<T>()`, `GetComponent<T>()`, `Find(string name)`, `FindWithTag(string tag)`
* Class A có chứa public field của B để có thể kéo thả GameObject có B trên Inspector GameObject A
* Gọi qua static reference của Class B

### Ưu điểm

* Tương đối dễ code, dễ hình dung
* Có thể từ thiết kế theo OOP mà tách ra các Component
* Gắn trực tiếp với GameObject của Unity nên dễ debug qua Inspector
* Có thể tái sử dụng code từ việc dùng chung Component

### Nhược điểm

* Do vẫn phụ thuộc trực tiếp vào `MonoBehaviour` và API của Unity nên vẫn mang nhiều nhược điểm như Phương pháp OOP (cần link sang page OOP)
* Thường sẽ mất nhiều thời gian để xây dựng toàn bộ các GameObject đều là tổ hợp của Components, nên hay được xây dựng đan xen giữa OOP và Composite, hình thành thiết kế code không thống nhất.

### Đánh giá

* Thích hợp với xây dựng game prototype đơn giản trong thời gian ngắn nhưng có thể bóc tách Component cho việc tái sử dụng.