using System.Web;
using System.Web.Optimization;

namespace Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
			bundles.Add(new ScriptBundle("~/vendor/Alljs.js").Include(
				"~/vendor/jquery/jquery-1.12.3.js",
				"~/vendor/tether/js/tether.js",
				"~/vendor/bootstrap/js/bootstrap.js",
				"~/vendor/detectmobilebrowser/detectmobilebrowser.js",
				"~/vendor/DataTables/js/jquery.dataTables.js",
				"~/vendor/DataTables/js/dataTables.bootstrap4.min.js",
				"~/vendor/DataTables/Responsive/js/dataTables.responsive.js",
				"~/vendor/DataTables/Responsive/js/responsive.bootstrap4.min.js",
				"~/vendor/DataTables/Buttons/js/dataTables.buttons.js",
				"~/vendor/DataTables/Buttons/js/buttons.bootstrap4.min.js",
				"~/vendor/DataTables/JSZip/jszip.js",
				"~/vendor/DataTables/pdfmake/build/pdfmake.js",
				 "~/vendor/DataTables/pdfmake/build/vfs_fonts.js",
				"~/vendor/DataTables/Buttons/js/buttons.html5.js",
				"~/vendor/DataTables/Buttons/js/buttons.print.js",
				"~/vendor/DataTables/Buttons/js/buttons.colVis.min.js",

				"~/Scripts/js/app.js",
				"~/Scripts/jquery.validate.js",
				"~/Scripts/jquery.validate.unobtrusive.js",
				"~/Scripts/js/tables-datatable.js",
				"~/vendor/sweetalert2/sweetalert2.min.js",
				//"~/Scripts/CommonMethod.js",
				"~/Angular/Method.js",
				"~/Scripts/angular.js",
				"~/Angular/Module.js"
				));


			bundles.Add(new StyleBundle("~/Content/css/Allcss.css").Include(
				"~/vendor/bootstrap/css/bootstrap.css",
				//"~/vendor/themify-icons/themify-icons.css",
				//"~/vendor/font-awesome/css/font-awesome.css",
				//"~/Content/css/core.css",
				//"~/Content/css/override.css",
				"~/vendor/animate.css/animate.css",
				//"~/vendor/jscrollpane/jquery.jscrollpane.css",
				"~/vendor/sweetalert2/sweetalert2.css",
				"~/vendor/DataTables/css/dataTables.bootstrap4.min.css",
				"~/vendor/DataTables/Responsive/css/responsive.bootstrap4.min.css",
				"~/vendor/DataTables/Buttons/css/buttons.dataTables.css",
				"~/vendor/DataTables/Buttons/css/buttons.bootstrap4.min.css"));

			//bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
			//            "~/Scripts/jquery-{version}.js"));

			//bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
			//            "~/Scripts/jquery.validate*"));

			//// Use the development version of Modernizr to develop with and learn from. Then, when you're
			//// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			//bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
			//            "~/Scripts/modernizr-*"));

			//bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
			//          "~/Scripts/bootstrap.js"));

			//bundles.Add(new StyleBundle("~/Content/css").Include(
			//          "~/Content/bootstrap.css",
			//          "~/Content/site.css"));
		}
    }
}
