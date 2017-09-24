using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace DashSharp
{

    class Program
    {
        static void Main(string[] args)
        {
            argumentsFromConsole = args;
            try
            {
                int counter = 0;
                string line;
                StreamReader file = new StreamReader(@args[0]);
                bool isReadingMain = false;
                while ((line = file.ReadLine()) != null)
                {
                    if (line == "void Main {")
                    {
                        isReadingMain = true;
                    }

                    if (line == "}")
                    {
                        isReadingMain = false;
                    }

                    if (isReadingMain)
                    {
                        CL(line);
                    }

                    counter++;
                }

                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static char LineSplitCharDash = '-';

        static void CL(string line) // Cheack Line
        {

            string[] args = line.Split(LineSplitCharDash);

            

            if (args[0] == "Echo")
            {
                Console.WriteLine(VC(args[1]));

            }
            else if (args[0] == "Set")
            {
                
                string[] Eq = args[1].Split('=');
                SetVar(Eq[0], Eq[1]);
            }
            else if (args[0] == "Print")
            {
                Console.Write(VC(args[1]));
            }
            else if (args[0] == "Math")
            {
                if (args[1] == "Round")
                {
                    SetVar(args[3], Math.Round(double.Parse(VC(args[2]))));
                }
                else if (args[1] == "Calc")
                {
                    StringToFormula sf = new StringToFormula();
                    SetVar(args[3], sf.Eval(VC(args[2])));
                }
                else if (args[1] == "Ceiling")
                {
                    SetVar(args[3], Math.Ceiling(double.Parse(VC(args[2]))));
                }
                else if (args[1] == "Floor")
                {
                    SetVar(args[3], Math.Floor(double.Parse(VC(args[2]))));
                }
                else if (args[1] == "Sqrt")
                {
                    SetVar(args[3], Math.Sqrt(double.Parse(VC(args[2]))));
                }
                else if (args[1] == "Min")
                {
                    SetVar(args[4], Math.Min(double.Parse(VC(args[2])), double.Parse(VC(args[3]))));
                }
                else if (args[1] == "Max")
                {
                    SetVar(args[4], Math.Max(double.Parse(VC(args[2])), double.Parse(VC(args[3]))));
                }
            }
            else if (args[0] == "File")
            {
                if (args[1] == "ReadAllLines")
                {
                    SetVar(args[3], File.ReadAllLines(VC(args[2])));
                }
                else if (args[1] == "ReadAllText")
                {
                    SetVar(args[3], File.ReadAllText(VC(args[2])));
                }
                else if (args[1] == "AppendAllText")
                {
                    File.AppendAllText(VC(@args[2]), VC(args[3]));
                }
                else if (args[1] == "Copy")
                {
                    File.Copy(VC(@args[2]), VC(@args[3]));
                }
                else if (args[1] == "Create")
                {
                    File.Create(VC(@args[2]));
                }
                else if (args[1] == "Delete")
                {
                    File.Delete(VC(@args[2]));
                }
                else if (args[1] == "Exists")
                {
                    if (File.Exists(VC(@args[2])))
                    {
                        SetVar(args[3], "true");
                    }
                    else
                    {
                        SetVar(args[3], "false");
                    }
                }
                else if (args[1] == "Move")
                {
                    File.Move(VC(@args[2]), VC(@args[3]));
                }
                else if (args[1] == "Write")
                {
                    StreamWriter sw = new StreamWriter(VC(@args[2]));
                    sw.Write(VC(args[3]));
                    sw.Close();
                }
                else if (args[1] == "WriteLine")
                {
                    StreamWriter sw = new StreamWriter(VC(@args[2]));
                    sw.WriteLine(VC(args[3]));
                    sw.Close();
                }

            }
            else if (args[0] == "Mail")
            {
                if (args[1].Contains("="))
                {
                    string[] EqSplit = args[1].Split('=');

                    if (EqSplit[0] == "Username")
                    {
                        emailAdress = VC(EqSplit[1]);

                    }
                    else if (EqSplit[0] == "Password")
                    {
                        emailPassword = VC(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Target")
                    {
                        emailTo = VC(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Body")
                    {
                        emailBody = VC(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Subject")
                    {
                        emailTitle = VC(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Server")
                    {
                        emailSMTPserver = VC(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Port")
                    {
                        emailPort = int.Parse(VC(EqSplit[1]));
                    }
                }
                else if (args[1] == "Send")
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(emailSMTPserver);

                    mail.From = new MailAddress(emailAdress);
                    if (emailTo.Contains(","))
                    {
                        string[] EAS = emailTo.Split(',');
                        foreach (string s in EAS)
                        {
                            mail.To.Add(s);
                        }
                    }
                    else
                    {
                        mail.To.Add(emailTo);
                    }
                    mail.Subject = emailTitle;
                    mail.Body = emailBody;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(emailAdress, emailPassword);
                    SmtpServer.EnableSsl = emailSLL;

                    SmtpServer.Send(mail);
                }
            }
            else if (args[0] == "Animation")
            {
                if (args[1] == "PlayFile")
                {
                    int counter = 0;
                    string line1;
                    StreamReader file = new StreamReader(VC(args[3])); // stream reader
                    while ((line1 = file.ReadLine()) != null)
                    {
                        if (line1 == "-")
                        {
                            Thread.Sleep(int.Parse(VC(args[2])));
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine(line);
                        }
                        counter++;
                    }
                    file.Close();
                }
                if (args[1] == "ConsoleImage")
                {
                    Image image = Image.FromFile(@args[2]);
                    FrameDimension dimension = new FrameDimension(
                        image.FrameDimensionsList[0]);
                    int frameCount = image.GetFrameCount(dimension);
                    StringBuilder sb;

                    int left = Console.WindowLeft, top = Console.WindowTop;

                    char[] chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' };

                    for (int i = 0; ; i = (i + 1) % frameCount)
                    {
                        sb = new StringBuilder();
                        image.SelectActiveFrame(dimension, i);
                        for (int h = 0; h < image.Height; h++)
                        {
                            for (int w = 0; w < image.Width; w++)
                            {
                                Color cl = ((Bitmap)image).GetPixel(w, h);
                                int gray = (cl.R + cl.G + cl.B) / 3;
                                int index = (gray * (chars.Length - 1)) / 255;

                                sb.Append(chars[index]);
                            }
                            sb.Append('\n');
                        }

                        Console.SetCursorPosition(left, top);
                        Console.Write(sb.ToString());

                        Thread.Sleep(100);
                    }
                }
            }
            else if (args[0] == "Web")
            {
                WebClient wc = new WebClient();
                if (args[1] == "Scrape")
                {
                    SetVar(args[3], wc.DownloadString(VC(@args[2])));
                }
                else if (args[1] == "DownloadFile")
                {
                    wc.DownloadFile(VC(@args[2]), VC(@args[3]));
                }
                else if (args[1] == "Ftp")
                {
                    if (args[2] == "Upload")
                    {
                        FileInfo toUpload = new FileInfo(@args[3]);
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@args[4] + toUpload.Name);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential(args[5], args[6]);
                        Stream ftpStream = request.GetRequestStream();
                        FileStream file = File.OpenRead(@args[3]);
                        int length = int.Parse(args[7]);
                        byte[] buffer = new byte[length];
                        int bytesRead = 0;

                        do
                        {
                            bytesRead = file.Read(buffer, 0, length);
                            ftpStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);

                        file.Close();
                        ftpStream.Close();
                    }
                }

            }
            else if (args[0] == "Pause")
            {
                Console.Write(VC(args[1]));
                Console.ReadKey();
            }
            else if (args[0] == "Input")
            {
                Console.Write(VC(args[1]));
                SetVar(args[2], Console.ReadLine());
            }
            else if (args[0] == "Sleep")
            {
                Thread.Sleep(int.Parse(VC(args[1])));
            }
            else if (args[0] == "String")
            {
                if (args[1] == "Trim")
                {
                    SetVar(args[4], VC(args[2].Trim(StringToChar(args[2]))));
                }
                else if (args[1] == "TrimEnd")
                {
                    SetVar(args[4], VC(args[2].TrimEnd(StringToChar(args[2]))));
                }
                else if (args[1] == "TrimStart")
                {
                    SetVar(args[4], VC(args[2].TrimStart(StringToChar(args[2]))));
                }
                else if (args[1] == "Split")
                {
                    string[] SplittedArg2 = args[2].Split(StringToChar(args[3]));
                    int count = 0;
                    foreach (string s in SplittedArg2)
                    {
                        SetVar(args[4] + count.ToString(), VC(s));
                        count++;
                    }

                }
            }
            else if (args[0] == "Console")
            {
                if (args[1] == "GetKey")
                {
                    ConsoleKeyInfo cKey = Console.ReadKey();
                    Char cKeyChar = cKey.KeyChar;
                    SetVar(args[2], cKeyChar.ToString());
                }
                else if (args[1] == "Label")
                {
                    string[] Splited = args[2].Split(',');
                    int x = int.Parse(Splited[0]);
                    int y = int.Parse(Splited[1]);
                    Console.SetCursorPosition(x, y);
                    Console.Write(args[3]);
                    Console.SetCursorPosition(0, 0);
                }
                else if (args[1] == "Draw")
                {
                   
                    if (args[2] == "Circle")
                    {
                        Console.Clear();

                        double r;

                        r = double.Parse(args[3]);

                        bool fill = false;

                        if (args[4] == "true")
                        {
                            fill = true;
                        }

                        Console.WriteLine();

                        double r_in = r - 0.4;
                        double r_out = r + 0.4;

                        for (double y = r; y >= -r; --y)
                        {
                            for (double x = -r; x < r_out; x += 0.5)
                            {
                                double value = x * x + y * y;
                                if (value >= r_in * r_in && value <= r_out * r_out)
                                {
                                    Console.Write("*");
                                }
                                else if (fill && value < r_in * r_in && value < r_out * r_out)
                                {
                                    Console.Write("*");
                                }
                                else
                                {
                                    Console.Write(" ");
                                }
                            }

                            Console.WriteLine();
                        }
                    }
                    else if (args[2] == "Square")
                    {
                        string[] Splited = args[3].Split(',');
                        int x = int.Parse(Splited[0]);
                        int y = int.Parse(Splited[1]);
                        Console.SetCursorPosition(x, y);

                        int sq_side = Convert.ToInt32(args[4]);
                        Console.WriteLine();
                        for (int i = 0; i < sq_side; ++i)
                        {
                            for (int j = 0; j < sq_side; ++j)
                                Console.Write("* ");
                            Console.WriteLine();
                        }

                        Console.WriteLine();

                    }
                    else if (args[2] == "Rectangle")
                    {
                        string[] Splited = args[3].Split(',');
                        int x = int.Parse(Splited[0]);
                        int y = int.Parse(Splited[1]);
                        Console.SetCursorPosition(x, y);

                        string[] Splited2 = args[4].Split(',');
                        int rec_height = int.Parse(Splited2[0]);
                        int rec_length = int.Parse(Splited2[1]);
                        Console.WriteLine();


                        //Draws a rectangle based on the input
                        for (int i = 0; i < rec_height; ++i)
                        {
                            for (int j = 0; j < rec_length; ++j)
                                Console.Write("* ");
                            Console.WriteLine();
                        }

                        //Break a line
                        Console.WriteLine();
                    }
                    else if (args[2] == "Triangle")
                    {
                        string[] Splited = args[3].Split(',');
                        int x = int.Parse(Splited[0]);
                        int y = int.Parse(Splited[1]);
                        Console.SetCursorPosition(x, y);

                        string[] Splited2 = args[4].Split(',');

                        int triHSize, triWSize;
                        triHSize = int.Parse(Splited2[0]);
                        triWSize = int.Parse(Splited2[1]);
                        //draw a triangle with height of triHSize and width of triWSize
                        if (triWSize > 0 && triHSize > 0)
                        {
                            double increment = (triWSize) / (triHSize); //find an appropriate increment value for width per height
                            if (increment < 1.0)
                                increment = 1.0;
                            Console.WriteLine("*");
                            double i = increment;
                            for (int t = 1; t != triHSize - 1; t++)
                            {

                                string stars = new string('*', (int)i);
                                Console.WriteLine(stars);
                                i += increment;
                                if (i >= triWSize)
                                    i = triWSize;
                                else if (i - ((int)i) > 0.5)
                                    i += 1 - (i - ((int)i));
                            }
                            string starss = new string('*', triWSize);
                            Console.WriteLine(starss);
                        }
                        else if (triWSize == 0 && triHSize > 0)
                        {
                            for (int t = 0; t != triHSize; t++)
                            {
                                string spaces = new string(' ', triHSize - t - 1);
                                string stars = new string('*', t * 2 + 1);
                                Console.WriteLine(spaces + stars);
                            }
                        }
                        else
                        {
                            Console.WriteLine("invalid height or witdth");
                        }
                    }


                }
                else if (args[1] == "MsgBox")
                {
                    int rwv = args[2].Length;
                    Console.Write("|");
                    for (int i = 1; i < rwv; i++)
                    {
                        Console.Write("-");
                    }
                    Console.Write("-");
                    Console.WriteLine("|");
                    Console.WriteLine("|" + args[2] + "|");
                    Console.Write("|");
                    for (int i = 1; i < rwv; i++)
                    {
                        Console.Write("-");
                    }
                    Console.Write("-");
                    Console.WriteLine("|");
                }
                else if (args[1] == "Hide")
                {
                    HideConsoleWindow();
                }
                else if (args[1] == "Show")
                {
                    ShowConsoleWindow();
                }



            }
            else if (args[0] == "Cryto")
            {
                if (args[1] == "Encrypt")
                {
                    SetVar(args[3], Encrypt(VC(args[2])));
                }
                else if (args[1] == "Decrypt")
                {
                    SetVar(args[3], VC(Decrypt(args[2])));
                }
            }
            else if (args[0] == "Settings")
            {
                if (args[1] == "ChangeDash")
                {
                    char[] arrayChar = args[2].ToCharArray();
                    LineSplitCharDash = arrayChar[0];
                } 
                else if (args[1] == "ResetVars")
                {
                    VariableNames.Clear();
                    VariableContent.Clear();
                }
            }
            else if (args[0] == "Gui")
            {
                if (args[1] == "Show")
                {
                    Application.EnableVisualStyles();
                    Application.Run(aForm);
                }
                else if (args[1] == "Close")
                {
                    aForm.Close();
                }
                else if (args[1] == "Title")
                {
                    aForm.Text = VC(args[2]);
                }
                else if (args[1] == "Settings")
                {
                    string[] Spl = args[2].Split('=');
                    if (Spl[0] == "FullScreen")
                    {
                        if (Spl[1] == "true")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.None;
                            aForm.WindowState = FormWindowState.Maximized;
                        }
                        else
                        {
                            aForm.FormBorderStyle = FormBorderStyle.Fixed3D;
                            aForm.WindowState = FormWindowState.Normal;
                        }
                    }
                    if (Spl[0] == "Transparent")
                    {
                        if (Spl[1] == "true")
                        {
                            WindowColorBeforeTransparent = aForm.BackColor;
                            aForm.BackColor = Color.Fuchsia;
                            aForm.TransparencyKey = Color.Fuchsia;
                        }
                        else
                        {
                            aForm.BackColor = WindowColorBeforeTransparent;
                        }
                    }
                    if (Spl[0] == "Width")
                    {
                        aForm.Width = int.Parse(Spl[1]);
                    }
                    if (Spl[0] == "Height")
                    {
                        aForm.Height = int.Parse(Spl[1]);
                    }
                    if (Spl[0] == "Size")
                    {
                        string[] splited = Spl[1].Split(',');
                        aForm.Width = int.Parse(splited[0]);
                        aForm.Width = int.Parse(splited[1]);
                    }
                    if (Spl[0] == "Icon")
                    {
                        Icon ico = new Icon(@Spl[1]);
                        aForm.Icon = ico;
                    }
                    if (Spl[0] == "BorderStyle")
                    {
                        if (Spl[1] == "Fixed3D") {
                            aForm.FormBorderStyle = FormBorderStyle.Fixed3D;
                        }
                        else if (Spl[1] == "None")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.None;
                        }
                        else if (Spl[1] == "FixedSingle")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        }
                        else if (Spl[1] == "Sizable")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.Sizable;
                        }
                        else if (Spl[1] == "SizableToolWindow")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                        }
                        else if (Spl[1] == "FixedDialog")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                        }
                        else if (Spl[1] == "FixedToolWindow")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        }
                    }
                }
                else if (args[1] == "Color")
                {
                    string[] sp = args[2].Split('=');
                    if (sp[0] == "BackGround")
                    {
                        aForm.BackColor = Color.FromName(VC(sp[1]));
                    }
                    else if (sp[0] == "ForeGround")
                    {
                        aForm.ForeColor = Color.FromName(VC(sp[1]));
                    }
                }
                else if (args[1] == "Get")
                {
                    foreach (Control c in aForm.Controls)
                    {
                        if (c.Name == args[2])
                        {
                            if (args[3] == "Name")
                            {
                                SetVar(args[4], c.Name);
                            }
                            else if (args[3] == "Font")
                            {
                                SetVar(args[3], c.Font);
                            }
                            else if (args[3] == "Width")
                            {
                                SetVar(args[3], c.Width);
                            }
                            else if (args[3] == "Height")
                            {
                                SetVar(args[3], c.Height);
                            }
                            else if (args[3] == "ForeColor")
                            {
                                SetVar(args[3], c.ForeColor);
                            }
                            else if (args[3] == "BackColor")
                            {
                                SetVar(args[3], c.BackColor);
                            }
                            else if (args[3] == "Text")
                            {
                                SetVar(args[4], c.Text);
                            }
                            else if (args[3] == "Value")
                            {
                                if (c is TrackBar)
                                {
                                    SetVar(args[4], ((TrackBar)c).Value);
                                } 
                                else if (c is ProgressBar)
                                {
                                    SetVar(args[4], ((ProgressBar)c).Value);
                                }
                            }
                        }
                    }
                }
                else if (args[1].Contains("New"))
                {
                    //string[] Split = args[1].Split(' ');
                    if (args[1] == "New")
                    {
                        if (args[2] == "Label")
                        {
                            aForm.Controls.Add(new Label() { Name = VC(args[3]) });
                        }
                        else if (args[2] == "TextBox")
                        {
                            aForm.Controls.Add(new TextBox() { Name = VC(args[3]) });
                        }
                        else if (args[2] == "Image")
                        {
                            aForm.Controls.Add(new PictureBox() { Name = VC(args[3]) });
                        }
                        else if (args[2] == "GroupBox")
                        {
                            aForm.Controls.Add(new GroupBox() { Name = VC(args[3]) });
                        }
                        else if (args[2] == "Button")
                        {
                            aForm.Controls.Add(new Button() { Name = VC(args[3]) });
                        }
                        else if (args[2] == "ProgressBar")
                        {
                            aForm.Controls.Add(new ProgressBar() { Name = VC(args[3]) });
                        }
                        else if (args[2] == "Panel")
                        {
                            aForm.Controls.Add(new Panel() { Name = VC(args[3]) });
                        }
                        else if (args[2] == "TrackBar")
                        {
                            aForm.Controls.Add(new TrackBar() { Name = VC(args[3]) });
                        }
                    }
                }
                else
                {
                    foreach (Control c in aForm.Controls)
                    {
                        if (c.Name == args[1] && c is Label)
                        {
                            string[] Split = VC(args[2]).Split('=');
                            if (Split[0] == "Text")
                            {
                                ((Label)c).Text = Split[1];
                            }
                            else if (Split[0] == "BackColor")
                            {
                                ((Label)c).BackColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "ForeColor")
                            {
                                ((Label)c).ForeColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "Location")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((Label)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((Label)c).Name = VC(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((Label)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }


                        }
                        if (c.Name == args[1] && c is TextBox)
                        {
                            string[] Split = VC(args[2]).Split('=');
                            if (Split[0] == "Text")
                            {
                                ((TextBox)c).Text = Split[1];
                            }
                            else if (Split[0] == "Name")
                            {
                                ((TextBox)c).Name = Split[1];
                            }
                            else if (Split[0] == "Text")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((TextBox)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((TextBox)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "BackColor")
                            {
                                ((TextBox)c).BackColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "ForeColor")
                            {
                                ((TextBox)c).ForeColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "Location")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((TextBox)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[1].Contains("true"))
                            {
                                if (Split[0] == "Multiline")
                                {
                                    ((TextBox)c).Multiline = true;
                                }
                                else if (Split[0] == "ReadOnly")
                                {
                                    ((TextBox)c).ReadOnly = true;
                                }
                                else if (Split[0] == "WordWrap")
                                {
                                    ((TextBox)c).WordWrap = true;
                                }
                            }
                            else if (Split[1].Contains("false"))
                            {
                                if (Split[0] == "Multiline")
                                {
                                    ((TextBox)c).Multiline = false;
                                }
                                else if (Split[0] == "ReadOnly")
                                {
                                    ((TextBox)c).ReadOnly = false;
                                }
                                else if (Split[0] == "WordWrap")
                                {
                                    ((TextBox)c).WordWrap = false;
                                }
                            }
                        }
                        if (c.Name == args[1] && c is GroupBox)
                        {
                            string[] Split = VC(args[2]).Split('=');
                            if (Split[0] == "Text")
                            {
                                ((GroupBox)c).Text = Split[1];
                            }
                            else if (Split[0] == "BackColor")
                            {
                                ((GroupBox)c).BackColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "ForeColor")
                            {
                                ((GroupBox)c).ForeColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "Location")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((GroupBox)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((GroupBox)c).Name = VC(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((GroupBox)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "Width")
                            {
                                //string[] fontStuff = MakeArray(Split[1]);
                                ((GroupBox)c).Width = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Height")
                            {
                                ((GroupBox)c).Height = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Size")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((GroupBox)c).Width = int.Parse(Sp[0]);
                                ((GroupBox)c).Height = int.Parse(Sp[1]);
                            }
                            else if (Split[0] == "AutoSize")
                            {
                                if (Split[1] == "true")
                                {
                                    ((GroupBox)c).AutoSize = true;
                                }
                                else
                                {
                                    ((GroupBox)c).AutoSize = false;
                                }
                            }

                        }
                        if (c.Name == args[1] && c is PictureBox)
                        {
                            string[] Split = VC(args[2]).Split('=');
                            if (Split[0] == "Text")
                            {
                                ((PictureBox)c).Text = Split[1];
                            }
                            else if (Split[0] == "BackColor")
                            {
                                ((PictureBox)c).BackColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "ForeColor")
                            {
                                ((PictureBox)c).ForeColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "Location")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((PictureBox)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((PictureBox)c).Name = VC(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((PictureBox)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "Width")
                            {
                                //string[] fontStuff = MakeArray(Split[1]);
                                ((PictureBox)c).Width = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Height")
                            {
                                ((PictureBox)c).Height = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Size")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((PictureBox)c).Width = int.Parse(Sp[0]);
                                ((PictureBox)c).Height = int.Parse(Sp[1]);
                            }
                            else if (Split[0] == "AutoSize")
                            {
                                if (Split[1] == "true")
                                {
                                    ((PictureBox)c).AutoSize = true;
                                }
                                else
                                {
                                    ((PictureBox)c).AutoSize = false;
                                }
                            }
                            else if (Split[0] == "ImageLocation")
                            {
                                ((PictureBox)c).ImageLocation = Split[1];
                            }
                        }
                        if (c.Name == args[1] && c is Button)
                        {
                            string[] Split = VC(args[2]).Split('=');
                            if (Split[0] == "Text")
                            {
                                ((Button)c).Text = Split[1];
                            }
                            else if (Split[0] == "BackColor")
                            {
                                ((Button)c).BackColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "ForeColor")
                            {
                                ((Button)c).ForeColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "Location")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((Button)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((Button)c).Name = VC(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((Button)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "Event")
                            {
                                string allArgsAfter = "";
                                int counterInt = 0;
                                foreach (string s in args)
                                {

                                    if (counterInt >= 2)
                                    {
                                        allArgsAfter += s + "-";
                                    }
                                    counterInt++;
                                }
                                string[] allArgsAfterBefore = allArgsAfter.Split('=');
                                allArgsAfter = allArgsAfterBefore[1].TrimEnd('-');
                                ((Button)c).Click += (s, e) => { CL(VC(allArgsAfter)); };

                            }


                        }
                        if (c.Name == args[1] && c is ProgressBar)
                        {
                            string[] Split = VC(args[2]).Split('=');
                            if (Split[0] == "Text")
                            {
                                ((ProgressBar)c).Text = Split[1];
                            }
                            else if (Split[0] == "BackColor")
                            {
                                ((ProgressBar)c).BackColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "ForeColor")
                            {
                                ((ProgressBar)c).ForeColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "Location")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((ProgressBar)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((ProgressBar)c).Name = VC(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((ProgressBar)c).Font = new Font(VC(fontStuff[0]), int.Parse(VC(fontStuff[1])), FontStyle.Regular);
                            }
                            else if (Split[0] == "Value")
                            {
                                ((ProgressBar)c).Value = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Width") {
                                ((ProgressBar)c).Width = int.Parse(VC(Split[1]));
                            }
                            else if (Split[0] == "Height")
                            {
                                ((ProgressBar)c).Height = int.Parse(VC(Split[1]));
                            }
                        }
                        if (c.Name == args[1] && c is Panel)
                        {
                            string[] Split = args[2].Split('=');
                            Split[1] = VC(Split[1]);
                            
                            if (Split[0] == "BackColor")
                            {
                                ((Panel)c).BackColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "ForeColor")
                            {
                                ((Panel)c).ForeColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "Location")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((Panel)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((Panel)c).Name = VC(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((Panel)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "Width")
                            {
                                ((Panel)c).Width = int.Parse(VC(Split[1]));
                            }
                            else if (Split[0] == "Width")
                            {
                                ((Panel)c).Height = int.Parse(VC(Split[1]));
                            }
                            else if (Split[0] == "Width")
                            {
                                string[] QuoteSplit = Split[1].Split(',');
                                ((Panel)c).Width = int.Parse(VC(QuoteSplit[0]));
                                ((Panel)c).Height = int.Parse(VC(QuoteSplit[1]));
                            }


                        }
                        if (c.Name == args[1] && c is TrackBar)
                        {
                            string[] Split = VC(args[2]).Split('=');
                            if (Split[0] == "Text")
                            {
                                ((TrackBar)c).Text = Split[1];
                            }
                            else if (Split[0] == "BackColor")
                            {
                                ((TrackBar)c).BackColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "ForeColor")
                            {
                                ((TrackBar)c).ForeColor = Color.FromName(Split[1]);
                            }
                            else if (Split[0] == "Location")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((TrackBar)c).Location = new Point(int.Parse(Sp[0]), int.Parse(Sp[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                string[] Sp = MakeArray(Split[1]);
                                ((TrackBar)c).Name = VC(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((TrackBar)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "Value")
                            {
                                ((TrackBar)c).Value = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Height")
                            {
                                ((TrackBar)c).Height = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Width")
                            {
                                ((TrackBar)c).Width = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Size")
                            {
                                string[] Stuff = MakeArray(Split[1]);
                                ((TrackBar)c).Width = int.Parse(Stuff[0]);
                                ((TrackBar)c).Height = int.Parse(Stuff[1]);
                            }
                            else if (Split[0] == "Minimum")
                            {
                                ((TrackBar)c).Minimum = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Maximum")
                            {
                                ((TrackBar)c).Maximum = int.Parse(Split[1]);
                            }
                            




                        }
                    }
                }
            }
            else if (args[0] == "Compile")
            {
                if (args[1] == "CSharp")
                {
                    string _Errors = "";
                    StreamReader sr = new StreamReader(VC(args[2]));
                    if (CompileCode(new Microsoft.CSharp.CSharpCodeProvider(), @sr.ReadToEnd(), null, VC(args[3]), null, null, ref _Errors))
                        Console.Write("");
                    else
                        Console.WriteLine("Error occurred during compilation : \r\n" + _Errors);
                    sr.Close();
                }
                else if (args[1] == "MajorToExe")
                {

                }

            }
            else if (args[0] == "SendKeys")
            {
                SendKeys.SendWait(args[1]);
            }
            else if (args[0] == "if")
            {
                if (args[1].Contains("="))
                {
                    string[] splitArg = args[1].Split('=');

                    string CombinedArgs = "";
                    int count = 0;
                    foreach (string s in args)
                    {
                        if (count > 1)
                        {
                            CombinedArgs += s + "-";
                        }
                        count++;
                    }

                    CombinedArgs = CombinedArgs.TrimEnd('-');

                    if (VC(splitArg[0]) == VC(splitArg[1]))
                    {
                        CL(VC(CombinedArgs));
                    }
                }
            }
            else if (args[0] == "Color")
            {
                string[] EqSplit = VC(args[1]).Split('=');

                if (EqSplit[0] == "Fore" || EqSplit[0] == "ForeGround")
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), EqSplit[1]);
                } else if (EqSplit[0] == "Back" || EqSplit[0] == "BackGround")
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), EqSplit[1]);
                } else if (EqSplit[0] == "Re" || EqSplit[0] == "Reset")
                {
                    Console.ResetColor();
                }
            }
            else if (args[0] == "Call")
            {
                if (args[2] == null || args[2].Length == 0)
                {
                    int counter = 0;
                    string line6;
                    StreamReader file = new StreamReader(@args[1]);
                    bool isReadingMain = false;
                    while ((line6 = file.ReadLine()) != null)
                    {
                        if (line6 == "void Main {")
                        {
                            isReadingMain = true;
                        }

                        if (line6 == "}")
                        {
                            isReadingMain = false;
                        }

                        if (isReadingMain)
                        {
                            CL(line6);
                        }

                        counter++;
                    }

                    file.Close();
                }
                else
                {
                    int counter = 0;
                    string line6;
                    StreamReader file = new StreamReader(@args[1]);
                    bool isReadingMain = false;

                    bool isInFunc = false;  
                    while ((line6 = file.ReadLine()) != null)
                    {
                        if (line6 == "void " + args[2] + " {")
                        {
                            isReadingMain = true;
                        }

                        if (line6 == "}")
                        {
                            isReadingMain = false;
                        }

                        if (isReadingMain)
                        {
                            CL(line6);
                        }

                        if (line6 == "function " + args[0] + " {")
                        {
                            int counter2 = 0;
                            foreach (string s in args)
                            {
                                SetVar(args[0] + counter2.ToString(), s);
                                //Console.WriteLine(args[0] + counter2.ToString());
                                //Console.WriteLine(s);
                                counter2++;
                            }
                            isInFunc = true;
                        }

                        if (isInFunc == true && line6 == "}")
                        {
                            isInFunc = false;

                            int counter2 = 0;
                            foreach (string s in args)
                            {
                                VariableNames.Remove(args[0] + counter2.ToString());
                                VariableContent.Remove(args[counter2]);
                                counter2++;
                            }

                            break;
                        }

                        if (isInFunc)
                        {
                            CL(line6);
                        }




                        counter++;
                    }

                    file.Close();
                }
            }
            else
            {
                int counter = 0;
                string line5;
                StreamReader file = new StreamReader(@argumentsFromConsole[0]);

                bool isInVoid = false;

                bool isInFunc = false;
                string LastLine = line;

                while ((line5 = file.ReadLine()) != null)
                {

                    // voids
                    if (line5 == "void " + args[0] + " {")
                    {
                        isInVoid = true;
                    }
                    if (isInVoid == true && line5 == "}")
                    {
                        isInVoid = false;
                    }
                    if (isInVoid)
                    {
                        CL(line5);
                    }



                    // functions
                    if (line5 == "function " + args[0] + " {")
                    {
                        int counter2 = 0;
                        foreach (string s in args)
                        {
                                SetVar(args[0] + counter2.ToString(), s);
                                //Console.WriteLine(args[0] + counter2.ToString());
                                //Console.WriteLine(s);
                                counter2++;
                        }
                        isInFunc = true;
                    }

                    if (isInFunc == true && line5 == "}")
                    {
                        isInFunc = false;

                        int counter2 = 0;
                        foreach (string s in args)
                        {
                                VariableNames.Remove(args[0] + counter2.ToString());
                                VariableContent.Remove(args[counter2]);
                                counter2++;
                        }

                        break;
                    }

                    if (isInFunc)
                    {
                        CL(line5);
                    }


                    counter++;
                }

                file.Close();
            }

        }

        static string VC(string str)
        {
            string TempString = str;
            int count = 0; // number counter

            foreach (string s in VariableNames) // foreach string in var names
            {
                TempString = TempString.Replace(VariableNames[count], VariableContent[count].ToString()); // Replaces var names with the content of the vars and turns it from a object to a string.
                count++;
            }

            return TempString; // returns the string done :)
        } 

        static void SetVar (string name, object anything)
        {
            VariableNames.Remove(name);
            VariableContent.Remove(anything);

            VariableContent.Add(anything);
            VariableNames.Add(name);
        } // sets a var

        static List<string> VariableNames = new List<string>(); // var names
        static List<object> VariableContent = new List<object>(); // var content

        static ConsoleColor ColorToString(string color)
        {
            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }

        // variables for cool stuff down here please!

        static string emailAdress;
        static string emailPassword;
        static string emailTo;
        static string emailSMTPserver;
        static int emailPort;
        static bool emailSLL = true;
        static string emailBody;
        static string emailTitle;

       

        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");

        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);


        // Form

        static Form aForm = new Form();

        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);


        public static void HideConsoleWindow()
        {
            IntPtr hWnd = GetConsoleWindow();
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 0);
            }
        }

        public static void ShowConsoleWindow()
        {
            IntPtr hWnd = GetConsoleWindow();
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 1);
            }
        }

        public static void ButtonHandler_Click(Object sender, EventArgs e)
        {

        }

        static Color WindowColorBeforeTransparent;

        public static string[] MakeArray(string str)
        {
            return str.Split(',');
        }


        private static bool CompileCode(System.CodeDom.Compiler.CodeDomProvider _CodeProvider, string _SourceCode, string _SourceFile, string _ExeFile, string _AssemblyName, string[] _ResourceFiles, ref string _Errors)
        {
            // set interface for compilation
            System.CodeDom.Compiler.ICodeCompiler _CodeCompiler = _CodeProvider.CreateCompiler();

            // Define parameters to invoke a compiler
            System.CodeDom.Compiler.CompilerParameters _CompilerParameters =
                new System.CodeDom.Compiler.CompilerParameters();

            if (_ExeFile != null)
            {
                // Set the assembly file name to generate.
                _CompilerParameters.OutputAssembly = _ExeFile;

                // Generate an executable instead of a class library.
                _CompilerParameters.GenerateExecutable = true;
                _CompilerParameters.GenerateInMemory = false;
            }
            else if (_AssemblyName != null)
            {
                // Set the assembly file name to generate.
                _CompilerParameters.OutputAssembly = _AssemblyName;

                // Generate an executable instead of a class library.
                _CompilerParameters.GenerateExecutable = false;
                _CompilerParameters.GenerateInMemory = false;
            }
            else
            {
                // Generate an executable instead of a class library.
                _CompilerParameters.GenerateExecutable = false;
                _CompilerParameters.GenerateInMemory = true;
            }


            // Generate debug information.
            //_CompilerParameters.IncludeDebugInformation = true;

            // Set the level at which the compiler 
            // should start displaying warnings.
            _CompilerParameters.WarningLevel = 3;

            // Set whether to treat all warnings as errors.
            _CompilerParameters.TreatWarningsAsErrors = false;

            // Set compiler argument to optimize output.
            _CompilerParameters.CompilerOptions = "/optimize";
            

            // Set a temporary files collection.
            // The TempFileCollection stores the temporary files
            // generated during a build in the current directory,
            // and does not delete them after compilation.
            _CompilerParameters.TempFiles = new System.CodeDom.Compiler.TempFileCollection(".", true);

            if (_ResourceFiles != null && _ResourceFiles.Length > 0)
                foreach (string _ResourceFile in _ResourceFiles)
                {
                    // Set the embedded resource file of the assembly.
                    _CompilerParameters.EmbeddedResources.Add(_ResourceFile);
                }


            try
            {
                // Invoke compilation
                System.CodeDom.Compiler.CompilerResults _CompilerResults = null;

                if (_SourceFile != null && System.IO.File.Exists(_SourceFile))
                    // soruce code in external file
                    _CompilerResults = _CodeCompiler.CompileAssemblyFromFile(_CompilerParameters, _SourceFile);
                else
                    // source code pass as a string
                    _CompilerResults = _CodeCompiler.CompileAssemblyFromSource(_CompilerParameters, _SourceCode);

                if (_CompilerResults.Errors.Count > 0)
                {
                    // Return compilation errors
                    _Errors = "";
                    foreach (System.CodeDom.Compiler.CompilerError CompErr in _CompilerResults.Errors)
                    {
                        _Errors += "Line number " + CompErr.Line +
                                    ", Error Number: " + CompErr.ErrorNumber +
                                    ", '" + CompErr.ErrorText + ";\r\n\r\n";
                    }

                    // Return the results of compilation - Failed
                    return false;
                }
                else
                {
                    // no compile errors
                    _Errors = null;
                }
            }
            catch (Exception _Exception)
            {
                // Error occurred when trying to compile the code
                _Errors = _Exception.Message;
                return false;
            }

            // Return the results of compilation - Success
            return true;
        }


       

        private List<string> ButtonClickEventCL = new List<string>();

        public static string[] argumentsFromConsole;
        private static List<string> voidAndFunctions = new List<string>();

        static ConsoleColor StringToConsoleColor(string str) // str means string 
        {
            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), str); // converts your input into a string
        }

        static char StringToChar(string str)
        {
            char[] array = str.ToCharArray();
            return array[0];
        }

        static char[] StringToCharArray(string str)
        {
            char[] array = str.ToCharArray();
            return array;
        }


    } 
}