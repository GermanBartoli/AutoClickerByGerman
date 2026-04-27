# AQW corto: secuencia y tiempos

## Tiempo entre teclas
- Intervalo entre cada tecla: 1500 ms
- No hay intervalo adicional entre cada ciclo.

## Nota
En la implementacion actual de AQW del proyecto, la secuencia exacta usada en codigo es:
1, 2, 3, 4, 5

Al presionar Comenzar, la automatizacion continua sin pausa extra al cerrar ciclo: al terminar la tecla 5, vuelve a la tecla 1 respetando solo el intervalo entre teclas.

En Autoatack se usa exactamente esa secuencia completa.

En VHL, Revenant y Yami la cola principal usa solo estas teclas:
- VHL: 3, 2, 4, 5
- Revenant: 2, 3, 4, 5
- Yami: 2, 3, 4, 5

## Regla especial de la tecla 1
- En VHL, Revenant y Yami, la tecla 1 es prioritaria.
- Cooldown de la tecla 1: 2000 ms.
- Se dispara en su tiempo de cooldown programado aunque exista cola de otras teclas pendientes.
- La tecla 1 no forma parte de la cola principal de 2, 3, 4 y 5.
- La tecla 1 no espera el intervalo global de cola para poder ejecutarse cuando vence su tiempo.
- La cola principal de 2, 3, 4 y 5 sigue respetando su separacion de 1500 ms sin frenarse por el cooldown de la tecla 1.
