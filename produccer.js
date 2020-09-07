const ampqlib = require("amqplib");

const settings = {
    protocol: "amqp",
    hostname: "ubuntu-server",
    port: 5672,
    username: "test",
    password: "test",
    vhost: "/",
    authMechanism: ["PLAIN", "AMQPLAIN", "EXTERNAL"]
}



connect();

async function connect() {
    try {

        const msgs=[
            {name:"name-1", type:"test-performance"},
            {name:"name-2", type:"test-performance"},
            {name:"name-3", type:"test-performance"},
            {name:"name-4", type:"test-performance"},
            {name:"name-5", type:"test-performance"}
        ]

        const queueName = "queue_produccer_one";
        const conn = await ampqlib.connect(settings);
        console.log("connected!")

        const chanel = await conn.createChannel();
        console.log("chanel created!")
        
        const res = await chanel.assertQueue(queueName);
        console.log("Queue created!")

        for(let msg in msgs){
            await chanel.sendToQueue(queueName, Buffer.from(JSON.stringify(msgs[msg])));
            console.log("Message send to Rabbit");
        }


    } catch (err) {
        console.error(err);
    }
}
