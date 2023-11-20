using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Helpers
{
    class SmartControl
    {
        public static Control LoadSmartControl(string assemblyFile, string typeName)
        {
            Form _userControl;
            Assembly _assembly;
            Type _type;

            _assembly = Assembly.Load(assemblyFile);
            _type = _assembly.GetType(typeName);

            Type[] argumentType = { typeof(object[]) };

            ConstructorInfo constructorSmartControl = _type.GetConstructor(argumentType);

            constructorSmartControl = _type.GetConstructor(Type.EmptyTypes);
            if (constructorSmartControl == null)
                throw new Exception(_type.FullName + " no tiene un constructor publico sin parametros.");

            _userControl = constructorSmartControl.Invoke(null) as Form;
            _userControl.AutoScroll = true;
            return _userControl;

        }
    }
}
