# Client / Server application showing live stock prices.
Just a small project built in one day to practice. Needs refactoring!

## Client
Client built using C# and WPF utilizing a MVVM architecture. 
I also added a Data Access layer that handles the request/response to the server (message broker).

## Server
Server built using C# and .Net Sockets.
The server is setup as a homebrew message broker having different layers consisting of producers and a broker where it conmmunicates with clients also called consumers.

![alt text](https://github.com/martinloenne/live-stock-prices/blob/master/runningApplication.png)
