using Microsoft.AspNetCore.Mvc;
using Passports.Repositories;

namespace Passports.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PassportController : ControllerBase {
        private LocalRepository _localRepository;
        private PostgresRepository _postgresRepository;
        public PassportController(LocalRepository localRepository, PostgresRepository postgresRepository) {
            _localRepository = localRepository;
            _postgresRepository = postgresRepository;
        }

        [HttpGet("FindInChunk")]
        public IActionResult FindInChunk(int series, int number) {
            //_localRepository.DecompressFile();
            var matches=_localRepository.FindInChunks(series, number);
            return Ok(matches + " Founded");
        }

        [HttpGet("WriteToPostgres")]
        public IActionResult WriteToPostgres() {
            var passports = _localRepository.ReadAll();
            _postgresRepository.WriteAll(passports);
            return Ok();
        }

        [HttpGet("ReadFromPostgres")]
        public IActionResult ReadFromPostgres() {
            var passports = _postgresRepository.ReadAll();
            return Ok(passports.Count);
        }
    }
}
