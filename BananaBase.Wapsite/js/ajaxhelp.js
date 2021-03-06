﻿

/**
* JavaScript Ajax Library v0.1
* Copyright (c) 2010 snandy
* Blog: http://snandy.javaeye.com/
* QQ群: 34580561
* Date: 2010-07-20
* 
* 
* 执行基本ajax请求,返回XMLHttpRequest
* Ajax.request(url,{
* 		async 	是否异步 true(默认)
* 		method 	请求方式 POST or GET(默认)
* 		data 	请求参数 (键值对字符串)
* 		success 请求成功后响应函数，参数为xhr
* 		failure 请求失败后响应函数，参数为xhr
* });
*
*/
Ajax =
function () {
    function request(url, opt) {
        function fn() { }
        var async = opt.async !== false,
			method = opt.method || 'GET',
			data = opt.data || null,
			success = opt.success || fn,
			failure = opt.failure || fn;
        method = method.toUpperCase();
        if (method == 'GET' && data) {
            url += (url.indexOf('?') == -1 ? '?' : '&') + data;
            data = null;
        }
        var xhr = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
        xhr.onreadystatechange = function () {
            _onStateChange(xhr, success, failure);
        };
        xhr.open(method, url, async);
        if (method == 'POST') {
            xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded;');
        }
        xhr.send(data);
        return xhr;
    }
    function _onStateChange(xhr, success, failure) {
        if (xhr.readyState == 4) {
            var s = xhr.status;
            if (s >= 200 && s < 300) {
                success(xhr);
            } else {
                failure(xhr);
            }
        } else { }
    }
    return { request: request };
} ();
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}