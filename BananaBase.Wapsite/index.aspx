<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Banana.Wapsite.index" %>

<%@ Import Namespace="Banana.Wapsite" %>
<%@ Import Namespace="Banana.Wapsite.Common" %>
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
    <link href="/css/forIndex.css" rel="stylesheet" type="text/css">
    <script src="/js/baidu.js"></script>
</head>
<body>
    <div class="searchbg">
        <div class="searchBox bdradius6 clf">
            <input type="button" onclick="seach()" class="searchBtn Bg50" value="" />
            <input type="text"  name="keyword" id="keyword" class="searchTxt" placeholder="Banana情趣商品搜索" />
        </div>
    </div>
    <div class="ptypeBox clf">
        <ul>
            <li><a href="prodlist.aspx?type=男性用品" class="type01"><span><i></i></span>男性用品</a> </li>
            <li><a href="prodlist.aspx?type=女性用品" class="type02"><span><i></i></span>女性用品</a> </li>
            <li><a href="prodlist.aspx?type=延时锻炼" class="type03"><span><i></i></span>延时锻炼</a> </li>
            <li><a href="prodlist.aspx?type=情趣服饰" class="type04"><span><i></i></span>情趣服饰</a> </li>
            <li><a href="prodlist.aspx?type=调情助兴" class="type05"><span><i></i></span>调情助兴</a> </li>
            <li><a href="prodlist.aspx?type=套套" class="type06"><span><i></i></span>套套</a> </li>
            <li><a href="temai.aspx" class="type07"><span><i></i></span>限时折扣</a> </li>
            <li><a href="new.aspx" class="type08"><span><i></i></span>最新上架</a> </li>
        </ul>
    </div>
    <div class="CommandBox clf">
        <a href="<%=banner.Linkurl %>">
            <img src="/<%=banner.Bannerimg %>" alt="/<%=banner.Bannerimg %>" class="lazy"></a></div>
    <div class="block">
        <div class="indextop">
            <a href="remai.aspx"><span class="Bg50">热卖推荐</span></a>
        </div>
        <div class="temaiBox">
            <ul class="clf">
                <asp:Repeater ID="TodayDaZhe" runat="server">
                    <ItemTemplate>
                        <li class="bdradius6"><a href="shop_list_detail.aspx?pro_id=<%#Eval("id") %>">
                            <img src="<%# Eval("SmallThumPic") %>" alt="/<%# ApplicationSettings.Get("imgurl")+Eval("SmallThumPic") %>"
                                class="lazy"><span class="ptitle"><%#Eval("ProductName")%></span><span class="price"><i
                                    class="red">￥<%#Eval("OemPrice").ToString().Split('.')[0]%></i>￥<%#Eval("MarketPrice").ToString().Split('.')[0]%></span></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
    <asp:Repeater ID="BannerList" runat="server">
        <ItemTemplate>
            <div class="block">
                <div class="indextop">
                    <a href="prodlist.aspx?type=<%#Eval("Description").ToString().Trim2() %>"><span class="Bg50">
                        <%#Eval("Description").ToString().Trim2()%></span></a>
                </div>
                <asp:Repeater ID="Repeater2" runat="server" DataSource='<%#  BananaTop(Eval("Id").ToString()) %>'>
                    <ItemTemplate>
                        <div class="adBanner clf">
                            <a href="<%#Eval("linkurl") %>">
                                <img src="/<%#  ApplicationSettings.Get("imgurl")+Eval("Bannerimg") %>" alt="/<%#  ApplicationSettings.Get("imgurl")+Eval("Bannerimg") %>"
                                    class="lazy"></a></div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="adList">
                    <ul class="clf">
                        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#  BananaList(Eval("Id").ToString()) %>'>
                            <ItemTemplate>
                                <li><a href="<%#Eval("linkurl") %>">
                                    <img src="/<%#  ApplicationSettings.Get("imgurl")+Eval("Bannerimg") %>" alt="/<%#  ApplicationSettings.Get("imgurl")+Eval("Bannerimg") %>"
                                        class="lazy">
                                    <%#Eval("recommendedinfo")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="prodlist" id="prodlist">
        <div class="indextop">
            <a href="#"><span class="Bg50">猜你喜欢</span></a>
        </div>
        <ul id="theList" class="clf">
            
        </ul>
        <div class="loadingdata">数据加载中请稍候!</div>
    </div>
    <div class="RightMenu">
       <a href="/mine.aspx" class="user Bg50"></a> <a href="/orderinfo.aspx" class="cart Bg50"><span class="bdradius6" id="cart">0</span></a> <a href="#" class="gotop Bg50">
        </a>
    </div>
    <script src="/js/loadimage.js" language="javascript" type="text/jscript"></script>
    <script src="/js/loadingDate1.js" language="javascript" type="text/jscript"></script>
    <script src="/js/ajaxhelp.js" type="text/javascript"></script>
    <script>
        var canScroll = true;
        window.onload = function () {
            Getcainixihuan();
            loadPicNow();
            loadcook();
            window.onscroll = function () {
                ScrollloadImage();
                loadingData("theList", "index_cainixihuan");

            };
            function loadcook() {

                Ajax.request('/ajax/getgouwuchecount.ashx', {
                    method: "GET",
                    success: function (xhr) {
                        //to do with xhr<a href="ajax/test.ashx">ajax/test.ashx</a>
                        document.getElementById("cart").innerHTML = xhr.responseText;

                    }
                });

            }

            function Getcainixihuan() {
                var Cont = document.getElementById("theList");
               var ajaxdata = "page=" + 0;
                Ajax.request('/ajax/index_cainixihuan.ashx', {
                    data: ajaxdata,
                    method: "GET",
                    success: function (xhr) {
                        if (xhr != "-1") {
                            Cont.innerHTML = Cont.innerHTML + xhr.responseText;
                        } else {
                            alert("请求数据异常");
                        }
                    }
                });

            }

        }

        function seach() {
            var text = document.getElementById("keyword").value;
            window.location.href = "search.aspx?seach=" + text;
        }
    </script>
</body>
</html>
