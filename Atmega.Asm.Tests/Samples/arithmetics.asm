;Арифметические операции
;Мнемоника	Описание	Операция	Изменяемые флаги	Время исполнения (циклов)
section code
	ADC	r12,r22;	Сложение c переносом	Rn <- Rn + Rk + C	Z, C, N, V, S, H	1
	ADD	r12,r22;	Сложение без переноса	Rn <- Rn + Rk	Z, C, N, V, S, H	1
	ADIW r24,10;	Сложение с константой	(Rn+1):Rn <- (Rn+1):Rn + K	Z, C, N, V, S	2
	SUB	r12,r22;	Вычитание без заема	Rn <- Rn - Rk	Z, C, N, V, S, H	1
	subi R22,10;	Вычитание константы без заема	Rn <- Rn - K	Z, C, N, V, S, H	1
	sbci r22,10;	Вычитание константы с заемом	Rn <- Rn - K - C	Z, C, N, V, S, H	1
	SBC R12,r22;	Вычитание c заемом	Rn <- Rn - Rk - C	Z, C, N, V, S, H	1
	SBIW R24,10;	Вычитание константы	Rn+1):Rn <- (Rn+1):Rn - K	Z, C, N, V, S	2
	COM R12;	Дополнение до 1	Rn <- 0FFh - Rn	Z, C, N, V, S	1
	NEG R12;	Дополнение до 2	Rn <- 0 - Rn	Z, C, N, V, S, H	1
	INC R12;	Увеличение на 1	Rn <- Rn + 1	Z, N, V, S	1
	DEC R12;	Уменьшение на 1	Rn <- Rn - 1	Z, N, V, S	1
	cp r22,r23;	Сравнение регистров	Rn - Rk	Z, C, N, V, S, H	1
	cpi r22,10;	Сравнение регистра с константой	Rn - K	Z, C, N, V, S, H	1
	CPC R12,r22;	Сравнение регистров с заемом	Rn - Rk - C	Z, C, N, V, S, H	1

	mul r3, r22
	muls r23, r31
	fmul r23, r23