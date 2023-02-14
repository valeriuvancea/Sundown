using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Services.Interfaces;
using MissionReportingTool.Services;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Contracts.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MissionReportingTool.Authorization;

namespace MissionReportingTool.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class BaseCrudController<T, U, V> : Controller where T : BaseContract where U : BaseCreationRequest where V : IService<T,U>
    {
        protected readonly V Service;

        protected BaseCrudController(V service)
        {
            this.Service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(U request)
        {
            return Json(new IdResponse(await Service.Create(request)));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(long id)
        {
            return Json(await Service.GetById(id));
        }

        [HttpPost("_page")]
        public virtual async Task<IActionResult> GetByPaginationRequest(PaginationRequest request)
        {
            return Json(await Service.GetByPaginationRequest(request));
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(long id, T contract)
        {
            if (id != contract.Id)
            {
                throw new IdDoesNotMatchException();
            }
            return Json(new IdResponse(await Service.Update(contract)));
        }

        [HttpDelete("{id}")]
        public virtual async Task Delete(long id)
        {
            await Service.Delete(id);
        }
    }
}
