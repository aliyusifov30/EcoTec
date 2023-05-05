(function ($) {
"use strict";

    $(window).on('load', function () {
        preloader();
    });

    // website preloader
    function preloader() {
        $('#preloader').delay(0).fadeOut();
    };

    // Choose Items
    $(".choose-item-two").hover(function(){
        var cur = $(this);
        $(".choose-item-two").removeClass("active");
        cur.addClass("active");
        return true;
    });

    $('.counter').counterUp({
        delay: 10,
        time: 3000
    });


    /*-------------------------------------
    Intersection Observer
    -------------------------------------*/
    if (!!window.IntersectionObserver) {
    let observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add("active-animation");
            //entry.target.src = entry.target.dataset.src;
            observer.unobserve(entry.target);
        }
        });
    }, {
        rootMargin: "0px 0px -100px 0px"
    });
    document.querySelectorAll('.has-animation').forEach(block => {
        observer.observe(block)
    });
    } else {
    document.querySelectorAll('.has-animation').forEach(block => {
        block.classList.remove('has-animation')
    });
    }


    /*=============================================
        =            Scroll Up               =
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
        =           Isotope Active            =
    =============================================*/
    $('.portfolio-active').imagesLoaded(function () {
        // init Isotope
        var $grid = $('.portfolio-active').isotope({
            itemSelector: '.grid-item',
            percentPosition: true,
            masonry: {
                columnWidth: 1,
            }
        });
        // filter items on button click
        $('.portfolio-menu').on('click', 'button', function () {
            var filterValue = $(this).attr('data-filter');
            $grid.isotope({ filter: filterValue });
        });
    });
    //for menu active class
    $('.portfolio-menu button').on('click', function (event) {
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        event.preventDefault();
    });


    /*=============================================
       =              Faq Active              =
    =============================================*/
    $(".faq-set > a").on("click", function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
            $(this)
                .siblings(".content")
                .slideUp(200);
            $(".faq-set > a i")
                .removeClass("fa-minus")
                .addClass("fa-plus");
        } else {
            $(".faq-set > a i")
                .removeClass("fa-minus")
                .addClass("fa-plus");
            $(this)
                .find("i")
                .removeClass("fa-plus")
                .addClass("fa-minus");
            $(".faq-set > a").removeClass("active");
            $(this).addClass("active");
            $(".content").slideUp(200);
            $(this)
                .siblings(".content")
                .slideDown(200);
        }
        return false
    });

    // offcanvas menu
    $(".menu-tigger").on("click", function () {
        $(".extra-info, .offcanvas-overly").addClass("active");
        return false;
    });
    $(".menu-close,.offcanvas-overly").on("click", function () {
        $(".extra-info,.offcanvas-overly").removeClass("active");
    });

    $(".menu-tigger").on("click", function () {
        $(".off-canvas-menu,.offcanvas-overly").addClass("active");
        return false;
    });
    $(".menu-close,.offcanvas-overly").on("click", function () {
        $(".off-canvas-menu,.offcanvas-overly").removeClass("active");
    });

    jQuery('.elementor-accordion-icon-opened i').removeClass('fas fa-arrow-up');
    jQuery('.elementor-accordion-icon-opened i').addClass('fal fa-arrow-up');

    jQuery('.elementor-accordion-icon-closed i').removeClass('fas fa-arrow-down');
    jQuery('.elementor-accordion-icon-closed i').addClass('fal fa-arrow-down');

})(jQuery);