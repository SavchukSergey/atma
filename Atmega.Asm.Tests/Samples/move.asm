﻿;Операции перемещения данных
;Мнемоника	Описание	Операция	Изменяемые флаги	Время исполнения (циклов)
MOV Rn,Rk	;Копировать Rk в Rn	Rn <- Rk	-	1
MOVW Rn,Rk	;Копировать регистровую пару	Rn+1:Rn <- Rk+1:Rk	-	1
LD Rn, Ri	;Загрузить из памяти данных, используя индексный регистр	Rn <- (Ri)	-	2
LD Rn, Ri+	;Rn <- (Ri), Ri <- Ri + 1	-	2
LD Rn, -Ri	;Ri <- Ri - 1, Rn <- (Ri)	-	2
LDD Rn, Ri + q	;Rn <- (Ri + q)	-	2
LDI Rn,K	;Загрузить константу в регистр	Rn <- K	-	1
LDS Rn,K	;Загрузить в регистр из памяти данных	Rn <- (K)	-	2
LPM	;Загрузить из памяти программ	R0 <- (Z)	-	3
LPM Rn, Z	;Rn <- (Z)	-	3
LPM Rn, Z+	;Rn <- (Z), Z <- Z + 1	-	3
ELPM	;R0 <- (RAMPZ:Z)	-	3
ELPM Rn, Z	;Rn <- (RAMPZ:Z)	-	3
ELPM Rn, Z+	;Rn <- (RAMPZ:Z), Z <- Z + 1	-	3
SPM	;Загрузить в память программ	(Z) <- R1:R0	-	3
STS K,Rn	;Загрузить в память данных	(K) <- Rn	-	2
ST Ri, Rn	;Загрузить в память данных, используя индексный регистр	(Ri) <- Rn	-	2
ST Ri+, Rn	;(Ri) <- Rn, Ri <- Ri + 1	-	2
ST -Ri, Rn	;Ri <- Ri - 1, (Ri) <- Rn	-	2
STD Ri + q, Rn;	(Ri + q) <- Rn	-	2
IN Rn,I	;Прочитать I/O-регистр I в Rn	Rn <- I	-	1
OUT I,Rn	;Записать Rn в I/O-регистр I	I <- Rk	-	1
PUSH Rn	;Сохранить Rn в стеке	STACK <- Rn	-	2
POP Rn	;Восстановить Rn из стека	Rn <- STACK	-	2
SWAP Rn	;Обмен тетрад в регистре	Rn(0...3) <- Rn(4...7), Rn(4...7) <- Rn(0...3)	-	1