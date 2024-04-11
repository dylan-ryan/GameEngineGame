using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public enum QuestStage
    {
        QuestStart,
        QuestMiddle,
        QuestEnd
    }

    [SerializeField]
    private QuestStage currentQuestStage = QuestStage.QuestStart;

    public QuestStage GetCurrentQuestStage()
    {
        return currentQuestStage;
    }

    public void SetCurrentQuestStage(QuestStage newQuestStage)
    {
        currentQuestStage = newQuestStage;
    }
}
