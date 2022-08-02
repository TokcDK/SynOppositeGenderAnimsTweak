using Mutagen.Bethesda.Synthesis.Settings;

namespace SynOppositeGenderAnimsTweak
{
    public class Settings
    {
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