﻿namespace TelehealthKafkaConsumer.Settings
{
    public class KafkaConsumerSettings
    {
        public string? BootstrapServers { get; set; }
        public string? GroupId { get; set; }
        public string? Topic { get; set; }
    }
}
