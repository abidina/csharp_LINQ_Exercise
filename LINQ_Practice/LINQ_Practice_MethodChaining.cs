﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LINQ_Practice.Models;
using System.Linq;

namespace LINQ_Practice
{
    [TestClass]
    public class LINQ_Practice_MethodChaining
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
        public void GetAllCohortsWithZacharyZohanAsPrimaryOrJuniorInstructor()
        {
            var ActualCohorts = PracticeData.Where(cohort => cohort.PrimaryInstructor.LastName == "Zohan" || cohort.JuniorInstructors.Any(jrInstructor => jrInstructor.LastName == "Zohan")).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort2, CohortBuilder.Cohort3 });
        }

        [TestMethod]
        public void GetAllCohortsWhereFullTimeIsFalseAndAllInstructorsAreActive()
        {
            var ActualCohorts = PracticeData.Where(cohort => (cohort.FullTime == false) && (cohort.PrimaryInstructor.Active == true) && (cohort.JuniorInstructors.All(jrInstructor => jrInstructor.Active == true))).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort1 });
        }

        [TestMethod]
        public void GetAllCohortsWhereAStudentOrInstructorFirstNameIsKate()
        {
            var ActualCohorts = PracticeData.Where(cohort => (cohort.Students.Any(student => student.FirstName == "Kate")) || (cohort.JuniorInstructors.Any(jrInstructor => jrInstructor.FirstName == "Kate")) || (cohort.PrimaryInstructor.FirstName == "Kate")).ToList();
            CollectionAssert.AreEqual(ActualCohorts, new List<Cohort> { CohortBuilder.Cohort1, CohortBuilder.Cohort3, CohortBuilder.Cohort4 });
        }

        [TestMethod]
        public void GetOldestStudent()
        {
            var student = PracticeData.SelectMany(cohort => cohort.Students).OrderByDescending(students => students.Birthday).Last();
            Assert.AreEqual(student, CohortBuilder.Student18);
        }

        [TestMethod]
        public void GetYoungestStudent()
        {
            var student = PracticeData.SelectMany(cohort => cohort.Students).OrderByDescending(students => students.Birthday).First();
            Assert.AreEqual(student, CohortBuilder.Student3);
        }

        [TestMethod]
        public void GetAllInactiveStudentsByLastName()
        {
            var ActualStudents = PracticeData.SelectMany(cohort => cohort.Students).Where(student => student.Active == false).OrderBy(student => student.LastName).ToList();
            CollectionAssert.AreEqual(ActualStudents, new List<Student> { CohortBuilder.Student2, CohortBuilder.Student11, CohortBuilder.Student12, CohortBuilder.Student17 });
        }
    }
}
