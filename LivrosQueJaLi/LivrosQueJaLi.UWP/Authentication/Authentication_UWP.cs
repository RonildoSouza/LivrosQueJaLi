﻿using LivrosQueJaLi.Authentication;
using LivrosQueJaLi.UWP.Authentication;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Authentication_UWP))]
namespace LivrosQueJaLi.UWP.Authentication
{
    public class Authentication_UWP : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(
            IMobileServiceClient pClient, MobileServiceAuthenticationProvider pProvider)
        {
            try
            {
                var msUser = await pClient.LoginAsync(pProvider);
                return msUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task LogoutAsync(IMobileServiceClient pClient)
        {
            try
            {
                await pClient.LogoutAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
