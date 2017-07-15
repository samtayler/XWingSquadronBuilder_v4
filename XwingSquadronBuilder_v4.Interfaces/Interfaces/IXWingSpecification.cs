using System.Collections.Generic;

namespace XWingSquadronBuilder_v4.Interfaces
{

    public enum SpecificationType
    {
        Critical,
        NonCritical
    }
    public interface IXWingSpecification<T>
    {
        string ErrorMessage { get; }
        SpecificationType SpecType { get; }

        bool IsSatisfiedBy(T entity, List<string> errors);
    }
}