using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsIntro.IoC
{
    public class SimpleIocContainer : IContainer
    {
        #region Constructor
        private static object objLock = new object();
        private static SimpleIocContainer ioCResolver;
        public static SimpleIocContainer Instance
        {
            get
            {
                lock (objLock)
                {
                    if (ioCResolver == null)
                    {

                        if (ioCResolver == null)
                            ioCResolver = new SimpleIocContainer();
                    }
                }

                return ioCResolver;
            } 
        }


        #endregion
        private readonly IList<RegisteredObject> registeredObjects = new List<RegisteredObject>();
        public IList<RegisteredObject> RegisteredObjects
        {
            get
            {
                return Instance.registeredObjects;
            }
        }

       

        public void Register<TTypeToResolve, TConcrete>()
        {
            Register<TTypeToResolve, TConcrete>(LifeCycle.Singleton);
        }

        public void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle)
        {
            RegisteredObjects.Add(new RegisteredObject(typeof(TTypeToResolve), typeof(TConcrete), lifeCycle));
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));
        }

        public object Resolve(Type typeToResolve)
        {
            return ResolveObject(typeToResolve);
        }

        private object ResolveObject(Type typeToResolve)
        {
            var registeredObject = RegisteredObjects.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
            if (registeredObject == null)
            {
                throw new TypeNotRegisteredException(string.Format(
                    "The type {0} has not been registered", typeToResolve.Name));
            }
            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.Instance == null ||
                registeredObject.LifeCycle == LifeCycle.Transient)
            {
                IEnumerable<object> parameters = ResolveConstructorParameters(registeredObject);
                object[] paramArray = parameters.ToArray();
                registeredObject.CreateInstance(paramArray);
            }
            return registeredObject.Instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return ResolveObject(parameter.ParameterType);
            }
        }
    }
}
