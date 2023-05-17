using graduation.Data.infrastructure;
using graduation.Data.infrastructure.Entities;
using Microsoft.Extensions.Options;


namespace graduation.Data
{
    public class MediaData : EntityBaseData<Model.Media>

    {
        public MediaData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}
