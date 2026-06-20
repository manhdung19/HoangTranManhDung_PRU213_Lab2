# TÀI LIỆU PHÂN TÍCH YÊU CẦU DỰ ÁN: "SNOW BOARDER"

## 1. Tổng Quan Dự Án (Overview)
* [cite_start]**Tên game:** Snow Boarder 
* [cite_start]**Thể loại:** Thể thao (Sports), Arcade [cite: 3]
* [cite_start]**Nền tảng:** PC [cite: 4]
* [cite_start]**Đồ họa:** 2D, chủ đề mùa đông [cite: 27]

---

## 2. Phân Tích Yêu Cầu Chức Năng (Functional Requirements)

Hệ thống tính điểm cho bài Lab được chia thành các hạng mục cốt lõi sau:

### 2.1. Cơ chế Điều khiển Nhân vật (Snowboarding Adventure) - [20 Điểm]
* [cite_start]**Di chuyển:** Người chơi điều khiển nhân vật trượt tuyết di chuyển xuống dốc[cite: 7]. [cite_start]Phản hồi điều khiển phải nhạy khi rẽ trái và rẽ phải[cite: 8].
* [cite_start]**Tốc độ:** Tích hợp cơ chế kiểm soát tốc độ để tăng độ chân thực và trải nghiệm nhập vai cho người chơi[cite: 9].

### 2.2. Vật lý và Môi trường (Unity's Physics Engine) - [15 Điểm]
* [cite_start]**Hệ thống vật lý:** Sử dụng bộ engine vật lý của Unity để giả lập chuyển động trượt tuyết thực tế[cite: 11].
* [cite_start]**Yếu tố tác động:** Triển khai các lực tác động lên nhân vật dựa trên độ dốc của địa hình bao gồm[cite: 12]:
    * [cite_start]Lực ma sát (Friction) [cite: 12]
    * [cite_start]Trọng lực (Gravity) [cite: 12]
    * [cite_start]Động lượng (Momentum) [cite: 12]

### 2.3. Thiết kế Màn chơi và Thử thách (Obstacle Course) - [20 Điểm]
* [cite_start]**Chướng ngại vật:** Thiết kế các màn chơi có mật độ vật cản đa dạng như dốc nhảy (ramps), bệ nhảy (jumps), và cây cối (trees)[cite: 14].
* [cite_start]**Thử thách tương tác:** Tích hợp các vật phẩm thu thập (bông tuyết) hoặc cơ chế thực hiện kỹ năng (tricks) để người chơi kiếm thêm điểm thưởng[cite: 15].

### 2.4. Xử lý Va chạm và Vùng kích hoạt (Collisions & Triggers) - [15 Điểm]
* [cite_start]**Xử lý Va chạm (Collisions):** Khi nhân vật va chạm với chướng ngại vật, tốc độ của người chơi phải bị giảm đi hoặc khiến nhân vật gặp tai nạn/ngã (crash)[cite: 20].
* **Vùng kích hoạt (Triggers):** Sử dụng Trigger để thiết lập:
    * [cite_start]Các vật phẩm tăng sức mạnh (Power-ups) như: tăng tốc (speed boosts), bất tử tạm thời (invincibility)[cite: 21].
    * [cite_start]Các lối đi tắt trên bản đồ (shortcuts)[cite: 21].

### 2.5. Logic Game và Hệ thống Điểm số (Game Logic & Scoring) - [20 Điểm]
* [cite_start]**Tính điểm:** Điểm số được tính dựa trên 3 yếu tố chính: Tốc độ di chuyển, số lượng vật phẩm thu thập được, và các kỹ năng (tricks) thực hiện thành công[cite: 23].
* [cite_start]**Hệ thống Combo:** Thiết kế cơ chế chuỗi (combo) khi người chơi thực hiện liên tiếp các kỹ năng[cite: 24].
* [cite_start]**Hệ số nhân điểm (Multiplier):** Áp dụng nhân điểm thưởng đối với các chuỗi hành động/kỹ năng thành công liên tiếp[cite: 25].

### 2.6. Giao diện Người dùng (User Interface Design) - [10 Điểm]
* [cite_start]**Main Menu (Màn hình chính):** Phải có tối thiểu 3 nút chức năng[cite: 17]:
    * [cite_start]*Start:* Bắt đầu trò chơi [cite: 17]
    * [cite_start]*Options:* Cài đặt tùy chỉnh [cite: 17]
    * [cite_start]*Quit:* Thoát trò chơi [cite: 17]
* [cite_start]**In-game HUD (Giao diện trong màn chơi):** Hiển thị thời gian thực các thông số[cite: 18]:
    * [cite_start]Điểm số hiện tại (Current score) [cite: 18]
    * [cite_start]Tốc độ hiện tại (Speed) [cite: 18]
    * [cite_start]Số mạng còn lại của người chơi (Player lives) [cite: 18]

---

## 3. Yêu Cầu Phi Chức Năng (Non-Functional Requirements)

### 3.1. Hình ảnh và Hiệu ứng (Visuals & Aesthetics)
* [cite_start]Đồ họa 2D sắc nét với phong cách/bối cảnh mùa đông[cite: 27].
* [cite_start]Hiệu ứng thời tiết động (Dynamic weather) như tuyết rơi (falling snow) hoặc thay đổi tầm nhìn của người chơi.
* [cite_start]Hệ thống chuyển động (Animation) của nhân vật phải mượt mà khi thực hiện các thao tác điều khiển và kỹ năng trượt tuyết[cite: 29].

### 3.2. Tính năng mở rộng (Optional - Tính thêm điểm cộng nếu có)
* [cite_start]Hệ thống nhân vật đa dạng với các chỉ số (stats) độc nhất cho mỗi nhân vật[cite: 31].
* [cite_start]Thiết kế nhiều ngọn núi/bản đồ với các cấp độ thử thách và độ khó khác nhau[cite: 32].
* [cite_start]Chế độ chơi tính giờ (Time trial mode) nhằm tăng tính cạnh tranh[cite: 33].
* [cite_start]Cho phép tùy biến ván trượt (snowboards) và trang phục của nhân vật[cite: 34].

---

## 4. Hướng Dẫn Nộp Bài (Submission Guidelines)

Khi hoàn thành, sinh viên cần nộp các thành phần sau:
1.  [cite_start]**Mã nguồn:** File dự án Unity (.unitypackage) hoặc một thư mục nén chứa toàn bộ các file của project Unity[cite: 36].
2.  [cite_start]**Tài liệu báo cáo:** Một văn bản ngắn gọn giải thích về[cite: 37]:
    * [cite_start]Các quyết định thiết kế game (Design decisions)[cite: 37].
    * [cite_start]Các khó khăn/thách thức gặp phải trong quá trình làm (Challenges faced)[cite: 37].
    * [cite_start]Các tính năng mở rộng/tính năng thêm đã triển khai (nếu có)[cite: 37].