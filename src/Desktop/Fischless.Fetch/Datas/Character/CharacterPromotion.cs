using Fischless.Fetch.Datas.Core;

namespace Fischless.Fetch.Datas.Character;

public class CharacterPromotion
{
    public int AvatarPromoteId { get; set; }

    public int PromoteLevel { get; set; }

    public int ScoinCost { get; set; }

    public int UnlockMaxLevel { get; set; }

    public List<PromotionCostItem> CostItems { get; set; }

    public List<PromotionAddProperty> AddProps { get; set; }

    public int RequiredPlayerLevel { get; set; }
}
