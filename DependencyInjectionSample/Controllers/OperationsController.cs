using DependencyInjectionSample.Filters;
using DependencyInjectionSample.Models;
using DependencyInjectionSample.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationSingletonInstance _singletonInstanceOperation;
        private readonly OperationService _operationService;
        //private readonly OperationService _operationService2;

        public OperationsController(IOperationTransient transientOperation,
                                IOperationScoped scopedOperation,
                                IOperationSingleton singletonOperation,
                                IOperationSingletonInstance singletonInstanceOperation,
                                OperationService operationService/*, OperationService operationService2*/)
        {
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
            _singletonInstanceOperation = singletonInstanceOperation;
            _operationService = operationService;
            //_operationService2 = operationService2;
        }

        // GET api/operations
        [HttpGet]
        //[AddGuidHeader]
        //[TypeFilter(typeof(AddGuidHeaderAttribute), Arguments = new object[] {"X-YetAnotherAwesomeGuid"})]
        //[ServiceFilter(typeof(AddGuidHeaderAttribute))]
        public ActionResult<object> Get()
        {
            return Ok(new
            {
                ControllerTransientOperation = _transientOperation,
                ControllerScopedOperation = _scopedOperation,
                ControllerSingletonOperation = _singletonOperation,
                ControllerSingletonInstanceOperation = _singletonInstanceOperation,
                MasterService = _operationService /*,
                MasterService2 = _operationService2*/
            });
        }
    }
}