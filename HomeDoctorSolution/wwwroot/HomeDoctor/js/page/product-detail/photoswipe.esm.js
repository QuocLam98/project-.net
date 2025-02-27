'use strict';
(function ($) {

    // Init empty gallery array
    let galleryArray = [];

    // Loop over gallery items and push it to the array
    $('.gallery').find('figure').each(function () {
        var $link = $(this).find('a'),
            item = {
                src: $link.attr('href'),
                w: $link.data('width'),
                h: $link.data('height'),
                title: $link.attr('title')
            };

        galleryArray.push(item);
    });

    console.log(galleryArray);

    // Define click event on gallery item
    $('.open-galley').click(function (event) {

        // Prevent location change
        event.preventDefault();

        // Define object and gallery options
        var $pswp = $('.pswp')[0],
            options = {
                index: $(this).parent('figure').index(),
                bgOpacity: 0.85,
                showHideOpacity: true
            };


        // Initialize PhotoSwipe
        var gallery = new PhotoSwipe($pswp, PhotoSwipeUI_Default, galleryArray, options);
        gallery.init();
    });

})(jQuery);
//# sourceURL=pen.js