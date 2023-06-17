# Development

To start the Back and Front End, you can use the VSCode Run Task `Run Back + Front End`.

It will `ng serve` the `ui` (Angular) app and `dotnet run` the `webapi` app.

## MongoDB Local

Instructions:

1. Install [MongoDB Community Server](https://www.mongodb.com/try/download/community).
2. [Optionally] install MongoDB shell(mongosh).
3. [Optionally] install Command Line Database Tools. (I think this one is not optional if you want to be able to deploy)
4. [Optionally] install Atlas CLI.

> Don't forget to add the `bin` folders to your PATH variable.

## The Set-Up was done as follows (Using a Windows 10 OS)

1. Create a folder named "Gemeinschaftskochbuch".
2. Open a terminal in that folder (using versions Angular CLI 15.2.4 and dotnet 7.0.302).
3. Run `dotnet new webapi -n api`.
    - Run `cd api ; dotnet new gitignore ; cd ..` (you can exclude the `cd`s, but the gitignore-file has to be in the `api` folder).
4. Run `ng new ui` and select SCSS for styles and use Angular Routing.
    - `ng new` initializes a git repository. **Delete the `.git` folder.**
    - Keep the `.gitignore` file. It works recursively.
5. Run `git init`.

> When you use VSCode, all (not ignored) files should now be highlighted in green. (If you don't delete the `.git` folder in the `ui` folder, it will not highlight them and not track them.)

6. Run `git commit -a -m 'Init'`.
    - Then, I created the repository on GitHub, and GitHub showed me 3 commands to execute (add remote as origin, push initial commit, create main branch).

Not part of the setup, but my preference:

- Disallow implicit usings and change code accordingly by adding the necessary `usings`.

## Seeding

Use the Run Task or run `node .\seed-mongo-do\server.js`.

It will create a few Recipes (locally only) with the use of `mongoose` and `faker.js`.

> Apparently, there is a problem with Node version 17+ (For seeding). I fixed it, but maybe you run into issues when using lower versions now.

## Cloning this repository

1. Run `git clone http-url` in your desired location.
2. Open it in VS Code: `cd Gemeinschaftskochbuch`, `code .`
3. Open up a terminal.
4. Navigate to `api` with `cd api` and build the project to get rid of missing dependency errors: `dotnet build`.
5. Do the same for `api-tests` with `cd ../api-tests` and `dotnet build`.
6. Navigate to `ui` with `cd ../ui` and install all the packages with `npm install` (All errors should be gone now).
7. Navigate to `seed-mongo-db` with `cd ../seed-mongo-db` and `npm install` (optionally run the seeding => Run Task).
8. If you haven't done so yet, install MongoDB server. Otherwise, you are likely to get HTTP errors when you `ng serve`.
