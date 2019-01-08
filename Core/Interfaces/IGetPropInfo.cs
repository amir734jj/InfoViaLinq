using System.Collections.Generic;
using System.Reflection;

namespace InfoViaLinq.Interfaces
{
    /// <summary>
    /// Returns the info
    /// </summary>
    public interface IGetPropInfo<in T>
    {
        /// <summary>
        /// Returns all member PropertyInfos
        /// </summary>
        /// <returns></returns>
        IEnumerable<PropertyInfo> Members();

        /// <summary>
        /// Returns all members mapped with actual PropertyInfos. Key being the parent level PropertyInfo and
        /// Value being the child level PropertyInfo
        /// Note: The idea is if a class implements an interface and the interface has a property x then
        /// the Expression will use x from interface not the class. This method will try to find the
        /// x from class and create a map from x of interface to x of class
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> MappedMembers();
    }
}