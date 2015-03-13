
using System.Text;
namespace NISOCountries.Core.ValueNormalizers
{
    public interface IValueNormalizer<T>
        where T : IISOCountry
    {
        NormalizationForm NormalizationForm { get; }
        NormalizeFlags NormalizeFlags { get; }

        T Normalize(T value);
    }
}
