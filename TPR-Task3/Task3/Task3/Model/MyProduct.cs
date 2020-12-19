using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Model
{
    class MyProduct : Product
    {
        public MyProduct(Product product)
        {
            PropertyInfo[] props = product.GetType().GetProperties();
            foreach(PropertyInfo prop in props)
            {
                if (prop.CanWrite)
                {
                    prop.SetValue(this, prop.GetValue(product));
                }
            }
        }
    }
}
