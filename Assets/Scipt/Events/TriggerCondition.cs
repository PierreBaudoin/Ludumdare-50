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

    public bool isActive(Character target)
    {
        bool flag;

        if(triggerType == TriggerType.AND)
        {
            flag = true;
            foreach(ConditionStat c in conditionsStats)
            {
                flag = flag && c.IsValid(target);
            }

            foreach(ConditionTrait c in conditionsTraits)
            {
                flag = flag && c.IsValid(target);
            }
        }
        else
        {
            flag = false;
            foreach(ConditionStat c in conditionsStats)
            {
                flag = flag || c.IsValid(target);
            }

            foreach(ConditionTrait c in conditionsTraits)
            {
                flag = flag || c.IsValid(target);
            }
        }

        return flag;
    }


}
