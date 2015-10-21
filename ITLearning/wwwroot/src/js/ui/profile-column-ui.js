$(function () {

    //TODO disable for $(window).width() < 768

    var $window = $(window),
        $container = $('.user-profile-container');

    $window.scroll(function () {
        $container.css('top', $(window).scrollTop() + 'px');
    });
});