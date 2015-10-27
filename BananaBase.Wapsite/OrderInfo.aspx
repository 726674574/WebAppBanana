<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.aspx.cs" Inherits="Banana.Wapsite.OrderInfo1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html >
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,minimum-scale=1,user-scalable=no">
    <meta content="telephone=no" name="format-detection">
    <meta name="apple-touch-fullscreen" content="yes">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Banana商城</title>
    <link href="/css/base.css" type="text/css" rel="stylesheet">
    <link href="/css/myaddress.css" type="text/css" rel="stylesheet">
    <link href="/css/mycart.css" type="text/css" rel="stylesheet">
</head>
<body>
    <header id="common_hd" class="c_txt rel"><a id="hd_back" class="abs comm_p8" href="javascript:void(0);" onclick="javascript:window.history.go(-1);">返回</a>
<h1 class="hd_tle" id="titlename">Banana商城 订购确认</h1></header>
    <section class="cart_sec">
    <div class="cart_wrap">
      <div class="cart_list_title rel">收货人信息</div>
      <div id="address_form" class="hide" style="display: block;">
        <p class="address_p rel"><input name="nam" type="text" class="block input" id="nam" placeholder="收货人" tabindex="1" value="" onFocus="this.value=''"></p>
        <p class="address_p rel"><input name="tele" type="tel" class="block input" id="tele" placeholder="手机号码" tabindex="2" maxlength="11" value="" onFocus="this.value=''"></p>
        <p class="address_p rel">
            <select id="province" name="province" class="select left" tabindex="3"><option value="-1">--省份--</option><option value="0">北京</option><option value="1">安徽</option><option value="2">福建</option><option value="3">甘肃</option><option value="4">广东</option><option value="5">广西</option><option value="6">贵州</option><option value="7">海南</option><option value="8">河北</option><option value="9">河南</option><option value="10">黑龙江</option><option value="11">湖北</option><option value="12">湖南</option><option value="13">吉林</option><option value="14">江苏</option><option value="15">江西</option><option value="16">辽宁</option><option value="17">内蒙古</option><option value="18">宁夏</option><option value="19">青海</option><option value="20">山东</option><option value="21">山西</option><option value="22">陕西</option><option value="23">上海</option><option value="24">四川</option><option value="25">天津</option><option value="26">西藏</option><option value="27">新疆</option><option value="28">云南</option><option value="29">浙江</option><option value="30">重庆</option><option value="31">香港</option><option value="32">澳门</option><option value="33">台湾</option></select>
            <select id="city" name="city" class="select right" tabindex="4"><option value="-1">--城市--</option></select>
        </p>
        <p class="address_p rel"><input name="detail_add" id="detail_add" placeholder="输入详细地址,宝贝带回家！" class="block input" tabindex="6" value="" onFocus="this.value=''"></p>
      </div>
    </div>
    
</section>
    <!--<section class="cart_sec">
    <div class="cart_wrap">
      <div class="cart_list_title rel">香蕉积分兑换 (<span class="red_txt"> 10香蕉=1人民币 </span>)</div>
      <div class="hide" style="display: block;">
      <p class="payment_p">
        <label>你有<span class="red_txt">500香蕉</span> ，最多可抵 <span class="red_txt">￥50</span></label><br>

        <label><input name="payment01" type="radio" value="" >25香蕉</label>
        <label><input name="payment01" type="radio" value="" >50香蕉</label>
        <label><input name="payment01" type="radio" value="" >100香蕉</label>
        <label><input name="payment01" type="radio" value="">200香蕉</label>
 <br>         
<span class="gray_txt">注： 最多可抵扣10%的货款</span></p>
      </div>
    </div>
