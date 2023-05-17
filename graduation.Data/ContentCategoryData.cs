using graduation.Data.infrastructure;
using graduation.Data.infrastructure.Entities;
using Microsoft.Extensions.Options;


namespace graduation.Data
{
    public class ContentCategoryData : EntityBaseData<Model.ContentCategory>

    {
        public ContentCategoryData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}
