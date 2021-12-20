﻿using MentorBot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MentorBot.Controllers
{
  [ApiController, Route("mentee")]
  public class MenteeController : ControllerBase
  {

    private readonly ITicketContext _store;

    public MenteeController(ITicketContext store)
    {
      _store = store;
    }

    [HttpPost, Throttle(6, Name = "Ticket Create")]
    public async ValueTask<ActionResult<TicketDto>> Create([FromQuery] TicketCreate createArgs)
    {
      var ticket = await _store.CreateTicketAsync(createArgs, HttpContext.RequestAborted);
      if (ticket == null)
      {
        return NotFound();
      }

      return ticket.ToDto();
    }

    [HttpGet("{ticketId}"), Throttle(3, Name = "Ticket Get")]
    public async ValueTask<ActionResult<TicketDto>> Get(ulong ticketId)
    {
      var ticket = await _store.GetTicketAsync(ticketId, HttpContext.RequestAborted);
      if (ticket == null)
      {
        return NotFound();
      }

      return ticket.ToDto();
    }
  }
}
