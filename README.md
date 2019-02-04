# Small Micro Service Mesage Broker System with MASSTRANSİT

## Try to learn how MicroService talking eachother,

**And this repo have 3 different services**,

MakeTransaction Service is create a Entry and send it like IEntry class and Listen Fault<IEntry> class  
Listener is listen Entry's Message and compute what to do with Entry Message
Listener has 2 options ,first one is can accept Entry Message second one Throw exeption
The last one is Father,  
 Father Listening the other 2 service and compute All Entry Messages and compute Accepted Entry Messages
Then print the screen

**U have to Rabbitmq services , or u can use this Docker Container:**

_docker run -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management_

or u can use docker file in the root directory with
_docker build ._
_docker run [Container Id]_
