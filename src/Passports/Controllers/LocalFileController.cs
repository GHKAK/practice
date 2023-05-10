using Microsoft.AspNetCore.Mvc;
using Passports.Repositories;

namespace Passports.Controllers {
        [ApiController]
    [Route("[controller]")]
    public class LocalFileController : ControllerBase {
        private LocalRepository _localRepository;

        public LocalFileController(LocalRepository localRepository) {
            _localRepository = localRepository;
        }
        [HttpGet(Name = "ReadLocalFile")]
        public IActionResult ReadLocalFile() {
            _localRepository.ReadFile();
            return Ok();
        }
    }
}