</section>-->
    <section class="cart_sec" id="cart_desc"> 
      <div class="cart_wrap">
        <div class="cart_seller_wrap isBuyNowOrMico">
          <ul class="cart_ul">
           <% foreach (var p in productList)
              { 
           %>
          <li class="cart_li rel"><a href="shop_list_detail.aspx?pro_id=<%=p.Id %>" class="cart_img abs"><img src="<%=Banana.Wapsite.ApplicationSettings.Get("imgurl")+p.SmallThumPic %>" width="61" height="60"></a><a href="shop_list_detail.aspx?pro_id=<%=p.Id %>" id="uname" class="cart_tle over_hidden"><%=p.ProductName %> <br>
          </a>
            <p class="cart_cls over_hidden ellipsis">价格：¥<span class="i_pri"><%=((decimal)p.OemPrice).ToString("f0")%></span> 小计：¥<span class="t_pri"><%=((decimal)p.OemPrice).ToString("f0")%></span></p>
          <div class="control_count ellipsis cart_cls" data-stock="2000" data-current-num="1">
          <div class="left"><span class="delbtn" myAttr="<%=p.Id %>">删除</span></div><div class="control_num right c_txt bold rel"> <em class="control_num_sub abs">－</em> <input type="tel" class="item_num bold c_txt block" value="1" name="item_num" data="<%=p.Id %>"> <em class="control_num_add abs">＋</em></div></div></li>
          <%} %>
        
        
          </ul>
  </div></div>
</section>
    <section class="cart_sec">
    <div class="cart_wrap">
      <div class="cart_list_title rel">支付方式</div>
      <div id="address_form" class="hide" style="display: block;">
        <p class="payment_p"><label><input name="payment" id="zhifubao" type="radio" value="支付宝支付" class="left" style="margin-top:8px;"> 
        <span class="left" style="margin-left:5px;">支付宝支付</span><span class="red_txt right">满69包邮</span></label><label id="huodao"><input name="payment" type="radio" value="货到付款" checked class="left" style="margin-top:8px;"> 
        <span class="left" style="margin-left:5px;">货到付款</span><span class="red_txt right">满199包邮</span></label> </p>
      </div>
    </div>
</section>
    <section class="cart_sec" >
  <div class="cart_wrap">
<div class="payment_p01"> <p class="left">立省 ¥<span class="red_txt" id="bananaCount">0</span></p> <p class="right"><label class="y_btn"><input name="" type="checkbox" id="checkbox" value="15" onChange=""> 使用香蕉积分</label>
</p></div></div>
</section>
    <!--<section class="cart_sec">
  <div class="cart_wrap">
<div class="payment_p01"> <p class="left">立省 <span class="red_txt">¥5</span></p> <p class="right"><a href="#" class="y_btn">下载Banana!客户端</a></p></div></div>
</section> -->
    <section class="cart_sec" style="padding-bottom: 60px;">
  <div class="cart_wrap">
<div class="payment_p">配送费：¥<span class="red_txt" id="peisong">15</span></div></div>
</section>
    <nav class="buybtn" style="position: fixed; bottom: 0; width: 100%; left: 0; z-index: 100000000"><a href="javascript:void(0)" id="js_okBtn" class="btnok">总计：¥<span id="total">123</span> &nbsp;&nbsp; 提交订单</a><a href="javascript:void(0)" id="js_sending"  class="btnok" style="display:none;">订单提交中...</a></nav>
     <span id="sppcity" datap="<%=userp %>" datac="<%=userc %>" istj="0"></span>
      <input type="hidden"  id="isLinepayment" value="<%=isLinepayment %>" />
