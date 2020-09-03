using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Utilities
{
    //public static T DeepClone<T>(this T source)
    //{
    //    if (!typeof(T).IsSerializable)
    //    {
    //        throw new System.ArgumentException("The type must be serializable.", nameof(source));
    //    }

    //    if (UnityEngine.Object.ReferenceEquals(source, null))
    //    {
    //        return default(T);
    //    }

    //    System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
    //    System.IO.Stream stream = new System.IO.MemoryStream();
    //    using (stream)
    //    {
    //        formatter.Serialize(stream, source);
    //        stream.Seek(0, System.IO.SeekOrigin.Begin);
    //        return (T)formatter.Deserialize(stream);
    //    }

    //}


    public static T DeepClone<T>(this T obj)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new System.ArgumentException("The type must be serializable.", nameof(obj));
        }

        if (UnityEngine.Object.ReferenceEquals(obj, null))
        {
            return default(T);
        }
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public static string GetDescription(this Enum en)
    {
        Type type = en.GetType();
        MemberInfo[] memInfo = type.GetMember(en.ToString());
        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }
        return en.ToString();
    }

    public static string ToCommaFormatString(this int amount)
    {
        return string.Format("{0:#,##0}",amount);
    }
    public static string ToDeletedKeyChars(this string keyString)
    {
        return keyString.Replace("$", "").Replace("{", "").Replace("}", "");
    }

    public static string ToCommaFormatStringWithSign(this int amount)
    {
        string sign = string.Empty;
        if (amount > 0) sign += "+";
        if (amount < 0) sign += "";
        return sign+string.Format("{0:#,##0}", amount);
    }

    public static object GetInstanceField<T>(this T instance, string fieldName)
    {
        BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        FieldInfo field = typeof(T).GetField(fieldName, bindFlags);
        return field.GetValue(instance);
    }
    public static object CallPrivateMethod(this object o, string methodName, params object[] args)
    {
        var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (mi != null)
        {
            return mi.Invoke(o, args);
        }
        return null;
    }

    public static void SetInstanceField<T>(this T instance,string fieldName, object value)
    {
        var prop = instance.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic
    |   System.Reflection.BindingFlags.Instance);
        prop.SetValue(instance, value);
    }

    public static GameObject FindWithTagInChild(this GameObject parent, string tag)
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.gameObject;
            }
        }
        return null;
    }

    public static void Destroy(UnityEngine.Object obj, float delay=0f)
    {
        if (Application.isPlaying)
            GameObject.Destroy(obj,delay);
        else
            GameObject.DestroyImmediate(obj);
    }
}
