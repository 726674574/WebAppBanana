<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCheck.aspx.cs" Inherits="Banana.Wapsite.OrderCheck1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,minimum-scale=1,user-scalable=no">
    <meta content="telephone=no" name="format-detection">
    <meta name="apple-touch-fullscreen" content="yes">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Banana商城-订单详情</title>
    <link href="css/base.css" type="text/css" rel="stylesheet">
    <link href="css/myaddress.css" type="text/css" rel="stylesheet">
    <link href="css/mycart.css" type="text/css" rel="stylesheet">

</head>
<body>
    <header id="common_hd" class="c_txt rel"><a id="hd_back" class="abs comm_p8" href="javascript:history:go(-1);">返回</a>
<h1 class="hd_tle red_txt" id="titlename">恭喜订单提交成功!</h1></header>
    
    <section class="cart_sec">
      <div class="check_wrap">
        <div class="check01">
          <p>
    为保证宝贝能准确送达，稍后美女客服将电话联系确认收货信息。</p>
         <p>如果您不方便接电话，请自行<span>短信验证</span></p>
      </div>
      <div class="check02" id="checksucess" style="display:none">
          <span>短信验证成功</span>，我们将今天尽快为您发货！<br>
          预计<span id="sendtime"></span>送达,具体以快递公司联系为准！
        </div>
        
        <div class="check02" id="checkfail" style="padding-bottom:0; display:none">
          <span>短信验证失败</span>，请重新输入
        </div>
        <div class="check02" id="checkfail2" style="padding-bottom:0; display:none">
          <span>验证短信失效</span>，请重新获取验证码
        </div>
        
       
      
       
        
        <div class="check02" id="checkForm">
          
          <p><input  id="btn_yzm" type="button" class="w100" value="获取短信验证码"></p>
          <p><input id="ipt_yzm" type="text" class="w60" placeholder="输入短信验证码" value="输入短信验证码" onFocus="this.value=''"><input id="btn_yz" type="button" class="w40" value="验证"></p>
        </div>
   </div>

</section>
    
    <section class="cart_sec"> 
      
      <div class="hide" style="display: block;">
      <div class="cart_list_title rel">订单信息</div>
      <div class="cart_seller_wrap">
        <ul class="cart_ul01">
      
        <li>
        订单号：<%=orderEntity.OrderNo %></li>
        <li>
        收货信息：<%=orderEntity.ReciverName %>，<%=orderEntity.RevicerTel %>，<%=orderEntity.RevicerAddress %></li>
        <li>
        总计货款：¥<%=((decimal)orderEntity.TotalPrice).ToString("f0") %></li>
        <li>
        支付方式：货到付款</li>
    
    </ul></div></div>
    </section>

    <section class="cart_sec"> 
      
      <div id="cart_seller_list" class="hide" style="display: block;">
      <div class="cart_list_title rel" id="address_title">商品清单</div>
      <div class="cart_seller_wrap isBuyNowOrMico"><ul class="cart_ul">
      <%foreach (var o in orderListEntity)
        { %>
      <li class="cart_li rel" style="height:auto">
      <%=GetProName(o.Productid.Value) %><div class="control_count">
      <div class="left">¥<span class="i_pri"><%=((decimal)o.OemPrice).ToString("f0") %></span> 数量：<span class="i_pri"><%=o.Count %></span> 小计：¥<span class="t_pri"><%=((decimal)(o.OemPrice*o.Count)).ToString("f0") %></span> </div></div>
      </li>
      <%} %>
      
      
      </ul>
      </div></div>
    </section>    
    
    <nav class="buybtn"><a href="index.aspx" class="btnok">继续购物</a></nav>
    
</body><script src="js/ajaxhelp.js" type="text/javascript"></script><script>
    var Delaytime;
    var Delayval;
    btn_yzm.addEventListener("click", function() {

        document.getElementById("checkfail2").style.display = "none";
        document.getElementById("checkfail").style.display = "none";
        Delaytime = 180;
        document.getElementById("btn_yzm").disabled = true;
        var ajaxdata = "";
        ajaxdata += "tel=<%=orderEntity.RevicerTel %>&oid=<%=OrderId %>";
        Ajax.request('ajax/yzm.ashx', {
                data: ajaxdata,
                method: "GET",
                success: function(xhr) {
                    if (xhr.responseText == "1") {
                        Delayval = window.setInterval(SetRemainTime, 1000);
                    } else if (xhr.responseText == "-3") {
                        document.getElementById("btn_yzm").disabled = false;
                        alert("图片验证码错误");
                    } else {
                        document.getElementById("btn_yzm").disabled = false;
                        alert("获取验证码失败");
                    }
                },
                failure: function(xhr) {
                    //to do with xhr
                }
            }
        );

        });
        function SetRemainTime() {
            if (Delaytime > 0) {
                document.getElementById("btn_yzm").value = Delaytime + "秒后重新获取验证码";
                Delaytime = Delaytime - 1;
            } else {
                window.clearInterval(Delayval);
                document.getElementById("btn_yzm").value = "点击重新获取验证码";
                document.getElementById("btn_yzm").disabled = false;
            }
        }

    btn_yz.addEventListener("click", function() {
        document.getElementById("btn_yz").disabled = true;
        var ajaxdata = "";
        ajaxdata += "y=" + document.getElementById("ipt_yzm").value + "&oid=<%=OrderId %>";
        Ajax.request('ajax/yz.ashx', {
                data: ajaxdata,
                method: "GET",
                success: function(xhr) {
                    if (xhr.responseText == "1") {
                        //验证成功
                        document.getElementById("sendtime").innerHTML = "2天后";
                        document.getElementById("checksucess").style.display = "block";
                        document.getElementById("checkfail").style.display = "none";
                        document.getElementById("checkfail2").style.display = "none";
                        document.getElementById("checkForm").style.display = "none";


                    } else if (xhr.responseText == "0") {
                        //显示错误
                        document.getElementById("checkfail").style.display = "block";
                        document.getElementById("checkfail2").style.display = "none";
                        document.getElementById("btn_yz").disabled = false;
                    } else {
                        //显示错误
                        document.getElementById("checkfail2").style.display = "block";
                        document.getElementById("checkfail").style.display = "none";
                        document.getElementById("btn_yz").disabled = false;
                    }
                },
                failure: function(xhr) {
                    //to do with xhr
                }
            }
        );

    });
</script>
</html>
