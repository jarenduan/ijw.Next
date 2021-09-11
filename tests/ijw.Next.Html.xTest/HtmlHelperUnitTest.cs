using System;
using Xunit;
using ijw.Next.Net;
namespace ijw.Next.Html.xTest {
    public class HtmlHelperUnitTest {
        [Fact]
        public void SelectTextsByXPathTest() {
            var html = @"
<!DOCTYPE html>
<html lang=""zh-CN"">
<head>
    <meta charset=""UTF-8"">
    <meta content=""width=1100, initial-scale=1.0, user-scalable=yes, target-densitydpi=device-dpi;"" name=""viewport"" />
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>IP查询_专业的 IP 地址库_IPIP.NET</title>
<link href=""https://cdn.ipip.net"" rel=""dns-prefetch"" />
<link href=""https://cdn.ipip.net/loveapp/ipip/www_v2/theme/css/bootstrap.min.css,/loveapp/ipip/www_v2/theme/css/style.css?400829.css"" rel=""stylesheet"" type=""text/css"" />

    <script src=""https://cdn.ipip.net/loveapp/ipip/www_v2/theme/js/jquery.min.js"" charset=""UTF-8""></script>
    <script src=""https://cdn.ipip.net/loveapp/ipip/www_v2/theme/js/bootstrap.min.js"" charset=""UTF-8""></script>
<script>(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)})(window,document,'script','//www.google-analytics.com/analytics.js','ga');ga('create','UA-28114143-1','auto');ga('send','pageview');</script></head>

<body class=""aboutBody"">
<div class=""outer topNav"" >
    <div class=""inner"">
        <div class=""fl"">
            <a class=""logo"" href=""https://www.ipip.net/"" title=""IPIP.NET""><img src=""https://cdn.ipip.net/loveapp/ipip/www_v2/theme/css/img/Logo_IPIP.png"" alt=""全球领先的 IP 地址库_IPIP.NET""></a>
            <a href=""https://www.ipip.net"" >首页</a>
            <a href=""https://www.ipip.net/product/ip.html"" class=""popProduct "">产品价格</a>
            <a href=""https://www.ipip.net/ip.html"" class=""act"">IP查询</a>
            <a href=""javascript:;"" class=""popSupport "">技术支持</a>
            <a href=""https://www.ipip.net/customer.html"" >客户</a>
            <a href=""https://www.ipip.net/about.html"" >关于</a>
        </div>
        <div class=""fr"">
                            <a href=""javascript:;"" class=""popTools"">工具</a>|<a href=""https://user.ipip.net/login.html"">登录</a>/<a href=""https://user.ipip.net/register.html"">注册</a>|<a href=""https://en.ipip.net/?origin=CN"" class=""popLang"">English</a>
                    </div>
<!--        <ul class=""topPop popProductItems"">
            <li><a href=""/product/ip.html"">IP数据定位</a></li>
            <li><a href=""/product/location.html"">综合定位</a></li>
            <li><a href=""/product/security.html"">网络安全数据</a></li>
            <li><a href=""/product/client.html"">免费 & 客户端</a></li>
            <li><a href=""/product/other.html"">其他产品</a></li>
        </ul>-->
        <ul class=""topPop popSupportItems"">
            <li><a href=""https://www.ipip.net/support/faq.html"">常见问题</a></li>
            <li><a href=""https://www.ipip.net/support/api.html"">接口文档</a></li>
            <li><a href=""https://www.ipip.net/support/code.html"">解析代码</a></li>
            <li><a href=""https://www.ipip.net/support/data.html"">相关数据</a></li>
            <li><a href=""https://www.ipip.net/support/db_update.html"">下载示例</a></li>
        </ul>
        <ul class=""topPop popToolsItems toolPosition"">
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','trace'])"" href=""https://tools.ipip.net/traceroute.php"">TraceRoute</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','ping'])"" href=""https://tools.ipip.net/ping.php"">Ping</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','web-ping'])"" href=""https://tools.ipip.net/webping.php"">WebPing</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','asn'])"" href=""https://tools.ipip.net/as.php"">ASN</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','cdn'])"" href=""https://tools.ipip.net/cdn.php"">CDN</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','head'])"" href=""https://tools.ipip.net/httphead.php"">HttpHead</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','dns'])"" href=""https://tools.ipip.net/dns.php"">DNS</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','domain'])"" href=""https://tools.ipip.net/domain.php"">域名查询</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','ipdomain'])"" href=""https://tools.ipip.net/ipdomain.php"">IP反查域名</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','icp'])"" href=""https://tools.ipip.net/icp.php"">ICP查询</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','ip-la'])"" href=""https://www.ip.la"" target=""_blank"">IP.LA</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','ipv6test'])"" href=""https://v6t.ipip.net/"" target=""_blank"">IPv6Test</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','site'])"" href=""https://v6sc.ipip.net/"" target=""_blank"">IPv6网站测试</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','bt'])"" href=""https://labs.ipip.net/bt.html"" target=""_blank"">BT 网络监控</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','zuobiao'])"" href=""https://tools.ipip.net/map.php"">坐标拾取</a></li>
            <li><a onclick=""_hmt.push(['_trackEvent','tool','click','convert'])"" href=""https://tools.ipip.net/convert.php"">转换工具</a></li>
        </ul>
