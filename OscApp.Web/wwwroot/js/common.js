// global vendor styles
import 'bootstrap/dist/css/bootstrap.css';
import 'protip/css/protip.css';
import 'osc-style/src/_settings.scss';
import 'osc-style/src/_objects.text.scss';
import 'osc-style/src/_components.text.scss';
import 'bootstrap-daterangepicker/daterangepicker.css';
import 'bootstrap-select/dist/css/bootstrap-select.css';
import 'eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css';
import 'toastr/build/toastr.css';
import '../css/_base.scss';
import '../css/_components.scss';
import '../css/_tools.scss';

// global vendor scripts
import 'bootstrap';
import 'bootstrap-select';
import 'bootstrap-daterangepicker';
import toastr from 'toastr';
import $ from 'jquery';
import Bb from 'backbone';
import Mn from 'backbone.marionette';
import Handlebars from 'handlebars';
import './helpers/handlebars-helpers';

toastr.options.positionClass = 'toast-bottom-right';
var save = Bb.Model.prototype.save;
var destroy = Bb.Model.prototype.destroy;
Bb.Model.prototype.save = function(args, options) {
	options = options || {};

	this.listenToOnce(this, 'sync', function() {
		toastr.success(options.successMessage || 'Success.');
	});

	this.listenToOnce(this, 'error', function() {
		toastr.error(options.errorMessage || 'Error.');
	});

	return save.call(this, args, options);
};
Bb.Model.prototype.destroy = function(options) {
	options = options || {};

	this.listenToOnce(this, 'destroy', function() {
		toastr.success(options.successMessage || 'Success.');
	});

	this.listenToOnce(this, 'error', function() {
		toastr.error(options.errorMessage || 'Error.');
	});

	return destroy.call(this, options);
};

require('regions-extras').register({
	Handlebars: Handlebars,
	Marionette: Mn
});


$(document).ready(function() {
	// Side nav children object toggling
	$('#data-nav-item').on('click', function() {
		$('.side-nav-child-items').slideToggle();
	});
});
