jQuery(document).ready(function($){

    var presetBrowser ={
          globals:{         
              currentAudioGallery: null,
              stickyplayer:{
                  status:null
              }
          }
      }
    
    init();
    
    function init(){
          //initIsotope();
          initAudioPlayer();
          // Listen for the event.
          document.addEventListener('mySuperEvt', function(e){
              console.log("mySuperEvt capture on document level");
              document.getElementById(presetBrowser.globals.currentAudioGallery).api_goto_next();
          }, false);
      }
    
       
    
      /**
       * Instantiate an audioGallery on each preset banks (audioGallery/player plugin)
       */
    function initAudioPlayer(){
    
      var settings_ap = {
          autoplayNext:true,
          autoplay: 'true',
          disable_scrub: 'default',
          design_skin: 'skin-wave',
          skinwave_dynamicwaves:'on',
          cue: 'on',
          transition:'fade'
        };
    
    
      var ag_id  = "#audio-gallery-0";
    
      var gallery = dzsag_init(ag_id,{
        'transition':'fade'
        ,'autoplay' : 'off'
        ,'settings_ap':settings_ap
      });
    
      
    
      // listener on ended event // never raised
      $("audio" ,ag_id).on("ended",function(){
        console.log("audio audiogallery jQuery .on('ended')");
        document.getElementById(presetBrowser.globals.currentAudioGallery).api_goto_next();
      });
    
    initStickyPlayer(this);
        
    }
    
    /**
       *  Init or reinit player sticked to bottom of page, add event listener (prev and next audio demo) at first call
       * @param audioGal
       */
    function initStickyPlayer(audioGal){
    
     // var audioURL = $('.audioplayer-tobe',audioGal).attr("data-source");
       //$("#stickyplayer").attr("data-source",audioURL);
    
      var settings_ap = {
        autoplayNext:true,
        swf_location: "/templates/arturia-bootstrap/assets/scripts/jquery-plugins/waveform-audio-player/ap.swf",
        autoplay: 'true',
        preload_method: 'none',
        disable_scrub: 'default',
        design_skin: 'skin-wave',
        skinwave_dynamicwaves:'on',
        cue: 'on',
        transition:'fade',
        skinwave_mode: 'small'
      };
    
      dzsap_init('.stickyplayer',settings_ap);
    
    
      /**
      * Add Listeners on button prev and next in stickyplayer
      */
      if(presetBrowser.globals.stickyplayer.status != "initialized_almost_once")
      {
        $('.stickyplayer .btn-prev').on('click', function (event) {
          document.getElementById(presetBrowser.globals.currentAudioGallery).api_goto_prev();
        })
        $('.stickyplayer .btn-next').on('click', function (event) {
          document.getElementById(presetBrowser.globals.currentAudioGallery).api_goto_next();
        })
      }
    
      // Adding event listerners on oneded event
      // Dom way
      $('.stickyplayer audio')[0].addEventListener('ended', function (event) {
        console.log("stickyplayer ended captured from addEventListern");
      })
      $('.stickyplayer audio')[0].onended = function (event) {
        console.log("stickyplayer ended captured from onended");
      }
    
      // jQuery way
      $(".stickyplayer audio").on("ended",function(){
        console.log("stickyplayer jQuery .on('ended')");
        document.getElementById(presetBrowser.globals.currentAudioGallery).api_goto_next();
      })
    
      presetBrowser.globals.stickyplayer.status = "initialized_almost_once";
    }
    
    function showStickyPlayer(audiogaleryID){
    
      var packname = $('.playerInfos',audiogaleryID).data('packname');
    
      $(".dzsap-sticktobottom .the-artist").html(packname);
    
      $(".dzsap-sticktobottom").animate({
        opacity: 1
      }, 700, function() {
        $(window).trigger('resize'); // force waveform to be recalculate
      });
    
    }
    
    function hideStickyPlayer(){
      $(".dzsap-sticktobottom").css({
        "opacity": "0"
      });
    }
    
    });