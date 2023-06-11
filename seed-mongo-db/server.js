const express = require('express');
const mongoose = require('mongoose');

const uri = "mongodb://localhost:27017";
const app = express();

async function connectedToMongoDo() {
  await mongoose.connect(uri)
    .then((client) => {
      console.log("Connected to MongoDB");

    }).catch(err => console.error(err));
}

connectedToMongoDo();

const port = 8418;
app.listen(port, () => {
  console.log("Server started on port " + port);
});
