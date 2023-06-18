# Gemeinschaftskochbuch API

## (Layered) Architecture

### Application Layer (Controllers)

Responsible for (no pun intended):

- listening to HTTP Requests and giving HTTP Responses.
- mapping to and from DTOs for HTTP communication.
- delegating work to the Domain Layer.

Normally, the flow of a method within a Controller will look like this:

#### Example HttpPost

1. Receive HttpRequest (body with DTO)
2. Validate the DTO using something very similar to the Specification Pattern [Specification Pattern Wikipedia](Specification Pattern)
   - If invalid, return the "error messages" in a `BadRequest` (not sure if this is the correct one to use) via the [Specifications (Eric Evans and Martin Fowler)](https://martinfowler.com/apsupp/spec.pdf)
3. Delegate the work to the lower layers (Domain Layer and Infrastructure)
4. Return the newly created model in a HttpResponse (body)

### Domain Layer

I'm not going to explain what a Domain is. It doesn't matter anyways since this is only a simple CRUD application.

If you want to learn more about DDD (Domain-Driven Design), there is a short summary from 2015 called [Domain-Driven Design Reference - Definitions and Pattern Summaries](https://www.domainlanguage.com/wp-content/uploads/2016/05/DDD_Reference_2015-03.pdf), or you can read the ['big blue book' by Eric Evans](https://www.amazon.de/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215/ref=sr_1_1?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&crid=13DX941RWJJ3&keywords=Domain+Driven+Design&qid=1686647527&sprefix=domain+driven+desig%2Caps%2C109&sr=8-1) or the ['big red book' by Vaughn Vernon](https://www.amazon.de/Implementing-Domain-Driven-Design-Vaughn-Vernon/dp/0321834577/ref=pd_bxgy_img_sccl_2/258-2676143-5713501?pd_rd_w=8pPzi&content-id=amzn1.sym.1fd66f59-86e9-493d-ae93-3b66d16d3ee0&pf_rd_p=1fd66f59-86e9-493d-ae93-3b66d16d3ee0&pf_rd_r=9FWZ16J9515FK36S1DMR&pd_rd_wg=6Q5s0&pd_rd_r=feefa4a8-a1aa-4575-b659-e51200c7b5a6&pd_rd_i=0321834577&psc=1). There is also an (unfinished) GitHub example [IDDD_Samples (Vaughn Vernon using .NET)](https://github.com/VaughnVernon/IDDD_Samples_NET/tree/master) and the [Java Version of it](https://github.com/VaughnVernon/IDDD_Samples).

> Essentially, any Business Logic remains here. This is what the actual application 'is about'.

Also, the Domain is responsible for every data-related problem.
Meaning, IDs and dates will all be generated here, not in the frontend, nor the database. (If think this has obvious reasons and advantages I don't need to point out)

### Infrastructure Layer

Third-party Libraries/Frameworks/etc. will have their `ConcreteImplementation` here.

The ignorant Interface belongs to the Domain Layer.

## Common

This "Library" (or folder in this case :D) is literally just a shared module.

## Development

When you run `dotnet run`, you can navigate to `http://localhost:5263/Swagger` to test the API without UI.

## Deployment

Replace the `appsettings.template.json` file with our own `appsettings.json` file in your local development environment.
It contains sensitive informations, like connections strings.

## ToDo

- ~~Add Unit Tests.~~
- Make use of Mocking.
- Implement `UnitOfWork` for transactions (And maybe other things).
- Add authentication.
- Add email service (Firebase is for free, I think).
- Maybe use Domain Events for some things, like sending emails, but the Application Layer is sufficient for this.
- For more, [see Requirements](../README.md#requirements)

## Docker

run `docker run -it --rm -p 5263:80 -e ASPNETCORE_ENVIRONMENT=Development imagename`
`-it` will keep the terminal attached to view the logs.
`--rm` will remove the container, when the terminal is gracefully terminated.
`-e` is to set environment varibales.
`5263` is the port you need to enter in your browser (the port your container is listening to).
`80` is the port your container will map your request to within.

> Since your local version of mongodb is not within the container, the api will not really work, but you can see the swagger ui. Else if you omit the environment varibale you will reviece a 404 Not Found.
