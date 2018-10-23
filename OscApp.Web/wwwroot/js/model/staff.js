var Bb = require('backbone');

module.exports = Bb.Model.extend({
	idAttribute: 'optimeIndex',
	url: function () {
		return '/api/staff/' + (this.get('optimeIndex') != undefined ? this.get('optimeIndex') : '');
	}
});