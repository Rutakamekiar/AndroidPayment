using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Wallet;
using Android.Locations;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace LastyTestProject.Droid
{
    [Activity(Label = "LastyTestProject", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            IPaymentService paymentService = new PaymentService();

            MessagingCenter.Subscribe<string>(this,
                                              "Pay",
                                              value =>
                                              {
                                                  paymentService.AuthorizePayment(1);
                                              });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 999)
            {
                switch (resultCode)
                {
                    case Result.Canceled:
                        break;
                    case Result.FirstUser:
                        var statusFromIntent = AutoResolveHelper.GetStatusFromIntent(data);
                        var x = statusFromIntent.StatusCode;
                        break;
                    case Result.Ok:
                        var paymentData = PaymentData.GetFromIntent(data);
                        string paymentInfo = paymentData.ToJson();

                        if (paymentInfo == null)
                        {
                            return;
                        }

                        var paymentMethodData = (JObject)JsonConvert.DeserializeObject(paymentInfo);
                        string tokenData = paymentMethodData.SelectToken("paymentMethodData.tokenizationData.token").ToString();
                        var token = JsonConvert.DeserializeObject<GooglePaymentResponseToken>(tokenData);
                        break;
                }
            }
        }
    }
}