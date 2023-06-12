const express = require('express');
const mongoose = require('mongoose'); // https://mongoosejs.com/docs/populate.html#population
const { Schema } = mongoose;

const { faker } = require('@faker-js/faker'); // https://fakerjs.dev/guide/usage.html
const uri = "mongodb://localhost:27017/gkb";
const app = express();

const rezeptSchema = Schema({
  _id: Schema.Types.String,
  __v: Schema.Types.Number,
  name: Schema.Types.String,
  erstelldatum: Schema.Types.String,
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

    const amountFakeRezepte = 2;

    for (let i = 0; i < amountFakeRezepte; i++) {
      const fakeRezept = fakeRandomRezept();
      const rezeptDocument = new RezeptModel(fakeRezept);
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
    _id: faker.string.uuid(), // '4136cd0b-d90b-4af7-b485-5d1ded8db252'
    __v: 0,
    name: faker.commerce.productName(), // 'Incredible Soft Gloves'
    erstelldatum: new Date(faker.date.past().toUTCString()).toISOString(), // '2022-07-31T01:33:29.567Z' (ToISO is neccessary, the doc in anytime is a lie)
  };
}
