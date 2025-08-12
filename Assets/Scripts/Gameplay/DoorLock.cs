public class DoorLock : MonoBehaviour {
    [Range(0, 100)] public int keyFailChance = 30; // 30% шанс отказа
    public GameObject dynamiteAlternative; // Префаб динамита
    
    void OnInteract(Player player) {
        if (player.Inventory.HasItem("Key")) {
            if (Random.Range(0, 100) > keyFailChance) {
                OpenDoor();
            } else {
                // Ключ сломался
                player.Inventory.RemoveItem("Key");
                DialogueSystem.Show("Ключ сломался в замке! Нужен динамит");
                SpawnDynamite();
            }
        }
    }
    
    void SpawnDynamite() {
        Instantiate(dynamiteAlternative, transform.position + Vector3.up, Quaternion.identity);
    }
}
