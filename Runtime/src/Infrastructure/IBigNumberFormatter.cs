namespace UMF.Core.Infrastructure
{
    public interface IBigNumberFormatter
    {
        string Format(BigNumber bigNumber);

        string FormatToLower(BigNumber bigNumber);

        string FormatToBigger(BigNumber bigNumber);
    }
}