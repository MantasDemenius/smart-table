using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_table.Models;
using smart_table.Models.DataBase;
using QRCoder;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace smart_table.Staff.Controllers
{
    public class QrCodeStaffController : Controller
    {
        private readonly DataBaseContext _context;
        //Change this for QRCode link generation
        private static string QRText = "http://localhost:65312/TakeTable?id=";

        public QrCodeStaffController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: QrCode/Details/5
        public async Task<IActionResult> downloadQrCode(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");

            Byte[] QRcode = createQrCode(id);
            var dataTuple = new Tuple<List<CustomerTables>, Byte[]>(await _context.CustomerTables.ToListAsync(), QRcode);
            return View("~/Staff/Views/" + "TableListView.cshtml", dataTuple);
        }

        private static Byte[] createQrCode(long? id)
        {
            string qrText = $"{QRText}{id}";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return makePNGImage(qrCodeImage);
        }

        private static Byte[] makePNGImage(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
