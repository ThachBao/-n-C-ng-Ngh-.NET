-- Tạo cơ sở dữ liệu QL_NHANSU
CREATE DATABASE QL_NHANSU;
GO

-- Sử dụng cơ sở dữ liệu QL_NHANSU
USE QL_NHANSU;
GO

--Bảng phòng ban
CREATE TABLE PhongBan (
    MaPhong VARCHAR(50) PRIMARY KEY,
    TenPhong NVARCHAR(100),
    MoTa NVARCHAR(255)
);

-- Tạo bảng Bộ phận
CREATE TABLE BoPhan (
    MaBoPhan VARCHAR(50) PRIMARY KEY,
    TenBoPhan NVARCHAR(100),
    MoTa NVARCHAR(255)
);

-- Tạo bảng liên kết giữa Bộ phận và Phòng ban
CREATE TABLE BoPhan_PhongBan (
    MaBoPhan VARCHAR(50),
    MaPhong VARCHAR(50),
    PRIMARY KEY (MaBoPhan, MaPhong),
    FOREIGN KEY (MaBoPhan) REFERENCES BoPhan(MaBoPhan),
    FOREIGN KEY (MaPhong) REFERENCES PhongBan(MaPhong)
);

--Bảng chức vụ:
CREATE TABLE ChucVu (
    MaChucVu VARCHAR(50) PRIMARY KEY,
    TenChucVu NVARCHAR(100)
);

-- Bảng trình độ năng lực
CREATE TABLE TrinhDoNangLuc (
    MaTrinhDo VARCHAR(50) PRIMARY KEY,
    TenTrinhDo NVARCHAR(100)
);

-- Tạo bảng Nhân Viên
CREATE TABLE NhanVien (
    MaNV VARCHAR(50) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(3) Check(GioiTinh in ('Nam', N'Nữ')),
    NgaySinh DATE,
    DiaChi NVARCHAR(255),
    CCCD VARCHAR(12),
	Hinhanh VARCHAR(100),
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
    MaNV VARCHAR(50),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);


-- Bảng bảo hiểm
CREATE TABLE BaoHiem (
    MaBaoHiem INT PRIMARY KEY,
    SoBaoHiem NVARCHAR(50),
    NgayCap DATE,
    NoiCap NVARCHAR(100),
    NoiKhamBenh NVARCHAR(100),
    MaNV VARCHAR(50),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);


