using System;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using Core;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            XmlSerializer requestSerializer = new XmlSerializer(typeof(Core.TextRequest));
            XmlSerializer responseSerializer = new XmlSerializer(typeof(Core.StatusResponse));
            
            var address = IPAddress.Parse("127.0.0.1");
            var port = 3000;
            
            TcpListener listener = new TcpListener(address, port);
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();

            using (var stream = client.GetStream())
            {
                var textRequestDto = (Core.TextRequest) requestSerializer.Deserialize(stream);
                Console.WriteLine(textRequestDto.Text);

                var success = SaveTextToDb(textRequestDto);
                var statusResponseDto = new StatusResponse { Success = success };
                responseSerializer.Serialize(stream, statusResponseDto);
            }

        }

        public static bool SaveTextToDb(TextRequest textRequest)
        {
            Text text = mapTextRequestToTextEntity(textRequest);

            using (ExampleContext context = new ExampleContext())
            {
                try
                {
                    context.Texts.Add(text);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    return false;
                }
            }
        }

        public static Text mapTextRequestToTextEntity(TextRequest textRequest)
        {
            Text text = new Text();
            text.TextContent = textRequest.Text;
            return text;
        }
    }
}