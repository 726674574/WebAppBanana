<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="Banana.Wapsite.search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="utf-8">
<meta content="width=device-width,height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
<meta name="description" content="banana情趣商城，是一家专卖成人两性情趣用品的特卖站，在线销售杜蕾斯、冈本、LELO等国际一线品牌男女情趣用品、情趣内衣、安全套、人体润滑剂等数千款优质性用品">
<meta name="keywords" content="飞机杯,跳蛋,震动棒,情趣内衣,套套,催情迷情用品 成人用品,情趣用品,性用品,两性用品,成人网站"/>
<title>banana情趣商城</title>
<link href="/css/basic.css" rel="stylesheet" type="text/css">
<link href="/css/shop.css" rel="stylesheet" type="text/css">
</head>
<body>


<div class="header">
  <div class="top">
    <a href="#" onClick="javascript:history.go(-1);" class="Bg50 back" style="top:15px"></a>
    <div class="searchbg" style=" padding-left:50px;">
      <div class="searchBox bdradius6 clf">
         <input type="button" onclick="seach()" class="searchBtn Bg50" value=""/>
      <input type="text"  value="<%=seach %>" name="keyword" id="keyword" class="searchTxt" placeholder="Banana情趣商品搜索" />
      </div>
    </div>
  </div>
</div>




<div class="prodlist" id="prodlist" style="margin-top:70px">

  <ul id="theList" class="clf">
   
  </ul>
  <div class="loadingdata">数据加载中请稍候!</div>
</div>


<div class="flbox" id="flbox">
  <div class="flcont" id="animate01">
    <ul>
      <li class="sel" onClick="flcontLiShowsSub(0)">
        <h2><a href="#">男性玩具</a></h2>
        <p><a href="#" class="bdradius6">手动飞机杯</a>
        <a href="#" class="bdradius6">自动飞机杯</a>
        <a href="#" class="bdradius6">前列腺按摩器</a>
        <a href="#" class="bdradius6">女优名器</a>
        <a href="#" class="bdradius6">充气娃娃</a></p></li>
      <li onClick="flcontLiShowsSub(1)">
        <h2><a href="#">女性玩具</a></h2>
        <p><a href="#" class="bdradius6">手动飞机杯</a>
        <a href="#" class="bdradius6">自动飞机杯</a>
        <a href="#" class="bdradius6">前列腺按摩器</a>
        <a href="#" class="bdradius6">女优名器</a>
        <a href="#" class="bdradius6">充气娃娃</a></p>
      </li>
      <li onClick="flcontLiShowsSub(2)">
        <h2><a href="#">情趣内衣</a></h2>
        <p><a href="#" class="bdradius6">手动飞机杯</a>
        <a href="#" class="bdradius6">自动飞机杯</a>
        <a href="#" class="bdradius6">前列腺按摩器</a>
        <a href="#" class="bdradius6">女优名器</a>
        <a href="#" class="bdradius6">充气娃娃</a></p>
      </li>
      <li onClick="flcontLiShowsSub(3)">
        <h2><a href="#">套套世界</a></h2>
        <p><a href="#" class="bdradius6">手动飞机杯</a>
        <a href="#" class="bdradius6">自动飞机杯</a>
        <a href="#" class="bdradius6">前列腺按摩器</a>
        <a href="#" class="bdradius6">女优名器</a>
        <a href="#" class="bdradius6">充气娃娃</a></p>
        </li>
      <li onClick="flcontLiShowsSub(4)">
        <h2><a href="#">润滑油</a></h2>
        <p><a href="#" class="bdradius6">手动飞机杯</a>
        <a href="#" class="bdradius6">自动飞机杯</a>
        <a href="#" class="bdradius6">前列腺按摩器</a>
        <a href="#" class="bdradius6">女优名器</a>
        <a href="#" class="bdradius6">充气娃娃</a></p>
        </li>
      <li onClick="flcontLiShowsSub(5)">
        <h2><a href="#">调情助兴</a></h2>
        <p><a href="#" class="bdradius6">手动飞机杯</a>
        <a href="#" class="bdradius6">自动飞机杯</a>
        <a href="#" class="bdradius6">前列腺按摩器</a>
        <a href="#" class="bdradius6">女优名器</a>
        <a href="#" class="bdradius6">充气娃娃</a></p>
        </li>

    </ul>
  </div>
  <a href="javascript:closeIt();" class="flout"><span>点击关闭</span></a>
</div>


<div class="RightMenu">
  <a href="#" class="cart Bg50"><span class="bdradius6" id="cart">0</span></a>
  <a href="#" class="gotop Bg50"></a>
</div>

 <script src="/js/loadimage.js" language="javascript" type="text/jscript"></script>
 <script src="/js/loadingDate1.js" language="javascript" type="text/jscript"></script>
   <script src="/js/ajaxhelp.js" type="text/javascript"></script>
<script>
    var canScroll = true;
    window.onload = function () {


        Getindexseach();
        loadPicNow();
        loadcook();
        window.onscroll = function() {
            ScrollloadImage();
            loadingData("theList", "index_seach");

        };
    };
    function Getindexseach() {
        var cont = document.getElementById("theList");
        var seach = document.getElementById("keyword").value;
        var ajaxdata = "page=" + 0 + "&seach=" + seach;
        Ajax.request('/ajax/index_seach.ashx', {
            data: ajaxdata,
            method: "GET",
            success: function (xhr) {
                if (xhr != "-1") {
                    cont.innerHTML = cont.innerHTML + xhr.responseText;
                    loadPicNow();
                } else {
                    alert("请求数据异常");
                }
            }
        });

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


    function seach() {
        var Cont = document.getElementById("theList");
        var seach = document.getElementById("keyword").value;
        var ajaxdata = "page=" + 0 + "&seach=" + seach;
        Ajax.request('/ajax/index_seach.ashx', {
            data: ajaxdata,
            method: "GET",
            success: function (xhr) {
                if (xhr != "-1") {
                    Cont.innerHTML = xhr.responseText;
                } else {
                    alert("请求数据异常");
                }
            }
        });
    }	 

	 
</script>

</body>
</html>
