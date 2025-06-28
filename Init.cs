using MelonLoader;
using SteamAuthenticatorForQuestToLogInWithASteamTicket;

[assembly: MelonInfo(typeof(MelonLoaderInit), ModInfo.Name, ModInfo.Version, ModInfo.Author)]
[assembly: MelonGame("Duttbust", "Capuchin")]

namespace SteamAuthenticatorForQuestToLogInWithASteamTicket
{
    public class ModInfo
    {
        public const string Name = "SteamAuthenticatorForQuestToLogInWithASteamTicket";
        public const string Author = "Monky";
        public const string Version = "1.0.0";
    }
    public class MelonLoaderInit : MelonMod;
}
