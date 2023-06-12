# Gemeinschaftskochbuch API

## (Layered) Architecture

### Application Layer (Controllers)

Responsible for (no pun intended):

- listening to HTTP Requests and giving a HTTP Responses.
- mapping to and from DTOs for HTTP communication.
- delegating work to the Domain Layer.

### Domain Layer

I'm not going to explain what a Domain is. It doesn't matter anyways, since this is only a simple CRUD application.

> Any Business Logic remains here.

### Infrastructure Layer

Thrid Party Libraries/Frameworks/etc. will have their `ConcreteImplementation` here.

The ignorant Interface belongs to the Domain Layer.

## Common

This "Library" ( or folder in this case :D ) is literally just a shared module.

## Development

When you run `dotnet run` you can navigate to `http://localhost:5263/Swagger` to test the API without UI.

## ToDo

- Add Unit Tests.
- make use of Mocking.
- implement `UnitOfWork` for transactions (And may be other things).
- add authentification.
- add email service (firebase is for free I think).
- MayBe use Domain Events for some things, like sending emails, but the Application Layer is sufficient for this.
- for more [see Requirements](../README.md#requirements)
