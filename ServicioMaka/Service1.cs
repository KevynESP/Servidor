using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioMaka
{
    public partial class Service1 : ServiceBase
    {
        List<string> list = new List<string>();
        public static List<MiCliente> lista_clientes = new List<MiCliente>();
        public static readonly object l = new object();
        public bool funcionando;

        public class MiCliente
        {
            public string apodo;
            //public TcpClient tcp;
            public Socket socket;
            public MiCliente()
            {

            }
        }
        public Service1()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("ServicioMakaLog"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "ServicioMakaLog",
                    "ServicioMaka");
            }
            eventLog1.Source = "ServicioMakaLog";
            eventLog1.Log = "ServicioMaka";
        }

        public static void handle_clients(object o)
        {
            //TcpClient Cliente = (TcpClient)o;
            using (Socket sCliente = (Socket)o)
            {
                IPEndPoint ieCliente = (IPEndPoint)sCliente.RemoteEndPoint;
                string apodo = "";
                string msg = "";
                Console.WriteLine("Cliente conectado: {0} en el puerto {1}", ieCliente.Address, ieCliente.Port);

                using (NetworkStream ns = new NetworkStream(sCliente))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    string bienvenida = "Bienvenido a Maka!";
                    //\nPrueba a añadir usuarios conectados.
                    sw.WriteLine(bienvenida);
                    sw.Flush();

                    while (true)
                    {
                        //lock (l)
                        //{
                        try
                        {
                            msg = sr.ReadLine();
                            Console.WriteLine(msg);
                            if (msg != null)
                            {
                                if (msg.Length > 11 && msg.Substring(0, 11) == "-!''-{]:::C")
                                {
                                    apodo = msg.Substring(11);
                                    string tipo = sCliente.RemoteEndPoint.ToString();
                                    bool sw2 = true;

                                    foreach (MiCliente b in lista_clientes)
                                    {
                                        if (b.apodo == apodo)
                                        {
                                            msg = "-!''-{]:::CRNo se ha podido conectar";
                                            sw2 = false;
                                            sw.WriteLine(msg);
                                            sw.Flush();

                                            break;
                                        }
                                    }
                                    if (sw2)
                                    {
                                        MiCliente miCliente = new MiCliente();

                                        miCliente.socket = sCliente;
                                        miCliente.apodo = apodo;

                                        msg = "-!''-{]:::CATe has conectado";


                                        sw.WriteLine(msg);
                                        sw.Flush();


                                        foreach (MiCliente b in lista_clientes)
                                        {
                                            using (NetworkStream st2 = new NetworkStream(b.socket))
                                            {
                                                msg = "-!''-{]:::A" + apodo;
                                                sw.WriteLine(msg);
                                                sw.Flush();
                                            }
                                        }
                                        /*string nombres = "-!''-{]:::L";
                                        foreach (MiCliente b in lista_clientes)
                                        {
                                            if (b.apodo != apodo)
                                                nombres += "-!''-{]:::X" + b.apodo;
                                        }
                                        msg = nombres;
                                        sw.Write(msg);
                                        sw.Flush();*/
                                        lock (l)
                                        {
                                            lista_clientes.Add(miCliente);
                                           
                                        }

                                        Console.WriteLine("Se ha conetado: " + msg.Substring(12));
                                    }
                                }
                                else
                                {
                                    if (msg.Length > 11 && msg.Substring(0, 11) == "-!''-{]:::D")
                                    {
                                        apodo = msg.Substring(11);
                                        lock (l)
                                        {
                                            foreach (MiCliente b in lista_clientes)
                                            {
                                                if (b.apodo == apodo)
                                                {
                                                    lista_clientes.Remove(b);
                                                    break;
                                                }
                                            }
                                        }
                                       

                                        foreach (MiCliente b in lista_clientes)
                                        {
                                            using (NetworkStream st2 = new NetworkStream(b.socket))
                                            {
                                                msg = "-!''-{]:::E" + apodo;
                                                sw.WriteLine(msg);
                                                sw.Flush();
                                            }
                                        }
                                        Console.WriteLine("Se ha desconetado: " + msg.Substring(11));
                                        break;
                                    }
                                    else
                                    if (msg.Substring(0, 11) == "-!''-{]:::M")
                                    {
                                        string mensaje = msg.Substring(11);
                                        string destinatario = mensaje.Substring(0, 15).Trim();
                                        mensaje = mensaje.Substring(15);


                                        foreach (MiCliente cliente in lista_clientes)
                                        {
                                            if (destinatario.Equals(cliente.apodo))
                                            {
                                                using (NetworkStream st2 = new NetworkStream(cliente.socket))
                                                using (StreamWriter sw2 = new StreamWriter(st2))
                                                {

                                                    msg = "-!''-{]:::M" + apodo.PadRight(15) + mensaje;
                                                    sw2.WriteLine(msg);
                                                    sw2.Flush();
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    if (msg.Substring(0, 11) == "-!''-{]:::U")
                                    {
                                        string mensaje = msg.Substring(11).Trim();
                                        string result = "NO";
                                        foreach (MiCliente cliente in lista_clientes)
                                        {
                                            if (mensaje.Equals(cliente.apodo))
                                            {
                                                result = "SI";

                                                break;
                                            }
                                        }
                                        msg = "-!''-{]:::U" + result + mensaje;
                                        sw.WriteLine(msg);
                                        sw.Flush();
                                    }

                                    else
                                    {
                                        Console.WriteLine(msg);
                                        sw.WriteLine(msg);
                                        sw.Flush();
                                    }
                                }
                            }
                        }
                        catch (IOException)
                        {
                            break;
                        }
                        //}

                    }
                    Console.WriteLine("Se desconecto: " + apodo);



                    //---------------------------------------------------------
                    /*
                    NetworkStream stream = Cliente.GetStream();
                    byte[] buffer = new byte[1024];
                    byte[] buffer1 = new byte[1024];
                    int byte_count = stream.Read(buffer, 0, buffer.Length);
                    byte[] formated = new Byte[byte_count];
                    //handle  the null characteres in the byte array
                    Array.Copy(buffer, formated, byte_count);
                    string data = Encoding.ASCII.GetString(formated);

                    if (data.Length > 11 && data.Substring(0, 11) == "-!''-{]:::C")
                    {

                        apodo = data.Substring(11);
                        string tipo = Cliente.Client.RemoteEndPoint.ToString();
                        bool sw = true;
                        foreach (MiCliente b in lista_clientes)
                        {
                            if (b.apodo == apodo)
                            {
                                buffer = Encoding.ASCII.GetBytes("-!''-{]:::CRNo se ha podido conectar");
                                sw = false;
                                stream.Write(buffer, 0, buffer.Length);

                                break;
                            }
                        }
                        if (sw)
                        {
                            MiCliente miCliente = new MiCliente();

                            miCliente.tcp = Cliente;
                            miCliente.apodo = apodo;
                            buffer = Encoding.ASCII.GetBytes("-!''-{]:::CATe has conectado");
                            stream.Write(buffer, 0, buffer.Length);
                            foreach (MiCliente b in lista_clientes)
                            {
                                using (NetworkStream st2 = new NetworkStream(b.tcp.Client))
                                {
                                    buffer1 = Encoding.ASCII.GetBytes("-!''-{]:::A" + apodo);
                                    st2.Write(buffer1, 0, buffer1.Length);
                                }
                            }
                            string nombres = "-!''-{]:::L";
                            foreach (MiCliente b in lista_clientes)
                            {
                                if (b.apodo != apodo)
                                    nombres += "-!''-{]:::X" + b.apodo;
                            }
                            buffer1 = Encoding.ASCII.GetBytes(nombres);
                            stream.Write(buffer1, 0, buffer1.Length);

                            lista_clientes.Add(miCliente);

                            Console.WriteLine("Se ha conetado: " + data.Substring(11));
                        }
                    }
                    else
                    {
                        if (data.Length > 11 && data.Substring(0, 11) == "-!''-{]:::D")
                        {
                            apodo = data.Substring(11);

                            foreach (MiCliente b in lista_clientes)
                            {
                                if (b.apodo == apodo)
                                {
                                    lista_clientes.Remove(b);
                                    break;
                                }
                            }

                            foreach (MiCliente b in lista_clientes)
                            {
                                using (NetworkStream st2 = new NetworkStream(b.tcp.Client))
                                {
                                    buffer1 = Encoding.ASCII.GetBytes("-!''-{]:::E" + apodo);
                                    st2.Write(buffer1, 0, buffer1.Length);
                                }
                            }
                            Console.WriteLine("Se ha desconetado: " + data.Substring(11));
                            break;
                        }
                        else
                        if (data.Substring(0, 11) == "-!''-{]:::M")
                        {
                            string mensaje = data.Substring(11);
                            string destinatario = mensaje.Substring(0, 15).Trim();
                            mensaje = mensaje.Substring(15);


                            foreach (MiCliente cliente in lista_clientes)
                            {
                                if (destinatario.Equals(cliente.apodo))
                                {
                                    using (NetworkStream st2 = new NetworkStream(cliente.tcp.Client))
                                    {
                                        buffer1 = Encoding.ASCII.GetBytes("-!''-{]:::M" + apodo.PadRight(15) + mensaje);
                                        st2.Write(buffer1, 0, buffer1.Length);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        if (data.Substring(0, 11) == "-!''-{]:::U")
                        {
                            string mensaje = data.Substring(11).Trim();
                            string result = "NO";
                            foreach (MiCliente cliente in lista_clientes)
                            {
                                if (mensaje.Equals(cliente.apodo))
                                {
                                    result = "SI";

                                    break;
                                }
                            }
                            buffer1 = Encoding.ASCII.GetBytes("-!''-{]:::U" + result + mensaje);
                            stream.Write(buffer1, 0, buffer1.Length);
                        }

                        else
                        {
                            Console.WriteLine(data);
                            buffer = Encoding.ASCII.GetBytes(data);
                            stream.Write(buffer, 0, buffer.Length);
                        }
                    }*/
                    //Console.WriteLine(box.apodo);
                    //broadcast(list_connections, data);
                }
                sCliente.Close();
            }
        }

        public void IniciarServidor()
        {
            int port = 5000;
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, port);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    s.Bind(ie);
                    s.Listen(15);

                    while (funcionando)
                    {
                        Socket sCliente = s.Accept();
                        eventLog1.WriteEntry("Cliente conectado");
                        Thread t = new Thread(handle_clients);
                        t.Start(sCliente);
                    }
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                {
                    eventLog1.WriteEntry("Error de conexión.");
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            funcionando = true;
            Thread hilo = new Thread(IniciarServidor);
            hilo.Start();
            eventLog1.WriteEntry("ServicioMaka iniciado");
        }
            

        protected override void OnStop()
        {
            eventLog1.WriteEntry("ServicioMaka se detuvo");
            funcionando = false;
        }
    }
}
