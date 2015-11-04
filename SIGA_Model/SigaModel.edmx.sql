
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/01/2015 14:05:14
-- Generated from EDMX file: E:\GitHub\SIGA\SIGA_Model\SigaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SIGA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Administrador_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Administrador] DROP CONSTRAINT [FK_Administrador_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Alumno_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Alumno] DROP CONSTRAINT [FK_Alumno_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Calificacion_Curso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Calificacion] DROP CONSTRAINT [FK_Calificacion_Curso];
GO
IF OBJECT_ID(N'[dbo].[FK_Curso_Modulo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curso] DROP CONSTRAINT [FK_Curso_Modulo];
GO
IF OBJECT_ID(N'[dbo].[FK_Curso_Programacion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curso] DROP CONSTRAINT [FK_Curso_Programacion];
GO
IF OBJECT_ID(N'[dbo].[FK_Matricula_Curso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Matricula] DROP CONSTRAINT [FK_Matricula_Curso];
GO
IF OBJECT_ID(N'[dbo].[FK_Matricula_Recibo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Matricula] DROP CONSTRAINT [FK_Matricula_Recibo];
GO
IF OBJECT_ID(N'[dbo].[FK_Profesor_Curso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profesor] DROP CONSTRAINT [FK_Profesor_Curso];
GO
IF OBJECT_ID(N'[dbo].[FK_Profesor_Usuario1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profesor] DROP CONSTRAINT [FK_Profesor_Usuario1];
GO
IF OBJECT_ID(N'[dbo].[FK_Programacion_Aula]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Programacion] DROP CONSTRAINT [FK_Programacion_Aula];
GO
IF OBJECT_ID(N'[dbo].[FK_Programacion_Horario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Programacion] DROP CONSTRAINT [FK_Programacion_Horario];
GO
IF OBJECT_ID(N'[dbo].[FK_Recibo_Pago]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Recibo] DROP CONSTRAINT [FK_Recibo_Pago];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuario_Persona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Persona];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuario_Tipo_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Tipo_Usuario];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Administrador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Administrador];
GO
IF OBJECT_ID(N'[dbo].[Alumno]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Alumno];
GO
IF OBJECT_ID(N'[dbo].[Aula]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Aula];
GO
IF OBJECT_ID(N'[dbo].[Calificacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calificacion];
GO
IF OBJECT_ID(N'[dbo].[Curso]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Curso];
GO
IF OBJECT_ID(N'[dbo].[Horario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Horario];
GO
IF OBJECT_ID(N'[dbo].[Matricula]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Matricula];
GO
IF OBJECT_ID(N'[dbo].[Modulo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modulo];
GO
IF OBJECT_ID(N'[dbo].[Pago]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pago];
GO
IF OBJECT_ID(N'[dbo].[Persona]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persona];
GO
IF OBJECT_ID(N'[dbo].[Profesor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profesor];
GO
IF OBJECT_ID(N'[dbo].[Programacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Programacion];
GO
IF OBJECT_ID(N'[dbo].[Recibo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Recibo];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TipoUsuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoUsuario];
GO
IF OBJECT_ID(N'[dbo].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuario];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Administrador'
CREATE TABLE [dbo].[Administrador] (
    [Adm_ID] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL,
    [Adm_Area] varchar(50)  NULL,
    [Adm_Rpm] char(12)  NULL,
    [Adm_Nextel] char(12)  NOT NULL
);
GO

-- Creating table 'Alumno'
CREATE TABLE [dbo].[Alumno] (
    [Alu_Id] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL,
    [Alu_Apoderado] varchar(50)  NULL,
    [Alu_Estado] bit  NOT NULL,
    [Alu_Foto] varchar(200)  NULL,
    [Alu_FechaIngreso] datetime  NOT NULL,
    [Alu_FechaRegistro] datetime  NULL
);
GO

-- Creating table 'Aula'
CREATE TABLE [dbo].[Aula] (
    [Aul_Id] int IDENTITY(1,1) NOT NULL,
    [Aul_Capacidad] varchar(50)  NULL
);
GO

-- Creating table 'Calificacion'
CREATE TABLE [dbo].[Calificacion] (
    [User_Id] int  NOT NULL,
    [Cur_Id] int  NOT NULL,
    [Cal_Fech] datetime  NULL,
    [Cal_Condicion] varchar(50)  NOT NULL,
    [Cal_Practica] decimal(12,2)  NOT NULL,
    [Cal_Teorica] decimal(12,2)  NOT NULL,
    [Cal_Oral] decimal(12,2)  NULL,
    [Cal_Otros] decimal(12,2)  NULL
);
GO

-- Creating table 'Curso'
CREATE TABLE [dbo].[Curso] (
    [Cur_Id] int IDENTITY(1,1) NOT NULL,
    [Mod_Id] int  NOT NULL,
    [Prog_Id] int  NOT NULL,
    [Cur_Name] varchar(50)  NULL,
    [Cur_NumHoras] int  NULL,
    [Cur_Precio] nchar(10)  NULL
);
GO

-- Creating table 'Horario'
CREATE TABLE [dbo].[Horario] (
    [Hor_Id] int IDENTITY(1,1) NOT NULL,
    [Hor_Grupo] varchar(50)  NULL,
    [Hor_Ini] time  NULL,
    [Hor_Dia] varchar(50)  NULL,
    [Hor_Turno] varchar(50)  NULL,
    [Hor_Fin] time  NULL
);
GO

-- Creating table 'Matricula'
CREATE TABLE [dbo].[Matricula] (
    [Mat_Id] int IDENTITY(1,1) NOT NULL,
    [Cur_Id] int  NOT NULL,
    [User_Id] int  NOT NULL,
    [Rec_Id] int  NOT NULL,
    [Mat_Tipo] varchar(50)  NULL,
    [Mat_Fecha] datetime  NOT NULL,
    [Mat_Estado] bit  NOT NULL
);
GO

-- Creating table 'Modulo'
CREATE TABLE [dbo].[Modulo] (
    [Mod_Id] int IDENTITY(1,1) NOT NULL,
    [Mod_Nombre] varchar(50)  NOT NULL,
    [Mod_Tipo] varchar(50)  NOT NULL,
    [Mod_Unidad] varchar(50)  NULL,
    [Mod_NumHoras] varchar(50)  NULL,
    [Mod_NumMes] varchar(50)  NOT NULL,
    [Mod_NumCursos] nchar(10)  NULL,
    [Mod_Nivel] int  NULL
);
GO

-- Creating table 'Pago'
CREATE TABLE [dbo].[Pago] (
    [Pag_Id] int IDENTITY(1,1) NOT NULL,
    [Alu_Id] int  NULL,
    [Pag_Fech] datetime  NULL
);
GO

-- Creating table 'Persona'
CREATE TABLE [dbo].[Persona] (
    [Per_Id] int IDENTITY(1,1) NOT NULL,
    [Per_Dni] int  NOT NULL,
    [Per_Nombre] varchar(50)  NULL,
    [Per_ApePaterno] varchar(50)  NULL,
    [Per_ApeMaterno] varchar(50)  NOT NULL,
    [Per_Sexo] char(1)  NULL,
    [Per_Dir] varchar(50)  NULL,
    [Per_Cel] char(9)  NULL,
    [Per_Tel] varchar(20)  NOT NULL,
    [Per_Email] varchar(50)  NULL,
    [Per_FechaNacimiento] datetime  NULL
);
GO

-- Creating table 'Profesor'
CREATE TABLE [dbo].[Profesor] (
    [Prof_Id] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL,
    [Cur_Id] int  NOT NULL,
    [Prof_Especialidad] varchar(50)  NULL
);
GO

-- Creating table 'Programacion'
CREATE TABLE [dbo].[Programacion] (
    [Prog_Id] int IDENTITY(1,1) NOT NULL,
    [Aul_Id] int  NOT NULL,
    [Hor_Id] int  NOT NULL
);
GO

-- Creating table 'Recibo'
CREATE TABLE [dbo].[Recibo] (
    [Rec_Id] int  NOT NULL,
    [User_Id] int  NOT NULL,
    [Pag_Id] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TipoUsuario'
CREATE TABLE [dbo].[TipoUsuario] (
    [TipoUser_Id] int IDENTITY(1,1) NOT NULL,
    [TipoUser_Descrip] varchar(50)  NULL,
    [TipoUser_Nivel] tinyint  NOT NULL
);
GO

-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [User_Id] int IDENTITY(1,1) NOT NULL,
    [Per_Id] int  NOT NULL,
    [TipoUser_Id] int  NOT NULL,
    [User_Nombre] varchar(50)  NOT NULL,
    [User_Pass] varchar(20)  NOT NULL,
    [User_Inactivo] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Adm_ID] in table 'Administrador'
ALTER TABLE [dbo].[Administrador]
ADD CONSTRAINT [PK_Administrador]
    PRIMARY KEY CLUSTERED ([Adm_ID] ASC);
GO

-- Creating primary key on [Alu_Id] in table 'Alumno'
ALTER TABLE [dbo].[Alumno]
ADD CONSTRAINT [PK_Alumno]
    PRIMARY KEY CLUSTERED ([Alu_Id] ASC);
GO

-- Creating primary key on [Aul_Id] in table 'Aula'
ALTER TABLE [dbo].[Aula]
ADD CONSTRAINT [PK_Aula]
    PRIMARY KEY CLUSTERED ([Aul_Id] ASC);
GO

-- Creating primary key on [User_Id], [Cur_Id] in table 'Calificacion'
ALTER TABLE [dbo].[Calificacion]
ADD CONSTRAINT [PK_Calificacion]
    PRIMARY KEY CLUSTERED ([User_Id], [Cur_Id] ASC);
GO

-- Creating primary key on [Cur_Id] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [PK_Curso]
    PRIMARY KEY CLUSTERED ([Cur_Id] ASC);
GO

-- Creating primary key on [Hor_Id] in table 'Horario'
ALTER TABLE [dbo].[Horario]
ADD CONSTRAINT [PK_Horario]
    PRIMARY KEY CLUSTERED ([Hor_Id] ASC);
GO

-- Creating primary key on [Mat_Id] in table 'Matricula'
ALTER TABLE [dbo].[Matricula]
ADD CONSTRAINT [PK_Matricula]
    PRIMARY KEY CLUSTERED ([Mat_Id] ASC);
GO

-- Creating primary key on [Mod_Id] in table 'Modulo'
ALTER TABLE [dbo].[Modulo]
ADD CONSTRAINT [PK_Modulo]
    PRIMARY KEY CLUSTERED ([Mod_Id] ASC);
GO

-- Creating primary key on [Pag_Id] in table 'Pago'
ALTER TABLE [dbo].[Pago]
ADD CONSTRAINT [PK_Pago]
    PRIMARY KEY CLUSTERED ([Pag_Id] ASC);
GO

-- Creating primary key on [Per_Id] in table 'Persona'
ALTER TABLE [dbo].[Persona]
ADD CONSTRAINT [PK_Persona]
    PRIMARY KEY CLUSTERED ([Per_Id] ASC);
GO

-- Creating primary key on [Prof_Id] in table 'Profesor'
ALTER TABLE [dbo].[Profesor]
ADD CONSTRAINT [PK_Profesor]
    PRIMARY KEY CLUSTERED ([Prof_Id] ASC);
GO

-- Creating primary key on [Prog_Id] in table 'Programacion'
ALTER TABLE [dbo].[Programacion]
ADD CONSTRAINT [PK_Programacion]
    PRIMARY KEY CLUSTERED ([Prog_Id] ASC);
GO

-- Creating primary key on [Rec_Id] in table 'Recibo'
ALTER TABLE [dbo].[Recibo]
ADD CONSTRAINT [PK_Recibo]
    PRIMARY KEY CLUSTERED ([Rec_Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [TipoUser_Id] in table 'TipoUsuario'
ALTER TABLE [dbo].[TipoUsuario]
ADD CONSTRAINT [PK_TipoUsuario]
    PRIMARY KEY CLUSTERED ([TipoUser_Id] ASC);
GO

-- Creating primary key on [User_Id] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([User_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'Administrador'
ALTER TABLE [dbo].[Administrador]
ADD CONSTRAINT [FK_Administrador_Usuario]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Usuario]
        ([User_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Administrador_Usuario'
CREATE INDEX [IX_FK_Administrador_Usuario]
ON [dbo].[Administrador]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Alumno'
ALTER TABLE [dbo].[Alumno]
ADD CONSTRAINT [FK_Alumno_Usuario]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Usuario]
        ([User_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Alumno_Usuario'
CREATE INDEX [IX_FK_Alumno_Usuario]
ON [dbo].[Alumno]
    ([User_Id]);
GO

-- Creating foreign key on [Aul_Id] in table 'Programacion'
ALTER TABLE [dbo].[Programacion]
ADD CONSTRAINT [FK_Programacion_Aula]
    FOREIGN KEY ([Aul_Id])
    REFERENCES [dbo].[Aula]
        ([Aul_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Programacion_Aula'
CREATE INDEX [IX_FK_Programacion_Aula]
ON [dbo].[Programacion]
    ([Aul_Id]);
GO

-- Creating foreign key on [Cur_Id] in table 'Calificacion'
ALTER TABLE [dbo].[Calificacion]
ADD CONSTRAINT [FK_Calificacion_Curso]
    FOREIGN KEY ([Cur_Id])
    REFERENCES [dbo].[Curso]
        ([Cur_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Calificacion_Curso'
CREATE INDEX [IX_FK_Calificacion_Curso]
ON [dbo].[Calificacion]
    ([Cur_Id]);
GO

-- Creating foreign key on [Mod_Id] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [FK_Curso_Modulo]
    FOREIGN KEY ([Mod_Id])
    REFERENCES [dbo].[Modulo]
        ([Mod_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Curso_Modulo'
CREATE INDEX [IX_FK_Curso_Modulo]
ON [dbo].[Curso]
    ([Mod_Id]);
GO

-- Creating foreign key on [Prog_Id] in table 'Curso'
ALTER TABLE [dbo].[Curso]
ADD CONSTRAINT [FK_Curso_Programacion]
    FOREIGN KEY ([Prog_Id])
    REFERENCES [dbo].[Programacion]
        ([Prog_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Curso_Programacion'
CREATE INDEX [IX_FK_Curso_Programacion]
ON [dbo].[Curso]
    ([Prog_Id]);
GO

-- Creating foreign key on [Cur_Id] in table 'Matricula'
ALTER TABLE [dbo].[Matricula]
ADD CONSTRAINT [FK_Matricula_Curso]
    FOREIGN KEY ([Cur_Id])
    REFERENCES [dbo].[Curso]
        ([Cur_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Matricula_Curso'
CREATE INDEX [IX_FK_Matricula_Curso]
ON [dbo].[Matricula]
    ([Cur_Id]);
GO

-- Creating foreign key on [Cur_Id] in table 'Profesor'
ALTER TABLE [dbo].[Profesor]
ADD CONSTRAINT [FK_Profesor_Curso]
    FOREIGN KEY ([Cur_Id])
    REFERENCES [dbo].[Curso]
        ([Cur_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Profesor_Curso'
CREATE INDEX [IX_FK_Profesor_Curso]
ON [dbo].[Profesor]
    ([Cur_Id]);
GO

-- Creating foreign key on [Hor_Id] in table 'Programacion'
ALTER TABLE [dbo].[Programacion]
ADD CONSTRAINT [FK_Programacion_Horario]
    FOREIGN KEY ([Hor_Id])
    REFERENCES [dbo].[Horario]
        ([Hor_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Programacion_Horario'
CREATE INDEX [IX_FK_Programacion_Horario]
ON [dbo].[Programacion]
    ([Hor_Id]);
GO

-- Creating foreign key on [Rec_Id] in table 'Matricula'
ALTER TABLE [dbo].[Matricula]
ADD CONSTRAINT [FK_Matricula_Recibo]
    FOREIGN KEY ([Rec_Id])
    REFERENCES [dbo].[Recibo]
        ([Rec_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Matricula_Recibo'
CREATE INDEX [IX_FK_Matricula_Recibo]
ON [dbo].[Matricula]
    ([Rec_Id]);
GO

-- Creating foreign key on [Pag_Id] in table 'Recibo'
ALTER TABLE [dbo].[Recibo]
ADD CONSTRAINT [FK_Recibo_Pago]
    FOREIGN KEY ([Pag_Id])
    REFERENCES [dbo].[Pago]
        ([Pag_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Recibo_Pago'
CREATE INDEX [IX_FK_Recibo_Pago]
ON [dbo].[Recibo]
    ([Pag_Id]);
GO

-- Creating foreign key on [Per_Id] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Persona]
    FOREIGN KEY ([Per_Id])
    REFERENCES [dbo].[Persona]
        ([Per_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Persona'
CREATE INDEX [IX_FK_Usuario_Persona]
ON [dbo].[Usuario]
    ([Per_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Profesor'
ALTER TABLE [dbo].[Profesor]
ADD CONSTRAINT [FK_Profesor_Usuario1]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Usuario]
        ([User_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Profesor_Usuario1'
CREATE INDEX [IX_FK_Profesor_Usuario1]
ON [dbo].[Profesor]
    ([User_Id]);
GO

-- Creating foreign key on [TipoUser_Id] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Tipo_Usuario]
    FOREIGN KEY ([TipoUser_Id])
    REFERENCES [dbo].[TipoUsuario]
        ([TipoUser_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Tipo_Usuario'
CREATE INDEX [IX_FK_Usuario_Tipo_Usuario]
ON [dbo].[Usuario]
    ([TipoUser_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------