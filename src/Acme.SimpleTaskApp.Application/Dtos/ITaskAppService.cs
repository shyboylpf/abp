﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Acme.SimpleTaskApp.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//using Task = Acme.SimpleTaskApp.Tasks.Task;

namespace Acme.SimpleTaskApp.Dtos
{
    public interface ITaskAppService : IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);
    }

    public class GetAllTasksInput
    {
        public TaskState? State { get; set; }
    }

    [AutoMapFrom(typeof(Tasks.Task))]
    public class TaskListDto : EntityDto, IHasCreationTime
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public TaskState State { get; set; }
    }
}