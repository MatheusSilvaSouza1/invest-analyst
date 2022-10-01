using invest_analyst.Domain;

namespace invest_analyst.Services
{
    public interface ICsv
    {
        List<Idiv> Read(string path);
    }
}