-- Tạo bảng CHAMCONG
CREATE TABLE CHAMCONG
(
    ID_CHAMCONG CHAR(20) NOT NULL,
    NGAYVAO DATETIME,
	TRANGTHAIVAO BIT NULL,
	TRANGTHAIRA BIT NULL,
    MaNV VARCHAR(50) NOT NULL,
    CONSTRAINT PK_CHAMCONG PRIMARY KEY (ID_CHAMCONG, MaNV),
	CONSTRAINT FK_CHAMCONG_EMPLOYEE FOREIGN KEY (MANV) REFERENCES NhanVien(MANV)
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
    MaNV VARCHAR(50) NOT NULL,
    NGAY INT,
    THANG INT,
    NAM INT,
    ID_CHAMCONG CHAR(20),
    TRANGTHAI NVARCHAR(20),
    CONSTRAINT PK_BANGCONG PRIMARY KEY (ID_BANGCONG),
    CONSTRAINT FK_BANGCONG_EMPLOYEE FOREIGN KEY (MANV) REFERENCES NhanVien(MANV),
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



--chay trigger de them du lieu vao bang cong 
CREATE TRIGGER THEMDULIEUVAOBANGCONG
ON CHAMCONG
FOR INSERT
AS
BEGIN
    -- Chèn dữ liệu vào bảng BANGCONG với các bản ghi vừa được thêm vào bảng CHAMCONG
    INSERT INTO BANGCONG (ID_BANGCONG, MANV, NGAY, THANG, NAM, ID_CHAMCONG)
    SELECT 
        NEWID(),  -- Sinh ID_BANGCONG mới
        i.MANV,  -- Lấy MANV từ bảng ảo INSERTED
        DAY(GETDATE()) AS NGAY,  -- Lấy ngày từ THOIGIANVAO trong bảng ảo
        MONTH(GETDATE()) AS THANG,  -- Lấy tháng từ THOIGIANVAO trong bảng ảo
        YEAR(GETDATE()) AS NAM,  -- Lấy năm từ THOIGIANVAO trong bảng ảo
        i.ID_CHAMCONG  -- Lấy ID_CHAMCONG từ bảng ảo INSERTED
		-- Giá trị cho cột TRANGTHAI
    FROM 
        INSERTED i;  -- Lấy dữ liệu từ bảng ảo INSERTED (các bản ghi mới được thêm)
END;
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
		WHEN CH.TRANGTHAIVAO =NULL OR CH.TRANGTHAIRA = NULL THEN N'Chưa Hoàn Thành'
        ELSE N'Hoàn Thành'
    END
    FROM BANGCONG BG
    INNER JOIN inserted I ON I.ID_CHAMCONG = BG.ID_CHAMCONG
    INNER JOIN CHAMCONG CH ON I.ID_CHAMCONG = CH.ID_CHAMCONG AND I.MANV = CH.MANV;
END;
GO

-- Bảng kỉ luật khen thưởng
CREATE TABLE KhenThuongKyLuat (
    MaKTKL INT PRIMARY KEY,
    TenKTKL NVARCHAR(100),
    NoiDung NVARCHAR(255),
    Ngay DATE,
    MaNV VARCHAR(50),
    Loai NVARCHAR(50),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);


-- Tạo bảng lương
CREATE TABLE Luong (
    MaNV VARCHAR(50),
    MaChucVu VARCHAR(50),
    --HoTen NVARCHAR(100),
    TongChiPhi DECIMAL(18, 2),
    LuongThucNhan DECIMAL(18, 2),
    BaoHiemNhanSu DECIMAL(18, 2),
    LuongNhanSu DECIMAL(18, 2),
    PRIMARY KEY (MaNV),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
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
-- Tạo bảng đầu việc dự án
CREATE TABLE DAUVIECDUAN
(
	MaDauViec VARCHAR(20),
	TenDauViec NVARCHAR(255),
	 MaDuAn VARCHAR(50),
	MaNV VARCHAR(50),
	CONSTRAINT PK_DAUVIECDUAN PRIMARY KEY(MaDauViec,MaDuAn),
	CONSTRAINT FK_DAUVIECDUAN_NHANVIEN FOREIGN  KEY (MaNV) REFERENCES NhanVien(MaNV),
	CONSTRAINT FK_DAUVIECDUAN_DUAN FOREIGN  KEY (MaDuAn) REFERENCES DuAn(MaDuAn)
)
-- Tạo bảng Phân công
CREATE TABLE PhanCong (
    MaPhanCong VARCHAR(50) PRIMARY KEY,
    NgayThamGia DATE,
    NgayKetThuc DATE,
    MaNV VARCHAR(50),
    MaDuAn VARCHAR(50),
    VaiTro NVARCHAR(100),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaDuAn) REFERENCES DuAn(MaDuAn)
);


-- Tạo bảng DaoTao
CREATE TABLE DaoTao (
    MaDaoTao VARCHAR(50) ,
    TenKhoaHoc NVARCHAR(100),
    ThoiGianBatDau DATE,
    ThoiGianKetThuc DATE,
    NoiDung NVARCHAR(255),
    MaNV VARCHAR(50),
	CONSTRAINT PK_DAOTAO PRIMARY KEY(MaDaoTao,MaNV),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);




CREATE TABLE ACCOUNTS (
    ACC_ID INT IDENTITY(1,1) PRIMARY KEY,
	TENDANGNHAP VARCHAR(50),
    MATKHAU NVARCHAR(50) NOT NULL,
    PHANQUYEN NVARCHAR(10) CHECK(PHANQUYEN IN('ADMIN', 'QLCHAMCONG','QLLUONG')),
    EMAIL NVARCHAR(50),
);
GO

-- ================================================================================
-- INSERT INTO PhongBan (sửa lại mã phòng ban cho tính duy nhất)
INSERT INTO PhongBan (MaPhong, TenPhong, MoTa)
VALUES
('PB01', N'Phòng Kỹ Thuật', N'Chịu trách nhiệm về công nghệ và kỹ thuật.'),
('PB02', N'Phòng Kế toán', N'Quản lý tài chính và kế toán của công ty.'),
('PB03', N'Phòng Nhân Sự', N'Tuyển dụng, đào tạo và quản lý nhân sự.'),
('PB04', N'Phòng Marketing', N'Tiếp thị và phát triển thương hiệu.'),
('PB05', N'Phòng Nghiên cứu và Phát triển', N'Nghiên cứu và phát triển sản phẩm mới.'),
('PB06', N'Phòng Hỗ trợ Khách hàng', N'Cung cấp dịch vụ hỗ trợ và chăm sóc khách hàng.'),
('PB07', N'Phòng Bán hàng', N'Chịu trách nhiệm về doanh thu và bán hàng.'),
('PB08', N'Phòng IT', N'Quản lý hệ thống công nghệ thông tin.'),
('PB09', N'Phòng Pháp lý', N'Đảm bảo tuân thủ pháp luật và các quy định.'),
('PB10', N'Phòng Chiến lược', N'Lập kế hoạch chiến lược dài hạn cho công ty.');

INSERT INTO BoPhan (MaBoPhan, TenBoPhan, MoTa)
VALUES
('BP01', N'Bộ Phận IT', N'Cung cấp giải pháp công nghệ cho doanh nghiệp.'),
('BP02', N'Bộ Phận Tiếp Thị', N'Quản lý hoạt động tiếp thị và quảng cáo.'),
('BP03', N'Bộ Phận Tuyển Dụng', N'Tìm kiếm và tuyển dụng ứng viên.'),
('BP04', N'Bộ Phận Tài Chính', N'Quản lý ngân sách và tài chính.'),
('BP05', N'Bộ Phận Nghiên cứu và Phát triển', N'Nghiên cứu và phát triển sản phẩm mới.'),
('BP06', N'Bộ Phận Hỗ trợ Khách hàng', N'Giải quyết vấn đề và hỗ trợ khách hàng.'),
('BP07', N'Bộ Phận Bán hàng', N'Thực hiện các hoạt động bán hàng và chăm sóc khách hàng.'),
('BP08', N'Bộ Phận Pháp lý', N'Đảm bảo tuân thủ các quy định pháp luật.'),
('BP09', N'Bộ Phận Chiến lược', N'Lập kế hoạch và chiến lược kinh doanh.'),
('BP10', N'Bộ Phận Đào tạo', N'Tổ chức các khóa đào tạo cho nhân viên.');

INSERT INTO BoPhan_PhongBan (MaBoPhan, MaPhong)
VALUES
('BP01', 'PB01'), -- Bộ Phận IT có Phòng Kỹ Thuật
('BP01', 'PB08'), -- Bộ Phận IT cũng có Phòng IT
('BP02', 'PB04'), -- Bộ Phận Tiếp Thị có Phòng Marketing
('BP03', 'PB03'), -- Bộ Phận Tuyển Dụng có Phòng Nhân Sự
('BP04', 'PB02'), -- Bộ Phận Tài Chính có Phòng Kế toán
('BP05', 'PB05'), -- Bộ Phận Nghiên cứu và Phát triển có Phòng Nghiên cứu và Phát triển
('BP06', 'PB06'), -- Bộ Phận Hỗ trợ Khách hàng có Phòng Hỗ trợ Khách hàng
('BP07', 'PB07'), -- Bộ Phận Bán hàng có Phòng Bán hàng
('BP08', 'PB09'), -- Bộ Phận Pháp lý có Phòng Pháp lý
('BP09', 'PB10'), -- Bộ Phận Chiến lược có Phòng Chiến lược
('BP02', 'PB02'), -- Bộ Phận Tiếp Thị có thể liên kết tới Phòng Kế toán
('BP03', 'PB04'), -- Bộ Phận Tuyển Dụng cũng có thể liên kết tới Phòng Marketing
('BP06', 'PB04'), -- Bộ Phận Hỗ trợ Khách hàng có thể làm việc với Phòng Marketing
('BP07', 'PB05'); -- Bộ Phận Bán hàng có thể có liên kết tới Phòng Nghiên cứu và Phát triển

SELECT * FROM BoPhan
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


SET DATEFORMAT DMY
INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, NgaySinh, DiaChi, CCCD, Hinhanh,MaChucVu, MaBoPhan, MaPhong, TrangThai, MaTrinhDo)
VALUES
('NV001', N'Nguyễn Văn A', N'Nam', '01/01/1990', N'Hà Nội', N'205456240539', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh1.jpg','CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD01'),
('NV002', N'Trần Thị B', N'Nữ', '15/02/1992', N'Hà Nội', N'177186313065', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh2.png','CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD02'),
('NV003', N'Nguyễn Văn C', N'Nam', '10/03/1995', N'TP.HCM', N'072146665526','C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh2.png', 'CV03', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV004', N'Phạm Thị D', N'Nữ', '20/04/1994', N'TP.HCM', N'887073099882','C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh2.png', 'CV04', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV005', N'Hoàng Văn E', N'Nam', '25/05/1998', N'Hải Phòng', N'368003773012','C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh2.png', 'CV01', 'BP03', 'PB03', N'Đang làm việc', 'TD02'),
('NV006', N'Phạm Văn F', N'Nam', '30/06/1989', N'Đà Nẵng', N'925615126393','C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh2.png', 'CV02', 'BP04', 'PB02', N'Nghỉ việc', 'TD03'),
('NV007', N'Lê Thị G', N'Nữ', '12/07/1993', N'Quảng Ninh', N'934076430789','C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh2.png', 'CV03', 'BP03', 'PB03', N'Đang làm việc', 'TD01'),
('NV008', N'Vũ Văn H', N'Nam', '18/08/1991', N'Vũng Tàu', N'981000009525','C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh2.png', 'CV04', 'BP04', 'PB02', N'Nghỉ việc', 'TD02'),
('NV009', N'Ngô Thị I', N'Nữ', '21/09/1996', N'Huế', N'831113591270','C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh1.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV010', N'Phan Văn K', N'Nam', '11/10/1997', N'Cần Thơ', N'407323696288','C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh1.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV011', N'Nguyễn Văn L', N'Nam', '02/01/1985', N'Thái Nguyên', N'123456789012', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh3.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD01'),
('NV012', N'Trần Thị M', N'Nữ', '15/02/1987', N'Hà Nội', N'234567890123', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh4.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD02'),
('NV013', N'Nguyễn Văn N', N'Nam', '10/03/1991', N'TP.HCM', N'345678901234', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh5.jpg', 'CV03', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV014', N'Phạm Thị O', N'Nữ', '20/04/1992', N'TP.HCM', N'456789012345', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh6.jpg', 'CV04', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV015', N'Hoàng Văn P', N'Nam', '25/05/1980', N'Hải Phòng', N'567890123456', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh7.jpg', 'CV01', 'BP03', 'PB03', N'Đang làm việc', 'TD02'),
('NV016', N'Phạm Văn Q', N'Nam', '30/06/1988', N'Đà Nẵng', N'678901234567', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh8.jpg', 'CV02', 'BP04', 'PB02', N'Nghỉ việc', 'TD03'),
('NV017', N'Lê Thị R', N'Nữ', '12/07/1993', N'Quảng Ninh', N'789012345678', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh9.jpg', 'CV03', 'BP03', 'PB03', N'Đang làm việc', 'TD01'),
('NV018', N'Vũ Văn S', N'Nam', '18/08/1991', N'Vũng Tàu', N'890123456789', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh10.jpg', 'CV04', 'BP04', 'PB02', N'Nghỉ việc', 'TD02'),
('NV019', N'Ngô Thị T', N'Nữ', '21/09/1995', N'Huế', N'901234567890', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh3.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV020', N'Phan Văn U', N'Nam', '11/10/1996', N'Cần Thơ', N'012345678901', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh4.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV021', N'Nguyễn Văn V', N'Nam', '01/01/1984', N'Hà Nội', N'123456789013', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh5.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD02'),
('NV022', N'Trần Thị W', N'Nữ', '15/02/1986', N'Hà Nội', N'234567890124', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh6.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV023', N'Nguyễn Văn X', N'Nam', '10/03/1989', N'TP.HCM', N'345678901235', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh7.jpg', 'CV03', 'BP01', 'PB01', N'Đang làm việc', 'TD02'),
('NV024', N'Phạm Thị Y', N'Nữ', '20/04/1988', N'TP.HCM', N'456789012346', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh8.jpg', 'CV04', 'BP02', 'PB04', N'Đang làm việc', 'TD03'),
('NV025', N'Hoàng Văn Z', N'Nam', '25/05/1990', N'Hải Phòng', N'567890123457', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh9.jpg', 'CV01', 'BP03', 'PB03', N'Đang làm việc', 'TD01'),
('NV026', N'Phạm Văn AA', N'Nam', '30/06/1985', N'Đà Nẵng', N'678901234568', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh10.jpg', 'CV02', 'BP04', 'PB02', N'Nghỉ việc', 'TD02'),('NV027', N'Nguyễn Văn AB', N'Nam', '02/01/1980', N'Thái Nguyên', N'123456789014', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh11.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD01'),
('NV028', N'Trần Thị AC', N'Nữ', '15/02/1985', N'Hà Nội', N'234567890125', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh12.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD02'),
('NV029', N'Nguyễn Văn AD', N'Nam', '10/03/1992', N'TP.HCM', N'345678901236', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh13.jpg', 'CV03', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV030', N'Phạm Thị AE', N'Nữ', '20/04/1990', N'TP.HCM', N'456789012347', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh14.jpg', 'CV04', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV031', N'Hoàng Văn AF', N'Nam', '25/05/1986', N'Hải Phòng', N'567890123458', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh15.jpg', 'CV01', 'BP03', 'PB03', N'Đang làm việc', 'TD02'),
('NV032', N'Phạm Văn AG', N'Nam', '30/06/1988', N'Đà Nẵng', N'678901234569', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh16.jpg', 'CV02', 'BP04', 'PB02', N'Nghỉ việc', 'TD03'),
('NV033', N'Lê Thị AH', N'Nữ', '12/07/1991', N'Quảng Ninh', N'789012345679', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh17.jpg', 'CV03', 'BP03', 'PB03', N'Đang làm việc', 'TD01'),
('NV034', N'Vũ Văn AI', N'Nam', '18/08/1987', N'Vũng Tàu', N'890123456790', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh18.jpg', 'CV04', 'BP04', 'PB02', N'Nghỉ việc', 'TD02'),
('NV035', N'Ngô Thị AJ', N'Nữ', '21/09/1995', N'Huế', N'901234567891', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh19.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV036', N'Phan Văn AK', N'Nam', '11/10/1996', N'Cần Thơ', N'012345678902', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh20.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV037', N'Nguyễn Văn AL', N'Nam', '01/01/1990', N'Hà Nội', N'123456789015', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh21.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD02'),
('NV038', N'Trần Thị AM', N'Nữ', '15/02/1993', N'Hà Nội', N'234567890126', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh22.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV039', N'Nguyễn Văn AN', N'Nam', '10/03/1988', N'TP.HCM', N'345678901237', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh23.jpg', 'CV03', 'BP01', 'PB01', N'Đang làm việc', 'TD02'),
('NV040', N'Phạm Thị AO', N'Nữ', '20/04/1992', N'TP.HCM', N'456789012348', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh24.jpg', 'CV04', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV041', N'Hoàng Văn AP', N'Nam', '25/05/1996', N'Hải Phòng', N'567890123459', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh25.jpg', 'CV01', 'BP03', 'PB03', N'Đang làm việc', 'TD02'),
('NV042', N'Phạm Văn AQ', N'Nam', '30/06/1994', N'Đà Nẵng', N'678901234570', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh26.jpg', 'CV02', 'BP04', 'PB02', N'Nghỉ việc', 'TD03'),
('NV043', N'Lê Thị AR', N'Nữ', '12/07/1991', N'Quảng Ninh', N'789012345680', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh27.jpg', 'CV03', 'BP03', 'PB03', N'Đang làm việc', 'TD01'),
('NV044', N'Vũ Văn AS', N'Nam', '18/08/1992', N'Vũng Tàu', N'890123456801', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh28.jpg', 'CV04', 'BP04', 'PB02', N'Nghỉ việc', 'TD02'),
('NV045', N'Ngô Thị AT', N'Nữ', '21/09/1990', N'Huế', N'901234567902', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh29.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD03'),
('NV046', N'Phan Văn AU', N'Nam', '11/10/1994', N'Cần Thơ', N'012345678903', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh30.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV047', N'Nguyễn Văn AV', N'Nam', '01/01/1992', N'Hà Nội', N'123456789016', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh31.jpg', 'CV01', 'BP01', 'PB01', N'Đang làm việc', 'TD02'),
('NV048', N'Trần Thị AW', N'Nữ', '15/02/1993', N'Hà Nội', N'234567890127', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh32.jpg', 'CV02', 'BP02', 'PB04', N'Đang làm việc', 'TD01'),
('NV049', N'Nguyễn Văn AX', N'Nam', '10/03/1987', N'TP.HCM', N'345678901238', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh33.jpg', 'CV03', 'BP01', 'PB01', N'Đang làm việc', 'TD02'),
('NV050', N'Phạm Thị AY', N'Nữ', '20/04/1991', N'TP.HCM', N'456789012349', 'C:\Running DotNet\DA_CNNET\HMresourcemanagementsystem\HMresourcemanagementsystem\Image\hinh34.jpg', 'CV04', 'BP02', 'PB04', N'Đang làm việc', 'TD01');

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
    ('CC010', 'NV010'),
    ('CC011', 'NV011'),
    ('CC012', 'NV012'),
    ('CC013', 'NV013'),
    ('CC014', 'NV014'),
    ('CC015', 'NV015'),
    ('CC016', 'NV016'),
    ('CC017', 'NV017'),
    ('CC018', 'NV018'),
    ('CC019', 'NV019'),
    ('CC020', 'NV020'),
    ('CC021', 'NV021'),
    ('CC022', 'NV022'),
    ('CC023', 'NV023'),
    ('CC024', 'NV024'),
    ('CC025', 'NV025'),
    ('CC026', 'NV026'),
    ('CC027', 'NV027'),
    ('CC028', 'NV028'),
    ('CC029', 'NV029'),
    ('CC030', 'NV030'),
    ('CC031', 'NV031'),
    ('CC032', 'NV032'),
    ('CC033', 'NV033'),
    ('CC034', 'NV034'),
    ('CC035', 'NV035'),
    ('CC036', 'NV036'),
    ('CC037', 'NV037'),
    ('CC038', 'NV038'),
    ('CC039', 'NV039'),
    ('CC040', 'NV040'),
    ('CC041', 'NV041'),
    ('CC042', 'NV042'),
    ('CC043', 'NV043'),
    ('CC044', 'NV044'),
    ('CC045', 'NV045'),
    ('CC046', 'NV046'),
    ('CC047', 'NV047'),
    ('CC048', 'NV048'),
    ('CC049', 'NV049'),
    ('CC050', 'NV050');


--INSERT INTO KhenThuongKyLuat (MaKTKL, TenKTKL, NoiDung, Ngay, MANV, Loai)
--VALUES
--(1, N'Khen thưởng tháng 1', N'Hoàn thành xuất sắc công việc', '2023-01-31', 1, N'Khen thưởng'),
--(2, N'Khen thưởng tháng 2', N'Đóng góp tích cực vào dự án', '2023-02-28', 2, N'Khen thưởng'),
--(3, N'Kỉ luật tháng 3', N'Đi làm muộn 3 ngày liên tiếp', '2023-03-15', 3, N'Kỉ luật'),
--(4, N'Khen thưởng tháng 4', N'Đề xuất giải pháp cải tiến quy trình', '2023-04-30', 4, N'Khen thưởng'),
--(5, N'Khen thưởng tháng 5', N'Hỗ trợ dự án vượt qua khó khăn', '2023-05-31', 5, N'Khen thưởng'),
--(6, N'Kỉ luật tháng 6', N'Vi phạm quy định công ty', '2023-06-15', 6, N'Kỉ luật'),
--(7, N'Khen thưởng tháng 7', N'Tích cực tham gia hoạt động công ty', '2023-07-31', 7, N'Khen thưởng'),
--(8, N'Kỉ luật tháng 8', N'Không hoàn thành nhiệm vụ đúng hạn', '2023-08-15', 8, N'Kỉ luật'),
--(9, N'Khen thưởng tháng 9', N'Hỗ trợ đồng nghiệp trong dự án', '2023-09-30', 9, N'Khen thưởng'),
--(10, N'Khen thưởng tháng 10', N'Đóng góp lớn vào doanh số quý 3', '2023-10-10', 10, N'Khen thưởng');


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
('NV005', 'CV01',  31000000, 26000000, 3100000, 2500000),
('NV006', 'CV02',  25000000, 20000000, 2500000, 2500000),
('NV007', 'CV03',  29000000, 24000000, 2900000, 2600000),
('NV008', 'CV04',  30000000, 25000000, 3000000, 2500000),
('NV009', 'CV01',  32000000, 27000000, 3200000, 2700000),
('NV010', 'CV02',  26000000, 22000000, 2600000, 2400000),
('NV011', 'CV03',  28000000, 23500000, 2800000, 2400000),
('NV012', 'CV04',  29000000, 24500000, 2900000, 2500000),
('NV013', 'CV01',  31000000, 26000000, 3100000, 2500000),
('NV014', 'CV02',  27000000, 21500000, 2700000, 2200000),
('NV015', 'CV03',  30000000, 25000000, 3000000, 2600000),
('NV016', 'CV04',  32000000, 27000000, 3200000, 2700000),
('NV017', 'CV01',  33000000, 28000000, 3300000, 2800000),
('NV018', 'CV02',  29000000, 24000000, 2900000, 2500000),
('NV019', 'CV03',  31000000, 26000000, 3100000, 2700000),
('NV020', 'CV04',  32000000, 27000000, 3200000, 2600000),
('NV021', 'CV01',  30000000, 25000000, 3000000, 2500000),
('NV022', 'CV02',  31000000, 26000000, 3100000, 2700000),
('NV023', 'CV03',  28000000, 23000000, 2800000, 2400000),
('NV024', 'CV04',  29000000, 24000000, 2900000, 2500000),
('NV025', 'CV01',  26000000, 21000000, 2600000, 2200000),
('NV026', 'CV02',  25000000, 20000000, 2500000, 2400000),
('NV027', 'CV03',  27000000, 22000000, 2700000, 2300000),
('NV028', 'CV04',  30000000, 25000000, 3000000, 2500000),
('NV029', 'CV01',  32000000, 27000000, 3200000, 2700000),
('NV030', 'CV02',  26000000, 22000000, 2600000, 2400000),
('NV031', 'CV03',  29000000, 24000000, 2900000, 2600000),
('NV032', 'CV04',  30000000, 25000000, 3000000, 2500000),
('NV033', 'CV01',  31000000, 26000000, 3100000, 2500000),
('NV034', 'CV02',  27000000, 21500000, 2700000, 2200000),
('NV035', 'CV03',  32000000, 27000000, 3200000, 2700000),
('NV036', 'CV04',  29000000, 24000000, 2900000, 2500000),
('NV037', 'CV01',  33000000, 28000000, 3300000, 2800000),
('NV038', 'CV02',  30000000, 25000000, 3000000, 2600000),
('NV039', 'CV03',  31000000, 26000000, 3100000, 2700000),
('NV040', 'CV04',  32000000, 27000000, 3200000, 2600000),
('NV041', 'CV01',  30000000, 25000000, 3000000, 2500000),
('NV042', 'CV02',  31000000, 26000000, 3100000, 2700000),
('NV043', 'CV03',  28000000, 23000000, 2800000, 2400000),
('NV044', 'CV04',  29000000, 24000000, 2900000, 2500000),
('NV045', 'CV01',  26000000, 21000000, 2600000, 2200000),
('NV046', 'CV02',  25000000, 20000000, 2500000, 2400000),
('NV047', 'CV03',  27000000, 22000000, 2700000, 2300000),
('NV048', 'CV04',  30000000, 25000000, 3000000, 2500000),
('NV049', 'CV01',  32000000, 27000000, 3200000, 2700000),
('NV050', 'CV02',  26000000, 22000000, 2600000, 2400000);



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
-- Dự án A
('PC01', '2023-01-10', '2023-06-20', 'NV001', 'DA01', N'Trưởng nhóm'),
('PC02', '2023-01-10', '2023-06-20', 'NV002', 'DA01', N'Nhân viên'),
('PC03', '2023-01-10', '2023-06-20', 'NV003', 'DA01', N'Nhân viên'),
('PC04', '2023-01-10', '2023-06-20', 'NV004', 'DA01', N'Nhân viên'),
('PC05', '2023-01-10', '2023-06-20', 'NV005', 'DA01', N'Nhân viên'),
('PC06', '2023-01-10', '2023-06-20', 'NV006', 'DA01', N'Nhân viên'),
('PC07', '2023-01-10', '2023-06-20', 'NV007', 'DA01', N'Nhân viên'),
('PC08', '2023-01-10', '2023-06-20', 'NV008', 'DA01', N'Nhân viên'),
('PC09', '2023-01-10', '2023-06-20', 'NV009', 'DA01', N'Nhân viên'),
('PC10', '2023-01-10', '2023-06-20', 'NV010', 'DA01', N'Nhân viên'),

-- Dự án B
('PC11', '2023-02-15', '2023-07-15', 'NV011', 'DA02', N'Trưởng nhóm'),
('PC12', '2023-02-15', '2023-07-15', 'NV012', 'DA02', N'Nhân viên'),
('PC13', '2023-02-15', '2023-07-15', 'NV013', 'DA02', N'Nhân viên'),
('PC14', '2023-02-15', '2023-07-15', 'NV014', 'DA02', N'Nhân viên'),
('PC15', '2023-02-15', '2023-07-15', 'NV015', 'DA02', N'Nhân viên'),
('PC16', '2023-02-15', '2023-07-15', 'NV016', 'DA02', N'Nhân viên'),
('PC17', '2023-02-15', '2023-07-15', 'NV017', 'DA02', N'Nhân viên'),
('PC18', '2023-02-15', '2023-07-15', 'NV018', 'DA02', N'Nhân viên'),
('PC19', '2023-02-15', '2023-07-15', 'NV019', 'DA02', N'Nhân viên'),
('PC20', '2023-02-15', '2023-07-15', 'NV020', 'DA02', N'Nhân viên'),

-- Dự án C
('PC21', '2023-03-20', '2023-08-20', 'NV021', 'DA03', N'Trưởng nhóm'),
('PC22', '2023-03-20', '2023-08-20', 'NV022', 'DA03', N'Nhân viên'),
('PC23', '2023-03-20', '2023-08-20', 'NV023', 'DA03', N'Nhân viên'),
('PC24', '2023-03-20', '2023-08-20', 'NV024', 'DA03', N'Nhân viên'),
('PC25', '2023-03-20', '2023-08-20', 'NV025', 'DA03', N'Nhân viên'),
('PC26', '2023-03-20', '2023-08-20', 'NV026', 'DA03', N'Nhân viên'),
('PC27', '2023-03-20', '2023-08-20', 'NV027', 'DA03', N'Nhân viên'),
('PC28', '2023-03-20', '2023-08-20', 'NV028', 'DA03', N'Nhân viên'),
('PC29', '2023-03-20', '2023-08-20', 'NV029', 'DA03', N'Nhân viên'),
('PC30', '2023-03-20', '2023-08-20', 'NV030', 'DA03', N'Nhân viên'),

-- Dự án D
('PC31', '2023-04-25', '2023-09-25', 'NV031', 'DA04', N'Trưởng nhóm'),
('PC32', '2023-04-25', '2023-09-25', 'NV032', 'DA04', N'Nhân viên'),
('PC33', '2023-04-25', '2023-09-25', 'NV033', 'DA04', N'Nhân viên'),
('PC34', '2023-04-25', '2023-09-25', 'NV034', 'DA04', N'Nhân viên'),
('PC35', '2023-04-25', '2023-09-25', 'NV035', 'DA04', N'Nhân viên'),
('PC36', '2023-04-25', '2023-09-25', 'NV036', 'DA04', N'Nhân viên'),
('PC37', '2023-04-25', '2023-09-25', 'NV037', 'DA04', N'Nhân viên'),
('PC38', '2023-04-25', '2023-09-25', 'NV038', 'DA04', N'Nhân viên'),
('PC39', '2023-04-25', '2023-09-25', 'NV039', 'DA04', N'Nhân viên'),
('PC40', '2023-04-25', '2023-09-25', 'NV040', 'DA04', N'Nhân viên'),

-- Dự án E
('PC41', '2023-05-30', '2023-10-30', 'NV041', 'DA05', N'Trưởng nhóm'),
('PC42', '2023-05-30', '2023-10-30', 'NV042', 'DA05', N'Nhân viên'),
('PC43', '2023-05-30', '2023-10-30', 'NV043', 'DA05', N'Nhân viên'),
('PC44', '2023-05-30', '2023-10-30', 'NV044', 'DA05', N'Nhân viên'),
('PC45', '2023-05-30', '2023-10-30', 'NV045', 'DA05', N'Nhân viên'),
('PC46', '2023-05-30', '2023-10-30', 'NV046', 'DA05', N'Nhân viên'),
('PC47', '2023-05-30', '2023-10-30', 'NV047', 'DA05', N'Nhân viên'),
('PC48', '2023-05-30', '2023-10-30', 'NV048', 'DA05', N'Nhân viên'),
('PC49', '2023-05-30', '2023-10-30', 'NV049', 'DA05', N'Nhân viên'),
('PC50', '2023-05-30', '2023-10-30', 'NV050', 'DA05', N'Nhân viên');
INSERT INTO DaoTao (MaDaoTao, TenKhoaHoc, ThoiGianBatDau, ThoiGianKetThuc, NoiDung, MaNV)
VALUES
-- Khóa đào tạo 1: Kỹ năng lãnh đạo
('DT001', N'Kỹ năng lãnh đạo', '2023-01-01', '2023-01-05', N'Nâng cao kỹ năng quản lý và lãnh đạo', 'NV001'),
('DT001', N'Kỹ năng lãnh đạo', '2023-01-01', '2023-01-05', N'Nâng cao kỹ năng quản lý và lãnh đạo', 'NV002'),
('DT001', N'Kỹ năng lãnh đạo', '2023-01-01', '2023-01-05', N'Nâng cao kỹ năng quản lý và lãnh đạo', 'NV003'),
('DT001', N'Kỹ năng lãnh đạo', '2023-01-01', '2023-01-05', N'Nâng cao kỹ năng quản lý và lãnh đạo', 'NV004'),
('DT001', N'Kỹ năng lãnh đạo', '2023-01-01', '2023-01-05', N'Nâng cao kỹ năng quản lý và lãnh đạo', 'NV005'),

-- Khóa đào tạo 2: Giao tiếp hiệu quả
('DT002', N'Giao tiếp hiệu quả', '2023-02-10', '2023-02-15', N'Phát triển kỹ năng giao tiếp và thuyết phục', 'NV006'),
('DT002', N'Giao tiếp hiệu quả', '2023-02-10', '2023-02-15', N'Phát triển kỹ năng giao tiếp và thuyết phục', 'NV007'),
('DT002', N'Giao tiếp hiệu quả', '2023-02-10', '2023-02-15', N'Phát triển kỹ năng giao tiếp và thuyết phục', 'NV008'),
('DT002', N'Giao tiếp hiệu quả', '2023-02-10', '2023-02-15', N'Phát triển kỹ năng giao tiếp và thuyết phục', 'NV009'),
('DT002', N'Giao tiếp hiệu quả', '2023-02-10', '2023-02-15', N'Phát triển kỹ năng giao tiếp và thuyết phục', 'NV010'),

-- Khóa đào tạo 3: Thuyết trình chuyên nghiệp
('DT003', N'Thuyết trình chuyên nghiệp', '2023-03-12', '2023-03-17', N'Kỹ năng thuyết trình trước công chúng', 'NV011'),
('DT003', N'Thuyết trình chuyên nghiệp', '2023-03-12', '2023-03-17', N'Kỹ năng thuyết trình trước công chúng', 'NV012'),
('DT003', N'Thuyết trình chuyên nghiệp', '2023-03-12', '2023-03-17', N'Kỹ năng thuyết trình trước công chúng', 'NV013'),
('DT003', N'Thuyết trình chuyên nghiệp', '2023-03-12', '2023-03-17', N'Kỹ năng thuyết trình trước công chúng', 'NV014'),
('DT003', N'Thuyết trình chuyên nghiệp', '2023-03-12', '2023-03-17', N'Kỹ năng thuyết trình trước công chúng', 'NV015'),

-- Khóa đào tạo 4: Quản lý dự án
('DT004', N'Quản lý dự án', '2023-04-01', '2023-04-10', N'Học cách lập kế hoạch và quản lý dự án hiệu quả', 'NV016'),
('DT004', N'Quản lý dự án', '2023-04-01', '2023-04-10', N'Học cách lập kế hoạch và quản lý dự án hiệu quả', 'NV017'),
('DT004', N'Quản lý dự án', '2023-04-01', '2023-04-10', N'Học cách lập kế hoạch và quản lý dự án hiệu quả', 'NV018'),
('DT004', N'Quản lý dự án', '2023-04-01', '2023-04-10', N'Học cách lập kế hoạch và quản lý dự án hiệu quả', 'NV019'),
('DT004', N'Quản lý dự án', '2023-04-01', '2023-04-10', N'Học cách lập kế hoạch và quản lý dự án hiệu quả', 'NV020'),

-- Khóa đào tạo 5: Training về công nghệ mới
('DT005', N'Training về công nghệ mới', '2023-05-20', '2023-05-25', N'Cập nhật kiến thức công nghệ mới trong lĩnh vực IT', 'NV021'),
('DT005', N'Training về công nghệ mới', '2023-05-20', '2023-05-25', N'Cập nhật kiến thức công nghệ mới trong lĩnh vực IT', 'NV022'),
('DT005', N'Training về công nghệ mới', '2023-05-20', '2023-05-25', N'Cập nhật kiến thức công nghệ mới trong lĩnh vực IT', 'NV023'),
('DT005', N'Training về công nghệ mới', '2023-05-20', '2023-05-25', N'Cập nhật kiến thức công nghệ mới trong lĩnh vực IT', 'NV024'),
('DT005', N'Training về công nghệ mới', '2023-05-20', '2023-05-25', N'Cập nhật kiến thức công nghệ mới trong lĩnh vực IT', 'NV025'),

-- Khóa đào tạo 6: Kỹ năng đàm phán
('DT006', N'Kỹ năng đàm phán', '2023-06-15', '2023-06-20', N'Phát triển kỹ năng đàm phán và thương lượng', 'NV026'),
('DT006', N'Kỹ năng đàm phán', '2023-06-15', '2023-06-20', N'Phát triển kỹ năng đàm phán và thương lượng', 'NV027'),
('DT006', N'Kỹ năng đàm phán', '2023-06-15', '2023-06-20', N'Phát triển kỹ năng đàm phán và thương lượng', 'NV028'),
('DT006', N'Kỹ năng đàm phán', '2023-06-15', '2023-06-20', N'Phát triển kỹ năng đàm phán và thương lượng', 'NV029'),
('DT006', N'Kỹ năng đàm phán', '2023-06-15', '2023-06-20', N'Phát triển kỹ năng đàm phán và thương lượng', 'NV030'),

-- Khóa đào tạo 7: Nâng cao năng lực quản lý tài chính
('DT007', N'Nâng cao năng lực quản lý tài chính', '2023-07-10', '2023-07-15', N'Kỹ năng phân tích tài chính và lập ngân sách', 'NV031'),
('DT007', N'Nâng cao năng lực quản lý tài chính', '2023-07-10', '2023-07-15', N'Kỹ năng phân tích tài chính và lập ngân sách', 'NV032'),
('DT007', N'Nâng cao năng lực quản lý tài chính', '2023-07-10', '2023-07-15', N'Kỹ năng phân tích tài chính và lập ngân sách', 'NV033'),
('DT007', N'Nâng cao năng lực quản lý tài chính', '2023-07-10', '2023-07-15', N'Kỹ năng phân tích tài chính và lập ngân sách', 'NV034'),
('DT007', N'Nâng cao năng lực quản lý tài chính', '2023-07-10', '2023-07-15', N'Kỹ năng phân tích tài chính và lập ngân sách', 'NV035'),

-- Khóa đào tạo 8: Kỹ năng bán hàng chuyên nghiệp
('DT008', N'Kỹ năng bán hàng chuyên nghiệp', '2023-08-05', '2023-08-10', N'Chiến lược và kỹ năng bán hàng hiệu quả', 'NV036'),
('DT008', N'Kỹ năng bán hàng chuyên nghiệp', '2023-08-05', '2023-08-10', N'Chiến lược và kỹ năng bán hàng hiệu quả', 'NV037'),
('DT008', N'Kỹ năng bán hàng chuyên nghiệp', '2023-08-05', '2023-08-10', N'Chiến lược và kỹ năng bán hàng hiệu quả', 'NV038'),
('DT008', N'Kỹ năng bán hàng chuyên nghiệp', '2023-08-05', '2023-08-10', N'Chiến lược và kỹ năng bán hàng hiệu quả', 'NV039'),
('DT008', N'Kỹ năng bán hàng chuyên nghiệp', '2023-08-05', '2023-08-10', N'Chiến lược và kỹ năng bán hàng hiệu quả', 'NV040'),

-- Khóa đào tạo 9: Đào tạo phân tích dữ liệu
('DT009', N'Đào tạo phân tích dữ liệu', '2023-09-01', '2023-09-07', N'Kỹ năng phân tích và trực quan hóa dữ liệu', 'NV041'),
('DT009', N'Đào tạo phân tích dữ liệu', '2023-09-01', '2023-09-07', N'Kỹ năng phân tích và trực quan hóa dữ liệu', 'NV042'),
('DT009', N'Đào tạo phân tích dữ liệu', '2023-09-01', '2023-09-07', N'Kỹ năng phân tích và trực quan hóa dữ liệu', 'NV043'),
('DT009', N'Đào tạo phân tích dữ liệu', '2023-09-01', '2023-09-07', N'Kỹ năng phân tích và trực quan hóa dữ liệu', 'NV044'),
('DT009', N'Đào tạo phân tích dữ liệu', '2023-09-01', '2023-09-07', N'Kỹ năng phân tích và trực quan hóa dữ liệu', 'NV045'),

-- Khóa đào tạo 10: An toàn thông tin
('DT010', N'An toàn thông tin', '2023-10-05', '2023-10-10', N'Bảo mật và bảo vệ hệ thống thông tin', 'NV046'),
('DT010', N'An toàn thông tin', '2023-10-05', '2023-10-10', N'Bảo mật và bảo vệ hệ thống thông tin', 'NV047'),
('DT010', N'An toàn thông tin', '2023-10-05', '2023-10-10', N'Bảo mật và bảo vệ hệ thống thông tin', 'NV048'),
('DT010', N'An toàn thông tin', '2023-10-05', '2023-10-10', N'Bảo mật và bảo vệ hệ thống thông tin', 'NV049'),
('DT010', N'An toàn thông tin', '2023-10-05', '2023-10-10', N'Bảo mật và bảo vệ hệ thống thông tin', 'NV050');


-- Thêm đầu mục cho Dự án A
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV001', N'Phân tích yêu cầu', 'DA01', 'NV001'),
('DV002', N'Thiết kế hệ thống', 'DA01', 'NV002'),
('DV003', N'Phát triển ứng dụng', 'DA01', 'NV003'),
('DV004', N'Kiểm thử', 'DA01', 'NV004'),
('DV005', N'Bàn giao dự án', 'DA01', 'NV005');

-- Thêm đầu mục cho Dự án B
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV006', N'Phân tích yêu cầu', 'DA02', 'NV006'),
('DV007', N'Thiết kế hệ thống', 'DA02', 'NV007'),
('DV008', N'Phát triển ứng dụng', 'DA02', 'NV008'),
('DV009', N'Kiểm thử', 'DA02', 'NV009'),
('DV010', N'Bàn giao dự án', 'DA02', 'NV010');

-- Thêm đầu mục cho Dự án C
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV011', N'Phân tích yêu cầu', 'DA03', 'NV011'),
('DV012', N'Thiết kế cơ sở dữ liệu', 'DA03', 'NV012'),
('DV013', N'Phát triển ứng dụng', 'DA03', 'NV013'),
('DV014', N'Kiểm thử', 'DA03', 'NV014'),
('DV015', N'Bàn giao dự án', 'DA03', 'NV015');

-- Thêm đầu mục cho Dự án D
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV016', N'Phân tích quy trình', 'DA04', 'NV016'),
('DV017', N'Thiết kế giải pháp', 'DA04', 'NV017'),
('DV018', N'Phát triển hệ thống', 'DA04', 'NV018'),
('DV019', N'Kiểm thử', 'DA04', 'NV019'),
('DV020', N'Bàn giao dự án', 'DA04', 'NV020');

-- Thêm đầu mục cho Dự án E
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV021', N'Phân tích yêu cầu', 'DA05', 'NV021'),
('DV022', N'Thiết kế giao diện', 'DA05', 'NV022'),
('DV023', N'Phát triển ứng dụng', 'DA05', 'NV023'),
('DV024', N'Kiểm thử', 'DA05', 'NV024'),
('DV025', N'Bàn giao dự án', 'DA05', 'NV025');

-- Thêm đầu mục cho Dự án F
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV026', N'Phân tích yêu cầu', 'DA06', 'NV026'),
('DV027', N'Thiết kế hệ thống bảo mật', 'DA06', 'NV027'),
('DV028', N'Phát triển ứng dụng', 'DA06', 'NV028'),
('DV029', N'Kiểm thử', 'DA06', 'NV029'),
('DV030', N'Bàn giao dự án', 'DA06', 'NV030');

-- Thêm đầu mục cho Dự án G
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV031', N'Phân tích quy trình', 'DA07', 'NV031'),
('DV032', N'Thiết kế giải pháp', 'DA07', 'NV032'),
('DV033', N'Phát triển hệ thống', 'DA07', 'NV033'),
('DV034', N'Kiểm thử', 'DA07', 'NV034'),
('DV035', N'Bàn giao dự án', 'DA07', 'NV035');

-- Thêm đầu mục cho Dự án H
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV036', N'Phân tích yêu cầu', 'DA08', 'NV036'),
('DV037', N'Thiết kế chiến lược', 'DA08', 'NV037'),
('DV038', N'Phát triển kế hoạch', 'DA08', 'NV038'),
('DV039', N'Kiểm thử', 'DA08', 'NV039'),
('DV040', N'Bàn giao dự án', 'DA08', 'NV040');

-- Thêm đầu mục cho Dự án I
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV041', N'Phân tích yêu cầu', 'DA09', 'NV041'),
('DV042', N'Thiết kế website', 'DA09', 'NV042'),
('DV043', N'Phát triển ứng dụng', 'DA09', 'NV043'),
('DV044', N'Kiểm thử', 'DA09', 'NV044'),
('DV045', N'Bàn giao dự án', 'DA09', 'NV045');

-- Thêm đầu mục cho Dự án J
INSERT INTO DAUVIECDUAN (MaDauViec, TenDauViec, MaDuAn, MaNV) VALUES
('DV046', N'Phân tích nhu cầu', 'DA10', 'NV046'),
('DV047', N'Thiết kế chương trình đào tạo', 'DA10', 'NV047'),
('DV048', N'Triển khai đào tạo', 'DA10', 'NV048'),
('DV049', N'Kiểm thử', 'DA10', 'NV049'),
('DV050', N'Bàn giao tài liệu', 'DA10', 'NV050');

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
SELECT * FROM BANGCONG


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



SELECT DVDA.MaDauViec, DVDA.TenDauViec
FROM DAUVIECDUAN DVDA
WHERE DVDA.TenDauViec = N'Phân tích yêu cầu' AND DVDA.MaDuAn='DA01'
--function
--mỗi ngày import vào mỗi nhân viên
DECLARE @CurrentDate DATETIME = GETDATE();

INSERT INTO CHAMCONG (ID_CHAMCONG, NGAYVAO, MANV)
SELECT 
    LEFT(CAST(MANV AS CHAR(10)) + FORMAT(@CurrentDate, 'MMdd'), 20), -- Chỉ lấy phần cần thiết
    @CurrentDate, 
    MANV
FROM NhanVien; -- Lấy tất cả mã nhân viên từ bảng HoSoNhanVien