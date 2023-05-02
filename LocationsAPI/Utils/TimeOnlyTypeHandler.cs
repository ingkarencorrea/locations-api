using System.Data;
using Dapper;

namespace LocationsAPI.Utils;

public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly> // Dapper handler for TimeOnly
{
    public override TimeOnly Parse(object value)
    {
        if (value is DateTime) return TimeOnly.FromDateTime((DateTime)value);
        if (value is TimeSpan timeSpanValue) return TimeOnly.FromTimeSpan(timeSpanValue);
        if (TimeOnly.TryParse(value.ToString(), out var timeOnlyValue)) return timeOnlyValue;
        throw new ArgumentException($"Cannot parse '{value}' as TimeOnly.");
    }

    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.Value = value;
    }
}