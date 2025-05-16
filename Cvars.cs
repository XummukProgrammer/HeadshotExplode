using CounterStrikeSharp.API.Modules.Cvars;

namespace HeadshotExplode
{
    public partial class HeadshotExplode
    {
        public FakeConVar<bool> Enable = new("hexd_enable", "Enable the plugin?", true);
        public FakeConVar<string> ParticleFile = new("hexd_particle_file", "The particle file.", "particles/explosions_fx/c4_train_ground_effect.vpcf");
        public FakeConVar<float> ParticleLifeTime = new("hexd_particle_life_time", "The lifetime of the particle.", 1);
        public FakeConVar<string> ParticleOrigin = new("hexd_particle_origin", "The origin of the particle.", "0 0 50");
        public FakeConVar<string> SoundFile = new("hexd_sound", "The sound of an explosion.", "hexd.explode");
    }
}
