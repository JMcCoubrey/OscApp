var AbstractView = require('../abstractmainview');
var ModalView = require('./modalview');
var RowView = require('./rowview');
var Model = require('../../model/staff');

$(document).ready(function () {
	var StaffView = AbstractView.extend({
		modalView: ModalView,
		rowView: RowView,
		model: Model
	});
	new StaffView();
});