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

## Architecture

- angular for Front-End
- ASP.NET Core webapi .NET 7 for Back-End
- Firebase Auth
- Firebase for Hosting
- MongoDB as persistence mechanism

## Development

API:

1. navigate to your root directory. 'Gemeinschaftskochbuch'
2. Start local web server: 'dotnet run --project .\api\'

UI:

1. navigate to your ui directory. 'Gemeinschaftskochbuch/ui'
2. Start Locale Dev Server for angular `ng serve`

> apprently there is more than one way to pass a path to ng serve, but it seemed to change often, so I didnt bother reading into it, as it is likely to change again.

## Debug

In order to debug, launch.json and task.json had to be created at root level for the Run and Debug VSCode extension tab to work.
Mostly, it simply redirects to the tasks wihtin api and ui.
I opened the api folder in its own vscode window, to get the pop up which asks to generate launch and tasks files. (you can also go to the debug tab and press create files, but this will also only work, when api is your root folder in the workspace).

## Custom Tasks

There is one Task, you can execute, which starts `ng serve` and `dotnet run` in separate terminals.
Its name is "Run Back + Front End"
