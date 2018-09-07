using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.SignalR;
using N_tier_solution.DAL;
using N_tier_solution.Models;

namespace N_tier_solution.Controllers
{
    public class UploadController : Controller
    {
        private SolutionContext db = new SolutionContext();

        // GET: Records
        public ActionResult Index( int? Page)
        {
            int pageSize = 15;
            int pageNumber = (Page??1);
            return View(db.Records.ToList().ToPagedList(pageNumber,pageSize));
        }

        // GET: Records/Load
        public ActionResult Load()
        {
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Load(FileUpload File)
        {
            List<Records> records = new List<Records>();
            foreach (var file in File.Files)
            {
                if (file.ContentLength > 0)
                {

                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/" + filename));
                    file.SaveAs(path);

                    if (System.IO.File.Exists(path))
                    {
                        readFile(path, File.Files.Count());
                        
                    }

                    
                }
            }
           
            return View();
        }
       
        private void readFile(string path,int fileno)
        {
            List<Records> records = new List<Records>();
            if (System.IO.File.Exists(path))
            {
                using (var nstream = new StreamReader(path))
                {
                    string readText = "";
                    string[] items = { };
                    char[] chr = { '\r', '\n', ';' };
                    int isInteger = 0;
                    while ((readText = nstream.ReadLine()) != null)
                    {
                        items = readText.Split(chr).ToArray();
                        if (int.TryParse(items[0], out isInteger))
                        {
                            records.Add(new Records
                            {
                                FormulaID = int.Parse(items[0]),
                                A = int.Parse(items[1]),
                                B = int.Parse(items[2]),
                                C = int.Parse(items[3]),
                                // Results = Compute(int.Parse(items[0]), int.Parse(items[1]), int.Parse(items[2]), int.Parse(items[3]))
                            });
                        }

                    }

                }
            }
            if (records.Count > 0)
            {
                records.ForEach(x => x.Results = Compute(x.FormulaID, x.A, x.B, x.C));
                records.ForEach(r => db.Records.Add(r));
                db.SaveChanges();
                send("Files No: " + fileno + "<br />records processed: " + records.Count());


            }
        }
        /// <summary>
        /// notify the connected clients using signal r
        /// </summary>
        /// <param name="message"></param>
        private void send(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SolutionsHub>();
           
            context.Clients.All.send(message);
        }

        private double Compute(int formulaID, int a, int b, int c)
        {
            double results = 0;

            switch (formulaID)
            {
                case 1:
                    results = a * b / c;
                    break;
                case 2:
                    results = a % b * c;
                    break;
                case 3:
                    results = Math.Pow(a, c) - Math.Sqrt(b) * c;
                    break;

            }
            return results;
        }
        // GET: Records/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Records records = db.Records.Find(id);
            if (records == null)
            {
                return HttpNotFound();
            }
            return View(records);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FormulaID,A,B,C,Results")] Records records)
        {
            if (ModelState.IsValid)
            {
                db.Entry(records).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(records);
        }

        // GET: Records/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Records records = db.Records.Find(id);
            if (records == null)
            {
                return HttpNotFound();
            }
            return View(records);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Records records = db.Records.Find(id);
            db.Records.Remove(records);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
