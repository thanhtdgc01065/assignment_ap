USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='CS_Assignment')
DROP DATABASE CS_Assignment
GO

CREATE DATABASE CS_Assignment
GO

USE CS_Assignment
GO

CREATE TABLE [DonVi]
(
[MaDonVi] VARCHAR(100),
CONSTRAINT PK_MaDonVi PRIMARY KEY(MaDonVi),
[TenPhong] VARCHAR(200),
[TenDoi] VARCHAR(200),
[IsDeleted] BIT DEFAULT 0
)
GO

CREATE TABLE [TaiKhoan]
(
[TenTaiKhoan] VARCHAR(100),
CONSTRAINT PK_TenTaiKhoan PRIMARY KEY(TenTaiKhoan),
[MatKhau] VARCHAR(100) NOT NULL,
[HoTen] NVARCHAR(300),
[MaDonVi] VARCHAR(100) NOT NULL,
CONSTRAINT FK_DonVi_TaiKhoan FOREIGN KEY(MaDonVi) REFERENCES [DonVi](MaDonVi),
[Email] VARCHAR(300),
CONSTRAINT UQ_Email UNIQUE(Email),
[Mobile] VARCHAR(15),
CONSTRAINT UQ_Mobile UNIQUE(Mobile),
[DiaChi] NVARCHAR(MAX),
[IsDeleted] BIT DEFAULT 0
)
GO

CREATE TABLE [NghiepVu]
(
[MaNghiepVu] VARCHAR(100),
CONSTRAINT PK_MaNghiepVu PRIMARY KEY(MaNghiepVu),
[MaDonVi] VARCHAR(100),
CONSTRAINT FK_DonVi_NghiepVu FOREIGN KEY(MaDonVi) REFERENCES [DonVi](MaDonVi),
[TenNghiepVu] VARCHAR(100),
[IsDeleted] BIT DEFAULT 0
)
GO

CREATE TABLE [CauHoi]
(
[MaCauHoi] INT IDENTITY,
CONSTRAINT PK_MaCauHoi PRIMARY KEY(MaCauHoi),
[MaNghiepVu] VARCHAR(100),
CONSTRAINT FK_NghiepVu_CauHoi FOREIGN KEY(MaNghiepVu) REFERENCES [NghiepVu](MaNghiepVu),
[CauHoi] NVARCHAR(MAX),
[TraLoi1] NVARCHAR(MAX),
[TraLoi2] NVARCHAR(MAX),
[TraLoi3] NVARCHAR(MAX),
[DapAn] INT NOT NULL,
[IsDeleted] BIT DEFAULT 0
)
GO

CREATE TABLE [DeThi]
(
[MaDeThi] VARCHAR(100),
CONSTRAINT PK_MaDeThi PRIMARY KEY(MaDeThi),
[MaNghiepVu] VARCHAR(100),
CONSTRAINT FK_NghiepVu_DeThi FOREIGN KEY(MaNghiepVu) REFERENCES [NghiepVu](MaNghiepVu),
[MaCauHoi1] INT,
CONSTRAINT FK_CauHoi_DeThi1 FOREIGN KEY(MaCauHoi1) REFERENCES [CauHoi](MaCauHoi),
[MaCauHoi2] INT,
CONSTRAINT FK_CauHoi_DeThi2 FOREIGN KEY(MaCauHoi2) REFERENCES [CauHoi](MaCauHoi),
[MaCauHoi3] INT,
CONSTRAINT FK_CauHoi_DeThi3 FOREIGN KEY(MaCauHoi3) REFERENCES [CauHoi](MaCauHoi),
[IsDeleted] BIT DEFAULT 0
)
GO

