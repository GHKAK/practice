using Microsoft.AspNetCore.Mvc;
using Passports.Repositories;
using System.Diagnostics;

namespace Passports.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PassportController : ControllerBase {
        private LocalRepository _localRepository;
        private PostgresRepository _postgresRepository;
        readonly Stopwatch sw = new();
        public PassportController(LocalRepository localRepository, PostgresRepository postgresRepository) {
            _localRepository = localRepository;
            _postgresRepository = postgresRepository;
        }

        [HttpGet("FindInChunk")]
        public IActionResult FindInChunk(int series, int number) {
            //_localRepository.DecompressFile();
            sw.Restart();
            var matches =_localRepository.FindInChunks(series, number);
            return Ok($"{matches} Founded   in {sw.Elapsed} seconds");

        }
        [HttpGet("FindInChunksAsync")]
        public async Task<IActionResult> FindInChunksAsync(int series, int number) {
            //_localRepository.DecompressFile();
            sw.Restart();
            var matches = await _localRepository.FindInChunksAsync(series, number);
            sw.Stop();
            return Ok($"{matches} Founded   in {sw.Elapsed} seconds");
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
        [HttpGet("GetLinesCountAsync1")]
        public async Task<IActionResult> GetLinesCountAsync1() {
            sw.Restart();
            int count = await _localRepository.GetLinesCountAsync();
            sw.Stop();
            return Ok($"{count}  {sw.Elapsed} seconds");
        }
        [HttpGet("GetLinesCountAsync2")]
        public async Task<IActionResult> GetLinesCountAsync2() {
            sw.Restart();
            int count = await _localRepository.GetLinesCount2Async();
            sw.Stop();
            return Ok($"{count}  {sw.Elapsed} seconds");
        }
        [HttpGet("GetLinesCountAsync3")]
        public async Task<IActionResult> GetLinesCountAsync3() {
            sw.Restart();
            int count = await _localRepository.GetLinesCount3Async();
            sw.Stop();
            return Ok($"{count}  {sw.Elapsed} seconds");
        }
        [HttpGet("GetLinesCount4")]
        public async Task<IActionResult> GetLinesCount4() {
            sw.Restart();
            int count =  _localRepository.GetLinesCount4();
            sw.Stop();
            return Ok($"{count}  {sw.Elapsed} seconds");
        }
    }
}
