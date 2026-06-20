# BÁO CÁO HOÀN THIỆN DỰ ÁN - SNOW BOARDER (LAB 2)

## 1. Giới thiệu tổng quan
Dự án **Snow Boarder** là một trò chơi thể thao mạo hiểm 2D được phát triển trên công cụ Unity. Người chơi sẽ điều khiển nhân vật Barry thực hiện hành trình trượt tuyết vượt qua các địa hình uốn lượn, thực hiện các kỹ năng nhào lộn (Flips) để tích lũy điểm số và thu thập các vật phẩm đặc biệt trước khi cán đích an toàn.

---

## 2. Quyết định thiết kế & Cấu trúc kỹ thuật
*   **Thời tiết động (Dynamic Weather):** Tích hợp hiệu ứng tuyết rơi (`FallingSnow`) bằng Unity Particle System gắn trực tiếp trên Main Camera với chế độ mô phỏng không gian thế giới (`World Simulation Space`), mang lại chiều sâu thẩm mỹ cao.
*   **Vật lý dốc tuyết uốn lượn:** Sử dụng package **2D Sprite Shape** kết hợp `SurfaceEffector2D` và `EdgeCollider2D` để giả lập lực đẩy tự động, quán tính và gia tốc khi lao dốc. Gán vật liệu vật lý trơn trượt (`Friction = 0`) để tối ưu động lượng trượt.
*   **Hiệu ứng tương tác trực quan:** 
    *   Hạt bụi tuyết (`Dust Particles`) phun ra sau ván trượt khi tiếp xúc với đất cát, tự động tắt khi người chơi bay lên không trung để tăng tính thực tế.
    *   Hiệu ứng tóe tuyết ngã (`Crash Effect`) và đổi màu nhân vật sang màu vàng kim khi kích hoạt trạng thái bất tử.
*   **Hệ thống máy ảnh thông minh:** Sử dụng package **Cinemachine Virtual Camera** bám theo vị trí (`Follow`) của Barry mà không khóa hướng nhìn (`Look At = None`) để tránh hiện tượng xoay nghiêng màn hình 2D không mong muốn.

---

## 3. Các khó khăn đã giải quyết (Challenges & Solutions)
*   **Xung đột hệ thống điều khiển (Input System Package):**
    *   *Khó khăn:* Trình điều khiển mặc định sử dụng API legacy `Input.GetKey` gây lỗi `InvalidOperationException` do dự án được cấu hình bằng hệ thống New Input System của Unity.
    *   *Giải pháp:* Hướng dẫn chuyển đổi thiết lập `Active Input Handling` trong Project Settings sang chế độ `Both` để giữ tính tương thích cao và chạy mượt mà cả hai hệ thống điều khiển.
*   **Lỗi vòng lặp ngã vô hạn khi hồi sinh (Infinite Crash Loop):**
    *   *Khó khăn:* Khi người chơi bị ngã, hệ thống hồi sinh họ tại tâm của checkpoint cũ. Nếu checkpoint đặt quá sát mặt đất, phần chân của nhân vật sẽ bị kẹt dưới đất gây ngã liên tục.
    *   *Giải pháp:* Cải tiến script `PlayerController.cs` tự động cộng thêm một khoảng dịch chuyển offset theo trục Y (`+1.5f`) khi cập nhật checkpoint để nhân vật luôn hồi sinh lơ lửng trên không một chút và rơi xuống tiếp đất bằng ván trượt.

---

## 4. Tính năng mở rộng nâng cao (Extra Features)
*   **Hệ thống nhào lộn (Trick Detection):** Tính toán tích lũy góc xoay (Z angle) của nhân vật khi bay trên không bằng hàm `Mathf.DeltaAngle`. Nếu xoay đủ vòng 360 độ (sai số cho phép 20 độ) và tiếp đất an toàn sẽ được tính điểm kỹ năng.
*   **Hệ số nhân Combo (Multiplier System):**
    *   Mỗi khi nhào lộn thành công liên tiếp hoặc thu thập bông tuyết, hệ số nhân combo sẽ tăng thêm (x1, x2, x3...).
    *   Nếu va chạm chướng ngại vật hoặc bị ngã (Crash), hệ số nhân combo sẽ bị đặt lại về x1.
*   **Vật phẩm Power-ups đặc biệt:**
    *   *Speed Boost (Tăng tốc):* Tăng tốc độ tối đa của dốc tuyết lên gấp rưỡi trong 3 giây.
    *   *Invincibility (Bất tử):* Bảo vệ người chơi hoàn toàn khỏi chướng ngại vật thường và va chạm đập đầu ngã trong 5 giây, kèm theo hiệu ứng nhấp nháy đổi màu nhân vật.
