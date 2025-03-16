CREATE TABLE Tua_Sach(
id_tua_sach BIGSERIAL PRIMARY KEY ,
ten_sach varchar(100) NOT NULL,
the_loai varchar(100) NOT NULL,
nam_xuat_ban int,
nha_xuat_ban varchar(100),
Thoi_gian DATE NOT NULL 
);

alter table Tua_Sach add column so_luong int NOT NULL;

CREATE TABLE Tac_Gia (
    id_tac_gia BIGSERIAL PRIMARY KEY ,
    ten_tac_gia VARCHAR(255) UNIQUE
);

CREATE TABLE TuaSach_TacGia (
    id_tua_sach INT REFERENCES Tua_Sach(id_tua_sach) ON DELETE CASCADE,
    id_tac_gia INT REFERENCES Tac_Gia(id_tac_gia) ON DELETE CASCADE,
    tac_gia_chinh BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (id_tua_sach, id_tac_gia)
);





select * from Tua_Sach;

TRUNCATE TABLE Tua_sach RESTART IDENTITY;

