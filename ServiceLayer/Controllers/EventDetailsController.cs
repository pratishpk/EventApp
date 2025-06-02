using EventManagement.Model;
using EventManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace ServiceLayer.Controllers
{
    [Route("api/EventDetails")]
    [ApiController]
    public class EventDetailsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventDetailsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: api/EventDetails allowed for both Admin and Participant
        [HttpGet]
        [Authorize(Roles = "Admin,Participant")]

        public IActionResult GetAll()
        {
            var events = _eventRepository.GetAll();
            return Ok(events);
        }

        // GET: api/EventDetails by ID allowed for both Admin and Participant
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Participant")]

        public IActionResult GetById(int id)
        {
            var evt = _eventRepository.Get(id);
            if (evt == null) return NotFound();
            return Ok(evt);
        }

        // Create Event - Allowed only for Admin
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(EventDetails eventDetails)
        {
            _eventRepository.Add(eventDetails);
            _eventRepository.Save();
            return CreatedAtAction(nameof(GetById), new { id = eventDetails.EventId }, eventDetails);
        }

        // Update Event - Allowed only for Admin
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Update(int id, EventDetails eventDetails)
        {
            if (id != eventDetails.EventId) return BadRequest();
            _eventRepository.Update(eventDetails);
            _eventRepository.Save();
            return NoContent();
            
        }

        // Delete Event - Allowed only for Admin
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            var evt = _eventRepository.Get(id);
            if (evt == null)
                return NotFound();

            _eventRepository.Delete(id);
            _eventRepository.Save();

            return NoContent();
        }

    }
}
