function cleanwindow(theURL,winName,features) {
	var newWin = window.open(theURL,winName,features);
	newWin.focus();
}