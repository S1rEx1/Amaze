public class TutorialGnome : MonoBehaviour {
    public List<string> questPhrases;
    private int currentPhase = 0;
    public float appearDistance = 3f;

    void Update() {
        // Появление рядом с игроком
        if (Vector3.Distance(transform.position, player.position) > appearDistance) {
            transform.position = player.position + Random.insideUnitSphere * 2f;
        }
    }

    public void TriggerDialogue() {
        // Deltarune-стиль: отдельное окно с портретом
        UI_DialogueWindow.Show(questPhrases[currentPhase], gnomePortrait);
        
        if (currentPhase == 2) { // При выдаче квеста на ключ
            GameState.SpawnItem("Key", spawnPosition);
        }
    }
    
    // Вызывается при выполнении этапа
    public void AdvanceQuest() {
        currentPhase = Mathf.Min(currentPhase + 1, questPhrases.Count - 1);
    }
}
