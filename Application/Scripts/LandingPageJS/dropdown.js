$(function(){
    
    $('.at-drop-down').on('click', function(e){
        if(Modernizr.mq('screen and (max-width:767px)')) {
            e.preventDefault();
            $(this).next($('.sub-menu')).slideToggle();
        }
    });
    
    $(window).resize(function() {
        $('.sub-menu').attr("style", "");
    });

});