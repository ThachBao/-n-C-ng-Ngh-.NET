-- Tạo cơ sở dữ liệu QL_NHANSU
CREATE DATABASE QL_NHANSU;
GO
-- Sử dụng cơ sở dữ liệu QL_NHANSU
USE QL_NHANSU;
GO


--Tạo bảng Bộ phận
CREATE TABLE BoPhan (
    MaBoPhan VARCHAR(50) PRIMARY KEY,
    TenBoPhan NVARCHAR(100)
);

--Bảng chức vụ:
CREATE TABLE ChucVu (
    MaChucVu VARCHAR(50) PRIMARY KEY,
    TenChucVu NVARCHAR(100)
);

--Bảng phòng ban
CREATE TABLE PhongBan (
    MaPhong VARCHAR(50) PRIMARY KEY,
    TenPhong NVARCHAR(100)
);


-- Bảng trình độ năng lực
CREATE TABLE TrinhDoNangLuc (
    MaTrinhDo VARCHAR(50) PRIMARY KEY,
    TenTrinhDo NVARCHAR(100)
);

-- Tạo bảng EMPLOYEE
CREATE TABLE HoSoNhanVien (
    MANV NVARCHAR(50) PRIMARY KEY,
    HOTEN NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(3) Check(GioiTinh in ('Nam', N'Nữ')),
    NgaySinh DATE,
    DiaChi NVARCHAR(255),
    CCCD NVARCHAR(12),
	MaChucVu VARCHAR(50),
    MaBoPhan VARCHAR(50),
    MaPhong VARCHAR(50),
    TrangThai NVARCHAR(100),
    MaTrinhDo VARCHAR(50),
	FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu),
	FOREIGN KEY (MaBoPhan) REFERENCES BoPhan(MaBoPhan),
	FOREIGN KEY (MaPhong) REFERENCES PhongBan(MaPhong),
	FOREIGN KEY (MaTrinhDo) REFERENCES TrinhDoNangLuc(MaTrinhDo),
);

-- Tạo bảng Hợp đồng
CREATE TABLE HopDong (
    SoHopDong INT PRIMARY KEY,
    NgayBatDau DATE,
    NgayKetThuc DATE,
	--HOTEN NVARCHAR(50),
	NoiDung NVARCHAR(255),
    ThoiHan INT,
    HeSoLuong DECIMAL(10, 2),
    MANV NVARCHAR(50),
    FOREIGN KEY (MANV) REFERENCES HoSoNhanVien(MANV)
);


-- Bảng bảo hiểm
CREATE TABLE BaoHiem (
    MaBaoHiem INT PRIMARY KEY,
    SoBaoHiem NVARCHAR(50),
    NgayCap DATE,
    NoiCap NVARCHAR(100),
    NoiKhamBenh NVARCHAR(100),
    MANV NVARCHAR(50),
    FOREIGN KEY (MANV) REFERENCES HoSoNhanVien(MANV)
);


-- Tạo bảng CHAMCONG
CREATE TABLE CHAMCONG
(
    ID_CHAMCONG CHAR(20) NOT NULL,
    NGAYVAO DATETIME,
	TRANGTHAIVAO BIT NULL,
	TRANGTHAIRA BIT NULL,
    MANV NVARCHAR(50),
    CONSTRAINT PK_CHAMCONG PRIMARY KEY (ID_CHAMCONG, MaNV),
	CONSTRAINT FK_CHAMCONG_HoSoNhanVien FOREIGN KEY (MANV) REFERENCES HoSoNhanVien(MANV)
);
GO
CREATE TRIGGER SET_NGAYVAO
ON CHAMCONG
FOR INSERT 
AS
BEGIN
    SET NOCOUNT ON;--Ngăn không cho SQL Server gửi thông báo về số dòng đã bị ảnh hưởng đến client.
	--Điều này có thể giúp cải thiện hiệu suất, đặc biệt khi trigger thực hiện nhiều lệnh.

    UPDATE CHAMCONG
    SET NGAYVAO = GETDATE() -- Lấy ngày và giờ hiện tại
    FROM CHAMCONG cc
    INNER JOIN inserted i ON cc.ID_CHAMCONG = i.ID_CHAMCONG AND cc.MANV = i.MANV;
