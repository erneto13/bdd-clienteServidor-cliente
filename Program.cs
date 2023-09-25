using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace PRAC1_ClienteServidor_Sockets
{
    class ProgramClient
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n APLICACION CLIENTE v0.0.1");
            while (true)
            {
                try
                {
                    TcpClient tcpclnt = new TcpClient();
                    Console.ResetColor(); // Restablece los colores a los valores predeterminados
                    Console.WriteLine("\n Esperando respuesta del servidor...");
                    // Utilizamos la IP del servidor. Local ya que es la misma PC
                    tcpclnt.Connect("192.168.41.131", 8001);
                    Console.WriteLine(" Conexión exitosa al servidor");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n Introduce la instrucción a ejecutar: ");
                    Console.ResetColor(); // Restablece los colores a los valores predeterminados
                    String str = Console.ReadLine();
                    NetworkStream stm = tcpclnt.GetStream();
                    // Convertimos la cadena a ASCII para transmitirla
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    Console.ResetColor(); // Restablece los colores a los valores predeterminados
                    Console.WriteLine("\n Transmitiendo la cadena, esperando respuesta");
                    stm.Write(ba, 0, ba.Length);
                    // Recibir confirmación, se debe convertir a string
                    byte[] bb = new byte[100];
                    int k = stm.Read(bb, 0, 100);
                    string acuse = "";
                    for (int i = 0; i < k; i++)
                    {
                        acuse = acuse + Convert.ToChar(bb[i]);
                    }
                    Console.WriteLine(acuse);
                    tcpclnt.Close();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Se encontró el error: " + e.StackTrace);
                }
                Console.ResetColor(); // Restablece los colores a los valores predeterminados
            }
        }
    }
}
