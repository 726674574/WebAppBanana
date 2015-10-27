<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mine.aspx.cs" Inherits="Banana.Wapsite.mine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="utf-8">
<meta content="width=device-width,height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
<meta name="full-screen" content="yes">
<meta name="x5-fullscreen" content="true" />
<title>我的订单</title>
<link href="/css/basic.css" rel="stylesheet" type="text/css">
<link href="/css/mine.css" rel="stylesheet" type="text/css">


</head>
<body>
<div class="header">
  <div class="top"><a href="javascript:history.go(-1);"   class="Bg50 back"></a>我的订单</div>
  <div class="submenu"><a href="javascript:;" class="on" onClick="showThistype(this)">待付款</a><a href="javascript:;" onClick="showThistype(this)">待发货</a>   <a href="javascript:;" onClick="showThistype(this)">已完结</a></div>
</div>



<!---小组列表-->
<div style="margin:110px 0 0 0 ">
  <ul class="plist fororder" id="theList">
   
      
    </ul>
  </div>

  
<script src="/js/loadimage.js" language="javascript" type="text/jscript"></script>
 <script src="/js/loadingDate.js" language="javascript" type="text/jscript"></script>
  <script src="/js/ajaxhelp.js" type="text/javascript"></script>
<script>
    var canScroll = true;
    window.onload = function () {
        loadPicNow();
        getOrder();
        window.onscroll = function () {
            ScrollloadImage();
            loadingData("theList", "index_mine");

        };
    };

    function getOrder() {
        var Cont = document.getElementById("theList");
        var type = "待付款";
        var ajaxdata = "page=" + 0 + "&order=" + type;
        Ajax.request('/ajax/index_mine.ashx', {
            data: ajaxdata,
            method: "GET",
            success: function (xhr) {
                if (xhr.responseText != "-1") {
                    Cont.innerHTML =  xhr.responseText;
                } else {
                    alert("请求数据异常");
                }
            }
        });
    }
    
    function showThistype(elm) {
        elm.parentNode.getElementsByClassName("on")[0].classList.remove('on');
        elm.classList.add("on");
        var type = elm.text;
        var Cont = document.getElementById("theList");
        var ajaxdata = "page=" + 0 + "&order=" + type;
        Ajax.request('/ajax/index_mine.ashx', {
            data: ajaxdata,
            method: "GET",
            success: function (xhr) {
                if (xhr.responseText != "-1") {
                    Cont.innerHTML = xhr.responseText;
                } else {
                    alert("请求数据异常");
                }
            }
        });
    }

    function btdelete(obj) {
        if (confirm("确认要删除？")) {
            var ajaxdata = "id="+obj+"&act=delete";
            Ajax.request('/ajax/index_mine.ashx', {
                data: ajaxdata,
                method: "GET",
                success: function (xhr) {
                    if (xhr.responseText == "1") {
                        location.reload();
                    } else {
                        alert("请求数据异常");
                    }
                }
            }); 

        }
    }


</script>


</body>
</html>
