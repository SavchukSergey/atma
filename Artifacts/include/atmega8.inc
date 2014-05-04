include "ports.inc"
include "eeprom.inc"

ZL equ r30
ZH equ r31

WDT_off:
	; reset WDT
	WDR
	; Write logical one to WDCE and WDE
	in r16, WDTCR
	ori r16, (1<<WDCE)|(1<<WDE)
	out WDTCR, r16
	; Turn off WDT
	ldi r16, (0<<WDE)
	out WDTCR, r16
	ret