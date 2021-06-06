using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoangTrungThong_54.Models;

namespace HoangTrungThong_54.Controllers
{
    public class SanPhamsController : Controller
    {
        private LTQLDb db = new LTQLDb();

        // GET: SanPhams
        [Authorize]
        public ActionResult Index()
        {
            return View(db.SanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,TenSanPham,MaNhaCungCap")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSanPham,TenSanPham,MaNhaCungCap")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      /*  public ActionResult DownloadFile()
        {
            //duong dan chua file muon download
            string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/Excels/"; // tu muc chua fiel excel
            //chuyen file sang dang byte
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "fielexcel.xlsx"); //doan nay de file excel
            //ten file khi download ve
            string fileName = "Sinhviencapnhat.xlsx";
            //tra ve file
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            string _FileName = "ruoumoi.xls";

            string _path = Path.Combine(Server.MapPath("~/Uploads/Excels"), _FileName);

            file.SaveAs(_path);

            DataTable dt = ReadDataFromExcelFile(_path);
            *//* CopyDataByBulk(dt);*//*
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SanPham kr = new SanPham();
                kr.TenSanPham = dt.Rows[i][0].ToString();
                kr.MaNhaCungCap = dt.Rows[i][1].ToString();
                db.SanPhams.Add(kr);
                db.SaveChanges();
            }

            *//* CopyDataByBulk(excel.ReadDataFromExcelFile(_path));*//*
            return RedirectToAction("Index");
        }

        private void CopyDataByBulk(object v)
        {
            throw new NotImplementedException();
        }
        public DataTable ReadDataFromExcelFile(string filepath)
        {
            string connectionString = "";
            string fileExtention = filepath.Substring(filepath.Length - 4).ToLower();
            if (fileExtention.IndexOf("xlsx") == 0)
            {
                connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + filepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO\"";
            }
            else if (fileExtention.IndexOf(".xls") == 0)
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=Excel 8.0";
            }


            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {

                oledbConn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                DataSet ds = new DataSet();

                oleda.Fill(ds);

                data = ds.Tables[0];
            }
            catch
            {
            }
            finally
            {

                oledbConn.Close();
            }
            return data;
        }

        private void CopyDataByBulk(DataTable dt)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RuouDbContext"].ConnectionString);
            SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
            bulkcopy.DestinationTableName = "KhoRuous";
            bulkcopy.ColumnMappings.Add(0, "Tên Rượu");
            bulkcopy.ColumnMappings.Add(1, "Nồng Độ");
            bulkcopy.ColumnMappings.Add(2, "Tính Chất");
            bulkcopy.ColumnMappings.Add(3, " Số Lượng");
            bulkcopy.ColumnMappings.Add(4, " Đơn Vị");
            bulkcopy.ColumnMappings.Add(5, "Thể Tích");
            bulkcopy.ColumnMappings.Add(6, "Nguồn Gốc");
            bulkcopy.ColumnMappings.Add(7, "Đánh Giá");
            con.Open();
            bulkcopy.WriteToServer(dt);
            con.Close();
        }*/
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
