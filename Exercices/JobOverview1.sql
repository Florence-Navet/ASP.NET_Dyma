
CREATE TABLE Filières
(
  Codes VARCHAR(20) NOT NULL,
  Nom   VARCHAR(60) NOT NULL,
  CONSTRAINT PK_Filières PRIMARY KEY (Codes)
)
GO

CREATE TABLE Logiciels
(
  Code         VARCHAR(20) NOT NULL,
  Codes        VARCHAR(20) NOT NULL,
  CodesFilière VARCHAR(20) NOT NULL,
  Nom          VARCHAR(60) NOT NULL,
  CONSTRAINT PK_Logiciels PRIMARY KEY (Code)
)
GO

CREATE TABLE Modules
(
  Code               VARCHAR(20) NOT NULL,
  CodeLogiciel       VARCHAR(20) NOT NULL,
  Nom                VARCHAR(20) NOT NULL,
  CodeModuleParent   VARCHAR(20) NOT NULL,
  CodeLogicielParent VARCHAR(20) NOT NULL,
  CONSTRAINT PK_Modules PRIMARY KEY (Code, CodeLogiciel)
)
GO

CREATE TABLE Releases
(
  Numero        float NOT NULL,
  NumeroVersion float NOT NULL,
  CodeLogiciel  VARCHAR(20) NOT NULL,
  DatePubli     DATE  NOT NULL,
  CONSTRAINT PK_Release PRIMARY KEY (Numero, NumeroVersion, CodeLogiciel)
)
GO

CREATE TABLE Versions
(
  Numero           float       NOT NULL,
  CodeLogiciel     VARCHAR(20) NOT NULL,
  Millésime        SMALLINT    NOT NULL,
  DateOuverture    DATE        NOT NULL,
  DateSortiePrévue DATE        NOT NULL,
  DateSortieRéelle DATE       ,
  CONSTRAINT PK_Versions PRIMARY KEY (Numero, CodeLogiciel)
)
GO

ALTER TABLE Versions
  ADD CONSTRAINT FK_Logiciels_TO_Versions
    FOREIGN KEY (CodeLogiciel)
    REFERENCES Logiciels (Code)
GO

ALTER TABLE Releases
  ADD CONSTRAINT FK_Versions_TO_Release
    FOREIGN KEY (NumeroVersion, CodeLogiciel)
    REFERENCES Versions (Numero, CodeLogiciel)
GO

ALTER TABLE Modules
  ADD CONSTRAINT FK_Logiciels_TO_Modules
    FOREIGN KEY (CodeLogiciel)
    REFERENCES Logiciels (Code)
GO

ALTER TABLE Logiciels
  ADD CONSTRAINT FK_Filières_TO_Logiciels
    FOREIGN KEY (Codes)
    REFERENCES Filières (Codes)
GO

ALTER TABLE Modules
  ADD CONSTRAINT FK_Modules_TO_Modules
    FOREIGN KEY (CodeModuleParent, CodeLogicielParent)
    REFERENCES Modules (Code, CodeLogiciel)
GO
