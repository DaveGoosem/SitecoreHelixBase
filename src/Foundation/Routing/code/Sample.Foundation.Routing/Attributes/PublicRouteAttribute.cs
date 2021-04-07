using System;

namespace Sample.Foundation.Routing.Attributes
{
    //Attribute can be applicable for Classes and Methods
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class PublicRouteAttribute : Attribute
    {
    }
}