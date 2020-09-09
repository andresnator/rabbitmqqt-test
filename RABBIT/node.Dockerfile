FROM node:10

WORKDIR /app
COPY . .

RUN npm i -s

CMD [ "node", "consumer.js" ]

