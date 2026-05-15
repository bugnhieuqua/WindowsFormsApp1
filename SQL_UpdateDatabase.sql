-- ============================================
-- SQL Script: Tạo cột DiemSo trong table Users
-- Database: Cowofsurviva
-- ============================================

USE Cowofsurviva;

-- Kiểm tra xem cột DiemSo đã tồn tại chưa
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'DiemSo')
BEGIN
	-- Thêm cột DiemSo (mặc định = 0)
	ALTER TABLE Users
	ADD DiemSo INT DEFAULT 0;

	PRINT 'Cột DiemSo đã được thêm thành công!';
END
ELSE
BEGIN
	PRINT 'Cột DiemSo đã tồn tại!';
END

-- Xem lại cấu trúc table
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users';

-- Xem dữ liệu trong table
SELECT * FROM Users;

-- Cập nhật điểm số cho một số người chơi (test)
UPDATE Users SET DiemSo = 100 WHERE MaUS = 'admin';
UPDATE Users SET DiemSo = 50 WHERE MaUS = 'user1';
UPDATE Users SET DiemSo = 75 WHERE MaUS = 'user2';

PRINT 'Dữ liệu đã được cập nhật!';
