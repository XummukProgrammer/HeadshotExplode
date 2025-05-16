using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using System.Runtime.InteropServices;

namespace HeadshotExplode
{
    public partial class HeadshotExplode
    {
        // Search string with "DeathCry" and the function using this argument is EmitSoundParams.
        private static MemoryFunctionVoid<CBaseEntity, string, int, float, float> CBaseEntity_EmitSoundParamsFunc = new(GameData.GetSignature("CBaseEntity_EmitSoundParams"));
        private static MemoryFunctionWithReturn<nint, nint, uint, uint, short, ulong, ulong> CSoundOpGameSystem_StartSoundEventFunc = new(GameData.GetSignature("CSoundOpGameSystem_StartSoundEvent"));
        private static MemoryFunctionVoid<nint, nint, ulong, nint, nint, short, byte> CSoundOpGameSystem_SetSoundEventParamFunc = new(GameData.GetSignature("CSoundOpGameSystem_SetSoundEventParam"));
        private static MemoryFunctionVoid<nint, string> StopSoundEvent = new(GameData.GetSignature("StopSoundEvent"));

        internal static void Init()
        {
            //CSoundOpGameSystem_StartSoundEventFunc.Hook(CSoundOpGameSystem_StartSoundEventFunc_PostHook, HookMode.Post);
        }

        internal static void CleanUp()
        {
            //CSoundOpGameSystem_StartSoundEventFunc.Unhook(CSoundOpGameSystem_StartSoundEventFunc_PostHook, HookMode.Post);
        }

        [ThreadStatic]
        private static IReadOnlyDictionary<string, float>? CurrentParameters;

        /// <summary>
        /// Emit a sound event by name (e.g., "Weapon_AK47.Single").
        /// TODO: parameters passed in here only seem to work for sound events shipped with the game, not workshop ones.
        /// </summary>
        public static void EmitSound(CBaseEntity? entity, string soundName, IReadOnlyDictionary<string, float>? parameters = null)
        {
            if (entity == null)
                return;

            if (!entity.IsValid)
            {
                throw new ArgumentException("Entity is not valid.");
            }

            try
            {
                // We call CBaseEntity::EmitSoundParams,
                // which calls a method that returns an ID that we can use
                // to modify the playing sound.

                CurrentParameters = parameters;

                // Pitch, volume etc aren't actually used here
                CBaseEntity_EmitSoundParamsFunc.Invoke(entity, soundName, 100, 1f, 0f);
            }
            finally
            {
                CurrentParameters = null;
            }
        }

        public static void StopSound(CBaseEntity entity, string soundName)
        {
            if (entity == null || !entity.IsValid || entity.Entity == null)
            {
                throw new ArgumentException("Entity is not valid.");
            }

            StopSoundEvent.Invoke(entity.Entity.Handle, soundName);
        }

        /*private static HookResult CSoundOpGameSystem_StartSoundEventFunc_PostHook(DynamicHook hook)
        {
            if (CurrentParameters is not { Count: > 0 })
            {
                return HookResult.Continue;
            }

            var pSoundOpGameSystem = hook.GetParam<nint>(0);
            var pFilter = hook.GetParam<nint>(1);
            var soundEventId = hook.GetReturn<ulong>();

            foreach (var parameter in CurrentParameters)
            {
                CSoundOpGameSystem_SetSoundEventParam(pSoundOpGameSystem, pFilter,
                    soundEventId, parameter.Key, parameter.Value);
            }

            return HookResult.Continue;
        }*/

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct FloatParamData
        {
            // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
            private readonly uint _type1;
            private readonly uint _type2;

            private readonly uint _size1;
            private readonly uint _size2;

            private readonly float _value;
            private readonly uint _padding;
            // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

            public FloatParamData(float value)
            {
                _type1 = 1;
                _type2 = 8;

                _size1 = 4;
                _size2 = 4;

                _value = value;
                _padding = 0;
            }
        }

        /*private static unsafe void CSoundOpGameSystem_SetSoundEventParam(nint pSoundOpGameSystem, nint pFilter,
            ulong soundEventId, string paramName, float value)
        {
            var data = new FloatParamData(value);
            var nameByteCount = Encoding.UTF8.GetByteCount(paramName);

            var pData = Unsafe.AsPointer(ref data);
            var pName = stackalloc byte[nameByteCount + 1];

            Encoding.UTF8.GetBytes(paramName, new Span<byte>(pName, nameByteCount));

            CSoundOpGameSystem_SetSoundEventParamFunc.Invoke(pSoundOpGameSystem, pFilter, soundEventId, (nint)pName, (nint)pData, 0, 0);
        }*/
    }
}
