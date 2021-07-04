using System;
using Android.Gms.Tasks;
using Android.Gms.Wallet;
using AndroidX.AppCompat.App;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace LastyTestProject.Droid
{
    public class PaymentService : AppCompatActivity, IPaymentService, IOnCompleteListener
    {
        public PaymentsClient PaymentsClient { get; set; }

        public PaymentService()
        {
            PaymentsClient = WalletClass.GetPaymentsClient(Android.App.Application.Context,
                                                           new WalletClass.WalletOptions.Builder()
                                                               .SetEnvironment(WalletConstants.EnvironmentTest)
                                                               .Build());

            var readyToPayRequest = IsReadyToPayRequest.FromJson(GetReadyToPayRequest());
            var task = PaymentsClient.IsReadyToPay(readyToPayRequest);
            task.AddOnCompleteListener(this);
        }

        public event EventHandler CanMakePaymentsUpdated;

        public event EventHandler<string> AuthorizationComplete;

        public bool CanMakePayments { get; set; }

        public void AuthorizePayment(decimal total)
        {
            AutoResolveHelper.ResolveTask(PaymentsClient.LoadPaymentData(CreatePaymentDataRequest(total)),
                                          Platform.CurrentActivity,
                                          999);
        }

        protected PaymentDataRequest CreatePaymentDataRequest(decimal total)
        {
            var request = GetBaseRequest();

            request.TransactionInfo = new TransactionInfo
            {
                TotalPrice = total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                TotalPriceStatus = "FINAL",
                CurrencyCode = "UAH"
            };

            return PaymentDataRequest.FromJson(JsonConvert.SerializeObject(request));
        }

        public void OnComplete(Task completeTask)
        {
            CanMakePayments = completeTask.IsComplete;
            CanMakePaymentsUpdated?.Invoke(this, null);
        }

        public string GetReadyToPayRequest() => JsonConvert.SerializeObject(GetBaseRequest());

        protected GooglePaymentRequest GetBaseRequest() =>
            new GooglePaymentRequest
            {
                ApiVersion = 2,
                ApiVersionMinor = 0,
                MerchantInfo = new MerchantInfo { MerchantName = "my shop" },
                AllowedPaymentMethods = new[]
                {
                    new PaymentMethod
                    {
                        Type = "CARD",
                        Parameters = new PaymentParameters
                        {
                            AllowedAuthMethods = new[] { "PAN_ONLY", "CRYPTOGRAM_3DS" },
                            AllowedCardNetworks = new[] { "MASTERCARD", "VISA" }
                        },
                        TokenizationSpecification = new TokenizationSpecification
                        {
                            Type = "PAYMENT_GATEWAY",
                            Parameters = new TokenizationSpecificationParameters
                            {
                                Gateway = "liqpay",
                                GatewayMerchantId = "sandbox_i25615435363"
                            }
                        }
                    }
                }
            };
    }
}