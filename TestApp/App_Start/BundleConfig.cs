using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace TestApp {

    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            var styleBundle = new StyleBundle("~/Content/bundle");
            var scriptBundleMap = new StyleBundle("~/Scripts/bundleMap");
            var styleBundleMap = new StyleBundle("~/Style/bundleMap");

            // jQuery
            scriptBundle
                .Include("~/Scripts/jquery-3.1.0.js");

            // Bootstrap
            scriptBundle
                .Include("~/Scripts/bootstrap.js");

            // Bootstrap
            styleBundle
                .Include("~/Content/bootstrap.css");

            // Custom site styles
            styleBundle
                .Include("~/Content/Site.css");

            // leaflet
            scriptBundleMap
                .Include("~/Scripts/leaflet/leaflet.js"
                , "~/Scripts/map.js");
            styleBundleMap
                .Include("~/Scripts/leaflet/leaflet.css");


            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
            bundles.Add(scriptBundleMap);
            bundles.Add(styleBundleMap);

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}