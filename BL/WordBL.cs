﻿using DL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class WordBL: IWordBL
    {
        IWordDL wordDL;

        public WordBL(IWordDL wordDL)
        {
            this.wordDL = wordDL;
        }

        public async Task<List<TblDifficultyLevel>> GetAllLevels(int problemsTypeId, int speechTherapistId)
        {
            return await wordDL.GetAllLevels(problemsTypeId,speechTherapistId);
        }

        public async Task<List<TblPronunciationProblemsType>> GetAllPronunciationProblemsTypes()
        {
            return await wordDL.GetAllPronunciationProblemsTypes();
        }

   

        public async Task<List<TblWord>> GetAllWords(int levelId)
        {
            return await wordDL.GetAllWords(levelId);
        }

        public async Task<TblWordsGivenToPractice> GetWordToPractice(int wordGivenToPracticeId)
        {
            return await wordDL.GetWordToPractice(wordGivenToPracticeId);
        }

        public async Task<TblDifficultyLevel> PostLevel(TblDifficultyLevel difficultyLevel)
        {
           return await wordDL.PostLevel(difficultyLevel);
        }

        public async Task PostWord(TblWord word)
        {
            await wordDL.PostWord(word);
        }

        public async Task DeleteLevel(int levelId)
        {
            await wordDL.DeleteLevel(levelId);
        }

        public async Task DeleteWord(int wordId)
        {
            await wordDL.DeleteWord(wordId);
        }

        public async Task<bool> PutLevel(int id, int levelName)
        {
           return await wordDL.PutLevel(id, levelName);
        }

        public async Task PutWord(TblWord tblWord)
        {
            await wordDL.PutWord(tblWord);
        }

        public async Task<string> getLocalRecordPath(int word_id)
        {
            return await wordDL.getLocalRecordPath(word_id);
        }
    }
}
