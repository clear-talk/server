﻿
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class LessonDL : ILessonDL
    {
        GeneralDBContext generalDBContext;
        public LessonDL(GeneralDBContext generalDBContext)
        {
            this.generalDBContext =generalDBContext;
        }

        public async Task<List<TblLesson>> GetAllLessons(int patientID)
        {
            return await generalDBContext.TblLessons.Where(l => l.PatientId == patientID).Include(l=>l.DifficultyLevel).ThenInclude(d=>d.PronunciationProblem).ToListAsync();//.Include(x => ((TblWordsGivenToPractice)x).Score)
        }

        public async Task<List<TblWordsGivenToPractice>> GetLessonWords(int lessonID)
        {
            return await generalDBContext.TblWordsGivenToPractices.Where(w => w.LessonId == lessonID).Include(l=>l.Word).ToListAsync();
        }

        public async Task PostLesson(TblLesson tblLesson)
        {
            await generalDBContext.TblLessons.AddAsync(tblLesson);
            await generalDBContext.SaveChangesAsync();
        }

        public async Task PostWordToLesson(TblWordsGivenToPractice WordGivenToPractice)
        {

            await generalDBContext.TblWordsGivenToPractices.AddAsync(WordGivenToPractice);
            await generalDBContext.SaveChangesAsync();
        }

        public async Task PutLesson(TblLesson tblLesson)
        {
            TblLesson lesson = await generalDBContext.TblLessons.FindAsync(tblLesson.Id);
            if (lesson == null)
            {
                return;
            }
            generalDBContext.Entry(lesson).CurrentValues.SetValues(tblLesson);
            await generalDBContext.SaveChangesAsync();
        }

        public async Task PutWordForLesson(TblWordsGivenToPractice word)
        {
            TblWordsGivenToPractice practiceWord = await generalDBContext.TblWordsGivenToPractices.FindAsync(word.Id);
            if (practiceWord == null)
            {
                return;
            }
            generalDBContext.Entry(practiceWord).CurrentValues.SetValues(word);
            await generalDBContext.SaveChangesAsync();
        }

        public async Task PutColIsCheckedAtLesson(int lessonId)
        {
            TblLesson lesson = await generalDBContext.TblLessons.FindAsync(lessonId);
            var x = generalDBContext.Entry(lesson);
            lesson.IsChecked = !lesson.IsChecked;
            x.CurrentValues.SetValues(lesson);
            await generalDBContext.SaveChangesAsync();
        }

        public async Task PutColIsValidAtWordToPractice(int wordId)
        {
            TblWordsGivenToPractice wordGivenToPractice = await generalDBContext.TblWordsGivenToPractices.FindAsync(wordId);
            var x = generalDBContext.Entry(wordGivenToPractice);
            wordGivenToPractice.IsValid = !wordGivenToPractice.IsValid;
            x.CurrentValues.SetValues(wordGivenToPractice);
            await generalDBContext.SaveChangesAsync();
        }

        public async Task DeleteLesson(int lessonId)
        {
            TblLesson lesson = await generalDBContext.TblLessons.FindAsync(lessonId);
            foreach (var word in generalDBContext.TblWordsGivenToPractices)
            {
                if (word.LessonId== lessonId)
                {
                    DeleteWordFromLesson(word.Id);
                }
            }
                generalDBContext.TblLessons.Remove(lesson);
            await generalDBContext.SaveChangesAsync();
        }

        public async Task DeleteWordFromLesson(int wordId)
        {
            TblWordsGivenToPractice wordGivenToPractice = await generalDBContext.TblWordsGivenToPractices.FindAsync(wordId);
            generalDBContext.TblWordsGivenToPractices.Remove(wordGivenToPractice);
            await generalDBContext.SaveChangesAsync();
        }

        public async Task PutWordRecording(TblWordsGivenToPractice word, string filePath)
        {
            TblWordsGivenToPractice practiceWord = await generalDBContext.TblWordsGivenToPractices.FindAsync(word.Id);
            if (practiceWord == null)
            {
                return;
            }
            var x = generalDBContext.Entry(practiceWord);
            practiceWord.PatientRecording = filePath;
            x.CurrentValues.SetValues(practiceWord);
            //word.PatientRecording = filePath;

            //generalDBContext.Entry(practiceWord).CurrentValues.SetValues(word);
            await generalDBContext.SaveChangesAsync();
            int s = 3;
        }
    }
}

