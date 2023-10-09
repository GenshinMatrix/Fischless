using Fischless.Fetch.Datas.Core;

namespace Fischless.Fetch.Datas.Character;

public class CharacterTalent
{
    public int TalentId { get; set; }

    public int Order { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Icon { get; set; }

    public float CdTime { get; set; }

    public int MaxChargeNumber { get; set; }

    public float CostElementValue { get; set; }

    public List<CharacterTalentLevel> Levels { get; set; }

}


public class CharacterTalentLevel
{
    public int ProudSkillId { get; set; }

    public int ProudSkillGroupId { get; set; }

    public int Level { get; set; }

    public int CoinCost { get; set; }

    public List<PromotionCostItem> CostItems { get; set; }

    public List<CharacterTalentLevelParam> Params { get; set; }
}


public class CharacterTalentLevelParam
{
    public string Title { get; set; }

    public string Value { get; set; }

    public string Desc { get; set; }
}
