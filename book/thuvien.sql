CREATE TABLE Tua_Sach(
id_tua_sach BIGSERIAL PRIMARY KEY ,
ten_sach varchar(100) NOT NULL,
the_loai varchar(100) NOT NULL,
nam_xuat_ban int,
so_luong INT NOT NULL DEFAULT 0,
nha_xuat_ban varchar(100),
Thoi_gian DATE NOT NULL,
trang_thai BOOLEAN 
);

CREATE TABLE Tac_Gia (
    id_tac_gia BIGSERIAL PRIMARY KEY ,
    ten_tac_gia VARCHAR(255) 
);

CREATE TABLE TuaSach_TacGia (
    id_tua_sach INT REFERENCES Tua_Sach(id_tua_sach) ON DELETE CASCADE,
    id_tac_gia INT REFERENCES Tac_Gia(id_tac_gia) ON DELETE CASCADE,
    tac_gia_chinh BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (id_tua_sach, id_tac_gia)
);
CREATE TABLE Dau_Sach (
    id_dau_sach BIGSERIAL PRIMARY KEY,
    id_tua_sach BIGINT NOT NULL REFERENCES Tua_Sach(id_tua_sach) ON DELETE CASCADE,
    ma_dau_sach VARCHAR(50) UNIQUE NOT NULL, 
    trang_thai BOOLEAN,  
    ngay_nhap DATE NOT NULL DEFAULT CURRENT_DATE
);

select * from Tua_Sach;


