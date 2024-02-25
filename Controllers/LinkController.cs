﻿using Microsoft.AspNetCore.Mvc;
using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces;
using securityApp.Repositories;


namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : Controller
    {
        private VirusTotalSettings _totalSettings;
        private readonly ILinkRepository _linkRepository;
        private readonly Encoder _encoder;


        public LinkController(VirusTotalSettings virusTotalSettings, ILinkRepository linkRepository, Encoder encoder)
        {
            _totalSettings = virusTotalSettings;
            _linkRepository = linkRepository;
            _encoder = encoder;
        }


        [HttpPost]
        public async Task<IActionResult> SendLink(string link)
        {
            var result = await _linkRepository.PostUrlScanAsync(link);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetLinkResult(string link)
        {
            var encodedUrl = _encoder.EncodeUrl(link);
            var response = await _linkRepository.GetUrlScanResultAsync(encodedUrl);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response.Content);
        }
    }
}
