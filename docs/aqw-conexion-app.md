# Conexion AQW con la app

## Objetivo
Este flujo permite enviar teclas a la ventana de AQW sin depender de que la ventana este en foco.

## Flujo general
1. En la vista AQW, el usuario pulsa Capturar ventana (3s).
2. La app espera 3 segundos para que el usuario cambie a la ventana de AQW.
3. Se guarda el handle de la ventana activa como ventana objetivo.
4. Al pulsar Iniciar, se levanta un hilo en segundo plano.
5. El hilo recorre una secuencia de teclas y las envia con intervalo fijo.

## Como se envia la entrada
La app usa mensajes Win32 para enviar teclado a la ventana objetivo:

- Se valida que el handle siga siendo valido.
- Si la ventana esta minimizada, se intenta restaurar antes del envio.
- Se calcula una lista de destinos:
  - Ventana principal.
  - Ventanas hijas.
  - Control que tiene foco en el hilo de la ventana objetivo.
- A cada destino se le envian:
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
- Separacion minima global entre teclas: 1000 ms para evitar choques.
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
- Ejecuta la clase con teclas y cooldown independiente:
  - Tecla 2: 3 segundos.
  - Tecla 3: 4 segundos.
  - Tecla 4: 3 segundos.
  - Tecla 5: 10 segundos.
- Cada tecla se vuelve a enviar apenas termina su cooldown, siempre que se cumpla la separacion minima global de 1 segundo.

### Autoatack + VHL al mismo tiempo
- Se pueden activar ambos checks.
- El scheduler evita enviar dos teclas pegadas y mantiene la separacion minima.
- Si una tecla ya cumplio cooldown, queda lista y se envia en el siguiente hueco disponible.

## Ventajas de este enfoque
- No obliga a mantener AQW en foco.
- Tolera mejor ventanas con controles internos.
- Evita usar input global que afecta otras apps.

## Limites conocidos
- Algunos motores de juego pueden ignorar mensajes sinteticos en ciertos estados.
- Si la ventana cambia de proceso/control interno, conviene recapturar la ventana objetivo.

## Referencia tecnica
Implementado en la clase AqwForm, en los metodos de captura, automatizacion y envio de teclas por mensajes Win32.
