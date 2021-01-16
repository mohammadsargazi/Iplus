using System;
using System.Collections.Generic;
using System.Text;

namespace Bipap.DAL
{
    public interface ICustomDbContext
    {
        IEnumerable<object> GetTable(Type type, bool cast);
        List<KeyValuePair<object, string>> GetTable(Type type);
    }
}
