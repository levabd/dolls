using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <typeparam name="T">Singleton class</typeparam>
// ReSharper disable once CheckNamespace
public class Singleton<T> where T : class
{
    protected Singleton() { }

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

    public static T Instance => SingletonCreator<T>.CreatorInstance;
}

public class CurrentExam : Singleton<CurrentExam>
{
    /// Вызовет защищённый конструктор класса Singleton
    private CurrentExam() { }

    public BaseExam Exam;
}