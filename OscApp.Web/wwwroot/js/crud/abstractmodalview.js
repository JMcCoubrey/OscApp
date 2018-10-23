var Mn = require('backbone.marionette');
require('bootstrap-datetimepicker');

var view = Mn.View.extend({
	className: 'modal',
	ui: {
		'allInputs': 'input',
		'allSelects': 'select',
		'errorContainer': '#error-container',
		'saveBtn': '#save-btn'
	},
	events: {
		'change @ui.allInputs': 'changeModel',
		'hide.bs.select @ui.allSelects': 'changeModel',
		'click @ui.saveBtn': 'save'
	},
	initialize: function (options) {
		this.model = new this.model();

		this.mode = options.mode;
		if (options.mode == 'edit') {
			this.model.set('optimeIndex', options.optimeIndex);
			this.listenToOnce(this.model, 'sync', this.onSync);
			this.model.fetch();
		}

		this.listenTo(this.model, 'change', this.render);
		this.show();
	},
	onSync: function () {
		this.render();
	},
	show: function () {
		this.render();
		this.$el.modal('show');
	},
	showError: function (message) {
		this.ui.errorContainer.show();
		this.ui.errorContainer.html(message);
	},
	changeModel: function () {
		for (var key in this.requiredModelProperties) {
			if (!this.model.get(key)) {
				this.showError(this.requiredModelProperties[key]);
				this.ui.saveBtn.prop('disabled', true);
				return;
			} else {
				this.ui.errorContainer.hide();
				this.ui.saveBtn.prop('disabled', false);
			}
		}
	},
	save: function () {
		this.model.save(null, {
			success: function () {
				this.trigger('saved', this.model);
				this.$el.modal('hide');
			}.bind(this),
			error: function () {

			}
		});
	}
});

module.exports = view;