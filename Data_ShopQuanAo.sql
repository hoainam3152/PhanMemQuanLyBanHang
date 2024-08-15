CREATE DATABASE QL_ShopQuanAo

USE QL_ShopQuanAo

CREATE TABLE DANGNHAP
(
	MATK VARCHAR(10),
	TAIKHOAN VARCHAR(20) UNIQUE NOT NULL,
	MATKHAU VARCHAR(30) NOT NULL,
	LOAITK VARCHAR(8) CHECK (LOAITK IN ('admin', 'user')) DEFAULT 'user',
	CONSTRAINT PK_DANGNHAP PRIMARY KEY(MATK)
)

create table NHANVIEN
(
	MaNV nchar(10),
	TenNV nvarchar(30),
	SDTNV varchar(15),
	NgaySinhNV datetime,
	DiaChiNV nvarchar(50),
	GioiTinhNV nvarchar(10),
	CCCD varchar(15),
	NVL int,
	Luong numeric(10, 2),
	MATK VARCHAR(10),
	constraint PK_MaNV primary key(MaNV),
	constraint FK_NHANVIEN_DANGNHAP foreign key(MATK) references DANGNHAP(MATK)
)

create table KHACHHANG
(
	MaKH int identity(1000, 1),
	TenKH nvarchar(50),
	SDTKH varchar(10),
	DiaChiKH nvarchar(200),
	GioiTinhKH nvarchar(10),
	NamSinhKH int,
	constraint PK_MaKH primary key (MaKH)
)

create table LOAISP
(
	MaL nchar(10),
	TenL nvarchar(30)
	constraint PK_MaL primary key (MaL)
)

create table SANPHAM
(
	MaSP nchar(10),
	TenSP nvarchar(80),
	HinhSP VARCHAR(255),
	GiaBan float default 0,
	GioiTinh nvarchar(4),
	ThongTinSP nvarchar(400),
	ChatLieu nvarchar(100),
	Form nvarchar(20),
	SoLuongTon int default 0,
	DaBan int default 0,
	TinhTrang nvarchar(20),
	MaL nchar(10),
	constraint PK_MaSP primary key (MaSP),
	constraint FK_SANPHAM_LOAISP foreign key (MaL) references LOAISP(MaL)
)

create table HOADON
(
	MaHD INT IDENTITY(1000, 1),
	MaNV nchar(10),
	MaKH int,
	NgayBan datetime,
	TongThanhToan float,
	TrangThai nvarchar(20),
	HinhThucThanhToan nvarchar(20),
	GhiChu nvarchar(200),
	constraint PK_MaHD primary key(MaHD),
	constraint FK_HOADON_NHANVIEN foreign key (MaNV) references NHANVIEN(MaNV),
	constraint FK_HOADON_KHACHHANG foreign key (MaKH) references KHACHHANG(MaKH)	
)

create table CTHD
(
	MaHD INT,
	MaSP nchar(10),
	KichCo varchar(6),
	SoLuong int, 
	GiaBan float,
	ThanhTien float,
	constraint PK_MaHD_MaSP_MaNV primary key(MaHD, MaSP, KichCo),
	constraint FK_CTHD_HOADON foreign key (MaHD) references HOADON(MaHD),
	constraint FK_CTHD_SANPHAM foreign key (MaSP) references SANPHAM(MaSP)
)

GO
CREATE TRIGGER TG_CAPNHAP_SANPHAM_KHITHEM ON CTHD
AFTER INSERT
AS
	DECLARE @MASP NVARCHAR(10), @SLMUA INT, @SLCONLAI INT, @MAHD INT, @TT nvarchar(20)
	SELECT @MAHD = MaHD,  @MASP = MaSP, @SLMUA = SoLuong
	FROM inserted

	SET @TT = (SELECT TrangThai FROM HOADON WHERE MaHD = @MAHD)
	IF @TT NOT IN (N'Chưa thanh toán', N'Đặt cọc')
	BEGIN
		SET @SLCONLAI = (SELECT SoLuongTon FROM SANPHAM WHERE MaSP = @MASP) - @SLMUA

		IF @SLCONLAI < 0
		BEGIN
			UPDATE SANPHAM
			SET SoLuongTon = 0, TinhTrang = N'Hết hàng', DaBan = DaBan + @SLMUA
			WHERE MaSP = @MASP;
		END 
		ELSE
		BEGIN
			UPDATE SANPHAM
			SET SoLuongTon = SoLuongTon - @SLMUA, DaBan = DaBan + @SLMUA
			WHERE MaSP = @MASP;
		END
	END
