#Atma

Atma is a free and open source Avr assembler.

Although it is functional it is for education purposes only.

Syntax is fasm-inspired

## Compiler overview
TODO
### System requirements
TODO
### Executing compiler from command line
To execute atma assembler from the command line you need to provide one single parameter - source file name. Output file will be guessed automatically. When the compilation process is successful, compiler will write the generated code to the destination file and display the summary of compilation process; otherwise it will display the information about error that occurred.

Usage:
```
atma source.asm
```

### Compiler messages
TODO
### Output formats
TODO
## Assembly syntax
TODO
### Instruction syntax
Instructions in assembly language are separated by line breaks, and one instruction is expected to fill the one line of text. If a line contains a semicolon, except for the semicolons inside the quoted strings, the rest of this line is the comment and compiler ignores it. If a line ends with \ character (eventually the semicolon and comment may follow it), the next line is attached at this point.
#### Registers
There are 32 registers defined with names *r0* - *r31*. Register names are case insensitive.
Note that some instructions allow only *r16* - *r31* registers.
Syntax:
```
adc r12, r23 ; r12 = r12 + r23 + carry
```

TODO
### Data definitions
TODO
### Constants and labels
TODO
### Numerical expressions
TODO
### Jumps and calls
TODO
### Size settings
TODO
# Instruction Set
TODO
## The Atmega8 architecture instructions
TODO
### Data movement instructions
*push*, *pop* instructions can accept several arguments in a single line to reduce space
```
func:
    push r16 r17 r18 r19
    pop r19 r18 r17 r16
    ret
```
TODO
TODO other types of instructions
## Control directives
TODO
## Conditional assembly
TODO
## Addressing spaces
*org* directive sets address at which the following code is expected to appear in memory. It should be followed by numerical expression specifying the address. This directive begins the new addressing space, the following code itself is not moved in any way, but all the labels defined within it and the value of $ symbol are affected as if it was put at the given address. However it's the responsibility of programmer to put the code at correct address at run-time.
```
org 0x100
```
Note that addressing a value at code section is byte-measured. You don't have to multiply address by 2 to access value there.
Example:
```
ldi ZH, high(value_to_read)
ldi ZL, low(value_to_read)
lpm r16, Z; now r16 contains value 1
value_to_read db 1
```
## Preprocessor directives
TODO
### Including source file
*include* directive includes the specified source file at the position where it is used. It should be followed by the quoted name of file that should be included, for example:
```
    include 'macros.inc'
```
The whole included file is preprocessed before preprocessing the lines next to the line containing the *include* directive.
File path can be relative to referrer.
### Symbolic constants
The symbolic constants are different from the numerical constants, before the assembly process they are replaced with their values everywhere in source lines after their definitions, and anything can become their values.
The definition of symbolic constant consists of name of the constant followed by the *equ* directive. Everything that follows this directive will become the value of constant. If the value of symbolic constant contains other symbolic constants, they are replaced with their values before assigning this value to the new constant. For example:
```
MY_CONSTANT equ 123
MY_CONSTANT2 equ MY_CONSTANT - 12
ldi  r16, MY_CONSTANT2 ;r16 = 123 - 12
```
### Macroinstructions
TODO
### Structures
TODO
### Conditional preprocessing
TODO
## Formatter directives
TODO

## File structure
File may consist of three types of sections: code, data, flash. These section are marked with *section* directive. All sections of the same type are merged during compilation.
```
section code
main:
mov r12, r13
jmp main

section data
variable1 rb 1
variable2 rb 1

section flash
permanent1 db 1
permanent2 rb 20
```

## Assembly Syntax
### Strings
Strings can be enclosed in single quotes or double quotes.
```
db 'string' ;means bytes 's', 't', 'r', 'i', 'n', 'g'
db "other" ;means bytes 'o', 't', 'h', 'e', 'r'
```
Following escape sequences are allowed:

| Sequence | Value           | Code |
|----------|-----------------|------|
| \n       | Newline         | 0x0a |
| \r       | Carriage return | 0x0d |
| \a       | Alert bel       | 0x07 |
| \b       | Backspace       | 0x08 |
| \f       | Form feed       | 0x0c |
| \t       | Horizontal tab  | 0x09 |
| \v       | Horizontal tab  | 0x0b |
| \\       | Backslash       | 0x5c |
| \0       | Null character  | 0x00 |
| \'       | Single quote    | 0x27 |
| \"       | Double quote    | 0x22 |

