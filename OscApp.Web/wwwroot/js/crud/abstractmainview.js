require('../common');
require('../../css/_crud.scss');
var Mn = require('backbone.marionette');

module.exports = Mn.View.extend({
	el: '#main-content',
	ui: {
		'addBtn': '#add-btn',
		'tbody': 'tbody',
		'editBtn': '.edit-btn',
		'deleteBtn': '.delete-btn',
		'countBadge': 'h2 span.badge',
		'row': 'tbody row'
	},
	events: {
		'click @ui.addBtn': 'openAddModal',
		'click @ui.editBtn': 'openEditModal',
		'click @ui.deleteBtn': 'deleteItem'
	},
	openAddModal: function () {
		var modalView = new this.modalView();

		this.listenToOnce(modalView, 'saved', function (model) {
			var rowView = new this.rowView({ model: model });

			// update the count badge
			this.ui.countBadge.html(parseInt(this.ui.countBadge.html()) + 1);

			// put the new row at the top of the table
			this.ui.tbody.prepend(rowView.$el);
			rowView.render();
		});
	},
	openEditModal: function (e) {
		var modalView = new this.modalView({ mode: 'edit', optimeIndex: $(e.currentTarget).attr('data-id') });
		this.listenToOnce(modalView, 'saved', function (model) {
			var rowView = new this.rowView({ model: model });

			// replace the row that already exists for this entity with the new one
			this.$('tr[data-id="' + model.get('optimeIndex') + '"]').replaceWith(rowView.$el);
		});
	},
	deleteItem: function (e) {
		if (confirm('Are you sure you want to delete this item?')) {
			new this.model({ optimeIndex: $(e.currentTarget).attr('data-id') }).destroy({
				success: function () {

					// update the count badge
					this.ui.countBadge.html(parseInt(this.ui.countBadge.html()) - 1);
					
					// remove the relevant row from the table
					this.$('tr[data-id="' + $(e.currentTarget).attr('data-id') + '"]').remove();
				}.bind(this)
			});
		}
	}
});