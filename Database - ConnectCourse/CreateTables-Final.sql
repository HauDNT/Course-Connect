CREATE TABLE [dbo].[TaiKhoanQTV] (
    [MaTK]    VARCHAR (10) NOT NULL,
    [TenTK]   VARCHAR (50) NULL,
    [MatKhau] VARCHAR (50) NULL,
    [Email]   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([MaTK] ASC)
);

GO
CREATE TABLE [dbo].[TaiKhoanGiangVien] (
    [maTK]          VARCHAR (10) NOT NULL,
    [username]      VARCHAR (50) NULL,
    [password]      VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([maTK] ASC)
);

GO
CREATE TABLE [dbo].[ThongTinGiangVien] (
    [MaGV]     VARCHAR (10)   NOT NULL,
    [HoTen]    NVARCHAR (100) NULL,
    [GioiTinh] NVARCHAR (5)   NULL,
    [NgaySinh] DATE           NULL,
    [HocVi]    NVARCHAR (50)  NULL,
    [Email]    VARCHAR (100)  NULL,
    [SoDT]     VARCHAR (11)   NULL,
    PRIMARY KEY CLUSTERED ([MaGV] ASC),
    FOREIGN KEY ([MaGV]) REFERENCES [dbo].[TaiKhoanGiangVien] ([maTK])
);

GO
CREATE TABLE [dbo].[TaiKhoanHocVien] (
    [maTK]          VARCHAR (10) NOT NULL,
    [username]      VARCHAR (50) NULL,
    [password]      VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([maTK] ASC)
);

GO
CREATE TABLE [dbo].[ThongTinHocVien] (
    [MaHV]     VARCHAR (10)  NOT NULL,
    [HoTen]    NVARCHAR (100) NULL,
    [GioiTinh] NVARCHAR (5)  NULL,
    [NgaySinh] DATE          NULL,
    [Email]    VARCHAR (100)  NULL,
    [SoDT]     VARCHAR (11)  NULL,
    PRIMARY KEY CLUSTERED ([MaHV] ASC)
);

GO
CREATE TABLE [dbo].[HocPhan] (
    [MaHP]  VARCHAR (10)   NOT NULL,
    [TenHP] NVARCHAR (100) NULL,
    [SoTC]  INT            NULL,
    PRIMARY KEY CLUSTERED ([MaHP] ASC)
);

GO
CREATE TABLE [dbo].[DangKyHocPhan] (
    [MaHV]   VARCHAR (10) NOT NULL,
    [MaHP]   VARCHAR (10) NOT NULL,
    [MaGV]   VARCHAR (10) NOT NULL,
    [NgayDK] DATE         NULL,
    PRIMARY KEY CLUSTERED ([MaHV] ASC, [MaHP] ASC, [MaGV] ASC),
    FOREIGN KEY ([MaHV]) REFERENCES [dbo].[ThongTinHocVien] ([MaHV]),
    FOREIGN KEY ([MaHP]) REFERENCES [dbo].[HocPhan] ([MaHP]),
    FOREIGN KEY ([MaGV]) REFERENCES [dbo].[ThongTinGiangVien] ([MaGV])
);

GO
CREATE TABLE [dbo].[GiangVien_HocPhan] (
    [MaGV] VARCHAR (10) NOT NULL,
    [MaHP] VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_GiangVien_HocPhan] PRIMARY KEY CLUSTERED ([MaGV] ASC, [MaHP] ASC),
    FOREIGN KEY ([MaGV]) REFERENCES [dbo].[ThongTinGiangVien] ([MaGV]),
    FOREIGN KEY ([MaHP]) REFERENCES [dbo].[HocPhan] ([MaHP])
);

GO
CREATE TABLE NhomHoc (
    MaNhom          VARCHAR (10) PRIMARY KEY,
    MaHP            VARCHAR (10),
    GiangVienDay    VARCHAR (10),
    FOREIGN KEY (GiangVienDay) REFERENCES ThongTinGiangVien(MaGV),
    FOREIGN KEY (MaHP) REFERENCES HocPhan(MaHP)
)

GO
CREATE TABLE LichHoc (
    MaNhom          VARCHAR (10) NOT NULL,
    ThoiGianBatDau  DATE,
    ThoiGianKetThuc DATE,
    GhiChuThem      NVARCHAR (50),
    FOREIGN KEY (MaNhom) REFERENCES NhomHoc(MaNhom)
)
