using Cauldron.Interception;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Janus;

namespace Janus.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class EntityAttribute : Attribute, Cauldron.Interception.IPropertyInterceptorInitialize, Cauldron.Interception.IPropertySetterInterceptor
    {
        public static Dictionary<Object, Entity> map = new Dictionary<object, Entity>();
        public bool OnException(Exception e)
        {
            throw e;
        }

        public void OnExit()
        {
            //       throw new NotImplementedException();
        }

        public void OnInitialize(PropertyInterceptionInfo propertyInterceptionInfo, object value)
        {
            if (!map.ContainsKey(propertyInterceptionInfo.Instance))
            {
                map.Add(propertyInterceptionInfo.Instance, Repository.GlobalRepository.CreateEntity(propertyInterceptionInfo.DeclaringType.FullName));
            }
        }

        public bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue)
        {
            var entity = map[propertyInterceptionInfo.Instance];
            if (propertyInterceptionInfo.PropertyType.FullName.StartsWith("System."))
            {
                Repository.GlobalRepository.CreateValue(
                    entity,
                    new RelationShipDescriptor { Name = propertyInterceptionInfo.PropertyName },
                    newValue.ToString(),
                    newValue.GetType().FullName
                   );
            }
            else
            {
                Repository.GlobalRepository.SetRelation(
                    entity,
                     map[newValue],
                     new RelationShipDescriptor { Name = propertyInterceptionInfo.PropertyName }
                   );
            }

            return false;
        }
    }
}
