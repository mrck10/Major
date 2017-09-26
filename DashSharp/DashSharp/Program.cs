/*
 Contribters to this File :
 - mrck10
*/

using System;
using System.IO;
using Major;

namespace DashSharp
{

    class Program
    {

        static void Main(string[] args)
        {
            LineCheck LineCheck = new LineCheck();
            LineCheck.argumentsFromConsole = args;

            try
            {
                int counter = 0; // counter
                string line;
                StreamReader file = new StreamReader(@args[0]); // read file to end
                bool isReadingMain = false; // is it reading inside void Main?
                while ((line = file.ReadLine()) != null) // while the file is reading
                {
                    if (line == "void Main {") // if we found the Main void
                    {
                        isReadingMain = true;
                    }

                    if (line == "}") // if the Main is Ended
                    {
                        isReadingMain = false;
                    }

                    if (isReadingMain) // if insdie Main
                    {
                        LineCheck.CheackCode(line); // Cheack line
                    }

                    counter++; // counter add 1
                }
                file.Close(); // if were done dont keep them inside the file all day and close the stream reader!
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        


        /* private static bool CompileCode(System.CodeDom.Compiler.CodeDomProvider _CodeProvider, string _SourceCode, string _SourceFile, string _ExeFile, string _AssemblyName, string[] _ResourceFiles, ref string _Errors) 
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
        */

    } 
}