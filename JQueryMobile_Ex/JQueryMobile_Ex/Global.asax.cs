using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace JQueryMobile_Ex
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Ū���o�� �������w�] Display Mode �]�w
            //var displayModes = DisplayModeProvider.Instance.Modes;

            #region �[�J iPhone �M Android ���P�_
            //DisplayModeProvider.Instance.Modes.Insert(0,
            //    new DefaultDisplayMode("iPhone")
            //    {
            //        ContextCondition = (context =>
            //                            context.GetOverriddenUserAgent().IndexOf("iPhone", StringComparison.OrdinalIgnoreCase)
            //                            >= 0)
            //    }
            //);

            //DisplayModeProvider.Instance.Modes.Insert(1,
            //    new DefaultDisplayMode("Android")
            //    {
            //        ContextCondition = (context =>
            //                            context.GetOverriddenUserAgent().IndexOf("Android", StringComparison.OrdinalIgnoreCase)
            //                            >= 0)
            //    }
            //); 
            #endregion

            // �쥻�o�ӱM�״N�����J
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // �[�J�F jQuery.Mobile.MVC �M�󤧫�A�n�]�w���J
            BundleMobileConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
