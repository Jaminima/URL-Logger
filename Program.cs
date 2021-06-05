using System;
using System.Net;
using System.IO;

namespace URLLogger
{
    class Program
    {
        static HttpListener listener;

        static void Main(string[] args)
        {
            Console.WriteLine("App Start");

            listener = new HttpListener();
            listener.Prefixes.Add("http://+:5000/");
            listener.Start();

            listener.BeginGetContext(Request, null);

            while (listener.IsListening)
            {
                Console.ReadLine();
            }
        }

        static void Request(IAsyncResult result)
        {
            HttpListenerContext context = listener.EndGetContext(result);
            listener.BeginGetContext(Request, null);

            Console.WriteLine(context.Request.RawUrl);

            StreamWriter writer = new StreamWriter(context.Response.OutputStream);
            writer.WriteLine(context.Request.RawUrl);
            writer.Flush();
            writer.Close();

            context.Response.Close();
        }
    }
}
