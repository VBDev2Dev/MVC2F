﻿Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"))

        'jqueryUI
        bundles.Add(New ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui-{version}.js",
                    "~/Scripts/jquery-ui.unobtrusive-{version}.js",
                    "~/Scripts/jquery.unobtrusive-ajax.js"))

        bundles.Add(New StyleBundle("~/Content/themes/base/css").Include(
                    "~/Content/themes/base/all.css"))

        bundles.Add(New StyleBundle("~/Content/themes/darkhive/css").Include(
                    "~/Content/themes/dark-hive/jquery-ui.dark-hive.css"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                   "~/Scripts/jquery.validate*"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.js",
                  "~/Scripts/respond.js"))

        bundles.Add(New ScriptBundle("~/bundles/SiteScripts").Include(
                  "~/Scripts/SiteScripts/*.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/Site.css"'                  "~/Content/css/*.css"
                  ))
    End Sub
End Module
