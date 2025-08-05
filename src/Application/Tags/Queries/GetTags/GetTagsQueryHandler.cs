using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.Tags.Dtos;
using AutoMapper.QueryableExtensions;

namespace Todo_App.Application.Tags.Queries.GetTags;
public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<TagDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .AsNoTracking()
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}