using System;
using DependencyInjectionSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DependencyInjectionSample.Filters
{

    // More reading @ https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.2#dependency-injection

    // This first example will be using [ServiceFilter] -- No constructor args allowed other than DI ones

    public class AddGuidHeaderAttribute : ResultFilterAttribute
    {
        private const string _name = "X-Guid4U";
        private readonly GuidGenerator _guidGenerator;
        private readonly Guid _instantGuid;

        public AddGuidHeaderAttribute(GuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
            _instantGuid = Guid.NewGuid();
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, new[] { _guidGenerator.Generate(), _instantGuid.ToString() });
            base.OnResultExecuting(context);
        }
    }

    // This second example will be using [TypeFilter] -- Constructor args are allowed other than DI ones

    //public class AddGuidHeaderAttribute : ResultFilterAttribute
    //{
    //    private readonly string _name;
    //    private readonly GuidGenerator _guidGenerator;
    //    private readonly Guid _instantGuid;

    //    public AddGuidHeaderAttribute(string name, GuidGenerator guidGenerator)
    //    {
    //        _name = name;
    //        _guidGenerator = guidGenerator;
    //        _instantGuid = Guid.NewGuid();
    //    }

    //    public override void OnResultExecuting(ResultExecutingContext context)
    //    {
    //        context.HttpContext.Response.Headers.Add(_name, new[] { _guidGenerator.Generate(), _instantGuid.ToString() });
    //        base.OnResultExecuting(context);
    //    }
    //}

    // This third example will be using [TypeFilter] -- Constructor args are not allowed other than DI ones, this does produce a
    // cleaner look in the code.

    //public class AddGuidHeaderAttribute : TypeFilterAttribute
    //{
    //    // This is a receipe that can be used when you don't need to pass any parameters other than injected
    //    // parameters to the Attribute.  It's a variation of the above example.

    //    public AddGuidHeaderAttribute():base(typeof(AddGuidHeaderAttributeImpl))
    //    {

    //    }

    //    private class AddGuidHeaderAttributeImpl : ResultFilterAttribute
    //    {
    //        private readonly string _name;
    //        private readonly GuidGenerator _guidGenerator;
    //        private readonly Guid _instantGuid;

    //        public AddGuidHeaderAttributeImpl(GuidGenerator guidGenerator)
    //        {
    //            _name = "X-YouGetAGuid"
    //            _guidGenerator = guidGenerator;
    //            _instantGuid = Guid.NewGuid();
    //        }

    //        public override void OnResultExecuting(ResultExecutingContext context)
    //        {
    //            context.HttpContext.Response.Headers.Add(_name, new[] { _guidGenerator.Generate(), _instantGuid.ToString() });
    //            base.OnResultExecuting(context);
    //        }
    //    }
    //}
}
