namespace OpenStack.Services.Identity.V3
{
    using OpenStack.Net;

#if NET40PLUS
    using System;
#else
    using OpenStack.Compat;
#endif

    public class AuthenticateApiCall : DelegatingHttpApiCall<Tuple<TokenId, AuthenticateResponse>>
    {
        public AuthenticateApiCall(IHttpApiCall<Tuple<TokenId, AuthenticateResponse>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
