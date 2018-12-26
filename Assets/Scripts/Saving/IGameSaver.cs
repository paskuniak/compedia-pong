public interface IGameSaver {
    bool HasSavedGame();
    bool LoadGame(out SaveData data);
    void SaveGame(SaveData data);
}