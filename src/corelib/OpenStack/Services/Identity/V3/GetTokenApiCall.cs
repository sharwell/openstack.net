namespace OpenStack.Services.Identity.V3
{
    using OpenStack.Net;

#if NET40PLUS
    using System;
#else
    using OpenStack.Compat;
#endif

    public class GetTokenApiCall : DelegatingHttpApiCall<Tuple<TokenId, AuthenticateResponse>>
    {
        public GetTokenApiCall(IHttpApiCall<Tuple<TokenId, AuthenticateResponse>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}
