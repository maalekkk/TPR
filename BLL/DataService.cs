using System;
using DL;
using DL.Interfaces;

namespace BLL
{
    public class DataService
    {
        private IDataLayerAPI dataLayer;

        public DataService(IDataLayerAPI dataLayer)
        {
            this.dataLayer = dataLayer;
        }
    }
}