</body>
<script src="/js/address.js" type="text/javascript"></script>
<script src="/js/ajaxhelp.js" type="text/javascript"></script>
<script src="/js/zepto.min.js" type="text/javascript"></script>
<script>  
 
  var isLinepayment = document.getElementById("isLinepayment").value;
    if (isLinepayment=="False") {
        document.getElementById("huodao").style.display = 'none';
        document.getElementById("zhifubao").checked = true;

    }

    function getSelectedText(name) {
        var obj = document.getElementsByName(name);
        for (i = 0; i < obj.length; i++) {
            if (obj[i].checked == true) {
                return obj[i].value;
            }
        }
    }

    total();
    var addNum = document.getElementsByClassName('control_num_add');
    var subNum = document.getElementsByClassName('control_num_sub');
    var item_num = document.getElementsByName("item_num");
    var delbtn = document.getElementsByClassName('delbtn');




    var js_okBtn = document.getElementById('js_okBtn');


    for (i = 0; i < addNum.length; i++) {

        addNum[i].addEventListener('click', function () {
            var num_count = parseFloat(this.parentNode.getElementsByTagName("input")[0].value);
            num_count++;
            this.parentNode.getElementsByTagName("input")[0].value = num_count;
            var price = parseFloat(this.parentNode.parentNode.parentNode.getElementsByClassName("i_pri")[0].innerHTML);
            this.parentNode.parentNode.parentNode.getElementsByClassName("t_pri")[0].innerHTML = price * num_count;
            total();

        });
        subNum[i].addEventListener('click', function () {

            var num_count = parseFloat(this.parentNode.getElementsByTagName("input")[0].value);

            if (num_count >= 2) {
                num_count--;
                this.parentNode.getElementsByTagName("input")[0].value = num_count;
                var price = parseFloat(this.parentNode.parentNode.parentNode.getElementsByClassName("i_pri")[0].innerHTML);
                this.parentNode.parentNode.parentNode.getElementsByClassName("t_pri")[0].innerHTML = price * num_count;
            }
            total();

        });

        delbtn[i].addEventListener('click', function () {
            var id = this.getAttribute("myattr");
            //var id = delbtn.item(i);
            Ajax.request("/ajax/delcookie.ashx", {
                method: "POST",
                data: "id=" + id
            });



            this.parentNode.parentNode.parentNode.remove();
            total();

        });
    }
    //勾选香蕉积分
    var checkbox = document.getElementById("checkbox");
    checkbox.addEventListener("click", function () {
        if (checkbox.checked) {
            total();
            //checkbox.checked = false;
        } else {
            total();
        }
    });
    js_okBtn.addEventListener("click", function() {

        var nam = document.getElementById('nam');
        var tele = document.getElementById('tele');
        var province = document.getElementById('province');
        var city = document.getElementById('city');
        var district = document.getElementById('district');
        var detail_add = document.getElementById('detail_add');
        var address = detail_add.value;

        if (nam.value == '') {
            alert('请填写联系人');
            return false;
        }
        if(tele.value==''){
        alert('请填写您的手机');
        return false;
        }
        if (!checkMobile(tele.value)) {
            return false;
        }
        if(province.value==-1){
        alert('请选择您的所在省份');
        return false;
        }
        if(city.value==-1){
        alert('请选择您的所在城市');
        return false;
        }
        if (address == "" & address.length < 4) {

            alert('请填写您的具体地址,以便宝贝送达');
            return false;
        } else {
            if (address.search(/[0-9]/) != -1 & address.search(/[0-9]/) < 2) {
                alert('请填写您的具体地址,以便宝贝送达');
                return false;
            } else {

                if (!address.match(/^[\u4E00-\u9FA5\uF900-\uFA2D]/)) {
                    alert('请填写您的具体地址,以便宝贝送达');
                    return false;
                }
            }


            /*if(address.search(/[0-9]/)==-1) 
            {
            if(address.indexOf('路')<=1&address.indexOf('街')<=1&address.indexOf('村')<=1&address.indexOf('小区')<=1&address.indexOf('园')<=1&address.indexOf('组')<=1&address.indexOf('苑')<=1){
            alert('请填写更具体的地址,方便快递送达');
            return false;}
            }*/
        }

        //赋值，表示已经提交，防止重复提交
        $("#sppcity").attr("istj", "1");
        //添加订单 
        var pid = getQueryString("pro_id");
        var pindex = province.selectedIndex;
        var cindex = city.selectedIndex;
        var payMentType = getSelectedText("payment");
        //var ajaxdata = "count=" + item_num.value + "&pro_id=" + pid + "&name=" + nam.value + "&phone=" + tele.value + "&provice=" + province.options[pindex].text + "&city=" + city.options[cindex].text + "&address=" + detail_add.value;
        var ajaxdata = "data=" + getData() + "&bananaCount=" + document.getElementById("bananaCount").innerHTML + "&price=" + document.getElementById("total").innerHTML + "&name=" + nam.value + "&phone=" + tele.value + "&provice=" + province.options[pindex].text + "&payMentType=" + payMentType + "&city=" + city.options[cindex].text + "&address=" + detail_add.value + "&pno=<%=pno %>";
        Ajax.request("ajax/statistics.ashx", {
            method: "POST",
            data: "act=pro3&objId=" + pid

        });

        Ajax.request("ajax/OrderInfo.ashx", {
            data: ajaxdata,
            method: "POST",
            success: function(xhr) {
                //to do with xhr<a href="ajax/test.ashx">ajax/test.ashx</a>
                if (parseInt(xhr.responseText) > 0) {
                    if (getSelectedText("payment") == "货到付款") {
                        window.location.href = "ordercheck.aspx?orderid=" + xhr.responseText;
                    } else {

                        window.location.href = "topay.aspx?name=" + $("#uname").text() + "&orderid=" + xhr.responseText + "&backUrl=mine.aspx";
                    }
                    //window.location.href = "mine_order.aspx";
                } else if (xhr.responseText == "-2") {
                    alert("你有末完成订单，请完成后再下单！");
                    window.location.href = "mine_order.aspx";
                } else {
                    $("#sppcity").attr("istj", "0");
                    alert("数据异常");
                }

            },
            failure: function(xhr) {
                //to do with xhr
            }
        });
    });



    function checkMobile(str) {
        var re = /^1\d{10}$/
        if (re.test(str)) {
            return true;
        } else {
            alert("手机号码输入有误，请重新输入！");
            return false;
        }
    }



    function total() {
        var count = 0;
        var bananaCount = 0;
        var tt = 0;
        var t_pri = document.getElementsByClassName('t_pri');
        for (k = 0; k < t_pri.length; k++) {
            tt += parseInt(t_pri.item(k).innerHTML);
            count += parseInt(t_pri.item(k).innerHTML);
           
        }
        Ajax.request("ajax/OrderInfo.ashx", {
            method: "POST",
            data: "act=getBananaCount",
            success: function(xhr) {
                if (parseInt(xhr.responseText) > 0) {
                    //应该省去的
                    count = Math.ceil(count * 0.1);
                    //可以使用的香蕉
                    bananaCount = parseInt(xhr.responseText * 0.1);
                    if (bananaCount > count) {
                        document.getElementById("bananaCount").innerHTML = count;
                    } else {
                        document.getElementById("bananaCount").innerHTML = bananaCount;
                    }


                }
            }

        });

        if (getSelectedText("payment") == "货到付款") {
            if (tt < 199) {
                document.getElementById("peisong").innerHTML = "15";
                tt += 15;
            }
            else {
                document.getElementById("peisong").innerHTML = "0";
            }
        } else {
            if (tt < 69) {
                document.getElementById("peisong").innerHTML = "15";
                tt += 15;
            }
            else {
                document.getElementById("peisong").innerHTML = "0";
            }
        }
        if (document.getElementById("checkbox").checked) {
            tt -= parseInt(document.getElementById("bananaCount").innerHTML);
        }

        document.getElementById("total").innerHTML = tt;


    }
    $("input[type='radio']").click(function () {
        total();
    });

    function getData() {
        var param = "";
        var t_param = document.getElementsByName("item_num");
        for (d = 0; d < t_param.length; d++) {
            param += t_param.item(d).getAttribute("data") + "|" + t_param.item(d).value + ",";
        }
        return param;
    }
    function deleteit(obj) {
        obj.parentNode.parentNode.parentNode.remove();

    }

    var province = document.getElementById('province');
    var city = document.getElementById('city');

    for (var key in regions) {
        var provinceOption = document.createElement('option');
        provinceOption.text = regions[key]["des"];
        provinceOption.value = key;
        province.options.add(provinceOption);
    }

    province.addEventListener('change', function () {
        city.length = 1;
        var sheng = province.value;
        var cityArr = regions[sheng].son;
        for (var key in cityArr) {
            var provinceOption = document.createElement('option');
            provinceOption.text = cityArr[key]["des"];
            provinceOption.value = key;
            city.options.add(provinceOption);
        }
    });

</script>
<script type="text/javascript">

    $(function () {
        var pstr = $("#sppcity").attr("datap");
        var cstr = $("#sppcity").attr("datac");
        // alert(pstr + " " + cstr);
        if (pstr.length > 0 && cstr.length > 0) {


            var province = document.getElementById('province');
            var city = document.getElementById('city');
            var count = $("#province option").length;
            for (var i = 0; i < count; i++) {
                if ($("#province ").get(0).options[i].text == pstr) {
                    $("#province ").get(0).options[i].selected = true;



                    city.length = 1;
                    var sheng = province.value;
                    var cityArr = regions[sheng].son;
                    for (var key in cityArr) {
                        var provinceOption = document.createElement('option');
                        provinceOption.text = cityArr[key]["des"];
                        provinceOption.value = key;
                        city.options.add(provinceOption);
                    }
                    var countchild = $("#city option").length;
                    for (var i = 0; i < count; i++) {
                        if ($("#city ").get(0).options[i].text == cstr) {
                            $("#city ").get(0).options[i].selected = true;
                            break
                        }
                    }
                    break;
                }
            }
        }
    })
</script>
</html>
