﻿namespace Panner.AspNetCore
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FilterParticlesModelBinder<TEntity> : IModelBinder
        where TEntity : class
    {
        private readonly IPContext PContext;

        public FilterParticlesModelBinder(IPContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.PContext = context;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult =
                bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Success(Array.Empty<IFilterParticle<TEntity>>());
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var csvInput = valueProviderResult.FirstValue;

            if (!PContext.TryParseCsv(
                input: csvInput,
                out IEnumerable<IFilterParticle<TEntity>> particles
            ))
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Could not parse provided filters.");
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(particles);
            return Task.CompletedTask;
        }
    }
}
