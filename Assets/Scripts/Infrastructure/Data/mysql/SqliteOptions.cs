using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Data.mysql
{
    
        public class SqliteOptions
        {
            public string DatabasePath { get; }

            public SqliteOptions(string dbPath)
            {
                DatabasePath = dbPath;
            }
        }
    }

