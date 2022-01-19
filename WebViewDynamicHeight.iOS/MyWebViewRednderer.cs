using System;
using Foundation;
using UIKit;
using WebKit;
using WebViewDynamicHeight.iOS;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(MyWebViewRednderer))]
namespace WebViewDynamicHeight.iOS
{
    public class MyWebViewRednderer : WkWebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            NavigationDelegate = new CustomWebViewDelegate(this);

            if (NativeView != null)
            {
                var webView = (WKWebView)NativeView;
                webView.ScrollView.ScrollEnabled = false;
            }
        }
    }

    public class CustomWebViewDelegate : WKNavigationDelegate
    {
        MyWebViewRednderer webViewRenderer;

        public CustomWebViewDelegate(MyWebViewRednderer _myWebViewRednderer = null)
        {
            webViewRenderer = _myWebViewRednderer ?? new MyWebViewRednderer();
        }

        public override async void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            var wv = webViewRenderer.Element as WebView;

            int i = 10;
            while (webView.ScrollView.ContentSize.Height == 0 && i-- > 0)
            {
                await System.Threading.Tasks.Task.Delay(100);
            }
            if (wv != null)
            {
                var contentSize = webView.ScrollView.ContentSize;
                wv.HeightRequest = contentSize.Height;
            }
        }
    }
}
