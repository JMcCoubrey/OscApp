require('../common');

$(window).ready(function () {

	var path = window.location.pathname;
	if (path === '/Manage/Index') {
		$('li[data-loc="index"]').addClass('active');
	} else if (path === '/Manage/ChangePassword') {
		$('li[data-loc="change-password"]').addClass('active');
	}

});