﻿using System.Threading;
using System.Threading.Tasks;
using Dwapi.Prep.Core.Domain;
using Dwapi.Prep.Core.Interfaces.Repository;
using Dwapi.Prep.SharedKernel.Exceptions;
using MediatR;

namespace Dwapi.Prep.Core.Command
{
    public class ValidateFacility : IRequest<MasterFacility>
    {
        public int SiteCode { get; }

        public ValidateFacility(int siteCode)
        {
            SiteCode = siteCode;
        }
    }
    public class ValidateFacilityHandler: IRequestHandler<ValidateFacility,MasterFacility>
    {
        private readonly IMasterFacilityRepository _repository;

        public ValidateFacilityHandler(IMasterFacilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<MasterFacility> Handle(ValidateFacility request, CancellationToken cancellationToken)
        {
            var masterFacility =await _repository.GetAsync(request.SiteCode);

            if (null==masterFacility)
                throw new FacilityNotFoundException(request.SiteCode);

            return masterFacility;
        }
    }
}
