const express = require('express');
const mongoose = require('mongoose'); // https://mongoosejs.com/docs/populate.html#population
const { Schema } = mongoose;

const { faker } = require('@faker-js/faker'); // https://fakerjs.dev/guide/usage.html
const uri = "mongodb://localhost:27017/gkb";
const app = express();

const rezeptSchema = Schema({
  id: Schema.Types.ObjectId,
  name: String,
  erstelldatum: String,
});

const collectionName = 'rezepte';
const RezeptModel = mongoose.model('Rezept', rezeptSchema, collectionName);

function LOG_SUCCESS(text) {
  console.log('\u001b[1;32m' + text + '\u001b[0m'); // set to green {text} than back to default
  // How to use colors: https://stackoverflow.com/questions/43528123/visual-studio-code-debug-console-colors
}

async function seed() {
  try {
    await mongoose.connect(uri);
    console.log("Connected to MongoDB");

    await RezeptModel.createCollection();

    const amountFakeRezepte = 100;

    for (let i = 0; i < amountFakeRezepte; i++) {
      const rezeptDocument = new RezeptModel(fakeRandomRezept());
      await rezeptDocument.save();
    }

    console.log(` - created ${amountFakeRezepte} fake ${collectionName}`);

    LOG_SUCCESS("SEEDING COMPLETED. Press CTRL + C to close the stop terminal and db connection.");
    } catch (error) {
    console.error(error)
  }
}

seed();

const port = 8418;
app.listen(port, () => {
  console.log("Server started on port " + port);
});

function fakeRandomRezept() {
  return {
    // id is genrated by mongo db
    name: faker.image.avatar(),
    erstelldatum: faker.date.anytime().toISOString(),
  };
}