<style>
    .allpros {
        display: none;
    }
    .pro0 {
        background: #191919;
        width: 666px;
        position: absolute;
        left: 160px;
        top: 60px;
        border: 1px solid #5f5656;
        height: 300px;
        overflow: hidden;
        clear: both;
        padding: 10px;
    }
    .pro0 dl {
        float: left;
        width: 160px;
        height: 336px;
    }
    .pro0 dl dt {
        font-weight: 700;
        text-align: center;
        border-bottom: 2px solid #c5c3aa;
        height: 40px;
        line-height: 35px;
        color: #eaeaea;
        font-size:16px;
    }
    .pro0 dl dt a {
        line-height: inherit;
    }
    .pro0 dl dd:first-child{
        margin-top: 10px;
    }
    .pro0 dl dd {
        height: 25px;
        text-align: center;
    }
    .pro0 dl dd a {
        color: #c7d2e0;
        line-height: 25px;
        font-size: 14px;
    }
</style>
        <div class=""allpros pro0"">
            <dl class=""pro1"">
                <dt><a onclick=""_hmt.push(['_trackEvent','product','click','ip'])"" href=""https://www.ipip.net/product/ip.html"">IP 数据产品</a> </dt>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','ipv4-city'])"" href=""https://www.ipip.net/product/ip.html#ipv4city"">IPv4 地级市精度库</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','district'])"" href=""https://www.ipip.net/product/ip.html#district"">IPv4 国内区县库</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','ipv4-geo'])"" href=""https://www.ipip.net/product/ip.html#ipgeo"">IPv4 国内高精度库</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','ipv6-city'])"" href=""https://www.ipip.net/product/ip.html#ipv6city"">IPv6 地址库</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','ip-api'])"" href=""https://www.ipip.net/product/ip.html#api_city"">IP 查询接口服务</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','idc'])"" href=""https://www.ipip.net/product/ip.html#idc"">IDC IPv4 库</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','cell'])"" href=""https://www.ipip.net/product/ip.html#base_station"">全球基站 IPv4 库</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','bgp'])"" href=""https://www.ipip.net/product/ip.html#bgp"">BGP/ASN IP 库</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','free'])"" href=""https://www.ipip.net/product/client.html"">IPv4 地址库免费版</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','dns'])"" href=""https://www.ipip.net/product/ip.html#dns"">CDN/DNS 专版地址库</a></dd>
            </dl>
            <dl class=""pro2"">
                <dt><a onclick=""_hmt.push(['_trackEvent','product','click','geo'])"" href=""https://www.ipip.net/product/location.html"">综合定位服务</a> </dt>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','station'])"" href=""https://www.ipip.net/product/location.html#cell"">移动网络基站定位</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','wifi'])"" href=""https://www.ipip.net/product/location.html#wifi"">Wi-Fi 定位</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','ip-geo'])"" href=""https://www.ipip.net/product/location.html#ipgeo"">IPv4 高精度定位</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','gps'])"" href=""https://www.ipip.net/product/location.html#gps"">GPS 经纬度定位</a></dd>
                <dd>
                    <dl>
                        <dt><a onclick=""_hmt.push(['_trackEvent','product','click','other'])"" href=""https://www.ipip.net/product/other.html"">其他产品</a></dt>
                        <dd><a onclick=""_hmt.push(['_trackEvent','product','click','mobile-phone'])"" href=""https://www.ipip.net/product/other.html#phone"">手机号码归属地数据库</a></dd>
                        <dd><a onclick=""_hmt.push(['_trackEvent','product','click','china_code'])"" href=""https://www.ipip.net/product/other.html#china"">中国行政区划代码区县版</a></dd>
                    </dl>
                </dd>
            </dl>
            <dl class=""pro3"">
                <dt><a onclick=""_hmt.push(['_trackEvent','besttrace','click','client'])"" href=""https://www.ipip.net/product/client.html"">客户端BestTrace</a> </dt>
                <dd><a onclick=""_hmt.push(['_trackEvent','besttrace','click','windows'])"" href=""https://www.ipip.net/product/client.html#besttrace"">Windows 版本</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','besttrace','click','android'])"" href=""https://www.ipip.net/product/client.html#besttrace"">Android 版本</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','besttrace','click','ios'])"" href=""https://www.ipip.net/product/client.html#besttrace"">iOS 版本</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','besttrace','click','mac'])"" href=""https://www.ipip.net/product/client.html#besttrace"">Mac OSX 版本</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','besttrace','click','linux'])"" href=""https://www.ipip.net/product/client.html#besttrace"">Linux 版本</a></dd>
            </dl>
            <dl class=""pro4"">
                <dt><a onclick=""_hmt.push(['_trackEvent','product','click','security'])"" href=""https://www.ipip.net/product/security.html"">网络安全基础数据</a></dt>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','bgp'])"" href=""https://www.ipip.net/product/security.html#bgp"">ASN & BGP IP 库</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','whois'])"" href=""https://www.ipip.net/product/security.html#whois"">IP Whois 数据</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','rdns'])"" href=""https://www.ipip.net/product/security.html#rdns"">IPv4 rDNS 数据</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','idc'])"" href=""https://www.ipip.net/product/security.html#IDC"">IDC IPv4 数据列表</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','cell'])"" href=""https://www.ipip.net/product/security.html#base_station"">全球基站 IPv4 数据</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','ip-port'])"" href=""https://www.ipip.net/product/security.html#port"">IPv4 端口协议识别</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','ip-proxy'])"" href=""https://www.ipip.net/product/security.html#proxy"">IP 代理识别</a></dd>
                <dd><a onclick=""_hmt.push(['_trackEvent','product','click','reputation'])"" href=""https://www.ipip.net/product/security.html#behavior2"">业务源风险 IPv4 专业版</a></dd>
            </dl>
        </div>
    </div>
