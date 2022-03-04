using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactsApp.Models;
namespace ContactsApp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactsDBEntities2 db = new ContactsDBEntities2();
        public ActionResult Index()
        {
            var model = new ContactViewModel();
            model.LContacts = (from products in db.Contacts
                               select products).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ContactViewModel model)
        {
            if (model.contact != null)
            {
                var contact = new Contact()
                {
                    Name = model.contact.Name,
                    PhNo = model.contact.PhNo,
                    Email = model.contact.Email
                };

                db.Contacts.Add(contact);
                db.SaveChanges();
                ModelState.Clear();

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User account)
        {
            if (ModelState.IsValid)
            {
                using (ContactsDBEntities2 db = new ContactsDBEntities2())
                {
                    db.Users.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                
            }
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user1)
        {
            using (ContactsDBEntities2 db = new ContactsDBEntities2())
            {
                var usr = db.Users.Single(u => u.Email == user1.Email && u.PassWord == user1.PassWord);
                if (usr != null)
                {
                    Session["Email"] = usr.Email.ToString();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                }
            }
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}