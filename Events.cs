using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core;

namespace HeadshotExplode
{
    public partial class HeadshotExplode
    {
        [GameEventHandler]
        public HookResult OnPlayerDeathHandler(EventPlayerDeath @event, GameEventInfo info)
        {
            if (!Enable.Value || !@event.Headshot)
                return HookResult.Continue;

            var victim = @event.Userid;
            CreateExplode(victim);

            return HookResult.Continue;
        }
    }
}