--DROP TRIGGER TG_CAPNHAP_SANPHAM_KHITHEM

--DROP TABLE CTHD
--DROP TABLE HOADON
--DROP TABLE NHANVIEN
--DROP TABLE KHACHHANG
--DROP TABLE DANGNHAP
--DROP TABLE SANPHAM
--DROP TABLE LOAISP

--Them rieng ADMIN
INSERT INTO DANGNHAP (MATK,TAIKHOAN, MATKHAU, LOAITK)
VALUES
('ADMIN','admin','123456','admin');

--Them cac user khac
INSERT INTO DANGNHAP (MATK,TAIKHOAN, MATKHAU)
VALUES
('TK001','ngoclinh','123456'),
('TK002','vankhoe','123456'),
('TK003','lethiet','123456'),
('TK004','ltrongtan','123456')

select * from DANGNHAP
Select MaTK from DANGNHAP
--TH ĐẶC BIỆT
INSERT INTO NHANVIEN(MaNV, TenNV)
VALUES ('NV000',N'--Thu Ngân--')

SET DATEFORMAT MDY;
INSERT INTO NHANVIEN(MaNV, TenNV, SDTNV, NgaySinhNV, DiaChiNV, GioiTinhNV, CCCD, NVL, Luong, MATK)
VALUES
('QL000',N'Nguyễn Văn Trỗi','0986524753','9-16-2001',N'145 lê liễu tphcm','Nam','0796565528',2021,null,'ADMIN'),
('NV001',N'Trần Ngọc Linh','0326786536','6-13-2003',N'85 bình long tphcm','Nam','079030007564',2022,6300000,'TK001'),
('NV002',N'Phan Văn Khỏe','0921695388','7-14-2004',N'321 văn cao tphcm',N'Nữ','079865224456',2023,3200000,'TK002'),
('NV003',N'Lê Thiệt','0326856472','8-15-2003',N'654 phan huy ích tphcm','Nam','079441004568',2022,5100000,'TK003'),
('NV004',N'Lê Trọng Tấn','0952369845','5-12-2003',N'536 tân kỳ tphcm',N'Nữ','079232005684',2023,4300000,'TK004')

SELECT * FROM NHANVIEN

SET DATEFORMAT MDY;
insert into KHACHHANG (TenKH, SDTKH,GioiTinhKH, DiaChiKH, NamSinhKH)
values
(N'Nguyễn Thùy Linh','0564243269',N'Nữ',N'23 Nguyễn Chí Thanh',2000),
(N'Trần Ngọc Lan','0327874591',N'Nữ',N'7 Lê Đại Hành',1998),
(N'Huỳnh Thanh Lượng','0983486657',N'Nam',N'11/2 Thoại Ngọc Hầu',1997),
(N'Đinh Gia Hải','0987345712',N'Nam',N'78 Thành Thái',2001)

SELECT * FROM KHACHHANG

insert into LOAISP (MaL, TenL)
values
('L000',N'Danh Mục'),
('L001',N'Áo Thun'),
('L002',N'Áo Polo'),
('L003',N'Áo Sơ Mi'),
('L004',N'Áo Khoác'),
('L005',N'Áo Kiểu'),
('L006',N'Quần Short'),
('L007',N'Quần Jean'),
('L008',N'Quần Kaki'),
('L009',N'Váy Đầm'),
('L010',N'Chân Váy'),
('L011',N'Áo Croptop')

SELECT * FROM LOAISP

