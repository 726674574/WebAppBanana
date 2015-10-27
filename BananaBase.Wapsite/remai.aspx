﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="remai.aspx.cs" Inherits="Banana.Wapsite.remai" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta content="width=device-width,height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0"
        name="viewport" />
    <meta name="description" content="banana情趣商城，是一家专卖成人两性情趣用品的特卖站，在线销售杜蕾斯、冈本、LELO等国际一线品牌男女情趣用品、情趣内衣、安全套、人体润滑剂等数千款优质性用品">
    <meta name="keywords" content="飞机杯,跳蛋,震动棒,情趣内衣,套套,催情迷情用品 成人用品,情趣用品,性用品,两性用品,成人网站" />
    <title>banana情趣商城</title>
    <link href="/css/basic.css" rel="stylesheet" type="text/css">
    <link href="/css/shop.css" rel="stylesheet" type="text/css">
</head>
<body>
    <div class="header">
        <div class="top">
            <a href="index.aspx" class="Bg50 back"></a>热卖推荐<a href="#" class="Bg50 search"></a></div>
        <div class="submenu">
            <a href="javascript:;" class="on" onclick="showThistype(this)">推荐</a><a href="javascript:;"
                onclick="showThistype(this)">价格<span>&uarr;</span></a> <a href="javascript:;" onclick="showThistype(this)">
                    销量<span>&uarr;</span></a> <a href="javascript:showfl();">分类</a></div>
    </div>
    <div class="prodlist" id="prodlist" style="margin-top: 100px">
        <ul id="theList" class="clf">
        </ul>
        <div class="loadingdata">数据加载中请稍候!</div>
    </div>
    <div class="flbox" id="flbox">
        <div class="flcont" id="animate01">
            <ul>
               <%=loadCategory()%>
            </ul>
        </div>
        <a href="javascript:closeIt();" class="flout"><span>点击关闭</span></a>
    </div>
    <div class="RightMenu">
        <a href="/orderinfo.aspx" class="cart Bg50"><span class="bdradius6" id="cart">0</span></a> <a href="#"
            class="gotop Bg50"></a>
    </div>
    <script src="/js/loadimage.js" language="javascript" type="text/jscript"></script>
    <script src="/js/loadingDate1.js" language="javascript" type="text/jscript"></script>
    <script src="/js/ajaxhelp.js" type="text/javascript"></script>
    <script>
        var canScroll = true;
        window.onload = function () {

            getremai();
            loadPicNow();
            loadcook();
            window.onscroll = function () {
                ScrollloadImage();
                loadingData("theList", "index_remai");
                temp1 = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
                ScrollloadImage();
                loadingData("theList", "index_remai");
                if (temp1 <= 50) {
                    document.getElementsByClassName("header").item(0).style.top = -temp1 + "px";
                }
                else {
                    document.getElementsByClassName("header").item(0).style.top = "-50px";
                }
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

        function getremai() {
            var order = document.getElementsByClassName("on")[0].text;
            var Cont = document.getElementById("theList");
            var ajaxdata = "page=" + 0 + "&order=" + order;
            Ajax.request('/ajax/index_remai.ashx', {
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

        function showThistype(elm) {
            elm.parentNode.getElementsByClassName("on")[0].classList.remove('on');
            elm.classList.add("on");
            if (elm.classList.contains("asc")) {
                elm.getElementsByTagName('span')[0].innerHTML = "&uarr;"
                elm.classList.add("desc"); elm.classList.remove("asc");
            }
            else if (elm.classList.contains("desc")) {
                elm.getElementsByTagName('span')[0].innerHTML = "&darr;"
                elm.classList.add("asc"); elm.classList.remove("desc");
            }
            else if (!elm.classList.contains("desc") & !elm.classList.contains("asc")) {
                elm.getElementsByTagName('span')[0].innerHTML = "&uarr;"
                elm.classList.add("desc");
            }
            var order = elm.text;
            var Cont = document.getElementById("theList");
            var ajaxdata = "page=" + 0 + "&order=" + order;
            Ajax.request('/ajax/index_remai.ashx', {
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
