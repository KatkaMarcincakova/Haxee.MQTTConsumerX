namespace Haxee.MQTTConsumer.DTOs
{
    public class Person
    {
        public required string ID { get; set; }
        public required DateTime TimeStamp { get; set; }
        public required string Stand { get; set; }
        public required  Status Status { get; set; }
    }
}
