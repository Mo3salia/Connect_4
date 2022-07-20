using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Net.Sockets;
using System.Net;
namespace connect4_server
{
    public partial class Form1 : Form
    {
        char[,] conect = new char[6, 7];
        char type = 'x';
        int colum = 0;
        int ci = 0;
        int turn = 1;
        int goodplay;
        int r;
        int flage = 0;
        int[] col = { 5, 5, 5, 5, 5, 5, 5 };
        array app = new array();
        Byte[] bytes = new Byte[256];
        String data = null;
        //server
        TcpListener server = null;
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        NetworkStream stream;
        TcpClient client;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object x, EventArgs e)
        {
            tabControl1.Visible = false;
        }

      

        
        public void printBoard(char[,] borad)
        {

            textBox1.Text = borad[0, 0].ToString();
            textBox2.Text = borad[1, 0].ToString();
            textBox3.Text = borad[2, 0].ToString();
            textBox4.Text = borad[3, 0].ToString();
            textBox5.Text = borad[4, 0].ToString();
            textBox6.Text = borad[5, 0].ToString();
            textBox7.Text = borad[0, 1].ToString();
            textBox8.Text = borad[1, 1].ToString();
            textBox9.Text = borad[2, 1].ToString();
            textBox10.Text = borad[3, 1].ToString();
            textBox11.Text = borad[4, 1].ToString();
            textBox12.Text = borad[5, 1].ToString();
            textBox13.Text = borad[0, 2].ToString();
            textBox14.Text = borad[1, 2].ToString();
            textBox15.Text = borad[2, 2].ToString();
            textBox16.Text = borad[3, 2].ToString();
            textBox17.Text = borad[4, 2].ToString();
            textBox18.Text = borad[5, 2].ToString();
            textBox19.Text = borad[0, 3].ToString();
            textBox20.Text = borad[1, 3].ToString();
            textBox21.Text = borad[2, 3].ToString();
            textBox22.Text = borad[3, 3].ToString();
            textBox23.Text = borad[4, 3].ToString();
            textBox24.Text = borad[5, 3].ToString();
            textBox25.Text = borad[0, 4].ToString();
            textBox26.Text = borad[1, 4].ToString();
            textBox27.Text = borad[2, 4].ToString();
            textBox28.Text = borad[3, 4].ToString();
            textBox29.Text = borad[4, 4].ToString();
            textBox30.Text = borad[5, 4].ToString();
            textBox31.Text = borad[0, 5].ToString();
            textBox32.Text = borad[1, 5].ToString();
            textBox33.Text = borad[2, 5].ToString();
            textBox34.Text = borad[3, 5].ToString();
            textBox35.Text = borad[4, 5].ToString();
            textBox36.Text = borad[5, 5].ToString();
            textBox37.Text = borad[0, 6].ToString();
            textBox38.Text = borad[1, 6].ToString();
            textBox39.Text = borad[2, 6].ToString();
            textBox40.Text = borad[3, 6].ToString();
            textBox41.Text = borad[4, 6].ToString();
            textBox42.Text = borad[5, 6].ToString();
            Invalidate();
            Refresh();
        }

        public int CheckWinnerAndTie()
        {
            flage = app.winner(tabPage1, conect);
            if (flage == 1)
            {
                if (type == 'x')
                {
                    textBox44.Text = "player 1 win";
                    printBoard(conect);
                    
                   
                }
                else if (type == 'o')
                {

                    textBox44.Text = "player 2 win";
                    printBoard(conect);


                    
                }
                client.Close();
                return 2;
            }



            if (flage == 2)
            {
                textBox44.Text = ("the result is draw");
                printBoard(conect);
                client.Close();
                return 2;

            }
            return 0;
        }

       
        private void MainMenu(object x, EventArgs e)
        {
            server = new TcpListener(localAddr, port);
            server.Start();
             client = server.AcceptTcpClient();
            MessageBox.Show("Connected!");
             stream = client.GetStream();
            tabControl1.Visible = true;
            data = null;
            button8.Visible = false;

        }

