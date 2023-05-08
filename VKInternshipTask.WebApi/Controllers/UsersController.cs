﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VKInternshipTask.Application.Features.Users.Commands.CreateUser;
using VKInternshipTask.Application.Features.Users.Commands.DeleteUser;
using VKInternshipTask.Application.Features.Users.Queries.GetUserInfo;
using VKInternshipTask.Application.Features.Users.Queries.GetUsers;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.WebApi.Dto;

namespace VKInternshipTask.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private IMapper _mapper;

        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UsersListViewModel>> GetAll([FromQuery] GetUsersDto getUsersDto)
        {
            GetUsersQuery query = _mapper.Map<GetUsersQuery>(getUsersDto);
            UsersListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> Get(int id)
        {
            GetUserInfoQuery query = new GetUserInfoQuery()
            {
                UserId = id
            };
            UserViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Create([FromBody] CreateUserDto createUserDto)
        {
            CreateUserCommand command = _mapper.Map<CreateUserCommand>(createUserDto);
            UserViewModel user = await Mediator.Send(command);
            return Created(nameof(UsersController), user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteUserCommand command = new DeleteUserCommand()
            {
                UserId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}