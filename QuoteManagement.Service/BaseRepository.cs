using Microsoft.Extensions.Options;
using QuoteManagement.Model;

namespace QuoteManagement.Service
{
    public abstract class BaseRepository
    {
        public readonly IOptions<DataConfig> _dataConfig;

        public BaseRepository(IOptions<DataConfig> dataConfig)
        {
            _dataConfig = dataConfig;
        }
    }
}
