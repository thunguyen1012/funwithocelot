Demonstration for async/sync services' communication

1. Sync
Order
Product
Payment

2. Async
Logging

3. API Gateway and console app
It's just a demonstration for services management and consume them.

4. Ref
https://github.com/ardalis/CleanArchitecture

TODO:
- Mediator

Command -> events -> publish to kafka
		-> entity -> save to db

Event -> Command
      -> entity -> save to db