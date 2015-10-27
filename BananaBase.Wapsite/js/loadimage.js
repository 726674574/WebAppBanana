

function ScrollloadImage(){
	var temp = -1;
     var imgElements = document.getElementsByTagName("img");
	var lazyImgArr = new Array();  
				var j = 0;  
				for(var i=0; i<imgElements.length; i++) {  
				   if(imgElements[i].className == "lazy"){  
					 lazyImgArr[j++] = imgElements[i];  
				   } 
				}  
			
			  var scrollHeight = document.body.scrollTop||document.documentElement.scrollTop;//滚动的高度  
			  var bodyHeight = document.documentElement.clientHeight||document.body.clientHeight;//body（页面）可见区域的总高度  
			  if(temp < scrollHeight) {//为true表示是向下滚动，否则是向上滚动，不需要执行动作。  
				
				for(var k=0; k<lazyImgArr.length; k++) {  
				var imgTop = lazyImgArr[k].offsetTop;//1305（图片纵坐标）  
				if((imgTop - scrollHeight) <= bodyHeight) {  
				  lazyImgArr[k].src = lazyImgArr[k].alt;
				  
				  lazyImgArr[k].className = "notlazy"  
								}  
			  } 
				
			  temp = scrollHeight;  
		   }  
	}

function loadPicNow(){
	var scrollHeight = document.body.scrollTop||document.documentElement.scrollTop;//滚动的高度  
    var bodyHeight = document.documentElement.clientHeight||document.body.scrollTop;//body（页面）可见区域的总高度  
	var imgElements = document.getElementsByTagName("img"); 
	//var ClienHeight=document.body.clientHeight||document.body.documentElement.clientHeight;
				for(var i=0; i<imgElements.length; i++) {
                  var imgTop = imgElements[i].offsetTop;//1305（图片纵坐标）
				  // alert(imgTop +" , "+scrollHeight +" , "+bodyHeight);
				   if((imgTop - scrollHeight) <= bodyHeight) {  
				       
					  imgElements[i].className = "notlazy";
					  imgElements[i].src=imgElements[i].alt;
					  
					  }
				  }
	
	}
	

function showTop(){
	var ScrollTop=document.body.scrollTop||document.documentElement.scrollTop;
	var Bodyheight=document.body.clientHeight||document.documentElement.clientHeight;
	var Gotop=document.getElementById("Gotop")
	if(ScrollTop>Bodyheight){Gotop.style.display="block";}
	else{
		Gotop.style.display="none"
		}
	}
	

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
//浏览器视口的高度
function getWindowHeight(){
　　var windowHeight = 0;
　　if(document.compatMode == "CSS1Compat"){
　　　　windowHeight = document.documentElement.clientHeight;
　　}else{
　　　　windowHeight = document.body.clientHeight;
　　}
　　return windowHeight;
}



function getRTime(str){
        var EndTime= new Date(str);
        var NowTime = new Date();
        var t =EndTime.getTime() - NowTime.getTime();

        var d=Math.floor(t/1000/60/60/24);
        var h=Math.floor(t/1000/60/60%24);
        var m=Math.floor(t/1000/60%60);
        var s=Math.floor(t/1000%60);
        var t_h=h+d*24;
        //document.getElementById("t_d").innerHTML = d+'天';
		try{
		document.getElementById("t_h").innerHTML = t_h<10?"0"+t_h:t_h;
        document.getElementById("t_m").innerHTML = m<10?"0"+m:m ;
        document.getElementById("t_s").innerHTML = s<10?"0"+s:s ; 
		}
		catch(e){}
}
    setInterval(function(){getRTime(times)},1000);
	

function getRTime01(str){
        var EndTime= new Date(str);
        var NowTime = new Date();
        var t =EndTime.getTime() - NowTime.getTime();

        var d=Math.floor(t/1000/60/60/24);
        var h=Math.floor(t/1000/60/60%24);
        var m=Math.floor(t/1000/60%60);
        var s=Math.floor(t/1000%60);
        try{
        document.getElementById("t_d1").innerHTML = d+'天';
		document.getElementById("t_h1").innerHTML = h<10?"0"+h:h;
        document.getElementById("t_m1").innerHTML = m<10?"0"+m:m ;
        document.getElementById("t_s1").innerHTML = s<10?"0"+s:s ;
		}
		catch(e){}
}

try {
    var times = document.getElementById("timesaleTime").value;
    setInterval(function () { getRTime01('2015/08/1 00:00:00') }, 1000);	
} catch (e) {

} 
	