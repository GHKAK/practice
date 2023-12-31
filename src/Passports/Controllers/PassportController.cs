﻿using Microsoft.AspNetCore.Mvc;
using Passports.Repositories;
using System.Diagnostics;

namespace Passports.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PassportController : ControllerBase {
        private LocalRepository _localRepository;
        private LocalRepositoryNew _localRepositoryNew;

        //private PostgresRepository _postgresRepository;
        readonly Stopwatch sw = new();
        public PassportController(LocalRepository localRepository, LocalRepositoryNew localRepositoryNew) {
            _localRepository = localRepository;
            _localRepositoryNew = localRepositoryNew;
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
        [HttpGet("FindInMemories")]
        public async Task<IActionResult> FindInMemories(int series, int number) {
            //_localRepository.DecompressFile();
            sw.Restart();
            var matches = await _localRepositoryNew.FindInChunksAsync(series, number);
            sw.Stop();
            return Ok($"{matches} Founded   in {sw.Elapsed} seconds");
        }
        [HttpGet("CountFileAsync")]
        public async Task<IActionResult> CountFileAsync() {
            //_localRepository.DecompressFile();
            sw.Restart();
            var count = await _localRepository.CountFileAsync();
            sw.Stop();
            return Ok($"{count} Readed");
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
