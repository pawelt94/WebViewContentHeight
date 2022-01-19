using System;
using Android.Content;
using WebViewDynamicHeight.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Android.Webkit.WebView;

[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(MyWebViewRednderer))]
namespace WebViewDynamicHeight.Droid
{
    public class MyWebViewRednderer : WebViewRenderer
    {
        public MyWebViewRednderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement is Xamarin.Forms.WebView webViewControl)
            {
                if (e.OldElement == null)
                {
                    Control.SetWebViewClient(new ExtendedWebViewClient(webViewControl));
                }
            }
        }

        class ExtendedWebViewClient : Android.Webkit.WebViewClient
        {
            private Xamarin.Forms.WebView _control;

            public ExtendedWebViewClient(Xamarin.Forms.WebView control)
            {
                _control = control;
            }

            public override async void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);

                if (_control != null)
                {
                    int i = 100;
                    while (view.ContentHeight <= 8 && i-- > 0)
                    {
                        await System.Threading.Tasks.Task.Delay(100);
                    }
                    _control.HeightRequest = view.ContentHeight;
                }
            }
        }
    }
}
