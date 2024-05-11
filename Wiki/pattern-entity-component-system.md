## Phương pháp Entity Component System

### Mô tả

Khá giống với thiết kế Composite, trong mô hình [Entity Component System](https://docs.unity3d.com/Packages/com.unity.entities@0.1/manual/ecs_core.html) (ECS), các Object (theo tư duy OOP) được coi như một Entity xác định, bao chứa nhiều dữ liệu là các Component. Những logic hành vi cho các nhóm Entity có điểm chung về Component được gọi là System.

Entity không có Class riêng ứng với Object trong thiết kế OOP, nên chỉ sử dụng 1 Class Entity chung cho việc chứa các Component. Tùy theo tổ hợp Component trên Entity mà sẽ có logic khác nhau cho Entity đó.

Có thể hiểu đơn giản thiết kế ECS là phân tách từ Object trong thiết kế OOP, làm riêng biệt phần dữ liệu và logic ra Component và System.

Mô hình ECS có thể được xây dựng nhờ framework có sẵn như [Entitas](https://github.com/sschmid/Entitas-CSharp), hoặc hệ thống [Unity ECS](https://docs.unity3d.com/Packages/com.unity.entities@0.1/manual/index.html) đang trong quá trình phát triển thử nghiệm.

### Ví dụ

```C#
var entity = context.CreateEntity();                 // class Entity chung
entity.AddComponent(new TransformComponent());		// thêm Transform Component
entity.AddComponent(new RotationComponent());        // thêm Rotation Component
entity.AddComponent(new RenderComponent());          // thêm Render Component

var rotateSystem = new RotateSystem();               // khởi tạo System cho logic Rotate
// nhóm Entity có thể Rotate
var canRotateGroup = context.GetGroup(TransformComponent, RotationComponent);

canRotateSystem.OnUpdate = () =>
{
    foreach (var e in canRotateGroup)
    {
        var transform = e.Get<TransformComponent>();
        var rotate = e.Get<RotationComponent>();
        // ... xử lý rotation cho transform
    }
};
```

### Xử lý liên kết

* Các Component chỉ thuần túy chứa dữ liệu, không chứa logic ảnh hưởng bởi bên ngoài
* Entity là Class định nghĩa chung, không phụ thuộc logic game
* Các System hoạt động dựa trên event từ hệ thống, từ Unity hoặc khi hệ thống Entity có sự thay đổi về Component. Logic của System chỉ tác động lên Component, không tác động vào System khác.

### Ưu điểm

* Nếu thiết kế hợp lý thì hệ thống gần như không có coupling, có thể thiết kế unit test trên từng System
* Với thiết kế Component càng tối giản thì càng dễ tái sử dụng lại code cho nhiều game khác nhau
* Dễ thích ứng với nhiều thay đổi trong thiết kế nội dung game
* Thiết kế ECS là phương pháp tổng quát có thể dùng cho nhiều game, có thư viện framework hỗ trợ trên hầu hết các nền tảng code, hoặc việc tự xây dựng framework cũng không quá phức tạp
* Có nhiều cách để tối ưu hiệu suất việc xử lý logic

### Nhược điểm

* Mất nhiều thời gian để nghiên cứu và áp dụng hợp lý
* Do coder phần lớn quen với cách làm OOP nên cần thời gian để thích nghi sang ECS
* Khó debug do Component và Entity đều thiết kế tách khỏi GameObject

### Đánh giá

* Thích hợp để đầu tư xây dựng code base cho những game phức tạp, có thể làm trong thời gian dài



