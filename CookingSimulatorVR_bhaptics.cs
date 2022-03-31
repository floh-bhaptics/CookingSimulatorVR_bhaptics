using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MelonLoader;
using HarmonyLib;
using MyBhapticsTactsuit;

namespace CookingSimulatorVR_bhaptics
{
    public class CookingSimulatorVR_bhaptics : MelonMod
    {
        public static TactsuitVR tactsuitVr;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            tactsuitVr = new TactsuitVR();
            tactsuitVr.PlaybackHaptics("HeartBeat");
        }
        
        [HarmonyPatch(typeof(CuttingKnifeV2), "HapticsOnSuccessCut", new Type[] { typeof(BNG.ControllerHand) })]
        public class bhaptics_KnifeCutSuccess
        {
            [HarmonyPostfix]
            public static void Postfix(BNG.ControllerHand handSide)
            {
                tactsuitVr.Handle("CutSuccess", (handSide == BNG.ControllerHand.Right));
            }
        }

        [HarmonyPatch(typeof(CuttingKnifeV2), "HapticsOnFailedCut", new Type[] { typeof(BNG.ControllerHand) })]
        public class bhaptics_KnifeCutFailed
        {
            [HarmonyPostfix]
            public static void Postfix(BNG.ControllerHand handSide)
            {
                tactsuitVr.Handle("CutFailed", (handSide == BNG.ControllerHand.Right));
            }
        }

        [HarmonyPatch(typeof(CombinedProductPartEvents), "ManageHaptics", new Type[] { typeof(float) })]
        public class bhaptics_CombineProducts
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.Handle("Combine", true);
                tactsuitVr.Handle("Combine", false);
            }
        }

        [HarmonyPatch(typeof(BNG.Grabbable), "OnCollisionEnter")]
        public class bhaptics_GrabbableCollide
        {
            [HarmonyPostfix]
            public static void Postfix(BNG.Grabbable __instance)
            {
                
                tactsuitVr.Handle("CutSuccess", (__instance.GetPrimaryGrabber().HandSide == BNG.ControllerHand.Right), 0.5f);
            }
        }

        [HarmonyPatch(typeof(KitchenTimer), "StartRinging", new Type[] {  })]
        public class bhaptics_AlarmRing
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StartAlarm();
            }
        }

        [HarmonyPatch(typeof(KitchenTimer), "EndRinging", new Type[] { })]
        public class bhaptics_AlarmStop
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopAlarm();
            }
        }

        [HarmonyPatch(typeof(BlenderEvents), "StartBlender", new Type[] { })]
        public class bhaptics_BlenderStart
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StartBlender();
            }
        }

        [HarmonyPatch(typeof(BlenderEvents), "StopBlender", new Type[] { })]
        public class bhaptics_BlenderStop
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopBlender();
            }
        }

        [HarmonyPatch(typeof(MixContainer), "StartMix", new Type[] { })]
        public class bhaptics_MixerStart
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StartBlender();
            }
        }

        [HarmonyPatch(typeof(MixContainer), "EndMix", new Type[] { })]
        public class bhaptics_MixerStop
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.StopBlender();
            }
        }

        [HarmonyPatch(typeof(PlayerStats), "LevelUp", new Type[] { typeof(int), typeof(bool) })]
        public class bhaptics_LevelUp
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("LevelUp");
            }
        }

        [HarmonyPatch(typeof(Explosive), "Explode", new Type[] { typeof(bool), typeof(bool) })]
        public class bhaptics_Explode
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("ExplosionBelly");
            }
        }

    }
}
