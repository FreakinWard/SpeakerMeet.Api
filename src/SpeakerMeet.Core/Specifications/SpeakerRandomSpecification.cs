﻿using System;
using Ardalis.Specification;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Specifications
{
    public class SpeakerRandomSpecification : Specification<Speaker>
    {
        public SpeakerRandomSpecification()
        {
            Take = 4;
            Query
                .Where(x => x.IsActive)
                .OrderBy(x => Guid.NewGuid());
        }

        public new int Take { get; internal set; }
    }
}