const mongoose = require("mongoose");

async function connect() {
	try {
		await mongoose.connect("mongodb://localhost:27017/fatmouse_app", {
			useNewUrlParser: true,
			useUnifiedTopology: true,
		});
		console.log("success!");
	} catch (error) {
		console.log("fail!", error);
	}
}
module.exports = { connect };
