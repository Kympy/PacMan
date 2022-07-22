using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class Manager<T>  where T: class, new()
    {
        private static volatile T _instance = null;
        private static object lockObj = new object();
        public static T Instance
        {
            get
            {
                if( _instance == null )
                {
                    lock(lockObj)
                    {
                        if(_instance == null)
                        {
                            _instance = new T();
                        }
                        return _instance;
                    }
                }
                return _instance;
            }
        }
    }
}
