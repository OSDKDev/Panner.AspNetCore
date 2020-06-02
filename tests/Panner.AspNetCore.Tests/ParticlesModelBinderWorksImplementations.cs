namespace Panner.AspNetCore.Tests
{
    public class TestObj
    { }

    public class SortParticlesModelBinderWorks : ParticlesModelBinderWorks<SortParticlesModelBinder<TestObj>>
    {
        // All tests are in the generic ParticlesModelBinderWorks class.
        // This class only provides construction of the specific model binder.
        protected override SortParticlesModelBinder<TestObj> Create(IPContext context)
        {
            return new SortParticlesModelBinder<TestObj>(context);
        }
    }

    public class FilterParticlesModelBinderWorks : ParticlesModelBinderWorks<FilterParticlesModelBinder<TestObj>>
    {
        // All tests are in the generic ParticlesModelBinderWorks class.
        // This class only provides construction of the specific model binder.
        protected override FilterParticlesModelBinder<TestObj> Create(IPContext context)
        {
            return new FilterParticlesModelBinder<TestObj>(context);
        }
    }
}
