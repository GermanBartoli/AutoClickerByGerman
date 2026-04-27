# Conexion AQW con la app

## Objetivo
Este flujo permite enviar teclas a la ventana de AQW sin depender de que la ventana este en foco.

## Flujo general
1. En la vista AQW, el usuario pulsa Capturar ventana (3s).
2. La app espera 3 segundos para que el usuario cambie a la ventana de AQW.
3. Se guarda el handle de la ventana activa como ventana objetivo.
4. Al pulsar Iniciar, se levanta un hilo en segundo plano.
5. El hilo agenda teclas segun el modo activo y las envia respetando su temporizacion.

## Como se envia la entrada
La app usa mensajes Win32 para enviar teclado a la ventana objetivo:

- Se valida que el handle siga siendo valido.
- Si la ventana esta minimizada, se intenta restaurar antes del envio.
- Se intenta obtener el control con foco del hilo de la ventana objetivo.
- Si existe un control con foco valido, se usa como destino del envio.
- Si no existe, se usa la ventana principal como fallback.
- Al destino elegido se le envian:
  - WM_ACTIVATE
  - WM_SETFOCUS
  - WM_KEYDOWN
  - WM_KEYUP

Con este enfoque, la entrada no depende del foco global del teclado y mejora mucho el comportamiento en segundo plano.

## Armado de la tecla
Para WM_KEYDOWN y WM_KEYUP se arma lParam con:

- Repeticion = 1
- Scan code obtenido con MapVirtualKey
- Flags de transicion para KEYUP

Esto hace que el mensaje se parezca a un evento real de teclado.

## Ciclo de automatizacion
- Estado ejecutando: while en segundo plano que agenda teclas por modo.
- Separacion minima global entre teclas de cola: 1500 ms para evitar choques.
- En VHL, Revenant y Yami la tecla 1 tiene un temporizador independiente de 2000 ms y no bloquea la cola principal.
- Detencion:
  - Boton Detener.
  - Cierre del formulario.
  - Ventana objetivo invalida.

## Modos disponibles

### Autoatack
- Se activa con el checkbox Autoatack.
- Mantiene la logica actual de secuencia 1, 2, 3, 4, 5.
- Respeta el intervalo configurado en codigo entre teclas y entre ciclos.

### VHL
- Se activa con el checkbox VHL.
- Es exclusivo con Revenant y Yami.
- La cola principal usa la secuencia 3, 2, 4, 5.
- La tecla 1 se dispara aparte cada 2000 ms, sin frenar la cola principal.

### Revenant
- Se activa con el checkbox Revenant.
- Es exclusivo con VHL y Yami.
- La cola principal usa la secuencia 2, 3, 4, 5.
- La tecla 1 se dispara aparte cada 2000 ms, sin frenar la cola principal.

### Yami
- Se activa con el checkbox Yami.
- Es exclusivo con VHL y Revenant.
- La cola principal usa la secuencia 2, 3, 4, 5.
- La tecla 1 se dispara aparte cada 2000 ms, sin frenar la cola principal.

### Autoatack combinado con un modo
- Autoatack puede convivir con VHL, Revenant o Yami.
- El scheduler evita enviar dos teclas pegadas en la cola principal y mantiene la separacion minima de 1500 ms.
- La tecla 1 del modo activo puede dispararse en su propio tiempo aunque la cola principal siga procesando otras teclas.

## Ventajas de este enfoque
- No obliga a mantener AQW en foco.
- Tolera mejor ventanas con controles internos.
- Evita usar input global que afecta otras apps.

## Limites conocidos
- Algunos motores de juego pueden ignorar mensajes sinteticos en ciertos estados.
- Si la ventana cambia de proceso/control interno, conviene recapturar la ventana objetivo.

## Referencia tecnica
Implementado en la clase AqwForm, en los metodos de captura, automatizacion y envio de teclas por mensajes Win32.
