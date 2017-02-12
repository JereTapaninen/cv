/*
 * Main JavaScript file for my Taitaja 2016 website.
 * (C) Jere Tapaninen 2015
 * All Rights Reserved
 */

/* 
 * Public variables
 */
var lastScroll = 0;
var scrollPosition = 0;
var detachedNavigationBar = false;
var doneScrolling = true;

/*
 * Here we declare our own extension methods
 */
/*
 * Gets whether the element has the given attribute or not.
 * Returns true if it has, otherwise false.
 *
 * By the way, credits to karim97 on stackoverflow!
 * ==> http://stackoverflow.com/a/1318128
 */
$.fn.hasAttr = function(name) {
	return this.attr(name) !== undefined;
}

/*
 * Happens when the user scrolls down or up.
 */
$(window).scroll(function() {
	scrollPosition = $(window).scrollTop();

	$("article").each(function() {
		var curArticle = $(this);

		if (scrollPosition >= Math.floor($(this).offset().top) && scrollPosition <= Math.floor($(this).offset().top + $(this).height()))
		{
			$("nav ul li").each(function() {
				if ($(this).attr('data-link') === curArticle.attr('id'))
				{
					if (!$(this).hasClass("current"))
						$(this).addClass("current");
				}
				else
				{
					if ($(this).hasClass("current"))
						$(this).removeClass("current");
				}
			});
		}
		else
		{
			if (scrollPosition < $("main").offset().top)
			{
				$("nav ul li").each(function() {
					if ($(this).hasClass("current"))
						$(this).removeClass("current");
				});
			}
		}
	});

	lastScroll = scrollPosition;
});

/*
 * Happens when the document is ready.
 * Default initialization function
 */
$(document).ready(function() {
	// Does JS work? Hopefully yes.
	console.info("Hello fellow developer :-)!\nI just wanted to print out that JS works!\n\nDEVELOPERS DEVELOPERS DEVELOPERS DEVELOPERS\n=>https://www.youtube.com/watch?v=KMU0tzLwhbE");

	// Get the current location (URL)
	var _location = $(location).attr('href');

	// Here we check if the user is a debugger and wants to do some of just that
	if (_location.indexOf("#debug-") > -1)
		$(_location.split('-')[1]).addClass("debug");
	else if (_location.indexOf("#debug") > -1)
		$("*").addClass("debug");

	$("tr").each(function() {
		var seats = ($($(this).children()[0]).text() === "Teema" ? $($(this).children()[5]).text() : $($(this).children()[6]).text());

		if (seats !== "Vapaat paikat")
		{
			if (seats > 0)
			{
				$(this).addClass("open").attr('title', "Vapaita paikkoja jäljellä: " + seats);
			}
			else
			{
				$(this).addClass("closed").attr('title', 'Esitys on loppuunmyyty!');
			}
		}
	});

	$("tr.open").click(function() {
		var esitysID = $($(this).children()[0]).text();

		$("#esitys").attr('value', esitysID);
		$("#submit").click();
	});

	$(".galleryimage").click(function() {
		$("#gallerycanvas").css("visibility", "visible");
		$("#gallerycanvas").animate({opacity: 1}, 500, "swing", function() {
		});
		$("#galleryimg").attr('alt', $(this).attr('alt'));
		$("#galleryimg").attr('src', $(this).attr('src'));
	});

	$("#gallerycanvas").click(function() {
		$("#gallerycanvas").animate({opacity: 0}, 500, "swing", function() {
			$(this).css({"visibility": "hidden"});
		});
	});

	// Handle the links in the HTML document
	handleLinks();

	// Initialize the slow update
	onSlowUpdate();
	// Run the slow onUpdate-function every 1 sec
	setInterval(onSlowUpdate, 1000);

	// Run the onUpdate-function every 50 msec
	setInterval(onUpdate, 50);
});

function closeGallery() {
	$("#gallerycanvas").animate({opacity: 0}, 500, "swing", function() {
		$(this).css({"visibility": "hidden"});
	});
}

/*
 * An onUpdate-function that is called every 50msec
 */
function onUpdate()
{
	if (scrollPosition >= Math.round($("main").offset().top) && !detachedNavigationBar)
	{
		$("#navigation").css({"position": "fixed", "top": "0", "left": "0"});

		detachedNavigationBar = true;
	}
	else if (scrollPosition < Math.round($("main").offset().top) && detachedNavigationBar)
	{
		$("#navigation").css("position", "static");

		detachedNavigationBar = false;
	}
}

/*
 * An onUpdate-function that is called once a second
 */
function onSlowUpdate()
{
	// Set the height of the main-element
	$("main").css("min-height", ($("main").children().length * 100) + "%");
}

/*
 * Handles the navigation bar's links' functionality
 */
function handleLinks()
{
	$("nav ul li").each(function() {
		if ($(this).hasAttr('data-link'))
		{
			$(this).click(function() {
				if (doneScrolling !== false)
				{
					var elem = $("#" + $(this).attr('data-link'));
					doneScrolling = false;

					$("html, body").animate({
						scrollTop: elem.offset().top,
						scrollLeft: elem.offset().left
					}, 600, function() {
						doneScrolling = true;
					});
				}
			});
		} 
	});
}