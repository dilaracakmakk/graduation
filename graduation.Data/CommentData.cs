using graduation.Data.infrastructure;
using graduation.Data.infrastructure.Entities;
using Microsoft.Extensions.Options;


namespace graduation.Data
{
    public class CommentData : EntityBaseData<Model.Comment>

    {
        public CommentData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}
