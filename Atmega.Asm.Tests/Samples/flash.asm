include 'include/atmega8.inc'

section code

main:
	ldi	r17, 1
	ldi	r18, 0
	mov	r16, 2
	call EEPROM_write
	jmp	main