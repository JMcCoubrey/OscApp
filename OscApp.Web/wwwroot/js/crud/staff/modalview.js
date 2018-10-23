var AbstractView = require('../abstractmodalview');
var Model = require('../../model/staff');

require('bootstrap-datetimepicker');

var view = AbstractView.extend({
	template: require('./modalview.hbs'),
	model: Model,
	requiredModelProperties: {
		'id': 'Id is required.'
	},
	ui: function () {
		return _.extend({}, AbstractView.prototype.ui, {
			'idInput': '#id-input',
			'descInput': '#desc-input',
			'forenameInput': '#forename-input',
			'surnameInput': '#surname-input',
			'lineManagerInput': '#linemanager-input'
		});
	},
	initialize: function (options) {
		view.__super__.initialize.call(this, options);
	},
	onRender: function () {
		this.ui.allSelects.selectpicker({
			liveSearch: true,
		});
	},
	changeModel: function () {
		this.model.set({
			id: this.ui.idInput.val(),
			description: this.ui.descInput.val(),
			forename: this.ui.forenameInput.val(),
			surname: this.ui.surnameInput.val(),
			lineManager: this.ui.lineManagerInput.val()
		});

		view.__super__.changeModel.call(this);
	}
});

module.exports = view;