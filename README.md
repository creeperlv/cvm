# CVM

Common Virtual Machine

Another Virtual Machine with different instruction design.

## Instruction

|0..7|8..63|
|--|--|
|OP Code| Data|

## Module

|Data Length(Bytes)|Description  |
|-|-|
|4|Data Count|
|4|Data Length of Data 0|
|X|Data 0|
|4|Data Length of Data 1|
|X|Data 1|
|...|...|
|4|Data Length of Data N|
|X|Data N|
|4|Instruction Count|
|8|Instruction 0|
|8|Instruction 1|
|...|...|
|8|Instruction N|
