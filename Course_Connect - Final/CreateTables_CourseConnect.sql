CREATE TABLE TaiKhoanQTV (
    MaTK    VARCHAR (10) NOT NULL,
    TenTK   VARCHAR (50) NULL,
    MatKhau VARCHAR (50) NULL,
    Email   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED (MaTK ASC)
);

CREATE TABLE TaiKhoanGiangVien (
    maTK          VARCHAR (10) NOT NULL,
    username      VARCHAR (50) NULL,
    password      VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED (maTK ASC)
);

CREATE TABLE ThongTinGiangVien (
    MaGV     VARCHAR (10)   NOT NULL,
    HoTen    TEXT (100) NULL,
    GioiTinh TEXT (5)   NULL,
    NgaySinh DATE           NULL,
    HocVi    TEXT (50)  NULL,
    Email    VARCHAR (100)  NULL,
    SoDT     VARCHAR (11)   NULL,
    PRIMARY KEY CLUSTERED (MaGV ASC),
    FOREIGN KEY (MaGV) REFERENCES TaiKhoanGiangVien (maTK)
);

CREATE TABLE TaiKhoanHocVien (
    maTK          VARCHAR (10) NOT NULL,
    username      VARCHAR (50) NULL,
    password      VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED (maTK ASC)
);

CREATE TABLE ThongTinHocVien (
    MaHV     VARCHAR (10)  NOT NULL,
    HoTen    TEXT (100) NULL,
    GioiTinh TEXT (5)  NULL,
    NgaySinh DATE          NULL,
    Email    VARCHAR (100)  NULL,
    SoDT     VARCHAR (11)  NULL,
    PRIMARY KEY CLUSTERED (MaHV ASC)
);

CREATE TABLE HocPhan (
    MaHP  VARCHAR (10)   NOT NULL,
    TenHP TEXT (100) NULL,
    SoTC  INT            NULL,
    PRIMARY KEY CLUSTERED (MaHP ASC)
);

CREATE TABLE DangKyHocPhan (
    MaHV   VARCHAR (10) NOT NULL,
    MaHP   VARCHAR (10) NOT NULL,
    MaGV   VARCHAR (10) NOT NULL,
    NgayDK DATE         NULL,
    PRIMARY KEY CLUSTERED (MaHV ASC, MaHP ASC, MaGV ASC),
    FOREIGN KEY (MaHV) REFERENCES ThongTinHocVien (MaHV),
    FOREIGN KEY (MaHP) REFERENCES HocPhan (MaHP),
    FOREIGN KEY (MaGV) REFERENCES ThongTinGiangVien (MaGV)
);

CREATE TABLE GiangVien_HocPhan (
    MaGV VARCHAR (10) NOT NULL,
    MaHP VARCHAR (10) NOT NULL,
    CONSTRAINT PK_GiangVien_HocPhan PRIMARY KEY CLUSTERED (MaGV ASC, MaHP ASC),
    FOREIGN KEY (MaGV) REFERENCES ThongTinGiangVien (MaGV),
    FOREIGN KEY (MaHP) REFERENCES HocPhan (MaHP)
);

CREATE TABLE NhomHoc (
    MaNhom          VARCHAR (10) PRIMARY KEY,
    MaHP            VARCHAR (10),
    GiangVienDay    VARCHAR (10),
    FOREIGN KEY (GiangVienDay) REFERENCES ThongTinGiangVien(MaGV),
    FOREIGN KEY (MaHP) REFERENCES HocPhan(MaHP)
)

CREATE TABLE LichHoc (
    MaNhom          VARCHAR (10) NOT NULL,
    ThoiGianBatDau  DATE,
    ThoiGianKetThuc DATE,
    GhiChuThem      TEXT (50),
    FOREIGN KEY (MaNhom) REFERENCES NhomHoc(MaNhom)
)
