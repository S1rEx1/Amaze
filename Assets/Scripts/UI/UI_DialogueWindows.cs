public class UI_DialogueWindow : MonoBehaviour {
    public Image characterPortrait;
    public Text dialogueText;
    public CanvasGroup group;
    
    public static void Show(string text, Sprite portrait=null) {
        instance.characterPortrait.sprite = portrait;
        instance.dialogueText.text = text;
        instance.group.alpha = 1;
        Time.timeScale = 0; // Пауза игры
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && group.alpha > 0) {
            group.alpha = 0;
            Time.timeScale = 1; // Возобновление игры
        }
    }
}
