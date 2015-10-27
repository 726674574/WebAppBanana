<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop_list_detail.aspx.cs" Inherits="Banana.Wapsite.shop_list_detail" %>
<%@ Import Namespace="Banana.Wapsite" %>
<%@ Import Namespace="Banana.Wapsite.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="utf-8">
<meta content="width=device-width,height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
<meta name="description" content="banana情趣商城，是一家专卖成人两性情趣用品的特卖站，在线销售杜蕾斯、冈本、LELO等国际一线品牌男女情趣用品、情趣内衣、安全套、人体润滑剂等数千款优质性用品">
<meta name="keywords" content="飞机杯,跳蛋,震动棒,情趣内衣,套套,催情迷情用品 成人用品,情趣用品,性用品,两性用品,成人网站"/>
<title><%=title%></title>
<link href="/css/basic.css" rel="stylesheet" type="text/css">
<link href="/css/shop.css" rel="stylesheet" type="text/css">
<script src="/js/baidu.js"></script>
</head>
<body>

<div class="header" style="background:rgba(255,255,255,.8);">
  <div class="top" style="background:none"><a href="javascript:window.history.go(-1);;"  class="Bg50 back"></a>图文详情<a style="display:none;" href="#" onClick="likeit(this,<%=p.Id %>)" class="Bg50 <%=islike %>"></a></div>
</div>

<div id="Detail01">
<div class="detailTop clf" style="margin-bottom:1px;">
  <img src="/<%=ApplicationSettings.Get("imgurl") + p.BigThumPic %>" alt="/<%=ApplicationSettings.Get("imgurl") + p.BigThumPic %>" class="lazy">
  <a href="javascript:;" class="playBtn" id="playBtn">播放视频</a>
  <div class="videoBox" id="videoBox">
    <video id="det_video" src="/<%=p.VideoUrl %>" style=""  controls></video></div>
    <input type="hidden" id="Isnotvideo" value=<%=Isnotvideo %> />
  <div class="Pdetail clf">
    <h2><%=p.ProductName %></h2>
    <h3 class="Mtop10"><span class="red">￥<%=p.OemPrice.ToString().Split('.')[0]%></span> <span class="yj">￥<%=p.MarketPrice.ToString().Split('.')[0] %></span></h3>
    <p class="Mtop10">销量：<%=p.Sale ?? new Random().Next(500)%></p>
  </div>
</div>
<div class="block01"><p><b></b>材质安全</p><p><b></b>保证正品</p><p><b></b>隐私包装</p></div>


<div class="block">
  <div class="detailBox clf">
 
      <asp:Repeater ID="DetailPic" runat="server">
          <ItemTemplate>
               <img src="/images/smallPic.png" alt="/<%# ApplicationSettings.Get("imgurl")+Eval("ImgUrl") %>" class="lazy">
          </ItemTemplate>
      </asp:Repeater>

  <a name="d02" id="d02"></a>
  </div>
</div>
</div>


<div class="prodlist" id="prodlist" style="margin:10px 0 30px 0">
  <div class="indextop">
     <a href="#">相关推荐</a>
  </div>
  <ul id="theList" class="clf">
   
      <asp:Repeater ID="ProductDetailTuiJian" runat="server">
      <ItemTemplate>
           <li class="bdradius6"><a href="shop_list_detail.aspx?pro_id=<%#Eval("id") %>">
          <span class="smallImgbox"><img src="/images/smallPic.png" alt="/<%# ApplicationSettings.Get("imgurl") +Eval("BigThumPic") %>" class="lazy" ></span>
          <span class="prodTitle"><%#Eval("ProductName")%></span>
          <span class="prodPrice clf"> <%#Eval("OemPrice").ToString().Split('.')[0].ToInt() > 199 ? "<i class=\"fl\">包邮</i>" : ""%>  <i class="fl">已售 <%#Eval("Sale") ?? new Random().Next(500)%></i></span>
          <span class="prodPrice clf"><i class="red fl">￥<%#Eval("OemPrice").ToString().Split('.')[0]%></i> <i class="fl">￥<%#Eval("MarketPrice").ToString().Split('.')[0]%></i></span>
        </a></li>
      </ItemTemplate>
      </asp:Repeater>
  </ul>

</div>


<div class="footer">
  <div class="btns" id="btns">
  <a href="<%=huodaofukuanlink %>" onclick="addShop1(<%=pro_id %>)" id="huodao" class="BtnZ01 fl">货到付款</a>
  <a href="javascript:;" onclick="addShop(<%=pro_id %>)" class="BtnZ02 fl">加入购物车</a>
  </div>
