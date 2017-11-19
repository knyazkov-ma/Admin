using Admin.DataService.Interface;
using Admin.Entity;
using System.Collections.Generic;
using System.IO;
using Admin.WebApp.App.Resources;

namespace Admin.WebApp.Helpers
{
    public class JsResourceHelper
    {
        
        public void Set()
        {
            
            JsResource t4JsResource = new JsResource();
            t4JsResource.Session = new Dictionary<string, object>();
           

            string pageContent = t4JsResource.TransformText();
            File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/App/Resources/jsResource.js"), pageContent);
        }
    }
}