(function ($) {
	"use strict";

/*=============================================
	=    		 Preloader			      =
=============================================*/
function preloader() {
	$('#preloader').delay(0).fadeOut();
};

$(window).on('load', function () {
	preloader();
	mainSlider();
	mainSliderActive();
	wowAnimation();
	tg_title_animation();
});



/*=============================================
	=    		Mobile Menu			      =
=============================================*/
//SubMenu Dropdown Toggle
if ($('.menu-area li.menu-item-has-children ul').length) {
	$('.menu-area .navigation li.menu-item-has-children').append('<div class="dropdown-btn"><span class="fas fa-angle-down"></span></div>');

}

//Mobile Nav Hide Show
if ($('.mobile-menu').length) {

	var mobileMenuContent = $('.menu-area .main-menu').html();
	$('.mobile-menu .menu-box .menu-outer').append(mobileMenuContent);

	//Dropdown Button
	$('.mobile-menu li.menu-item-has-children .dropdown-btn').on('click', function () {
		$(this).toggleClass('open');
		$(this).prev('ul').slideToggle(500);
	});
	//Menu Toggle Btn
	$('.mobile-nav-toggler').on('click', function () {
		$('body').addClass('mobile-menu-visible');
	});

	//Menu Toggle Btn
	$('.menu-backdrop, .mobile-menu .close-btn').on('click', function () {
		$('body').removeClass('mobile-menu-visible');
	});
}

/*=============================================
	=          Active Class               =
=============================================*/
$('.testimonial-item').on('mouseenter', function () {
	$(this).addClass('active').parent().siblings().find('.testimonial-item').removeClass('active');
});


/*=============================================
	=     Menu sticky & Scroll to top      =
=============================================*/
$(window).on('scroll', function () {
	var scroll = $(window).scrollTop();
	if (scroll < 245) {
		$("#sticky-header").removeClass("sticky-menu");
		$('.scroll-to-target').removeClass('open');

	} else {
		$("#sticky-header").addClass("sticky-menu");
		$('.scroll-to-target').addClass('open');
	}
});


/*=============================================
	=    		 Scroll Up  	         =
=============================================*/
if ($('.scroll-to-target').length) {
  $(".scroll-to-target").on('click', function () {
    var target = $(this).attr('data-target');
    // animate
    $('html, body').animate({
      scrollTop: $(target).offset().top
    }, 1000);

  });
}

/*=============================================
=              offCanvas  Menu                =
=============================================*/
$(".menu-trigger").on("click", function () {
	$(".extra-info,.offcanvas-overly").addClass("active");
	return false;
});
$(".menu-close,.offcanvas-overly").on("click", function () {
	$(".extra-info,.offcanvas-overly").removeClass("active");
});

/*=============================================
	=            Header Search            =
=============================================*/
$(".header-search > a").on('click', function () {
	$(".search-popup-wrap").slideToggle();
	return false;
});

$(".search-close").on('click', function () {
	$(".search-popup-wrap").slideUp(500);
});



/*=============================================
	=           services Effect             =
=============================================*/
$('.services-item-two, .choose-item-three').on('mouseenter', function (e) {
	var parentOffset = $(this).offset(),
		relX = e.pageX - parentOffset.left,
		relY = e.pageY - parentOffset.top;
	$(this).find('.shape').css({ top: relY, left: relX })
}).on('mouseout', function (e) {
	var parentOffset = $(this).offset(),
		relX = e.pageX - parentOffset.left,
		relY = e.pageY - parentOffset.top;
	$(this).find('.shape').css({ top: relY, left: relX })
});


/*=============================================
	=          Data Background               =
=============================================*/
$("[data-background]").each(function () {
	$(this).css("background-image", "url(" + $(this).attr("data-background") + ")")
})


/*=============================================
	=    		 Main Slider		      =
=============================================*/
function mainSlider() {
	var BasicSlider = $('.slider-active');
	BasicSlider.on('init', function (e, slick) {
		var $firstAnimatingElements = $('.single-slider:first-child').find('[data-animation]');
		doAnimations($firstAnimatingElements);
	});
	BasicSlider.on('beforeChange', function (e, slick, currentSlide, nextSlide) {
		var $animatingElements = $('.single-slider[data-slick-index="' + nextSlide + '"]').find('[data-animation]');
		doAnimations($animatingElements);
	});
	BasicSlider.slick({
		autoplay: false,
		autoplaySpeed: 10000,
		dots: false,
		fade: true,
		arrows: false,
		responsive: [
			{ breakpoint: 767, settings: { dots: false, arrows: false } }
		]
	});

	function doAnimations(elements) {
		var animationEndEvents = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
		elements.each(function () {
			var $this = $(this);
			var $animationDelay = $this.data('delay');
			var $animationType = 'animated ' + $this.data('animation');
			$this.css({
				'animation-delay': $animationDelay,
				'-webkit-animation-delay': $animationDelay
			});
			$this.addClass($animationType).one(animationEndEvents, function () {
				$this.removeClass($animationType);
			});
		});
	}
}


$('.blog-thumb-active').slick({
	dots: false,
	infinite: true,
	arrows: true,
	speed: 1500,
	slidesToShow: 1,
	slidesToScroll: 1,
	fade: true,
	prevArrow: '<button type="button" class="slick-prev"><i class="far fa-arrow-left"></i></button>',
	nextArrow: '<button type="button" class="slick-next"><i class="far fa-arrow-right"></i></button>',
});


/*=============================================
	=         Main Slider Active            =
=============================================*/
function mainSliderActive() {
	$('.main-slider-active').slick({
		slidesToShow: 1,
		slidesToScroll: 1,
		arrows: false,
		infinite: true,
		fade: true,
		dots: false,
		asNavFor: '.main-slider-nav',
		responsive: [
			{
				breakpoint: 1200,
				settings: {
					dots: false,
				}
			},
			{
				breakpoint: 575,
				settings: {
					dots: false,
				}
			},
		]
	})
	$('.main-slider-nav').slick({
		slidesToShow: 5,
		slidesToScroll: 1,
		infinite: false,
		asNavFor: '.main-slider-active',
		dots: false,
		arrows: false,
		focusOnSelect: true,
	});
}

/*=============================================
	=    		Brand Active		      =
=============================================*/
$('.brand-active').slick({
	dots: false,
	infinite: true,
	speed: 1000,
	autoplay: true,
	arrows: false,
	slidesToShow: 5,
	slidesToScroll: 1,
	responsive: [
		{
			breakpoint: 1200,
			settings: {
				slidesToShow: 5,
				slidesToScroll: 1,
				infinite: true,
			}
		},
		{
			breakpoint: 992,
			settings: {
				slidesToShow: 4,
				slidesToScroll: 1
			}
		},
		{
			breakpoint: 767,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 1,
				arrows: false,
			}
		},
		{
			breakpoint: 575,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 1,
				arrows: false,
			}
		},
	]
});



