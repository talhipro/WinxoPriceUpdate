namespace Shared.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters.
    /// </summary>
    public static class AppSettings
    {
        public static bool isLoggedIn = false;
        public static bool isLoginAnimationDone = false;

        private static Plugin.Settings.Abstractions.ISettings Settings => Plugin.Settings.CrossSettings.Current;

        public static string ValidUntil
        {
            get => Settings.GetValueOrDefault(nameof(ValidUntil), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(ValidUntil), value);
        }

        public static string AccessToken
        {
            get => Settings.GetValueOrDefault(nameof(AccessToken), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(AccessToken), value);
        }
        public static string UserRoles
        {
            get => Settings.GetValueOrDefault(nameof(UserRoles), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(UserRoles), value);
        }

        public static string UserId
        {
            get => Settings.GetValueOrDefault(nameof(UserId), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(UserId), value);
        }

        public static string FullName
        {
            get => Settings.GetValueOrDefault(nameof(FullName), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(FullName), value);
        }

        public static string Phone
        {
            get => Settings.GetValueOrDefault(nameof(Phone), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(Phone), value);
        }

        public static string Email
        {
            get => Settings.GetValueOrDefault(nameof(Email), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(Email), value);
        }
        public static void ClearSettings()
        {
            ValidUntil = string.Empty;
            AccessToken = string.Empty;
            UserRoles = string.Empty;
            UserId = string.Empty;
            FullName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
        }
    }
}