END;
GO
-- Tạo bảng BANGCONG
CREATE TABLE BANGCONG
(
    ID_BANGCONG UNIQUEIDENTIFIER ,
	MANV NVARCHAR(50),
    NGAY INT,
    THANG INT,
    NAM INT,
    ID_CHAMCONG CHAR(20),
    TRANGTHAI NVARCHAR(20),
    CONSTRAINT PK_BANGCONG PRIMARY KEY (ID_BANGCONG),
    CONSTRAINT FK_BANGCONG_EMPLOYEE FOREIGN KEY (MANV) REFERENCES HoSoNhanVien(MANV),
    CONSTRAINT FK_BANGCONG_CHAMCONG FOREIGN KEY (ID_CHAMCONG, MANV) REFERENCES CHAMCONG(ID_CHAMCONG, MANV),
    CONSTRAINT CHK_NGAY CHECK 
    (
        (NGAY BETWEEN 1 AND 31) AND 
        (
            (THANG = 1 OR THANG = 3 OR THANG = 5 OR THANG = 7 OR THANG = 8 OR THANG = 10 OR THANG = 12) OR
            (THANG = 4 OR THANG = 6 OR THANG = 9 OR THANG = 11 AND NGAY <= 30) OR
            (THANG = 2 AND (
                (NAM % 4 = 0 AND NAM % 100 <> 0) OR NAM % 400 = 0) AND NGAY <= 29 OR
                (THANG = 2 AND NGAY <= 28)
            )
        )
    )
);
GO

ALTER TABLE BANGCONG
ADD THU AS 
    CASE DATEPART(WEEKDAY, CAST(CONCAT(NAM, '-', THANG, '-', NGAY) AS DATE))
        WHEN 1 THEN N'Chủ nhật'
        WHEN 2 THEN N'Thứ hai'
        WHEN 3 THEN N'Thứ ba'
        WHEN 4 THEN N'Thứ tư'
        WHEN 5 THEN N'Thứ năm'
        WHEN 6 THEN N'Thứ sáu'
        WHEN 7 THEN N'Thứ bảy'
    END;
GO


SET DATEFORMAT DMY
--chay trigger de them du lieu vao bang cong 
CREATE TRIGGER THEMDULIEUVAOBANGCONG
ON CHAMCONG
FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    -- Chèn dữ liệu vào bảng BANGCONG với các bản ghi vừa được thêm vào bảng CHAMCONG
    INSERT INTO BANGCONG (ID_BANGCONG, MANV, NGAY, THANG, NAM, ID_CHAMCONG)
    SELECT 
        NEWID(),  -- Sinh ID_BANGCONG mới
        i.MANV,  -- Lấy MANV từ bảng ảo INSERTED
        DAY(GETDATE()) AS NGAY,  -- Lấy ngày từ NGAYVAO trong bảng ảo
        MONTH(GETDATE()) AS THANG,  -- Lấy tháng từ NGAYVAO trong bảng ảo
        YEAR(GETDATE()) AS NAM,  -- Lấy năm từ NGAYVAO trong bảng ảo
        i.ID_CHAMCONG  -- Lấy ID_CHAMCONG từ bảng ảo INSERTED
    FROM 
        inserted i
END;
GO

CREATE TRIGGER trg_UpdateTrangThai
ON CHAMCONG
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Cập nhật TRANGTHAI trong bảng BANGCONG
    UPDATE BANGCONG
    SET TRANGTHAI = CASE 
        WHEN CH.TRANGTHAIVAO = 0 OR CH.TRANGTHAIRA = 0 THEN N'Chưa Hoàn Thành'
        ELSE N'Hoàn Thành'
    END
    FROM BANGCONG BG
    INNER JOIN inserted I ON I.ID_CHAMCONG = BG.ID_CHAMCONG
    INNER JOIN CHAMCONG CH ON I.ID_CHAMCONG = CH.ID_CHAMCONG AND I.MANV = CH.MANV
    WHERE ISDATE(CH.NGAYVAO) = 1;  -- Kiểm tra giá trị ngày hợp lệ