CREATE TABLE [BaiThi]
(
[MaBaiThi] INT IDENTITY(10000,1),
CONSTRAINT PK_MaBaiThi PRIMARY KEY(MaBaiThi),
[MaDeThi] VARCHAR(100),
CONSTRAINT FK_DeThi_BaiThi FOREIGN KEY(MaDeThi) REFERENCES [DeThi](MaDeThi),
[TenTaiKhoan] VARCHAR(100),
CONSTRAINT FK_TaiKhoan_BaiThi FOREIGN KEY(TenTaiKhoan) REFERENCES [TaiKhoan](TenTaiKhoan),
[NgayThi] DATE,
[IsDeleted] BIT DEFAULT 0
)

CREATE TABLE [KetQua]
(
[MaKetQua] INT IDENTITY,
CONSTRAINT PK_MaKetQua PRIMARY KEY(MaKetQua),
[MaBaiThi] INT,
CONSTRAINT QU_KetQua_MaBaiThi UNIQUE(MaBaiThi),
CONSTRAINT FK_BaiThi_KetQua FOREIGN KEY(MaBaiThi) REFERENCES [BaiThi](MaBaiThi),
[DiemSo] INT,
[IsDeleted] BIT DEFAULT 0
)
GO

INSERT INTO [DonVi](MaDonVi,TenPhong,TenDoi) VALUES
('BOD','1. Board of Director','1.1. Board of Director'),
('KTHC','2. Accounting and Administration Dept.','2.1. Dpt. Manager'),
('KHHC','2. Accounting and Administration Dept.','2.2. Admin Team'),
('KTTK','2. Accounting and Administration Dept.','2.3. Accounting Team'),
('XE','2. Accounting and Administration Dept.','2.4. Vehicle Team'),
('DVTK','3. Inflight Service Dept.','3.1. Dpt. Manager'),
('QLDVTK','3. Inflight Service Dept.','3.2. Inflight Management & Service Team'),
('KHO','3. Inflight Service Dept.','3.3. Warehouse Management Team'),
('DVHK','4. Passenger Service Dept.','4.1. Dpt. Manager'),
('DVHKDI','4. Passenger Service Dept.','4.2. Departure Passenger Service Team'),
('DVHKDEN','4. Passenger Service Dept.','4.3. Arrival Passenger Service Team'),
('DHKT','5. Operation Management Dept.','5.1. Dpt. Manager'),
('DPBAY','5. Operation Management Dept.','5.2. Dispatch Team'),
('TBDH','5. Operation Management Dept.','5.3. Operation Management Team'),
('QLDVSD','5. Operation Management Dept.','5.4. Ground Management & Service Team'),
('ATCL','5. Operation Management Dept.','5.5. Quality Control Team'),
('ADMIN','6. System Management','6.1. Admin Team')
GO

INSERT INTO [TaiKhoan](TenTaiKhoan,MatKhau,MaDonVi,HoTen,Email,Mobile,DiaChi) VALUES
('admin.noc','21232f297a57a5a743894a0e4a801fc3','ADMIN',N'Admin Noc','admin.noc@vietnamairlines.com','0966424116',N'NoiBai International Airport, Ha Noi, Viet Nam'),
('nguyenvanan','202cb962ac59075b964b07152d234b70','DVHKDI',N'Nguyễn Văn An','nguyenvanan@gmail.com','0912345678',N'Ha Noi, VN'),
('tranthino','202cb962ac59075b964b07152d234b70','DVHKDEN',N'Trần Thị Nở','notranthi@gmail.com','0996521458',N'Ha Noi, VN')
GO

INSERT INTO [NghiepVu](MaNghiepVu,MaDonVi,TenNghiepVu) VALUES
('GLP','DVHKDI','Golden Lotus Plus'),
('WT','DVHKDEN','World Tracer'),
('SYSTEM','ADMIN','System Management')
GO

