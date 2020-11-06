using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Interfaces
{
    public interface IDataFiller
    {
        void Fill(LibraryContext libraryContext, String path);
    }
}