/*=============================================
	=    		Instagram Active		      =
=============================================*/
$('.instagram-active').slick({
	dots: false,
	infinite: true,
	speed: 1000,
	autoplay: true,
	arrows: false,
	slidesToShow: 6,
	slidesToScroll: 1,
	responsive: [
		{
			breakpoint: 1200,
			settings: {
				slidesToShow: 5,
				slidesToScroll: 1,
				infinite: true,
			}
		},
		{
			breakpoint: 992,
			settings: {
				slidesToShow: 4,
				slidesToScroll: 1
			}
		},
		{
			breakpoint: 767,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 1,
				arrows: false,
			}
		},
		{
			breakpoint: 575,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 1,
				arrows: false,
			}
		},
	]
});


/*=============================================
	=    		Testimonial Active		      =
=============================================*/
// $('.testimonial-active').slick({
// 	dots: false,
// 	infinite: true,
// 	speed: 1000,
// 	autoplay: true,
// 	arrows: true,
// 	prevArrow: '<button type="button" class="slick-prev"><i class="fal fa-long-arrow-left"></i></button>',
// 	nextArrow: '<button type="button" class="slick-next"><i class="fal fa-long-arrow-right"></i></button>',
// 	appendArrows: ".testimonial-nav",
// 	slidesToShow: 1,
// 	slidesToScroll: 1,
// 	responsive: [
// 		{
// 			breakpoint: 1200,
// 			settings: {
// 				slidesToShow: 5,
// 				slidesToScroll: 1,
// 				infinite: true,
// 			}
// 		},
// 		{
// 			breakpoint: 992,
// 			settings: {
// 				slidesToShow: 4,
// 				slidesToScroll: 1
// 			}
// 		},
// 		{
// 			breakpoint: 767,
// 			settings: {
// 				slidesToShow: 3,
// 				slidesToScroll: 1,
// 				arrows: false,
// 			}
// 		},
// 		{
// 			breakpoint: 575,
// 			settings: {
// 				slidesToShow: 2,
// 				slidesToScroll: 1,
// 				arrows: false,
// 			}
// 		},
// 	]
// });