INSERT INTO [CauHoi](MaNghiepVu,CauHoi,TraLoi1,TraLoi2,TraLoi3,DapAn) VALUES
('GLP',N'Theo PHM, bạn cho biết mốc thời điểm nhân viên phục vụ mặt đất phải thông báo cho các đơn vị liên quan và offlk hành lý ký gửi của khách không có mặt tại cửa khởi hành',N'A. 05 phút trước ETD',N'B. 10 phút trước ETD',N'C. 15 phút trước ETD',2),
('GLP',N'Khách đến quầy làm thủ tục đi HAN – DAD trên VN169/ dte, khách khai báo bị bệnh tiểu đường cần phải mang bơm kim tiêm theo người để tiêm thuốc. Kiểm tra kim tiêm còn nguyên bao bì có độ dài 2.5cm, thủ tục viên báo NOC, là chuyên viên NOC trực chuyến bạn sẽ quyết định',N'A. Không chấp nhận khách mang theo bơm kim tiêm',N'B. Chấp nhận cho khách mang theo dạng hành lý xách tay',N'C. Chấp nhận cho khách mang theo hành lý ký gửi',3),
('GLP',N'Khách là trẻ em đi một mình đặt dịch vụ UM sẽ được nhân viên mặt đất dẫn từ phòng đợi ra máy bay',N'A. Bàn giao cho tiếp viên của chuyến bay',N'B. Bàn giao cho cơ trưởng của chuyến bay',N'C. Khách UM tự ra một mình',1),
('GLP',N'Trình tự gọi khách ra máy bay',N'A. Khách cần sự giúp đỡ, tàn tật, đau ốm, người già, phụ nữ có thai, người đi cùng trẻ nhỏ/ khách thường hạng phổ thông/ khách có thẻ hội viên, khách hàng thương gia/ khách VIP A',N'B. Khách VIP A/ khách có thẻ hội viên, khách hạng thương gia/ khách cần sự giúp đỡ, tàn tật, đau ốm, người già, phụ nữ có thai',N'C. Khách hạng thương gia, khách có thẻ hội viên/ khách VIP A/ khách cần sự giúp đỡ, tàn tật, đau ốm, người già, phụ nữ có thai.',1),
('GLP',N'Lý do VNA có quyền từ chối khách',N'A. Khách có hành vi làm mất trật tự công cộng, uy hiếp an toàn bay hoặc gây anh hưởng đến tính mạng, sức khỏe của người khác',N'B. Vì lý do an ninh hàng không, theo yêu cầu của nhà nước',N'C. Các câu trả lời trên',3),
('WT',N'Trên chuyến bay VN16/11JAN CDG-HAN, có 1 khách vào quầy khiếu nại hành lý của khách hiệu Samsonite bị hỏng tay cầm và vỡ toàn bộ phần bánh xe, khách có vé và thẻ hành lý đầy đủ. NOC gặp khách, xin lỗi và đề nghị đền bù cho khách 100USD, khách đồng ý nhưng yêu cầu VNA trả khách bằng MCO để khách mua dịch vụ của VNA sau này. Trong trường hợp này có thể trả bằng MCO được không? Nếu có thì giá trị MCO là bao nhiêu tiền?',N'A. NOC chỉ có thể trả bằng tiền mặt là 100USD cho việc đền bù hành lý rách vỡ.',N'B. Hướng dẫn khách về Chi nhánh Miền bắc (NRO) để viết MCO, giá trị MCO là 150USD',N'C. NOC đền bù bằng MCO, giá trị trên MCO là 150USD',3),
('WT',N'Khách Robert có hành trìnhVN016/11JAN CDG-HAN vé hạng D. Khách có 01 kiện hành lý ký gửi đến HAN chưa nhận được. L&F đã lập hồ sơ AHL và khi hành lý về thì đem trả hành lý tại khách sạn cho khách. 02 ngày sau hành lý được nhân viên NIAGS  bàn giao cho khách tại khách sạn trong tình trạng tốt. Khách khiếu nại yêu cầu VNA trả cho khách hóa đơn mua quần áo trị giá 100 USD (khách có hóa đơn tài chính phù hợp với qui định của Ban tài chính). Bạn sẽ bồi thường khách như thế nào ?',N'A. Đền bù 100USD (theo hóa đơn)',N'B. 40USD',N'C. 30USD',1),
('WT',N'Đoàn 4 khách của Bộ công thương đi trên chuyến bay VN17/23MAR HAN-CDG checkin 4 kiện hành lý (hành lý của khách nào check riêng khách đó), vé của khách là hạng Y, khi đến CDG toàn bộ hành lý của đoàn bị thiếu do bị kẹt băng chuyền tại HAN, CDGVN đã lập hồ sơ hành lý thiếu, toàn bộ hành lý của khách được chuyển đến CDG vào chuyến bay ngày hôm sau và CDGVN đã trả hành lý cho khách tại khách sạn. Khi khách về đến HAN đã yêu cầu NOC đền bù tiền hành lý vận chuyển chậm, tại thời điểm khách khiếu nại tại HAN, NOC không có thông tin từ CDG. Theo anh chị, trường hợp này khách có được đền bù không, số tiền đền bù là bao nhiêu?',N'A. Không vì trách nhiệm đền bù là của CDGVN do phát sinh xảy tại CDG',N'B. Có đền bù ngay cho khách, số tiền đền bù là 70USD x 4 = 280USD (quy đổi ra VNĐ theo tỷ giá).',N'C. Không đền bù ngay, VNA sẽ trả lời khách khi có đủ thông tin và hồ sơ, giải thích với khách quy định của VNA về bồi thường hành lý thiếu, số tiền có thể đền bù là 80USD x 4 = 320 USD (quy đổi ra VNĐ theo tỷ giá).',3),
('WT',N'Một khách quốc tịch Pháp có hành trình VN16/12DEC CDG-HAN, checkin 2pc/30kgs, khi về đến HAN khách đã nhận được 1 kiện/16kgs  và thiếu 1 kiện hành lý,  HANVN đã mở hồ sơ tìm kiếm cho khách. Sau đó 5 ngày khách đi tiếp RGN trên VN957, khách đến gặp NOC và yêu cầu đền bù hành lý mất bằng tiền USD. Trong trường hợp này, anh chị có đền cho khách không?',N'A. Đền bù 14kgsx 20USD = 280USD',N'B. Không đền bù vì chưa đủ 21 ngày theo quy định.',N'C. Có đền bù 280USD nhưng chỉ bằng VNĐ',1),
('WT',N'Một khách quốc tịch Nhật Bản đi trên chuyến bay VN224/4MAR SGN- HAN vào quầy khai báo mất 1 số đồ trong hành lý ký gửi, khách checkin 2pcs/20kgs, khi cân lại tại HAN, trọng lượng 2 kiện hành lý là 20,1 kgs. Kiểm tra trong 1 kiện hành lý ký gửi bị xáo trộn, khóa có dấu hiệu bị cong vênh, trọng lượng cân lại của kiện hành lý bị xáo trộn là 9,8kgs, khách kê khai mất 2 bộ quần áo, 1 đôi giầy, 1 máy ảnh, tổng trị giá 640USD. Trong trường hợp này có được đền bù không, nếu có giá trị đền bù tối đa là bao nhiêu?',N'A. Có đền bù, số tiền đền bù tối đa là 9,8kgs x 20USD= 196USD',N'B. Đền bù, số tiền đền bù tối đa là 20 kgs x 20USD + 150 USD (hành lý rách vỡ) = 550 USD',N'C. Không đền bù vì hành lý của khách không bị hao hụt trọng lượng (checkin 20kgs, cân lại tại HAN là 20,1kgs).',1)
GO

