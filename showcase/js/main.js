function IsIE() {
	return navigator.msMaxTouchPoints !== void 0;
}

function NavigateTo(url, addc, button)
{
	if(button)
		$('body').css({"animation": "ToBack 0.25s"});
	
	var curUrl = window.location.href;
	
	if(addc)
	{
		if(curUrl.indexOf("?c=") > -1)
		{
			url += "?c=" + curUrl.split("=")[1];
		}

		if(url.indexOf("&t") > -1)
		{
			url = url.split("&t")[0];
		}
	}	
	
	window.location.href = url;
}

function ChangeColor(color) 
{
	var id = 0;
	
	if(color === "white")
		id = 0;
	else if (color === "black")
		id = 1;
	else if (color === "red")
		id = 2;
	else if (color === "green")
		id = 3;
	else if (color === "yellow")
		id = 4;
	else if (color === "blue")
		id = 5;
	else
		id = 0;
	
	var navUrl = "";
	var curUrl = window.location.href;
	
	if(curUrl.indexOf("?c=") > -1)
		navUrl = curUrl.split("?c=")[0];
	else
		navUrl = curUrl;
	
	navUrl += "?c=" + id + "&t=true";
	
	$("body").css({"animation": "SlideOut 0.5s"});
	
	NavigateTo(navUrl, false, false);
}

$(window).load(function() {
	if(window.location.href.indexOf("&t=true") > -1)
		$("body").css({"animation": "SlideToFront 0.5s"});
	else
		$("body").css({"animation": "ToFront 0.5s"});
});

$(document).ready(function() {
	if(IsIE())
		NavigateTo("../errors/old.php", true, false);
	
	$('.navbutton').each(function(index) {
		$(this).click(function() { NavigateTo("./" + $(this).attr('id') + ".php", true, true); });
	});
	
	$('.rectangle').each(function(index) {
		$(this).click(function() { ChangeColor($(this).attr('id')); });
	});
});