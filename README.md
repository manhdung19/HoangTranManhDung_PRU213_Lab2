# Snow Boarder 2D - Unity Project (PRU213 - Lab 2)

Chào mừng bạn đến với **Snow Boarder 2D**, một dự án game thể thao mạo hiểm 2D được xây dựng bằng công cụ Unity (sử dụng Unity 6). Người chơi sẽ nhập vai vào nhân vật Barry trượt tuyết vượt qua các dốc tuyết uốn lượn đầy thử thách, thực hiện các kỹ năng nhào lộn trên không trung và thu thập vật phẩm để đạt điểm số cao nhất.

---

## 🎮 Cách chơi & Hệ thống điều khiển

Khi đang chơi trên màn hình Game, sử dụng các phím sau để điều khiển nhân vật Barry:

*   **Mũi tên Trái (`Left Arrow`) / Phím `A`:** Xoay ngược chiều kim đồng hồ (Lộn nhào về phía sau khi bay trên không).
*   **Mũi tên Phải (`Right Arrow`) / Phím `D`:** Xoay theo chiều kim đồng hồ (Lộn nhào về phía trước khi bay trên không).
*   **Mũi tên Lên (`Up Arrow`) / Phím `Space`:** Tăng tốc độ trượt tuyết (Boost).
*   **Mũi tên Xuống (`Down Arrow`) / Phím `S`:** Phanh giảm tốc độ trượt tuyết.

---

## ✨ Tính năng chính nổi bật

1.  **Vật lý trượt tuyết mượt mà:** Địa hình uốn lượn sử dụng *2D Sprite Shape* kết hợp `SurfaceEffector2D` và vật liệu trơn trượt để mang lại cảm giác lướt tuyết chân thực.
2.  **Hiệu ứng Bụi tuyết & Tác động thời tiết:** 
    *   Hạt tuyết rơi (`FallingSnow`) di chuyển động theo camera.
    *   Hiệu ứng bụi tuyết (`Dust Particles`) phun ra chân thực sau ván trượt và tự động tắt khi bay lên không trung.
3.  **Hệ thống kỹ năng nhào lộn (Trick System):** Tự động đo góc xoay Z của nhân vật khi bay trên không. Hoàn thành vòng lộn 360 độ và tiếp đất an toàn sẽ được cộng điểm kỹ năng lớn và tăng hệ số nhân combo.
4.  **Vật phẩm Power-ups phong phú:**
    *   **Bông tuyết (Snowflake - Màu xanh):** Cộng điểm trực tiếp nhân với combo hiện tại.
    *   **Tăng tốc (Speed Boost - Màu vàng):** Tăng tốc độ cực đại trong 3 giây.
    *   **Bất tử (Invincibility - Màu vàng kim):** Giúp Barry miễn nhiễm hoàn toàn va chạm chướng ngại vật thường và không bị ngã khi chạm đầu trong 5 giây (nhân vật đổi màu vàng sáng nhấp nháy).
5.  **Hệ thống mạng chơi & Checkpoint thông minh:**
    *   Người chơi khởi đầu với 3 mạng.
    *   Va chạm đập đầu xuống đất sẽ bị tính ngã (Crash), phát âm thanh ngã và giảm đi 1 mạng.
    *   Hệ thống checkpoint tự động lưu lại vị trí và hồi sinh Barry ở phía trên checkpoint 1.5 mét để tránh kẹt vật lý dưới tuyết.
6.  **Giao diện hoàn chỉnh (UI & HUD Canvas):**
    *   **Menu chính (MainMenu):** Chứa nút Bắt đầu, Cài đặt và Thoát game.
    *   **Bảng điều khiển HUD:** Hiển thị điểm số, hệ số combo, vận tốc thực tế (đã quy đổi sang km/h) và số mạng chơi còn lại.
    *   **Màn hình báo thua (Game Over Panel):** Xuất hiện khi hết 3 mạng, hỗ trợ chơi lại nhanh (Restart) hoặc quay về Menu chính.

---

## 🛠️ Yêu cầu cài đặt & Chạy dự án

1.  Mở thư mục dự án bằng **Unity Hub** (khuyên dùng phiên bản Unity 6 hoặc các bản Unity 2022+ mới nhất).
2.  Đảm bảo các gói package hỗ trợ sau đã được cài đặt (thông qua *Window > Package Manager*):
    *   **Cinemachine** (Quản lý camera bám theo nhân vật).
    *   **2D Sprite Shape** (Tạo hình dạng dốc tuyết).
3.  Truy cập vào thư mục **`Assets/Scenes`** trên cửa sổ Project.
4.  Nhấp đúp mở Scene **`MainMenu`**.
5.  Nhấn nút **Play** ở góc trên cùng của Unity Editor để bắt đầu trải nghiệm trò chơi!

---

## 📂 Cấu trúc mã nguồn chính (`Assets/Scripts`)

*   **`PlayerController.cs`**: Quản lý vật lý di chuyển, điều khiển phím, đo góc nhào lộn, tính điểm, xử lý nhận sát thương ngã và cập nhật Checkpoint.
*   **`CrashDetector.cs`**: Phát hiện va chạm ở đầu nhân vật (`Boarder_Top`) với mặt đất dốc tuyết để kích hoạt ngã.
*   **`FinishLine.cs`**: Phát hiện Barry chạm vạch đích để dừng điều khiển và chuyển cảnh chiến thắng.
*   **`Checkpoint.cs`**: Cập nhật vị trí hồi sinh cho người chơi khi đi qua.
*   **`Collectible.cs`**: Xử lý logic va chạm của bông tuyết và các loại Power-ups.
*   **`HUDManager.cs`**: Quản lý cập nhật dữ liệu của Score, Combo, Speed, Lives lên màn hình giao diện.
*   **`MainMenuController.cs`**: Xử lý logic chuyển cảnh và thoát game từ Menu chính.
