﻿using System;
using System.Collections.Generic;

namespace WpfMqttSubApp.Models;

public partial class Process
{
    public int PrcIdx { get; set; }

    public int SchIdx { get; set; }

    /// <summary>
    /// 공정처리 ID(UK)
    /// yyyyMMdd-NewGuid(36)
    /// </summary>
    public string PrcCd { get; set; } = null!;

    public DateOnly PrcDate { get; set; }

    public int PrcLoadTime { get; set; }

    public TimeOnly? PrcStartTime { get; set; }

    public TimeOnly? PrcEndTime { get; set; }

    public string? PrcFacilityId { get; set; }

    public sbyte? PrcResult { get; set; }

    public DateTime? RegDt { get; set; }

    public DateTime? ModDt { get; set; }

    public virtual Schedule SchIdxNavigation { get; set; } = null!;
}
