﻿using Common.Models;
using System;

namespace Common.Lookups
{
    /// <summary>
    /// Represents the A00 Customer remark section
    /// </summary>
    public static class FareValueFieldDefinition
    {
        public static FieldDefinition A07SEC =>
            new()
            {
                Name = "A07SEC",
                SegmentPosition = 0,
                Length = 3,
            };
        public static FieldDefinition A07FSI =>
            new()
            {
                Name = "A07FSI",
                SegmentPosition = 3,
                Length = 2,
            };
        public static FieldDefinition A07CRB =>
            new()
            {
                Name = "A07CRB",
                SegmentPosition = 5,
                Length = 3,
            };
        public static FieldDefinition A07TBF =>
            new()
            {
                Name = "A07TBF",
                SegmentPosition = 8,
                Length = 12,
            };
        public static FieldDefinition A07CRT =>
            new()
            {
                Name = "A07CRT",
                SegmentPosition = 20,
                Length = 3,
            };
        public static FieldDefinition A07TTA =>
            new()
            {
                Name = "A07TTA",
                SegmentPosition = 23,
                Length = 12,
            };
        public static FieldDefinition A07CRE =>
            new()
            {
                Name = "A07CRE",
                SegmentPosition = 35,
                Length = 3,
            };
        public static FieldDefinition A07EQV =>
            new()
            {
                Name = "A07EQV",
                SegmentPosition = 38,
                Length = 12,
            };
        public static FieldDefinition A07NRI =>
            new()
            {
                Name = "A07NRI",
                Length = 3,
                IsOptionalField = true,
                CodeId = "NR:",
            };
        public static FieldDefinition A07NRT =>
            new()
            {
                Name = "A07NRT",
                Length = 8,
                IsOptionalField = true,
            };
        public static FieldDefinition A07CUR =>
            new()
            {
                Name = "A07CUR",
                Length = 3,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TI1 =>
            new()
            {
                Name = "A07TI1",
                Length = 3,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TT1 =>
            new()
            {
                Name = "A07TT1",
                Length = 8,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TC1 =>
            new()
            {
                Name = "A07TC1",
                Length = 2,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TI2 =>
            new()
            {
                Name = "A07TI2",
                Length = 3,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TT2 =>
            new()
            {
                Name = "A07TT2",
                Length = 8,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TC2 =>
            new()
            {
                Name = "A07TC2",
                Length = 2,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TI3 =>
            new()
            {
                Name = "A07TI3",
                Length = 3,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TT3 =>
            new()
            {
                Name = "A07TT3",
                Length = 8,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TC3 =>
            new()
            {
                Name = "A07TC3",
                Length = 2,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TI4 =>
            new()
            {
                Name = "A07TI4",
                Length = 3,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TT4 =>
            new()
            {
                Name = "A07TT4",
                Length = 8,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TC4 =>
            new()
            {
                Name = "A07TC14",
                Length = 2,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TI5 =>
            new()
            {
                Name = "A07TI5",
                Length = 3,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TT5 =>
            new()
            {
                Name = "A07TT5",
                Length = 8,
                IsOptionalField = true,
            };
        public static FieldDefinition A07TC5 =>
            new()
            {
                Name = "A07TC5",
                Length = 2,
                IsOptionalField = true,
            };
        public static int CarriageReturnNumber => 2;
    }
}
