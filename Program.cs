using Haxee.MQTTConsumer.DTOs;
using MQTTnet.Client.Receiving;
using System.Security.Claims;
using System.Text;

class Program
{
    private static string _currentYearTopic = "2023/#";
    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        // Creates a new client
        MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                    .WithClientId("HaxeeConsumer")
                                    .WithTcpServer("192.168.0.244", 1883);

        // Create client options objects
        ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                                .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                                .WithClientOptions(builder.Build())
                                .Build();

        // client object
        IManagedMqttClient _mqttClient = new MqttFactory().CreateManagedMqttClient();


        // Set up handlers
        _mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
        _mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
        _mqttClient.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);

        _mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(a => {
            Log.Logger.Information("Message recieved: {payload}", a.ApplicationMessage);
        });

        // Starts a connection with the Broker
        _mqttClient.StartAsync(options).GetAwaiter().GetResult();




        _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
            .WithTopic(_currentYearTopic)
            .Build()).GetAwaiter().GetResult();

        _mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            try
            {
                string topic = e.ApplicationMessage.Topic;
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.Write($"{topic}: ");
                Console.WriteLine(payload);
                /*

                if (string.IsNullOrWhiteSpace(topic) == false)
                {
                    string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    string type = topic.Substring(topic.LastIndexOf('/') + 1);

                    if (topic.Contains("person"))
                    {
                        var id = payload.Split(' ')[0];
                        var dateTime = payload.Split(' ')[1];

                        var person = new Person
                        {
                            PersonId = id,
                            At = DateTime.Parse(dateTime)
                        };

                        Console.WriteLine(JsonSerializer.Serialize(person));
                        PostApi(person);
                    }
                    else if (topic.Contains("alarm"))
                    {
                        var value = payload.Split(' ')[0];

                        var valueBoolean = false;
                        if (value.Equals("true"))
                        {
                            valueBoolean = true;
                        }
                        else
                        {
                            valueBoolean = false;
                        }

                        var alarm = new Alarm
                        {
                            Enabled = valueBoolean
                        };

                        Console.WriteLine(JsonSerializer.Serialize(alarm));
                        PostApi(alarm);
                    }
                    else
                    {
                        var value = payload.Split(' ')[0];
                        var dateTime = payload.Split(' ')[1];


                        var dataPoint = new DataPoint
                        {
                            Value = Convert.ToSingle(value.Replace('.', ',')),
                            At = DateTime.Parse(dateTime),
                        };

                        Console.WriteLine(JsonSerializer.Serialize(dataPoint));
                        PostApi(dataPoint, type);
                    }
                    Console.WriteLine($"Topic: {topic}. Message Received: {payload}");
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        });

        while (true)
        {
            Task.Delay(1000).GetAwaiter().GetResult();
        }
    }

    // HANDLERS
    public static void OnConnected(MqttClientConnectedEventArgs obj)
    {
        Log.Logger.Information("Successfully connected.");
    }

    public static void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
    {
        Log.Logger.Warning("Couldn't connect to broker.");
    }

    public static void OnDisconnected(MqttClientDisconnectedEventArgs obj)
    {
        Log.Logger.Information("Successfully disconnected.");
    }
}