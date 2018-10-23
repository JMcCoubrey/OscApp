var Mn = require('backbone.marionette');

require('../common');
require('../../css/_home.scss');
require('bootstrap-select');

$(document).ready(function () {

	var view = Mn.View.extend({
		el: '#main-content',
		ui: {
		},
		events: {
		},
		initialize: function () {
			$('.navbar').css('background', 'transparent');
		}
	});

	new view();
});