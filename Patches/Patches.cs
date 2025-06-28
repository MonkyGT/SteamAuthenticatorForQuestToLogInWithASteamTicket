using System.IO;
using HarmonyLib;
using Il2Cpp;
using Il2CppCapuchinPlayFab;
using Il2CppPlayFab;
using Il2CppPlayFab.ClientModels;
using Il2CppSystem.Threading.Tasks;
using MelonLoader.Utils;

namespace SteamAuthenticatorForQuestToLogInWithASteamTicket.Patches
{
    [HarmonyPatch(typeof(LoginManager), nameof(LoginManager.StartLoginProcess))]
    public class StartLoginProcessPatch
    {
        public static bool Prefix(LoginManager __instance)
        {
            var authTicketPath = Path.Combine(MelonEnvironment.UserDataDirectory, "steam_ticket.txt");

            if (!File.Exists(authTicketPath))
            {
                return true;
            }

            Il2CppSystem.Collections.Generic.Dictionary<string, string> dictionary = new();
            dictionary.Add("MetaId", "STEAM");
            dictionary.Add("MetaAuth", "STEAM");

            string authTicket = File.ReadAllText(authTicketPath);
            LR_DebugMenu.instance.debugValuePairs.Add("loginAttempted", true);

            var loginRequest = new LoginWithSteamRequest
            {
                SteamTicket = authTicket,
                CreateAccount = new Il2CppSystem.Nullable<bool>(false),
                CustomTags = dictionary,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserInventory = true,
                    GetUserVirtualCurrency = true,
                    GetTitleData = true,
                    GetPlayerProfile = true
                }
            };
            System.Action<LoginResult> onSuccess = __instance.OnLogIn;
            System.Action<PlayFabError> onError = __instance.OnPlayFabError;

            PlayFabClientAPI.LoginWithSteam(loginRequest, onSuccess, onError);
            return false;
        }
    }

    [HarmonyPatch(typeof(FusionHub), nameof(FusionHub.GetNonce))]
    public class GetNoncePatch
    {
        static bool Prefix(ref Task<string> __result)
        {
            __result = Task.FromResult("STEAM");
            return false;
        }
    }
}
