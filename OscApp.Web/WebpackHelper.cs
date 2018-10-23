using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OscApp.Web
{


    public static class WebpackHelper
    {
        private static JObject AssetMap;
        private static long LastUpdate;

        private static JObject GetWebpackAssetsJson(string appPath)
        {
            var webpackAssetsFilePath = $"{appPath}/webpack.assets.json";
            var lastaccess = File.GetLastWriteTime(webpackAssetsFilePath).Ticks;

            if (AssetMap == null || lastaccess > LastUpdate)
            {
                using (StreamReader webpackAssetsFile = File.OpenText(webpackAssetsFilePath))
                {
                    using (JsonTextReader webpackAssetsReader = new JsonTextReader(webpackAssetsFile))
                    {
                        AssetMap = (JObject)JToken.ReadFrom(webpackAssetsReader);
                        LastUpdate = DateTime.Now.Ticks;
                    }
                }
            }

            return AssetMap;
        }

        private static string GetAsset(string appPath, string assetName, string fileType)
        {
            return GetWebpackAssetsJson(appPath).SelectToken(assetName).Value<string>(fileType);
        }

        public static HtmlString WebpackAsset(this IHtmlHelper helper, string assetName)
        {
            var appPath = helper.ViewContext.HttpContext.RequestServices.GetService<IHostingEnvironment>();
            return new HtmlString($"<script src='{GetAsset(appPath.ContentRootPath, assetName, "js")}'></script>");
        }


        public static HtmlString WebpackAssetAsync(this IHtmlHelper helper, string assetName)
        {
            var appPath = helper.ViewContext.HttpContext.RequestServices.GetService<IHostingEnvironment>();
            return new HtmlString($"<script async='async' src='{GetAsset(appPath.WebRootPath, assetName, "js")}'></script>");
        }

        public static HtmlString WebpackStyleAsset(this IHtmlHelper helper, string assetName)
        {
            var appPath = helper.ViewContext.HttpContext.RequestServices.GetService<IHostingEnvironment>();
            var result = new HtmlString($"<link rel='stylesheet' type='text/css' href='{GetAsset(appPath.WebRootPath, assetName, "css")}'/>");
            return result;
        }
    }
}