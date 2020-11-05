using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Core.Helper.Attributes
{
    /// <summary>
    /// Base methods for work with .NET attributes
    /// </summary>
    public static class BaseMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="BrowsableObject"></param>
        /// <returns></returns>
        public static T GetOne<T>(object BrowsableObject) where T : Attribute
        {
            if (BrowsableObject != null)
            {
                object[] attributes = BrowsableObject.GetType().GetCustomAttributes(typeof(T), false);
                if (attributes.Count() > 0 && attributes[0].GetType() == typeof(T))
                {
                    T attribute = (T)attributes.First();
                    return attribute;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="BrowsableType"></param>
        /// <returns></returns>
        public static T GetOne<T>(Type BrowsableType) where T : Attribute
        {
            if (BrowsableType != null)
            {
                object[] attributes = BrowsableType.GetCustomAttributes(typeof(T), false);
                if (attributes.Count() > 0 && attributes[0].GetType() == typeof(T))
                {
                    T attribute = (T)attributes.First();
                    return attribute;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="Method"></param>
        /// <returns></returns>
        public static T GetOne<T>(MethodInfo Method) where T : Attribute
        {
            if (Method != null)
            {
                object[] attributes = Method.GetCustomAttributes(typeof(T), false);
                if (attributes.Count() > 0 && attributes[0].GetType() == typeof(T))
                {
                    T attribute = (T)attributes.First();
                    return attribute;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="Property"></param>
        /// <returns></returns>
        public static T GetOne<T>(PropertyInfo Property) where T : Attribute
        {
            if (Property != null)
            {
                object[] attributes = Property.GetCustomAttributes(typeof(T), false);
                if (attributes.Count() > 0)
                    return (T)attributes[0];
            }
            return null;
        }

        /// <summary>
        /// To get a direct link to a property attribute
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="Property"></param>
        /// <returns></returns>
        public static T GetOne<T>(PropertyDescriptor Property) where T : Attribute
        {
            if (Property != null)
                return (T)Property.Attributes[typeof(T)];
            return null;
        }

        /// <summary>
        /// To get a direct link to a property attribute
        /// </summary>
        /// <typeparam name="AttributeT">Attribute type</typeparam>
        /// <typeparam name="T">Type of object, which contains the property</typeparam>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public static AttributeT GetOne<AttributeT, T>(string PropertyName) where AttributeT : Attribute
        {
            if (PropertyName != null)
            {
                var Descriptor = TypeDescriptor.GetProperties(typeof(T))[PropertyName];
                return GetOne<AttributeT>(Descriptor);
            }
            return null;
        }

        /// <summary>
        /// To get a direct link to a property attribute
        /// </summary>
        /// <typeparam name="AttributeT">Attribute type</typeparam>
        /// <typeparam name="T">Type of object, which contains the property</typeparam>
        /// <param name="Property"></param>
        /// <returns></returns>
        public static AttributeT GetOne<AttributeT, T>(PropertyInfo Property) where AttributeT : Attribute
        {
            if (Property != null)
                return GetOne<AttributeT, T>(Property.Name);
            return null;
        }

        public static bool Existed<T>(PropertyInfo Property) where T : Attribute
        {
            return GetOne<T>(Property) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="Field"></param>
        /// <returns></returns>
        public static T GetOne<T>(FieldInfo Field) where T : Attribute
        {
            if (Field != null)
            {
                object[] attributes = Field.GetCustomAttributes(typeof(T), false);
                if (attributes.Count() > 0 && attributes[0].GetType() == typeof(T))
                {
                    T attribute = (T)attributes.First();
                    return attribute;
                }
            }
            return null;
        }

        public static bool Existed<T>(FieldInfo Field) where T : Attribute
        {
            return GetOne<T>(Field) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="AttributT">Attribute type</typeparam>
        /// <param name="EnumMember"></param>
        /// <returns></returns>
        public static AttributT GetOneFromEnumMember<AttributT>(object EnumMember) where AttributT : Attribute
        {
            if (EnumMember != null)
            {
                var et = EnumMember.GetType();
                MemberInfo[] MemberInfo;
                try
                {
                    MemberInfo = et.GetMember(Enum.GetName(et, EnumMember));
                }
                catch (ArgumentException)
                {
                    return null;
                }
                var attributes = MemberInfo.First().GetCustomAttributes(typeof(AttributT), false);
                if (attributes.Count() > 0)
                    return (AttributT)attributes.First();
            }
            return null;
        }

        public static AttributT GetOneFromEnumMember<AttributT>(FieldInfo EnumMember) where AttributT : Attribute
        {
            if (EnumMember != null)
            {
                var attributes = EnumMember.GetCustomAttributes(typeof(AttributT), false);
                if (attributes.Count() > 0)
                    return (AttributT)attributes.First();
            }
            return null;
        }
    }
}
