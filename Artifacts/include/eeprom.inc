EEPROM_write:
	; Wait for completion of previous write
	sbic EECR,EEWE
	rjmp EEPROM_write 
	; Set up address (r18:r17) in address register
	out EEARH, r18
	out EEARL, r17
	; Write data (r16) to data register
	out EEDR,r16
	; Write logical one to EEMWE
	sbi EECR,EEMWE
	; Start eeprom write by setting EEWE
	sbi EECR,EEWE
	ret

EEPROM_read:
	; Wait for completion of previous write
	sbic EECR,EEWE
	rjmp EEPROM_read
	; Set up address (r18:r17) in address register
	out EEARH, r18
	out EEARL, r17
	; Start eeprom read by writing EERE
	sbi EECR,EERE
	; Read data from data register
	in r16,EEDR
	ret
