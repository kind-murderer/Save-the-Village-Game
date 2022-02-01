using System;
using UnityEngine;

public class AchievementsServer : MonoBehaviour
{
    public enum Achievement
    {
        DragonsFavourite,
        OneBodyIsNobody,
        ItsPractice,
        QuickDeath
    }

    private int lastRecordDays = 9999;
    private int lastRecordMiners = 0; 
    private int lastRecordSlayers = 0;
    private bool haveDragonAchievement = false;
    private bool haveOneBodyAchievement = false;
    private bool haveItsPracticeAchievement = false;
    private bool haveQuickDeathAchievement = false;

    /// <summary>
    /// miners, slayers, day
    /// </summary>
    public event Action<int, int, int> NewRecord;
    public event Action<Achievement> AchievementGained;
    public void CheckVictorySummary(int numberOfMiners, int numberOfSlayers, int numberOfDragonsWeHad, int day, bool noLossInBattle)
    {
        if (!haveDragonAchievement)
        {
            if (numberOfDragonsWeHad >= 3)
            {
                AchievementGained?.Invoke(Achievement.DragonsFavourite);
                haveDragonAchievement = true;
            }
        }
        if (!haveItsPracticeAchievement)
        {
            if (noLossInBattle)
            {
                AchievementGained?.Invoke(Achievement.ItsPractice);
                haveItsPracticeAchievement = true;
            }
        }
        if (day < lastRecordDays)
        {
            NewRecord?.Invoke(numberOfMiners, numberOfSlayers, day);
        }
        else if (day == lastRecordDays && numberOfMiners >= lastRecordMiners && numberOfSlayers >= lastRecordSlayers)
        {
            NewRecord?.Invoke(numberOfMiners, numberOfSlayers, day);
        }
    }
    public void CheckDefeatSummary(int day, bool loseWithSingleSlayer, bool noLossInBattle)
    {
        if (!haveOneBodyAchievement)
        {
            if (loseWithSingleSlayer)
            {
                AchievementGained?.Invoke(Achievement.OneBodyIsNobody);
                loseWithSingleSlayer = true;
            }
        }
        if (!haveQuickDeathAchievement)
        {
            if (day == 3)
            {
                AchievementGained?.Invoke(Achievement.QuickDeath);
                haveQuickDeathAchievement = true;
            }
        }
        if (!haveItsPracticeAchievement)
        {
            if (noLossInBattle)
            {
                AchievementGained?.Invoke(Achievement.ItsPractice);
                haveItsPracticeAchievement = true;
            }
        }
    }
}
