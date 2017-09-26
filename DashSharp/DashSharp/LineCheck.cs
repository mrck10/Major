/*
This File is used for cheacking the Lines of Code.
You could call this file the 2nd Program.cs
*/
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
using Major;

namespace Major
{
    public class LineCheck
    {
        public char splitDash = '-'; // The Defualt Dash is set and 

        public void CheackCode(string line) // Cheacks the Code 
        {
            FormRelatedFunctions FormRelatedFunctions = new FormRelatedFunctions();
            Cryto Cryto = new Cryto();
            EmailFunctions EmailFunctions = new EmailFunctions();

            string[] args = line.Split(splitDash); // Splits the line into arguments which are divied up by the Dash (which defualtly is '-')

            if (args[0] == "Echo")
            {
                Console.WriteLine(VariableCheack(args[1])); // Cheacks for Vars and Prints args 1 to screen
            } 
            else if (args[0] == "Set")
            {

                string[] argumentSplited = args[1].Split('='); // Splits args 1 with '='

                int count = 0; // counter

                // This foreach statement is for old variables that need to be assigned.

                foreach (object obj in VariableContent) // foreach object in variable content
                {
                    if (VariableNames[count] == argumentSplited[0]) 
                    {
                        VariableNames[count] = argumentSplited[0]; // sets old var to new var
                        VariableContent[count] = argumentSplited[1]; 
                        break; // "exits the line"
                    } 
                    count++; // count add 1
                }
                // if it's a new variable it will do this :
                SetVar(argumentSplited[0], argumentSplited[1]); // Sets Left of the '=' as the Name and right as the Content.
            }
            else if (args[0] == "Print")
            {
                Console.Write(VariableCheack(args[1])); // Prints Text to screen 
            } 
            else if (args[0] == "Math") // Math Functions are easy. So i did not leave very many code Comments
            {
                if (args[1] == "Round") // 
                {
                    SetVar(args[3], Math.Round(double.Parse(VariableCheack(args[2])))); // 
                }
                else if (args[1] == "Calc")
                {
                    StringToFormula stringToFormula = new StringToFormula();
                    SetVar(args[3], StringToFormula.Eval(VariableCheack(args[2]))); // Cheack the code in String to Formula i didnt write it my self.
                }
                else if (args[1] == "Ceiling")
                {
                    SetVar(args[3], Math.Ceiling(double.Parse(VariableCheack(args[2])))); 
                }
                else if (args[1] == "Floor")
                {
                    SetVar(args[3], Math.Floor(double.Parse(VariableCheack(args[2]))));
                }
                else if (args[1] == "Sqrt")
                {
                    SetVar(args[3], Math.Sqrt(double.Parse(VariableCheack(args[2]))));
                }
                else if (args[1] == "Min")
                {
                    SetVar(args[4], Math.Min(double.Parse(VariableCheack(args[2])), double.Parse(VariableCheack(args[3]))));
                }
                else if (args[1] == "Max")
                {
                    SetVar(args[4], Math.Max(double.Parse(VariableCheack(args[2])), double.Parse(VariableCheack(args[3]))));
                }
            } 
            else if (args[0] == "File")
            {
                if (args[1] == "ReadAllText")
                {
                    SetVar(args[3], File.ReadAllText(VariableCheack(args[2]))); // Reads the Hole file and Cheacks for variables
                }
                else if (args[1] == "AppendAllText")
                {
                    File.AppendAllText(VariableCheack(@args[2]), VariableCheack(args[3])); // Writes to a file basicly
                }
                else if (args[1] == "Copy")
                {
                    File.Copy(VariableCheack(@args[2]), VariableCheack(@args[3])); // Copys 1 file to another
                }
                else if (args[1] == "Create")
                {
                    File.Create(VariableCheack(@args[2])); // Creates a New File
                }
                else if (args[1] == "Delete")
                {
                    File.Delete(VariableCheack(@args[2])); // Deletes a file
                }
                else if (args[1] == "Exists")
                {
                    if (File.Exists(VariableCheack(@args[2]))) // Cheacks if the File Exisits
                    {
                        SetVar(args[3], "true"); // return true if it does
                    }
                    else
                    {
                        SetVar(args[3], "false"); // return false if it does not
                    }
                }
                else if (args[1] == "Move")
                {
                    File.Move(VariableCheack(@args[2]), VariableCheack(@args[3])); // Moves 1 file to another location
                }
                else if (args[1] == "Write")
                {
                    StreamWriter streamWriter = new StreamWriter(VariableCheack(@args[2])); // Creates a Stream writer 
                    try
                    {
                        streamWriter.Write(VariableCheack(args[3])); // Write what the input has asked 
                    }
                    finally
                    {
                        streamWriter.Close(); // Closes the Stream Writer so it's not paused all day.
                    }
                }
                else if (args[1] == "WriteLine")
                {
                    StreamWriter streamWriter = new StreamWriter(VariableCheack(@args[2])); // Creates a Stream writer 
                    try
                    {
                        streamWriter.WriteLine(VariableCheack(args[3])); // Write what the input has asked
                        
                    } finally
                    {
                        streamWriter.Close(); // Closes the Stream Writer so it's not paused all day.
                    }
                }

            } 
            else if (args[0] == "Mail")
            {
                if (args[1].Contains("=")) // if it does contain '=' that means it's setting a variable, other wise send a email
                {
                    string[] EqSplit = args[1].Split('=');

                    if (EqSplit[0] == "Username")
                    {
                        EmailFunctions.emailAdress = VariableCheack(EqSplit[1]);

                    }
                    else if (EqSplit[0] == "Password")
                    {
                        EmailFunctions.emailPassword = VariableCheack(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Target")
                    {
                        EmailFunctions.emailTo = VariableCheack(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Body")
                    {
                        EmailFunctions.emailBody = VariableCheack(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Subject")
                    {
                        EmailFunctions.emailTitle = VariableCheack(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Server")
                    {
                        EmailFunctions.emailSMTPserver = VariableCheack(EqSplit[1]);
                    }
                    else if (EqSplit[0] == "Port")
                    {
                        EmailFunctions.emailPort = int.Parse(VariableCheack(EqSplit[1]));
                    }
                }
                else if (args[1] == "Send") 
                {
                    MailMessage mail = new MailMessage(); // Declares a new Email
                    SmtpClient SmtpServer = new SmtpClient(EmailFunctions.emailSMTPserver); // Gets the Server

                    mail.From = new MailAddress(EmailFunctions.emailAdress); // Gets the username
                    if (EmailFunctions.emailTo.Contains(",")) // if it is a array than have multiple email adresses
                    {
                        string[] EmailAdresses = EmailFunctions.emailTo.Split(','); // Splits like it would in an array
                        foreach (string str in EmailAdresses) // foreach Email adress
                        {
                            mail.To.Add(str); // Add the email to the sendng list
                        }
                    }
                    else
                    {
                        mail.To.Add(EmailFunctions.emailTo); // adds one email
                    }

                    mail.Subject = EmailFunctions.emailTitle; // Gets the subject and sets it
                    mail.Body = EmailFunctions.emailBody; // Gets the Body and sets it

                    SmtpServer.Port = EmailFunctions.emailPort;  // Gets the Port and sets it
                    SmtpServer.Credentials = new System.Net.NetworkCredential(EmailFunctions.emailAdress, EmailFunctions.emailPassword); // Gets Credentials and Sets them
                    SmtpServer.EnableSsl = EmailFunctions.emailSLL; // Gets email sll and sets it

                    SmtpServer.Send(mail); // Sends the email. DONE
                }
            } 
            else if (args[0] == "Animation")
            {
                if (args[1] == "PlayFile") // Plays a file from a Major animation file
                {
                    int counter = 0; // The counter to count each line
                    string lineCurrentlyReading; // The Line it's Currently Reading
                    StreamReader streamReader = new StreamReader(VariableCheack(args[3])); // stream reader declared
                    while ((lineCurrentlyReading = streamReader.ReadLine()) != null) // While the file is Reading and isnt at End Of File (EOF)
                    {
                        if (lineCurrentlyReading == "-") // The animation is Splitted up by one line just being '-'
                        {
                            Thread.Sleep(int.Parse(VariableCheack(args[2]))); // Sleeps for the time the user set 
                            Console.Clear(); // Clears the screen
                        }
                        else
                        {
                            Console.WriteLine(line); // else we can just contuie to write more.
                        }
                        counter++; // The counter incresses so the lines incress
                    }
                    streamReader.Close(); // Closes the stream reader.
                }
                if (args[1] == "ConsoleImage") // Console Image Function
                {
                    Image image = Image.FromFile(@args[2]); // Declars a new image
                    FrameDimension dimension = new FrameDimension( // Sets a new Frame Dimension
                        image.FrameDimensionsList[0]); // gets it from the file dimensions
                    int frameCount = image.GetFrameCount(dimension); // gets the frame count if its a gif.
                    StringBuilder stringBuilder; // makes a new String Builder

                    int left = Console.WindowLeft, top = Console.WindowTop; // gets some console info on Console Dimensions

                    char[] chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' }; // Sets the Chars it will show

                    for (int i = 0; ; i = (i + 1) % frameCount) // for command
                    {
                        stringBuilder = new StringBuilder(); // Make a new String Builder
                        image.SelectActiveFrame(dimension, i); // Sets the active frame
                        for (int h = 0; h < image.Height; h++) // For image Height
                        {
                            for (int w = 0; w < image.Width; w++) // Fore image Width
                            {
                                Color cl = ((Bitmap)image).GetPixel(w, h); // Gets pixel
                                int gray = (cl.R + cl.G + cl.B) / 3;  // Sets gray int
                                int index = (gray * (chars.Length - 1)) / 255; // sets the index int 

                                stringBuilder.Append(chars[index]); // Appends the chars 
                            }
                            stringBuilder.Append('\n'); // \n means new line
                        }

                        Console.SetCursorPosition(left, top); // Sets the cursor posotion
                        Console.Write(stringBuilder.ToString()); // Writes what we just did

                        Thread.Sleep(100); // Sleeps for 1/10 secs
                    }
                }
            } 
            else if (args[0] == "Web")
            {
                WebClient webClient = new WebClient(); // new Webclient 

                if (args[1] == "Scrape")
                {
                    SetVar(args[3], webClient.DownloadString(VariableCheack(@args[2]))); // Downloads the site code. Sets it as a variable
                }
                else if (args[1] == "DownloadFile") 
                {
                    webClient.DownloadFile(VariableCheack(@args[2]), VariableCheack(@args[3])); // Downloads the file, Sets it as a variable
                }
            } 
            else if (args[0] == "Pause")
            {
                Console.Write(VariableCheack(args[1])); // Prompts the user
                Console.ReadKey(); // Reads the key acting like a pause
            } 
            else if (args[0] == "Input")
            {
                Console.Write(VariableCheack(args[1])); // Writes the Prompt ( print ) 
                SetVar(args[2], Console.ReadLine()); // Sets the var from the Input Console.ReadLine
            } 
            else if (args[0] == "Sleep")
            {
                Thread.Sleep(int.Parse(VariableCheack(args[1]))); // cheacks for vars, converts to a int and stops the console thread
            } 
            else if (args[0] == "String")
            {
                if (args[1] == "Trim")
                {
                    SetVar(args[4], VariableCheack(args[2].Trim(StringToChar(args[2])))); // Trims the input returns as a variable
                }
                else if (args[1] == "TrimEnd")
                {
                    SetVar(args[4], VariableCheack(args[2].TrimEnd(StringToChar(args[2])))); // Trims the end of the code
                }
                else if (args[1] == "TrimStart")
                {
                    SetVar(args[4], VariableCheack(args[2].TrimStart(StringToChar(args[2])))); // Trims the start of the code
                }
                else if (args[1] == "Split")
                {
                    string[] args2Splitted = args[2].Split(StringToChar(args[3])); // Splits arg 2 with the charitor to split with (String to Char Function)
                    int count = 0; // the counter
                    foreach (string str in args2Splitted)  // Foreach string str in args2Spliited
                    {
                        SetVar(args[4] + count.ToString(), VariableCheack(str)); // Set outputvar + number so each split is outputvar + number
                        count++; // adds 1 to count (++)
                    }

                }
            } 
            else if (args[0] == "Console")
            {
                if (args[1] == "GetKey")
                {
                    ConsoleKeyInfo consoleKey = Console.ReadKey(); // Console Read Key
                    Char consoleKeyChar = consoleKey.KeyChar; // Console Key Converted to a Charitor
                    SetVar(args[2], consoleKeyChar.ToString()); // Sets output var as the Charitor
                }
                else if (args[1] == "Label")
                {
                    int xCursorNow = Console.CursorLeft; 
                    int yCursorNow = Console.CursorTop;
                    // Sets x and y axis now

                    string[] Location = VariableCheack(args[2]).Split(','); // Splits so , seperates the Labels location
                    int x = int.Parse(Location[0]); // Convert string 2 int 
                    int y = int.Parse(Location[1]); // Convert string 2 int 
                    Console.SetCursorPosition(x, y);  // Set Cursor Position From X and Y
                    Console.Write(VariableCheack(args[3])); // Prints args 3 

                    Console.SetCursorPosition(xCursorNow, yCursorNow); // Goes back to were the cursor was.
                }
                else if (args[1] == "Draw")
                {
                    if (args[2] == "Circle")
                    {
                        double radius = double.Parse(args[3]); // sets it to args
                         
                        bool fill = false; // fill = defualt to false

                        if (args[4] == "true") // changes fill if it is true
                        {
                            fill = true;
                        }

                        Console.WriteLine(); // WriteLine cause i had bugs

                        double r_in = radius - 0.4; 
                        double r_out = radius + 0.4;

                        for (double y = radius; y >= -radius; --y) 
                        {
                            for (double x = -radius; x < r_out; x += 0.5)
                            {
                                double value = x * x + y * y;
                                if (value >= r_in * r_in && value <= r_out * r_out)
                                {
                                    Console.Write("*"); // writes to console if the all of the above is true
                                }
                                else if (fill && value < r_in * r_in && value < r_out * r_out)
                                {
                                    Console.Write("*"); // wries again
                                }
                                else
                                {
                                    Console.Write(" "); // again if its end
                                }
                            }

                            Console.WriteLine(); // new line
                        }
                    }
                    else if (args[2] == "Square")
                    {
                        string[] Location = args[3].Split(','); // Splits the argument
                        int x = int.Parse(Location[0]); // converts strings to ints
                        int y = int.Parse(Location[1]);

                        Console.SetCursorPosition(x, y); // sets the console postion

                        int sq_side = Convert.ToInt32(args[4]); // converts string to int
                        Console.WriteLine();
                        for (int i = 0; i < sq_side; ++i) // each sq_side
                        {
                            for (int j = 0; j < sq_side; ++j) 
                                Console.Write("* ");
                            Console.WriteLine();
                        }

                        Console.WriteLine(); // bug fixed

                    }
                    else if (args[2] == "Rectangle")
                    {
                        string[] Location = args[3].Split(','); // Splits the argument and puts it in the Location array
                        int x = int.Parse(Location[0]); // Converts string to int
                        int y = int.Parse(Location[1]);

                        Console.SetCursorPosition(x, y); // Sets the Cursor postion

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

                                string stars = new string('*', (int)i); // stars and cool stuff
                                Console.WriteLine(stars);
                                i += increment;
                                if (i >= triWSize)
                                    i = triWSize;
                                else if (i - ((int)i) > 0.5)
                                    i += 1 - (i - ((int)i));
                            }
                            string starss = new string('*', triWSize); // more stars
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
                    int argsLength = args[2].Length; // gets arg2 Length
                    Console.Write("|");
                    for (int i = 1; i < argsLength; i++) // Writes the first line above
                    {
                        Console.Write("-");
                    }
                    Console.Write("-");
                    Console.WriteLine("|"); // Writes the 2nd line
                    Console.WriteLine("|" + args[2] + "|"); // writes the input
                    Console.Write("|");
                    for (int i = 1; i < argsLength; i++) // Writes the line bellow
                    {
                        Console.Write("-");
                    }
                    Console.Write("-");
                    Console.WriteLine("|");
                }
                else if (args[1] == "Hide")
                {
                    FormRelatedFunctions.HideConsoleWindow(); // Hides Console Window
                }
                else if (args[1] == "Show")
                {
                    FormRelatedFunctions.ShowConsoleWindow(); // Shows the Console Window
                }
                else if (args[1] == "Clear")
                {
                    Console.Clear(); // Clears the Console Window
                }



            } // Console realted commands
            else if (args[0] == "Cryto")
            {
                if (args[1] == "Encrypt")
                {
                    SetVar(args[3], Cryto.Encrypt(VariableCheack(args[2]))); // Encrypts the Console Window
                }
                else if (args[1] == "Decrypt")
                {
                    SetVar(args[3], VariableCheack(Cryto.Decrypt(args[2]))); // Decrypts the Console Window
                }
            } 
            else if (args[0] == "Settings")
            {
                if (args[1] == "ChangeDash")
                {
                    char[] arrayChar = args[2].ToCharArray(); // Char array from string
                    splitDash = arrayChar[0]; // Sets the Dash to Item 0 in the Array char, so dont have more than one kids :)
                }
                else if (args[1] == "ResetVars")
                {
                    VariableNames.Clear(); 
                    VariableContent.Clear();
                    // Clears both Variables
                }
            }
            else if (args[0] == "Gui")
            {
                if (args[1] == "Show")
                {
                    Application.EnableVisualStyles(); // Fix
                    Application.Run(aForm); // Runs it
                }
                else if (args[1] == "Close")
                {
                    aForm.Close(); // Closes form
                }
                else if (args[1] == "Title")
                {
                    aForm.Text = VariableCheack(args[2]); // Sets Form title to arguments 2
                }
                else if (args[1] == "Settings")
                {
                    string[] argumentsSplitted = args[2].Split('=');
                    if (argumentsSplitted[0] == "FullScreen")
                    {
                        if (argumentsSplitted[1] == "true")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.None; 
                            aForm.WindowState = FormWindowState.Maximized; // sets it to full screen if Full Screen is true
                        }
                        else
                        {
                            aForm.FormBorderStyle = FormBorderStyle.Fixed3D;
                            aForm.WindowState = FormWindowState.Normal; // sets it to non full screen if Full screen is not true
                        }
                    }
                    if (argumentsSplitted[0] == "Transparent")
                    {
                        if (argumentsSplitted[1] == "true")
                        {
                            FormRelatedFunctions.WindowColorBeforeTransparent = aForm.BackColor; // sets the color to change if needed to change back
                            aForm.BackColor = Color.Fuchsia; // sets the  color to Fuchsia
                            aForm.TransparencyKey = Color.Fuchsia; // sets the Transparcy key to Fuchsia
                        }
                        else
                        {
                            aForm.BackColor = FormRelatedFunctions.WindowColorBeforeTransparent; // sets the color back to the color it was before.
                        }
                    }
                    if (argumentsSplitted[0] == "Width")
                    {
                        aForm.Width = int.Parse(argumentsSplitted[1]); // Sets the Form width 
                    }
                    if (argumentsSplitted[0] == "Height") // Sets the form Height
                    {
                        aForm.Height = int.Parse(argumentsSplitted[1]);
                    }
                    if (argumentsSplitted[0] == "Size") // sets the Form size ( size is width + height )
                    {
                        string[] splittedArgument = argumentsSplitted[1].Split(','); // Splits string
                        aForm.Width = int.Parse(splittedArgument[0]); // Converts both ints
                        aForm.Height = int.Parse(splittedArgument[1]);
                    }
                    if (argumentsSplitted[0] == "Icon") 
                    {
                        Icon ico = new Icon(@argumentsSplitted[1]); // Gets the icon from the file
                        aForm.Icon = ico; // Sets the Icon
                    }
                    if (argumentsSplitted[0] == "BorderStyle") 
                    {
                        if (argumentsSplitted[1] == "Fixed3D")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.Fixed3D; // Sets the Borderstyle to Fixed 32
                        }
                        else if (argumentsSplitted[1] == "None")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.None; // Sets the Borderstyle to None
                        }
                        else if (argumentsSplitted[1] == "FixedSingle")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.FixedSingle; // Sets the Borderstyle to FixedSingle
                        }
                        else if (argumentsSplitted[1] == "Sizable")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.Sizable; // Sets the Borderstyle to Sizable
                        }
                        else if (argumentsSplitted[1] == "SizableToolWindow")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.SizableToolWindow; // Sets the BorderStyle to Sizable Tool Window
                        }
                        else if (argumentsSplitted[1] == "FixedDialog")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.FixedDialog; // Sets the Borderstyle to Fixed Dialog
                        }
                        else if (argumentsSplitted[1] == "FixedToolWindow")
                        {
                            aForm.FormBorderStyle = FormBorderStyle.FixedToolWindow; // Sets the Borderstyle to Fixed Tool Window
                        }
                    }
                }
                else if (args[1] == "Color")
                {
                    string[] argumentSplitted = args[2].Split('='); // Splits arguemnts 2 by the '=' charitor
                    if (argumentSplitted[0] == "BackGround")
                    {
                        aForm.BackColor = Color.FromName(VariableCheack(argumentSplitted[1])); // Changes the Back Color of the Form
                    }
                    else if (argumentSplitted[0] == "ForeGround")
                    {
                        aForm.ForeColor = Color.FromName(VariableCheack(argumentSplitted[1])); // Changes the Fore Color of the Form
                    }
                }
                else if (args[1] == "Get")
                {
                    foreach (Control c in aForm.Controls) // For each Control in aForm
                    {
                        if (c.Name == args[2])
                        {
                            // For all of The get commands, it's cheacking if its value has been called than settings an output variable with the get Value
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
                                // i had to do this becuase the only a handfull of Controls have the Value property on them.
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
                    if (args[1] == "New")
                    {
                        // Adds a new Label with name, the name is called when you want to edit a property.
                        if (args[2] == "Label")
                        {
                            aForm.Controls.Add(new Label() { Name = VariableCheack(args[3]) });
                        }
                        else if (args[2] == "TextBox")
                        {
                            aForm.Controls.Add(new TextBox() { Name = VariableCheack(args[3]) });
                        }
                        else if (args[2] == "Image")
                        {
                            aForm.Controls.Add(new PictureBox() { Name = VariableCheack(args[3]) });
                        }
                        else if (args[2] == "GroupBox")
                        {
                            aForm.Controls.Add(new GroupBox() { Name = VariableCheack(args[3]) });
                        }
                        else if (args[2] == "Button")
                        {
                            aForm.Controls.Add(new Button() { Name = VariableCheack(args[3]) });
                        }
                        else if (args[2] == "ProgressBar")
                        {
                            aForm.Controls.Add(new ProgressBar() { Name = VariableCheack(args[3]) });
                        }
                        else if (args[2] == "Panel")
                        {
                            aForm.Controls.Add(new Panel() { Name = VariableCheack(args[3]) });
                        }
                        else if (args[2] == "TrackBar")
                        {
                            aForm.Controls.Add(new TrackBar() { Name = VariableCheack(args[3]) });
                        }
                    }
                }
                else
                {
                    foreach (Control c in aForm.Controls) // Foreach Contorl in aForm
                    {
                        if (c.Name == args[1] && c is Label) // if it's a label
                        {

                            // Basicly everything here is the same, just diffrent property  sets.


                            string[] Split = VariableCheack(args[2]).Split('=');
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
                                ((Label)c).Name = VariableCheack(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((Label)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }


                        }
                        if (c.Name == args[1] && c is TextBox)
                        {
                            string[] Split = VariableCheack(args[2]).Split('=');
                            if (Split[0] == "Text")
                            {
                                ((TextBox)c).Text = Split[1];
                            }
                            else if (Split[0] == "Name")
                            {
                                ((TextBox)c).Name = Split[1];
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
                                string[] Location = MakeArray(Split[1]);
                                ((TextBox)c).Location = new Point(int.Parse(Location[0]), int.Parse(Location[1]));
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
                            string[] Split = VariableCheack(args[2]).Split('=');
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
                                string[] Location = MakeArray(Split[1]);
                                ((GroupBox)c).Location = new Point(int.Parse(Location[0]), int.Parse(Location[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                ((GroupBox)c).Name = VariableCheack(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((GroupBox)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "Width")
                            {
                                ((GroupBox)c).Width = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Height")
                            {
                                ((GroupBox)c).Height = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Size")
                            {
                                string[] sizeSplit = MakeArray(Split[1]);
                                ((GroupBox)c).Width = int.Parse(sizeSplit[0]);
                                ((GroupBox)c).Height = int.Parse(sizeSplit[1]);
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
                            string[] Split = VariableCheack(args[2]).Split('=');
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
                                string[] Location = MakeArray(Split[1]);
                                ((PictureBox)c).Location = new Point(int.Parse(Location[0]), int.Parse(Location[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                ((PictureBox)c).Name = VariableCheack(args[5]);
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
                                string[] Size = MakeArray(Split[1]);
                                ((PictureBox)c).Width = int.Parse(Size[0]);
                                ((PictureBox)c).Height = int.Parse(Size[1]);
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
                            string[] Split = VariableCheack(args[2]).Split('=');
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
                                string[] Location = MakeArray(Split[1]);
                                ((Button)c).Location = new Point(int.Parse(Location[0]), int.Parse(Location[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                ((Button)c).Name = VariableCheack(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((Button)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "Event")
                            {
                                string allArgs = ""; // all args the command
                                int counterInt = 0; // counter int
                                foreach (string s in args) // foreach string in args
                                {

                                    if (counterInt >= 2)
                                    {
                                        allArgs += s + "-"; // add s to counter args
                                    }
                                    counterInt++; // counter int add 1
                                }
                                string[] allArgsAfter = allArgs.Split('='); // all args after 
                                allArgsAfter[1] = allArgsAfter[1].TrimEnd('-'); // changed this so it makes more scence
                                ((Button)c).Click += (s, e) => { CheackCode(VariableCheack(allArgsAfter[1])); }; // Click button Event add
                            }
                        }
                        if (c.Name == args[1] && c is ProgressBar)
                        {
                            string[] Split = VariableCheack(args[2]).Split('=');
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
                                string[] Location = MakeArray(Split[1]);
                                ((ProgressBar)c).Location = new Point(int.Parse(Location[0]), int.Parse(Location[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                ((ProgressBar)c).Name = VariableCheack(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((ProgressBar)c).Font = new Font(VariableCheack(fontStuff[0]), int.Parse(VariableCheack(fontStuff[1])), FontStyle.Regular);
                            }
                            else if (Split[0] == "Value")
                            {
                                ((ProgressBar)c).Value = int.Parse(Split[1]);
                            }
                            else if (Split[0] == "Width")
                            {
                                ((ProgressBar)c).Width = int.Parse(VariableCheack(Split[1]));
                            }
                            else if (Split[0] == "Height")
                            {
                                ((ProgressBar)c).Height = int.Parse(VariableCheack(Split[1]));
                            }
                        }
                        if (c.Name == args[1] && c is Panel)
                        {
                            string[] Split = args[2].Split('=');
                            Split[1] = VariableCheack(Split[1]);

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
                                string[] Location = MakeArray(Split[1]);
                                ((Panel)c).Location = new Point(int.Parse(Location[0]), int.Parse(Location[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                ((Panel)c).Name = VariableCheack(args[5]);
                            }
                            else if (Split[0] == "Font")
                            {
                                string[] fontStuff = MakeArray(Split[1]);
                                ((Panel)c).Font = new Font(fontStuff[0], int.Parse(fontStuff[1]), FontStyle.Regular);
                            }
                            else if (Split[0] == "Width")
                            {
                                ((Panel)c).Width = int.Parse(VariableCheack(Split[1]));
                            }
                            else if (Split[0] == "Height")
                            {
                                ((Panel)c).Height = int.Parse(VariableCheack(Split[1]));
                            }
                            else if (Split[0] == "Size")
                            {
                                string[] Size = Split[1].Split(',');
                                ((Panel)c).Width = int.Parse(VariableCheack(Size[0]));
                                ((Panel)c).Height = int.Parse(VariableCheack(Size[1]));
                            }
                        }
                        if (c.Name == args[1] && c is TrackBar)
                        {
                            string[] Split = VariableCheack(args[2]).Split('=');
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
                                string[] Location = MakeArray(Split[1]);
                                ((TrackBar)c).Location = new Point(int.Parse(Location[0]), int.Parse(Location[1]));
                            }
                            else if (Split[0] == "Name")
                            {
                                ((TrackBar)c).Name = VariableCheack(args[5]);
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
                                string[] Size = MakeArray(Split[1]);
                                ((TrackBar)c).Width = int.Parse(Size[0]);
                                ((TrackBar)c).Height = int.Parse(Size[1]);
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
            } // Gui in here
            else if (args[0] == "Compile")
            {
                if (args[1] == "CSharp")
                {
                    
                }
            } 
            else if (args[0] == "SendKeys")
            {
                SendKeys.SendWait(args[1]); // SendKeys.SendWait works but the other one (what most people use) doesn't work.
            } 
            else if (args[0] == "if") 
            {
                if (args[1].Contains("=")) // cheack if its a =
                {
                    string[] splitArg = args[1].Split('='); // Splits by Arg

                    string combinedArgs = ""; // Combined args is Set
                    int count = 0; // counter
                    foreach (string s in args) 
                    {
                        if (count > 1) 
                        {
                            combinedArgs += s + "-"; // Adds args to combined Args
                        }
                        count++;
                    }

                    combinedArgs = combinedArgs.TrimEnd('-'); // Trims the end so it does not have a '-' on the end

                    if (VariableCheack(splitArg[0]) == VariableCheack(splitArg[1])) // does the if command
                    {
                        CheackCode(VariableCheack(combinedArgs)); // cheacks the Code
                    }
                }
            } 
            else if (args[0] == "Color")
            {
                string[] eqSplit = VariableCheack(args[1]).Split('='); // Splits Args 1 into variable Equal Split

                if (eqSplit[0] == "Fore" || eqSplit[0] == "ForeGround") 
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), eqSplit[1]); // Convert String > Color
                }
                else if (eqSplit[0] == "Back" || eqSplit[0] == "BackGround")
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), eqSplit[1]);// Convert String > Color
                }
                else if (eqSplit[0] == "Re" || eqSplit[0] == "Reset")
                {
                    Console.ResetColor(); // Reset Color
                }
            } 
            else if (args[0] == "Call")
            {
                if (args[2] == null || args[2].Length == 0)
                {
                    int counter = 0; // Counter
                    string currentLine; // Current Line Reading 
                    StreamReader file = new StreamReader(@args[1]); // Stream reader args 1 ( file ) 

                    bool isReadingMain = false; // Bool for is reading Main 

                    while ((currentLine = file.ReadLine()) != null)
                    {
                        if (currentLine == "void Main {") // if it detects the Main void
                        {
                            isReadingMain = true; // is Reading Main is true!
                        }

                        if (currentLine == "}") // if its at the end of the main
                        {
                            isReadingMain = false; // set Is Reading Man to false!
                        }

                        if (isReadingMain) // is isReadingMain is true
                        {
                            CheackCode(currentLine); // Cheack the code for that line!
                        }

                        counter++; // counter add
                    }
                    file.Close(); // Close file so no wait.
                }
                else
                {
                    int counter = 0; // counter
                    string currentLine; // current line reading
                    StreamReader file = new StreamReader(@args[1]); // stream reader , reading file

                    bool isReadingMain = false; // Is the current line Reading the Main void?

                    bool isInFunc = false; // Is the current line Reading a Function?

                    while ((currentLine = file.ReadLine()) != null)
                    {
                        if (currentLine == "void " + args[2] + " {") // Does the current line were reading equal to the void we called?
                        {
                            isReadingMain = true; // is Reading Main is true!
                        }

                        if (currentLine == "}") // EOF ( end of function ( it actually means End Of Flie but who cares? Some memer "i do", me "screw you" ) ) 
                        {
                            isReadingMain = false; // is Reading Main is false!
                        }

                        if (isReadingMain) // if Reading the Main void is True than...
                        {
                            CheackCode(currentLine);  // Cheack the code , current line
                        }

                        if (currentLine == "function " + args[0] + " {") // if we found it as a function
                        {
                            int counter2 = 0; // counter
                            foreach (string str in args) // foreach styring in args
                            {
                                SetVar(args[0] + counter2.ToString(), str); // Set Function Arguments
                                counter2++; // add 1 to count
                            }
                            isInFunc = true; // yeah we can say it's in a function.
                        }

                        if (isInFunc == true && currentLine == "}") // End of Function
                        {
                            isInFunc = false; // set it

                            int counter2 = 0; // counter
                            foreach (string str in args) // for each string in args
                            {
                                VariableNames.Remove(args[0] + counter2.ToString()); // remove function variable names
                                VariableContent.Remove(args[counter2]); // remove function variable content
                                counter2++; // counter add 1
                            }
                            break; // breack out of if statement
                        }
                        if (isInFunc)
                        {
                            CheackCode(currentLine); // Cheack the code if it's in a function becuase that is what a function is!!!!
                        }
                        counter++; // add one to the counter
                    }
                    file.Close(); // Close the streamReader / File Reader
                }
            } 
            else if (args[0] == "foreach")
            {
                string[] arraySplit = MakeArray(VariableCheack(args[3])); // makes an array

                int counter = 0; // counter
                string argsAfter = ""; // strings after the code ( the code to do after the for comand ) 

                foreach (string str in args) // foreach string in args
                {
                    if (counter > 3) // if counter is above 3 ( after the for command )
                    {
                        argsAfter += str + "-"; // add the args to the main args
                    }
                    counter++; // counter add 1
                }

                argsAfter = argsAfter.TrimEnd('-'); // trim the end of the args after string

                foreach (object obj in arraySplit) // does the actuall for loop
                {
                    SetVar(args[1], obj); // sets the var 
                    CheackCode(VariableCheack(argsAfter)); // cheacks the code

                    VariableNames.Remove(args[1]); // removes the variable we just used
                    VariableContent.Remove(obj); // removes the variable we just used

                    // now it's ready to loop again scince we removed the variable
                }
            }
            else
            {
                int counter = 0; // counter int
                string currentLine; // current line string
                StreamReader file = new StreamReader(@argumentsFromConsole[0]); // Gets arguments from Console or whatever and uses that file to Stream read it again
                 
                bool isInVoid = false; // Is the current line in a void?

                bool isInFunc = false; // is the current line in a function?

                string LastLine = line; // get the line paramter from this function CheackLine

                while ((currentLine = file.ReadLine()) != null) // while file isnt at end of file (EOF) 
                {

                    // voids
                    if (currentLine == "void " + args[0] + " {") // is it a void?
                    {
                        isInVoid = true;
                    }
                    if (isInVoid == true && currentLine == "}") // is it at end of void?
                    {
                        isInVoid = false;
                    }
                    if (isInVoid) // is it in a void?
                    {
                        CheackCode(currentLine);  // than Cheack the code
                    }



                    // functions
                    if (currentLine == "function " + args[0] + " {") // is it in a function?
                    {
                        int counter2 = 0; 
                        foreach (string str in args) // foreach string in args
                        {
                            SetVar(args[0] + counter2.ToString(), str); // Set a variable 
                            counter2++; // counter add 1
                        }
                        isInFunc = true; // ok now it's safe to say were in a function.
                    }

                    if (isInFunc == true && currentLine == "}") // end of function
                    {
                        isInFunc = false; // safe to say, where gona return

                        int counter2 = 0; // counter 
                        foreach (string str in args) // for each string in args
                        {
                            VariableNames.Remove(args[0] + counter2.ToString()); // Remove vars (In Function Vars)
                            VariableContent.Remove(args[counter2]); // Remove Vars (In Function Vars)
                            counter2++; // counter add 1
                        }

                        break; // return
                    }

                    if (isInFunc)
                    {
                        CheackCode(currentLine); // cheack the code current line.
                    }

                    counter++; // counter add 1
                }

                file.Close(); // close so we dont have a endless stop
            } 

        }

        public string VariableCheack(string str)
        {
            string tempString = str; // Temperary String to use
            int count = 0; // number counter

            foreach (string s in VariableNames) // foreach string in var names
            {
                tempString = tempString.Replace( // 
                    VariableNames[count], // Replace Variable Name 
                    VariableContent[count].ToString() // With the Variable Content
                    );
                count++; // Count + 1 so it goes and cheacks the Next Variable
            }

            return tempString; // returns the string done :)
        }  // Cheacks String for Variable

        public void SetVar(string name, object anything)
        {
            VariableContent.Add(anything); // Adds Content 
            VariableNames.Add(name); // Adds the Name 
        } // sets a var

        public List<string> VariableNames = new List<string>(); // Variable names stored as a string
        public List<object> VariableContent = new List<object>(); // Variable content stored as a object

        public ConsoleColor ColorToString(string color) // String > Color 
        {
            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color); // Converts String > Color
        }

        public Form aForm = new Form(); // Gui form

        public static string[] MakeArray(string str)
        {
            return str.Split(','); // makes an array from spliting with ','
        }

        public static string[] argumentsFromConsole; // args from file args / Console

        private static List<string> voidAndFunctions = new List<string>();

        static char StringToChar(string str) // string 2 char
        {
            char[] array = str.ToCharArray(); // add to char array
            return array[0]; // return the first char
        }

        static char[] StringToCharArray(string str) // string to char array
        {
            char[] array = str.ToCharArray(); 
            return array; // return str but as a char array.
        }
    }
}
