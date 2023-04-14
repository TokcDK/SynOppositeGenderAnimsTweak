using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynOppositeGenderAnimsTweak
{
    public static class Ext
    {
        static readonly FormKey actorTypeNPCKeyword = FormKey.Factory("013794:Skyrim.esm");

        /// <summary>
        /// get race and check gender specific behavour path
        /// </summary>
        /// <param name="npcGetter"></param>
        /// <param name="state"></param>
        /// <param name="isFemale"></param>
        public static bool ChechAndFixRaceHaveOppositeAnimation(this INpcGetter npcGetter, IPatcherState<ISkyrimMod, ISkyrimModGetter> state, bool isFemale)
        {
            if (!npcGetter.Race.TryResolve<IRaceGetter>(state.LinkCache, out IRaceGetter? raceGetter))
            {
                return false;
            }

            // ignore all races where is missing ActorTypeNPC keyword
            if (raceGetter.Keywords == null || !raceGetter.Keywords.Contains(actorTypeNPCKeyword)) return false;

            bool needPathChange = false;
            if (isFemale)
            {
                needPathChange = raceGetter.BehaviorGraph?.Female?.File.DataRelativePath == "Meshes\\Actors\\Character\\DefaultMale.hkx";
            }
            else if (!isFemale)
            {
                needPathChange = raceGetter.BehaviorGraph?.Male?.File.DataRelativePath == "Meshes\\Actors\\Character\\DefaultFemale.hkx";
            }

            if (!needPathChange) return false;

            var raceToChange = state.PatchMod.Races.GetOrAddAsOverride(raceGetter);

            // patch race
            if (isFemale)
            {
                raceToChange.BehaviorGraph!.Female!.File.SetPath("Meshes\\Actors\\Character\\DefaultFemale.hkx");
            }
            else
            {
                raceToChange.BehaviorGraph!.Female!.File.SetPath("Meshes\\Actors\\Character\\DefaultMale.hkx");
            }
            Console.WriteLine($"Fix race behavour file path for'{raceToChange.FormKey.ID}|{raceToChange.EditorID}'");

            return true; // race was patched
        }
    }
}
