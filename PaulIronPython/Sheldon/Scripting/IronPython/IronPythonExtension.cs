using System;
using System.Windows;
using System.Windows.Markup;
using IronPython.Hosting;
using System.ComponentModel;
using System.Reflection;

namespace Sheldon.Scripting.IronPython
{
    public class IronPythonExtension : MarkupExtension
    {
        private static readonly PythonEngine _engine;

        static IronPythonExtension()
        {
            _engine = new PythonEngine();
            _engine.Import("clr");
            AddReference("System");
            ImportNamespace("System");
        }

        public static void AddGlobal(string name, object o)
        {
            _engine.Globals[name] = o;
        }

        public static void AddReference(string partialName)
        {
            _engine.Execute(string.Format("clr.AddReferenceByPartialName(\"{0}\")", partialName));
        }

        public static void AddReference(Assembly assembly)
        {
            _engine.Execute(string.Format("clr.AddReferenceByName(\"{0}\")", assembly.FullName));
        }

        private static void ImportNamespace(string namespaceToImport)
        {
            _engine.Execute(string.Format("from {0} import *;", namespaceToImport));
        }

        public string Eval { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                var provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
                if (provideValueTarget != null)
                {
                    var targetObject = provideValueTarget.TargetObject as DependencyObject;
                    if (targetObject != null)
                    {
                        var targetType = provideValueTarget.TargetProperty is DependencyProperty 
                                             ? ((DependencyProperty)provideValueTarget.TargetProperty).PropertyType 
                                             : ((PropertyInfo)provideValueTarget.TargetProperty).PropertyType;

                        var result = _engine.Evaluate(Eval);
                        if (result == null) return null;

                        var resultType = result.GetType();
                        var converter = TypeDescriptor.GetConverter(resultType);
                        return converter.CanConvertTo(targetType) ? converter.ConvertTo(result, targetType) : result;
                    }
                }
            }
            return null;
        }
    }
}