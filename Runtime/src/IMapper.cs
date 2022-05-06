namespace MF.Core.Scripts.Core.src
{
    public interface IMapper<in TFrom, out TTo>
    {
        TTo Map(TFrom from);
    }
}