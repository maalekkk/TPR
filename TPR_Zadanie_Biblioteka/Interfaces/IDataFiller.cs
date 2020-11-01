using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    interface IDataFiller
    {
        LibraryContext Fill(String path);
    }
}
