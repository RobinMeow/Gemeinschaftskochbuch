# Gemeinschaftskochbuch

Set-Up was done as follows (Using a Windows 10 OS):

1. make a folder named "Gemeinschaftskochbuch"
2. open a terminal in that folder (using versions angular CLI 15.2.4 and dotnet 7.0.302)
3. run `dotnet new webapi -n api`
    - run `cd ui ; dotnet new gitignore ; cd ..` (you can exclude the `cd`s, but the files needs to be in api/.gitignore)
4. run `ng new ui` select scss and use Angular Routing
    - `ng new` initializes a git repository. Delete the `.git` folder
    - keep the `.gitignore`, it works recursivly
5. run `git init`

> When you use VSCode, all (not ignored) files should now be highlighted in green. (When you dont delete the .git folder in the ui folder, it will not highlight them, and not track them)

6. run `git commit -m"Init"`
    - then I created the repo on GitHub and GitHub showed me 3 commands to execute (add remote as origin, push initial commit, create main branch)

Not part of set-up, but my preference:

- disallow implicit usings and change code accordingly by adding the usings

## Requirements

ToDo: move requirements to here from confluence.

## Architecture

- Front-End Angular using standalone "stuff" [ui/README](ui/README.md)
- Back-End dotnet webapi (ASP.NET Core with .NET 7) [api/README](api/README.md)
- UnitTests for Back-End [api-tests/README](api-tests/README.md)
- Persistence Mechanism is/will be implemented using MongoDB.

## Development

To start Back and Front End, you can use the VSCode Run Task `Run Back + Front End`.

It will `ng serve` the `ui` (angular) app and `dotnet run` the web`api` app.

To get database working, you need to download and install the [MongoDB Community Server](https://www.mongodb.com/try/download/community) (Which should be enough, but I personally installed the mongoshell as well as CmdLine Database Tools and Atlas CLI))

## Others

I wanted to have everything in a single git repository and a single vscode workspace for easy of development. (As it is easy to split them up anyways since they do not have dependencies to each other)

## Seeding

use the Run Task or run `node .\seed-mongo-do\server.js`.

It will create a few Recipes (locally only) with the use of `mongoose` and `faker.js`.
