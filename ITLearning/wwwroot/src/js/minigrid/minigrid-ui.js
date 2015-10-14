$(function () {

    var applyMinigrid = function () {
        minigrid('.grid', '.grid-item');
    };

    applyMinigrid();

    $(window).resize(function () {
        applyMinigrid();
    });

});