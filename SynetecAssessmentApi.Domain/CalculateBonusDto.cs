﻿using System.ComponentModel.DataAnnotations;

namespace SynetecAssessmentApi.Dtos
{
    public class CalculateBonusDto
    {
        public int TotalBonusPoolAmount { get; set; }
        [Required]
        [Range(1,double.PositiveInfinity)]
        public int SelectedEmployeeId { get; set; }
    }
}