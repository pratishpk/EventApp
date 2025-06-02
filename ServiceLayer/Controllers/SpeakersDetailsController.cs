using EventManagement.Model;
using EventManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace ServiceLayer.Controllers
{
    [Route("api/SpeakersDetails")]
    [ApiController]
    public class SpeakersDetailsController : ControllerBase
    {
        private readonly ISpeakersRepository _repo;

        public SpeakersDetailsController(ISpeakersRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Participant")]

        public IActionResult GetAll()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Participant")]

        public IActionResult GetById(int id)
        {
            var speaker = _repo.Get(id);
            if (speaker == null) return NotFound();
            return Ok(speaker);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult Create(SpeakersDetails speaker)
        {
            _repo.Add(speaker);
            _repo.Save();
            return CreatedAtAction(nameof(GetById), new { id = speaker.SpeakerId }, speaker);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Update(int id, SpeakersDetails speaker)
        {
            if (id != speaker.SpeakerId) return BadRequest();
            _repo.Update(speaker);
            _repo.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return NoContent();
        }
    }
}
