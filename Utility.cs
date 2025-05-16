using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API;
using System.Globalization;

namespace HeadshotExplode
{
    public partial class HeadshotExplode
    {
        public CParticleSystem? CreateParticle(Vector position, string filePath, float lifeTime)
        {
            CParticleSystem? particle = Utilities.CreateEntityByName<CParticleSystem>("info_particle_system");
            if (particle == null || !particle.IsValid)
                return null;

            particle.EffectName = filePath;
            particle.Teleport(position, new QAngle(), new Vector());
            particle.DispatchSpawn();

            particle.AcceptInput("Start");
            particle.AddEntityIOEvent("Kill", delay: lifeTime);

            return particle;
        }

        public Vector GetVectorFromString(string vector) => GetFromString(vector, (x, y, z) => new Vector(x, y, z));
        public QAngle GetQAngleFromString(string angles) => GetFromString(angles, (x, y, z) => new QAngle(x, y, z));

        public T GetFromString<T>(string values, Func<float, float, float, T> createInstance)
        {
            var split = values.Split(' ');

            if (split.Length >= 3 &&
                float.TryParse(split[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var x) &&
                float.TryParse(split[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var y) &&
                float.TryParse(split[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var z))
            {
                return createInstance(x, y, z);
            }

            return default!;
        }
    }
}
