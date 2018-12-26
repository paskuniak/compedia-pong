public interface IGameStateManager {
    bool playerGameOn { get; }
    bool player2Human { get; set; }

    void StartGame(bool p1Human, bool p2Human, int p1points = 0, int p2points = 0);

    void SetPaused(bool paused);

    bool HasSavedGame();
    void LoadGame();
    void SaveGame();

}