</div>
<script>
    function getByClass(c) {
        var e = [];
        a = document.body;
        for (var d = a.getElementsByTagName(""*""), h = new RegExp(""\\b"" + c + ""\\b"", ""i""), b = 0, f = d.length; b < f; b++) h.test(d[b].className) && e.push(d[b]);
        return e
    }
    function tab(hoverObj, popObj) {
        var timer = null;
        var timerIn = null;
        var h = 0;
        hoverObj.onmouseover = popObj.onmouseover = function () {
            clearInterval(timer);
            clearInterval(timerIn);
            popObj.style.display = 'block';
            co = popObj.getElementsByTagName('li');
            if (!co || co.length == 0) {
                popObj.style.height = '300px'
            } else {
                popObj.style.height = popObj.getElementsByTagName('li')[0].offsetHeight * popObj.getElementsByTagName('li').length + 'px'
            }
        }
        hoverObj.onmouseout = popObj.onmouseout = function () {
            clearInterval(timer);
            clearInterval(timerIn);
            timer = setTimeout(function () {
                popObj.style.height = '0';
                timerIn = setTimeout(function() {
                    popObj.style.display = 'none';
                }, 300);
            }, 300)
        }
    }

    // tab(getByClass('popProduct')[0], getByClass('popProductItems')[0]);
    tab(getByClass('popProduct')[0], getByClass('allpros')[0]);
    tab(getByClass('popSupport')[0], getByClass('popSupportItems')[0]);
    tab(getByClass('popTools')[0], getByClass('popToolsItems')[0]);
