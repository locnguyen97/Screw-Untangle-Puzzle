# IEC Game

## Empty Project

> Project để khởi tạo code các dự án game

* [naming-convention] - Quy chuẩn đặt tên
* [folder-structure] - Cấu trúc thư mục
* [project-structure] - Cấu trúc mẫu project

## [Project IEC Module] - git subtree cho code chung

[Project IEC Module]: <https://gitlab.com/iecgames/iec-unity-modules>
[naming-convention]: <Wiki/naming-convention.md>
[folder-structure]: <Wiki/assets-folder-structure.md>
[project-structure]: <Wiki/event-data-controller-template-project.md>

## Các giai đoạn sản xuất game
* Test CPI
* Tối ưu LTV
* Đẩy Monetize

## Tổ chức git branch
#### Các branch chính
* `develop` : nơi commit chuẩn bị cho release kế tiếp
* `master` : nơi commit chuẩn bị cho release hiện tại
* `release/aab` , `release/ios` : nơi commit build các bản release
#### Các nhóm branch phụ
* `sdk/` : dành cho maintain sdk riêng với android, ios
* `dev/` : dành cho riêng từng feature
* `hotfix/` : branch fix bug cho bản release
* `mkt/` : branch tính năng quay video ads

### Tạo Gitlab project mới bằng Discord
```
!create-git <Name of the project>
[Example] 
!create-git Ball Sort Puzzle
```

### Tạo Unity Cloud Build bằng Discord
```
!create-ucb <link-to-google-sheet-checklist>
[Example] 
!create-ucb https://docs.google.com/spreadsheets/d/132-mvGPajWw38ZuNMS6uPHisaw0H8jZsmapIdup42zc/edit#gid=927018210
```

### Quy trình giai đoạn Test CPI
1. Tạo project `gitlab`
2. Checkout branch `develop`
3. Commit hoàn thiện tính năng yêu cầu cho bản test CPI
4. Gắn `Facebook` SDK, gắn `Kochava` SDK, Dependency Resolver > Resolve project
5. Dùng tool gắn ID từ `checklist`
6. Config gắn `keystore` đã tạo bởi tool
7. Merge branch vào `release`
8. Tạo Unity cloud build trên branch `release/aab`

### Quy trình giai đoạn Tối ưu LTV
1. Checkout branch `develop`
2. Gắn `Applovin` SDK
4. Dùng tool gắn Applovin Ads ID, inter, banner, reward ID, Admob ID
5. Commit hoàn thiện các tính năng AB test (xem thêm Quy trình bổ sung tính năng mới)
6. Dùng tool update config AB test từ checklist AB Test
7. Merge branch vào `release`
8. Check Unity cloud build

### Quy trình giai đoạn Đẩy Monetize
1. Block push thẳng với branch `release`
2. Chỉnh sửa vào branch release phải qua merge request gửi tới dev phụ trách chính

### Quy trình merge branch vào release
1. Merge `develop` vào `master`
2. Merge `master` vào `release/aab`
(hoặc push lên gitlab tạo merge request)

### Quy trình bổ sung tính năng mới
1. Tạo branch `dev/feature-x`
2. Tạo code bật/tắt cho tính năng logic mới
3. Đặt test code bật tính năng trong `ABTestController`
4. Đề xuất Review bổ sung tính năng mới
5. Xóa test code bật tính năng trong `ABTestController`
5. Merge branch vào `develop`

### Quy trình review bổ sung tính năng mới
1. Kiểm tra code mới có block toggle bật/tắt với code cũ
2. Biến toggle có tên dạng `(bool) isHaveABC` hoặc `(int) versionABC`
3. Test tắt feature không ảnh hưởng logic cũ

### Quy trình fix bug release (Merge)
1. Checkout branch `master`
2. Commit fix bug
3. Merge `master` vào cả `develop` và `release`

### Quy trình fix bug release (Cherry-pick)
1. Checkout branch `develop`
2. Commit fix bug
3. Cherry-pick commit vào cả `develop` và `release`

### Checklist bug hay gặp
* Gameplay, UI bị sai khi ở tỉ lệ màn hình 16:9, 18:9,...
* Safe area
* Banner bottom Ads đè UI
* Show inter bị delay vào gameplay khiến click nhầm

Xem thêm tại IEC Dev Workflow
<https://docs.google.com/document/d/1ziseqnM_tzq4_2u9jZ8u17AQ83y8k_A9xG0E1CeOU_k/edit?usp=sharing>
