﻿;Логические операции
;Мнемоника	Описание	Операция	Изменяемые флаги	Время исполнения (циклов)
AND r12,r31	;Логическое "И"	Rn <- Rn & Rk	Z, N, V, S	1
ANDI r22,12	;Логическое "И" с константой	Rn <- Rn & K	Z, N, V, S	1
OR r14,r22	;Логическое "ИЛИ"	Rn <- Rn | Rk	Z, N, V, S	1
ori r16,0	;Логическое "ИЛИ" с константой	Rn <- Rn | K	Z, N, V, S	1
eor r0,r1	;"Исключающее ИЛИ"	Rn <- Rn ^ Rk	Z, N, V, S	1
CBR r22,112	;Очистка битов	Rn <- Rn & (0FFh-K)	Z, N, V, S	1
SBR R22,115	;Установка битов	Rn <- Rn | K	Z, N, V, S	1
CLR R12	;Очистка регистра	Rn <- Rn ^ Rn	Z, N, V, S	1
SER R22	;Установка регистра	Rn <- 0FFh	-	1
TST R12	;Проверка на 0 и минус	Rn <- Rn & Rn	Z, N, V, S	1