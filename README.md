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

## Architecture: 

- angular for Front-End 
- .Net 8 for Back-End 
- Google Auth for Accounts (which I can implement using firebase afaik)
- Firebase for Hosting 
- Firestore as Database (tho Im not decided on the database yet, but its needs to be free of charge)

## package-lock.json 
the top level file was created by ng new, for whatever reason .. (Ill check if it is used somewhere, than Ill delete it most likely)
