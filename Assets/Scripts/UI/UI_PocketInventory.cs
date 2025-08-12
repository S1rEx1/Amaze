public class PocketInventory : MonoBehaviour {
    public GameObject pocketUI; // Полноэкранный UI
    public PocketType pocketType; // Enum: Left, Right
    public List<Rigidbody> itemsInPocket = new();

    void Update() {
        if (Input.GetKeyDown(pocketType == PocketType.Left ? KeyCode.Q : KeyCode.E)) {
            TogglePocket();
        }
    }

    void TogglePocket() {
        pocketUI.SetActive(!pocketUI.activeSelf);
        Cursor.visible = pocketUI.activeSelf;
        
        foreach (var item in itemsInPocket) {
            item.isKinematic = !pocketUI.activeSelf;
        }
    }

    public void AddItem(GameObject item) {
        var rb = item.AddComponent<Rigidbody>();
        rb.mass *= 0.25f; // Ослабленная гравитация
        rb.drag = 10f;
        itemsInPocket.Add(rb);
        item.transform.SetParent(pocketUI.transform);
    }
}
