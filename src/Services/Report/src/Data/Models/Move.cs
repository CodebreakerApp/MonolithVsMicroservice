﻿using CodeBreaker.Services.Report.Data.Models.Fields;
using CodeBreaker.Services.Report.Data.Models.KeyPegs;

namespace CodeBreaker.Services.Report.Data.Models;

public class Move
{
    public int Id { get; set; }

    public required IReadOnlyList<Field> Fields { get; init; }

    public IReadOnlyList<KeyPeg>? KeyPegs { get; set; }

    internal int Position { get; set; }
}