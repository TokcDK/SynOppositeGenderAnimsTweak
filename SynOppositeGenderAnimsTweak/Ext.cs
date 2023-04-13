using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynOppositeGenderAnimsTweak
{
    public static class Ext
    {
        /// <summary>
        /// get race and check gender specific behavour path
        /// </summary>
        /// <param name="npcGetter"></param>
        /// <param name="state"></param>
        /// <param name="isFemale"></param>
        public static void ChechAndFixRaceHaveOppositeAnimation(this INpcGetter npcGetter, IPatcherState<ISkyrimMod, ISkyrimModGetter> state, bool isFemale)
        {
            if (!npcGetter.Race.TryResolve<IRaceGetter>(state.LinkCache, out IRaceGetter? raceGetter))
            {
                return;
            }

            bool needPathChange = false;
            if (isFemale)
            {
                needPathChange = raceGetter.BehaviorGraph?.Female?.File.DataRelativePath == "Actors\\Character\\DefaultMale.hkx";
            }
            else if (!isFemale)
            {
                needPathChange = raceGetter.BehaviorGraph?.Male?.File.DataRelativePath == "Actors\\Character\\DefaultFemale.hkx";
            }

            if (!needPathChange) return;

            var raceToChange = state.PatchMod.Races.GetOrAddAsOverride(raceGetter);

            if (isFemale)
            {
                raceToChange.BehaviorGraph!.Female!.File.SetPath("Actors\\Character\\DefaultFemale.hkx");
            }
            else
            {
                raceToChange.BehaviorGraph!.Female!.File.SetPath("Actors\\Character\\DefaultMale.hkx");
            }
        }
    }
}
