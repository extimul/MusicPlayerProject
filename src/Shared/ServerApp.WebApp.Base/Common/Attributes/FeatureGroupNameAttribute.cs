namespace ServerApp.WebApp.Base.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class FeatureGroupNameAttribute : Attribute
{
    public FeatureGroupNameAttribute(string groupName) => GroupName = groupName;

    public string GroupName { get; }
}