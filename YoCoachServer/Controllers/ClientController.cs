﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YoCoachServer.Models.Repositories;

namespace YoCoachServer.Controllers
{
    [Authorize]
    public class ClientController : BaseApiController
    {
        public IHttpActionResult ListCoaches()
        {
            try
            {
                if (CurrentUser.Id != null && CurrentUser.Type.Equals("CL"))
                {
                    var coaches = ClientRepository.ListCoaches(CurrentUser.Id);
                    return Ok(coaches);
                }
                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}