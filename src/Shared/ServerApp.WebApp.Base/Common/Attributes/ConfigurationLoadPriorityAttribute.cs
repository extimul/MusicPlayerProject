namespace ServerApp.WebApp.Base.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ConfigurationLoadPriorityAttribute : Attribute
{
    public int Priority { get; }

    public ConfigurationLoadPriorityAttribute(int priority)
    {
        Priority = priority;
    }
}