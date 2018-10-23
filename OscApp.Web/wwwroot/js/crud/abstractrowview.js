var Mn = require('backbone.marionette');

module.exports = Mn.View.extend({
	tagName: 'tr',
	className: 'new',
	attributes: function () {
		return {
			'data-id': this.model.get('optimeIndex')
		};
	},
	initialize: function () {
		this.render();
	}
});