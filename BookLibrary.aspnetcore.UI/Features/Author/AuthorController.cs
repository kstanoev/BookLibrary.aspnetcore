﻿
namespace BookLibrary.aspnetcore.UI.Features.Author
{
    using AutoMapper;
    using BookLibrary.aspnetcore.Domain;
    using BookLibrary.aspnetcore.Services.Interfaces;
    using BookLibrary.aspnetcore.UI.Features.Book;
    using BookLibrary.aspnetcore.UI.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AuthorController : BaseController
    {
        private IBookService _bookService;
        private IAuthorService _authorService;

        public AuthorController(IMapper mapper, IBookService bookService,
                               IAuthorService authorService) : base(mapper)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        // GET: Author
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAuthors()
        {
            var authors = await _authorService.GetAll();
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(authors);
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var author = await _authorService.Get(id.Value);

            return View(_mapper.Map<Author, AuthorViewModel>(author));
        }
        public async Task<IActionResult> DetailsPartial(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var author = await _authorService.Get(id.Value);

            return PartialView("_authorDetails", _mapper.Map<Author, AuthorViewModel>(author));
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            var authorVM = new AuthorViewModel();
            return View(authorVM);
        }

        // POST: Author/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorViewModel authorVM)
        {
            var created = await _authorService.Create(_mapper.Map<AuthorViewModel, Author>(authorVM));
            return created ? Ok() as ActionResult : BadRequest();
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var author = await _authorService.Get(id.Value);

            var authorVM = _mapper.Map<Author, AuthorViewModel>(author);
            return View(authorVM);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] AuthorViewModel authorVM)
        {
            var edited = await _authorService.Update(_mapper.Map<AuthorViewModel, Author>(authorVM));
            return edited ? Ok() as ActionResult : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorService.Delete(id);
            return deleted ? Ok() as ActionResult : BadRequest();
        }

        public async Task<IEnumerable<BookViewModel>> GetBooks()
        {
            var books = await _bookService.GetAll();
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
        }

    }

}