using Mutagen.Bethesda.Synthesis.Settings;

namespace SynOppositeGenderAnimsTweak
{
    public class Settings
    {
        const string _checkMalesDescr = "Check Male npcs for the flag. Disabled by default. Usually flag can be added for man elves.";
        [SynthesisSettingName("Check Male npcs")]
        [SynthesisTooltip(_checkMalesDescr)]
        [SynthesisDescription(_checkMalesDescr)]
        public bool IsCheckMales { get; set; } = false;

        const string _checkFemalesDescr = "Check Female npcs for the flag. Usually flag can be added for strong womans in heavy armor.";
        [SynthesisSettingName("Check Female npcs")]
        [SynthesisTooltip(_checkFemalesDescr)]
        [SynthesisDescription(_checkFemalesDescr)]
        public bool IsCheckFemales { get; set; } = true;

        const string _onlyUniqueDescr = "Remove opposite gender flag only from unique npcs";
        [SynthesisSettingName("Only unique npc")]
        [SynthesisTooltip(_onlyUniqueDescr)]
        [SynthesisDescription(_onlyUniqueDescr)]
        public bool IsOnlyUnique { get; set; } = false;

        const string _checkRaceBehDescr = "Some races like Ork can have no opposite gender flag\nbut have same animation flag for both genders.\nThe option will enable checking races like Orc and will change male animation of womans to female";
        [SynthesisSettingName("Check race behavour path")]
        [SynthesisTooltip(_checkRaceBehDescr)]
        [SynthesisDescription(_checkRaceBehDescr)]
        public bool CheckRaceBehavourPath { get; set; } = true;

        [SynthesisSettingName("Excluded npc formid keywords")]
        [SynthesisTooltip("Enter here keywords wich is containing in npc's editorid which must be excluded.\nFor example keyword 'bandit' will skip all records where is editorid contains string 'bandit'. Case insensetive.")]
        [SynthesisDescription("List of keywords containing in npc editorid which must be excluded")]
        public List<string> ExcludedNPCKeywordsList { get; set; } = new List<string>();
    }
}