</div>


<div class="RightMenu">
  <a href="/orderinfo.aspx" class="cart Bg50" id="menu"><span class="bdradius6" id="cart">0</span></a>
</div>

<input id="isLinepayment"  type="hidden" value=<%=isLinepayment %> />
<div id="JoinCart">已加入购物车</div>


 <script src="/js/loadimage.js" language="javascript" type="text/jscript"></script>
 <script src="/js/loadingDate.js" language="javascript" type="text/jscript"></script>
    <script src="/js/ajaxhelp.js" type="text/javascript"></script>
<script>
    window.onload = function () {
        loadPicNow();
        loadcook();
        var isnotvideo = document.getElementById("Isnotvideo");
        if (isnotvideo.value == "False") {
            playBtn.style.display = "none";
        }
        window.onscroll = function () {
            ScrollloadImage();
            //loadingData("theList");

        };

    }

    function loadcook() {
       
        Ajax.request('/ajax/getgouwuchecount.ashx', {
            method: "GET",
            success: function(xhr) {
                //to do with xhr<a href="ajax/test.ashx">ajax/test.ashx</a>
                document.getElementById("cart").innerHTML = xhr.responseText;

            }
        });

    }

    function changeClass(itemid) {
        var btn01 = document.getElementById("btn01");
        var btn02 = document.getElementById("btn02");
        var btnNow = document.getElementById("btn0" + itemid);
        btn01.className = '';
        btn02.className = '';
        btnNow.className = 'now';
    }

    function likeit(obj, itemId) {

        if (obj.className == "Bg50 like") {
            obj.className = 'Bg50 liked';
            /* 插入加入收藏代码*/
            addcancelsc("add", itemId);
        }
        else {
            obj.className = 'Bg50 like';
            /* 插入取消收藏代码*/
            addcancelsc("del", itemId);
        }

    }
    function addcancelsc(typestr, proid) {
        var ajaxdata = "";
        ajaxdata += "type=" + typestr + "&id=" + proid;
        Ajax.request('/ajax/min_sc_delbyid.ashx', {
            data: ajaxdata,
            method: "GET",
            success: function (xhr) {
                //to do with xhr<a href="ajax/test.ashx">ajax/test.ashx</a>
                if (xhr.responseText == "1") {
                    if (typestr == "add") {
                        alert("收藏成功！");
                    } else {
                        alert("取消成功！");
                    }
                    // 0时用户末登录指定登录

                } else if (xhr.responseText == "-2") {
                    window.location.href = "/mine.aspx";
                }

                else {
                    alert("请求数据异常");
                }

            },
            failure: function (xhr) {
                //to do with xhr
            }
        }
            );
    }
    

    var playBtn = document.getElementById("playBtn");
    var videoBox = document.getElementById("videoBox");
    var det_video = document.getElementById("det_video");
    playBtn.addEventListener("click", function (e) {

        videoBox.style.display = "block";
        det_video.play();


    });


    function JoinCart() {
        document.getElementById("JoinCart").className = "JoinCart";
        setTimeout(function () { document.getElementById("JoinCart").className = "" }, 2100);

        if (document.getElementById("cart").innerHTML == "") {
            cartCount = 1;
        }
        else {
            cartCount = parseInt(document.getElementById("cart").innerHTML) + 1;
        }
        document.getElementById("cart").innerHTML = cartCount;
    }
    function addShop(productId) {
        
        var ajaxdata = "";
        ajaxdata += "productId=" + productId;
        Ajax.request('/ajax/order_add_shop.ashx', {
            data: ajaxdata,
            method: "GET",
            success: function (xhr) {
                //to do with xhr<a href="ajax/test.ashx">ajax/test.ashx</a>
                if (xhr.responseText == "1") {

                    // 添加成功
                    JoinCart();
                }
                else if (xhr.responseText == "0") {
                    alert("不能重复添加");
                } else {
                    alert("加入购物车失败");
                }

            },
            failure: function (xhr) {
                //to do with xhr
            }
        }
            );

    }
    function addShop1(productId) {

        var ajaxdata = "";
        ajaxdata += "productId=" + productId;
        Ajax.request('/ajax/order_add_shop.ashx', {
            data: ajaxdata,
            method: "GET",
            success: function (xhr) {
            },
            
        }
            );

    }

	   var isLinepayment = document.getElementById("isLinepayment").value;
    if (isLinepayment=="False") {
        document.getElementById("huodao").innerHTML="立即购买";
    }  
	   
</script>

</body>
</html>
