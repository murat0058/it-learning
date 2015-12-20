$(function () {

    var $toggleBtn = $('.menu-title-toggle'),
        $menu = $('nav.menu'),
        $icon = $('.menu-icon'),
        $overlay = $('.overlay'),
        animation = 'ease-in',
        animationStates = {
            0: {
                position: 0,
                overlayVisible: true
            },
            1: {
                position: -250,
                overlayVisible: false
            }
        },
        currentState = 0;

    var toggleMenuVisibility = function () {

        var newState = animationStates[currentState % 2],
            newPosition = newState.position,
            isOverlayVisible = newState.overlayVisible;

        var overlayAnimationSpeed = 150,
            requiredOpacity = 0.8;

        if (isOverlayVisible) {
            $overlay.show();
            $overlay.fadeTo(overlayAnimationSpeed, requiredOpacity);
        } else {
            $overlay.fadeTo(overlayAnimationSpeed, 0, function () {
                $overlay.hide();
            });
        }

        $menu.animate({ right: newPosition }, animation);

        currentState++;
    };

    $toggleBtn.click(toggleMenuVisibility);

});