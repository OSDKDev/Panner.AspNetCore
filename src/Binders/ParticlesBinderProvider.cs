namespace Panner.AspNetCore
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
    using System;
    using System.Collections.Generic;

    public class ParticlesBinderProvider : IModelBinderProvider
    {
        private static readonly Dictionary<Type, Type> _binderTypes = new Dictionary<Type, Type>()
        {
            {typeof(IFilterParticle<>), typeof(FilterParticlesModelBinder<>)},
            {typeof(ISortParticle<>), typeof(SortParticlesModelBinder<>)}
        };

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var type = context.Metadata.ModelType;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>))
            {
                var genericArgument = type.GetGenericArguments()[0];

                if (_binderTypes.TryGetValue(genericArgument.GetGenericTypeDefinition(), out var genericBinderType))
                {
                    Type[] typeArgs = { genericArgument.GetGenericArguments()[0] };
                    Type binderType = genericBinderType.MakeGenericType(typeArgs);
                    return new BinderTypeModelBinder(binderType);
                }
            }

            return null;
        }
    }
}
