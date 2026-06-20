# Lộ trình triển khai Lab 2: Snow Boarder (Chuẩn chấm điểm 100/100)

Dự án được thiết kế chi tiết theo 7 giai đoạn để đáp ứng đầy đủ và tối đa điểm số của từng hạng mục trong file `lab2.md` (bao gồm cả phần UI, Vật lý, Logic điểm số nâng cao, và Thẩm mỹ).

---

## Giai đoạn 1: Khởi tạo dự án & Thiết lập Scene ban đầu (10 điểm)
* Tổ chức cấu trúc thư mục gọn gàng (`Scenes`, `Scripts`, `Sprites`, `Audio`, `Prefabs`, `Materials`).
* Nhập tài nguyên (Sprites, Audio, Scene `Level1`) từ thư mục `Lab2Assets` vào dự án.
* Thiết lập **Build Profiles** (Unity 6) gồm các Scene: `MainMenu`, `Level1`, và `OptionsScene` (hoặc tích hợp Panel Options).
* Tạo hạt tuyết rơi (**Dynamic Weather - Falling Snow**) bằng **Unity Particle System** phủ toàn bộ bản đồ để đáp ứng tiêu chí Thẩm mỹ.

## Giai đoạn 2: Thiết kế Đường trượt & Vật lý Địa hình (Slope Physics - 15 điểm)
* Sử dụng package **2D Sprite Shape** để thiết kế địa hình dốc tuyết uốn lượn nhấp nhô, bổ sung các bệ phóng (ramps, jumps) và chướng ngại vật (rocks, trees).
* Gắn `EdgeCollider2D` và **`Surface Effector 2D`** lên dốc tuyết để giả lập:
  * Lực đẩy đẩy ván trượt về phía trước theo hướng dốc.
  * Quán tính và động lượng (Momentum) khi lao dốc và bay lên không trung.
  * Lực ma sát dốc tuyết (Friction) thông qua Physics Material 2D trơn trượt.

## Giai đoạn 3: Thiết lập Vật lý Người chơi & Điều khiển nhạy bén (20 điểm)
* Đưa nhân vật trượt tuyết vào cảnh chơi, cấu hình `Rigidbody2D` (trọng lực, khối lượng, lực cản không khí).
* Thiết lập 2 Collider trên người chơi để xử lý các va chạm khác nhau:
  * Ván trượt: Gắn `CapsuleCollider2D` (hoặc BoxCollider2D) với Physics Material 2D trơn tru để trượt mượt mà.
  * Đầu nhân vật: Gắn `CircleCollider2D` để phát hiện va chạm ngã (Crash) khi tiếp đất lỗi.
* Viết script `PlayerController.cs`:
  * Xoay người nhạy bén (Trái/Phải hoặc A/D) khi đang bay để biểu diễn nhào lộn (Tricks).
  * Điều khiển tốc độ chủ động (tăng tốc/giảm tốc bằng phím mũi tên Lên/Xuống hoặc Space/S).
* Tạo hiệu ứng bụi tuyết bắn ra từ ván trượt (Snow Dust Trail) bằng Particle System, lập trình bật hiệu ứng khi tiếp đất và tắt khi bay trên không.

## Giai đoạn 4: Thu thập vật phẩm & Vùng kích hoạt nâng cao (Triggers & Power-ups - 15 điểm)
* Tạo Prefab bông tuyết thu thập (`Snowflake`) có gắn Collider ở chế độ `Is Trigger` để cộng điểm khi ăn.
* Tạo các vật phẩm/vùng trigger Power-ups:
  * **Speed Boost (Tăng tốc):** Tăng mạnh lực trượt trong thời gian ngắn.
  * **Invincibility (Bất tử):** Bảo vệ người chơi không bị ngã (Crash) khi đụng chướng ngại vật hoặc đập đầu xuống tuyết trong 5-10 giây.
* Bố trí các chướng ngại vật (Cây `Snow-Tree`, Đá `Snow-Rock`):
  * Khi va chạm thông thường (không phải chạm đầu) sẽ làm giảm tốc độ hiện tại của người chơi.

## Giai đoạn 5: Hệ thống Điểm số, Nhào lộn (Tricks) & Combo Multiplier (20 điểm)
* Viết logic phát hiện nhào lộn (Trick Detection):
  * Đo góc xoay của người chơi khi ở trên không. Nếu hoàn thành một vòng xoay 360 độ thành công và tiếp đất an toàn (không bị ngã), tính là 1 cú "Flip/Trick" thành công.
* Thiết lập hệ thống tính điểm động (Dynamic Score System):
  * Điểm số = Điểm tốc độ (trượt càng nhanh điểm càng tăng theo thời gian) + Điểm bông tuyết thu thập + Điểm thực hiện tricks.
* Thiết lập hệ thống **Combo** và **Hệ số nhân (Multiplier)**:
  * Thực hiện thành công liên tiếp các tricks/ăn bông tuyết sẽ tăng hệ số nhân (x1, x2, x3...).
  * Nếu bị ngã (Crash), hệ số nhân combo sẽ bị reset về x1.

## Giai đoạn 6: Hệ thống mạng (Lives) & Xử lý va chạm tai nạn
* Thiết lập cơ chế Mạng chơi (Player Lives, mặc định 3 mạng):
  * Chạm đầu (Crash) hoặc đâm chướng ngại vật nặng sẽ trừ đi 1 mạng.
  * Nếu còn mạng: Hồi sinh (Respawn) người chơi tại điểm checkpoint gần nhất trên dốc tuyết để tiếp tục.
  * Nếu hết mạng (Lives = 0): Chuyển màn hình Game Over.
* Phát các âm thanh tương ứng: `Crash SFX` khi ngã, `Finish SFX` khi chạm vạch đích để chiến thắng.

## Giai đoạn 7: Giao diện UI & Báo cáo hoàn thiện (HUD & Menus - 10 điểm)
* Thiết kế scene **`MainMenu`** với 3 nút chức năng:
  * **Start:** Vào màn chơi chính.
  * **Options:** Bảng điều chỉnh âm lượng hoặc cài đặt cơ bản.
  * **Quit:** Thoát ứng dụng.
* Thiết kế **In-game HUD** trong màn chơi chính:
  * Hiển thị điểm số hiện tại (Score) & Hệ số nhân (Multiplier).
  * Hiển thị tốc độ hiện tại (Speed).
  * Hiển thị số mạng còn lại (Lives).
  * Hiển thị thời gian/thanh trạng thái Power-up (nếu đang kích hoạt).
* Viết tài liệu báo cáo ngắn gọn (Quyết định thiết kế, khó khăn giải quyết, tính năng mở rộng) phục vụ nộp bài chấm điểm.