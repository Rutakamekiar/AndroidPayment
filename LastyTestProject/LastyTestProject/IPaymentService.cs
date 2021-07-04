using System;

namespace LastyTestProject
{
    public interface IPaymentService
    {
        event EventHandler CanMakePaymentsUpdated;

        event EventHandler<string> AuthorizationComplete;

        bool CanMakePayments { get; }

        void AuthorizePayment(decimal total);
    }
}