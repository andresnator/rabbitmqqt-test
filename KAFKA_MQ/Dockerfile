FROM openjdk
RUN cd /opt && curl -OL https://downloads.apache.org/kafka/2.5.0/kafka_2.13-2.5.0.tgz && tar -zxvf kafka_2.13-2.5.0.tgz && rm kafka_2.13-2.5.0.tgz
WORKDIR /opt/kafka_2.13-2.5.0
COPY start.sh .
RUN chmod a+x start.sh
CMD [ "./start.sh" ]