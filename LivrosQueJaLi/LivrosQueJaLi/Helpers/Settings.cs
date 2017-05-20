// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LivrosQueJaLi.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings { get { return CrossSettings.Current; } }

        #region Setting Constants

        private const string UserKey = "user_id";
        private static readonly string UserDefault = string.Empty;

        private const string AuthTokenKey = "auth_token";
        private static readonly string AuthTokenDefault = string.Empty;

        private const string AccessTokenKey = "access_token";
        private static readonly string AccessTokenDefault = string.Empty;

        #endregion 

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault<string>(UserKey, UserDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserKey, value); }
        }

        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault<string>(AuthTokenKey, AuthTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AuthTokenKey, value); }
        }

        public static string AccessToken
        {
            get { return AppSettings.GetValueOrDefault<string>(AccessTokenKey, AccessTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AccessTokenKey, value); }
        }
    }
}