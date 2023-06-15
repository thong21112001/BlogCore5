using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebBlogCore5.Helpers;
using WebBlogCore5.Models;

namespace WebBlogCore5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly BLOGCORE5Context _context;

        public CategoriesController(BLOGCORE5Context context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = Utilities.PAGE_SIZE; //20 record
            var lsCategories = _context.Categories.OrderByDescending(x => x.CateId);    // lấy ra danh sách danh mục và sắp xếp mới nhất theo cateId
            PagedList<Category> models = new PagedList<Category>(lsCategories, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CateId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            ViewData["DanhMucGoc"] = new SelectList(_context.Categories.Where(x => x.Levels == 1), "CateId", "CateName");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CateId,CateName,Tittle,Alias,MetaDes,MetaKey,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category, Microsoft.AspNetCore.Http.IFormFile fThumb, Microsoft.AspNetCore.Http.IFormFile fCover, Microsoft.AspNetCore.Http.IFormFile fIcon)
        {
            if (ModelState.IsValid)
            {
                category.Alias = Utilities.SEOUrl(category.CateName);   //Tao duong dan url tu name cate
                if (category.Levels == null)
                {
                    category.Levels = 1;
                }
                else
                {
                    category.Levels = category.Parents == 0 ? 1 : 2;
                }
                if (fThumb != null)
                {
                    string ex = Path.GetExtension(fThumb.FileName);
                    string NewName = Utilities.SEOUrl(category.CateName) + "preview_" + ex;
                    category.Thumb = await Utilities.UploadFile(fThumb, @"categories\", NewName.ToLower());
                }
                if (fCover != null)
                {
                    string ex = Path.GetExtension(fCover.FileName);
                    string NewName = "cover_" + Utilities.SEOUrl(category.CateName) + ex;
                    category.Cover = await Utilities.UploadFile(fCover, @"covers\", NewName.ToLower());
                }
                if (fIcon != null)
                {
                    string ex = Path.GetExtension(fIcon.FileName);
                    string NewName = "icon_" + Utilities.SEOUrl(category.CateName) + ex;
                    category.Icon = await Utilities.UploadFile(fIcon, @"icons\", NewName.ToLower());
                }

                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CateId,CateName,Tittle,Alias,MetaDes,MetaKey,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category, Microsoft.AspNetCore.Http.IFormFile fThumb, Microsoft.AspNetCore.Http.IFormFile fCover, Microsoft.AspNetCore.Http.IFormFile fIcon)
        {
            if (id != category.CateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.Alias = Utilities.SEOUrl(category.CateName);   //Tao duong dan url tu name cate
                    if (category.Levels == null)
                    {
                        category.Levels = 1;
                    }
                    else
                    {
                        category.Levels = category.Parents == 0 ? 1 : 2;
                    }
                    if (fThumb != null)
                    {
                        string ex = Path.GetExtension(fThumb.FileName);
                        string NewName = Utilities.SEOUrl(category.CateName) + "preview_" + ex;
                        category.Thumb = await Utilities.UploadFile(fThumb, @"categories\", NewName.ToLower());
                    }
                    if (fCover != null)
                    {
                        string ex = Path.GetExtension(fCover.FileName);
                        string NewName = "cover_" + Utilities.SEOUrl(category.CateName) + ex;
                        category.Cover = await Utilities.UploadFile(fCover, @"covers\", NewName.ToLower());
                    }
                    if (fIcon != null)
                    {
                        string ex = Path.GetExtension(fIcon.FileName);
                        string NewName = "icon_" + Utilities.SEOUrl(category.CateName) + ex;
                        category.Icon = await Utilities.UploadFile(fIcon, @"icons\", NewName.ToLower());
                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CateId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CateId == id);
        }
    }
}