Examples:
```
db 'Hello\tWorld\0'
db 'Roger\'s dog'
```
Note that you can use quote character itself twice as escape sequence.
Example:
```
db 'someone''s string' ; means someone's string
db "someone""s string" ; means someone"s string
```


## Integers
Syntax:
```
db 123; means 123 decimal integer
db 0x23; means 23 hexadecimal integer (35 decimal integer)
db 45h; means 45 hexadecimal integer (69 decimal integer)
```

## Data Definition
You can define custom data at your code and flash section. Data section is microcontroller's RAM and thus cannot be initialized with specific data. But you can reserve space and declare variable at data section.
Syntax:
```
section code
db 123, 'characters'
db 45h
rb 10 ;reserve 10 bytes (filled with zero)
section data
mem rb 30 
counter rb 1
```

## Labels
Note that all offset and labels are calculated on byte basis regardless of instruction 16 bit size.
Examples:
```
loop: rjmp loop ;define label at current offset
dataArray rb 110 ;define 110 byte array with name dataArray
```
You can also define local labels using dot syntax:
```
main:
jmp .key1 
jmp .key2
.key1: jmp main ;defines local to "main" label. Full name of label becomes main.key1
.key2: jmp main
main2:
.key1 jmp .key2 ;this is absolutely different label (main2.key1)
.key2 jmp .key1
```
There is a special symbol $ which means address of current instruction.
```
jmp $ ;infinite loop
```

## Numerical expressions
You can build mathematical expressions for defining compilation-time constants. There are follwing operators defined:

| operator | description    | priority |
|----------|----------------|----------|
| +        | addition       | 0        |
| -        | subtraction    | 0        |
| *        | multiplication | 1        |
| /        | division       | 1        |
| %        | mod            | 2        |
| &        | bitwise and    | 3        |
| \|       | bitwise or     | 3        |
| <<       | shift left     | 4        |
| >>       | shift right    | 4        |

Example:
```
ldi r16, 1 << (2 + 3 * (4 + 2) % 3)
```
There are following build-it functions defined:

| name | description          | example             |
|------|----------------------|---------------------|
| LOW  | Returns lowest byte  | LOW(0x1234) = 0x34  |
| HIGH | Returns highest byte | HIGH(0x1234) = 0x12 |

There is special symbol $ which means offset in current section.
Example
```
 rjmp $ ;Infinite loop
```


# Boilerplate
```
include 'inc/atmega8.inc'

section code
	rjmp RESET ; Reset Handler
	rjmp EXT_INT0 ; IRQ0 Handler
	rjmp EXT_INT1 ; IRQ1 Handler
	rjmp TIM2_COMP ; Timer2 Compare Handler
	rjmp TIM2_OVF ; Timer2 Overflow Handler
	rjmp TIM1_CAPT ; Timer1 Capture Handler
	rjmp TIM1_COMPA ; Timer1 CompareA Handler
	rjmp TIM1_COMPB ; Timer1 CompareB Handler
	rjmp TIM1_OVF ; Timer1 Overflow Handler
	rjmp TIM0_OVF ; Timer0 Overflow Handler
	rjmp SPI_STC ; SPI Transfer Complete Handler
	rjmp USART_RXC ; USART RX Complete Handler
	rjmp USART_UDRE ; UDR Empty Handler
	rjmp USART_TXC ; USART TX Complete Handler
	rjmp ADC ; ADC Conversion Complete Handler
	rjmp EE_RDY ; EEPROM Ready Handler
	rjmp ANA_COMP ; Analog Comparator Handler
	rjmp TWSI ; Two-wire Serial Interface Handler
	rjmp SPM_RDY ; Store Program Memory Ready Handler
RESET:
	ldi	r16, high(RAMEND); Main program start
	out	SPH, r16 ; Set Stack Pointer to top of RAM
	ldi	r16, low(RAMEND)
	out	SPL, r16
	sei
main_loop:
	rjmp	main_loop

interrupt_stub:
	reti

EXT_INT0:
EXT_INT1:
TIM2_COMP:
TIM2_OVF:
TIM1_CAPT:
TIM1_COMPA:
TIM1_COMPB :
	reti

interrupt_intx:
	push	r16
	in	r16, SREG
	push	r16
;body
	pop	r16
	out	SREG, r16
	pop	r16
	reti


section data
org	0x60
mydata rb 1
```
