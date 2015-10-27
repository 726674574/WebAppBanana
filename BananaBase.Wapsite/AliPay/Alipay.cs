using System.Collections.Generic;

namespace Banana.Wapsite.AliPay
{
    public class Alipay
    {
        public string GetBuildRequest(string subject, string body, decimal totalFee, string urlArg, string outTradeNo)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = ConfigHelper.GetConfigString("alipay_notify_url");
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = ConfigHelper.GetConfigString("alipay_return_url");
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //商户网站订单系统中唯一订单号，必填

            //必填

            //付款金额
            string total_fee = totalFee.ToString("#0.00");
            //必填
            //商品展示地址
            string show_url = "";
            //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数

            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1


            ////////////////////////////////////////////////////////////////////////////////////////////////
            Config.Key = ConfigHelper.GetConfigString("alipay_key");
            Config.Seller_email = "landinginfo@163.com";
            Config.Partner = ConfigHelper.GetConfigString("alipay_pid");
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("seller_id", Config.Partner);
            sParaTemp.Add("_input_charset", "utf-8");
            //标准双接口交易->service=trade_create_by_buyer
            //即时到帐交易->service=create_direct_pay_by_user
            //手机网页支付->alipay.wap.create.direct.pay.by.user
            sParaTemp.Add("service", "alipay.wap.create.direct.pay.by.user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", string.Format("{0}?{1}", return_url, urlArg));
            sParaTemp.Add("out_trade_no", outTradeNo);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            return Submit.BuildRequest(sParaTemp, "get", "确认");
        }
    }
}