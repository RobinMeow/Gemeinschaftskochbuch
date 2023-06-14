# Gemeinschaftskochbuch

Welcome to Gemeinschaftskochbuch, a community cookbook project where users can share and explore recipes.
"Community" as in, family and maybe friends, not world wide.

## Navigation (not ToC)

- [Development Notes](development-notes.md)
- [api README](api/README.md)
- [api-tests README](api-tests/README.md)
- [ui README](ui/README.md)
- [seed local mongoDB with fake data README](seed-mongo-db/README.md)

## Requirements

- The app should be able to recalculate any quantities based on desired amount of dishes, selected by the user.
- A `Recipe` is considered "uniqie" based on the Auther + RecipeName combination. (Composite Key)
- Ingredients (their Name) should be reused where possible to avoid different spelling for the same thing. But there should be no additional work for the user.
  - Exmaple: When "adding a `Ingredient` to a `Recipe` it should have sugesstions of existing Ingredients, and create a new one automatically if you type in a not yet used `Ingredient`.
  - In a later version it might be desired to have a UI, which allows you to merge two `Ingredients` so all references point to the same `Ingredient`. (Merge `Apple` and `apples` so all point to `apples` and delete `Apple` as `Ingredient`)
- You should be able to "categorize" or "tag" a `Recipe` for a better search filter experience.
- A `Recipe` can optionally contain multiple "descriptions", usually used for cooking instructions.
  - It should also be able to contain multiple `Note`s which should be highlighted in same way.
    - Example: *InstructionA and InstructionB can be combined if you own an instant pot*
  - Its probably desired to have the "description"s and `Note`s drag 'n drop'able for reordering the display. (Maybe even the `Ingredients`)
- The the front-end does only need to support "german" as language. No i18n necessary.
  - *Multiple languages would cause trouble anyways, because our culinary skills are multi cultured*
- Since the app should **not** be viewable world wide for everyone, it should have a "key" for the group of people who want to access it, because it should be on the internet.
- Every user should be able to exclude Recipes (in search filtering) based on its `Author`. This setting may not be visible to others.
- Some sort of Like/Dislike system for the recipes. But only the postive likes should be public, dislikes are for private use only.

> There are other Requirements written down in the confluence. Still need to put them here. Big sorry.

---
> My personal requirement: I would like to make this project open source, so I can be deployed easily more than once, for different group's of people.

---

## Architecture

- Front-End Angular using standalone "stuff" [ui/README](ui/README.md)
- Back-End dotnet webapi (ASP.NET Core with .NET 7) [api/README](api/README.md)
- UnitTests for Back-End [api-tests/README](api-tests/README.md)
- Persistence Mechanism is/will be implemented using MongoDB.
- Even tho they share the same git repository (for my personal convienience), they are decoupled, and it should be kept this way, with the exception of the seeding express app. It may contain references (relative paths) to the TypeScript models, if this helps fully automating the seeding.

## Others

I wanted to have everything in a single git repository and a single vscode workspace for easy of development. (As it is easy to split them up anyways since they do not have dependencies to each other.)

Also take into consideration, that I have alot of information put into source code (and README) which I normally wouldnt put there for production code.

> partly my Motivation is creating the app so it can solve the "problem" for my family and friends.
>
> other Motivation is just me wanting to learn whole bunch of up2date technologies I have not touched yet. So I want to implement the whole infrastructure, for learning purpose. Also I dont want to get stuck in the middle of my project, so I get the "answers the unkonws early" before I start with the actual development.
>
> This will include deployment, meaning I want a "MVP" deployable version with only a Recipename and a single angular componentfor create and read.
>
> I might even go so far, and read into build pipelines (using Azur or Bitbucket I think? idk, never done, but I feel like the need arises slowly. But it needs to be free of charge like everything else in this project :D )

## Contributions

I don't take contributions, yet.
> Will do, as soon as I decided which copyleft License to use.
