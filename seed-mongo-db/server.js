const express = require('express');
const mongoose = require('mongoose');
const { Schema } = mongoose;

const { faker } = require('@faker-js/faker');
const uri = "mongodb://localhost:27017/gkb";
const app = express();

async function seed() {
  try {
    await mongoose.connect(uri);
    console.log("Connected to MongoDB");

    const rezeptSchema = Schema({
      id: Schema.Types.ObjectId,
      name: String,
      erstelldatum: String,
    });

    const RezeptModel = mongoose.model('Rezept', rezeptSchema, 'rezepte'); // appends an s ...
    await RezeptModel.createCollection();

    for (let i = 0; i < 10; i++) {
      const rezeptDocument = new RezeptModel(fakeRandomRezept());
      await rezeptDocument.save();
      console.log("created fake rezept.");
    }
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
    name: faker.image.avatar(),
    erstelldatum: faker.date.anytime(),
  };
}
