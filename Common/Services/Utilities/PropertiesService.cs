using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Services
{
    public class PropertiesService
    {
        /// <summary>
        /// Gets a list of properties from an object
        /// </summary>
        /// <param name="anObject">The object to get the properties list</param>
        /// <returns>A properties list</returns>
        public static List<Property> GetDynamicProperties(object anObject)
        {
            List<Property> propertyList = new List<Property>();
            if (anObject != null)
                foreach(var property in anObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    propertyList.Add(new Property { 
                        Name = property.Name,
                        Type = property.PropertyType,
                        CanWrite = property.CanWrite,
                    });
            return propertyList;
        }
        /// <summary>
        /// Gets a list of properties from an object
        /// </summary>
        /// <param name="dynamicObject">The object to get the properties list</param>
        /// <returns>A properties list</returns>
        public static List<Property> GetDynamicProperties(Type objectType)
        {
            List<Property> propertyList = new List<Property>();
            if (objectType != null)
                foreach (var property in objectType.GetProperties())
                    propertyList.Add(new Property
                    {
                        Name = property.Name,
                        Type = property.PropertyType
                    });
            return propertyList;
        }

        /// <summary>
        /// Gets a list of properties and its values from an object
        /// </summary>
        /// <param name="dynamicObject">The object to get the property/value list</param>
        /// <returns>A property/value list</returns>
        public static List<PropertyValue> GetDynamicPropertyValues(dynamic dynamicObject)
        {
            List<PropertyValue> propertyValueList = new List<PropertyValue>();
            if (dynamicObject != null)
                foreach (var property in dynamicObject.GetType().GetProperties())
                {
                    var propName = (string)property.Name;
                    var propVal = dynamicObject.GetType().GetProperty(propName).GetValue(dynamicObject, null);
                    propertyValueList.Add(new PropertyValue
                    {
                        Name = propName,
                        Type = property.PropertyType,
                        Value = propVal
                    });
                }
            return propertyValueList;
        }
        public static List<PropertyValue> GetDynamicPropertyValues(Type objectType)
        {
            List<PropertyValue> propertyValueList = new List<PropertyValue>();
            if (objectType != null)
                foreach (var property in objectType.GetProperties())
                {
                    var propName = property.Name;
                    var propVal = objectType.GetProperty(propName).GetValue(objectType, null);
                    propertyValueList.Add(new PropertyValue
                    {
                        Name = propName,
                        Type = property.PropertyType,
                        Value = propVal
                    });
                }
            return propertyValueList;
        }
        public static void SetPropertyValue(object anObject, string propertyName, object value)
        {
            PropertyInfo prop = anObject.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
                prop.SetValue(anObject, value);
        }
    }
}