END
-- Bảng kỉ luật khen thưởng
CREATE TABLE KhenThuongKyLuat (
    MaKTKL INT PRIMARY KEY,
    TenKTKL NVARCHAR(100),
    NoiDung NVARCHAR(255),
    Ngay DATE,
    MaNV NVARCHAR(50),
    Loai NVARCHAR(50),
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV)
);


-- Tạo bảng lương
CREATE TABLE Luong (
    MaNV NVARCHAR(50),
    MaChucVu VARCHAR(50),
    --HoTen NVARCHAR(100),
    TongChiPhi DECIMAL(18, 2),
    LuongThucNhan DECIMAL(18, 2),
    BaoHiemNhanSu DECIMAL(18, 2),
    LuongNhanSu DECIMAL(18, 2),
    PRIMARY KEY (MaNV),
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV),
    FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu)
);

-- Tạo bảng dự án
CREATE TABLE DuAn (
    MaDuAn VARCHAR(50) PRIMARY KEY,
    TenDuAn NVARCHAR(100),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    MoTa NVARCHAR(255),
    MaPhong VARCHAR(50),
    FOREIGN KEY (MaPhong) REFERENCES PhongBan(MaPhong)
);


-- Tạo bảng Phân công
CREATE TABLE PhanCong (
    MaPhanCong VARCHAR(50) PRIMARY KEY,
    NgayThamGia DATE,
    NgayKetThuc DATE,
    MaNV NVARCHAR(50),
    MaDuAn VARCHAR(50),
    VaiTro NVARCHAR(100),
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV),
    FOREIGN KEY (MaDuAn) REFERENCES DuAn(MaDuAn)
);

-- Tạo bảng DaoTao
CREATE TABLE DaoTao (
    MaDaoTao VARCHAR(50) PRIMARY KEY,
    TenKhoaHoc NVARCHAR(100),
    ThoiGianBatDau DATE,
    ThoiGianKetThuc DATE,
    NoiDung NVARCHAR(255),
    MaNV NVARCHAR(50),
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV)
);


-- Tạo bảng ACCOUNTS
CREATE TABLE ACCOUNTS (
    ACC_ID INT IDENTITY(1,1) PRIMARY KEY,
	TENDANGNHAP VARCHAR(50),
    MATKHAU NVARCHAR(50) NOT NULL,
    PHANQUYEN NVARCHAR(10) CHECK(PHANQUYEN IN('ADMIN', 'QLCHAMCONG','QLLUONG')),
    EMAIL NVARCHAR(50),
);
GO
-- ================================================================================
INSERT INTO BoPhan (MaBoPhan, TenBoPhan)
VALUES
('BP01', N'Bộ Phận Kỹ thuật'),
('BP02', N'Bộ Phận Kinh doanh'),
('BP03', N'Bộ Phận Nhân sự'),
('BP04', N'Bộ Phận Tài chính')

-- INSERT INTO PhongBan (sửa lại mã phòng ban cho tính duy nhất)
INSERT INTO PhongBan (MaPhong, TenPhong)
VALUES
('PB01', N'Phòng IT'), -- Bộ phận kỹ thuật
('PB02', N'Phòng Kế toán'), --Bộ phận tài chính
('PB03', N'Phòng Tuyển Dụng'),--Bộ phận nhân sự
('PB04', N'Phòng Đào Tạo'), --Bộ phận nhân sự
('PB05', N'Phòng Marketing') --Bộ phận kinh doanh


-- INSERT INTO ChucVu (sửa mã chức vụ duy nhất)
INSERT INTO ChucVu (MaChucVu, TenChucVu)
VALUES
('CV01', N'Giám đốc'),
('CV02', N'Trưởng phòng'),
('CV03', N'Nhân viên'),
('CV04', N'Thư ký')

-- INSERT INTO TrinhDoNangLuc (sửa lại mã trình độ duy nhất)
INSERT INTO TrinhDoNangLuc (MaTrinhDo, TenTrinhDo)
VALUES
('TD01', N'Cử nhân'),
('TD02', N'Thạc sĩ'),
('TD03', N'Kỹ sư'),
('TD04', N'Tiến sĩ')

