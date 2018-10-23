var Handlebars = require('handlebars');

Handlebars.registerHelper('ifEquals', function (arg1, arg2, options) {
	return (arg1 === arg2) ? options.fn(this) : options.inverse(this);
});

Handlebars.registerHelper('ifNotEquals', function (arg1, arg2, options) {
	return (arg1 !== arg2) ? options.fn(this) : options.inverse(this);
});

Handlebars.registerHelper('IsMatch', function (shiftIds, id, options) {

	if (shiftIds.indexOf(id) > -1) {
		return options.fn(this);
	}
	return options.inverse(this);
});

Handlebars.registerHelper('readOnlyIf', function (condition) {
	return condition ? new Handlebars.SafeString('readonly="readonly"') : '';
});

Handlebars.registerHelper('disableIf', function (condition) {
	return condition ? 'disabled' : '';
});