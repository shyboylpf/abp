﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Acme.SimpleTaskApp.Tasks.Dto;
using Microsoft.EntityFrameworkCore;

namespace Acme.SimpleTaskApp.Dtos
{
    public class TaskAppService : SimpleTaskAppAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Tasks.Task> _taskRepository;

        public TaskAppService(IRepository<Tasks.Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                .GetAll()
                .Include(t => t.AssignedPerson)
                .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                .OrderByDescending(t => t.CreationTime)
                .ToListAsync();

            return new ListResultDto<TaskListDto>(
                ObjectMapper.Map<List<TaskListDto>>(tasks)
            );
        }

        public async Task Create(CreateTaskInput input)
        {
            var task = ObjectMapper.Map<Tasks.Task>(input);
            await _taskRepository.InsertAsync(task);
        }
    }
}