INSERT INTO ACCOUNTS(TENDANGNHAP,MATKHAU,PHANQUYEN,EMAIL)
VALUES
('Thachbao',N'1234567','ADMIN','thachbao2910@gmail.com'),
('Hoanghai',N'2345678','QLCHAMCONG','hoanghai@gmail.com'),
('Trongduc',N'3456789','QLLUONG','trongduc@gmail.com')

INSERT INTO HoSoNhanVien (MaNV, HoTen, GioiTinh, NgaySinh, DiaChi, CCCD, MaChucVu, MaBoPhan, MaPhong, TrangThai, MaTrinhDo)
VALUES
('NV001', N'Nguyễn Văn A', N'Nam', '1990-01-01', N'Hà Nội', N'205456240539', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD01'),
('NV002', N'Trần Thị B', N'Nữ', '1992-02-15', N'Hà Nội', N'177186313065', 'CV02', 'BP02', 'PB02', N'Đang làm việc', 'TD02'),
('NV003', N'Nguyễn Văn C', N'Nam', '1995-03-10', N'TP.HCM', N'072146665526', 'CV03', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV004', N'Phạm Thị D', N'Nữ', '1994-04-20', N'TP.HCM', N'887073099882', 'CV04', 'BP02', 'PB02', N'Đang làm việc', 'TD01'),
('NV005', N'Hoàng Văn E', N'Nam', '1988-05-25', N'Hải Phòng', N'368003773012', 'CV01', 'BP03', 'PB03', N'Đang làm việc', 'TD02'),
('NV006', N'Phạm Văn F', N'Nam', '1989-06-30', N'Đà Nẵng', N'925615126393', 'CV02', 'BP04', 'PB04', N'Nghỉ việc', 'TD03'),
('NV007', N'Lê Thị G', N'Nữ', '1993-07-12', N'Quảng Ninh', N'934076430789', 'CV03', 'BP03', 'PB03', N'Đang làm việc', 'TD01'),
('NV008', N'Vũ Văn H', N'Nam', '1991-08-18', N'Vũng Tàu', N'981000009525', 'CV04', 'BP04', 'PB04', N'Nghỉ việc', 'TD02'),
('NV009', N'Ngô Thị I', N'Nữ', '1996-09-21', N'Huế', N'831113591270', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV010', N'Phan Văn K', N'Nam', '1997-10-11', N'Cần Thơ', N'407323696288', 'CV02', 'BP02', 'PB02', N'Đang làm việc', 'TD01');


--INSERT INTO HopDong (SoHopDong, NgayBatDau, NgayKetThuc, HoTenNV, NoiDung, ThoiHan, HeSoLuong, MaNV)
--VALUES
--(1, '2021-01-01', '2023-01-01', N'Nguyễn Văn A', N'Hợp đồng lao động 3 năm', 36, 1.2, 1),
--(2, '2021-02-01', '2023-02-01', N'Trần Thị B', N'Hợp đồng lao động 3 năm', 36, 1.1, 2),
--(3, '2022-03-01', '2024-03-01', N'Nguyễn Văn C', N'Hợp đồng lao động 2 năm', 24, 1.5, 3),
--(4, '2020-04-01', '2023-04-01', N'Phạm Thị D', N'Hợp đồng lao động 3 năm', 36, 1.3, 4),
--(5, '2021-05-01', '2024-05-01', N'Hoàng Văn E', N'Hợp đồng lao động 3 năm', 36, 1.2, 5),
--(6, '2022-06-01', '2024-06-01', N'Phạm Văn F', N'Hợp đồng lao động 2 năm', 24, 1.1, 6),
--(7, '2020-07-01', '2023-07-01', N'Lê Thị G', N'Hợp đồng lao động 3 năm', 36, 1.5, 7),
--(8, '2019-08-01', '2022-08-01', N'Vũ Văn H', N'Hợp đồng lao động 3 năm', 36, 1.4, 8),
--(9, '2022-09-01', '2025-09-01', N'Ngô Thị I', N'Hợp đồng lao động 3 năm', 36, 1.6, 9),
--(10, '2021-10-01', '2024-10-01', N'Phan Văn K', N'Hợp đồng lao động 3 năm', 36, 1.3, 10)

INSERT INTO HopDong (SoHopDong, NgayBatDau, NgayKetThuc, NoiDung, ThoiHan, HeSoLuong, MaNV)
VALUES
(1, '2021-01-01', '2023-01-01', N'Hợp đồng lao động 3 năm', 36, 1.2, 'NV001'),
(2, '2021-02-01', '2023-02-01', N'Hợp đồng lao động 3 năm', 36, 1.1, 'NV002'),
(3, '2022-03-01', '2024-03-01', N'Hợp đồng lao động 2 năm', 24, 1.5, 'NV003'),
(4, '2020-04-01', '2023-04-01', N'Hợp đồng lao động 3 năm', 36, 1.3, 'NV004'),
(5, '2021-05-01', '2024-05-01', N'Hợp đồng lao động 3 năm', 36, 1.2, 'NV005'),
(6, '2022-06-01', '2024-06-01', N'Hợp đồng lao động 2 năm', 24, 1.1, 'NV006'),
(7, '2020-07-01', '2023-07-01', N'Hợp đồng lao động 3 năm', 36, 1.5, 'NV007'),
(8, '2019-08-01', '2022-08-01', N'Hợp đồng lao động 3 năm', 36, 1.4, 'NV008'),
(9, '2022-09-01', '2025-09-01', N'Hợp đồng lao động 3 năm', 36, 1.6, 'NV009'),
(10, '2021-10-01', '2024-10-01', N'Hợp đồng lao động 3 năm', 36, 1.3, 'NV010')



--INSERT INTO BaoHiem (MaBaoHiem, SoBaoHiem, NgayCap, NoiCap, NoiKhamBenh, MaNV)
--VALUES
--(1, N'1234567890', '2020-01-01', N'Hà Nội', N'Bệnh viện Bạch Mai', 1),
--(2, N'1234567891', '2020-02-01', N'Hà Nội', N'Bệnh viện Việt Đức', 2),
--(3, N'1234567892', '2021-03-01', N'TP.HCM', N'Bệnh viện Chợ Rẫy', 3),
--(4, N'1234567893', '2019-04-01', N'TP.HCM', N'Bệnh viện Bình Dân', 4),
--(5, N'1234567894', '2022-05-01', N'Hải Phòng', N'Bệnh viện Việt Tiệp', 5),
--(6, N'1234567895', '2021-06-01', N'Đà Nẵng', N'Bệnh viện C Đà Nẵng', 6),
--(7, N'1234567896', '2020-07-01', N'Quảng Ninh', N'Bệnh viện Sản Nhi', 7),
--(8, N'1234567897', '2019-08-01', N'Vũng Tàu', N'Bệnh viện Vũng Tàu', 8),
--(9, N'1234567898', '2022-09-01', N'Huế', N'Bệnh viện Trung ương Huế', 9),
--(10, N'1234567899', '2021-10-01', N'Cần Thơ', N'Bệnh viện Đa khoa Cần Thơ', 10)

INSERT INTO BaoHiem (MaBaoHiem, SoBaoHiem, NgayCap, NoiCap, NoiKhamBenh, MaNV)
VALUES
(1, N'1234567890', '2020-01-01', N'Hà Nội', N'Bệnh viện Bạch Mai', 'NV001'),
(2, N'1234567891', '2020-02-01', N'Hà Nội', N'Bệnh viện Việt Đức', 'NV002'),
(3, N'1234567892', '2021-03-01', N'TP.HCM', N'Bệnh viện Chợ Rẫy', 'NV003'),
(4, N'1234567893', '2019-04-01', N'TP.HCM', N'Bệnh viện Bình Dân', 'NV004'),
(5, N'1234567894', '2022-05-01', N'Hải Phòng', N'Bệnh viện Việt Tiệp', 'NV005'),
(6, N'1234567895', '2021-06-01', N'Đà Nẵng', N'Bệnh viện C Đà Nẵng', 'NV006'),
(7, N'1234567896', '2020-07-01', N'Quảng Ninh', N'Bệnh viện Sản Nhi', 'NV007'),
(8, N'1234567897', '2019-08-01', N'Vũng Tàu', N'Bệnh viện Vũng Tàu', 'NV008'),
(9, N'1234567898', '2022-09-01', N'Huế', N'Bệnh viện Trung ương Huế', 'NV009'),
(10, N'1234567899', '2021-10-01', N'Cần Thơ', N'Bệnh viện Đa khoa Cần Thơ', 'NV010')


INSERT INTO CHAMCONG (ID_CHAMCONG, MANV)
VALUES
    ('CC001', 'NV001'),
    ('CC002', 'NV002'),
    ('CC003', 'NV003'),
    ('CC004', 'NV004'),
    ('CC005', 'NV005'),
	('CC006', 'NV006'),
    ('CC007', 'NV007'),
    ('CC008', 'NV008'),
    ('CC009', 'NV009'),
    ('CC010', 'NV010');
GO

INSERT INTO KhenThuongKyLuat (MaKTKL, TenKTKL, NoiDung, Ngay, MaNV, Loai)
VALUES
(1, N'Khen thưởng tháng 1', N'Hoàn thành xuất sắc công việc', '2023-01-31', 'NV001', N'Khen thưởng'),
(2, N'Khen thưởng tháng 2', N'Đóng góp tích cực vào dự án', '2023-02-28', 'NV002', N'Khen thưởng'),
(3, N'Kỉ luật tháng 3', N'Đi làm muộn 3 ngày liên tiếp', '2023-03-15', 'NV003', N'Kỉ luật'),
(4, N'Khen thưởng tháng 4', N'Đề xuất giải pháp cải tiến quy trình', '2023-04-30', 'NV004', N'Khen thưởng'),
(5, N'Khen thưởng tháng 5', N'Hỗ trợ dự án vượt qua khó khăn', '2023-05-31', 'NV005', N'Khen thưởng'),
(6, N'Kỉ luật tháng 6', N'Vi phạm quy định công ty', '2023-06-15', 'NV006', N'Kỉ luật'),
(7, N'Khen thưởng tháng 7', N'Tích cực tham gia hoạt động công ty', '2023-07-31', 'NV007', N'Khen thưởng'),
(8, N'Kỉ luật tháng 8', N'Không hoàn thành nhiệm vụ đúng hạn', '2023-08-15', 'NV008', N'Kỉ luật'),
(9, N'Khen thưởng tháng 9', N'Hỗ trợ đồng nghiệp trong dự án', '2023-09-30', 'NV009', N'Khen thưởng'),
(10, N'Khen thưởng tháng 10', N'Đóng góp lớn vào doanh số quý 3', '2023-10-10', 'NV010', N'Khen thưởng')


--INSERT INTO Luong (MaNV, MaChucVu, HoTen, TongChiPhi, LuongThucNhan, BaoHiemNhanSu, LuongNhanSu)
--VALUES
--(1, 1, N'Nguyễn Văn A', 30000000, 25000000, 3000000, 2000000),
--(2, 2, N'Trần Thị B', 28000000, 23000000, 2800000, 2200000),
--(3, 3, N'Nguyễn Văn C', 27000000, 22000000, 2700000, 2300000),
--(4, 4, N'Phạm Thị D', 26000000, 21000000, 2600000, 2400000),
--(5, 1, N'Hoàng Văn E', 31000000, 26000000, 3100000, 2500000),
--(6, 2, N'Phạm Văn F', 25000000, 20000000, 2500000, 2500000),
--(7, 3, N'Lê Thị G', 29000000, 24000000, 2900000, 2600000),
--(8, 4, N'Vũ Văn H', 30000000, 25000000, 3000000, 2500000),
--(9, 1, N'Ngô Thị I', 32000000, 27000000, 3200000, 2700000),
--(10, 2, N'Phan Văn K', 26000000, 22000000, 2600000, 2400000)

INSERT INTO Luong (MaNV, MaChucVu, TongChiPhi, LuongThucNhan, BaoHiemNhanSu, LuongNhanSu)
VALUES
('NV001', 'CV01',  30000000, 25000000, 3000000, 2000000),
('NV002', 'CV02',  28000000, 23000000, 2800000, 2200000),
('NV003', 'CV03',  27000000, 22000000, 2700000, 2300000),
('NV004', 'CV04',  26000000, 21000000, 2600000, 2400000),
('NV005', 'CV01', 31000000, 26000000, 3100000, 2500000),
('NV006', 'CV02', 25000000, 20000000, 2500000, 2500000),
('NV007', 'CV03',  29000000, 24000000, 2900000, 2600000),
('NV008', 'CV04', 30000000, 25000000, 3000000, 2500000),
('NV009', 'CV01', 32000000, 27000000, 3200000, 2700000),
('NV010', 'CV02', 26000000, 22000000, 2600000, 2400000)


--INSERT INTO DuAn (MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, MoTa, MaPhong)
--VALUES
--(1, N'Dự án A', '2023-01-01', '2023-06-30', N'Dự án phát triển phần mềm quản lý', 1),
--(2, N'Dự án B', '2023-02-01', '2023-07-31', N'Dự án thiết kế hệ thống mạng', 2),
--(3, N'Dự án C', '2023-03-01', '2023-08-31', N'Dự án xây dựng cơ sở dữ liệu', 3),
--(4, N'Dự án D', '2023-04-01', '2023-09-30', N'Dự án cải tiến quy trình làm việc', 4),
--(5, N'Dự án E', '2023-05-01', '2023-10-31', N'Dự án phát triển ứng dụng di động', 1),
--(6, N'Dự án F', '2023-06-01', '2023-11-30', N'Dự án nâng cấp hệ thống bảo mật', 2),
--(7, N'Dự án G', '2023-07-01', '2023-12-31', N'Dự án tối ưu hóa quy trình sản xuất', 3),
--(8, N'Dự án H', '2023-08-01', '2024-01-31', N'Dự án xây dựng chiến lược marketing', 4),
--(9, N'Dự án I', '2023-09-01', '2024-02-28', N'Dự án phát triển website', 1),
--(10, N'Dự án J', '2023-10-01', '2024-03-31', N'Dự án đào tạo nhân viên', 2)

INSERT INTO DuAn (MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, MoTa, MaPhong)
VALUES
('DA01', N'Dự án A', '2023-01-01', '2023-06-30', N'Dự án phát triển phần mềm quản lý', 'PB01'),
('DA02', N'Dự án B', '2023-02-01', '2023-07-31', N'Dự án thiết kế hệ thống mạng', 'PB02'),
('DA03', N'Dự án C', '2023-03-01', '2023-08-31', N'Dự án xây dựng cơ sở dữ liệu', 'PB03'),
('DA04', N'Dự án D', '2023-04-01', '2023-09-30', N'Dự án cải tiến quy trình làm việc', 'PB04'),
('DA05', N'Dự án E', '2023-05-01', '2023-10-31', N'Dự án phát triển ứng dụng di động', 'PB01'),
('DA06', N'Dự án F', '2023-06-01', '2023-11-30', N'Dự án nâng cấp hệ thống bảo mật', 'PB02'),
('DA07', N'Dự án G', '2023-07-01', '2023-12-31', N'Dự án tối ưu hóa quy trình sản xuất', 'PB03'),
('DA08', N'Dự án H', '2023-08-01', '2024-01-31', N'Dự án xây dựng chiến lược marketing', 'PB04'),
('DA09', N'Dự án I', '2023-09-01', '2024-02-28', N'Dự án phát triển website', 'PB01'),
('DA10', N'Dự án J', '2023-10-01', '2024-03-31', N'Dự án đào tạo nhân viên', 'PB02')


INSERT INTO PhanCong (MaPhanCong, NgayThamGia, NgayKetThuc, MaNV, MaDuAn, VaiTro)
VALUES
('PC01', '2023-01-10', '2023-06-20', 'NV001', 'DA01', N'Trưởng nhóm'),
('PC02', '2023-02-15', '2023-07-15', 'NV002', 'DA01', N'Nhân viên'),
('PC03', '2023-03-20', '2023-08-20', 'NV003', 'DA02', N'Trưởng nhóm'),
('PC04', '2023-04-25', '2023-09-25', 'NV004', 'DA02', N'Nhân viên'),
('PC05', '2023-05-30', '2023-10-30','NV005', 'DA03', N'Trưởng nhóm'),
('PC06', '2023-06-05', '2023-11-05', 'NV006', 'DA03', N'Nhân viên'),
('PC07', '2023-07-10', '2023-12-10', 'NV007', 'DA04', N'Trưởng nhóm'),
('PC08', '2023-08-15', '2024-01-15','NV008', 'DA04', N'Nhân viên'),
('PC09', '2023-09-20', '2024-02-20','NV009', 'DA05', N'Trưởng nhóm'),
('PC10', '2023-10-25', '2024-03-25', 'NV010', 'DA05', N'Nhân viên')


-- Thêm dữ liệu cho bảng DaoTao
INSERT INTO DaoTao (MaDaoTao, TenKhoaHoc, ThoiGianBatDau, ThoiGianKetThuc, NoiDung, MaNV)
VALUES
('DT001', N'Kỹ năng lãnh đạo', '2023-01-01', '2023-01-05', N'Nâng cao kỹ năng quản lý và lãnh đạo', 'NV001'),
('DT002', N'Giao tiếp hiệu quả', '2023-02-10', '2023-02-15', N'Phát triển kỹ năng giao tiếp và thuyết phục', 'NV002'),
('DT003', N'Thuyết trình chuyên nghiệp', '2023-03-12', '2023-03-17', N'Kỹ năng thuyết trình trước công chúng', 'NV003'),
('DT004', N'Quản lý dự án', '2023-04-01', '2023-04-10', N'Học cách lập kế hoạch và quản lý dự án hiệu quả', 'NV004'),
('DT005', N'Training về công nghệ mới', '2023-05-20', '2023-05-25', N'Cập nhật kiến thức công nghệ mới trong lĩnh vực IT', 'NV005'),
('DT006', N'Kỹ năng đàm phán', '2023-06-15', '2023-06-20', N'Phát triển kỹ năng đàm phán và thương lượng', 'NV006'),
('DT007', N'Nâng cao năng lực quản lý tài chính', '2023-07-10', '2023-07-15', N'Kỹ năng phân tích tài chính và lập ngân sách', 'NV007'),
('DT008', N'Kỹ năng bán hàng chuyên nghiệp', '2023-08-05', '2023-08-10', N'Chiến lược và kỹ năng bán hàng hiệu quả', 'NV008'),
('DT009', N'Đào tạo phân tích dữ liệu', '2023-09-01', '2023-09-07', N'Kỹ năng phân tích và trực quan hóa dữ liệu', 'NV009'),
('DT010', N'An toàn thông tin', '2023-10-05', '2023-10-10', N'Bảo mật và bảo vệ hệ thống thông tin', 'NV010');

SELECT * FROM HoSoNhanVien
SELECT * FROM Luong
SELECT * FROM HopDong
SELECT * FROM BaoHiem
SELECT * FROM BoPhan
SELECT * FROM CHAMCONG
SELECT * FROM ChucVu
SELECT * FROM DuAn
SELECT * FROM KhenThuongKyLuat
SELECT * FROM PhanCong
SELECT * FROM PhongBan
SELECT * FROM TrinhDoNangLuc
Select * from DaoTao
Select * from BANGCONG

drop table BaoHiem
drop table BoPhan
drop table CHAMCONG
drop table ChucVu
drop table DuAn
drop table HopDong
drop table HoSoNhanVien
drop table KhenThuongKyLuat
drop table Luong
drop table PhanCong
drop table PhongBan
drop table TrinhDoNangLuc
DROP TABLE BANGCONG

--mỗi ngày import vào mỗi nhân viên
DECLARE @CurrentDate DATETIME = GETDATE();

INSERT INTO CHAMCONG (ID_CHAMCONG, NGAYVAO, MANV)
SELECT 
    LEFT(CAST(MANV AS NVARCHAR(50)) + FORMAT(@CurrentDate, 'MMdd'), 20), -- Chỉ lấy phần cần thiết
    @CurrentDate, 
    MANV
FROM HoSoNhanVien; -- Lấy tất cả mã nhân viên từ bảng HoSoNhanVien
