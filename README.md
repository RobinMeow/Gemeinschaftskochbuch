# Gemeinschaftskochbuch

Welcome to Gemeinschaftskochbuch, a community cookbook project where users can share and explore recipes.
"Community" as in family and maybe friends, not worldwide.

## Navigation (not ToC)

- [Development Notes](development-notes.md)
- [api README](api/README.md)
- [api-tests README](api-tests/README.md)
- [ui README](ui/README.md)
- [seed local MongoDB with fake data README](seed-mongo-db/README.md)

## Requirements

- The app should be able to recalculate any quantities based on the desired amount of dishes, selected by the user.
- A `Recipe` is considered "unique" based on the Author + RecipeName combination. (Composite Key)
- Ingredients (their Name) should be reused where possible to avoid different spellings for the same thing. But there should be no additional work for the user.
  - Example: When "adding an `Ingredient` to a `Recipe`, it should have suggestions of existing Ingredients and create a new one automatically if you type in a not yet used `Ingredient`.
  - In a later version, it might be desired to have a UI that allows you to merge two `Ingredients` so all references point to the same `Ingredient`. (Merge `Apple` and `apples` so all point to `apples` and delete `Apple` as an `Ingredient`)
- You should be able to "categorize" or "tag" a `Recipe` for a better search filter experience.
- A `Recipe` can optionally contain multiple "descriptions", usually used for cooking instructions.
  - It should also be able to contain multiple `Note`s, which should be highlighted in the same way.
    - Example: *InstructionA and InstructionB can be combined if you own an instant pot.*
  - It's probably desired to have the "description"s and `Note`s drag 'n' dropable for reordering the display. (Maybe even the `Ingredients`)
- The frontend does only need to support "German" as the language. No i18n necessary.
  - *Multiple languages would cause trouble anyways because our culinary skills are multicultural.*
- Since the app should **not** be viewable worldwide for everyone, it should have a "key" for the group of people who want to access it because it should be on the internet.
- Every user should be able to exclude Recipes (in search filtering) based on its `Author`. This setting may not be visible to others.
- Some sort of Like/Dislike system for the recipes. But only the positive likes should be public; dislikes are for private use only.

> There are other Requirements written down in Confluence. Still need to put them here. Big sorry.

---
> I have a personal requirement for this project: I intend to make it open source, enabling easy deployment for multiple groups of people.

---

## Architecture

- Front-End Angular using standalone "stuff" [ui/README](ui/README.md)
- Back-End dotnet webapi (ASP.NET Core with .NET 7) [api/README](api/README.md)
- UnitTests for Back-End [api-tests/README](api-tests/README.md)
- Persistence Mechanism is/will be implemented using MongoDB.
- Even though they share the same git repository (for my personal convenience), they are decoupled, and it should be kept this way, with the exception of the seeding express app. It may contain references (relative paths) to the TypeScript models if this helps fully automate the seeding.

## Others

I wanted to have everything in a single git repository and a single vscode workspace for ease of development. (As it is easy to split them up anyways since they do not have dependencies on each other.)

Also, take into consideration that I have a lot of information put into source code (and README) which I normally wouldn't put there for production code.

> Partly, my motivation is creating the app so it can solve the "problem" for my family and friends.
>
> Another motivation is just me wanting to learn a whole bunch of up-to-date technologies I have not touched yet. So I want to implement the whole infrastructure for learning purposes. Also, I don't want to get stuck in the middle of my project, so I get the "answers to the unknowns early" before I start with the actual development.
>
> This will include deployment, meaning I want an "MVP" deployable version with only a Recipename and a single Angular component for create and read.
>
> I might even go so far as to read into build pipelines (using Azure or Bitbucket, I think? idk, never done, but I feel like the need arises slowly. But it needs to be free of charge like everything else in this project :D )

## Contributions

I don't accept contributions yet.
> Will do, as soon as I decide which copyleft License to use.
