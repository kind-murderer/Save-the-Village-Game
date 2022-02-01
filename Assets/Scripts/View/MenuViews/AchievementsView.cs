using UnityEngine;
using UnityEngine.UI;

public class AchievementsView : MonoBehaviour
{
    [SerializeField]
    private Image dragonsFavouriteImage, oneBodyIsNobodyImage, itsPracticeImage, quickDeathImage;
    [SerializeField]
    private Sprite starSprite;
    [SerializeField]
    private Text recordText;

    private void Start()
    {
        AchievementsServer achievementsServer = gameObject.GetComponent<AchievementsServer>();
        achievementsServer.newRecord += UpdateRecord;
        achievementsServer.AchievementGained += SetStar;
    }
    public void SetStar(AchievementsServer.Achievement achievement)
    {
        switch (achievement)
        {
            case AchievementsServer.Achievement.DragonsFavourite:
                dragonsFavouriteImage.sprite = starSprite;
                break;
            case AchievementsServer.Achievement.OneBodyIsNobody:
                oneBodyIsNobodyImage.sprite = starSprite;
                break;
            case AchievementsServer.Achievement.ItsPractice:
                itsPracticeImage.sprite = starSprite;
                break;
            case AchievementsServer.Achievement.QuickDeath:
                quickDeathImage.sprite = starSprite;
                break;
        }
    }
        public void UpdateRecord(int miners, int slayers, int day)
    {
        recordText.text = string.Format("You won on the {0} day with {1} miners and {2} dragon slayers left", day, miners, slayers);
    }
}