using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Configs
{
    public static class AuthOptions
    {
        public const string ISSUER = "TimeManagementTokenIssuer";
        public const string AUDIENCE = "TimeManagementTokenClient";
        const string KEY = "JeLiOnSeCrEtKKKey0ca6e1def6c94b82965a044232ee685a";
        public const int LIFETIME = 7; // дни
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
