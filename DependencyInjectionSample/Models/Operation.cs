using System;
using DependencyInjectionSample.Interfaces;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionSample.Models
{
    public class Operation : IOperationTransient, 
        IOperationScoped, 
        IOperationSingleton, 
        IOperationSingletonInstance
    {
        public Operation(ILogger<Operation> logger) : this(logger, Guid.NewGuid())
        {

        }

        public Operation(ILogger<Operation> logger, Guid id)
        {
            logger.LogInformation("Being constructed with Operation ID {OperationId}", id);
            OperationId = id;
        }

        public Guid OperationId { get; private set; }
    }
}
