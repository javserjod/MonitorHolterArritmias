Download project documentation (only available in Spanish): [monitor_holter_javserjod.pdf](https://github.com/user-attachments/files/20092336/monitor_holter_javserjod.pdf)

This is a software application developed in the C# programming language using the Microsoft Visual Studio 2022 integrated development environment, running on a computer with the Windows 10 operating system. It allows real-time detection of arrhythmias, including both bradycardia (if the heart rate falls below 60 beats per minute, bpm) and tachycardia (if it exceeds 100 bpm). The curvature of a single ECG channel is displayed on the screen as it is built, mainly intended for Leads I, II, and III, based on the corresponding physiological signals (voltages) collected via three electrodes attached to a BITalino electrocardiographic sensor. This sensor is connected to an analog input of the BITalino Core board, which transmits the data to the PC via Bluetooth.

Within the software application, certain parameters can be adjusted through the user interface without modifying the code (sampling frequency and R-wave peak detection threshold), and the user can start and stop the test at will. Additionally, once the measurement of the physiological signals is completed, the user can save an image of the recorded ECG. Throughout the process, a block of instructional text is displayed in the bottom-right corner of the interface.

• Instructions for use. The steps to follow are:

1. Open the application on a PC.

2. Connect the electrodes to the body surface, following Lead I, II, or III. According to lead terminology: The red wire is the exploring electrode. The black wire is the negative electrode. The white wire is the reference or ground. The electrodes are connected to the ECG sensor, which is then connected via another wire to an analog port (by default, A1) on the BITalino Core.

3. Turn on the BITalino Core board using its switch. A battery must be connected to the appropriate socket. If everything is set up correctly, the board will emit a steady white light.

4. Enable Bluetooth on the PC and pair it with the BITalino using PIN 1234.

5. Depending on the PC, you may need to modify the Bluetooth port name in the code to match the one the BITalino is connected to. By default, it monitors "COM6".

6. Start running the code (green arrow in Visual Studio).

7. Adjust the parameters as desired using the radio buttons.

8. Click Start on the left side of the user interface. The ECG will begin to be drawn on screen. If the bpm is within the acceptable range, the values will appear in green. If any arrhythmia or measurement artifacts (e.g., due to external noise) are detected, the values will be printed in red and the cause will be indicated.

9. Click Stop when you want to end the measurement. The obtained ECG will remain displayed. You can zoom in and out on the graph.

10. The user can click Save ECG to download a PNG image of the newly recorded ECG. The destination directory is the parent directory of the application.

11. At this point, the user can return to step 7 and repeat the ECG as many times as desired.

12. To finish using the software: Click the red X in the upper-right corner of the user interface. Stop the debugging process (red square in Visual Studio). Manually switch off the BITalino board. Remove the electrodes from the body surface.

------------------------------------------------------------------------------------------------------------------------------------------------------

Descargar documento del proyecto: [monitor_holter_javserjod.pdf](https://github.com/user-attachments/files/20092336/monitor_holter_javserjod.pdf)

Se trata de un SW desarrollado con el lenguaje de 
programación C# en el entorno de desarrollo integrado Microsoft Visual Studio 2022, en 
un ordenador con sistema operativo Windows 10. Permite detectar arritmias en tiempo 
real, tanto bradicardias (si el ritmo cardíaco baja de 60 pulsaciones por minuto, ppm) 
como taquicardias (si se supera las 100 ppm). Se muestra por pantalla la curvatura de 
un solo canal de ECG a medida que se construye, pensado principalmente para las 
Derivaciones I, II y III, a partir de las señales fisiológicas (voltajes) correspondientes 
recabadas a partir de 3 electrodos enganchados a un sensor electrocardiográfico 
BITalino, conectado a su vez a una entrada analógica de la placa BITalino Core, desde 
donde se envían al PC vía Bluetooth. Dentro de la aplicación SW, es posible variar 
algunos parámetros desde la interfaz de usuario, sin tener que modificar el código 
(frecuencia de muestreo y umbral para la detección de picos de ondas R), así como 
comenzar y finalizar la prueba a voluntad propia. Además, cuando el usuario finalice la 
medición de sus señales fisiológicas, puede guardar una imagen del ECG tomado. 
Durante todo el proceso, se imprime un bloque de texto en la esquina inferior derecha 
con instrucciones. 

• Instrucciones de uso. Los pasos a seguir son los siguientes: 
1. Se abre la aplicación en un PC. 

2. Conectar los electrodos a la superficie corporal, siguiendo la Derivación I, II o III. 
Según la terminología de las derivaciones: el cable rojo es el explorador, el cable 
negro el electrodo negativo y el cable blanco es la referencia o toma de tierra. 
Los electrodos se conectan al sensor electrocardiográfico, y este se conecta con 
otro cable a un puerto analógico (por defecto, el A1) del núcleo BITalino. 

3. Se enciende la placa BITalino Core con su interruptor. Requiere de una batería 
conectada al enchufe pertinente. Si se ha hecho correctamente, la placa 
producirá una luz blanca continua. 

4. Habilitar Bluetooth en el PC y emparejarse con el BITalino, con el PIN 1234. 

5. Según el PC, habrá que modificar desde el código el nombre del puerto 
Bluetooth al que se ha conectado el BITalino. Por defecto se vigila el “COM6”. 

6. Iniciar la ejecución del código (flecha verde en Visual Studio). 

7. Modificar los parámetros a su antojo mediante los botones de radio. 

8. Hacer clic en Comenzar, a la izquierda en la interfaz de usuario. Empezará a 
dibujarse el ECG en pantalla. En caso de ppm dentro del rango considerado 
correcto, se mostrarán en verde. Si se detecta cualquier arritmia o artefactos en 
la medición (debido a ruido externo) se imprimirán los valores en rojo y se 
informará de la causa. 

9. Hacer clic en Finalizar cuando se desee concluir la medición. Se seguirá 
mostrando el ECG obtenido. Puede hacerse zoom in y zoom out en la gráfica. 
10. El usuario puede clicar en Guardar ECG para descargar una imagen PNG del ECG 
recién construido. El directorio destino es el directorio padre de la aplicación. 

11. En este momento, el usuario puede volver al paso 7 para repetir el ECG, cuantas 
veces desee. 

12. Para dar por finalizado el uso del SW, se debe hacer clic en la esquina superior 
derecha de la interfaz de usuario (equis roja), detener la depuración (cuadrado 
rojo en Visual Studio). A continuación, debe apagarse manualmente la placa 
BITalino con el interruptor, y quitar los electrodos de la superficie corporal. 
