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
