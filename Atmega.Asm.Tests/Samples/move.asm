﻿;Операции перемещения данных
;Мнемоника	Описание	Операция	Изменяемые флаги	Время исполнения (циклов)
MOV r12,r22	;Копировать Rk в Rn	Rn <- Rk	-	1
MOVW r13:r12, r25:r24	;Копировать регистровую пару	Rn+1:Rn <- Rk+1:Rk	-	1

LD r0, X	;Загрузить из памяти данных, используя индексный регистр	Rn <- (Ri)	-	2
LD r0, Y	;Загрузить из памяти данных, используя индексный регистр	Rn <- (Ri)	-	2
LD r0, Z	;Загрузить из памяти данных, используя индексный регистр	Rn <- (Ri)	-	2
LD R1, X+	;Rn <- (Ri), Ri <- Ri + 1	-	2
LD R1, Y+	;Rn <- (Ri), Ri <- Ri + 1	-	2
LD R1, Z+	;Rn <- (Ri), Ri <- Ri + 1	-	2
LD R2, -X	;Ri <- Ri - 1, Rn <- (Ri)	-	2
LD R2, -Y	;Ri <- Ri - 1, Rn <- (Ri)	-	2
LD R2, -Z	;Ri <- Ri - 1, Rn <- (Ri)	-	2

ST X, R3	;Загрузить в память данных, используя индексный регистр	(Ri) <- Rn	-	2
ST Y, R3	;Загрузить в память данных, используя индексный регистр	(Ri) <- Rn	-	2
ST Z, R3	;Загрузить в память данных, используя индексный регистр	(Ri) <- Rn	-	2
ST X+, R4	;(Ri) <- Rn, Ri <- Ri + 1	-	2
ST Y+, R4	;(Ri) <- Rn, Ri <- Ri + 1	-	2
ST Z+, R4	;(Ri) <- Rn, Ri <- Ri + 1	-	2
ST -X, R5	;Ri <- Ri - 1, (Ri) <- Rn	-	2
ST -Y, R5	;Ri <- Ri - 1, (Ri) <- Rn	-	2
ST -Z, R5	;Ri <- Ri - 1, (Ri) <- Rn	-	2

;LDD Rn, Ri + q	;Rn <- (Ri + q)	-	2
LDI R18,253	;Загрузить константу в регистр	Rn <- K	-	1

LDS r12, $ ;Загрузить в регистр из памяти данных	Rn <- (K)	-	2
STS $,r12	;Загрузить в память данных	(K) <- Rn	-	2

LPM	;Загрузить из памяти программ	R0 <- (Z)	-	3
LPM R1, Z	;Rn <- (Z)	-	3
LPM R2, Z+	;Rn <- (Z), Z <- Z + 1	-	3
ELPM	;R0 <- (RAMPZ:Z)	-	3
ELPM R22, Z	;Rn <- (RAMPZ:Z)	-	3
ELPM R23, Z+	;Rn <- (RAMPZ:Z), Z <- Z + 1	-	3
SPM	;Загрузить в память программ	(Z) <- R1:R0	-	3
;STD Ri + q, Rn;	(Ri + q) <- Rn	-	2
IN r12,60	;Прочитать I/O-регистр I в Rn	Rn <- I	-	1
OUT 0x3e,r11	;Записать Rn в I/O-регистр I	I <- Rk	-	1
push	r12	;Сохранить Rn в стеке	STACK <- Rn	-	2
pop	r13	;Восстановить Rn из стека	Rn <- STACK	-	2
SWAP R22	;Обмен тетрад в регистре	Rn(0...3) <- Rn(4...7), Rn(4...7) <- Rn(0...3)	-	1