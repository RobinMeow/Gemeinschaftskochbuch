# Gemeinschaftskochbuch API

## (Layered) Architecture

### Application Layer (Controllers)

Responsible for (no pun intended):

- listening to HTTP Requests and giving a HTTP Responses.
- mapping to and from DTOs for HTTP communication.
- delegating work to the Domain Layer.

Normally the flow of a Method within a Controller will look like this:

#### Example HttpPost

1. Recieve HttpRequest (body with DTO)
2. Validates the DTO using the something very similar to the Specification Pattern [Specification Pattern Wikipedia](Specification Pattern)
    - If invalid returns the "error messages" in a `BadRequest`(not sure if this is the correct one to use) via the [Specifications (Eric Evans and Martin Fowler)](https://martinfowler.com/apsupp/spec.pdf)
3. Delegates the work to the lower layers (Domain Layer and Infrastructure)
4. Returns the newly created model in a HttpResponse (body)

### Domain Layer

I'm not going to explain what a Domain is. It doesn't matter anyways, since this is only a simple CRUD application.

If you want to learn more about DDD (Domain Driven Design) there is a short summary from 2015 called [Domain-Driven Design Reference - Definitioons and Pattern Summaries](https://www.domainlanguage.com/wp-content/uploads/2016/05/DDD_Reference_2015-03.pdf)
or you can read the ['big blue book' by Eric Evans](https://www.amazon.de/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215/ref=sr_1_1?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&crid=13DX941RWJJ3&keywords=Domain+Driven+Design&qid=1686647527&sprefix=domain+driven+desig%2Caps%2C109&sr=8-1) or the ['big red book' vy Vaughn Vernon](https://www.amazon.de/Implementing-Domain-Driven-Design-Vaughn-Vernon/dp/0321834577/ref=pd_bxgy_img_sccl_2/258-2676143-5713501?pd_rd_w=8pPzi&content-id=amzn1.sym.1fd66f59-86e9-493d-ae93-3b66d16d3ee0&pf_rd_p=1fd66f59-86e9-493d-ae93-3b66d16d3ee0&pf_rd_r=9FWZ16J9515FK36S1DMR&pd_rd_wg=6Q5s0&pd_rd_r=feefa4a8-a1aa-4575-b659-e51200c7b5a6&pd_rd_i=0321834577&psc=1). There is also an (unfinished) GitHub example [IDDD_Samples (Vaughn Vernon using .NET)](https://github.com/VaughnVernon/IDDD_Samples_NET/tree/master) and the [Java Version of it](https://github.com/VaughnVernon/IDDD_Samples).

> Essentially any Business Logic remains here. This is what the actual application 'is about'.

Also the Domain is responsible for every data-related problems.
Meaning, IDs and dates will all be generated here, not in the front end, nor the database. (If think this has obvious reasons and advantages I dont need to point out)

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
