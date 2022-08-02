using Mutagen.Bethesda.Synthesis.Settings;

namespace SynOppositeGenderAnimsTweak
{
    public class Settings
    {
        [SynthesisSettingName("Check Male npcs")]
        [SynthesisTooltip("Check Male npcs for the flag. Disabled by default. Usually flag can be added for man elves.")]
        [SynthesisDescription("Check Male npcs for the flag. Disabled by default. Usually flag can be added for man elves.")]
        public bool IsCheckMales { get; set; } = false;
        [SynthesisSettingName("Check Female npcs")]
        [SynthesisTooltip("Check Female npcs for the flag. Usually flag can be added for strong womans in heavy armor.")]
        [SynthesisDescription("Check Female npcs for the flag. Usually flag can be added for strong womans in heavy armor.")]
        public bool IsCheckFemales { get; set; } = true;

        [SynthesisSettingName("Only unique npc")]
        [SynthesisTooltip("Remove opposite gender flag only from unique npcs")]
        [SynthesisDescription("Remove opposite gender flag only from unique npcs")]
        public bool IsOnlyUnique { get; set; } = false;

        [SynthesisSettingName("Excluded npc formid keywords")]
        [SynthesisTooltip("Enter here keywords wich is containing in npc's editorid which must be excluded.\nFor example keyword 'bandit' will skip all records where is editorid contains string 'bandit'. Case insensetive.")]
        [SynthesisDescription("List of keywords containing in npc editorid which must be excluded")]
        public List<string> ExcludedNPCKeywordsList { get; set; } = new List<string>();
    }
}