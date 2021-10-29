using System;
using System.IO;
using Python.Runtime;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Runtime.PythonDLL = @"C:\Users\tu\AppData\Local\Programs\Python\Python39\python39.dll";
            string pathToVirtualEnv = @"D:\Pythonallinone\opencv\venv";
            string path = Environment.GetEnvironmentVariable("PATH")!.TrimEnd(Path.PathSeparator);
            path = string.IsNullOrEmpty(path) ? pathToVirtualEnv : path + Path.PathSeparator + pathToVirtualEnv;
            Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pathToVirtualEnv, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH",
                $"{pathToVirtualEnv}/Lib/site-packages{Path.PathSeparator}" +
                $"{pathToVirtualEnv}/Lib{Path.PathSeparator}", EnvironmentVariableTarget.Process);

            PythonEngine.PythonPath = PythonEngine.PythonPath + Path.PathSeparator + Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);
            PythonEngine.PythonHome = pathToVirtualEnv;

            PythonEngine.Initialize();
            dynamic sys = Py.Import("sys");
            //Console.WriteLine(sys.path);
            //Console.WriteLine(PythonEngine.PythonPath);

            PythonEngine.BeginAllowThreads();
            // Ejecución desde python
            using (Py.GIL())
            {
                //Example of opencv

                dynamic cv = Py.Import("cv2");

                dynamic img = cv.imread(@"C:\Users\tu\source\repos\ConsoleApp5\cho-con.jpg");

                dynamic imgBW = cv.cvtColor(img, cv.COLOR_RGB2GRAY);

                cv.imshow("Lenna.png", img);

                cv.imshow("LennaBW", imgBW);

                cv.waitKey(0);

                cv.destroyAllWindows();
            }

            Console.WriteLine("Python code ends");
        }
    }
}
