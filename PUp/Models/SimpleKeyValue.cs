using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models 
{


    /// <summary>
    ///  Simple Key Value Class to use as object for list instead of Dictionnary 
    ///    this is a simplification for UI that have hard time iterating over lists like
    ///    { {a:b},{a,c } } 
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SimpleKeyValue<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }

    }
}