insert into SANPHAM (MaSP, TenSP, HinhSP, GiaBan, GioiTinh, ThongTinSP, ChatLieu, Form, SoLuongTon, TinhTrang, MaL)
values
('SP001',N'Áo Thun Nam In Graphic Mountains 8675 Hope MTS 1314','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP001.jpg',329000,N'Nam',N'Áo Thun Nam In Graphic Mountains 8675 Hope MTS 1314. Chất liệu thun cotton mềm mịn tạo cảm giác thoải mái, dễ dàng phối được với nhiều phong cách khác nhau. Hiệu ứng hình in được sử dụng bằng bộ màu CMYK mang đến cảm giác chân thật và bắt mắt.',N'100% Cotton, 210 GSM',N'Regular',30,N'Còn hàng','L001'),
('SP002',N'Áo Thun Nam Typo Grow In Chuyển Màu MTS 1312','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP002.jpg',299000,N'Nam',N'Áo Thun Nam Typo Grow In Chuyển Màu MTS 1312 . Áo thun TYPO GROWN với điểm nhấn là phần hình in áp dụng kỹ thuật in dẻo có hiệu ứng chuyển màu nổi bật, phá cách. Áo được thiết kế 100% chất liệu từ Cotton với khả năng thấm hút mồ hôi tốt,thoáng mát, kết hợp cùng form dáng regular vừa vặn và tôn dáng người mặc.',N'100% Cotton, 210 GSM',N'Regular',30,N'Còn hàng','L001'),
('SP003',N'Áo Thun Nữ Oversize In Graphics Hoa Tiệp Màu WTS 2206','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP003.jpg',299000,N'Nữ',N'Áo Thun Nữ Oversize In Graphics Hoa Tiệp Màu WTS 2206. Áo thun với hình in hoa hiệu ứng tiệp màu với nền vải hiện đại. ',N'Thun xớ lớn OE18, thành phần 100% Cotton, trọng lượng 230GSM',N'Oversize',30,N'Còn hàng','L001'),
('SP004',N'Áo Polo Nam Pique Mắt Bo Kiểu Regular Fit MPO 1032','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP004.jpg',399000,N'Nam',N'Áo Polo Nam Pique Mắt Bo Kiểu Regular Fit MPO 1032. Mẫu áo polo với phối bo kiểu trẻ trung, năng động.Chất liệu Pique cao cấp 40% Cotton, 60% Polyester với kiểu dệt mắt lưới to mạnh mẽ, dày dặn.',N'Pique mắt lớn thành phần 40% Cotton, 60% Polyester',N'Regular',30,N'Còn hàng','L002'),
('SP005',N'Áo Polo Nữ Jersey Relax Fit In Typo Trước Ngực WPO 2025','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP005.jpg',399000,N'Nữ',N'Áo Polo Nữ Jersey Relax Fit In Typo Trước Ngực WPO 2025. Áo polo single jersey form relax thời trang in thông điệp FLOW OF REALITY.',N'Thun xớ thô OE18, thành phần 100% Cotton, trọng lượng 230GSM',N'Relax',30,N'Còn hàng','L002'),
('SP006',N'Áo Polo Nữ Slim Fit Basic Bo Dệt Gân WPO 2017','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP006.jpg',389000,N'Nữ',N'Áo Polo Nữ Slim Fit Bo Dệt Gân WPO 2017. Áo Polo dệt gân kiểu, thêu Logo tiệp màu vải tinh tế. Form dáng gọn gàng, tôn dáng, dễ phối với nhiều loại bottom..',N'Chất liệu Pique 4 chiều, thành phần 49% Cotton, 47% Polyester, 4% Spandex, trọng lượng 210GSM',N'Slim',30,N'Còn hàng','L002'),
('SP007',N'Áo Sơ Mi Nam Ngắn Tay Túi Đắp MSH 1012','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP007.jpg',499000,N'Nam',N'Áo Sơ Mi Nam Ngắn Tay Túi Đắp MSH  1012. Chất liệu cotton thoáng mát, mỏng nhẹ. Túi ngực có thêu chi tiết logo X tinh tế.',N'100% Cotton',N'Regular',30,N'Còn hàng','L003'),
('SP008',N'Sơ Mi Nam Khakis Túi Thêu MSH 1013','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP008.jpg',579000,N'Nam',N'Sơ Mi Nam Khakis Túi Thêu MSH 1013. Chất liệu cotton thoáng mát, nhẹ nhàng.  Túi ngực có thêu chi tiết logo X tinh tế.',N'Khakis 100% Cotton',N'Slim',30,N'Còn hàng','L003'),
('SP009',N'Áo Sơ Mi Nữ Oversize Túi Đắp WBL 2024','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP009.jpg',379000,N'Nữ',N'Áo Kiểu Nữ Sơ Mi Túi Đắp WBL 2024 Áo kiểu sơ mi kết hợp cùng quần short giả váy cùng set tạo nên outfit trẻ  trung, năng động. Chất liệu có thành phần rayon nguồn gốc tự nhiên, tạo cảm giác thoáng mát, mềm nhẹ trên da.',N'Đũi nhăn, thành phần 52% Rayon, 33% Polyester, 15% Nylon',N'Oversize',30,N'Còn hàng','L003'),
('SP010',N'Áo Khoác Dù Trượt Nước Nam X-Jacket Version 3.0 Limited Edition MOP 1018','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP010.jpg',1299000 ,N'Nam',N'Áo Khoác Dù Trượt Nước Nam X-Jacket Version 3.0 Limited Edition MOP 1018 là sản phẩm do Couple TX nghiên cứu phát triển độc quyền tích hợp 10 tính năng. Sản phẩm ứng dụng chất liệu sợi từ polyester tái chế, góp phần bảo vệ môi trường. Đặc tính trượt nước có được nhờ kết cấu vải 4 lớp, với lớp ngoài cùng được xử lý hóa chất trượt nước và lớp trong cán màng TPU chống thấm ngược vào trong.',N'45% Recycled Polyester, 55% Polyester thường',N'Relax',30,N'Còn hàng','L004'),
('SP011',N'Áo Khoác Nam Bomber Dù Oversize Bo Sọc Thêu Logo FF MOP 1027','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP011.jpg',829000,N'Nữ',N'Áo Khoác Bomber Dù Oversize Bo Sọc Thêu Logo FF MOP 1027. Áo khoác dù dày form bomber oversize với bo được dệt sọc trẻ trung, có đường coupe và diễu chỉ nhăn dọc tay cá tính, thân trước áo thêu logo F&F, thân sau gắn logo silicon X . • Áo có 2 túi dây kéo thân trước, 1 túi cơi ngang mặt trong.',N'100% polyester',N'Oversize bomber',30,N'Còn hàng','L004'),
('SP012',N'Áo Khoác Nữ Khakis Túi Đắp WOF 2030','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP012.jpg',649000,N'Nữ',N'Áo Khoác Nữ Khakis Túi Đắp WOF 2030.  Áo khoác Khaki xớ lớn, form oversize thời trang. Áo thân trước may 2 túi đắp có thể xỏ tay từ cạnh túi vào và 2 túi chìm may ăn vô coupe sườn. Áo được wash đường ngấn cánh gián đậm nhạt thời trang dọc theo các đường diễu.',N'vải kakis xớ lớn, thành phần 100% cotton.',N'Oversize',30,N'Còn hàng','L004'),
('SP013',N'Áo Khoác Nữ Anti UV Regular Fit Emboss WOK 2037','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP013.jpg',729000,N'Nữ',N'Áo Khoác Nữ Anti UV Regular Fit Emboss WOK 2037 với chỉ số chống nắng UPF 50+ phản xạ đến 98% tia UV đến từ bề mặt lồi lõm phản xạ tia UV và khả năng chống nắng tự nhiên từ sợi polyester. Chất liệu vừa dày dặn, vừa thoáng mát. Kết hợp giữa thành phần cotton cao đảm bảo thoáng khí và thành phần polyester bền màu - có tính chống UV tự nhiên.',N'Vải tricot 60% Cotton – 40% Polyester 240gsm',N'Regular',30,N'Còn hàng','L004'),
('SP014',N'Áo Kiểu Nữ Camisole Hoa Smocking WBL 2025','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP014.jpg',249000,N'Nữ',N'Áo Kiểu Nữ Camisole Hoa Smocking WBL 2025 Mẫu áo dây nhẹ nhàng với phần dây áo được thiết kế tinh tế giúp các bạn nữ có cái nhìn phóng khoáng, năng động nhưng vẫn không bị quá phô hay lộ quá nhiều khuyết điểm.',N'96% Polyester - 4% spandex',N'Skinny',30,N'Còn hàng','L005'),
('SP015',N'Áo Kiểu Nữ 2 Dây Linen WBL 2027','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP015.jpg',259000,N'Nữ',N'Áo Kiểu Nữ 2 Dây Linen WBL 2027 với chất liệu linen pha cotton thoáng mát tối ưu cho mùa hè, sợi linen giúp vải nhanh khô và có độ thoáng khí cao, đồng thời tạo nên bề mặt vải mộc đặc trưng cho mùa hè. Thiết kế thời trang và nữ tính, dễ sử dụng cho nhiều dịp: đi làm trong môi trường năng động, đi chơi trải nghiệm thường nhật, đi du lịch xa hoặc café ăn tối đơn giản.',N'55% Linen 45% Cotton',N'Slim',30,N'Còn hàng','L005'),
('SP016',N'Quần Short Nam Slim Fit Khakis Lưng Thun MSR 1016','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP016.jpg',399000,N'Nam',N'Quần Short Nam Slim Fit Khakis Lưng Thun MSR 1016. Sản phẩm với form slim gọn đùi, chiều dài vừa trên gối tôn dáng là mẫu quần hoàn hảo cho các bạn nam thích sự năng động trẻ trung. Chi tiết túi vuông phía sau rộng rãi, phong cách cùng với lưng thun dây rút thoải mái. Phù hợp với mọi phong cách nhờ dễ dàng kết hợp từ áo thun đến áo sơ mi.',N'Khaki 98% Cotton - 2% Spandex, trọng lượng 265gsm ',N'Slim',30,N'Còn hàng','L006'),
('SP017',N'Quần Short Khakis Nam Basic Slim Fit MSR 1030','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP017.jpg',499000,N'Nam',N'Quần Short Khakis Nam Basic Slim Fit MSR 1030. Quần Short Basic Khakis với form slim fit ống ôm vừa vặn, tôn dáng triệt để cho người mặc. Sản phẩm có thể dễ dàng phối với đa dạng áo thun, sơmi phù hợp với mọi hoạt động hàng ngày.',N'Khakis co dãn nhẹ, thành phần 97% Cotton, 3% Spandex, trọng lượng 270GSM',N'Slim',30,N'Còn hàng','L006'),
('SP018',N'Quần Short Nữ Linen Xẻ Lai WSR 2014','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP018.jpg',379000,N'Nữ',N'Quần Short Nữ Linen Xẻ Lai WSR 2014. Sản phẩm với form chữ A cực kì tôn dáng dành cho các bạn nữ. Với độ dài vừa phải và thiết kế xẻ lai tạo điểm nhấn thì mẫu váy độc đáo này phù hợp với mọi phong cách phối đồ. Chất liệu cotton pha linen có độ thấm hút cao, tạo cảm giác thoải mái, dễ chịu cho người mặc.',N'linen 45% Cotton - 55% Linen',N'A line',30,N'Còn hàng','L006'),
('SP019',N'Quần Jean Nam Regular MJE 1020','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP019.jpg',649000,N'Nam',N'Quần Jean Nam Regular MJE 1020. Sự kết hợp giữa kiểu dáng đơn giản cùng chất liệu bền chắc, mang lại sự thoải mái cho người mặc. Sự linh hoạt trong cách phối đồ làm cho mẫu quần này là một sự lựa chọn đa năng để mang lại sự tự tin mỗi ngày.',N'Chất liệu jeans, thành phần 100% Cotton',N'Regular',30,N'Còn hàng','L007'),
('SP020',N'Quần Jean Nữ Skinny Wash Rách WJE 2018','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP020.jpg',599000,N'Nữ',N'Quần Jeans Nữ Skinny Rách WJE 2018. Với điểm nhấn wash rách, tạo nét hiện đại, năng động. Form skinny trên chất liệu vải jean có độ co giãn ôm dáng, tôn dáng.',N'Jeans co giãn 50% Rayon, 27% Cotton, 21% Polyester, 2% Spandex',N'Skinny',30,N'Còn hàng','L007'),
('SP021',N'Quần Jean Nữ Coolmax Skinny Fit WJE 2021','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP021.jpg',699000,N'Nữ',N'Quần Jean Nữ Coolmax Skinny Fit WJE 2021. Quần jeans Skinny COOLMAX trơn với lưng quần ngang eo - không gây khó chịu, tôn dáng cho người mặc. Có thể phối hợp với nhiều sản phẩm áo thun khác nhau.  Được làm từ chất liệu COOLMAX có tính năng thoáng mát và mềm mại, nhẹ tạo cảm giác thoải mái cho người mặc.',N'Jeans Coolmax, 76% Cotton, 2% Lycra, 22% PES Coolmax',N'Skinny',30,N'Còn hàng','L007'),
('SP022',N'Quần Dài Nam Khaki Chinos MPA 1006','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP022.jpg',579000,N'Nam',N'Quần Dài Nam Khaki Chinos MPA 1006. Quần khakis form Chinos trẻ trung, năng động. Túi phía sau đính logo X tiện lợi, thời trang',N'Chất liệu khakis, thành phần 100%Cotton',N'Chinos',30,N'Còn hàng','L008'),
('SP023',N'Váy Nữ Midi A Line Xẻ Tà WSK 2016','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP023.jpg',449000,N'Nữ',N'Váy Nữ Midi A Line Xẻ Tà WSK 2016. Mẫu váy với độ dài vừa phải kết hợp cùng phần xẻ tà tinh tế thì các cô nàng dù theo phong cách cá tính, năng động hay bánh bèo, nữ tính đều có thể mix & match dễ dàng. Chi tiết túi xéo độc đáo, thời trang vô cùng tiện lợi cho các nàng nào lười mang túi lỉnh kỉnh mà vẫn có thể đựng được cả thế giới.',N'Khaki 100% cotton',N'Chữ A midi qua gối',30,N'Còn hàng','L009'),
('SP024',N'Đầm Nữ Hoa Nhí Summer WDR 2034','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP024.jpg',429000,N'Nữ',N'Đầm Nữ Hoa Nhí Summer WDR 2034 Mẫu đầm hoa họa tiết nhẹ nhàng, nữ tính. Điểm nhấn phần nhún sườn váy độc đáo, tinh tế giúp tôn dáng người mặc. ',N'Vải thun gân, thành phần 74% Polyester + 22% Rayon + 4% Spandex, trọng lượng 310GSM',N'Slim',30,N'Còn hàng','L009'),
('SP025',N'Váy Nữ Đắp Chéo WSK 2014','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP025.jpg',379000,N'Nữ',N'Váy Nữ Đắp Chéo Túi Hộp WSK 2014 Chân váy ngắn nhấn túi hộp và tà đắp chéo năng động. • Chất liệu khaki 100% Cotton mềm mại nhưng vẫn đứng form.',N'Khaki 100% cotton',N'Chữ A',30,N'Còn hàng','L009'),
('SP026',N'Chân Váy Nữ Hoa Nhí Xẻ Lai WSK 2022','D:\Documents\Tai_lieu_HK5\Tai_lieu_Cong_nghe_.Net\BT_CNN\Do_An\Nhom7_QuanLyShopQuanAo\QuanLyShopQuanAo\QuanLyShopQuanAo\Image\SP026.jpg',329000,N'Nữ',N'Chân Váy Nữ Hoa Nhí Xẻ Lai WSK 2022 Mẫu chân váy với điểm nhấn xẻ lai v độc đáo, kết hợp cùng họa tiết hoa nhẹ nhàng, tinh tế. ',N'96% Polyester - 4% spandex',N'Aline',30,N'Còn hàng','L010')

SELECT * FROM SANPHAM

SET DATEFORMAT DMY;
insert into HOADON(MaNV, MaKH, NgayBan, TongThanhToan, TrangThai, HinhThucThanhToan, GhiChu)
values 
('NV003','1000','30/11/2023',329000,N'Đã thanh toán',N'Chuyển khoản', NULL),
('NV001','1001','02/12/2023',798000,N'Đã thanh toán',N'Tiền mặt', NULL),
('NV004','1000','03/12/2023',389000,N'Chưa thanh toán',NULL, NULL),
('NV002','1002','04/12/2023',898000,N'Đặt cọc',N'Tiền mặt', N'Cọc: 500k'),
('NV002','1003','05/12/2023',658000,N'Đã thanh toán',N'Tiền mặt', NULL)

SELECT * FROM HOADON

insert into CTHD(MaHD, MaSP, KichCo, SoLuong, GiaBan, ThanhTien)
values 
('1000','SP003','L',1,329000,329000),
('1001','SP005','S',2,399000,798000),
('1002','SP006','M',1,389000,389000),
('1003','SP004','XXL',1,399000,399000),
('1003','SP007','XXL',1,499000,499000),
('1004','SP001','XL',2,329000,658000)

SELECT * FROM CTHD