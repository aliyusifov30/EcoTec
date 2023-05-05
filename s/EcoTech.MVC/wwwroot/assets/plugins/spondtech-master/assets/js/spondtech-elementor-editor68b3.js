( function( $ ) {  
  "use strict";
    /**
   * @param $scope The Widget wrapper element as a jQuery element
   * @param $ The jQuery alias
   */ 

    //default counter number
    var numberCounter = function( $scope, $ ){
        var $_this = $scope.find( '.countter' );
        var $currentID = '#'+$_this.attr( 'id' ),
            $delay = $_this.data( 'delay' ),
            $time = $_this.data( 'time' );

        var cncounter = $( $currentID );
        cncounter.counterUp({
            delay: $delay,
            time: $time,
        });
    }

    
    //Make sure you run this code under Elementor.
    $( window ).on( 'elementor/frontend/init', function() {
      elementorFrontend.hooks.addAction( 'frontend/element_ready/single-counter.default', numberCounter );
    } );
    

})( jQuery );