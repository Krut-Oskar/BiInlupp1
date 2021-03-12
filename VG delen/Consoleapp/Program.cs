using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consoleapp
{
    class Program
    {
        private static DeviceClient deviceClient =
            DeviceClient.CreateFromConnectionString("HostName=williamsnatverk.azure-devices.net;DeviceId=Consoleapp;SharedAccessKey=KR6TjXDX+NoNhZ8OPfyXuVjBoRU9BOrDQDOFGigDNR4=", TransportType.Mqtt);
        static void Main(string[] args)
        {
            int prevInt = 0;
            var generator = new Random();
            
            while (true)
            {
                int randInt = generator.Next() % 20 + 1;
                if (randInt != prevInt)
                {
                    prevInt = randInt;
                    var time = DateTimeOffset.Now.ToUnixTimeSeconds();
                    var msgToSend = new msg();
                    msgToSend.number = randInt;
                    string json = JsonConvert.SerializeObject(msgToSend);
                    var message = new Message(Encoding.UTF8.GetBytes(json));


                    // add properties to message event
                    message.Properties["Name"] = "Conny";
                    message.Properties["School"] = "Nackademin";
                    message.Properties["TS"] = time.ToString();
                    
                    Console.WriteLine(message);
                    SendMessageAsync(message).GetAwaiter();
                    Thread.Sleep(20 * 1000);
                }
            }
            
            
            
         
        }

        private static async Task SendMessageAsync(Message message)
        {
            
            await deviceClient.SendEventAsync(message);
        }
        public class msg
        {
            public int number { get; set; }
        }
    }
}
