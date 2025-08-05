using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Application.Tags.Dtos;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.Tags.Commands.CreateTag;
public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateTagCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TagDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var normalizedName = request.Name.Trim();

        // Check if tag exists
        var existing = await _context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Name.ToLower() == normalizedName.ToLower(), cancellationToken);

        if (existing != null)
        {
            return _mapper.Map<TagDto>(existing); // ← return existing instead of creating
        }

        var tag = new Tag { Name = normalizedName };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TagDto>(tag);
    }
}
