var abj = {
	// loaders
	load_tab: function(tab,url,done){
		if($('div#tabsect-'+tab).length)
			return;
		tiny.load(url+'/tab',function(r){
			$(r.content).insertAfter($('#mw').find('div.tabsect').last()).hide();
			abj.set_handlers();
			if(done)
				done();
		},function(e){alert(e);});
	},
	load_ns: function(trg,done){
		var xns = trg.attr('title');
		if($('ul#nstree-'+xns).length)
			return;
		var tgt = trg.attr('href');
		var wrap = trg.parent();
		wrap.toggleClass('loading');
		//
		tiny.load(tgt,function(r){
			wrap.toggleClass('loading');
			wrap.append(r.content);
			abj.set_handlers();
			if(done)
				done();
		},function(e){wrap.toggleClass('loading');alert(e);});
	},
	load_xbx: function(trg,done){
		var tgt = trg.attr('href');
		var xbox = $('#mw').find('div#xbox-'+trg.attr('title'));
		if(!xbox.length)
			return;
		var wrap = trg.parent();
		wrap.toggleClass('loading');
		//
		tiny.load(tgt,function(r){
			wrap.toggleClass('loading');
			xbox.html(r.content);
			abj.set_handlers();
			if(done)
				done();
		},function(e){wrap.toggleClass('loading');alert(e);});
	},
	hdl_nsclick: function(e){
		e.preventDefault();
		var x_trigger = $(this);
		var nstree = $('#mw').find('ul#nstree-'+x_trigger.attr('title'));
		if(!nstree.length)
			abj.load_ns(x_trigger,function(){
				nstree = $('#mw').find('ul#nstree-'+x_trigger.attr('title'));
				nstree.slideToggle();
			});
		else
			nstree.slideToggle();
	},
	hdl_fnclick: function(e){
		e.preventDefault();
		var x_trigger = $(this);
		var xbox = $('#mw').find('div#xbox-'+x_trigger.attr('title'));
		if(!xbox.length){
			x_trigger.parent().append('<div class="boundbox funcinfo" style="display:none;" id="xbox-'+x_trigger.attr('title')+'"></div>');
			xbox = $('#mw').find('div#xbox-'+x_trigger.attr('title'));
		}
		else if(!xbox.data('target') && xbox.html()){
			xbox.data('target',x_trigger.attr('href'));
		}
		//
		if('none' == xbox.css('display') || xbox.data('target') != x_trigger.attr('href') || !xbox.html()){
			abj.load_xbx(x_trigger,function(){
				xbox = $('#mw').find('div#xbox-'+x_trigger.attr('title'));
				xbox.data('target',x_trigger.attr('href'));
				if('none' == xbox.css('display'))
					xbox.slideDown();
			});
		}
		else if('none' != xbox.css('display'))
			xbox.slideUp();
	},
	hdl_tjimgclick: function(e){
		var captcha_url = $(this).attr('src');
		$(this).attr('src',captcha_url+'/'+new Date().getTime());
	},
	hdl_sub_fmsearch: function(r,nstree){
		$('ul.ns_tree').slideUp().promise().done(function(){
			nstree.slideDown().promise().done(function(){
				if(!$('div#xbox-'+r.fn).length || 'none' == $('div#xbox-'+r.fn).css('display')){
					$('a#fntrg-'+r.fn).triggerHandler('click');
				}
				$('html,body').animate({
					scrollTop: $('a#fntrg-'+r.fn).offset().top
				},400);
			});
		});
	},
	hdl_fmsearch: function(e){
		e.preventDefault();
		var fxm = $(this);
		var act = fxm.attr('action');
		tiny.submit(act,fxm.serialize(),function(r){
			var nstree = $('#mw').find('ul#nstree-'+r.ns);
			if(!nstree.length)
			{
				abj.load_ns($('#mw').find('a#nstrg-'+r.ns),function(){
					nstree = $('#mw').find('ul#nstree-'+r.ns);
					nstree.slideToggle();
					abj.hdl_sub_fmsearch(r,nstree)
				});
			}
			else
				abj.hdl_sub_fmsearch(r,nstree);
		},function(e){alert(e);});
	},
	hdl_fmedit: function(e){
		e.preventDefault();
		var fxm = $(this);
		var act = fxm.attr('action');
		tiny.submit(act,fxm.serialize(),function(r){
			$('#mw').find('img.tjurimg').attr('src',$('img.tjurimg').attr('src')+'/'+new Date().getTime());
			fxm.siblings('div.err').hide();
			var zbox = fxm.parent('div.funcinfo');
			zbox.siblings('a.fn_trigger').last().html(r.proto);
			zbox.siblings('a.fn_trigger').last().attr('href',r.info);
			zbox.siblings('a.fn_trigger').last().attr('title',r.func);
			zbox.siblings('a.fn_trigger').first().attr('title',r.func);
			zbox.siblings('a.fn_trigger').first().attr('href',r.info.replace('/info/','/edit/'));
			zbox.attr('id','xbox-'+r.func);
			var zd = r.info;
			tiny.load(r.info,function(r){
				zbox.html(r.content);
				zbox.data('target',zd);
				abj.set_handlers();
			},function(e){fxm.siblings('div.err').html(e).show();});
		},function(e){fxm.siblings('div.err').html(e).show();});
	},
	fx_tab: function(tbx){
		$('#mw').find('div.tabsect').fadeOut(400).promise().done(function(){tbx.fadeIn(400);});
	},
	// dynamic handlers
	set_handlers: function(){
		// reset all applicable
		$('#mw').find('a.ns_trigger').off('click');
		$('#mw').find('a.fn_trigger').off('click');
		$('#mw').find('img').off('click');
		$('#mw').find('form').off('submit');
		// apply new
		var nstrgz = $('#mw').find('a.ns_trigger');
		if(nstrgz.length)
			nstrgz.on('click',abj.hdl_nsclick);
		var fntrgz = $('#mw').find('a.fn_trigger');
		if(fntrgz.length)
			fntrgz.on('click',abj.hdl_fnclick);
		var tjurimg = $('#mw').find('img.tjurimg');
		if(tjurimg.length)
			tjurimg.on('click',abj.hdl_tjimgclick);
		var fmed = $('#mw').find('form.fn_editor');
		if(fmed.length)
			fmed.on('submit',abj.hdl_fmedit);
		var fmfinder = $('#mw').find('form.fn_finder');
		if(fmfinder.length)
			fmfinder.on('submit',abj.hdl_fmsearch);
	},
	// initializer
	init: function(){
		var tabtrg = $('#mw').find('a.tab_trigger');
		tabtrg.on('click',function(e){
			e.preventDefault();
			var xr = $(this).attr('href');
			var tabname = $(this).attr('id').split('-')[1];
			var tbx = $('#mw').find('div#tabsect-'+tabname);
			if(!tbx.length){
				// Threads sync big bad voodoo follows
				var tsfLoad = 0;
				var tsfFx = 0;
				$('#mw').find('div.tabsect').fadeOut(400).promise().done(function(){
					tsfFx = 1;
					if(tsfLoad && tsfFx)
						$('#mw').find('div#tabsect-'+tabname).fadeIn(400);
				});
				abj.load_tab(tabname,$(this).attr('href'),function(){
					tsfLoad = 1;
					if(tsfLoad && tsfFx)
						$('#mw').find('div#tabsect-'+tabname).fadeIn(400);
				});
			}
			else if('none' == tbx.css('display'))
				abj.fx_tab(tbx);
		});
		// namespaces: expand all/collapse all
		$('a#expandns').on('click',function(e){
			e.preventDefault();
			if(!$('.ns_tree').length)
				return;
			$('.ns_trigger').each(function(){
				var nstree = $('ul#nstree-'+$(this).attr('title'));
				if(!nstree.length || 'none' == nstree.css('display')){
					$(this).triggerHandler('click');
				}
			});
		});
		$('a#collapsens').on('click',function(e){
			e.preventDefault();
			if(!$('#mw').find('.ns_tree').length)
				return;
			$('#mw').find('.ns_tree').slideUp();
		});
		//
		window.onpopstate = function(e){
			// restore from: e.state
			console.log('[History]\n'+JSON.stringify(e));
		};
	}
};
