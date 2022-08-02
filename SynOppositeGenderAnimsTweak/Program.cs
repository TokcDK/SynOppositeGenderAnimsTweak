using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynOppositeGenderAnimsTweak
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "SynOppositeGenderAnimsTweak.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            foreach (var npcGetter in state.LoadOrder.PriorityOrder.Npc().WinningOverrides())
            {
                if (npcGetter == null) continue;
                if (!npcGetter.Configuration.Flags.HasFlag(NpcConfiguration.Flag.OppositeGenderAnims)) continue;

                Console.WriteLine($"Remove opposite gender flag for '{npcGetter.FormKey.ID}|{npcGetter.EditorID}'");
                state.PatchMod.Npcs.GetOrAddAsOverride(npcGetter).Configuration.Flags &= ~NpcConfiguration.Flag.OppositeGenderAnims;
            }
        }
    }
}
