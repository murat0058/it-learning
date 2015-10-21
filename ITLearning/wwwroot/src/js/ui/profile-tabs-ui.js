$(function () {

    var availableBackgrounds = {
        'tab-primary': '#018ee0',
        'tab-success': '#21b2a6',
        'tab-dark': '#2e3842',
        'tab-warning': '#ff8d00',
        'tab-danger': '#ed4933',
    };

    var activeTab = null,
        requestedHeight = null;

    var $contentWrapper = $('.user-achievements-content'),
        $content = $('.user-achievements-content .content'),
        $widgetTab = $('.user-achievements-tabs .tab');

    $widgetTab.click(function () {

        $widgetTab.removeClass('active');

        if (activeTab === $(this).attr('data-tab')) {
            requestedHeight = '0px';
            activeTab = '';
        } else {
            requestedHeight = '150px';
            activeTab = $(this).attr('data-tab');

            $contentWrapper.css('background-color', availableBackgrounds[activeTab]);
            $(this).addClass('active');
        }

        $content.animate({ height: requestedHeight }, 350, 'swing', function () {

        });
    });
});