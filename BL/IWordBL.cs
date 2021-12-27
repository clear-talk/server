﻿using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL
{
    public interface IWordBL
    {
        Task<IEnumerable<TblPronunciationProblemsType>> GetAllPronunciationProblemsTypes();
        Task<IEnumerable<TblDifficultyLevel>> GetAllLevels(int problemsTypeId);
        Task PostLevel(int pronunciationProblemId);
    }
}