// ReSharper disable once CheckNamespace
public class CurrentTool : Singleton<CurrentTool>
{
    /// Вызовет защищённый конструктор класса Singleton
    private CurrentTool() { }

    public ToolItem Tool;
}