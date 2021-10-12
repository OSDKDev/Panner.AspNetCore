namespace Panner.AspNetCore.Tests
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class ParticlesBinderProviderWorks
    {
        public class TestObj { }

        private readonly ParticlesBinderProvider ParticlesBinderProvider;

        public ParticlesBinderProviderWorks()
        {
            this.ParticlesBinderProvider = new ParticlesBinderProvider();
        }

        private ModelBinderProviderContext GetContext(Type modelType)
        {
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            ModelBinderProviderContext context = Substitute.For<ModelBinderProviderContext>();
            context.Metadata.Returns(provider.GetMetadataForType(modelType));
            return context;
        }

        [InlineData(typeof(IReadOnlyCollection<IFilterParticle<TestObj>>), typeof(FilterParticlesModelBinder<TestObj>))]
        [InlineData(typeof(IReadOnlyCollection<ISortParticle<TestObj>>), typeof(SortParticlesModelBinder<TestObj>))]
        [Theory]
        public void WorksForCorrectTypes(Type input, Type output)
        {
            Assert.NotNull(this.ParticlesBinderProvider.GetBinder(this.GetContext(input)));
#warning Missing assert to verify that we're getting the correct model binder.
        }

        [InlineData(typeof(List<IFilterParticle<TestObj>>))]
        [InlineData(typeof(IFilterParticle<TestObj>[]))]
        [InlineData(typeof(List<ISortParticle<TestObj>>))]
        [InlineData(typeof(ISortParticle<TestObj>[]))]
        [InlineData(typeof(IReadOnlyCollection<TestObj>))]
        [InlineData(typeof(TestObj))]
        [InlineData(typeof(int))]
        [Theory]
        public void ReturnsNullForIncorrectTypes(Type input)
        {
            Assert.Null(this.ParticlesBinderProvider.GetBinder(this.GetContext(input)));
        }

        [Fact]
        public void ThrowsOnNullContext()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.ParticlesBinderProvider.GetBinder(null);
            });
        }
    }
}
