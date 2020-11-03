using Core.src.Entity;

namespace Core.src.Utils
{
    public interface IBigNumberFormatter
    {
        string Format(BigNumber bigNumber);

        string FormatToLower(BigNumber bigNumber);

        string FormatToBigger(BigNumber bigNumber);
    }
}