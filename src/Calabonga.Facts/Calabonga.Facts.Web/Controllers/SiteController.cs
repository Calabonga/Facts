using Calabonga.Facts.Web.Mediatr;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Calabonga.Facts.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.Facts.Web.Controllers.Facts.Queries;

namespace Calabonga.Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;
        private readonly List<SelectListItem> _subjects;

        public SiteController(
            IMediator mediator,
            IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
            _subjects = new List<string> { "Связь с разработчиком", "Жалоба", "Предложение", "Другое" }
            .Select(x => new SelectListItem { Value = x, Text = x })
            .ToList();
        }

        public IActionResult About() => View();

        public IActionResult Feedback()
        {
            ViewData["Subjects"] = _subjects;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Feedback(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (TempData["Capture"] is null)
                    {
                        ModelState.AddModelError("_FORM", "Извините, не могу отправить сообщение, не работает reCapture");
                        ViewData["subjects"] = _subjects;
                        return View(model);
                    }

                    var result = int.Parse(TempData["Capture"].ToString()!);
                    if (model.HumanNumber != result)
                    {
                        ModelState.AddModelError("_FORM", "Извините, результат вычисления неверный. Попробуйте еще, пожалуйста");
                        ViewData["subjects"] = _subjects;
                        return View(model);
                    }
                    await _mediator.Publish(new FeedbackNotification(model));
                    TempData["Feedback"] = "Feedback";
                    return RedirectToAction("FeedbackSent", "Site");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", "Извините, не могу отправить сообщение:\n" + ex.Message);
                }
            }

            ViewData["subjects"] = _subjects;
            return View(model);
        }

        public IActionResult FeedbackSent()
        {
            if (TempData["Feedback"] is null)
            {
                return RedirectToAction("Index", "Facts");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        public IActionResult GetImage(int? x, int? y, int? z)
        {
            Random r = new();
            x ??= r.Next(21, 30);
            y ??= r.Next(11, 20);
            z ??= r.Next(1, 10);
            var width = 100;
            var height = 30;
            using Bitmap bmp = new(width, height);
            using var g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            var stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            var backgroundColors = new[] { Color.BlueViolet, Color.Blue, Color.Brown, Color.DarkMagenta, Color.DarkGreen };
            var foregroundColors = new[] { Color.AliceBlue, Color.Gold, Color.GhostWhite, Color.Aqua, Color.Ivory };

            g.Clear(backgroundColors[r.Next(0, backgroundColors.Length - 1)]);
            var font = new Font("Arial", 14, FontStyle.Bold);
            var brush = new SolidBrush(foregroundColors[r.Next(0, foregroundColors.Length - 1)]);
            g.DrawString($"{x}+{y}-{z}", font, brush, new PointF(50, 15), stringFormat);
            var filename = string.Concat(_environment.WebRootPath, "/", Guid.NewGuid().ToString("N"));
            bmp.Save(filename, ImageFormat.MemoryBmp);
            byte[] bytes;
            using (FileStream stream = new(filename, FileMode.Open))
            {
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            }

            System.IO.File.Delete(filename);
            TempData["Capture"] = x + y - z;
            return new FileContentResult(bytes, "image/jpeg");
        }

        public async Task<IActionResult> GetPicture(Guid factId)
        {
            var operationResult = await _mediator.Send(new FactGetByIdRequest(factId), HttpContext.RequestAborted);
            if (!operationResult.Ok)
            {
                return Content("Not found");
            }

            var text = operationResult.Result.Content;

            Random r = new();
            var width = 600;
            var height = 450;
            using Bitmap bmp = new(width, height);
            using var g = Graphics.FromImage(bmp);

            var stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            var backgroundColors = new[] { Color.BlueViolet, Color.Blue, Color.Brown, Color.DarkMagenta, Color.DarkGreen, Color.CadetBlue, Color.DarkBlue, Color.DarkSlateGray };
            var foregroundColors = new[] { Color.White, Color.Gold, Color.Aqua, Color.Ivory, Color.Bisque, Color.Cornsilk };
            g.Clear(backgroundColors[r.Next(0, backgroundColors.Length - 1)]);
            var brush = new SolidBrush(foregroundColors[r.Next(0, foregroundColors.Length - 1)]);
            var brush1 = new SolidBrush(foregroundColors[r.Next(0, foregroundColors.Length - 1)]);
            var ramka = new Rectangle[] { new(1, 1, 598, 448), new(6, 6, 588, 438) };
            g.DrawRectangles(new Pen(brush), ramka);

            g.DrawLine(new Pen(brush), new PointF(7, 30), new PointF(593, 30));


            g.DrawLine(new Pen(brush), new PointF(7, 420), new PointF(593, 420));

            var container = new Rectangle(6, 30, 594, 390);

            var font = FindFont(g, text, container.Size, new Font("Arial", 32, FontStyle.Regular, GraphicsUnit.Pixel));
            g.DrawString(text, font, brush, container, stringFormat);

            g.DrawString("Знаете ли вы что...", new Font("Arial", 14), brush1, new PointF(6,6));
            g.DrawString("jfacts.ru", new Font("Arial", 14), brush1, new PointF(520,420));

            var filename = string.Concat(_environment.WebRootPath, "/", Guid.NewGuid().ToString("N"));
            bmp.Save(filename, ImageFormat.MemoryBmp);
            byte[] bytes;
            using (FileStream stream = new(filename, FileMode.Open))
            {
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            }

            System.IO.File.Delete(filename);
            return new FileContentResult(bytes, "image/png");
        }

        // This function checks the room size and your text and appropriate font
        //  for your text to fit in room
        // PreferedFont is the Font that you wish to apply
        // Room is your space in which your text should be in.
        // LongString is the string which it's bounds is more than room bounds.
        private Font FindFont(
            Graphics g,
            string longString,
            Size containerSize,
            Font originalFont)
        {
            // you should perform some scale functions!!!
            // We utilize MeasureString which we get via a control instance           
            for (int adjustedSize = (int)originalFont.Size; adjustedSize >= 1; adjustedSize--)
            {
                var testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style, GraphicsUnit.Pixel);

                // Test the string with the new size
                var adjustedSizeNew = g.MeasureString(longString, testFont, containerSize.Width);

                if (containerSize.Height > Convert.ToInt32(adjustedSizeNew.Height))
                {
                    // Good font, return it
                    return testFont;
                }
            }

            return new Font(originalFont.Name, 1, originalFont.Style, GraphicsUnit.Pixel);
        }
    }
}