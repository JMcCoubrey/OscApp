var Bb = require('backbone');

// Collection of grid view shifts
module.exports = Bb.Collection.extend({
	url: function () {
		return '../api/staff/';
	}
});