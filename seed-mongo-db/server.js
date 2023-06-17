const express = require('express');
const mongoose = require('mongoose'); // https://mongoosejs.com/docs/populate.html#population
const { Schema } = mongoose;

const { faker } = require('@faker-js/faker'); // https://fakerjs.dev/guide/usage.html
const uri = "mongodb://localhost:27017/gkb";
const app = express();

const recipeSchema = Schema({
  _id: Schema.Types.String,
  __v: Schema.Types.Number,
  createdAt: Schema.Types.String,
  name: Schema.Types.String,
});

const collectionName = 'recipes';
const RecipeModel = mongoose.model('Recipe', recipeSchema, collectionName);

function LOG_SUCCESS(text) {
  console.log('\u001b[1;32m' + text + '\u001b[0m'); // set to green {text} than back to default
  // How to use colors: https://stackoverflow.com/questions/43528123/visual-studio-code-debug-console-colors
}

async function seed() {
  try {
    await mongoose.connect(uri);
    console.log("Connected to MongoDB");

    await RecipeModel.createCollection();

    const amountFakeRecipes = 55;

    for (let i = 0; i < amountFakeRecipes; i++) {
      const fakeRecipe = fakeRandomRecipe();
      const recipeDocument = new RecipeModel(fakeRecipe);
      await recipeDocument.save();
    }

    console.log(` - created ${amountFakeRecipes} fake ${collectionName}`);

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

function fakeRandomRecipe() {
  return {
    _id: faker.string.uuid(), // '4136cd0b-d90b-4af7-b485-5d1ded8db252'
    __v: 0,
    createdAt: new Date(faker.date.past().toUTCString()).toISOString(), // '2022-07-31T01:33:29.567Z' (ToISO is neccessary, the doc in 'past()' is a lie)
    name: faker.commerce.productName(), // 'Incredible Soft Gloves'
  };
}
