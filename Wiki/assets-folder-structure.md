### Cấu trúc thư mục Assets
* **_IEC** : thư mục riêng để tách biệt với các plugin khác
  * *Config* : dữ liệu thiết lập tĩnh, dạng file ScriptableObject
  * *Image* : dữ liệu đồ họa dạng ảnh
  * *Model3D* : dữ liệu đồ họa 3D, mesh + material đi kèm
  * *Prefab* : các file prefab tạo ra từ Unity
  * *Resources* : dữ liệu sử dụng qua script `Resources.Load`
  * *Scene* : các file scene tạo ra từ Unity
  * *Script* : chứa các file code
* **_IECModules** : git subtree từ `https://gitlab.com/iecgames/iec-unity-modules`
* **Thư mục khác** : plugins, SDK từ bên ngoài

Tùy theo độ phức tạp của assets mà sẽ cần phân loại thêm thư mục trong folder **_IEC**.

### Cấu trúc thư mục _IEC/Script

* Config : code file ScriptableObject
* Controller : code các Controller, có thể sử dụng qua `Controllers.Get`
* Data : code các object có thể coi là data, sử dụng qua `Pool.Get`
* Editor : script sử dụng Unity Editor, Unit Test
* Enum : các file định nghĩa Enum
* Event : các file định nghĩa string event