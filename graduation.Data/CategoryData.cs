using graduation.Data.infrastructure;
using graduation.Data.infrastructure.Entities;
using Microsoft.Extensions.Options;
using System;

namespace graduation.Data
{
    public class CategoryData : EntityBaseData<Model.Category>

    {
        public CategoryData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}

