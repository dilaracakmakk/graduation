using graduation.Data.infrastructure;
using graduation.Data.infrastructure.Entities;
using Microsoft.Extensions.Options;
using System;

namespace graduation.Data
{
    public class RolePageData : EntityBaseData<Model.RolePage>

    {
        public RolePageData(IOptions<DatabaseSettings> dbOptions)
            : base(new DataContext(dbOptions.Value.ConnectionString))
        {

        }
    }
}

