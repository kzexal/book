CREATE TABLE Tua_Sach(
ma_sach BIGSERIAL PRIMARY KEY ,
ten_sach varchar(100) NOT NULL,
tac_gia varchar(100) NOT NULL,
the_loai varchar(100) NOT NULL,
nam_xuat_ban int,
nha_xuat_ban varchar(100),
Thoi_gian DATE NOT NULL 
);

alter table Tua_Sach add column so_luong int NOT NULL;

insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('1984', 'George Orwell', 'Khoa học viễn tưởng',1949,'Secker & Warburg','24/11/2024',56);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Đắc nhân tâm', 'Dale Carnegie', 'Tự lực',1936,'Simon & Schuster','24/11/2025',15);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Nhà giả kim', 'Paulo Coelho', 'Phiêu lưu',1988,'HarperOne','24/12/2026',10);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Giết con chim nhại', 'Harper Lee', 'Văn học Mỹ',1960,'J. B. Lippincott & Co.','24/11/2027',20);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Hoàng tử bé', 'Antoine de Saint-Exupéry', 'Văn học thiếu nhi',1943,'Reynal & Hitchcock','30/11/2028',9);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Bắt trẻ đồng xanh', 'J.D. Salinger', 'Văn học trưởng thành',1951,'Little, Brown and Company','24/10/2029',8);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Chúa tể của những chiếc nhẫn', 'J.R.R. Tolkien', 'Giả tưởng',1954,'Allen & Unwin','2/11/2030',14);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Không gia đình', 'Hector Malot', 'Phiêu lưu',1878,'Flammarion','24/11/2031',16);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Harry Potter và Hòn đá Phù thủy', 'J.K. Rowling', 'Giả tưởng',1997,'Bloomsbury','24/1/2032',15);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Trăm năm cô đơn', 'Gabriel García Márquez', 'Hiện thực huyền ảo',1967,'Editorial Sudamericana','24/11/2033',18);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Rừng Nauy', 'Haruki Murakami', 'Tiểu thuyết',1987,'Kodansha','24/11/2034',15);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Sherlock Holmes toàn tập', 'Arthur Conan Doyle', 'Trinh thám',1887,'Nhiều NXB','24/11/2035',15);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Số đỏ', 'Vũ Trọng Phụng', 'Trào phúng',1936,'Lê Cường','24/11/2036',15);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Dế Mèn phiêu lưu ký', 'Tô Hoài', 'Thiếu nhi',1941,'NXB Tân Dân','24/11/2037',15);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Anne tóc đỏ dưới chái nhà xanh', 'Lucy Maud Montgomery', 'Văn học thiếu nhi',1908,'L.C. Page & Company','08/05/2038',30);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Những người khốn khổ', 'Victor Hugo', 'Tiểu thuyết',1862,'A. Lacroix, Verboeckhoven & Cie.','24/11/2039',100);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Chiến tranh và hòa bình', 'Lev Tolstoy', 'Tiểu thuyết',1869,'The Russian Messenger','24/11/2040',1000);
insert into Tua_Sach(ten_sach,tac_gia,the_loai,nam_xuat_ban,nha_xuat_ban,thoi_gian,so_luong) VALUES ('Tuổi thơ dữ dội', 'Phùng Quán', 'Văn học Việt Nam',1988,'NXB Văn Học','24/11/2041',19);

select * from Tua_Sach;

TRUNCATE TABLE Tua_sach RESTART IDENTITY;

