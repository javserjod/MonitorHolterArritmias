using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;



// Implementación de Monitor Holter con actuación ante arritmias
// Realizado por Javier Serrano Jodral

// El programa detecta si las pulsaciones por minuto del usuario, recibidas mediante Bitalino, están en el rango normal, 
// y en caso contraro, indica si se trata de Bradicardia o Taquicardia, ambas consideradas tipos de Arritmia.


namespace ImplementacionTrabajoGlobal
{
    public partial class Form1 : Form
    {
        Bitalino dev;
        Bitalino.Frame[] frames;    // Vector de datos recibidos del Bitalino
        public int umbralOndaR;     // Valor umbral en µV para detectar picos ondas R y poder medir intervalo RR = Ritmo cardíaco
        List<int> cumpleUmbral = new List<int>();    // Lista para detección de picos, o sea, onda R -> Valores 1 y 0
        int banderaContador = 0;      // Bandera para obviar en los cálculos las primeras muestras (hasta la primera onda R), ya que no son precisas
        int contador = 0;             // Conteo de muestras entre onda R y la siguiente onda R
        List<int> bufferBpm = new List<int>();    // Buffer con todos los tiempos de cada intervalo RR


        public Form1()
        {
            InitializeComponent();
            dev = new Bitalino("COM6");   // Inicializar variable dispositivo y asignar puerto Bluetooth
            int nDatos = 2;      // Cantidad de datos que se reciben cada vez que se lee el dispositivo
                                  // Tras varias pruebas, recibir 1 dato con una de las frecuencias ofrecidas es suficiente y va muy fluido
            frames = new Bitalino.Frame[nDatos];
            for (int i=0; i< frames.Length; i++)
            {
                frames[i] = new Bitalino.Frame();
            }

            textBox1.Text = "MODO DE USO: " +
                "Seleccione una frecuencia de muestreo, que representa la cantidad de veces por segundo que se lee el sensor. " +
                "Indique el umbral para distinguir los picos de las ondas R y poder calcular intervalo RR. " +
                "Haga click en Comenzar. Tenga en cuenta que los cambios en los botones solo afectarán a la siguiente ejecución.";

            // Código para poder hacer zoom en ECG
            ChartArea CA = chart1.ChartAreas[0];
            CA.AxisX.ScaleView.Zoomable = true;
            CA.CursorX.AutoScroll = true;
            CA.CursorX.IsUserSelectionEnabled = true;

        }

