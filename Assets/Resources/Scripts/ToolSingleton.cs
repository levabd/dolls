using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <typeparam name="M">Singleton class</typeparam>
// ReSharper disable once CheckNamespace
public class ToolSingleton<M> where M : class
{
    protected ToolSingleton() { }

    /// Отложенная инициализация экземпляра класса
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class SingletonCreator<S> where S : class
    {
        // Reflection для создания экземпляра класса без публичного конструктора
        private static readonly S _instance = (S)typeof(S).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[0],
            new ParameterModifier[0]).Invoke(null);

        // ReSharper disable once ConvertToAutoProperty
        public static S CreatorInstance => _instance;
    }

    public static M Instance => SingletonCreator<M>.CreatorInstance;
}

public class CurrentTool : Singleton<CurrentTool>
{
    /// Вызовет защищённый конструктор класса Singleton
    private CurrentTool() { }

    public ToolItem Tool;
}