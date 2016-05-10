using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RClient
{
    public interface IDataReader<T>
    {
        bool Next();

        T GetValue();

        void Reset();
    }
}
