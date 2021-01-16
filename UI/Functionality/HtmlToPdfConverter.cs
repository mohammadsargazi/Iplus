using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI.Functionality
{
    public class HtmlToPdfConverter
    {
        private static object globalObj = new object();
        private static string[] ignoreWkHtmlToPdfErrLines = new string[6]
        {
      "Exit with code 1 due to network error: ContentNotFoundError",
      "QFont::setPixelSize: Pixel size <= 0",
      "Exit with code 1 due to network error: ProtocolUnknownError",
      "Exit with code 1 due to network error: HostNotFoundError",
      "Exit with code 1 due to network error: ContentOperationNotPermittedError",
      "Exit with code 1 due to network error: UnknownContentError"
        };
        private const string headerFooterHtmlTpl = "<!DOCTYPE html><html><head>\r\n<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n<script>\r\nfunction subst() {{\r\n    var vars={{}};\r\n    var x=document.location.search.substring(1).split('&');\r\n\r\n    for(var i in x) {{var z=x[i].split('=',2);vars[z[0]] = unescape(z[1]);}}\r\n    var x=['frompage','topage','page','webpage','section','subsection','subsubsection'];\r\n    for(var i in x) {{\r\n      var y = document.getElementsByClassName(x[i]);\r\n      for(var j=0; j<y.length; ++j) y[j].textContent = vars[x[i]];\r\n    }}\r\n}}\r\n</script></head><body style=\"border:0; margin: 0;\" onload=\"subst()\">{0}</body></html>\r\n";
        private Process WkHtmlToPdfProcess;
        private bool batchMode;

        /// <summary>Get or set path where WkHtmlToPdf tool is located</summary>
        /// <remarks>
        /// By default this property points to the folder where application assemblies are located.
        /// If WkHtmlToPdf tool files are not present PdfConverter expands them from DLL resources.
        /// </remarks>
        public string PdfToolPath { get; set; }

        /// <summary>
        /// Get or set WkHtmlToPdf tool EXE file name ('wkhtmltopdf.exe' by default)
        /// </summary>
        public string WkHtmlToPdfExeName { get; set; }

        /// <summary>
        /// Get or set location for temp files (if not specified location returned by <see cref="M:System.IO.Path.GetTempPath" /> is used for temp files)
        /// </summary>
        /// <remarks>Temp files are used for providing cover page/header/footer HTML templates to wkhtmltopdf tool.</remarks>
        public string TempFilesPath { get; set; }

        /// <summary>Get or set PDF page orientation</summary>
        public PageOrientation Orientation { get; set; }

        /// <summary>Get or set PDF page orientation</summary>
        public PageSize Size { get; set; }

        /// <summary>
        /// Gets or sets option to generate low quality PDF (shrink the result document space)
        /// </summary>
        public bool LowQuality { get; set; }

        /// <summary>Gets or sets option to generate grayscale PDF</summary>
        public bool Grayscale { get; set; }

        /// <summary>Gets or sets zoom factor</summary>
        public float Zoom { get; set; }

        /// <summary>Gets or sets PDF page margins (in mm)</summary>
        public PageMargins Margins { get; set; }

        /// <summary>Gets or sets PDF page width (in mm)</summary>
        public float? PageWidth { get; set; }

        /// <summary>Gets or sets PDF page height (in mm)</summary>
        public float? PageHeight { get; set; }

        /// <summary>Gets or sets TOC generation flag</summary>
        public bool GenerateToc { get; set; }

        /// <summary>
        /// Gets or sets custom TOC header text (default: "Table of Contents")
        /// </summary>
        public string TocHeaderText { get; set; }

        /// <summary>Custom WkHtmlToPdf global options</summary>
        public string CustomWkHtmlArgs { get; set; }

        /// <summary>Custom WkHtmlToPdf page options</summary>
        public string CustomWkHtmlPageArgs { get; set; }

        /// <summary>
        /// Custom WkHtmlToPdf cover options (applied only if cover content is specified)
        /// </summary>
        public string CustomWkHtmlCoverArgs { get; set; }

        /// <summary>
        /// Custom WkHtmlToPdf toc options (applied only if GenerateToc is true)
        /// </summary>
        public string CustomWkHtmlTocArgs { get; set; }

        /// <summary>Get or set custom page header HTML</summary>
        public string PageHeaderHtml { get; set; }

        /// <summary>Get or set custom page footer HTML</summary>
        public string PageFooterHtml { get; set; }

        /// <summary>
        /// Get or set maximum execution time for PDF generation process (by default is null that means no timeout)
        /// </summary>
        public TimeSpan? ExecutionTimeout { get; set; }

        /// <summary>
        /// Gets or sets wkhtmltopdf process priority (Normal by default)
        /// </summary>
        public ProcessPriorityClass ProcessPriority { get; set; }

        /// <summary>
        /// Gets or sets wkhtmltopdf processor affinity (bitmask that represents the processors that may be used by the process threads).
        /// </summary>
        public IntPtr? ProcessProcessorAffinity { get; set; }

        /// <summary>
        /// Occurs when log line is received from WkHtmlToPdf process
        /// </summary>
        /// <remarks>
        /// Quiet mode should be disabled if you want to get wkhtmltopdf info/debug messages
        /// </remarks>
        public event EventHandler<DataReceivedEventArgs> LogReceived;

        /// <summary>
        /// Suppress wkhtmltopdf debug/info log messages (by default is true)
        /// </summary>
        public bool Quiet { get; set; }

        /// <summary>Component commercial license information.</summary>
        public LicenseInfo License { get; private set; }

        /// <summary>Create new instance of HtmlToPdfConverter</summary>
        public HtmlToPdfConverter()
        {
            this.ProcessPriority = ProcessPriorityClass.Normal;
            this.ProcessProcessorAffinity = new IntPtr?();
            this.License = new LicenseInfo();
            string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wkhtmltopdf");
            //if (HttpContext.Current != null)
            //    str = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\wkhtmltopdf");
            this.PdfToolPath = str;
            this.TempFilesPath = (string)null;
            this.WkHtmlToPdfExeName = "wkhtmltopdf.exe";
            this.Orientation = PageOrientation.Default;
            this.Size = PageSize.Default;
            this.LowQuality = false;
            this.Grayscale = false;
            this.Quiet = true;
            this.Zoom = 1f;
            this.Margins = new PageMargins();
        }

        private void EnsureWkHtmlLibs()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
            string str = "NReco.PdfGenerator.WkHtmlToPdf.";
            foreach (string name in manifestResourceNames)
            {
                if (name.StartsWith(str))
                {
                    string path = Path.Combine(this.PdfToolPath, Path.GetFileNameWithoutExtension(name.Substring(str.Length)));
                    lock (HtmlToPdfConverter.globalObj)
                    {
                        if (File.Exists(path))
                        {
                            if (File.GetLastWriteTime(path) > File.GetLastWriteTime(executingAssembly.Location))
                                continue;
                        }
                        if (!Directory.Exists(this.PdfToolPath))
                            Directory.CreateDirectory(this.PdfToolPath);
                        using (GZipStream gzipStream = new GZipStream(executingAssembly.GetManifestResourceStream(name), CompressionMode.Decompress, false))
                        {
                            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                byte[] buffer = new byte[65536];
                                int count;
                                while ((count = gzipStream.Read(buffer, 0, buffer.Length)) > 0)
                                    fileStream.Write(buffer, 0, count);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Generates PDF by specifed HTML content</summary>
        /// <param name="htmlContent">HTML content</param>
        /// <returns>PDF bytes</returns>
        public byte[] GeneratePdf(string htmlContent)
        {
            return this.GeneratePdf(htmlContent, (string)null);
        }

        /// <summary>
        /// Generates PDF by specfied HTML content and prepend cover page (useful with GenerateToc option)
        /// </summary>
        /// <param name="htmlContent">HTML document</param>
        /// <param name="coverHtml">first page HTML (optional; can be null)</param>
        /// <returns>PDF bytes</returns>
        public byte[] GeneratePdf(string htmlContent, string coverHtml)
        {
            MemoryStream memoryStream = new MemoryStream();
            this.GeneratePdf(htmlContent, coverHtml, (Stream)memoryStream);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Generates PDF by specfied HTML content (optionally with the cover page).
        /// </summary>
        /// <param name="htmlContent">HTML document</param>
        /// <param name="coverHtml">first page HTML (optional; can be null)</param>
        /// <param name="output">output stream for generated PDF</param>
        public void GeneratePdf(string htmlContent, string coverHtml, Stream output)
        {
            if (htmlContent == null)
                throw new ArgumentNullException(nameof(htmlContent));
            this.GeneratePdfInternal((WkHtmlInput[])null, htmlContent, coverHtml, "-", output);
        }

        /// <summary>
        /// Generates PDF by specfied HTML content (optionally with the cover page).
        /// </summary>
        /// <param name="htmlContent">HTML document</param>
        /// <param name="coverHtml">first page HTML (can be null)</param>
        /// <param name="outputPdfFilePath">path to the output PDF file (if file already exists it will be removed before PDF generation)</param>
        public void GeneratePdf(string htmlContent, string coverHtml, string outputPdfFilePath)
        {
            if (htmlContent == null)
                throw new ArgumentNullException(nameof(htmlContent));
            this.GeneratePdfInternal((WkHtmlInput[])null, htmlContent, coverHtml, outputPdfFilePath, (Stream)null);
        }

        /// <summary>
        /// Generate PDF by specfied HTML content and prepend cover page (useful with GenerateToc option)
        /// </summary>
        /// <param name="htmlFilePath">path to HTML file or absolute URL</param>
        /// <param name="coverHtml">first page HTML (optional, can be null)</param>
        /// <returns>PDF bytes</returns>
        public byte[] GeneratePdfFromFile(string htmlFilePath, string coverHtml)
        {
            MemoryStream memoryStream = new MemoryStream();
            this.GeneratePdfInternal(new string[1] { htmlFilePath }, coverHtml, (Stream)memoryStream);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Generate PDF by specfied HTML content and prepend cover page (useful with GenerateToc option)
        /// </summary>
        /// <param name="htmlFilePath">path to HTML file or absolute URL</param>
        /// <param name="coverHtml">first page HTML (optional, can be null)</param>
        /// <param name="output">output stream for generated PDF</param>
        public void GeneratePdfFromFile(string htmlFilePath, string coverHtml, Stream output)
        {
            this.GeneratePdfInternal(new string[1] { htmlFilePath }, coverHtml, output);
        }

        /// <summary>
        /// Generate PDF by specfied HTML content and prepend cover page (useful with GenerateToc option)
        /// </summary>
        /// <param name="htmlFilePath">path to HTML file or absolute URL</param>
        /// <param name="coverHtml">first page HTML (optional, can be null)</param>
        /// <param name="outputPdfFilePath">path to the output PDF file (if file already exists it will be removed before PDF generation)</param>
        public void GeneratePdfFromFile(
          string htmlFilePath,
          string coverHtml,
          string outputPdfFilePath)
        {
            if (File.Exists(outputPdfFilePath))
                File.Delete(outputPdfFilePath);
            this.GeneratePdfInternal(new WkHtmlInput[1]
            {
        new WkHtmlInput(htmlFilePath)
            }, (string)null, coverHtml, outputPdfFilePath, (Stream)null);
        }

        /// <summary>
        /// Generate PDF into specified <see cref="T:System.IO.Stream" /> by several HTML documents (local files or URLs)
        /// </summary>
        /// <param name="htmlFiles">list of HTML files or URLs</param>
        /// <param name="coverHtml">first page HTML (optional, can be null)</param>
        /// <param name="output">output stream for generated PDF</param>
        public void GeneratePdfFromFiles(string[] htmlFiles, string coverHtml, Stream output)
        {
            this.GeneratePdfInternal(htmlFiles, coverHtml, output);
        }

        private WkHtmlInput[] GetWkHtmlInputFromFiles(string[] files)
        {
            WkHtmlInput[] wkHtmlInputArray = new WkHtmlInput[files.Length];
            for (int index = 0; index < wkHtmlInputArray.Length; ++index)
                wkHtmlInputArray[index] = new WkHtmlInput(files[index]);
            return wkHtmlInputArray;
        }

        /// <summary>
        /// Generate PDF into specified output file by several HTML documents (local files or URLs)
        /// </summary>
        /// <param name="htmlFiles">list of HTML files or URLs</param>
        /// <param name="coverHtml">first page HTML (optional, can be null)</param>
        /// <param name="outputPdfFilePath">path to output PDF file (if file already exists it will be removed before PDF generation)</param>
        public void GeneratePdfFromFiles(
          string[] htmlFiles,
          string coverHtml,
          string outputPdfFilePath)
        {
            this.GeneratePdfFromFiles(this.GetWkHtmlInputFromFiles(htmlFiles), coverHtml, outputPdfFilePath);
        }

        /// <summary>
        /// Generate PDF into specified output file by several HTML documents (local files or URLs)
        /// </summary>
        /// <param name="htmlFiles">list of <see cref="T:NReco.PdfGenerator.WkHtmlInput" /></param>
        /// <param name="coverHtml">first page HTML (optional, can be null)</param>
        /// <param name="outputPdfFilePath">path to output PDF file (if file already exists it will be removed before PDF generation)</param>
        public void GeneratePdfFromFiles(
          WkHtmlInput[] inputs,
          string coverHtml,
          string outputPdfFilePath)
        {
            this.License.Check();
            if (File.Exists(outputPdfFilePath))
                File.Delete(outputPdfFilePath);
            this.GeneratePdfInternal(inputs, (string)null, coverHtml, outputPdfFilePath, (Stream)null);
        }

        private void GeneratePdfInternal(string[] htmlFiles, string coverHtml, Stream output)
        {
            this.GeneratePdfInternal(this.GetWkHtmlInputFromFiles(htmlFiles), (string)null, coverHtml, "-", output);
        }

        private void CheckWkHtmlProcess()
        {
            if (!this.batchMode && this.WkHtmlToPdfProcess != null)
                throw new InvalidOperationException("WkHtmlToPdf process is already started");
        }

        private string GetTempPath()
        {
            if (!string.IsNullOrEmpty(this.TempFilesPath) && !Directory.Exists(this.TempFilesPath))
                Directory.CreateDirectory(this.TempFilesPath);
            return this.TempFilesPath ?? Path.GetTempPath();
        }

        private string GetToolExePath()
        {
            if (string.IsNullOrEmpty(this.PdfToolPath))
                throw new ArgumentException("PdfToolPath property is not initialized with path to wkhtmltopdf binaries");
            string path = Path.Combine(this.PdfToolPath, this.WkHtmlToPdfExeName);
            if (!File.Exists(path))
                throw new FileNotFoundException("Cannot find wkhtmltopdf executable: " + path);
            return path;
        }

        private string CreateTempFile(string content, string tempPath, List<string> tempFilesList)
        {
            string path = Path.Combine(tempPath, "pdfgen-" + Path.GetRandomFileName() + ".html");
            tempFilesList.Add(path);
            if (content != null)
                File.WriteAllBytes(path, Encoding.UTF8.GetBytes(content));
            return path;
        }

        private string ComposeArgs(HtmlToPdfConverter.PdfSettings pdfSettings)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (this.Quiet)
                stringBuilder.Append(" -q ");
            if (this.Orientation != PageOrientation.Default)
                stringBuilder.AppendFormat(" -O {0} ", (object)this.Orientation.ToString());
            if (this.Size != PageSize.Default)
                stringBuilder.AppendFormat(" -s {0} ", (object)this.Size.ToString());
            if (this.LowQuality)
                stringBuilder.Append(" -l ");
            if (this.Grayscale)
                stringBuilder.Append(" -g ");
            if (this.Margins != null)
            {
                if (this.Margins.Top.HasValue)
                    stringBuilder.AppendFormat((IFormatProvider)CultureInfo.InvariantCulture, " -T {0}", (object)this.Margins.Top);
                if (this.Margins.Bottom.HasValue)
                    stringBuilder.AppendFormat((IFormatProvider)CultureInfo.InvariantCulture, " -B {0}", (object)this.Margins.Bottom);
                if (this.Margins.Left.HasValue)
                    stringBuilder.AppendFormat((IFormatProvider)CultureInfo.InvariantCulture, " -L {0}", (object)this.Margins.Left);
                if (this.Margins.Right.HasValue)
                    stringBuilder.AppendFormat((IFormatProvider)CultureInfo.InvariantCulture, " -R {0}", (object)this.Margins.Right);
            }
            if (this.PageWidth.HasValue)
                stringBuilder.AppendFormat((IFormatProvider)CultureInfo.InvariantCulture, " --page-width {0}", (object)this.PageWidth);
            if (this.PageHeight.HasValue)
                stringBuilder.AppendFormat((IFormatProvider)CultureInfo.InvariantCulture, " --page-height {0}", (object)this.PageHeight);
            if (pdfSettings.HeaderFilePath != null)
                stringBuilder.AppendFormat(" --header-html \"{0}\"", (object)pdfSettings.HeaderFilePath);
            if (pdfSettings.FooterFilePath != null)
                stringBuilder.AppendFormat(" --footer-html \"{0}\"", (object)pdfSettings.FooterFilePath);
            if (!string.IsNullOrEmpty(this.CustomWkHtmlArgs))
                stringBuilder.AppendFormat(" {0} ", (object)this.CustomWkHtmlArgs);
            if (pdfSettings.CoverFilePath != null)
            {
                stringBuilder.AppendFormat(" cover \"{0}\" ", (object)pdfSettings.CoverFilePath);
                if (!string.IsNullOrEmpty(this.CustomWkHtmlCoverArgs))
                    stringBuilder.AppendFormat(" {0} ", (object)this.CustomWkHtmlCoverArgs);
            }
            if (this.GenerateToc)
            {
                stringBuilder.Append(" toc ");
                if (!string.IsNullOrEmpty(this.TocHeaderText))
                    stringBuilder.AppendFormat(" --toc-header-text \"{0}\"", (object)this.TocHeaderText.Replace("\"", "\\\""));
                if (!string.IsNullOrEmpty(this.CustomWkHtmlTocArgs))
                    stringBuilder.AppendFormat(" {0} ", (object)this.CustomWkHtmlTocArgs);
            }
            foreach (WkHtmlInput inputFile in pdfSettings.InputFiles)
            {
                stringBuilder.AppendFormat(" \"{0}\" ", (object)inputFile.Input);
                string str = inputFile.CustomWkHtmlPageArgs ?? this.CustomWkHtmlPageArgs;
                if (!string.IsNullOrEmpty(str))
                    stringBuilder.AppendFormat(" {0} ", (object)str);
                if (inputFile.HeaderFilePath != null)
                    stringBuilder.AppendFormat(" --header-html \"{0}\"", (object)inputFile.HeaderFilePath);
                if (inputFile.FooterFilePath != null)
                    stringBuilder.AppendFormat(" --footer-html \"{0}\"", (object)inputFile.FooterFilePath);
                if ((double)this.Zoom != 1.0)
                    stringBuilder.AppendFormat((IFormatProvider)CultureInfo.InvariantCulture, " --zoom {0} ", (object)this.Zoom);
            }
            stringBuilder.AppendFormat(" \"{0}\" ", (object)pdfSettings.OutputFile);
            return stringBuilder.ToString();
        }

        private void GeneratePdfInternal(
          WkHtmlInput[] htmlFiles,
          string inputContent,
          string coverHtml,
          string outputPdfFilePath,
          Stream outputStream)
        {
            if (!this.batchMode)
                this.EnsureWkHtmlLibs();
            this.CheckWkHtmlProcess();
            string tempPath = this.GetTempPath();
            HtmlToPdfConverter.PdfSettings pdfSettings = new HtmlToPdfConverter.PdfSettings()
            {
                InputFiles = htmlFiles,
                OutputFile = outputPdfFilePath
            };
            List<string> tempFilesList = new List<string>();
            pdfSettings.CoverFilePath = !string.IsNullOrEmpty(coverHtml) ? this.CreateTempFile(coverHtml, tempPath, tempFilesList) : (string)null;
            pdfSettings.HeaderFilePath = !string.IsNullOrEmpty(this.PageHeaderHtml) ? this.CreateTempFile(string.Format("<!DOCTYPE html><html><head>\r\n<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n<script>\r\nfunction subst() {{\r\n    var vars={{}};\r\n    var x=document.location.search.substring(1).split('&');\r\n\r\n    for(var i in x) {{var z=x[i].split('=',2);vars[z[0]] = unescape(z[1]);}}\r\n    var x=['frompage','topage','page','webpage','section','subsection','subsubsection'];\r\n    for(var i in x) {{\r\n      var y = document.getElementsByClassName(x[i]);\r\n      for(var j=0; j<y.length; ++j) y[j].textContent = vars[x[i]];\r\n    }}\r\n}}\r\n</script></head><body style=\"border:0; margin: 0;\" onload=\"subst()\">{0}</body></html>\r\n", (object)this.PageHeaderHtml), tempPath, tempFilesList) : (string)null;
            pdfSettings.FooterFilePath = !string.IsNullOrEmpty(this.PageFooterHtml) ? this.CreateTempFile(string.Format("<!DOCTYPE html><html><head>\r\n<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n<script>\r\nfunction subst() {{\r\n    var vars={{}};\r\n    var x=document.location.search.substring(1).split('&');\r\n\r\n    for(var i in x) {{var z=x[i].split('=',2);vars[z[0]] = unescape(z[1]);}}\r\n    var x=['frompage','topage','page','webpage','section','subsection','subsubsection'];\r\n    for(var i in x) {{\r\n      var y = document.getElementsByClassName(x[i]);\r\n      for(var j=0; j<y.length; ++j) y[j].textContent = vars[x[i]];\r\n    }}\r\n}}\r\n</script></head><body style=\"border:0; margin: 0;\" onload=\"subst()\">{0}</body></html>\r\n", (object)this.PageFooterHtml), tempPath, tempFilesList) : (string)null;
            if (pdfSettings.InputFiles != null)
            {
                foreach (WkHtmlInput inputFile in pdfSettings.InputFiles)
                {
                    inputFile.HeaderFilePath = !string.IsNullOrEmpty(inputFile.PageHeaderHtml) ? this.CreateTempFile(string.Format("<!DOCTYPE html><html><head>\r\n<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n<script>\r\nfunction subst() {{\r\n    var vars={{}};\r\n    var x=document.location.search.substring(1).split('&');\r\n\r\n    for(var i in x) {{var z=x[i].split('=',2);vars[z[0]] = unescape(z[1]);}}\r\n    var x=['frompage','topage','page','webpage','section','subsection','subsubsection'];\r\n    for(var i in x) {{\r\n      var y = document.getElementsByClassName(x[i]);\r\n      for(var j=0; j<y.length; ++j) y[j].textContent = vars[x[i]];\r\n    }}\r\n}}\r\n</script></head><body style=\"border:0; margin: 0;\" onload=\"subst()\">{0}</body></html>\r\n", (object)inputFile.PageHeaderHtml), tempPath, tempFilesList) : (string)null;
                    inputFile.FooterFilePath = !string.IsNullOrEmpty(inputFile.PageFooterHtml) ? this.CreateTempFile(string.Format("<!DOCTYPE html><html><head>\r\n<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n<script>\r\nfunction subst() {{\r\n    var vars={{}};\r\n    var x=document.location.search.substring(1).split('&');\r\n\r\n    for(var i in x) {{var z=x[i].split('=',2);vars[z[0]] = unescape(z[1]);}}\r\n    var x=['frompage','topage','page','webpage','section','subsection','subsubsection'];\r\n    for(var i in x) {{\r\n      var y = document.getElementsByClassName(x[i]);\r\n      for(var j=0; j<y.length; ++j) y[j].textContent = vars[x[i]];\r\n    }}\r\n}}\r\n</script></head><body style=\"border:0; margin: 0;\" onload=\"subst()\">{0}</body></html>\r\n", (object)inputFile.PageFooterHtml), tempPath, tempFilesList) : (string)null;
                }
            }
            try
            {
                if (inputContent != null)
                    pdfSettings.InputFiles = new WkHtmlInput[1]
                    {
            new WkHtmlInput(this.CreateTempFile(inputContent, tempPath, tempFilesList))
                    };
                if (outputStream != null)
                    pdfSettings.OutputFile = this.CreateTempFile((string)null, tempPath, tempFilesList);
                if (this.batchMode)
                    this.InvokeWkHtmlToPdfInBatch(pdfSettings);
                else
                    this.InvokeWkHtmlToPdf(pdfSettings, (string)null, (Stream)null);
                if (outputStream == null)
                    return;
                using (FileStream fileStream = new FileStream(pdfSettings.OutputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    this.CopyStream((Stream)fileStream, outputStream, 65536);
            }
            catch (Exception ex)
            {
                if (!this.batchMode)
                    this.EnsureWkHtmlProcessStopped();
                throw new Exception("Cannot generate PDF: " + ex.Message, ex);
            }
            finally
            {
                foreach (string filePath in tempFilesList)
                    this.DeleteFileIfExists(filePath);
            }
        }

        private void InvokeWkHtmlToPdfInBatch(HtmlToPdfConverter.PdfSettings pdfSettings)
        {
            this.License.Check();
            string lastErrorLine = string.Empty;
            DataReceivedEventHandler receivedEventHandler = (DataReceivedEventHandler)((o, args) =>
            {
                if (args.Data == null)
                    return;
                if (!string.IsNullOrEmpty(args.Data))
                    lastErrorLine = args.Data;
                if (this.LogReceived == null)
                    return;
                this.LogReceived((object)this, args);
            });
            if (this.WkHtmlToPdfProcess == null || this.WkHtmlToPdfProcess.HasExited)
            {
                this.WkHtmlToPdfProcess = Process.Start(new ProcessStartInfo(this.GetToolExePath(), "--read-args-from-stdin")
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(this.PdfToolPath),
                    RedirectStandardInput = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = true
                });
                if (this.ProcessPriority != ProcessPriorityClass.Normal)
                    this.WkHtmlToPdfProcess.PriorityClass = this.ProcessPriority;
                if (this.ProcessProcessorAffinity.HasValue)
                    this.WkHtmlToPdfProcess.ProcessorAffinity = this.ProcessProcessorAffinity.Value;
                this.WkHtmlToPdfProcess.BeginErrorReadLine();
            }
            this.WkHtmlToPdfProcess.ErrorDataReceived += receivedEventHandler;
            try
            {
                if (File.Exists(pdfSettings.OutputFile))
                    File.Delete(pdfSettings.OutputFile);
                this.WkHtmlToPdfProcess.StandardInput.WriteLine(this.ComposeArgs(pdfSettings).Replace('\\', '/'));
                bool flag = true;
                while (flag)
                {
                    Thread.Sleep(25);
                    if (this.WkHtmlToPdfProcess.HasExited)
                        flag = false;
                    if (File.Exists(pdfSettings.OutputFile))
                    {
                        flag = false;
                        this.WaitForFile(pdfSettings.OutputFile);
                    }
                }
                if (!this.WkHtmlToPdfProcess.HasExited)
                    return;
                this.CheckExitCode(this.WkHtmlToPdfProcess.ExitCode, lastErrorLine, File.Exists(pdfSettings.OutputFile));
            }
            finally
            {
                if (this.WkHtmlToPdfProcess != null && !this.WkHtmlToPdfProcess.HasExited)
                    this.WkHtmlToPdfProcess.ErrorDataReceived -= receivedEventHandler;
                else
                    this.EnsureWkHtmlProcessStopped();
            }
        }

        private void WaitForFile(string fullPath)
        {
            double num1 = !this.ExecutionTimeout.HasValue || !(this.ExecutionTimeout.Value != TimeSpan.Zero) ? 60000.0 : this.ExecutionTimeout.Value.TotalMilliseconds;
            int num2 = 0;
            while (num1 > 0.0)
            {
                ++num2;
                num1 -= 50.0;
                try
                {
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 100))
                    {
                        fileStream.ReadByte();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Thread.Sleep(num2 < 10 ? 50 : 100);
                }
            }
            if (num1 != 0.0 || this.WkHtmlToPdfProcess == null || this.WkHtmlToPdfProcess.HasExited)
                return;
            this.WkHtmlToPdfProcess.StandardInput.Close();
            this.WkHtmlToPdfProcess.WaitForExit();
        }

        private void InvokeWkHtmlToPdf(
          HtmlToPdfConverter.PdfSettings pdfSettings,
          string inputContent,
          Stream outputStream)
        {
            string lastErrorLine = string.Empty;
            DataReceivedEventHandler receivedEventHandler = (DataReceivedEventHandler)((o, args) =>
            {
                if (args.Data == null)
                    return;
                if (!string.IsNullOrEmpty(args.Data))
                    lastErrorLine = args.Data;
                if (this.LogReceived == null)
                    return;
                this.LogReceived((object)this, args);
            });
            byte[] buffer = inputContent != null ? Encoding.UTF8.GetBytes(inputContent) : (byte[])null;
            try
            {
                this.WkHtmlToPdfProcess = Process.Start(new ProcessStartInfo(this.GetToolExePath(), this.ComposeArgs(pdfSettings))
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(this.PdfToolPath),
                    RedirectStandardInput = buffer != null,
                    RedirectStandardOutput = outputStream != null,
                    RedirectStandardError = true
                });
                if (this.ProcessPriority != ProcessPriorityClass.Normal)
                    this.WkHtmlToPdfProcess.PriorityClass = this.ProcessPriority;
                if (this.ProcessProcessorAffinity.HasValue)
                    this.WkHtmlToPdfProcess.ProcessorAffinity = this.ProcessProcessorAffinity.Value;
                this.WkHtmlToPdfProcess.ErrorDataReceived += receivedEventHandler;
                this.WkHtmlToPdfProcess.BeginErrorReadLine();
                if (buffer != null)
                {
                    this.WkHtmlToPdfProcess.StandardInput.BaseStream.Write(buffer, 0, buffer.Length);
                    this.WkHtmlToPdfProcess.StandardInput.BaseStream.Flush();
                    this.WkHtmlToPdfProcess.StandardInput.Close();
                }
                long num = 0;
                if (outputStream != null)
                    num = (long)this.ReadStdOutToStream(this.WkHtmlToPdfProcess, outputStream);
                this.WaitWkHtmlProcessForExit();
                if (outputStream == null && File.Exists(pdfSettings.OutputFile))
                    num = new FileInfo(pdfSettings.OutputFile).Length;
                this.CheckExitCode(this.WkHtmlToPdfProcess.ExitCode, lastErrorLine, num > 0L);
            }
            finally
            {
                this.EnsureWkHtmlProcessStopped();
            }
        }

        /// <summary>
        /// Intiates PDF processing in the batch mode (generate several PDF documents using one wkhtmltopdf process)
        /// </summary>
        public void BeginBatch()
        {
            if (this.batchMode)
                throw new InvalidOperationException("HtmlToPdfConverter is already in the batch mode.");
            this.batchMode = true;
            this.EnsureWkHtmlLibs();
        }

        /// <summary>Ends PDF processing in the batch mode.</summary>
        public void EndBatch()
        {
            if (!this.batchMode)
                throw new InvalidOperationException("HtmlToPdfConverter is not in the batch mode.");
            this.batchMode = false;
            if (this.WkHtmlToPdfProcess == null)
                return;
            if (!this.WkHtmlToPdfProcess.HasExited)
            {
                this.WkHtmlToPdfProcess.StandardInput.Close();
                this.WkHtmlToPdfProcess.WaitForExit();
                this.WkHtmlToPdfProcess.Close();
            }
            this.WkHtmlToPdfProcess = (Process)null;
        }

        private void WaitWkHtmlProcessForExit()
        {
            if (this.ExecutionTimeout.HasValue)
            {
                if (!this.WkHtmlToPdfProcess.WaitForExit((int)this.ExecutionTimeout.Value.TotalMilliseconds))
                {
                    this.EnsureWkHtmlProcessStopped();
                    throw new WkHtmlToPdfException(-2, string.Format("WkHtmlToPdf process exceeded execution timeout ({0}) and was aborted", (object)this.ExecutionTimeout));
                }
            }
            else
                this.WkHtmlToPdfProcess.WaitForExit();
        }

        private void EnsureWkHtmlProcessStopped()
        {
            if (this.WkHtmlToPdfProcess == null)
                return;
            if (!this.WkHtmlToPdfProcess.HasExited)
            {
                try
                {
                    this.WkHtmlToPdfProcess.Kill();
                    this.WkHtmlToPdfProcess.Close();
                    this.WkHtmlToPdfProcess = (Process)null;
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                this.WkHtmlToPdfProcess.Close();
                this.WkHtmlToPdfProcess = (Process)null;
            }
        }

        private int ReadStdOutToStream(Process proc, Stream outputStream)
        {
            byte[] buffer = new byte[32768];
            int num = 0;
            int count;
            while ((count = proc.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputStream.Write(buffer, 0, count);
                num += count;
            }
            return num;
        }

        private void CheckExitCode(int exitCode, string lastErrLine, bool outputNotEmpty)
        {
            if (exitCode != 0 && (exitCode != 1 || Array.IndexOf<string>(HtmlToPdfConverter.ignoreWkHtmlToPdfErrLines, lastErrLine.Trim()) < 0 || !outputNotEmpty))
                throw new WkHtmlToPdfException(exitCode, lastErrLine);
        }

        private void DeleteFileIfExists(string filePath)
        {
            if (filePath == null)
                return;
            if (!File.Exists(filePath))
                return;
            try
            {
                File.Delete(filePath);
            }
            catch
            {
            }
        }

        private void CopyStream(Stream inputStream, Stream outputStream, int bufSize)
        {
            byte[] buffer = new byte[bufSize];
            int count;
            while ((count = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                outputStream.Write(buffer, 0, count);
        }

        private class PdfSettings
        {
            public string CoverFilePath;
            public string HeaderFilePath;
            public string FooterFilePath;
            public WkHtmlInput[] InputFiles;
            public string OutputFile;
        }
    }
}