/*=============================================
	=    		Team Active		      =
=============================================*/
// $('.team-active').slick({
// 	dots: false,
// 	infinite: true,
// 	speed: 1000,
// 	autoplay: true,
// 	arrows: true,
// 	prevArrow: '<button type="button" class="slick-prev"><i class="fal fa-long-arrow-left"></i></button>',
// 	nextArrow: '<button type="button" class="slick-next"><i class="fal fa-long-arrow-right"></i></button>',
// 	appendArrows: ".team-nav",
// 	slidesToShow: 4,
// 	slidesToScroll: 1,
// 	responsive: [
// 		{
// 			breakpoint: 1200,
// 			settings: {
// 				slidesToShow: 5,
// 				slidesToScroll: 1,
// 				infinite: true,
// 			}
// 		},
// 		{
// 			breakpoint: 992,
// 			settings: {
// 				slidesToShow: 4,
// 				slidesToScroll: 1
// 			}
// 		},
// 		{
// 			breakpoint: 767,
// 			settings: {
// 				slidesToShow: 3,
// 				slidesToScroll: 1,
// 				arrows: false,
// 			}
// 		},
// 		{
// 			breakpoint: 575,
// 			settings: {
// 				slidesToShow: 2,
// 				slidesToScroll: 1,
// 				arrows: false,
// 			}
// 		},
// 	]
// });

/*=============================================
	=         Courses Active           =
=============================================*/
if (jQuery(".gallery-active").length > 0) {
	let courses = new Swiper(".gallery-active", {
		slidesPerView: 2.2,
		spaceBetween: 40,
		loop: true,
		autoplay: false,
		breakpoints: {
			500: {
				slidesPerView: 1,
				spaceBetween: 20,
			},
			768: {
				slidesPerView: 2,
				spaceBetween: 20,
			},
			992: {
				slidesPerView: 2,
				spaceBetween: 20,
			},
			1200: {
				slidesPerView: 2,
			},
			1500: {
				slidesPerView: 2.2,
			},
		},
		// If we need pagination
		pagination: {
			el: ".courses-swiper-pagination",
			clickable: true,
		},

		// Navigation arrows
		navigation: {
			nextEl: ".swiper-button-next",
			prevEl: ".swiper-button-prev",
		},

		// And if we need scrollbar
		scrollbar: {
			el: ".swiper-scrollbar",
		},
	});
}

/*=============================================
	=         gallery-active           =
=============================================*/
$('.mmm').slick({
	centerMode: true,
	autoplay: true,
	infinite: true,
	speed: 500,
	centerPadding: '0',
	arrows: false,
	slidesToShow: 1,
	responsive: [
		{
			breakpoint: 1800,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: true,
			}
		},
		{
			breakpoint: 1500,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				centerPadding: '0',
				infinite: true,
			}
		},
		{
			breakpoint: 1200,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				centerPadding: '0',
				infinite: true,
			}
		},
		{
			breakpoint: 992,
			settings: {
				slidesToShow: 1,
				centerPadding: '0',
				slidesToScroll: 1
			}
		},
		{
			breakpoint: 767,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				centerPadding: '0px',
				arrows: false,
			}
		},
		{
			breakpoint: 575,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				centerPadding: '0px',
				arrows: false,
			}
		},
	]
});

