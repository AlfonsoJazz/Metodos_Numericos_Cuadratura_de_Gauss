CREATE DATABASE METODOS_ALV;

USE METODOS_ALV;

CREATE TABLE DISTRIBUCION_NORMAL(
N INT,
K INT,
LAMBDA DECIMAL(18,9),
A DECIMAL(18,9)
);

INSERT INTO DISTRIBUCION_NORMAL VALUES (1,1,0,2);
INSERT INTO DISTRIBUCION_NORMAL VALUES (2,1,-0.5773527,1);
INSERT INTO DISTRIBUCION_NORMAL VALUES (2,2,0.5773527,1);
INSERT INTO DISTRIBUCION_NORMAL VALUES (3,1,-0.77459667,0.55555556);
INSERT INTO DISTRIBUCION_NORMAL VALUES (3,2,0,0.88888889);
INSERT INTO DISTRIBUCION_NORMAL VALUES (3,3,0.77459667,0.55555556);
INSERT INTO DISTRIBUCION_NORMAL VALUES (4,1,-0.86113631,0.34785484);
INSERT INTO DISTRIBUCION_NORMAL VALUES (4,2,-0.33998104,0.65214516);
INSERT INTO DISTRIBUCION_NORMAL VALUES (4,3,0.33998104,0.65214516);
INSERT INTO DISTRIBUCION_NORMAL VALUES (4,4,0.86113631,0.34785484);
INSERT INTO DISTRIBUCION_NORMAL VALUES (5,1,-0.90617985,0.23692688);
INSERT INTO DISTRIBUCION_NORMAL VALUES (5,2,-0.53846931,0.47862868);
INSERT INTO DISTRIBUCION_NORMAL VALUES (5,3,0,0.56888889);
INSERT INTO DISTRIBUCION_NORMAL VALUES (5,4,0.53846931,0.47862868);
INSERT INTO DISTRIBUCION_NORMAL VALUES (5,5,0.90617985,0.23692688);
INSERT INTO DISTRIBUCION_NORMAL VALUES (6,1,-0.93246951,0.1713245);
INSERT INTO DISTRIBUCION_NORMAL VALUES (6,2,-0.66120039,0.36076158);
INSERT INTO DISTRIBUCION_NORMAL VALUES (6,3,-0.23861919,0.46791394);
INSERT INTO DISTRIBUCION_NORMAL VALUES (6,4,0.23861919,0.46791394);
INSERT INTO DISTRIBUCION_NORMAL VALUES (6,5,0.66120039,0.36076158);
INSERT INTO DISTRIBUCION_NORMAL VALUES (6,6,0.93246951,0.1713245);
INSERT INTO DISTRIBUCION_NORMAL VALUES (7,1,-0.94910791,0.12948496);
INSERT INTO DISTRIBUCION_NORMAL VALUES (7,2,-0.74153119,0.2797054);
INSERT INTO DISTRIBUCION_NORMAL VALUES (7,3,-0.40584515,0.38183006);
INSERT INTO DISTRIBUCION_NORMAL VALUES (7,4,0,0.41795918);
INSERT INTO DISTRIBUCION_NORMAL VALUES (7,5,0.40584515,0.38183006);
INSERT INTO DISTRIBUCION_NORMAL VALUES (7,6,0.74153119,0.2797054);
INSERT INTO DISTRIBUCION_NORMAL VALUES (7,7,0.94910791,0.12948496);
INSERT INTO DISTRIBUCION_NORMAL VALUES (8,1,-0.96028986,0.10122854);
INSERT INTO DISTRIBUCION_NORMAL VALUES (8,2,-0.79666648,0.22238104);
INSERT INTO DISTRIBUCION_NORMAL VALUES (8,3,-0.52553242,0.31370664);
INSERT INTO DISTRIBUCION_NORMAL VALUES (8,4,-0.18343464,0.36268378);
INSERT INTO DISTRIBUCION_NORMAL VALUES (8,5,0.18343464,0.36268378);
INSERT INTO DISTRIBUCION_NORMAL VALUES (8,6,0.52553242,0.31370664);
INSERT INTO DISTRIBUCION_NORMAL VALUES (8,7,0.79666648,0.22238104);
INSERT INTO DISTRIBUCION_NORMAL VALUES (8,8,0.96028986,0.10122854);


USE CHU

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alfonso Mosco H
-- Create date: 25-11-2018
-- Description:	Selecciona un registro de la tabla de distribucion normal
-- =============================================
CREATE PROCEDURE usp_Selecciona_Distribucion_Normal
(
	@n INT
)
AS
BEGIN 
	SELECT * FROM DISTRIBUCION_NORMAL
	WHERE N = @n 
END
GO