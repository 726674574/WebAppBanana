// JavaScript Document
function loadingData(obj,objurl){
	prodlist= document.getElementById(obj)
	if(getScrollTop() + getWindowHeight() == getScrollHeight() ){
　　   // alert(canScroll)
	   if(canScroll){
		var loading = document.createElement("li");
		loading.classList="loading";
		loading.style.width="100%";
		//loading.style.background="rgba(0,0,0,.6)";
		loading.style.textAlign="center";
		loading.style.color="#999";
		loading.innerHTML="数据加载中请稍候!";
		loading.style.lineHeight="35px";
		loading.id="loading";
		prodlist.appendChild(loading);
		canScroll=false
		setTimeout(function(){	
		    loadingitem=document.getElementById("loading");	
			prodlist.removeChild(loadingitem);//清除加载样式
			addData(prodlist,objurl);//追加数据
			
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
        
	    if (parseInt(page) < parseInt(pagecount)) {
	        //如果有 下一页数据 先删除原来page数据
	        sppage.parentNode.removeChild(sppage);
	        var ajaxdata = "page=" + page + "&seach=" + seach+"&order="+order+"&type="+type;
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
	    }

//	htmls=obj.innerHTML;
//	obj.innerHTML=obj.innerHTML+htmls;
//	canScroll=true;
	}

	


