using Mutagen.Bethesda.Synthesis.Settings;

namespace SynOppositeGenderAnimsTweak
{
    public class Settings
    {
        [SynthesisSettingName("Only unique npc")]
        [SynthesisTooltip("Remove opposite gender flag only from unique npcs")]
        [SynthesisDescription("Remove opposite gender flag only from unique npcs")]
        public bool IsOnlyUnique { get; set; } = false;
    }
}