INSERT INTO DeThi(MaDeThi,MaNghiepVu,MaCauHoi1,MaCauHoi2,MaCauHoi3) VALUES
('GLP1','GLP',1,2,3),
('WT1','WT',6,7,8)

/*
INSERT INTO [BaiThi](MaDeThi) VALUES
('WT1'),('GLP1')
GO

INSERT INTO [KetQua](MaBaiThi,NgayThi,DiemSo) VALUES
(10000,'2015-11-20',9),
(10001,'2015-11-20',7)
GO
*/

SELECT * FROM [DeThi]
SELECT * FROM [DonVi] ORDER BY(TenDoi)

IF EXISTS(SELECT * FROM sys.views WHERE name='vwFullQuestionDetails')
DROP VIEW vwFullQuestionDetails
GO
CREATE VIEW vwFullQuestionDetails
AS
SELECT CH.MaCauHoi,CH.MaNghiepVu,DV.TenPhong,DV.TenDoi,NV.TenNghiepVu,CH.CauHoi,CH.TraLoi1,CH.TraLoi2,CH.TraLoi3,CH.DapAn
FROM [CauHoi] AS CH
INNER JOIN [NghiepVu] AS NV ON CH.MaNghiepVu=NV.MaNghiepVu
INNER JOIN [DonVi] AS DV ON nv.MaDonVi=dv.MaDonVi
WHERE CH.IsDeleted=0
GO

