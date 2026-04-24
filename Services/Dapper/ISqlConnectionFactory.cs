using System.Data;

namespace quickassist.Services.Dapper
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection Create();
    }
}
