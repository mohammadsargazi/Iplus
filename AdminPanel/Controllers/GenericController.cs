using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bipap.DAL;
using Bipap.DAL.Models;
using DynamicExpresso;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public partial class GenericController<T, T1> : BaseController<T1> where T : BaseModel where T1 : DbContext, ICustomDbContext
    {
        private List<string> includes;
        protected Dictionary<string, string> CustomActions;
        protected Random r;
        private readonly IHostingEnvironment _hostingEnvironment;
        public GenericController(T1 context, IConfiguration config, IHostingEnvironment hostingEnvironment) : base(context, config, hostingEnvironment)
        {
            includes = new List<string>();
            _hostingEnvironment = hostingEnvironment;
            AddIncludesLevel(ref includes, typeof(T), 3);
            includes = includes.Where(i => !i.EndsWith("." + typeof(T).Name)).ToList();

            //generate the dynamic menu
            CustomActions = new Dictionary<string, string>();
            CustomActions.Add("ساختن" + typeof(T).Name, "ساختن");
            CustomActions.Add("پیدا کردن " + typeof(T).Name + "(s)", "پیدا کردن");
            CustomActions.Add("Import from OpenXml Excel", "ImportFromOpenXMLExcel");
            CustomActions.Add("Hidden elements", "Hidden");
            r = new Random();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var type = typeof(T);
            var attr = type.CustomAttributes.Where(i => i.AttributeType == typeof(DisplayTableNameAttribute)).FirstOrDefault();
            ViewBag.TypeName = attr.NamedArguments.First().TypedValue.Value;
            ViewBag.CustomActions = CustomActions;
        }

        #region HelperMethod
        private bool IsImage(IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (!string.Equals(postedFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }
        private void SaveImage(IFormFileCollection files, T model)
        {

            string newFileName = "";
            bool isImage = true;
            var folderName = typeof(T).Name;
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    isImage = IsImage(file);
                    var uploadFolder = "";
                    if (isImage)
                        uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads\\" + folderName + "\\images");
                    else
                        uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads\\" + folderName + "\\audio");

                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine("uploads\\" + folderName + "\\images", fileName);

                        using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        newFileName = filePath;
                    }
                }
            }
            if (isImage)
            {
                var t = model.GetType();
                var property = t.GetProperty("ImageUrl");
                property.SetValue(model, newFileName, null);
            }
            else
            {
                var t = model.GetType();
                var property = t.GetProperty("FileUrl");
                property.SetValue(model, newFileName, null);
            }
        }
        //public static object GetPropValue(object src, string propName)
        //{
        //    var ddd = src.GetType().GetProperty(propName).GetValue(src, null);
        //    return src.GetType().GetProperty(propName).GetValue(src, null);
        //}
        //private void SetCurrentValue(T model)
        //{
        //    var oldModel = _context.Find<T>(model.Id);
        //    var imageValue = oldModel.GetType().GetProperty("ImageUrl").GetValue(oldModel);

        //    var t = model.GetType();
        //    var property = t.GetProperty("ImageUrl");
        //    property.SetValue(model, imageValue, null);

        //}
        #endregion

        /// <summary>
        /// Shows items from a table
        /// </summary>
        /// <param name="id">Page index</param>
        /// <param name="id1">Items per page</param>
        /// <returns></returns>
        public IActionResult Index(int? id, int? id1)
        {
            var type = typeof(T);
            var props = type.GetProperties().ToList().Where(i => !i.PropertyType.Name.StartsWith("ICollection")).ToList();
            var props1 = props.Where(i => i.CustomAttributes.Any(k => k.AttributeType == typeof(ForeignKeyAttribute))).ToList();
            var pagesize = id1 ?? 10;
            var pageindex = id ?? 1;
            var sourceCount = GetTable().Count();
            var source = GetTable().Skip((pageindex - 1) * pagesize).Take(pagesize);
            var t1 = source.Where(i => i.Visible).ToList();
            var data = t1;
            ViewBag.TotalPages = (int)sourceCount / pagesize + (sourceCount % pagesize == 0 ? 0 : 1);
            ViewBag.PageSize = pagesize;
            ViewBag.PageIndex = pageindex;
            props = props.Except(props1).ToList();
            props = props.Where(i => !i.CustomAttributes.Any(k => k.AttributeType == typeof(NotListedAttribute))).ToList();
            ViewBag.props = props;
            ViewBag.ImageUrl = typeof(T).Name + "\\images";
            ViewBag.AudioUrl = typeof(T).Name + "\\audio";
            return View(data);
        }

        /// <summary>
        /// Displays hidden elements of a table
        /// </summary>
        /// <param name="id">Page index</param>
        /// <param name="id1">Page size</param>
        /// <returns></returns>
        public IActionResult Hidden(int? id, int? id1)
        {
            var type = typeof(T);
            var props = type.GetProperties().ToList().Where(i => !i.PropertyType.Name.StartsWith("ICollection")).ToList();
            var props1 = props.Where(i => i.CustomAttributes.Any(k => k.AttributeType == typeof(ForeignKeyAttribute))).ToList();
            var pagesize = id1 ?? 100;
            var pageindex = id ?? 1;
            var source = GetTable().Skip((pageindex - 1) * pagesize).Take(pagesize);
            var t1 = source.Where(i => !i.Visible).ToList();
            var data = t1;
            ViewBag.TotalPages = (int)source.Count() / pagesize + (source.Count() % pagesize == 0 ? 0 : 1);
            ViewBag.PageSize = pagesize;
            props = props.Except(props1).ToList();
            props = props.Where(i => !i.CustomAttributes.Any(k => k.AttributeType == typeof(NotListedAttribute))).ToList();
            ViewBag.props = props;
            return View("Index", data);
        }

        /// <summary>
        /// Unhide an element
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns></returns>
        public IActionResult Unhide(int id)
        {
            var id_element = _context.Find<T>(id);
            var type = typeof(T);
            ///Get the user defined property to determinate if remove permanent the database row and it dependencies or hide it all
            var dtype = type.CustomAttributes.Any(i => i.AttributeType == typeof(DeleteBehaviorAttribute)) ? (DeleteBehaviorAttr)type.CustomAttributes.First(i => i.AttributeType == typeof(DeleteBehaviorAttribute)).ConstructorArguments.First().Value : DeleteBehaviorAttr.Delete;
            List<BaseModel> elements = new List<BaseModel>();
            RecursiveCascadesCollection(id_element, type, ref elements);
            elements.ForEach(i => i.Visible = true);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Search for include types to use with .Include()
        /// </summary>
        /// <param name="includes">The list</param>
        /// <param name="type">Type to search</param>
        /// <param name="level">Level of recursive</param>
        /// <param name="preffix">Preffix to add</param>
        private void AddIncludesLevel(ref List<string> includes, Type type, int level, string preffix = "")
        {
            var allp = type.GetProperties().ToList();
            allp = allp.Where(i => i.PropertyType.Name.StartsWith("ICollection") || i.CustomAttributes.Any(k => k.AttributeType == typeof(ForeignKeyAttribute))).ToList();
            foreach (var item in allp)
            {
                var iname = item.Name;
                if (item.CustomAttributes.Any(k => k.AttributeType == typeof(ForeignKeyAttribute)))
                {
                    iname = item.CustomAttributes.First(k => k.AttributeType == typeof(ForeignKeyAttribute)).ConstructorArguments.First().Value.ToString();
                }
                includes.Add(preffix + iname);
                if (level >= 1)
                {
                    var typ = item.PropertyType;
                    if (item.PropertyType.Name.StartsWith("ICollection"))
                    {
                        typ = item.PropertyType.GenericTypeArguments[0];
                    }
                    AddIncludesLevel(ref includes, typ, level - 1, preffix + iname + ".");
                }
            }
        }

        /// <summary>
        /// Allows the user to search entities of type T by it properties
        /// </summary>
        /// <returns></returns>
        public IActionResult FilterBy()
        {
            //TODO: DEtectar tipos adicionales de filtrado e incluirlos en el formulario
            var props = PrepareEditorView<T>(null, false);
            ViewBag.props = props;
            return View();
        }





        public IActionResult Details(int id)
        {
            var source = GetTable();
            var model = source.First(i => i.Id == id);
            var props = PrepareEditorView<T>(model, false).Where(i => !i.Attributes.Any(j => j.AttributeType == typeof(KeyAttribute)) && !i.Attributes.Any(j => j.AttributeType == typeof(NotListedAttribute))).ToList();
            ViewBag.props = props;
            return View(model);
        }

        public IQueryable<T> GetTable()
        {
            var source = _context.Set<T>().OrderBy(i => i.Id).AsQueryable();
            foreach (var include in includes)
            {
                source = source.Include(include);
            }
            return source;
        }

        private List<HtmlPropertyControl> PrepareEditorView<T1>(T1 model, bool only_editable = true, string color = "#FFF")
        {
            return PrepareEditorView(model, only_editable, typeof(T1), new Type[0], "", color);
        }

        /// <summary>
        /// Generate a list of HtmlPropertyControl for a selected Type/Model
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="only_editable">Exclude properties without setter properties</param>
        /// <param name="type">The Model type object</param>
        /// <param name="excludeTypes">A list of types to exclude</param>
        /// <param name="preffix">Preffix to add when the method get recursive paths</param>
        /// <param name="color">Background color used on each property</param>
        /// <returns>A list of HtmlPropertyControl for a selected Type/Model</returns> 
        private List<HtmlPropertyControl> PrepareEditorView(object model, bool only_editable, Type type, Type[] excludeTypes, string preffix = "", string color = "#FFF")
        {
            var result = new List<HtmlPropertyControl>();
            var dict = new Dictionary<PropertyInfo, string>();
            var attr = type.CustomAttributes.Where(i => i.AttributeType == typeof(DisplayTableNameAttribute)).FirstOrDefault();
            var tableName = attr.NamedArguments.First().TypedValue.Value.ToString();
            ViewData[preffix + type.Name] = tableName;
            var props = type.GetProperties().ToList();
            props = props.Where(i => !i.PropertyType.Name.StartsWith("ICollection")).ToList();
            if (only_editable)
                props = props.Where(i => i.GetSetMethod() != null).ToList();
            var props1 = props.Where(i => i.CustomAttributes.Any(k => k.AttributeType == typeof(ForeignKeyAttribute))).ToList();
            // Process all foreign keys properties and naviagation properties associates createing SelectList instances for each one.
            foreach (var item in props1)
            {
                var iattr = item.CustomAttributes.First(i => i.AttributeType == typeof(ForeignKeyAttribute));
                var field = iattr.ConstructorArguments.First().Value.ToString();
                var typex = props.First(i => i.Name == field);
                var typ = typex.PropertyType;
                if (excludeTypes.Any(k => k == typ)) //Exclude all propertys with type into the exclusion list
                {
                    props = props.Where(i => i.Name != field).ToList();
                    props = props.Where(i => i.Name != item.Name).ToList();
                    continue;
                }
                var data = _context.GetTable(typ);
                object pd = null;
                if (model != null)
                {
                    pd = item.GetValue(model);
                }
                if (item.PropertyType.Name.StartsWith("Nullable"))
                {
                    data.Insert(0, new KeyValuePair<object, string>(null, "---NULL---"));
                }
                ViewData[preffix + item.Name] = new SelectList(data, "Key", "Value", pd);
                props = props.Where(i => i.Name != field).ToList();
            }
            var props2 = type.GetProperties().ToList().Where(i => i.CustomAttributes.Any(j => j.AttributeType == typeof(DataTypeAttribute))).ToList();

            //Select html/input type for each property with a DataType attribute
            foreach (var prop2 in props2)
            {
                var custom_type = (DataType)prop2.CustomAttributes.First(i => i.AttributeType == typeof(DataTypeAttribute)).ConstructorArguments.First().Value;
                var np = new HtmlPropertyControl
                {
                    propertyInfo = prop2,
                    html_control_type = "input",
                    DataType = custom_type,
                    CurrentValue = model != null ? prop2.GetValue(model) : null,
                    Attributes = prop2.CustomAttributes.ToArray()
                };

                switch (custom_type)
                {
                    case DataType.CreditCard:
                        {
                            np.html_input_type = "text";
                            break;
                        }
                    case DataType.Currency:
                        {
                            np.html_input_type = "number";
                            break;
                        }
                    case DataType.Custom:
                        {
                            np.html_input_type = "text";
                            break;
                        }
                    case DataType.Date:
                        {
                            np.html_input_type = "date";
                            break;
                        }
                    case DataType.DateTime:
                        {
                            np.html_input_type = "datetime";
                            break;
                        }
                    case DataType.Duration:
                        {
                            np.html_input_type = "time";
                            break;
                        }
                    case DataType.EmailAddress:
                        {
                            np.html_input_type = "email";
                            break;
                        }
                    case DataType.Html:
                        {
                            np.html_input_type = "";
                            np.html_control_type = "textarea";
                            break;
                        }
                    case DataType.ImageUrl:
                        {
                            np.html_input_type = "url";
                            break;
                        }
                    case DataType.MultilineText:
                        {
                            np.html_control_type = "textarea";
                            np.html_input_type = "";
                            break;
                        }
                    case DataType.Password:
                        {
                            np.html_input_type = "password";
                            break;
                        }
                    case DataType.PhoneNumber:
                        {
                            np.html_input_type = "tel";
                            break;
                        }
                    case DataType.PostalCode:
                        {
                            np.html_input_type = "text";
                            break;
                        }
                    case DataType.Text:
                        {
                            np.html_input_type = "text";
                            break;
                        }
                    case DataType.Time:
                        {
                            np.html_input_type = "time";
                            break;
                        }
                    case DataType.Upload:
                        {
                            np.html_input_type = "file";
                            break;
                        }
                    case DataType.Url:
                        {
                            np.html_input_type = "url";
                            break;
                        }
                }
                np.bg_color = color;

                props.Remove(prop2);
                result.Add(np);
            }

            // For each remainent property 
            foreach (var item in props)
            {
                var np = new HtmlPropertyControl();
                np.bg_color = color;
                np.CurrentValue = model != null ? item.GetValue(model) : null;
                np.propertyInfo = item;
                np.Attributes = item.CustomAttributes;
                if (item.CustomAttributes.Any(ii => ii.AttributeType == typeof(RangeAttribute)))
                {
                    var range = item.CustomAttributes.First(ii => ii.AttributeType == typeof(RangeAttribute));
                    var ca = range.ConstructorArguments.ToList();
                    np.Min = ca[0].Value;
                    np.Max = ca[1].Value;
                    if (ca.Count > 2)
                        np.Message = ca[2].Value.ToString();
                }
                switch (np.propertyInfo.PropertyType.Name)
                {
                    case "Boolean":
                        {
                            np.html_input_type = "checkbox";
                            np.html_control_type = "input";
                            break;
                        }
                    case "String":
                        {
                            np.html_input_type = "text";
                            np.html_control_type = "input";
                            break;
                        }
                    case "DateTime":
                        {
                            np.html_input_type = "date";
                            np.html_control_type = "input";
                            break;
                        }
                    case "Int16":
                    case "Int32":
                    case "Int64":
                    case "UInt16":
                    case "UInt32":
                    case "UInt64":
                        {
                            np.html_control_type = "input";
                            np.html_input_type = "number";
                            np.Max = np.Max ?? item.PropertyType.UnderlyingSystemType.GetField("MaxValue").GetValue(null);
                            np.Min = np.Min ?? item.PropertyType.UnderlyingSystemType.GetField("MinValue").GetValue(null);
                            np.Message = np.Message ?? "Property out of range";
                            break;
                        }
                    case "Double":
                    case "Float":
                    case "Decimal":
                        {
                            np.html_control_type = "input";
                            np.html_input_type = "number";
                            np.Max = np.Max ?? item.PropertyType.UnderlyingSystemType.GetField("MaxValue").GetValue(null);
                            np.Min = np.Min ?? item.PropertyType.UnderlyingSystemType.GetField("MinValue").GetValue(null);
                            np.Message = np.Message ?? "Property out of range";
                            np.Step = "0,001";
                            break;
                        }
                }
                result.Add(np);
            }
            foreach (var item in result)
            {
                item.DisplayName = item.propertyInfo.CustomAttributes.Any(i => i.AttributeType == typeof(DisplayAttribute)) ? item.propertyInfo.CustomAttributes.First(i => i.AttributeType == typeof(DisplayAttribute)).NamedArguments.First().TypedValue.Value : item.propertyInfo.Name;
            }
            ViewData[preffix + "Controllers"] = dict;
            return result;
        }

        public IActionResult Create()
        {
            var props = PrepareEditorView<T>(null);
            props = AddCreationChainProperties(props);
            ViewBag.props = props;
            return View();
        }

        /// <summary>
        /// Adds the types included on the ChainOfCreation custom attribute to a list of HtmlPropertyControl
        /// </summary>
        /// <param name="props">Current properties</param>
        /// <param name="model">Model</param>
        /// <returns></returns>
        private List<HtmlPropertyControl> AddCreationChainProperties(List<HtmlPropertyControl> props, BaseModel model = null)
        {
            var mtype = typeof(T);
            var etypes = new Dictionary<Type, string>();
            //Quitar los ID y por cada propiedad agregada en lo siguiente, eliminar la propiedad asociada
            if (mtype.CustomAttributes.Any(i => i.AttributeType == typeof(ChainOfCreationAttribute)))
            {

                var types = (IEnumerable<CustomAttributeTypedArgument>)mtype.CustomAttributes.First(i => i.AttributeType == typeof(ChainOfCreationAttribute)).ConstructorArguments.First().Value;
                foreach (var type in types)
                {
                    var color = "#" + r.Next(128, 255).ToString("X") + r.Next(128, 255).ToString("X") + r.Next(128, 255).ToString("X");
                    props.AddRange(PrepareEditorView(null, true, (Type)type.Value, new[] { typeof(T) }, ((Type)type.Value).Name + "_", color));
                    etypes.Add((Type)type.Value, ((Type)type.Value).Name + "_");
                }
            }
            ViewBag.ETypes = etypes;
            // Removing PrimaryKey properties, and NotListed attributes
            var p1 = props.Where(i => !i.Attributes.Any(ia => ia.AttributeType == typeof(KeyAttribute) || ia.AttributeType == typeof(NotListedAttribute))).ToList();
            foreach (var item in p1)
            {
                if (item.Attributes.Any(i => i.AttributeType == typeof(ForeignKeyAttribute)))
                {
                    if (!ViewData.ContainsKey(item.propertyInfo.Name))
                        item.SelectorName = ViewData.Keys.First(i => i.EndsWith(item.propertyInfo.Name));
                }
            }
            return p1;
        }

        [HttpPost]
        public IActionResult Create(T model, IFormCollection fm)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                    SaveImage(files, model);
                _context.Add<T>(model);
                _context.SaveChanges();
                var l1 = AddCreationChainProperties(new List<HtmlPropertyControl>());
                foreach (KeyValuePair<Type, string> item in ViewBag.ETypes)
                {
                    var obj = item.Key.GetConstructors()[0].Invoke(null);
                    var props = item.Key.GetProperties().Where(i => !i.CustomAttributes.Any(j => j.AttributeType == typeof(KeyAttribute) || j.AttributeType == typeof(NotListedAttribute)) || !i.PropertyType.Name.StartsWith("ICollection")).ToList();
                    foreach (var prop in props)
                    {
                        var prop1 = prop;
                        if (prop1.CustomAttributes.Any(i => i.AttributeType == typeof(ForeignKeyAttribute)))
                        {
                            var prop2 = props.First(i => i.Name == (prop1.CustomAttributes.First(j => j.AttributeType == typeof(ForeignKeyAttribute)).ConstructorArguments.First().Value.ToString()));
                            if (prop2.PropertyType == typeof(T)) // If property type is model type then 
                            {
                                prop1.SetValue(obj, model.Id);
                            }
                            else
                            {
                                var pc = l1.FirstOrDefault(i => i.SelectorName == item.Value + prop.Name);
                                if (pc != null)
                                {
                                    var value = int.Parse(fm[pc.SelectorName]);
                                    prop1.SetValue(obj, value);
                                }
                            }
                        }
                        else
                        {
                            var pc = l1.FirstOrDefault(i => i.SelectorName == item.Value + prop.Name);
                            if (pc != null || fm.ContainsKey(prop1.Name))
                            {
                                object value = fm[pc != null ? pc.SelectorName : prop1.Name];
                                if (new[] { typeof(DateTime), typeof(Int32), typeof(Int64), typeof(Int16), typeof(Double), typeof(Decimal) }.Contains(prop.PropertyType))
                                {
                                    var tp = prop.PropertyType.GetMethods().First(i => i.Name == "Parse");
                                    object v1 = tp.Invoke(null, new[] { value.ToString() }); //ver por que no sale del método TryParse
                                    prop1.SetValue(obj, v1);
                                }
                                else
                                {
                                    prop1.SetValue(obj, value);
                                }
                            }
                        }
                    }
                    _context.Add(obj);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ee)
            {
                ViewBag.Exception = ee;
                var props = PrepareEditorView<T>(model).Where(i => !i.Attributes.Any(j => j.AttributeType == typeof(KeyAttribute))).ToList();
                props = AddCreationChainProperties(props);
                ViewBag.props = props;
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            var model = _context.Find<T>(id);
            ViewBag.props = PrepareEditorView<T>(model).Where(i => !i.Attributes.Any(j => j.AttributeType == typeof(KeyAttribute) || j.AttributeType == typeof(NeverUpdateAttribute))).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(T model)
        {
            try
            {
                var hasImageInModel = typeof(T).GetProperties().ToList().Where(i => i.CustomAttributes.Any(j => j.AttributeType == typeof(DataTypeAttribute) && ((DataType)j.ConstructorArguments[0].Value == DataType.Upload))).ToList().Count() > 0;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                    SaveImage(files, model);
                _context.Update<T>(model);
                if (files.Count == 0 && hasImageInModel)
                    _context.Entry(model).Property("ImageUrl").IsModified = false;
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ee)
            {
                ViewBag.Exception = ee;
                var props = PrepareEditorView(model).Where(i => !i.Attributes.Any(j => j.AttributeType == typeof(KeyAttribute))).ToList();
                ViewBag.props = props;
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var element = _context.Find<T>(id);
            ViewBag.props = PrepareEditorView<T>(element);
            return View(element);
        }

        [HttpPost]
        public IActionResult Delete(IFormCollection fm)
        {
            var id_element = _context.Find<T>(int.Parse(fm["id"]));
            var type = typeof(T);
            ///Get the user defined property to determinate if remove permanent the database row and it dependencies or hide it all
            var dtype = type.CustomAttributes.Any(i => i.AttributeType == typeof(DeleteBehaviorAttribute)) ? (DeleteBehaviorAttr)type.CustomAttributes.First(i => i.AttributeType == typeof(DeleteBehaviorAttribute)).ConstructorArguments.First().Value : DeleteBehaviorAttr.Delete;
            List<BaseModel> elements = new List<BaseModel>();
            //RecursiveCascadesCollection(id_element, type, ref elements);
            switch (dtype)
            {
                case DeleteBehaviorAttr.Delete:
                    {
                        _context.RemoveRange(elements.ToArray());
                        _context.SaveChanges();
                        _context.Remove<T>(id_element);
                        _context.SaveChanges();
                        break;
                    }
                case DeleteBehaviorAttr.Hide:
                    {
                        elements.ForEach(k => k.Visible = false);
                        _context.Find<T>(int.Parse(fm["id"])).Visible = false;
                        _context.SaveChanges();
                        break;
                    }
            }
            return RedirectToAction("Index");
        }

        private void RecursiveCascadesCollection(object paren, Type type, ref List<BaseModel> elements)
        {
            var parent = (BaseModel)paren;
            var props = type.GetProperties().Where(i => i.PropertyType.GenericTypeArguments.Any() && i.PropertyType.Name.StartsWith("ICollection")).ToList();
            var interp = new Interpreter();
            interp.EnableReflection();
            interp.Reference(type);
            foreach (var prop in props)
            {
                var gtype = prop.PropertyType.GenericTypeArguments.First();
                interp.Reference(gtype);
                var fkprop = FindPropertyWithFKTo(gtype, type);
                var table = _context.GetTable(gtype, true);
                var sexpr = $"(({gtype.Name})iterator).{fkprop.Name} == {parent.Id}";
                var expr = interp.ParseAsExpression<Func<object, bool>>((sexpr), new[] { "iterator" });
                var cl = expr.Compile();
                var source = table.Where(cl).ToList();
                foreach (var item in source)
                {
                    RecursiveCascadesCollection(item, item.GetType(), ref elements);
                    elements.Add((BaseModel)item);
                }
            }
            elements.Add((BaseModel)paren);
        }

        private PropertyInfo FindPropertyWithFKTo(Type tfrom, Type tto)
        {
            var props = from i in tfrom.GetProperties().Select(i => new { i, i.Name, i.PropertyType, AttributeFK = i.CustomAttributes.FirstOrDefault(k => k.AttributeType == typeof(ForeignKeyAttribute)).ConstructorArguments.FirstOrDefault().Value })
                        join j in tfrom.GetProperties().Select(ij => new { ij.PropertyType, ij.Name })
                        on i.AttributeFK equals j.Name
                        let k = new { i, j }
                        select k;
            return props.First(t => t.j.PropertyType == tto).i.i;
        }

        public IActionResult ImportFromCSVWithHeaders()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportFromCSVWithHeaders(IFormCollection fm)
        {
            var props = PrepareEditorView<T>(null).ToDictionary(i => i.propertyInfo.Name, i => i);
            var file = Request.Form.Files[0];
            var sr = new StreamReader(file.OpenReadStream());
            var lines = sr.ReadToEnd().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(i => string.IsNullOrWhiteSpace(i)).ToList();
            var headers = lines[0].Split(';').ToList();
            var data = lines.Skip(1).Select(i => i.Split(';')).ToList();

            var ctor = typeof(T).GetConstructors().First();
            foreach (var item in data)
            {
                T obj = (T)ctor.Invoke(null);
                foreach (var header in headers)
                {
                    if (!props.ContainsKey(header)) continue;
                    var svalue = item[headers.IndexOf(header)];
                    object valu = null;
                    switch (props[header].propertyInfo.PropertyType.Name)
                    {
                        case "string":
                            {
                                valu = svalue;
                                break;
                            }
                        default:
                            {
                                var tp = props[header].propertyInfo.PropertyType.GetMethods().Where(i => i.Name.Contains("TryParse")).FirstOrDefault();
                                if (tp == null) continue;
                                if (!(bool)tp.Invoke(null, new[] { svalue, valu }))
                                    continue;
                                break;
                            }
                    }
                    props[header].propertyInfo.SetValue(obj, valu);
                }
                _context.Add<T>(obj);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ImportFromOpenXMLExcel()
        {
            ViewBag.Action = "ImportFromOpenXMLExcel1";
            return View("ImportFromCSVWithHeaders");
        }



    }
}
