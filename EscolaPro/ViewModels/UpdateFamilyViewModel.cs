﻿using EscolaPro.Enums;

namespace EscolaPro.ViewModels;

public class UpdateFamilyViewModel : UpdateUserInternalViewModel
{
    public string? WorkAddress { get; set; }
    public string? Ocupation { get; set; }
    public FamilyTypeEnum? Type { get; set; }
}
