var Marionette = require('backbone.marionette');
require('../common');
require('../../css/_login.scss');

var LoginView = Marionette.View.extend({
	el: '.container',
});

$(document).ready(function () {
	new LoginView();
});
