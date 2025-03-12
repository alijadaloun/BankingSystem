using FinalLabTask1.Entities;
using FinalLabTask1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace FinalLabTask1.Controllers;

public class TransactionLogsODataController:ODataController
{
    private readonly ITransactionLogsService _transactionLogsService;

    public TransactionLogsODataController(ITransactionLogsService transactionLogsService)
    {
        _transactionLogsService = transactionLogsService;
    }

    [EnableQuery]
    public ActionResult<IEnumerable<TransactionLogs>> Get()
    {
        return Ok(_transactionLogsService.Get());
    }
}