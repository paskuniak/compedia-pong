public interface IUIManager {
    void Init(IGameStateManager gm, Settings newSettings);
    void ShowMenu();
    void HideMenu();
    void ShowText(string message);
    void UpdatePoints(int p1Points, int p2Points);
}