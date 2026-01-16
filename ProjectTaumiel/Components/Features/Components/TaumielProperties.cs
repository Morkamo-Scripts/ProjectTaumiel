using PlayerRoles;
using ProjectTaumiel.Components.Features.Components.Interfaces;

namespace ProjectTaumiel.Components.Features.Components;

public class TaumielProperties(ProjectTaumielProperties projectTaumielProperties) : IPropertyModule
{
    public ProjectTaumielProperties ProjectTaumielProperties { get; } = projectTaumielProperties;

    public bool PrefixEnabled { get; set; } = true;
}