        private void button1_Click(object sender, EventArgs e)    // Botón Comenzar/Finalizar
        {
            if (timer1.Enabled == false)   // Está apagado y se ha hecho click en Comenzar
            {
                button1.Text = "Finalizar";
                textBox1.Text = "Mediciones en curso. Recuerde que puede hacer zoom en el ECG obtenido arrastrando y soltando el ratón. " +
                    "Haga click en Finalizar para concluir el ECG.";

                // Asignar duración del timer en milisegundos, a partir de botón marcado -> Frecuencia = 1000/timer.Interval   (Hz)
                if (radioButton1.Checked)
                {
                    timer1.Interval = 5;            // 200 Hz (por defecto)
                }
                else if (radioButton2.Checked)
                {
                    timer1.Interval = 2;            // 500 Hz 
                }
                else if (radioButton3.Checked)
                {
                    timer1.Interval = 1;            // 1000 Hz 
                }

                // Asignar valor umbral, en µV
                if (radioButton4.Checked)   
                {
                    umbralOndaR = 700;              // (por defecto)
                }
                else if (radioButton5.Checked)
                {
                    umbralOndaR = 1200;            
                }
                else if (radioButton6.Checked)   
                {
                    umbralOndaR = 2000;              
                }

                // Línea que representa al umbral, para mejor comprensión visual
                chart1.ChartAreas["ChartArea1"].AxisY.StripLines.Clear();
                StripLine stripline = new StripLine();
                stripline.Interval = 0;
                stripline.IntervalOffset = umbralOndaR;
                stripline.StripWidth = 2;
                stripline.BackColor = Color.Blue;
                chart1.ChartAreas["ChartArea1"].AxisY.StripLines.Add(stripline);


                textBox1.Text = "Mediciones en curso. Haga click en Finalizar para concluir el ECG. " +
                    "Frecuencia de muestreo actual: " + 1000/timer1.Interval + " Hz. Umbral para detección de picos: "+umbralOndaR+ " µV.";

                chart1.Series[0].Points.Clear();   // Reseteo del chart

                dev.start(100, new int[] { 0 });   // Iniciar dispositivo. Frecuencia de lectura de sensores, Vector con entradas a leer
                timer1.Enabled = true;             // Habilitar timer una vez iniado el dispositivo

                button2.Visible = false;           // Ocultar botón de descarga del ECG
            }
            else    // timer1.Enabled == true  ---> Está midiendo y se ha clicado en Finalizar
            {
                button1.Text = "Comenzar";
                textBox1.Text = "Fin de las mediciones. Puede descargar una captura de la curva clicando en el botón de la izquierda Guardar ECG. " +
                    "Haga click en Comenzar si desea iniciar un nuevo ECG.";

                dev.stop();                        // Desconectar dispositivo
                timer1.Enabled = false;            // Apagar timer

                cumpleUmbral.Clear();    // Vaciar lista de detección de picos
                banderaContador = 0;     // Poner a 0 bandera
                contador = 0;            // Reiniciar contador
                bufferBpm.Clear();       // Vaciar buffer de latidos por minuto

                button2.Visible = true;    // Mostrar botón de descarga del ECG
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Se lee del dispositivo cada vez que concluye un intervalo del timer
            dev.read(frames);

            //Console.WriteLine(frames[0].analog[0]);

            int valorNuevo;    // Variable para almacenar datos recopilados y ajustarlos


            for (int i=0; i<frames.Length; i++)
            {
                valorNuevo = (frames[i].analog[0] - 500) * 10;     // Ajustar datos para que se centren en 0, y voltaje en microvoltios (µV)

                cumpleUmbral.Add(valorNuevo>=umbralOndaR?1:0);         // Añadir 1 o 0, si el valor actual supera o no el umbral

                if (cumpleUmbral.Count >= 2)       // Si ya se han evaluado dos valores (el primero y el actual)...
                {
                    if ((cumpleUmbral[cumpleUmbral.Count - 2] == 0) & (cumpleUmbral[cumpleUmbral.Count - 1] == 1))    // Si anterior fue  0 y actual 1 ("subida escalón")  _-
                    {                
                        if (banderaContador == 1)   // Si ya vale 1, se ha cumplido un ciclo desde la onda R anterior
                        {
                            // Console.WriteLine((100 * 60) / contador);    // Mostrar conversión de muestras contadas a latidos por minuto
                            bufferBpm.Add((100*60)/contador);      // Añadir al buffer latidos por minuto, tras conversión
                            if (bufferBpm[bufferBpm.Count - 1] < 60)         // Bradicardia detectada
                            {
                                label1.Text = "Pulsaciones por minuto en reposo: " + bufferBpm[bufferBpm.Count - 1] + " < 60 -> Bradicardia";
                                label1.BackColor = Color.IndianRed;
                            }
                            else if (bufferBpm[bufferBpm.Count - 1] > 100)   // Taquicardia detectada
                            {
                                if (bufferBpm[bufferBpm.Count - 1] <= 200)   // Valores demasiado elevados: ERROR en medición
                                {
                                    label1.Text = "Pulsaciones por minuto en reposo: " + bufferBpm[bufferBpm.Count - 1] + " > 100 -> Taquicardia";
                                    label1.BackColor = Color.IndianRed;
                                }
                                else
                                {
                                    label1.Text = "Pulsaciones por minuto en reposo: ? -> Error en la medición";
                                    label1.BackColor = Color.IndianRed;
                                }
                            }     
                            else    // Latidos dentro del rango sano
                            {
                                label1.Text = "Pulsaciones por minuto en reposo: " + bufferBpm[bufferBpm.Count - 1] + " -> Latido normal";
                                label1.BackColor = Color.Lime;
                            }

                            //bufferBpm.Average();          // Calcular la media de pulsaciones por minuto hasta el momento
                            contador = 0;          // Reiniciar contador para usarlo en el próximo ciclo cardíaco
                        }
                        banderaContador = 1;       // Poner bandera a 1 en el primer escalón (forma de descartar los primeros valores, que presentan mucho ruido). Luego siempre vale 1
                    }
                    if (banderaContador == 1) 
                    { 
                        contador++;  // Aumenta nDatos veces cada vez que se consulta el sensor. Se para al hallar otro escalón.
                    }
                }

                chart1.Series[0].Points.Add(valorNuevo);     // Añadir valores a la gráfica, convertidos a microvoltios (µV)
            
            }
        }

        private void button2_Click(object sender, EventArgs e)   // Botón de guardar imagen ECG
        {
            DateTime time = DateTime.Now;

            string nombreImg = String.Format("ECG_{0}{1}{2}_{3}{4}{5}", 
                time.Day, time.Month, time.Year, time.Second, time.Minute, time.Hour);    // Formato nombre archivo guardado

            string carpeta = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))));   // Directorio

            string pathImage = carpeta +  "\\" + nombreImg + ".png";

            chart1.SaveImage(pathImage, ChartImageFormat.Png);

            MessageBox.Show("Imagen guardada: " + nombreImg + ".png");    // Mostrar mensaje de guardado con éxito
        }
    }
}
