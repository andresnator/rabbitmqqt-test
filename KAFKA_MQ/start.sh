#!/bin/bash
./bin/zookeeper-server-start.sh config/zookeeper.properties &
sleep 7
./bin/kafka-server-start.sh config/server.properties