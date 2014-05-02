﻿;Операции перехода
;Мнемоника	Описание	Операция	Изменяемые флаги	Время исполнения (циклов)
;JMP K	;Абсолютный переход	PC <- K	-	3
;CALL K	;Абсолютный вызов	PC <- K	-	4 (5)
;RJMP K	;Относительный переход	PC <- PC + K + 1	-	2
;RCALL K	;Относительный вызов	PC <- PC + K + 1	-	2
IJMP	;Косвенный переход	PC(15:0) <- Z, PC(21:16) <- 0	-	2
ICALL	;Косвенный вызов	PC(15:0) <- Z, PC(21:16) <- 0	-	2
EIJMP	;Косвенный переход	PC(15:0) <- Z, PC(21:16) <- EIND	-	2
EICALL	;Косвенный вызов	PC(15:0) <- Z, PC(21:16) <- EIND	-	2
RET	;Выход из процедуры	PC <- STACK	-	4 (5)
RETI	;Выход из процедуры прерывания	PC <- STACK	I	4 (5)
;CPSE Rn,Rk	Сравнить и пропустить, если равно	if (Rn = Rk) PC <- PC + 2 or 3	-	1 / 2 / 3
SBRC r11,1	;Пропустить, если Rn(K) = 0	if (Rn(K) = 0) PC <- PC + 2 or 3	-	1 / 2 / 3
SBRS r22,2	;Пропустить, если Rn(K) = 1	if (Rn(K) = 1) PC <- PC + 2 or 3	-	1 / 2 / 3
;SBIC IR,K	;Пропустить, если IR(K) = 0	if (IR(K) = 0) PC <- PC + 2 or 3	-	1 / 2 / 3
;SBIS IR,K	;Пропустить, если IR(K) = 1	if (IR(K) = 1) PC <- PC + 2 or 3	-	1 / 2 / 3
;BRBS b,K	;Перейти, если SREG(b) = 1	if (SREG(b) = 1) PC <- PC + K + 1	-	1 / 2
;BRBC b,K	;Перейти, если SREG(b) = 0	if (SREG(b) = 0) PC <- PC + K + 1	-	1 / 2
BREQ $	;Перейти, если Z = 1	if (Z = 1) PC <- PC + K + 1	-	1 / 2
BRNE $	;Перейти, если Z = 0	if (Z = 0) PC <- PC + K + 1	-	1 / 2
BRCC $	;Перейти, если C = 0	if (C = 0) PC <- PC + K + 1	-	1 / 2
BRCS $	;Перейти, если C = 1	if (C = 1) PC <- PC + K + 1	-	1 / 2
BRHC $	;Перейти, если H = 0	if (H = 0) PC <- PC + K + 1	-	1 / 2
BRHS $	;Перейти, если H = 1	if (H = 1) PC <- PC + K + 1	-	1 / 2
BRSH $	;Перейти, если больше или равно	if (C = 0) PC <- PC + K + 1	-	1 / 2
BRLO $	;Перейти, если меньше	if (C = 1) PC <- PC + K + 1	-	1 / 2
BRPL $	;Перейти, если больше или равно 0	if (N = 0) PC <- PC + K + 1	-	1 / 2
BRMI $	;Перейти, если меньше 0	if (N = 1) PC <- PC + K + 1	-	1 / 2
BRGE $	;Перейти, если больше или равно (со знаком)	if (N + V = 0) PC <- PC + K + 1	-	1 / 2
BRLT $	;Перейти, если меньше (со знаком)	if (N + V = 1) PC <- PC + K + 1	-	1 / 2
BRTC $	;Перейти, если T = 0	if (T = 0) PC <- PC + K + 1	-	1 / 2
BRTS $	;Перейти, если T = 1	if (T = 1) PC <- PC + K + 1	-	1 / 2
BRVC $	;Перейти, если V = 0	if (V = 0) PC <- PC + K + 1	-	1 / 2
BRVS $	;Перейти, если V = 1	if (V = 1) PC <- PC + K + 1	-	1 / 2
BRIE $	;Перейти, если I = 0	if (I = 0) PC <- PC + K + 1	-	1 / 2
BRID $	;Перейти, если I = 1	if (I = 1) PC <- PC + K + 1	-	1 / 2
