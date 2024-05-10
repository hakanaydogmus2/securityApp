﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using securityApp.Data;
using securityApp.Helper;
using securityApp.Interfaces.IHybridAnalysisRepository;
using securityApp.Interfaces.VirusTotalInterfaces;
using securityApp.Models;
using System.Data;


namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private const string folderName = "FilesToUpload";
        private readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        private readonly IHybridFileRepository _hybridFileRepository;
        private readonly IVirusTotalFileRepository _virusTotalFileRepository;
        private readonly Encoder _encoder;
        private readonly VirusTotalSettings _virusTotalSettings;
        private readonly DataContext _context;

        public FileController(IVirusTotalFileRepository fileRepository, Encoder encoder, VirusTotalSettings virusTotalSettings, IHybridFileRepository hybridFileRepository, DataContext dataContext)
        {
            _encoder = encoder;
            _virusTotalFileRepository = fileRepository;
            _hybridFileRepository = hybridFileRepository;
            _virusTotalSettings = virusTotalSettings;
            _context = dataContext;
        }
        [HttpPost]
        [Route("VT-UploadFile")]
        public async Task<IActionResult> VtUploadFile(IFormFile file)
        {
            Console.WriteLine(_encoder.EncodeFileToSHA265(file));
            var result = await _virusTotalFileRepository.UploadFile(file);
            Console.WriteLine(result.Content);
            return Ok(result.Content);
        }

        [HttpGet]
        [Route("VT-GetFileResults")]
        public async Task<IActionResult> VTGetResultsOfFile(string encodedFileSha256)
        {

            var response = await _virusTotalFileRepository.GetFileResult(encodedFileSha256);
            Console.WriteLine(response.Content);

            return Ok(response.Content);
        }

        [HttpPost]
        [Route("Ha-UploadFile")]

        public async Task<IActionResult> PostHybridFile(IFormFile file)
        {
            var response = await _hybridFileRepository.SendFile(file);

            return Ok(response.Content);
        }

        [HttpGet]
        [Route("Ha-GetFileResult")]

        public async Task<IActionResult> GetHybridResultFile(string encodedFileSha)
        {
            var response = await _hybridFileRepository.GetFileReport(encodedFileSha);
            return Ok(response.Content);
        }
        [HttpPost]
        [Route("PostFileResult")]

        public IActionResult PostFileResult(Scan scan)
        {
            _context.Add(scan);
            var saved = _context.SaveChanges();
            return saved > 0 ? Ok() : BadRequest();     
        }

        [HttpGet]
        [Route("GetFileResultCount")]
        public IActionResult GetFileResultCount()
        {
            var count = _context.Scans.Count();
            return Ok(count);
        }
    }
}
