# AQW corto: secuencia y tiempos

## Tiempo entre teclas
- Intervalo entre cada tecla: 1500 ms
- Intervalo entre cada ciclo: 1500 ms

## Nota
En la implementacion actual de AQW del proyecto, la secuencia exacta usada en codigo es:
1, 2, 3, 4, 5

## Regla especial de la tecla 1
- En VHL, Revenant y Yami, la tecla 1 es prioritaria.
- Se dispara en su tiempo de cooldown programado aunque exista cola de otras teclas pendientes.
- La tecla 1 no espera el intervalo global de cola para poder ejecutarse cuando vence su tiempo.
