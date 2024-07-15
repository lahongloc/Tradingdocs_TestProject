const mongoose = require("mongoose");
const AutoIncrement = require("mongoose-sequence")(mongoose);
const Schema = mongoose.Schema;
const bcrypt = require("bcryptjs");

const postSchema = new Schema(
	{
		_id: Number,
		userId: {
			type: mongoose.Schema.Types.ObjectId,
			ref: "User",
			required: true,
		},
		document: {
			type: mongoose.Schema.Types.ObjectId,
			ref: "Documentation",
			required: true,
		},
		postType: {
			type: mongoose.Schema.Types.ObjectId,
			ref: "PostType",
			required: true,
		},
		price: { type: Number, required: false },
		image: { type: String, required: false },
		description: { type: String, required: true },
		place: {
			type: mongoose.Schema.Types.ObjectId,
			ref: "Place",
			required: true,
		},
		isNegotiable: { type: Boolean, required: true },
		quantity: { type: Number, required: true },
	},
	{ _id: false, timestamps: true },
);

postSchema.plugin(AutoIncrement, { id: "post_seq", inc_field: "_id" });

module.exports = mongoose.model("Post", postSchema);
