-- Tạo cơ sở dữ liệu QL_NHANSU
CREATE DATABASE QL_NHANSU;
GO

-- Sử dụng cơ sở dữ liệu QL_NHANSU
USE QL_NHANSU;
GO

--Tạo bảng Bộ phận
CREATE TABLE BoPhan (
    MaBoPhan INT PRIMARY KEY,
    TenBoPhan NVARCHAR(100)
);

--Bảng chức vụ:
CREATE TABLE ChucVu (
    MaChucVu INT PRIMARY KEY,
    TenChucVu NVARCHAR(100)
);

--Bảng phòng ban
CREATE TABLE PhongBan (
    MaPhong INT PRIMARY KEY,
    TenPhong NVARCHAR(100)
);


-- Bảng trình độ năng lực
CREATE TABLE TrinhDoNangLuc (
    MaTrinhDo INT PRIMARY KEY,
    TenTrinhDo NVARCHAR(100)
);

-- Tạo bảng EMPLOYEE
CREATE TABLE HoSoNhanVien (
    MaNV INT PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(3) Check(GioiTinh in ('Nam', N'Nữ')),
    NgaySinh DATE,
    DiaChi NVARCHAR(255),
    CCCD NVARCHAR(12),
	MaChucVu INT,
    MaBoPhan INT,
    MaPhong INT,
    TrangThai NVARCHAR(100),
    MaTrinhDo INT,
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
    HoTenNV NVARCHAR(100),
    NoiDung NVARCHAR(255),
    ThoiHan INT,
    HeSoLuong DECIMAL(10, 2),
    MaNV INT,
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV)
);


-- Bảng bảo hiểm
CREATE TABLE BaoHiem (
    MaBaoHiem INT PRIMARY KEY,
    SoBaoHiem NVARCHAR(50),
    NgayCap DATE,
    NoiCap NVARCHAR(100),
    NoiKhamBenh NVARCHAR(100),
    MaNV INT,
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV)
);


--Bảng chấm công
CREATE TABLE ChamCong (
    MaChamCong INT PRIMARY KEY,
	Date_Chamcong DATE,
    GioPhutVao DATETIME,
    GioPhutRa DATETIME,
    MaNV INT,
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV)
);

-- Bảng kỉ luật khen thưởng
CREATE TABLE KhenThuongKyLuat (
    MaKTKL INT PRIMARY KEY,
    TenKTKL NVARCHAR(100),
    NoiDung NVARCHAR(255),
    Ngay DATE,
    MaNV INT,
    Loai NVARCHAR(50),
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV)
);


-- Tạo bảng lương
CREATE TABLE Luong (
    MaNV INT,
    MaChucVu INT,
    HoTen NVARCHAR(100),
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
    MaDuAn INT PRIMARY KEY,
    TenDuAn NVARCHAR(100),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    MoTa NVARCHAR(255),
    MaPhong INT,
    FOREIGN KEY (MaPhong) REFERENCES PhongBan(MaPhong)
);


