using System;

[Serializable]
public class TriggerCondition
{
    public enum TriggerType
    {
        AND, OR
    }

    public TriggerType triggerType = TriggerType.AND;
    public ConditionStat[] conditionsStats;
    public ConditionTrait[] conditionsTraits;
}
