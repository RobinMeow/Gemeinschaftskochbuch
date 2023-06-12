# Seeding MongoDB Locally for development

The use will wipe the database called `gkb` and and then build it back up with new (random) data. (actually it doesnt wipe the data yet, I have used the mongosh for this so far)

1. run `npm install`
2. run `node server.js`
3. close the terminal again when it is fininshed

> The Models need to be kept up2date, but since I'm using only one source control, I can be sneaky and use relative pathing and generate models based on the angular application DTOs to automate the seeding process.
