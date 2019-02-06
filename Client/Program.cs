using System;
using System.Net.Sockets;
using System.Xml.Serialization;
using Core;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Request object to be serialized
            TextRequest textRequestDto = new TextRequest { Text = "This is some more text"}; 
            
            XmlSerializer requestSerializer = new XmlSerializer(typeof(TextRequest));
            XmlSerializer responseSerializer = new XmlSerializer(typeof(StatusResponse));
            
            var address = "127.0.0.1";
            var port = 3000;
            
            TcpClient clientConnection = new TcpClient(address, port);

            using (var stream = clientConnection.GetStream())
            {
                requestSerializer.Serialize(stream, textRequestDto);
                
                // Hacky solution to read blocking
                clientConnection.Client.Shutdown(SocketShutdown.Send);

                var statusResponse = (StatusResponse) responseSerializer.Deserialize(stream);
                Console.WriteLine(statusResponse.Success ? "Success" : "Failure");
            }
            
        }
    }
}