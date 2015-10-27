function getScrollTop(){
　　var scrollTop = 0, bodyScrollTop = 0, documentScrollTop = 0;
　　if(document.body){
　　　　bodyScrollTop = document.body.scrollTop;
　　}
　　if(document.documentElement){
　　　　documentScrollTop = document.documentElement.scrollTop;
　　}
　　scrollTop = (bodyScrollTop - documentScrollTop > 0) ? bodyScrollTop : documentScrollTop;
　　return scrollTop;
}
//文档的总高度
function getScrollHeight(){
　　var scrollHeight = 0, bodyScrollHeight = 0, documentScrollHeight = 0;
　　if(document.body){
　　　　bodyScrollHeight = document.body.scrollHeight;
　　}
　　if(document.documentElement){
　　　　documentScrollHeight = document.documentElement.scrollHeight;
　　}
　　scrollHeight = (bodyScrollHeight - documentScrollHeight > 0) ? bodyScrollHeight : documentScrollHeight;
　　return scrollHeight;
}
function getWindowHeight(){
　　var windowHeight = 0;
　　if(document.compatMode == "CSS1Compat"){
　　　　windowHeight = document.documentElement.clientHeight;
　　}else{
　　　　windowHeight = document.body.clientHeight;
　　}
　　return windowHeight;
}


function loadingData(obj, objurl) {
	prodlist= document.getElementById(obj)
	if(getScrollTop() + getWindowHeight() == getScrollHeight() ){
	   if(canScroll){
		canScroll=false
		setTimeout(function(){
		    addData(prodlist, objurl); //追加数据
			
			},1000);
	   }
	   
　　}
	
	}

	function addData(obj, objurl) {
	    //根据不同url请求不同页面
	    //var Cont = document.getElementById("theList");
	    var sppage = document.getElementById("pager");
	    var page = sppage.getAttribute("page");
	    var pagecount = sppage.getAttribute("pagecount");
	    var seach = "";
	    if (document.getElementById("keyword") != null) {
	        seach = document.getElementById("keyword").value;
	    }
	    var order = "";
	    if (document.getElementsByClassName("on").length != 0) {
	        order = document.getElementsByClassName("on")[0].text;
	    }
	    var type = "";
	    if (document.getElementById("type") != null) {
	        type = document.getElementById("type").value;
	    }
	    var type2 = "";
	    if (document.getElementById("pro_type2") != null) {
	        type2 = document.getElementById("pro_type2").value;
	    }

	    if (parseInt(page) < parseInt(pagecount)) {
	        //如果有 下一页数据 先删除原来page数据
	        sppage.parentNode.removeChild(sppage);
	        var ajaxdata = "page=" + page + "&seach=" + seach + "&order=" + order + "&type=" + type+"&type2="+type2;
	        Ajax.request('/ajax/' + objurl + '.ashx', {
	            data: ajaxdata,
	            method: "GET",
	            success: function(xhr) {
	                if (xhr != "-1") {
	                    htmls = obj.innerHTML;
	                    obj.innerHTML = obj.innerHTML + xhr.responseText;
	                    canScroll = true;
	                    //Cont.innerHTML = Cont.innerHTML + xhr.responseText;
	                } else {
	                    alert("请求数据异常");
	                }
	            }
	        });
	    } else {
	        var loadingdata = document.getElementById("loadingdata");
	        loadingdata.innerHTML = "已到最后一页";
	    }
	}