<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="temai.aspx.cs" Inherits="Banana.Wapsite.temai" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="utf-8">
<meta content="width=device-width,height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
<meta name="description" content="banana情趣商城，是一家专卖成人两性情趣用品的特卖站，在线销售杜蕾斯、冈本、LELO等国际一线品牌男女情趣用品、情趣内衣、安全套、人体润滑剂等数千款优质性用品">
<meta name="keywords" content="飞机杯,跳蛋,震动棒,情趣内衣,套套,催情迷情用品 成人用品,情趣用品,性用品,两性用品,成人网站"/>
<title>banana情趣商城-限时特卖</title>
<link href="/css/basic.css" rel="stylesheet" type="text/css">
<link href="/css/shop.css" rel="stylesheet" type="text/css">
</head>
<body>

<div class="header">
  <div class="top"><a href="index.aspx" class="Bg50 back"></a>限时特卖<a href="search.aspx" class="Bg50 search"></a></div>
 </div>

<div style="margin:50px 0 0 0">
  <p class="tmBox">距离今日场结束
    <span id="t_h">00</span> : 
    <span id="t_m">00</span> : 
    <span id="t_s">00</span></p>
    <input id="timesaleTime" value="<%=GetTimeSaleEndTime() %>" type="hidden" />
  <ul class="plist04 clf" id="theList">
   
   
  </ul>
</div>



<div class="RightMenu">
  <a href="/orderinfo.aspx" class="cart Bg50"><span class="bdradius6" id="cart">0</span></a>
  <a href="#" class="gotop Bg50"></a>
</div>

 <script src="/js/loadimage.js" language="javascript" type="text/jscript"></script>
 <script src="/js/loadingDate.js" language="javascript" type="text/jscript"></script>
 <script src="/js/ajaxhelp.js" type="text/javascript"></script>
<script>
    var canScroll = true;
    window.onload = function () {

        gettemai();
        loadPicNow();
        loadcook();
        window.onscroll = function () {
            ScrollloadImage();
            loadingData("theList","index_temai");

        };

    }
    function loadcook() {

        Ajax.request('/ajax/getgouwuchecount.ashx', {
            method: "GET",
            success: function (xhr) {
                //to do with xhr<a href="ajax/test.ashx">ajax/test.ashx</a>
                document.getElementById("cart").innerHTML = xhr.responseText;

            }
        });

    }

   function gettemai()
    {
       
        var Cont = document.getElementById("theList");
        var ajaxdata = "page=" + 0 ;
        Ajax.request('/ajax/index_temai.ashx', {
            data: ajaxdata,
            method: "GET",
            success: function (xhr) {
                if (xhr != "-1") {
                    Cont.innerHTML = Cont.innerHTML + xhr.responseText;
                    loadPicNow();
                } else {
                    alert("请求数据异常");
                }
            }
        });
    }

    function showfl() {
        var flbox = document.getElementById("flbox");
        var animate01 = document.getElementById("animate01");
        flbox.style.display = "block";
        animate01.className = 'flcont a-fadeinL';
    }

    function closeIt() {
        var flbox = document.getElementById("flbox");
        var animate01 = document.getElementById("animate01");

        animate01.className = 'flcont';
        flbox.style.display = "none";
    }


    var flcontLi = document.getElementById("animate01").getElementsByTagName("li");

    function flcontLiShowsSub(k) {

        for (i = 0; i < flcontLi.length; i++) {

            if (k == i) {
                flcontLi.item(k).className = "sel";
            }
            else {
                flcontLi.item(i).className = "";
            }

        }


    }	 

	 
</script>

</body>
</html>
