using graduation.Data.infrastructure;
using graduation.Data.infrastructure.Entities;
using Microsoft.Extensions.Options;


namespace graduation.Data
{
    public class ContentData : EntityBaseData<Model.Content>

    {
        public ContentData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}
