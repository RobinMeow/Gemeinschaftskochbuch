# Seeding MongoDB Locally for Development

The process will wipe the database called `gkb` and rebuild it with new (random) data. (Currently, it doesn't wipe the data)
I have used mongosh for this so far.

Follow these steps:

1. Navigate to `seed-mongo-db` in the terminal
2. Run `npm install`.
3. Run `node server.js`.
4. Close the terminal when it finishes.

> The models need to be kept up-to-date. Since I'm using only one source control. I could be sneaky and use relative pathing to generate models based on the Angular application DTOs, automating the seeding process.
>
> This would create a dependency, which I dont think I mind.