/*=============================================
	=         gallery-active           =
=============================================*/
// $('.gallery-active-two').slick({
// 	centerMode: true,
// 	autoplay: true,
// 	infinite: true,
// 	speed: 500,
// 	centerPadding: '0',
// 	slidesToShow: 1,
// 	prevArrow: '<span class="slick-prev"><i class="far fa-long-arrow-left"></i></span>',
// 	nextArrow: '<span class="slick-next"><i class="far fa-long-arrow-right"></i></span>',
// 	appendArrows: ".gallery-nav-two",
// 	responsive: [
// 		{
// 			breakpoint: 1800,
// 			settings: {
// 				slidesToShow: 1,
// 				slidesToScroll: 1,
// 				infinite: true,
// 			}
// 		},
// 		{
// 			breakpoint: 1500,
// 			settings: {
// 				slidesToShow: 1,
// 				slidesToScroll: 1,
// 				centerPadding: '30px',
// 				infinite: true,
// 			}
// 		},
// 		{
// 			breakpoint: 1200,
// 			settings: {
// 				slidesToShow: 1,
// 				slidesToScroll: 1,
// 				centerPadding: '50px',
// 				infinite: true,
// 				arrows: false,
// 			}
// 		},
// 		{
// 			breakpoint: 992,
// 			settings: {
// 				slidesToShow: 1,
// 				centerPadding: '0',
// 				slidesToScroll: 1,
// 				arrows: false,
// 			}
// 		},
// 		{
// 			breakpoint: 767,
// 			settings: {
// 				slidesToShow: 1,
// 				slidesToScroll: 1,
// 				centerPadding: '0px',
// 				arrows: false,
// 			}
// 		},
// 		{
// 			breakpoint: 575,
// 			settings: {
// 				slidesToShow: 1,
// 				slidesToScroll: 1,
// 				centerPadding: '0px',
// 				arrows: false,
// 			}
// 		},
// 	]
// });


/*=============================================
	=    		Odometer Active  	       =
=============================================*/
$('.odometer').appear(function (e) {
	var odo = $(".odometer");
	odo.each(function () {
		var countNumber = $(this).attr("data-count");
		$(this).html(countNumber);
	});
});


/*=============================================
	=    		Magnific Popup		      =
=============================================*/
$('.popup-image').magnificPopup({
	type: 'image',
	gallery: {
		enabled: true
	}
});

/* magnificPopup video view */
$('.popup-video').magnificPopup({
	type: 'iframe'
});



/*=============================================
		=           Fade effect         =
=============================================*/
var blurImg = $('.project-areas-two .img-blur');
blurImg.on({
	mouseenter: function () {
		$(this).siblings().stop().fadeTo(300).addClass('active');
	},
	mouseleave: function () {
		$(this).siblings().stop().fadeTo(300).removeClass('active');
	}
});


/*=============================================
		=           Fade effect         =
=============================================*/
var blurImg = $('.project-areas .img-blur');
blurImg.on({
	mouseenter: function () {
		$(this).siblings().stop().fadeTo(300).addClass('active');
	},
	mouseleave: function () {
		$(this).siblings().stop().fadeTo(300).removeClass('active');
	}
});



/*=============================================
	=           Fade effect               =
=============================================*/
var grayscaleImg = $('.projects-area-three .grayscale-img');
grayscaleImg.on({
	mouseenter: function () {
		$(this).siblings().stop().fadeTo(300).addClass('active');
	},
	mouseleave: function () {
		$(this).siblings().stop().fadeTo(300).removeClass('active');
	}
});


/*=============================================
	=    		Isotope	Active  	      =
=============================================*/
$('.project-active-three').imagesLoaded(function () {
	// init Isotope
	var $grid = $('.project-active-three').isotope({
		itemSelector: '.grid-item',
		percentPosition: true,
		masonry: {
			columnWidth: '.grid-sizer',
		}
	});
	// filter items on button click
	$('.portfolio-menu').on('click', 'button', function () {
		var filterValue = $(this).attr('data-filter');
		$grid.isotope({ filter: filterValue });
	});

});
//for menu active class
$('.product-license li').on('click', function (event) {
	$(this).siblings('.active').removeClass('active');
	$(this).addClass('active');
	event.preventDefault();
});


// niceSelect;
 $(".selected").niceSelect();

/*=============================================
	=    		 Wow Active  	         =
=============================================*/
function wowAnimation() {
	var wow = new WOW({
		boxClass: 'wow',
		animateClass: 'animated',
		offset: 0,
		mobile: false,
		live: true
	});
	wow.init();
}



})(jQuery);