-- Tạo bảng Phân công
CREATE TABLE PhanCong (
    MaPhanCong INT PRIMARY KEY,
    NgayThamGia DATE,
    NgayKetThuc DATE,
    MaNV INT,
    MaDuAn INT,
    VaiTro NVARCHAR(100),
    FOREIGN KEY (MaNV) REFERENCES HoSoNhanVien(MaNV),
    FOREIGN KEY (MaDuAn) REFERENCES DuAn(MaDuAn)
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
(1, N'Phòng Kỹ thuật'),
(2, N'Phòng Kinh doanh'),
(3, N'Phòng Nhân sự'),
(4, N'Phòng Tài chính');

INSERT INTO ACCOUNTS(TENDANGNHAP,MATKHAU,PHANQUYEN,EMAIL)
VALUES
('Thachbao',N'1234567','ADMIN','thachbao2910@gmail.com'),
('Hoanghai',N'2345678','QLCHAMCONG','hoanghai@gmail.com'),
('Trongduc',N'3456789','QLLUONG','trongduc@gmail.com')

INSERT INTO ChucVu (MaChucVu, TenChucVu)
VALUES
(1, N'Giám đốc'),
(2, N'Trưởng phòng'),
(3, N'Nhân viên'),
(4, N'Thư ký');


INSERT INTO PhongBan (MaPhong, TenPhong)
VALUES
(1, N'Phòng IT'),
(2, N'Phòng Kế toán'),
(3, N'Phòng Hành chính'),
(4, N'Phòng Marketing');

INSERT INTO TrinhDoNangLuc (MaTrinhDo, TenTrinhDo)
VALUES
(1, N'Cử nhân'),
(2, N'Thạc sĩ'),
(3, N'Kỹ sư'),
(4, N'Tiến sĩ');


INSERT INTO HoSoNhanVien (MaNV, HoTen, GioiTinh, NgaySinh, DiaChi, CCCD, MaChucVu, MaBoPhan, MaPhong, TrangThai, MaTrinhDo)
VALUES
(1, N'Nguyễn Văn A', N'Nam', '1990-01-01', N'Hà Nội', N'205456240539', 1, 1, 1, N'Đang làm việc', 1),
(2, N'Trần Thị B', N'Nữ', '1992-02-15', N'Hà Nội', N'177186313065', 2, 2, 2, N'Đang làm việc', 2),
(3, N'Nguyễn Văn C', N'Nam', '1995-03-10', N'TP.HCM', N'072146665526', 3, 1, 1, N'Đang làm việc', 3),
(4, N'Phạm Thị D', N'Nữ', '1994-04-20', N'TP.HCM', N'887073099882', 4, 2, 2, N'Đang làm việc', 1),
(5, N'Hoàng Văn E', N'Nam', '1988-05-25', N'Hải Phòng', N'368003773012', 1, 3, 3, N'Đang làm việc', 2),
(6, N'Phạm Văn F', N'Nam', '1989-06-30', N'Đà Nẵng', N'925615126393', 2, 4, 4, N'Nghỉ việc', 3),
(7, N'Lê Thị G', N'Nữ', '1993-07-12', N'Quảng Ninh', N'934076430789', 3, 3, 3, N'Đang làm việc', 1),
(8, N'Vũ Văn H', N'Nam', '1991-08-18', N'Vũng Tàu', N'981000009525', 4, 4, 4, N'Nghỉ việc', 2),
(9, N'Ngô Thị I', N'Nữ', '1996-09-21', N'Huế', N'831113591270', 1, 1, 1, N'Đang làm việc', 3),
(10, N'Phan Văn K', N'Nam', '1997-10-11', N'Cần Thơ', N'407323696288', 2, 2, 2, N'Đang làm việc', 1);


INSERT INTO HopDong (SoHopDong, NgayBatDau, NgayKetThuc, HoTenNV, NoiDung, ThoiHan, HeSoLuong, MaNV)
VALUES
(1, '2021-01-01', '2023-01-01', N'Nguyễn Văn A', N'Hợp đồng lao động 3 năm', 36, 1.2, 1),
(2, '2021-02-01', '2023-02-01', N'Trần Thị B', N'Hợp đồng lao động 3 năm', 36, 1.1, 2),
(3, '2022-03-01', '2024-03-01', N'Nguyễn Văn C', N'Hợp đồng lao động 2 năm', 24, 1.5, 3),
(4, '2020-04-01', '2023-04-01', N'Phạm Thị D', N'Hợp đồng lao động 3 năm', 36, 1.3, 4),
(5, '2021-05-01', '2024-05-01', N'Hoàng Văn E', N'Hợp đồng lao động 3 năm', 36, 1.2, 5),
(6, '2022-06-01', '2024-06-01', N'Phạm Văn F', N'Hợp đồng lao động 2 năm', 24, 1.1, 6),
(7, '2020-07-01', '2023-07-01', N'Lê Thị G', N'Hợp đồng lao động 3 năm', 36, 1.5, 7),
(8, '2019-08-01', '2022-08-01', N'Vũ Văn H', N'Hợp đồng lao động 3 năm', 36, 1.4, 8),
(9, '2022-09-01', '2025-09-01', N'Ngô Thị I', N'Hợp đồng lao động 3 năm', 36, 1.6, 9),
(10, '2021-10-01', '2024-10-01', N'Phan Văn K', N'Hợp đồng lao động 3 năm', 36, 1.3, 10);





INSERT INTO BaoHiem (MaBaoHiem, SoBaoHiem, NgayCap, NoiCap, NoiKhamBenh, MaNV)
VALUES
(1, N'1234567890', '2020-01-01', N'Hà Nội', N'Bệnh viện Bạch Mai', 1),
(2, N'1234567891', '2020-02-01', N'Hà Nội', N'Bệnh viện Việt Đức', 2),
(3, N'1234567892', '2021-03-01', N'TP.HCM', N'Bệnh viện Chợ Rẫy', 3),
(4, N'1234567893', '2019-04-01', N'TP.HCM', N'Bệnh viện Bình Dân', 4),
(5, N'1234567894', '2022-05-01', N'Hải Phòng', N'Bệnh viện Việt Tiệp', 5),
(6, N'1234567895', '2021-06-01', N'Đà Nẵng', N'Bệnh viện C Đà Nẵng', 6),
(7, N'1234567896', '2020-07-01', N'Quảng Ninh', N'Bệnh viện Sản Nhi', 7),
(8, N'1234567897', '2019-08-01', N'Vũng Tàu', N'Bệnh viện Vũng Tàu', 8),
(9, N'1234567898', '2022-09-01', N'Huế', N'Bệnh viện Trung ương Huế', 9),
(10, N'1234567899', '2021-10-01', N'Cần Thơ', N'Bệnh viện Đa khoa Cần Thơ', 10);


INSERT INTO ChamCong (MaChamCong, Date_Chamcong, GioPhutVao, GioPhutRa, MaNV)
VALUES
(1, '2023-10-01', '2023-10-01 08:00:00', '2023-10-01 17:00:00', 1),
(2, '2023-10-02', '2023-10-02 08:00:00', '2023-10-02 17:00:00', 2),
(3, '2023-10-03', '2023-10-03 08:00:00', '2023-10-03 17:00:00', 3),
(4, '2023-10-04', '2023-10-04 08:00:00', '2023-10-04 17:00:00', 4),
(5, '2023-10-05', '2023-10-05 08:00:00', '2023-10-05 17:00:00', 5),
(6, '2023-10-06', '2023-10-06 08:00:00', '2023-10-06 17:00:00', 6),
(7, '2023-10-07', '2023-10-07 08:00:00', '2023-10-07 17:00:00', 7),
(8, '2023-10-08', '2023-10-08 08:00:00', '2023-10-08 17:00:00', 8),
(9, '2023-10-09', '2023-10-09 08:00:00', '2023-10-09 17:00:00', 9),
(10, '2023-10-10', '2023-10-10 08:00:00', '2023-10-10 17:00:00', 10);


INSERT INTO KhenThuongKyLuat (MaKTKL, TenKTKL, NoiDung, Ngay, MaNV, Loai)
VALUES
(1, N'Khen thưởng tháng 1', N'Hoàn thành xuất sắc công việc', '2023-01-31', 1, N'Khen thưởng'),
(2, N'Khen thưởng tháng 2', N'Đóng góp tích cực vào dự án', '2023-02-28', 2, N'Khen thưởng'),
(3, N'Kỉ luật tháng 3', N'Đi làm muộn 3 ngày liên tiếp', '2023-03-15', 3, N'Kỉ luật'),
(4, N'Khen thưởng tháng 4', N'Đề xuất giải pháp cải tiến quy trình', '2023-04-30', 4, N'Khen thưởng'),
(5, N'Khen thưởng tháng 5', N'Hỗ trợ dự án vượt qua khó khăn', '2023-05-31', 5, N'Khen thưởng'),
(6, N'Kỉ luật tháng 6', N'Vi phạm quy định công ty', '2023-06-15', 6, N'Kỉ luật'),
(7, N'Khen thưởng tháng 7', N'Tích cực tham gia hoạt động công ty', '2023-07-31', 7, N'Khen thưởng'),
(8, N'Kỉ luật tháng 8', N'Không hoàn thành nhiệm vụ đúng hạn', '2023-08-15', 8, N'Kỉ luật'),
(9, N'Khen thưởng tháng 9', N'Hỗ trợ đồng nghiệp trong dự án', '2023-09-30', 9, N'Khen thưởng'),
(10, N'Khen thưởng tháng 10', N'Đóng góp lớn vào doanh số quý 3', '2023-10-10', 10, N'Khen thưởng');


INSERT INTO Luong (MaNV, MaChucVu, HoTen, TongChiPhi, LuongThucNhan, BaoHiemNhanSu, LuongNhanSu)
VALUES
(1, 1, N'Nguyễn Văn A', 30000000, 25000000, 3000000, 2000000),
(2, 2, N'Trần Thị B', 28000000, 23000000, 2800000, 2200000),
(3, 3, N'Nguyễn Văn C', 27000000, 22000000, 2700000, 2300000),
(4, 4, N'Phạm Thị D', 26000000, 21000000, 2600000, 2400000),
(5, 1, N'Hoàng Văn E', 31000000, 26000000, 3100000, 2500000),
(6, 2, N'Phạm Văn F', 25000000, 20000000, 2500000, 2500000),
(7, 3, N'Lê Thị G', 29000000, 24000000, 2900000, 2600000),
(8, 4, N'Vũ Văn H', 30000000, 25000000, 3000000, 2500000),
(9, 1, N'Ngô Thị I', 32000000, 27000000, 3200000, 2700000),
(10, 2, N'Phan Văn K', 26000000, 22000000, 2600000, 2400000);


INSERT INTO DuAn (MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, MoTa, MaPhong)
VALUES
(1, N'Dự án A', '2023-01-01', '2023-06-30', N'Dự án phát triển phần mềm quản lý', 1),
(2, N'Dự án B', '2023-02-01', '2023-07-31', N'Dự án thiết kế hệ thống mạng', 2),
(3, N'Dự án C', '2023-03-01', '2023-08-31', N'Dự án xây dựng cơ sở dữ liệu', 3),
(4, N'Dự án D', '2023-04-01', '2023-09-30', N'Dự án cải tiến quy trình làm việc', 4),
(5, N'Dự án E', '2023-05-01', '2023-10-31', N'Dự án phát triển ứng dụng di động', 1),
(6, N'Dự án F', '2023-06-01', '2023-11-30', N'Dự án nâng cấp hệ thống bảo mật', 2),
(7, N'Dự án G', '2023-07-01', '2023-12-31', N'Dự án tối ưu hóa quy trình sản xuất', 3),
(8, N'Dự án H', '2023-08-01', '2024-01-31', N'Dự án xây dựng chiến lược marketing', 4),
(9, N'Dự án I', '2023-09-01', '2024-02-28', N'Dự án phát triển website', 1),
(10, N'Dự án J', '2023-10-01', '2024-03-31', N'Dự án đào tạo nhân viên', 2);


INSERT INTO PhanCong (MaPhanCong, NgayThamGia, NgayKetThuc, MaNV, MaDuAn, VaiTro)
VALUES
(1, '2023-01-10', '2023-06-20', 1, 1, N'Trưởng nhóm'),
(2, '2023-02-15', '2023-07-15', 2, 2, N'Nhân viên'),
(3, '2023-03-20', '2023-08-20', 3, 3, N'Trưởng nhóm'),
(4, '2023-04-25', '2023-09-25', 4, 4, N'Nhân viên'),
(5, '2023-05-30', '2023-10-30', 5, 5, N'Trưởng nhóm'),
(6, '2023-06-05', '2023-11-05', 6, 6, N'Nhân viên'),
(7, '2023-07-10', '2023-12-10', 7, 7, N'Trưởng nhóm'),
(8, '2023-08-15', '2024-01-15', 8, 8, N'Nhân viên'),
(9, '2023-09-20', '2024-02-20', 9, 9, N'Trưởng nhóm'),
(10, '2023-10-25', '2024-03-25', 10, 10, N'Nhân viên');



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