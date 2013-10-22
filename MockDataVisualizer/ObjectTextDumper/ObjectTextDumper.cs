using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MockDataDebugVisualizer.ObjectTextDumper
{
    public class ObjectTextDumper
    {
        private int _level;
        private readonly int _indentSize;
        private readonly StringBuilder _stringBuilder;
        private readonly List<int> _hashListOfFoundElements;

        private ObjectTextDumper(int indentSize)
        {
            _indentSize = indentSize;
            _stringBuilder = new StringBuilder();
            _hashListOfFoundElements = new List<int>();
        }

        public static string Dump(object element)
        {
            return Dump(element, 2);
        }

        public static string Dump(object element, int indentSize)
        {
            var instance = new ObjectTextDumper(indentSize);
            var objectType = element.GetType();
            instance.Write("var {0} = new {1}();", objectType.Name.ToLower(), objectType);
            return instance.DumpElement(element);
        }

        private string DumpElement(object element)
        {
            if (element == null || element is ValueType || element is string)
            {
                Write(FormatValue(element));
            }
            else
            {
                DumpObjectType(element);
            }

            return _stringBuilder.ToString();
        }

        private void DumpObjectType(object element)
        {
            var objectType = element.GetType();
            if (!typeof (IEnumerable).IsAssignableFrom(objectType))
            {
                //Write("{{{0}}}", objectType.FullName);
                _hashListOfFoundElements.Add(element.GetHashCode());
                _level++;
            }

            var enumerableElement = element as IEnumerable;
            if (enumerableElement != null)
            {
                SetIEnumerableIndentation(enumerableElement);
            }
            else
            {
                MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                
                foreach (var memberInfo in members)
                {
                    var fieldInfo = memberInfo as FieldInfo;
                    var propertyInfo = memberInfo as PropertyInfo;

                    if (fieldInfo == null && propertyInfo == null)
                        continue;

                    var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                    object value = fieldInfo != null ? fieldInfo.GetValue(element) : propertyInfo.GetValue(element, null);

                    
                    if (type.IsValueType || type == typeof (string))
                    {
                        //Write("{0}: {1}", memberInfo.Name, FormatValue(value));
                        Write("var {0} = {1}", memberInfo.Name, FormatValue(value));
                    }
                    else
                    {
                        var isEnumerable = typeof (IEnumerable).IsAssignableFrom(type);
                        
                        if (isEnumerable)
                        {
                            var genericArgument = type.GetGenericArguments()[0].Name;
                            
                            var iEnumerableGenericType = string.Format("{0}<{1}>", type.Name.Substring(0, type.Name.Length - 2), genericArgument);

                            Write("var {0} = new {1}();", memberInfo.Name, iEnumerableGenericType);
                        }
                        else
                        {
                            //Write("{0}: {1}", memberInfo.Name, isEnumerable ? "..." : "{ }");
                            Write("var {0} = new {1}({2});", memberInfo.Name, type, FormatValue(value));    
                        }
                        

                        var alreadyTouched = !isEnumerable && AlreadyTouched(value);
                        _level++;
                        if (!alreadyTouched)
                            DumpElement(value);
                        else
                            Write("{{{0}}} <-- bidirectional reference found", value.GetType().FullName);
                        _level--;
                    }
                }
            }

            if (!typeof (IEnumerable).IsAssignableFrom(objectType))
            {
                _level--;
            }
        }

        private void SetIEnumerableIndentation(IEnumerable enumerableElement)
        {
            foreach (object item in enumerableElement)
            {
                if (item is IEnumerable && !(item is string))
                {
                    _level++;
                    DumpElement(item);
                    _level--;
                }
                else
                {
                    if (!AlreadyTouched(item))
                        DumpElement(item);
                    else
                        Write("{{{0}}} <-- bidirectional reference found", item.GetType().FullName);
                }
            }
        }

        private bool AlreadyTouched(object value)
        {
            var hash = value.GetHashCode();
            for (var i = 0; i < _hashListOfFoundElements.Count; i++)
            {
                if (_hashListOfFoundElements[i] == hash)
                    return true;
            }
            return false;
        }

        private void Write(string value, params object[] args)
        {
            var space = new string(' ', _level * _indentSize);

            if (args != null)
                value = string.Format(value, args);

            _stringBuilder.AppendLine(space + value);
        }

        private string FormatValue(object o)
        {
            if (o == null)
                return ("null");

            if (o is DateTime)
                return (((DateTime)o).ToShortDateString());

            if (o is string)
                return string.Format("\"{0}\";", o);

            if (o is ValueType)
                return string.Format("{0};", o);

            if (o is IEnumerable)
                return ("...");

            return ("{ }");
        }
    }
}