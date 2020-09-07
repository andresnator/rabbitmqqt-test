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


        const queueName = "queue_produccer_one";
        const conn = await ampqlib.connect(settings);
        console.log("connected!")

        const chanel = await conn.createChannel();
        console.log("chanel created!")

        const res = await chanel.assertQueue(queueName);
        console.log("Queue created!")


        chanel.consume(queueName, msg =>{
            // let message = JSON.parse(msg.content.toString());

            // console.log(`Look the message = ${message["name"]}`);
            // console.log(message);


            chanel.ack(msg);
        })

    } catch (err) {
        console.error(err);
    }
}