IF EXISTS(SELECT * FROM sys.views WHERE name='vwFullAccountDetails')
DROP VIEW vwFullAccountDetails
GO
CREATE VIEW vwFullAccountDetails
AS
SELECT TK.TenTaiKhoan,TK.HoTen,DV.TenPhong,DV.TenDoi,TK.Email,TK.Mobile
FROM [TaiKhoan] AS TK
INNER JOIN [DonVi] AS DV ON TK.MaDonVi=DV.MaDonVi
WHERE TK.IsDeleted=0
GO

IF EXISTS(SELECT * FROM sys.views WHERE name='vwFullExam')
DROP VIEW vwFullExam
GO
CREATE VIEW vwFullExam
AS
SELECT DT.MaDeThi,DT.MaNghiepVu,NV.TenNghiepVu,NV.MaDonVi,DT.MaCauHoi1,DT.MaCauHoi2,DT.MaCauHoi3
FROM [DeThi] AS DT
INNER JOIN [NghiepVu] AS NV ON DT.MaNghiepVu=NV.MaNghiepVu
WHERE DT.IsDeleted=0
GO

/*
IF EXISTS(SELECT * FROM sys.views WHERE name='vwResult')
DROP VIEW vwResult
GO
CREATE VIEW vwResult
AS
SELECT TK.TenTaiKhoan,TK.HoTen,DV.TenPhong,DV.TenDoi,NV.TenNghiepVu,BT.NgayThi,KQ.DiemSo
FROM [TaiKhoan] AS TK
INNER JOIN [DonVi] AS DV ON TK.MaDonVi=DV.MaDonVi
INNER JOIN [NghiepVu] AS NV ON DV.MaDonVi=NV.MaDonVi
INNER JOIN [BaiThi] AS BT ON TK.TenTaiKhoan=BT.TenTaiKhoan
INNER JOIN [KetQua] AS KQ ON BT.MaBaiThi=KQ.MaBaiThi
WHERE TK.IsDeleted=0
GO
*/

IF EXISTS (SELECT * FROM sys.views WHERE name='vwResult')
DROP VIEW vwResult
GO
CREATE VIEW vwResult
AS
SELECT TK.TenTaiKhoan,TK.HoTen,DV.TenPhong,DV.TenDoi,NV.TenNghiepVu,BT.NgayThi,KQ.DiemSo
FROM TaiKhoan AS TK
INNER JOIN BaiThi AS BT ON Tk.TenTaiKhoan=BT.TenTaiKhoan
INNER JOIN KetQua AS KQ ON BT.MaBaiThi=KQ.MaBaiThi
INNER JOIN DeThi AS DT ON BT.MaDeThi=DT.MaDeThi
INNER JOIN NghiepVu AS NV ON DT.MaNghiepVu=NV.MaNghiepVu
INNER JOIN DonVi AS DV ON NV.MaDonVi=DV.MaDonVi
WHERE TK.IsDeleted=0
GO

SELECT * FROM vwFullQuestionDetails
SELECT * FROM vwFullAccountDetails
SELECT * FROM vwFullExam
SELECT * FROM vwResult

EXECUTE sp_refreshview N'vwResult'