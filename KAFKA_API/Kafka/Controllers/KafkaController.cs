using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kafka.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class KafkaController : ControllerBase
    {
        private ProducerConfig _config;
        private readonly string _topic;

        public KafkaController(ProducerConfig config)
        {
            this._config = config;
            this._topic = "test";
        }


        [HttpPost]
        public async Task<ActionResult> Producer([FromBody]Message message)
        {

            string serializedEmployee = JsonConvert.SerializeObject(message);
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                await producer.ProduceAsync(_topic, new Message<Null, string> { Value = serializedEmployee });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }



        [HttpGet]
        public ActionResult Consumer()
        {

            //throw new NotImplementedException();
            var config = new ConsumerConfig
            {
                GroupId = "gid-consumers",
                BootstrapServers = "kafka_demo:9092",
                EnableAutoCommit = true,
                EnableAutoOffsetStore = true
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe(_topic);
                while (true)
                {
                    var cr = consumer.Consume();
                    Console.WriteLine(cr.Message.Value);

                }
            }

            //var topic = "test";
            //string serializedEmployee = JsonConvert.SerializeObject(message);
            //using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            //{
            //    await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedEmployee });
            //    producer.Flush(TimeSpan.FromSeconds(10));
            //    return Ok(true);
            //}
        }

    }

    public class Message
    {
        public int Id { get; set; }
        public string Msg { get; set; }
    }
}
