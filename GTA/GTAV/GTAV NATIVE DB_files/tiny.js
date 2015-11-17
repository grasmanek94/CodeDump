$ = jQuery; // dev purpose
var tiny = {
	// props
	base: null,
	title: null,
	hist: null,
	// methods
	resp_ok: function(r,success,fail){
		if(undefined == r.errors || !r.errors){if(success)success(r);}
		else{if(fail)fail('Server error: '+r.errors);}
	},
	resp_err: function(r,fail){
		if(fail)
			fail('Client error: '+r);
	},
	load: function(url,success,fail,always){
		$.get(url).done(function(r){tiny.resp_ok(r,success,fail);}).fail(function(e){tiny.resp_err(e,fail);}).always(function(x){if(always)always(x);});
	},
	submit: function(url,data,success,fail,always){
		$.post(url,data).done(function(r){tiny.resp_ok(r,success,fail);}).fail(function(e){tiny.resp_err(e,fail);}).always(function(x){if(always)always(x);});
	},
	hist_add: function(url,state){
		if(undefined != history.pushState)
			history.pushState(state,tiny.title,url);
		else{
			if(null == tiny.hist)
				tiny.hist = new Array();
			window.location.hash = url.replace(tiny.base,'');
			tiny.hist.push(state);
		}
	},
	hist_load: function(){
		if('#' != window.location.hash){
			window.location.assign(window.location.hash.substring(1));
		}
	}
};