        private void button1_Click(object x, EventArgs e)
        {
            int co = int.Parse((x as Button).Text);
            mm(co);
        }
        public void mm(int y) 
        {

         ci = CheckWinnerAndTie();
                if (flage == 0)
                {
                    turn = (turn + 1) % 2;
                    if (turn % 2 == 1)
                        type = 'o';
                    else if (turn % 2 == 0)
                        type = 'x';
                     goodplay = 0;
                    while (goodplay == 0)
                    {
                     
                        {
                            type = 'x';
                            Invalidate();
                            Refresh();
                            colum = y;
                            string s = colum.ToString();
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(s);
                            
                            r = col[colum - 1];
                            if (r + 1 != 0)
                            {
                                goodplay = 1;
                                stream.Write(msg, 0, msg.Length);
                            }
                            else
                            {
                                textBox44.Text = " this column is full ";
                                break;
                            }
                            conect[r, colum - 1] = app.fun1(conect, colum, type, col);
                            printBoard(conect);
                            
                        }
                      ci =  CheckWinnerAndTie();
                        if (goodplay == 1&&ci ==0)
                        {
                            type = 'o';
                            
                            {
                                int i;
                                textBox44.Text = ("waiting the other player");
                                Invalidate();
                                Refresh();
                                i = stream.Read(bytes, 0, bytes.Length);
                                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                                colum = int.Parse(data);
                                r = col[colum - 1];
                                if (r + 1 != 0)
                                    goodplay = 1;
                                else
                                    continue;
                                conect[r, colum - 1] = app.fun1(conect, colum, type, col);
                                textBox44 .Text= "";
                                printBoard(conect);
                            }
                        }
                    }
                }

             ci =   CheckWinnerAndTie();
                       }
    }
    class array
    {
       
        public int winner(TabPage playField, char[,] conect)
        {



            int flage = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (conect[i, j] == 'x' || conect[i, j] == 'o')
                        if (conect[i, j] == conect[i, j + 1] && conect[i, j] == conect[i, j + 2] && conect[i, j] == conect[i, j + 3])
                        {
                            flage = 1;
                        }
                }
            for (int j = 0; j < 7; j++)
                for (int i = 0; i < 3; i++)
                {
                    if (conect[i, j] == 'x' || conect[i, j] == 'o')
                        if (conect[i, j] == conect[i + 1, j] && conect[i, j] == conect[i + 2, j] && conect[i, j] == conect[i + 3, j])
                        {
                            flage = 1;

                        }
                }
            for (int i = 3; i < 6; i++)
                for (int j = 0; j < i - 3; j++)
                {
                    if (conect[i, j] == 'x' || conect[i, j] == 'o')
                        if (conect[i, j] == conect[i - 1, j + 1] && conect[i, j] == conect[i - 2, j + 2] && conect[i, j] == conect[i - 3, j + 3])
                        {
                            flage = 1;

                        }
                }
            for (int i = 5; i > 2; i--)
                for (int j = 3; j < 7; j++)
                {
                    if (conect[i, j] == 'x' || conect[i, j] == 'o')
                        if (conect[i, j] == conect[i - 1, j - 1] && conect[i, j] == conect[i - 2, j - 2] && conect[i, j] == conect[i - 3, j - 3])
                        {
                            flage = 1;

                        }
                }


            int drawFlag = 0;
            for(int i = 1; i<=42; i++)
            {
                TextBox tb = (TextBox)playField.Controls["textBox" + i];
                if (tb.Text == "")
                {
                    drawFlag = 1;
                    break;
                }
            }
            if (drawFlag == 0)
                flage = 2;

            return flage;
        }
        public char fun1(char[,] p, int c, char type, int[] col)
        {
            int r;
            r = col[c - 1];
            p[r, c - 1] = type;
            col[c - 1]--;
            return p[r, c - 1];
        }
    }
   
}
