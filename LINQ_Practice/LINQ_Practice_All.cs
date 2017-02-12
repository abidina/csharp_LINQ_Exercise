using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LINQ_Practice.Models;
using System.Linq;

namespace LINQ_Practice
{
    [TestClass]
    public class LINQ_Practice_All
    {

        public List<Cohort> PracticeData { get; set; }
        public CohortBuilder CohortBuilder { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            CohortBuilder = new CohortBuilder();
            PracticeData = CohortBuilder.GenerateCohorts();
        }

        [TestCleanup]
        public void TearDown()
        {
            CohortBuilder = null;
            PracticeData = null;
        }

        [TestMethod]
        public void DoAllCohortsHaveTwoOrMoreJuniorInstructors()
        {
            var doAll = PracticeData.All(cohort => cohort.JuniorInstructors.Count >= 2);
            Assert.IsTrue(doAll); 
        }

        [TestMethod]
        public void DoAllCohortsFiveStudents()
        {
            var doAll = PracticeData.All(cohort => cohort.Students.Count == 5);
            Assert.IsTrue(doAll);
        }

        [TestMethod]
        public void DoAllCohortsHavePrimaryInstructorsBornIn1980s()
        {
            var doAll = PracticeData.All(cohort => cohort.PrimaryInstructor.Birthday.Year >= 1980 && cohort.PrimaryInstructor.Birthday.Year <= 1989);
            Assert.IsFalse(doAll); 
        }

        [TestMethod]
        public void DoAllCohortsHaveActivePrimaryInstructors()
        {
            var doAll = PracticeData.All(cohort => cohort.PrimaryInstructor.Active); ;
            Assert.IsTrue(doAll);
        }

        [TestMethod]
        public void DoAllStudentsInCohort1HaveFirstNamesThatContainTheLetterE()
        {
            var doAll = PracticeData[0].Students.All(student => student.FirstName.ToLower().Contains("e") == true); 
            Assert.IsTrue(doAll);
        }

        [TestMethod]
        public void DoAllActiveCohortsHavePrimaryInstructorsWithFirstNamesThatContainTheLetterA()
        {
            var doAll = PracticeData.All(cohort => cohort.PrimaryInstructor.FirstName.ToLower().Contains("a") == true && cohort.PrimaryInstructor.Active);
            Assert.IsFalse(doAll);
        }
    }
}
