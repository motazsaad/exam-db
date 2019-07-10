﻿using exam_db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace exam_db.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
           ViewBag.college = db.Colleges.ToList();
            ViewBag.files = db.Files.
                Include(f => f.Course).OrderByDescending(x => x.Id).Take(5).ToList();
            //OrderByDescending(x => x.Id).Take(5).ToList();
            ViewBag.departments = db.Departments.ToList();
            ViewBag.courses = db.Courses.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string query)
        {
            ViewBag.files = db.Files
                    .Where(f => f.title.Contains(query))
                    .ToList();
            Random rand = new Random();
            string[] Questions = { query };
            if (System.Web.HttpContext.Current.Session["Questions"] == null)
            {
                System.Web.HttpContext.Current.Session["Questions"] = Questions; // here question is string array, 
                                                                                 //assigning value of array to session if session is null
            }
            else
            {
                string[] newQuestions = Questions;
                string[] existingQuestions = (string[])System.Web.HttpContext.Current.Session["Questions"];
                System.Web.HttpContext.Current.Session["Questions"] = newQuestions.Union(existingQuestions).ToArray();
            }

            return View();
        }

        public new ActionResult Profile()
        {
            return View();
        }
        
    }
}