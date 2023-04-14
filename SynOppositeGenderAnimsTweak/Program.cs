using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynOppositeGenderAnimsTweak
{
    public class Program
    {
        public static Lazy<Settings> PatchSettings = null!;
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetAutogeneratedSettings("Settings", "settings.json", out PatchSettings)
                .SetTypicalOpen(GameRelease.SkyrimLE, "SynOppositeGenderAnimsTweak.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            // set local vars
            bool isCheckMales = PatchSettings.Value.IsCheckMales;
            bool isCheckFemales = PatchSettings.Value.IsCheckFemales;
            bool isOnlyUnique = PatchSettings.Value.IsOnlyUnique;
            bool checkRaceBehavourPath = PatchSettings.Value.CheckRaceBehavourPath;
            var oppositeGenderAnimsFlag = NpcConfiguration.Flag.OppositeGenderAnims;
            var uniqueFlag = NpcConfiguration.Flag.Unique;
            var femaleFlag = NpcConfiguration.Flag.Female;
            bool isUseExcluded = PatchSettings.Value.ExcludedNPCKeywordsList.Count > 0;
            var excludedList = PatchSettings.Value.ExcludedNPCKeywordsList;
            bool isUseExcludedNpcList = excludedList.Count > 0 && excludedList.Any(s => !string.IsNullOrWhiteSpace(s));

            int npcPatched = 0;
            int racePatched = 0;
            var checkedRaces = checkRaceBehavourPath ? new HashSet<FormKey>() : null; // not check races which already was checked, must reduce time of race patching
            foreach (var npcGetter in state.LoadOrder.PriorityOrder.Npc().WinningOverrides())
            {
                // skip invalid npcs
                if (npcGetter == null) continue; // null
                //if (npcGetter.EditorID == "LashGraDushnikh")
                //{

                //}

                bool isFemale = npcGetter.Configuration.Flags.HasFlag(femaleFlag);
                if (isFemale && !isCheckFemales) continue; // disabled females but npc is female
                else if (!isFemale && !isCheckMales) continue; // disabled males but npc is male

                if (isOnlyUnique && !npcGetter.Configuration.Flags.HasFlag(uniqueFlag)) continue; // need only unique but not unique
                if (isUseExcluded
                    && !string.IsNullOrWhiteSpace(npcGetter.EditorID)
                    && isUseExcludedNpcList
                    && excludedList.Contains(npcGetter.EditorID)) continue; // editor id contains one of keywords from exclusions

                if (checkRaceBehavourPath && !checkedRaces!.Contains(npcGetter.Race.FormKey))
                {
                    checkedRaces.Add(npcGetter.Race.FormKey);

                    if (npcGetter.ChechAndFixRaceHaveOppositeAnimation(state, isFemale)) racePatched++;
                }

                if (!npcGetter.Configuration.Flags.HasFlag(oppositeGenderAnimsFlag)) continue; // have no opposite gender anims flag

                Console.WriteLine($"Remove opposite gender flag for '{npcGetter.FormKey.ID}|{npcGetter.EditorID}'");
                state.PatchMod.Npcs.GetOrAddAsOverride(npcGetter).Configuration.Flags &= ~oppositeGenderAnimsFlag;
                npcPatched++;
            }

            Console.WriteLine($"Finished! Patched {npcPatched} npcs and {racePatched} races.");
        }
    }
}
