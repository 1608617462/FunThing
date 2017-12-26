using Swashbuckle.Swagger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace FunThing.Class
{
    /* public class CachingSwaggerProvider:ISwaggerProvider
     {
         private static ConcurrentDictionary<string, SwaggerDocument> _cache = new ConcurrentDictionary<string, SwaggerDocument>();

         private readonly ISwaggerProvider _swaggerProvider;

         public CachingSwaggerProvider(ISwaggerProvider swaggerProvider)
         {
             _swaggerProvider = swaggerProvider;
         }

         public SwaggerDocument GetSwagger(string rootUrl,string apiVersion)
         {
             string cacheKey = rootUrl+apiVersion;
             SwaggerDocument srcDoc = null;
             //只读取一次
             if(!_cache.TryGetValue(cacheKey,out srcDoc))
             {
                 srcDoc = _swaggerProvider.GetSwagger(rootUrl,apiVersion);
                 srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc() }, { "", "" } };
                 _cache.TryAdd(cacheKey,srcDoc);
             }
             return srcDoc;
         }
         /// <summary>
         /// 从API文档中读取控制器注释
         /// </summary>
         /// <returns>所有控制器注释</returns>
         public static ConcurrentDictionary<string, string> GetControllerDesc()
         {
             string xmlPath = $"{AppDomain.CurrentDomain.BaseDirectory}/bin/FunThing.XML";
             ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
             if(File.Exists(xmlPath))
             {
                 XmlDocument xmlDoc = new XmlDocument();
                 xmlDoc.Load(xmlPath);
                 string type = string.Empty, path = string.Empty, controllerName = string.Empty;

                 string[] arrPath;
                 int length = -1, cCount = "Controller".Length;
                 XmlNode summaryNode = null;
                 foreach(XmlNode node in xmlDoc.SelectNodes("//member"))
                 {
                     type = node.Attributes["name"].Value;
                     if(type.StartsWith("T:"))
                     {
                         //控制器
                         arrPath = type.Split('.');
                         length = arrPath.Length;
                         controllerName = arrPath[length-1];
                         if(controllerName.EndsWith("Controller"))
                         {
                             //获取控制器注释
                             summaryNode = node.SelectSingleNode("summary");
                             string key = controllerName.Remove(controllerName.Length - cCount, cCount);
                             if(summaryNode!=null&&!string.IsNullOrEmpty(summaryNode.InnerText)&&!controllerDescDict.ContainsKey(key))
                             {
                                 controllerDescDict.TryAdd(key,summaryNode.InnerText.Trim());
                             }
                         }
                     }
                 }
             }
             return controllerDescDict;
         }
     }*/
}