//    tab(getByClass('popLang')[0], getByClass('popLangItems')[0]);
</script>

<script>
    var _hmt = _hmt || [];
    (function() {
        var hm = document.createElement(""script"");
        hm.src = ""https://hm.baidu.com/hm.js?123ba42b8d6d2f680c91cb43c1e2be64"";
        var s = document.getElementsByTagName(""script"")[0];
        s.parentNode.insertBefore(hm, s);
    })();
</script>

<div class=""outer ipSearchBanner"">
    <img src=""https://cdn.ipip.net/loveapp/ipip/www_en/theme/css/img/icon_Location_Search.png"" alt="""">
    <form method=""post"" action="""">
        <input type=""text"" name=""ip"" value=""221.217.55.190"" placeholder=""请输入IP地址，如：8.8.8.8"" autocomplete=""off"">
        <button type=""submit"">查询</button>
    </form>
</div>
<style>
    .tableNormal th:first-child{
        text-align: center;
    }
    .tableNormal tr td:first-child{
        width: 165px;
        text-align: center;
    }
    .tjun{
        display:none;
    }
    .tic{
        display:none;
    }
</style>
<div class=""outer tableNormal ipSearch""><div class=""inner"">
    <h2 class=""titH2"">IP 相关数据信息</h2>
    <table>
        <tr>
            <th style=""width: 165px;"">数据</th>
            <th style=""width: 915px;clear: both""><span style=""display: inline-block;width: 720px;float:left;"">城市级信息(数据来源于旗舰版)</span></th>
        </tr>
        <tr>
            <td>当前IP</td>
            <td style=""clear: both"">
                <span style=""display: inline-block;text-align: center;width: 720px;float: left;"">
                    <a style=""background: none;color: #0A246A;width: auto;"" href=""/ip/221.217.55.190.html"">221.217.55.190</a>                </span>
                <span style=""float: right;"">
                    <a style=""background: none;color: #888585;width: auto;"" href=""https://tools.ipip.net/ping.php?view=221.217.55.190"">Ping</a>
                    <a style=""background: none;color: #888585;width: auto;"" href=""https://tools.ipip.net/traceroute.php?ip=221.217.55.190"">Trace</a>
                    <a style=""background: none;color: #888585;width: auto;"" href=""https://tools.ipip.net/ipdomain.php?ip=221.217.55.190"">域名</a>
                </span>
            </td>
        </tr><!-- Reverse DNS  -->
                        <tr>
            <td>地理位置</td>
            <td style=""clear: both"">
                <span style=""display: inline-block;text-align: center;width: 720px;float: left;line-height: 46px;height: 46px;"">中国北京</span>
                <span style=""float: right"">
                    <a href=""/product/ip.html"" style=""background: none;color: #888585;width: auto;"">产品详情</a>
                </span>
            </td>
        </tr>
                        <tr>
                            <td>运营商</td>
                        <td style=""clear: both"">
                                    <span style=""display: inline-block;text-align: center;width: 720px;float: left;line-height: 46px;"">联通</span>
                            </td>
        </tr>
                        <tr>
            <td>时区</td>
            <td style=""clear: both""><span style=""display: inline-block;text-align: center;width: 720px;float: left;line-height: 46px;"">Asia/Shanghai UTC+8 </span></td>
        </tr>
        <tr>
            <td>地区中心经纬度</td>
            <td style=""clear: both""><span style=""display: inline-block;text-align: center;width: 720px;float: left;line-height: 46px;"">39.938884, 116.397459</span></td>
        </tr>
                                    </table>

                    <table>
                <tr>
                    <th style=""width: 165px"">数据</th>
                    <th style=""width: 915px;clear:both""><span style=""float:left;width: 720px;display: inline-block;line-height: 40px;"">国内区县级</span>
                        <span style=""float:right;margin-right: 10px; font-size:14px;line-height: 40px;"">Powered by <a style=""color: #fff;"" href=""http://www.rtbasia.com/"" target=""_blank"">RTBAsia.com</a></span>
                    </th>
                </tr>
                <tr>
                    <td>地理位置</td>
                    <td style=""clear: both"">
                        <span style=""float: left; line-height: 40px; display: inline-block; width: 720px;"">中国 北京 北京 昌平区</span>
                        <span style=""float: right;""><a style=""background: none;color: #888585;width: auto;"" href=""/product/ip.html#district"">产品详情</a> </span>
                    </td>
                </tr>
            </table>
                            <table>
                <tr>
                    <th style=""width: 165px;"">数据</th>
                    <th style=""width: 915px;clear: both;""><span style=""float: left;width: 720px;display: inline-block;line-height: 40px;"">国内高精度</span></th>
                </tr>
                <tr>
                    <td>地理位置</td>
                    <td style=""clear: both;""><span style=""display: inline-block;text-align: center;width: 720px;float: left;line-height: 46px;"">中国北京北京昌平区北七家(商圈)<span style=""color: #736e6e;font-size: 14px;"">(可信度：99)</span> <a style=""display: inline;"" target=""_blank"" href=""//labs.ipip.net/location/ip?ip=221.217.55.190"">查看地图</a></span></td>
                </tr>
            </table>
        
        <table>
            <tr>
                <th style=""width:165px"">数据</th>
                <th style=""width:915px;clear: both"">
                    <span style=""float: left; display: inline-block;width: 720px;"">网络安全风控基础数据</span>
                </th>
            </tr>
                            <tr>
                    <td>端口协议</td>
                                            <td style=""clear: both"">
                            <span style=""display: inline-block;text-align: center;width: 720px;float: left;line-height: 46px;"">该IP开放了3个端口，识别出1种协议。（更多数据请查看<a style=""display: inline"" href=""/product/security.html#port_protocol"">全网 IPv4 端口数据</a>）</span>
                        </td>
                                    </tr>
            
                        <tr>
                <td>代理识别</td>
                <td style=""clear: both"">
                    <span style=""display: inline-block;text-align: center;width: 720px;float: left;line-height: 46px;"">代理</span>
                    <span style=""float: right;"">
                            <a style=""background: none;color: #888585;width: auto;"" href=""/product/security.html"">产品详情</a>
                        </span>
                </td>
            </tr>
            
            
            <!--
                        <tr>
                <td>应用场景</td>
                <td></td>
            </tr>
                        -->
        </table>

        
        <style type=""text/css"">
            .asn tr a{
                background: none;
                width: auto;
                height: auto;
                color: #24414d;
            }
        </style>
        <table class=""asn"">
            <tr>
                <th style=""width: 165px;"">ASN数据</th>
                <th style=""width: 250px;"">CIDR</th>
                <th style=""width: 680px;"">信息</th>
            </tr>
                            <tr>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808"">AS4808</a> </td>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808/221.216.0.0/13"">221.216.0.0/13</a> </td>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808"">CHINA169-BJ China Unicom Beijing Province Network, CN</a> </td>
                </tr>
                            <tr>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808"">AS4808</a> </td>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808/221.217.0.0/18"">221.217.0.0/18</a> </td>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808"">CHINA169-BJ China Unicom Beijing Province Network, CN</a> </td>
                </tr>
                            <tr>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808"">AS4808</a> </td>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808/221.217.0.0/16"">221.217.0.0/16</a> </td>
                    <td><a onclick=""_hmt.push(['_trackEvent','site','click','whois'])"" target=""_blank"" href=""https://whois.ipip.net/AS4808"">CHINA169-BJ China Unicom Beijing Province Network, CN</a> </td>
                </tr>
                    </table>

<!--    <h2 class=""titH2"">地图定位</h2>-->
<!--    <div class=""map"">地图</div>-->

<!--    <h2 class=""titH2"">ASN数据</h2>-->

        <h2 class=""titH2 tjun"">威胁情报事件</h2>
        <table class=""tjun"">
            <tr>
                <th style=""width: 165px"">厂商</th>
                <th style=""width: 915px""><span style=""display: inline-block;width: 730px;float:left;"">详情</span></th>
            </tr>
            <tbody id=""tjun"">
            </tbody>
        </table>
        <script type=""text/javascript"">
            $.get(""/tjun/reports?ip=221.217.55.190"",function(dd){
                var dom = ""<tr><td>天际友盟</td><td style='text-align: center'>"";
                dd.forEach(function(d){
                    $("".tjun"").show();
                    var id = d.report_id.replace(/report-/, """");
                    dom += ""<a style='background:rgba(0,0,0,0);color: #000;float: left;width: 730px;' href='https://redqueen.tj-un.com/IntelDetails.html?id=""+id+""' target='_blank'>""+d.title+""</a><br>"";
                });
                dom += ""</td></tr>"";
                $(""#tjun"").prepend(dom);
            },""json"");
        </script>
        <h2 class=""titH2 tic"">历史域名</h2>
        <table class=""tic"">
            <tr>
                <th style=""width:23%"">厂商</th>
                <th>域名</th>
                <th>类型</th>
                <th>时间</th>
            </tr>
            <tbody id=""tic"">
            </tbody>
        </table>
        <script>
            $.get(""/tic/reports?ip=221.217.55.190"",function(dd){
                var length = dd.length;
                if(length<1){
                    return false;
                }
                $("".tic"").show();
                var i = 1;
                dd.forEach(function(d){
                    if(i == 1)
                    {
                        var source = ""<td rowspan=""+length+"">数字观星</td>"";
                        i = 0;
                    }else{
                        var source = """";
                    }
                    var dom = ""<tr>""+source+""<td style='padding-left: 15px;width: 165px;color: #000;text-align: center;'>""+d.domain+""</td><td>""+d.bwclass+""</td><td>""+d.time+""</td></tr>"";
                    $(""#tic"").append(dom);
                });
            },""json"");
        </script>
    <h2 class=""titH2"">其他数据<span style=""display: block; font-size: 16px;line-height: 30px;"">(以下数据仅作为对照参考列出，如有疑问请联系数据发行方)</span></h2>
    <table class=""fixWidth"">
        <tr>
            <th style=""width: 33.33%"">IP2Location</th>
            <th style=""width: 33.33%"">纯真IP库数据</th>
            <th style=""width: 33.33%"">MaxMind GEOLite2</th>
        </tr>
        <tr>
            <td>China Beijing Beijing<br/>221.216.0.0 - 221.223.255.255</td>
            <td>北京市昌平区 联通</td>
            <td>中国 北京市 北京<br/>221.217.32.0 - 221.217.63.255</td>
        </tr>
            </table>
        <table class=""fixWidth"">
            <tbody><tr>
                <th>
                    <a href=""http://ip.rtbasia.com/"" target=""_blank"" style=""font-size: 14px;color: #fcfcfc;margin-left: 160px;"">RTBAsia非人类访问量甄别服务</a>
                    <span style=""float: right;margin-right: 10px;"">
                        <a href=""https://wx.jdcloud.com/market/api/10363"" style=""font-size: 12px;color: #fcfcfc;"" target=""_blank"">真人度识别，199元查30万次</a>
                    </span>
                </th>
            </tr>
            <tr>
                <td>
                    <iframe frameborder=""0"" border=""0"" scrolling=""no"" marginwidth=""0"" marginheight=""1"" width=""360"" height=""20"" src=""https://inside.rtbasia.com/webservice/ipip?ipstr=221.217.55.190""></iframe>
                </td>
            </tr>
            </tbody></table>
        </div></div><div class=""outer ftMain"">
    <div class=""inner"">
        <dl>
            <dt>行业报告</dt>
            <dd><a target=""_blank"" href=""https://cdn.ipip.net/download/2017_1H_REPORT.jpg"">云计算服务商2017-1H</a> </dd>
            <dd><a target=""_blank"" href=""https://cdn.ipip.net/download/2017_2H_REPORT.jpg"">云计算服务商2017-2H</a> </dd>
            <dd><a target=""_blank"" href=""https://cdn.ipip.net/download/1H_2018_CSP_REPORT_CN.jpg"">云计算服务商2018-1H</a> </dd>
            <dd><a target=""_blank"" href=""https://cdn.ipip.net/download/2H_2018_CSP_REPORT_CN.jpg"">云计算服务商2018-2H</a> </dd>
            <dd><a target=""_blank"" href=""https://cdn.ipip.net/download/1H_2019_CSP_Report_CN.pdf"">云计算服务商2019-1H</a> </dd>
        </dl>
        <dl>
            <dt>产品服务</dt>
            <dd><a href=""https://www.ipip.net/product/ip.html"">IP 数据定位</a> </dd>
            <dd><a href=""https://www.ipip.net/product/location.html"">IP 高精度定位</a> </dd>
            <dd><a href=""https://www.ipip.net/product/location.html"">综合定位服务</a> </dd>
            <dd><a href=""https://www.ipip.net/product/security.html"">网络安全基础数据</a> </dd>
            <dd><a href=""https://www.ipip.net/product/other.html"">其他服务</a> </dd>
        </dl>
        <dl>
            <dt>BestTrace</dt>
            <dd><a href=""https://cdn.ipip.net/17mon/besttrace.exe"">Windows</a> </dd>
            <dd><a href=""https://itunes.apple.com/us/app/best-trace/id1037779758?l=zh&ls=1&mt=12"" target=""_blank"">Mac OS</a></dd>
            <dd><a href=""https://itunes.apple.com/cn/app/best-trace/id1026747589?ls=1&mt=8"" target=""_blank"">iOS</a> </dd>
            <dd><a href=""https://cdn.ipip.net/17mon/besttrace.apk"">Android</a> </dd>
            <dd><a href=""https://cdn.ipip.net/17mon/besttrace4linux.zip"">Linux</a> </dd>
        </dl>
        <dl>
            <dt>网络工具</dt>
            <dd><a href=""https://whois.ipip.net"">Whois 查询</a></dd>
            <dd><a href=""https://whois.ipip.net/countries"">Country ASNs</a></dd>
            <dd><a href=""https://whois.ipip.net/ix/"">Internet eXchange</a></dd>
            <dd><a href=""https://itunes.apple.com/cn/app/best-nettools/id1370798520"" target=""_blank"">Best NetTools</a> </dd>
        </dl>
        <dl>
            <dt>关于IPIP</dt>
            <dd><a href=""https://www.ipip.net/about.html"">公司简介</a></dd>
            <dd><a href=""https://www.ipip.net/job.html"">工作机会</a> </dd>
            <dd><a href=""https://www.ipip.net/contact.html"">联系我们</a> </dd>
            <dd><a href=""https://www.ipip.net/privacy.html"">隐私声明</a> </dd>
            <dd><a href=""https://labs.ipip.net/"">实验室</a></dd>
        </dl>
        <div class=""wx"">
            <img src=""https://cdn.ipip.net/loveapp/ipip/www_v2/theme/img/ipip_wx.jpg"" alt=""微信公共号"">
        </div>
    </div>
    <div class=""copy"">
        <p style=""color: #f0f0f0;line-height: 20px;padding-top: 10px;"">© 2013 - 2019 北京天特信科技有限公司 所有权利保留</p>
        <p style=""margin: 0px;line-height: 20px;"">
            <a style=""font-size: 12px;color: #c8c8c8;"" href=""http://www.miitbeian.gov.cn/"" target=""_blank"">京ICP备13047193号-1</a>&nbsp;
            <a style=""font-size: 12px;color: #c8c8c8;padding-left: 20px;background: url('https://cdn.ipip.net/loveapp/ipip/www/theme/images/gongan.png') no-repeat;"" href=""http://www.beian.gov.cn/portal/registerSystemInfo?recordcode=11010802020247"" target=""_blank"">京公网安备 11010802020247号</a>
        </p>
        <p style=""color: #c8c8c8;line-height: 20px;padding-bottom: 5px;"">
            本网站安全防护服务由<a href=""https://www.yunaq.com/?from=ipip.net"" style=""font-size: 14px;color: #c8c8c8;"" target=""_blank"">知道创宇云安全</a>提供<br>
            <a href=""https://www.trustasia.com/"" style=""font-size: 12px;color: #c8c8c8;"" target=""_blank"">本网站 SSL 证书由 TRUSTASIA 提供</a><br>
            <a href=""http://www.x-cti.org/"" style=""font-size: 12px;color: #c8c8c8;"" target=""_blank"">本公司为 烽火台安全威胁情报联盟 成员</a>
        </p>
    </div>
</div>

<script src=""https://cdn.ipip.net/loveapp/ipip/www/theme/js/fingerprint2.js"" charset=""UTF-8""></script>
<script>
    new Fingerprint2().get(function(result, components){
        $.post('/fingerprint.php', {hash:result, components:components}, function(){})
    });

    function randomString(len) {
        len = len || 32;
        var $chars = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';
        var maxPos = $chars.length;
        var pwd = '';
        for (i = 0; i < len; i++) {
            pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
        }
        return pwd;
    }

    var ds = [];
    var i;
    for (i = 0; i < 10; i++) {
        var k = randomString(16);

        var d = ""https://"" + k + "".skt.ipip.net/"";

        $('<img src=""'+d+'"" width=""0"" height=""0""/>').appendTo(document.body)

        ds.push(k);
    }
    $.get('/skt.php?q=' + ds.join("",""), function(){})
</script>
<script>

    $(function(){
        $(window).on('resize', function (event) {
            if ($(document.body).width() < 1200) {
                $('.customerBanner').css('background-size', 'cover');
                $('.aboutBanner').css('background-size', 'cover');
                $('.jobBanner').css('background-size', 'cover');
                $('.contactBanner').css('background-size', 'cover');
                //aboutBanner
            } else {
                $('.customerBanner').css('background-size', 'cover');
                $('.aboutBanner').css('background-size', 'cover');
                $('.jobBanner').css('background-size', 'cover');
                $('.contactBanner').css('background-size', 'cover');
            }
        });
    })
</script>
</body>
</html>";
            var v = html.SelectTextsByXPath("/html/body[@class='aboutBody']/div[@class='outer ipSearchBanner']/form/input/@value")[0];
            Assert.True(v.IsIPv4Address());
        }
    }
}
