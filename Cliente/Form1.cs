using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Form1 : Form
    {
        public class Contacto
        {
            public string apodo;
            private string chat = "";
            public bool bold = false;

            public string Chat { get => chat; set => chat = value; }

            public override string ToString()
            {
                return apodo;
            }
        }
        
        public static TcpClient client;
        public static Socket socket;
        public static readonly object l = new object();
        string nombre;
        string msg;
        Thread hilo;
        List<Contacto> clientList = new List<Contacto>();
        public Form1()
        {
            
            InitializeComponent();
            lbUsuarios.Visible = false;
            txtChat.Visible = false;
            btnEnviar.Visible = false;
            lbUsuarios.DisplayMember = "apodo";
            lbUsuarios.DrawMode = DrawMode.OwnerDrawFixed;

            hilo = new Thread(LeerMensaje);
            txtNombre.Focus();
        }

        public void Anadir(Contacto c)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Contacto>(Anadir), new object[] { c });
                return;
            }
            clientList.Add(c);
            lbUsuarios.Items.Add(c); 
        }
        public void Eliminar(Contacto c)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Contacto>(Eliminar), new object[] { c });
                return;
            }
            lbUsuarios.Items.Remove(c);
        }


        public void Texto(string s)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(Texto), new object[] { s });
                return;
            }
            txtChat.Text = s;
            txtChat.SelectionStart = txtChat.TextLength;
            txtChat.ScrollToCaret();
        }

        public void Texto(string s, string d)
        {
            
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string,string>(Texto), new object[] { s, d });
                    return;
                }
            if (lbUsuarios.SelectedItem != null && lbUsuarios.SelectedItem.ToString().Equals(d))
            {
                txtChat.Text += s;
                txtChat.SelectionStart = txtChat.TextLength;
                txtChat.ScrollToCaret();
            }
            else
            {


                    for (int i = 0; i < lbUsuarios.Items.Count; i++)
                    {
                        if (((Contacto)lbUsuarios.Items[i]).apodo == d)
                        {
                            ((Contacto)lbUsuarios.Items[i]).bold = true;
                        }
                    lbUsuarios.Items[i] = lbUsuarios.Items[i];
                    }
                
                lbUsuarios.Refresh();

            }


        }

        void LeerMensaje()
        {
            while (true)
            {
                try
                {
                    using (NetworkStream ns = new NetworkStream(socket))
                    using (StreamReader sr = new StreamReader(ns))
                    using (StreamWriter sw = new StreamWriter(ns))
                    {
                        string msg = sr.ReadLine();
                        if (msg.Length > 11)
                        {
                            switch (msg.Substring(0, 11))
                            {

                                case "-!''-{]:::U":
                                    if (msg.Substring(11, 2).Equals("SI"))
                                    {
                                        Anadir(new Contacto { apodo = msg.Substring(13), Chat = "" });
                                    }
                                    else
                                    {
                                        MessageBox.Show(msg.Substring(13) + " no está conectado.");
                                    }

                                    break;

                                case "-!''-{]:::E":
                                    foreach (Contacto contacto in lbUsuarios.Items)
                                    {
                                        if (contacto.apodo == msg.Substring(11))
                                        {
                                            Eliminar(contacto);
                                            break;
                                        }
                                    }
                                    break;


                                case "-!''-{]:::M":
                                    string mensaje = msg.Substring(11);
                                    string destinatario = mensaje.Substring(0, 15).Trim();
                                    mensaje = mensaje.Substring(15);
                                    msg = String.Format(Environment.NewLine) + destinatario + ": " + mensaje;
                                    foreach (Contacto contacto in clientList)
                                    {
                                        if (destinatario.Equals(contacto.apodo))
                                        {
                                            contacto.Chat += msg;
                                            Texto(msg, destinatario);
                                            break;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    /*
                    ns = client.GetStream();
                    byte[] receivedBytes = new byte[1024];
                    int byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length);
                    byte[] formated = new byte[byte_count];
                    //handle  the null characteres in the byte array
                    Array.Copy(receivedBytes, formated, byte_count);
                    string data = Encoding.ASCII.GetString(formated);
                    if (data.Length > 11)
                    {
                        switch (data.Substring(0, 11))
                        {
                            case "-!''-{]:::A":
                                string pattern = @"[^-!''\-{\]:::A]+";
                                Regex regex = new Regex(pattern);
                                MatchCollection conectados = regex.Matches(data);
                                foreach (Match match in conectados)
                                {
                                    Contacto c = new Contacto();
                                    c.apodo = match.Value;
                                    Anadir(c);
                                }
                                break;

                            case "-!''-{]:::U":
                                if (data.Substring(11, 2).Equals("SI"))
                                {
                                    Anadir(new Contacto { apodo = data.Substring(13), Chat = "" });
                                }
                                else
                                {
                                    MessageBox.Show(data.Substring(13) + " no está conectado.");
                                }

                                break;

                            case "-!''-{]:::E":
                                foreach (Contacto contacto in lbUsuarios.Items)
                                {
                                    if (contacto.apodo == data.Substring(11))
                                    {
                                        Eliminar(contacto);
                                        break;
                                    }
                                }
                                break;


                            case "-!''-{]:::M":
                                string mensaje = data.Substring(11);
                                string destinatario = mensaje.Substring(0, 15).Trim();
                                mensaje = mensaje.Substring(15);
                                data = String.Format(Environment.NewLine) + destinatario + ": " + mensaje;
                                foreach (Contacto contacto in clientList)
                                {
                                    if (destinatario.Equals(contacto.apodo))
                                    {
                                        contacto.Chat += data;
                                        Texto(data, destinatario);




                                        break;
                                    }
                                }
                                break;
                        }
                    }*/
                    /*else
                    {
                        data = String.Format(Environment.NewLine) + data;
                        Texto(data);
                    }*/

                    //Mensaje()
                    //Invoke(new Action(() => textBox3.Text += String.Format(Environment.NewLine) + data));
                    //textBox3.Text += String.Format(Environment.NewLine) + data;

                }
                catch (Exception)
                {



                }
            }
        }

        private void listboxDibujar(object sender, DrawItemEventArgs e)
        {

            Font f = e.Font;
            if (((Contacto)lbUsuarios.Items[e.Index]).bold)
            {
                f = new Font(e.Font, FontStyle.Bold);
            }
            else
            {
                f = new Font(e.Font, FontStyle.Regular);
            }
            e.DrawBackground();
            e.Graphics.DrawString(((ListBox)(sender)).Items[e.Index].ToString(), f, new SolidBrush(e.ForeColor), e.Bounds);
            e.DrawFocusRectangle();

        }

        /*private void Mensaje()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(Mensaje));
            else
                textBox3.Text = textBox3.Text + Environment.NewLine + " -> " ;
        }
        */
        private void Form1_Load(object sender, EventArgs e)
        {
            txtMensaje.Visible = false;
            btnEnviar.Visible = false;
            btnConectar.Text = "Conectar";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbUsuarios.SelectedItem != null && txtMensaje.Text != "")
            {
                using (NetworkStream ns = new NetworkStream(socket))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    string msg = "-!''-{]:::M" + lbUsuarios.SelectedItem.ToString().PadRight(15) + txtMensaje.Text;
                    sw.WriteLine(msg);
                    txtChat.Text += String.Format(Environment.NewLine) + txtMensaje.Text;
                    foreach (Contacto contacto in clientList)
                    {
                        if (lbUsuarios.SelectedItem != null && lbUsuarios.SelectedItem.ToString().Equals(contacto.apodo))
                        {
                            contacto.Chat = txtChat.Text;
                            break;
                        }
                    }
                    txtChat.SelectionStart = txtChat.TextLength;
                    txtChat.ScrollToCaret();

                    txtMensaje.Text = "";
                    txtMensaje.Focus();
                }
                /*
                byte[] buffer = Encoding.ASCII.GetBytes("-!''-{]:::M" + lbUsuarios.SelectedItem.ToString().PadRight(15) + txtMensaje.Text);
                ns.Write(buffer,0, buffer.Length);
                txtChat.Text += String.Format(Environment.NewLine) + txtMensaje.Text;
                foreach (Contacto contacto in clientList)
                {
                    if (lbUsuarios.SelectedItem != null && lbUsuarios.SelectedItem.ToString().Equals(contacto.apodo))
                    {
                        contacto.Chat = txtChat.Text;
                        break;
                    }
                }
                txtChat.SelectionStart = txtChat.TextLength;
                txtChat.ScrollToCaret();

                txtMensaje.Text = "";
                txtMensaje.Focus();
                */
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "Desconectar")
            {
                //OJO PROCESO DE DESDCONEXION
                this.Close();
            }
            else
            {
                if (txtNombre.Text != "")
                {

                    IPAddress ip = IPAddress.Parse("127.0.0.1");
                    int port = 5000;
                    IPEndPoint ie = new IPEndPoint(ip, port);

                     socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        socket.Connect(ie);
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show("Error de conexión: {0}", ex.Message);
                    }


                    using (NetworkStream ns = new NetworkStream(socket))
                    using (StreamReader sr = new StreamReader(ns))
                    using (StreamWriter sw = new StreamWriter(ns))
                    {
                        msg = sr.ReadLine();
                        MessageBox.Show(msg);
                        //Texto(msg);
                        if (msg != "")
                        {
                            sw.WriteLine("-!''-{]:::C" + txtNombre.Text);
                            sw.Flush();
                        }

                            //lock (l)
                            //{
                            msg = sr.ReadLine();

                            if (msg.Substring(0, 12) == "-!''-{]:::CA")
                                {
                                    nombre = txtNombre.Text;
                                    label1.Text = nombre;
                                    txtChat.Text = msg.Substring(12);
                                    txtChat.BackColor = System.Drawing.Color.White;
                                    txtNombre.Visible = false;
                                    btnConectar.Text = "Desconectar";
                                    txtChat.Visible = true;
                                    lbUsuarios.Visible = true;
                                    txtMensaje.Visible = true;
                                    btnEnviar.Visible = true;
                                    BtnAnadir.Visible = true;
                                    
                                /*
                                if (data.Substring(0, 11) == "-!''-{]:::L")
                                {
                                    //
                                    string pre = data.Substring(11);
                                    string pattern2 = @"[^-!''\-{\]:::X]+";
                                    Regex regex = new Regex(pattern2);
                                    MatchCollection conectados = regex.Matches(pre);
                                    foreach (Match match in conectados)
                                    {
                                        Contacto c = new Contacto();
                                        c.apodo = match.Value;
                                        Anadir(c);
                                    }
                                }*/

                                    hilo.Start();
                                }
                                else
                                {
                                    if (msg.Substring(0, 12) == "-!''-{]:::CR")
                                    {
                                        MessageBox.Show("Ya hay un usuario registrado con ese nombre");
                                        
                                    }
                                }
                            //}
                            
                        
                    }

                    /*client = new TcpClient();
                    client.Connect(ip, port);
                    ns = client.GetStream();

                    byte[] buffer = Encoding.ASCII.GetBytes("-!''-{]:::C" + txtNombre.Text);
                    ns.Write(buffer, 0, buffer.Length);
                    while (true)
                    {
                        byte[] receivedBytes = new byte[1024];
                        int byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length);
                        byte[] formated = new byte[byte_count];
                        //handle  the null characteres in the byte array
                        Array.Copy(receivedBytes, formated, byte_count);
                        string data = Encoding.ASCII.GetString(formated);

                        if (data.Substring(0, 12) == "-!''-{]:::CA")
                        {
                            nombre = txtNombre.Text;
                            label1.Text = nombre;
                            txtChat.Text = data.Substring(12);
                            txtChat.BackColor = System.Drawing.Color.White;
                            txtNombre.Visible = false;
                            btnConectar.Text = "Desconectar";
                            txtChat.Visible= true;
                            lbUsuarios.Visible = true;
                            txtMensaje.Visible = true;
                            btnEnviar.Visible = true;
                            BtnAnadir.Visible = true;
                            byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length);
                            formated = new byte[byte_count];
                            //handle  the null characteres in the byte array
                            Array.Copy(receivedBytes, formated, byte_count);
                            data = Encoding.ASCII.GetString(formated);
                            //
                            if (data.Substring(0, 11) == "-!''-{]:::L")
                            {
                                //
                                string pre = data.Substring(11);
                                string pattern2 = @"[^-!''\-{\]:::X]+";
                                Regex regex = new Regex(pattern2);
                                MatchCollection conectados = regex.Matches(pre);
                                foreach (Match match in conectados)
                                {
                                    Contacto c = new Contacto();
                                    c.apodo = match.Value;
                                    Anadir(c);
                                }
                            }//
                            
                            hilo.Start();
                            break;
                        }
                        else
                        {
                            if (data.Substring(0, 12) == "-!''-{]:::CR")
                            {
                                MessageBox.Show("Ya hay un usuario registrado con ese nombre");
                                break;
                            }
                        }
                    }*/
                }
                else
                {
                    MessageBox.Show("Debe tener un apodo");
                } 
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnConectar.Text == "Desconectar")
            {
                hilo.Suspend();
                msg = "-!''-{]:::D" + txtNombre.Text;
                using (NetworkStream ns = new NetworkStream(socket))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.WriteLine(msg);
                }
                socket.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbUsuarios.SelectedItem != null)
            {
                ((Contacto)lbUsuarios.Items[lbUsuarios.SelectedIndex]).bold = false;
                foreach (Contacto contacto in clientList)
                {
                    if (lbUsuarios.SelectedItem.ToString().Equals(contacto.apodo))
                    {
                        txtChat.Text = contacto.Chat;
                       
                        break;
                    }
                }
                txtMensaje.Focus();
            }
        }

        private void BtnAnadir_Click(object sender, EventArgs e)
        {
            using (UsuarioNuevo u = new UsuarioNuevo())
            {
                DialogResult d = u.ShowDialog();
                if (d == DialogResult.OK && u.getText() != nombre)
                {
                    msg = "-!''-{]:::U" + u.getText();
                    using (NetworkStream ns = new NetworkStream(socket))
                    using (StreamWriter sw = new StreamWriter(ns))
                    {
                        sw.WriteLine(msg);
                    }
                }
            }
            /*using (UsuarioNuevo u = new UsuarioNuevo())
            {
                DialogResult d = u.ShowDialog();
                if (d == DialogResult.OK && u.getText() != nombre)
                {
                    byte[] buffer = Encoding.ASCII.GetBytes("-!''-{]:::U" + u.getText());
                    ns.Write(buffer, 0, buffer.Length);
                }
            }*/

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnConectar.PerformClick();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnEnviar.PerformClick();
            }
        }
    }
}
