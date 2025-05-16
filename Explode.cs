using CounterStrikeSharp.API.Core;

namespace HeadshotExplode
{
    public partial class HeadshotExplode
    {
        public void CreateExplode(CCSPlayerController? controller)
        {
            if (controller == null)
                return;

            var position = controller.PlayerPawn.Value?.AbsOrigin;
            if (position == null)
                return;

            var particleFile = ParticleFile.Value;
            float lifeTime = ParticleLifeTime.Value;
            var origin = ParticleOrigin.Value;

            var soundFile = SoundFile.Value;

            if (string.IsNullOrEmpty(particleFile))
                return;

            if (!string.IsNullOrEmpty(origin))
                position += GetVectorFromString(origin);

            if (!string.IsNullOrEmpty(soundFile))
                EmitSound(controller, soundFile);

            CreateParticle(position, particleFile, lifeTime);
        }
    }
}
