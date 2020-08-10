using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Acme.SimpleTaskApp.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.SimpleTaskApp.Dtos
{
    public class LookupAppService : SimpleTaskAppAppServiceBase, ILookupAppService
    {
        private readonly IRepository<Person, Guid> _personRepository;

        public LookupAppService(IRepository<Person, Guid> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ListResultDto<ComboboxItemDto>> GetPeopleComboboxItems()
        {
            var people = await _personRepository.GetAllListAsync();
            return new ListResultDto<ComboboxItemDto>(
                people.Select(p => new ComboboxItemDto(p.Id.ToString("D"), p.Name)).ToList()
                );
        }
    }
}