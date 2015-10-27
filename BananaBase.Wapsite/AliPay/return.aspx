<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="return.aspx.cs" Inherits="Banana.Wapsite.AliPay._return" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,minimum-scale=1,user-scalable=no">
    <meta content="telephone=no" name="format-detection">
    <meta name="apple-touch-fullscreen" content="yes">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>提交订单</title>
    <link href="/order/css/base.css" type="text/css" rel="stylesheet">
    <link href="/order/css/myaddress.css" type="text/css" rel="stylesheet">
    <link href="/order/css/mycart.css" type="text/css" rel="stylesheet">
    <script src="/order/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            var state = $("#State").val();
            if (state == 1) {
               
                $("#payerror").hide();
            } else {
                $("#paysucess").hide();
            }
        });
    

        function btnok() {
            var orderId = $("#Number").val();
            var backUrl = $("#backUrl").val();
            window.location.href = "../topay.aspx?&number=" + orderId + "&backUrl=" + backUrl;
          
        }

    </script>
</head>
<body>
    <input type="hidden" id="State" value="<%=State %>">
    <input type="hidden" id="Number" value="<%=Number %>">
    <input type="hidden" id="backUrl" value="<%=BackUrl %>">
    <div id="paysucess">
        <header id="common_hd" class="c_txt rel"><a id="hd_back" class="abs comm_p8" href="/mine.aspx">返回</a> 
            <h1 class="hd_tle" id="titlename">支付成功</h1></header>
        <section class="cart_sec" id="address_sec">
                  <div class="cart_wrap" id="address_wrap" style=""><div id="address_form" class="hide" style="display: block;">
                  <p class="payment_p">
                    <span>您的单号：<%=Number%></span>
                    <span><br>
                    我们将在24小时内为您发货！</span><br>
                    <span>客服电话：18768190602 </span> 
                    </div></div></section>
        <nav class="buybtn" style="position: fixed; bottom: 0; width: 100%; left: 0; z-index: 100000000"><a href="/index.aspx"                       class="btnok">继续购物</a></nav>
    </div>
    <div id="payerror" >
        <body>
            <header id="common_hd" class="c_txt rel"><a id="hd_back" class="abs comm_p8" href="/mine.aspx">返回</a> 
        <h1 class="hd_tle" id="H1">支付失败</h1></header>
            <section class="cart_sec" id="address_sec">
              <div class="cart_wrap" id="Div1" style=""><div id="Div2" class="hide" style="display: block;">

              <p class="payment_p">由于*******问题导致支付失败，请重新支付，谢谢！</div></div></section>
            <nav  class="buybtn" style="position: fixed; bottom: 0; width: 100%; left: 0; z-index: 100000000"><a href="javascript:void(0)"           onclick="btnok()"  class="btnok">重新支付</a></nav>
        </body>
    </div>
</body>
</html>
